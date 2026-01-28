using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Main batch job service for Missing FK Discovery.
/// Performs data extraction, analysis, and generates results JSON.
/// </summary>
public class MissingFKScanService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly MissingFKSqlTranslationService _sqlTranslationService;
    private readonly MissingFKIgnoreService _ignoreService;
    private readonly IConnectionManager _connectionManager;
    private readonly string _outputFolder;
    private readonly string _jobId;
    private readonly MissingFKInputModel _inputModel;
    private readonly MissingFKResultsModel _resultsModel;
    
    public MissingFKScanService(
        MissingFKSqlTranslationService sqlTranslationService,
        MissingFKIgnoreService ignoreService,
        IConnectionManager connectionManager,
        string outputFolder,
        string jobId,
        MissingFKInputModel inputModel)
    {
        _sqlTranslationService = sqlTranslationService ?? throw new ArgumentNullException(nameof(sqlTranslationService));
        _ignoreService = ignoreService ?? throw new ArgumentNullException(nameof(ignoreService));
        _connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));
        _outputFolder = outputFolder ?? throw new ArgumentNullException(nameof(outputFolder));
        _jobId = jobId ?? throw new ArgumentNullException(nameof(jobId));
        _inputModel = inputModel ?? throw new ArgumentNullException(nameof(inputModel));
        
        _resultsModel = new MissingFKResultsModel
        {
            JobId = jobId,
            CompletedAtUtc = DateTime.UtcNow,
            Summary = new MissingFKSummary(),
            Candidates = new List<MissingFKCandidate>(),
            TablesWithoutKeys = new List<TableReference>()
        };
        
        Logger.Debug("MissingFKScanService initialized for job: {JobId}", jobId);
    }
    
    /// <summary>
    /// Execute the complete batch job: extract data, analyze, generate results.
    /// </summary>
    public async Task<MissingFKResultsModel> ExecuteAsync()
    {
        Logger.Info("Starting Missing FK Discovery batch job: {JobId}", _jobId);
        var startTime = DateTime.UtcNow;
        
        try
        {
            // Step 1: Extract table data to CSV files
            Logger.Info("Step 1: Extracting table data to CSV files");
            await ExtractTableDataAsync();
            
            // Step 2: Analyze data to find missing FK candidates
            Logger.Info("Step 2: Analyzing data for missing FK candidates");
            await AnalyzeMissingFKsAsync();
            
            // Step 3: Generate results JSON
            Logger.Info("Step 3: Generating results JSON");
            _resultsModel.CompletedAtUtc = DateTime.UtcNow;
            var resultsPath = Path.Combine(_outputFolder, "missing_fk_results.json");
            await SaveResultsJsonAsync(resultsPath);
            
            // Step 4: Write job log
            Logger.Info("Step 4: Writing job log");
            await WriteJobLogAsync(startTime);
            
            var duration = DateTime.UtcNow - startTime;
            Logger.Info("Missing FK Discovery batch job completed: {JobId} in {Duration}ms. Found {CandidateCount} candidates.",
                _jobId, duration.TotalMilliseconds, _resultsModel.Candidates.Count);
            
            return _resultsModel;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Missing FK Discovery batch job failed: {JobId}", _jobId);
            await WriteJobLogAsync(startTime, ex);
            throw;
        }
    }
    
    /// <summary>
    /// Extract table data to CSV files (streaming, chunked).
    /// </summary>
    private async Task ExtractTableDataAsync()
    {
        Logger.Info("Extracting data for {Count} tables", _inputModel.Tables.Count);
        
        var parallelLimit = _inputModel.Options.MaxParallelTables;
        var semaphore = new SemaphoreSlim(parallelLimit);
        var tasks = new List<Task>();
        
        foreach (var table in _inputModel.Tables)
        {
            // Skip if table should be ignored
            if (_ignoreService.ShouldIgnoreTable(table.Schema, table.Name))
            {
                Logger.Debug("Skipping ignored table: {Schema}.{Table}", table.Schema, table.Name);
                continue;
            }
            
            // Skip if row count is too low
            if (table.RowCount < _inputModel.Options.MinRowCount)
            {
                Logger.Debug("Skipping table {Schema}.{Table}: row count {Count} < min {Min}",
                    table.Schema, table.Name, table.RowCount, _inputModel.Options.MinRowCount);
                continue;
            }
            
            await semaphore.WaitAsync();
            tasks.Add(Task.Run(async () =>
            {
                try
                {
                    await ExtractSingleTableAsync(table);
                }
                finally
                {
                    semaphore.Release();
                }
            }));
        }
        
        await Task.WhenAll(tasks);
        Logger.Info("Data extraction complete for all tables");
    }
    
    /// <summary>
    /// Extract a single table to CSV file (streaming).
    /// </summary>
    private async Task ExtractSingleTableAsync(MissingFKTableMetadata table)
    {
        var fileName = SanitizeFileName($"{table.Schema}.{table.Name}.csv");
        var filePath = Path.Combine(_outputFolder, fileName);
        
        Logger.Debug("Extracting {Schema}.{Table} to {File}", table.Schema, table.Name, fileName);
        
        try
        {
            // Get SQL for data extraction
            var sql = await _sqlTranslationService.GetTranslatedStatementAsync(_connectionManager, "GetTableDataForExport");
            
            // Replace {SCHEMA} and {TABLE} placeholders
            var provider = _connectionManager.ConnectionInfo.ProviderType?.ToUpperInvariant() ?? "DB2";
            var quotedSchema = QuoteIdentifier(table.Schema, provider);
            var quotedTable = QuoteIdentifier(table.Name, provider);
            sql = sql.Replace("{SCHEMA}", quotedSchema).Replace("{TABLE}", quotedTable);
            
            Logger.Debug("Executing query for {Schema}.{Table}", table.Schema, table.Name);
            
            // Stream data directly to CSV file
            await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, FileOptions.SequentialScan);
            await using var writer = new StreamWriter(fileStream, Encoding.UTF8, 4096);
            
            // Write CSV header
            var columnNames = table.Columns.Select(c => EscapeCsvField(c.Name));
            await writer.WriteLineAsync(string.Join(",", columnNames));
            
            // Stream rows from database
            var result = await _connectionManager.ExecuteQueryAsync(sql);
            var rowCount = 0;
            
            foreach (DataRow row in result.Rows)
            {
                var fields = row.ItemArray.Select(field => EscapeCsvField(field?.ToString() ?? string.Empty));
                await writer.WriteLineAsync(string.Join(",", fields));
                rowCount++;
                
                // Log progress every 10000 rows
                if (rowCount % 10000 == 0)
                {
                    Logger.Debug("Exported {Count} rows from {Schema}.{Table}", rowCount, table.Schema, table.Name);
                }
            }
            
            await writer.FlushAsync();
            
            Logger.Info("Exported {Count} rows from {Schema}.{Table} to {File}", rowCount, table.Schema, table.Name, fileName);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to extract {Schema}.{Table}", table.Schema, table.Name);
            throw;
        }
    }
    
    /// <summary>
    /// Analyze data to find missing FK candidates.
    /// </summary>
    private async Task AnalyzeMissingFKsAsync()
    {
        Logger.Info("Starting FK candidate analysis");
        
        // Build parent key candidates (tables that can be referenced)
        var parentKeyCandidates = new Dictionary<string, List<ParentKeyCandidate>>();
        
        foreach (var table in _inputModel.Tables)
        {
            if (_ignoreService.ShouldIgnoreTable(table.Schema, table.Name))
                continue;
            
            if (table.RowCount < _inputModel.Options.MinRowCount)
                continue;
            
            var keys = FindParentKeyCandidates(table);
            if (keys.Count > 0)
            {
                var key = $"{table.Schema}.{table.Name}";
                parentKeyCandidates[key] = keys;
            }
            else
            {
                // Table has no keys
                _resultsModel.TablesWithoutKeys.Add(new TableReference
                {
                    Schema = table.Schema,
                    Name = table.Name
                });
            }
        }
        
        Logger.Debug("Found {Count} parent tables with keys", parentKeyCandidates.Count);
        
        // Find child column candidates (columns that might reference parent keys)
        var candidates = new List<MissingFKCandidate>();
        
        foreach (var childTable in _inputModel.Tables)
        {
            if (_ignoreService.ShouldIgnoreTable(childTable.Schema, childTable.Name))
                continue;
            
            if (childTable.RowCount < _inputModel.Options.MinRowCount)
                continue;
            
            // Find candidate child columns
            var childColumns = FindChildColumnCandidates(childTable);
            
            // Match child columns with parent keys
            foreach (var childCol in childColumns)
            {
                foreach (var parentEntry in parentKeyCandidates)
                {
                    var parentTableKey = parentEntry.Key;
                    var parts = parentTableKey.Split('.');
                    if (parts.Length != 2) continue;
                    
                    var parentSchema = parts[0];
                    var parentTableName = parts[1];
                    
                    // Skip if same table
                    if (parentSchema == childTable.Schema && parentTableName == childTable.Name)
                        continue;
                    
                    // Skip if already has FK
                    if (HasExistingFK(childTable, childCol.Name, parentSchema, parentTableName))
                        continue;
                    
                    foreach (var parentKey in parentEntry.Value)
                    {
                        // Check data type compatibility
                        if (!IsDataTypeCompatible(childCol, parentKey.Column))
                            continue;
                        
                        // Compute match ratio
                        var matchRatio = await ComputeMatchRatioAsync(
                            childTable, childCol.Name,
                            parentSchema, parentTableName, parentKey.Column.Name);
                        
                        if (matchRatio >= _inputModel.Options.MinMatchRatio)
                        {
                            var confidence = matchRatio >= _inputModel.Options.StrongMatchRatio ? "STRONG" : "POSSIBLE";
                            
                            candidates.Add(new MissingFKCandidate
                            {
                                ChildTable = new TableReference { Schema = childTable.Schema, Name = childTable.Name },
                                ChildColumns = new List<string> { childCol.Name },
                                ParentTable = new TableReference { Schema = parentSchema, Name = parentTableName },
                                ParentColumns = new List<string> { parentKey.Column.Name },
                                MatchRatio = matchRatio,
                                NullRatio = 0.0, // TODO: Calculate null ratio
                                Evidence = new MissingFKEvidence
                                {
                                    ChildDistinct = 0, // TODO: Calculate distinct counts
                                    ParentDistinct = 0,
                                    MissingInParent = 0
                                },
                                Confidence = confidence,
                                Recommendation = confidence == "STRONG" ? "ADD_FK" : "REVIEW"
                            });
                        }
                    }
                }
            }
        }
        
        _resultsModel.Candidates = candidates;
        _resultsModel.Summary.TablesScanned = _inputModel.Tables.Count;
        _resultsModel.Summary.CandidatesFound = candidates.Count;
        _resultsModel.Summary.StrongCandidates = candidates.Count(c => c.Confidence == "STRONG");
        _resultsModel.Summary.TablesWithoutKeys = _resultsModel.TablesWithoutKeys.Count;
        
        Logger.Info("Analysis complete: {CandidateCount} candidates found ({StrongCount} STRONG)",
            candidates.Count, _resultsModel.Summary.StrongCandidates);
    }
    
    /// <summary>
    /// Find parent key candidates for a table (PK, unique keys, or high-uniqueness columns).
    /// </summary>
    private List<ParentKeyCandidate> FindParentKeyCandidates(MissingFKTableMetadata table)
    {
        var candidates = new List<ParentKeyCandidate>();
        
        // Primary key (highest priority)
        if (table.PrimaryKey.Count > 0)
        {
            foreach (var pkCol in table.PrimaryKey)
            {
                var col = table.Columns.FirstOrDefault(c => c.Name == pkCol);
                if (col != null)
                {
                    candidates.Add(new ParentKeyCandidate
                    {
                        Column = col,
                        KeyType = "PRIMARY_KEY",
                        UniquenessRatio = 1.0 // PK is always unique
                    });
                }
            }
        }
        
        // Unique constraints
        foreach (var uniqueKey in table.UniqueKeys)
        {
            foreach (var ukCol in uniqueKey)
            {
                var col = table.Columns.FirstOrDefault(c => c.Name == ukCol);
                if (col != null && !candidates.Any(c => c.Column.Name == col.Name))
                {
                    candidates.Add(new ParentKeyCandidate
                    {
                        Column = col,
                        KeyType = "UNIQUE_CONSTRAINT",
                        UniquenessRatio = 1.0 // Unique constraint is always unique
                    });
                }
            }
        }
        
        // High-uniqueness columns (if no PK or unique keys)
        if (candidates.Count == 0)
        {
            // TODO: Calculate uniqueness ratio from data
            // For now, prefer columns with *_ID pattern
            foreach (var col in table.Columns)
            {
                if (col.Name.EndsWith("_ID", StringComparison.OrdinalIgnoreCase) &&
                    !_ignoreService.ShouldIgnoreColumn(table.Schema, table.Name, col.Name, col.DataType))
                {
                    candidates.Add(new ParentKeyCandidate
                    {
                        Column = col,
                        KeyType = "CANDIDATE_KEY",
                        UniquenessRatio = 0.95 // Assume high uniqueness for ID columns
                    });
                }
            }
        }
        
        return candidates;
    }
    
    /// <summary>
    /// Find child column candidates (columns that might reference parent keys).
    /// </summary>
    private List<MissingFKColumnInfo> FindChildColumnCandidates(MissingFKTableMetadata table)
    {
        var candidates = new List<MissingFKColumnInfo>();
        
        foreach (var col in table.Columns)
        {
            // Skip ignored columns
            if (_ignoreService.ShouldIgnoreColumn(table.Schema, table.Name, col.Name, col.DataType))
                continue;
            
            // Prefer columns with *_ID pattern
            if (col.Name.EndsWith("_ID", StringComparison.OrdinalIgnoreCase) ||
                col.Name.EndsWith("_FK", StringComparison.OrdinalIgnoreCase))
            {
                candidates.Add(col);
            }
        }
        
        return candidates;
    }
    
    /// <summary>
    /// Check if table already has a foreign key for the given relationship.
    /// </summary>
    private bool HasExistingFK(MissingFKTableMetadata table, string columnName, string refSchema, string refTable)
    {
        return table.ForeignKeys.Any(fk =>
            fk.Columns.Contains(columnName) &&
            fk.RefSchema == refSchema &&
            fk.RefTable == refTable);
    }
    
    /// <summary>
    /// Check if two columns have compatible data types.
    /// </summary>
    private bool IsDataTypeCompatible(MissingFKColumnInfo childCol, MissingFKColumnInfo parentCol)
    {
        // Exact match
        if (childCol.DataType == parentCol.DataType &&
            childCol.Length == parentCol.Length &&
            childCol.Scale == parentCol.Scale)
        {
            return true;
        }
        
        // Numeric types are generally compatible if same base type
        var numericTypes = new[] { "INTEGER", "INT", "BIGINT", "SMALLINT", "DECIMAL", "NUMERIC", "FLOAT", "REAL", "DOUBLE" };
        if (numericTypes.Contains(childCol.DataType.ToUpperInvariant()) &&
            numericTypes.Contains(parentCol.DataType.ToUpperInvariant()))
        {
            return true;
        }
        
        // VARCHAR/CHAR are compatible if lengths match or parent is longer
        if ((childCol.DataType.StartsWith("VARCHAR", StringComparison.OrdinalIgnoreCase) ||
             childCol.DataType.StartsWith("CHAR", StringComparison.OrdinalIgnoreCase)) &&
            (parentCol.DataType.StartsWith("VARCHAR", StringComparison.OrdinalIgnoreCase) ||
             parentCol.DataType.StartsWith("CHAR", StringComparison.OrdinalIgnoreCase)))
        {
            if (childCol.Length.HasValue && parentCol.Length.HasValue)
            {
                return childCol.Length <= parentCol.Length;
            }
            return true;
        }
        
        return false;
    }
    
    /// <summary>
    /// Compute match ratio: child values present in parent / total child values.
    /// </summary>
    private async Task<double> ComputeMatchRatioAsync(
        MissingFKTableMetadata childTable, string childColumn,
        string parentSchema, string parentTableName, string parentColumn)
    {
        Logger.Debug("Computing match ratio: {ChildSchema}.{ChildTable}.{ChildCol} -> {ParentSchema}.{ParentTable}.{ParentCol}",
            childTable.Schema, childTable.Name, childColumn, parentSchema, parentTableName, parentColumn);
        
        try
        {
            // Load CSV files and compare values
            var childCsvPath = Path.Combine(_outputFolder, SanitizeFileName($"{childTable.Schema}.{childTable.Name}.csv"));
            var parentCsvPath = Path.Combine(_outputFolder, SanitizeFileName($"{parentSchema}.{parentTableName}.csv"));
            
            if (!File.Exists(childCsvPath) || !File.Exists(parentCsvPath))
            {
                Logger.Warn("CSV files not found for match ratio calculation");
                return 0.0;
            }
            
            // Find column indices
            var childColIndex = await FindColumnIndexAsync(childCsvPath, childColumn);
            var parentColIndex = await FindColumnIndexAsync(parentCsvPath, parentColumn);
            
            if (childColIndex < 0 || parentColIndex < 0)
            {
                Logger.Warn("Column not found in CSV files");
                return 0.0;
            }
            
            // Load parent values into hash set
            var parentValues = new HashSet<string>();
            await foreach (var line in ReadCsvLinesAsync(parentCsvPath))
            {
                var fields = ParseCsvLine(line);
                if (fields.Count > parentColIndex)
                {
                    var value = fields[parentColIndex]?.Trim() ?? string.Empty;
                    if (!string.IsNullOrEmpty(value) && (!_inputModel.Options.IncludeNullsInMatch || value != "NULL"))
                    {
                        parentValues.Add(value);
                    }
                }
            }
            
            // Count matching child values
            long totalChildValues = 0;
            long matchingValues = 0;
            
            await foreach (var line in ReadCsvLinesAsync(childCsvPath))
            {
                var fields = ParseCsvLine(line);
                if (fields.Count > childColIndex)
                {
                    var value = fields[childColIndex]?.Trim() ?? string.Empty;
                    if (!string.IsNullOrEmpty(value))
                    {
                        totalChildValues++;
                        if (parentValues.Contains(value))
                        {
                            matchingValues++;
                        }
                    }
                }
            }
            
            var matchRatio = totalChildValues > 0 ? (double)matchingValues / totalChildValues : 0.0;
            Logger.Debug("Match ratio: {Matching}/{Total} = {Ratio}", matchingValues, totalChildValues, matchRatio);
            
            return matchRatio;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to compute match ratio");
            return 0.0;
        }
    }
    
    /// <summary>
    /// Find column index in CSV file header.
    /// </summary>
    private async Task<int> FindColumnIndexAsync(string csvPath, string columnName)
    {
        await using var fileStream = new FileStream(csvPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan);
        using var reader = new StreamReader(fileStream, Encoding.UTF8);
        
        var headerLine = await reader.ReadLineAsync();
        if (headerLine == null) return -1;
        
        var columns = ParseCsvLine(headerLine);
        for (int i = 0; i < columns.Count; i++)
        {
            if (columns[i]?.Trim().Equals(columnName, StringComparison.OrdinalIgnoreCase) == true)
            {
                return i;
            }
        }
        
        return -1;
    }
    
    /// <summary>
    /// Read CSV file line by line (async enumerable).
    /// </summary>
    private async IAsyncEnumerable<string> ReadCsvLinesAsync(string csvPath)
    {
        await using var fileStream = new FileStream(csvPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan);
        using var reader = new StreamReader(fileStream, Encoding.UTF8);
        
        // Skip header
        await reader.ReadLineAsync();
        
        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            yield return line;
        }
    }
    
    /// <summary>
    /// Parse a CSV line into fields (handles quoted fields).
    /// </summary>
    private List<string> ParseCsvLine(string line)
    {
        var fields = new List<string>();
        var currentField = new StringBuilder();
        var inQuotes = false;
        
        for (int i = 0; i < line.Length; i++)
        {
            var ch = line[i];
            
            if (ch == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    // Escaped quote
                    currentField.Append('"');
                    i++; // Skip next quote
                }
                else
                {
                    // Toggle quote state
                    inQuotes = !inQuotes;
                }
            }
            else if (ch == ',' && !inQuotes)
            {
                // Field separator
                fields.Add(currentField.ToString());
                currentField.Clear();
            }
            else
            {
                currentField.Append(ch);
            }
        }
        
        // Add last field
        fields.Add(currentField.ToString());
        
        return fields;
    }
    
    /// <summary>
    /// Save results JSON to file.
    /// </summary>
    private async Task SaveResultsJsonAsync(string filePath)
    {
        Logger.Debug("Saving results JSON to: {Path}", filePath);
        
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
        var json = JsonSerializer.Serialize(_resultsModel, options);
        await File.WriteAllTextAsync(filePath, json, Encoding.UTF8);
        
        Logger.Info("Results JSON saved: {Path} ({Length} bytes)", filePath, json.Length);
    }
    
    /// <summary>
    /// Write job log file.
    /// </summary>
    private async Task WriteJobLogAsync(DateTime startTime, Exception? error = null)
    {
        var logPath = Path.Combine(_outputFolder, $"job_{_jobId}_log.txt");
        Logger.Debug("Writing job log to: {Path}", logPath);
        
        var log = new StringBuilder();
        log.AppendLine($"Missing FK Discovery Batch Job Log");
        log.AppendLine($"Job ID: {_jobId}");
        log.AppendLine($"Started: {startTime:yyyy-MM-dd HH:mm:ss} UTC");
        log.AppendLine($"Completed: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC");
        log.AppendLine($"Duration: {DateTime.UtcNow - startTime:g}");
        log.AppendLine();
        log.AppendLine($"Summary:");
        log.AppendLine($"  Tables Scanned: {_resultsModel.Summary.TablesScanned}");
        log.AppendLine($"  Candidates Found: {_resultsModel.Summary.CandidatesFound}");
        log.AppendLine($"  Strong Candidates: {_resultsModel.Summary.StrongCandidates}");
        log.AppendLine($"  Tables Without Keys: {_resultsModel.Summary.TablesWithoutKeys}");
        log.AppendLine();
        
        if (error != null)
        {
            log.AppendLine($"ERROR: {error.Message}");
            log.AppendLine($"Stack Trace: {error.StackTrace}");
        }
        else
        {
            log.AppendLine("Status: SUCCESS");
        }
        
        await File.WriteAllTextAsync(logPath, log.ToString(), Encoding.UTF8);
        Logger.Info("Job log written: {Path}", logPath);
    }
    
    /// <summary>
    /// Sanitize file name to remove invalid characters.
    /// </summary>
    private string SanitizeFileName(string fileName)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        var sanitized = fileName;
        foreach (var ch in invalidChars)
        {
            sanitized = sanitized.Replace(ch, '_');
        }
        return sanitized;
    }
    
    /// <summary>
    /// Escape CSV field (handle quotes and commas).
    /// </summary>
    private string EscapeCsvField(string field)
    {
        if (string.IsNullOrEmpty(field))
            return string.Empty;
        
        // If field contains comma, quote, or newline, wrap in quotes and escape quotes
        if (field.Contains(',') || field.Contains('"') || field.Contains('\n') || field.Contains('\r'))
        {
            return $"\"{field.Replace("\"", "\"\"")}\"";
        }
        
        return field;
    }
    
    /// <summary>
    /// Quote identifier based on provider type.
    /// </summary>
    private string QuoteIdentifier(string identifier, string provider)
    {
        return provider.ToUpperInvariant() switch
        {
            "DB2" => identifier, // DB2 uses unquoted identifiers by default
            "POSTGRESQL" or "POSTGRES" => $"\"{identifier}\"",
            "SQLSERVER" or "MSSQL" => $"[{identifier}]",
            "ORACLE" => $"\"{identifier.ToUpperInvariant()}\"",
            "MYSQL" => $"`{identifier}`",
            _ => identifier // Default: no quoting
        };
    }
}

/// <summary>
/// Helper class for parent key candidates.
/// </summary>
internal class ParentKeyCandidate
{
    public MissingFKColumnInfo Column { get; set; } = new();
    public string KeyType { get; set; } = string.Empty; // PRIMARY_KEY, UNIQUE_CONSTRAINT, CANDIDATE_KEY
    public double UniquenessRatio { get; set; } // 0.0 to 1.0
}
