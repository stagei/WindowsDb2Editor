using NLog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Models;
using ICSharpCode.AvalonEdit.CodeCompletion;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Database-agnostic SQL IntelliSense provider.
/// Loads keywords, functions, and data types from provider-specific JSON files.
/// </summary>
public class SqlIntelliSenseProvider : IIntelliSenseProvider
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly IntelliSenseDataLoader _dataLoader = new();
    
    // Current provider metadata
    private IntelliSenseMetadata? _metadata;
    private string _currentProvider = "DB2";
    private string _currentVersion = "12.1";
    
    // Cached lists from metadata
    private List<string> _keywords = new();
    private List<string> _dataTypes = new();
    private List<string> _functions = new();
    private List<string> _systemTables = new();
    private Dictionary<string, SnippetDefinition> _snippets = new();
    
    // Live schema metadata (loaded from database)
    private Dictionary<string, List<ColumnInfo>> _tableColumns = new();
    private List<string> _tableNames = new();
    private List<string> _viewNames = new();
    private List<string> _procedureNames = new();
    private List<string> _functionNames = new();
    private List<string> _schemaNames = new();
    
    /// <summary>
    /// Current database provider (e.g., "DB2", "PostgreSQL").
    /// </summary>
    public string Provider => _currentProvider;
    
    /// <summary>
    /// Current provider version.
    /// </summary>
    public string Version => _currentVersion;
    
    public async Task LoadMetadataAsync(string keywordsFile, string statementsFile, string metadataFile)
    {
        Logger.Debug("Loading metadata - Provider: {Provider}, Version: {Version}", _currentProvider, _currentVersion);
        
        try
        {
            // Try to load from JSON first
            _metadata = await _dataLoader.LoadAsync(_currentProvider, _currentVersion);
            
            if (_metadata != null)
            {
                // Populate cached lists from metadata
                _keywords = _metadata.GetAllKeywords();
                _dataTypes = _metadata.GetAllDataTypes();
                _functions = _metadata.GetAllFunctions();
                _systemTables = _metadata.GetAllSystemTables();
                _snippets = _metadata.Snippets ?? new Dictionary<string, SnippetDefinition>();
                
                Logger.Info("Loaded IntelliSense from JSON: {Keywords} keywords, {DataTypes} data types, " +
                           "{Functions} functions, {SystemTables} system tables, {Snippets} snippets",
                           _keywords.Count, _dataTypes.Count, _functions.Count, _systemTables.Count, _snippets.Count);
            }
            else
            {
                // Fall back to legacy file loading
                await LoadLegacyMetadataAsync(keywordsFile, statementsFile, metadataFile);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load metadata");
            LoadDefaultKeywords();
        }
    }
    
    /// <summary>
    /// Set the active provider and version.
    /// </summary>
    public async Task SetProviderAsync(string provider, string version)
    {
        if (_currentProvider == provider && _currentVersion == version)
            return;
        
        _currentProvider = provider;
        _currentVersion = version;
        
        Logger.Info("Switching IntelliSense to {Provider} {Version}", provider, version);
        
        // Reload metadata for new provider
        await LoadMetadataAsync(string.Empty, string.Empty, string.Empty);
    }
    
    /// <summary>
    /// Legacy metadata loading for backward compatibility.
    /// </summary>
    private async Task LoadLegacyMetadataAsync(string keywordsFile, string statementsFile, string metadataFile)
    {
        Logger.Debug("Loading legacy metadata from files");
        
        try
        {
            // Load keywords from db2_12.1_keywords.json (old format)
            if (File.Exists(keywordsFile))
            {
                var keywordsJson = await File.ReadAllTextAsync(keywordsFile);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var keywordsData = JsonSerializer.Deserialize<LegacyKeywordsMetadata>(keywordsJson, options);
                
                if (keywordsData?.Keywords != null)
                {
                    _keywords = keywordsData.GetAllKeywords();
                    _dataTypes = keywordsData.Keywords.Datatypes ?? new List<string>();
                    _functions = keywordsData.Keywords.Functions ?? new List<string>();
                    _systemTables = keywordsData.Keywords.SystemTables ?? new List<string>();
                    
                    Logger.Info("Loaded legacy keywords: {Count} total", _keywords.Count);
                }
                else
                {
                    LoadDefaultKeywords();
                }
            }
            else
            {
                LoadDefaultKeywords();
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load legacy metadata");
            LoadDefaultKeywords();
        }
    }
    
    /// <summary>
    /// Load default SQL keywords when JSON file is not available.
    /// </summary>
    private void LoadDefaultKeywords()
    {
        Logger.Info("Loading default SQL keywords");
        
        _keywords = new List<string>
        {
            "SELECT", "INSERT", "UPDATE", "DELETE", "MERGE",
            "CREATE", "ALTER", "DROP", "TRUNCATE",
            "BEGIN", "END", "COMMIT", "ROLLBACK",
            "FROM", "WHERE", "GROUP BY", "HAVING", "ORDER BY",
            "JOIN", "INNER JOIN", "LEFT JOIN", "RIGHT JOIN", "FULL JOIN",
            "ON", "AND", "OR", "NOT", "IN", "EXISTS",
            "BETWEEN", "LIKE", "IS NULL", "IS NOT NULL",
            "DISTINCT", "ALL", "AS", "UNION", "INTERSECT", "EXCEPT",
            "FETCH FIRST", "ROWS ONLY", "WITH", "VALUES",
            "SET", "DECLARE", "CALL", "RETURN"
        };
        
        _dataTypes = new List<string>
        {
            "INTEGER", "INT", "SMALLINT", "BIGINT",
            "DECIMAL", "NUMERIC", "REAL", "DOUBLE", "FLOAT",
            "CHAR", "VARCHAR", "CLOB", "BLOB",
            "DATE", "TIME", "TIMESTAMP",
            "BOOLEAN", "XML"
        };
        
        _functions = new List<string>
        {
            "COUNT", "SUM", "AVG", "MIN", "MAX",
            "SUBSTR", "TRIM", "UPPER", "LOWER", "LENGTH",
            "COALESCE", "NULLIF", "CAST",
            "CURRENT DATE", "CURRENT TIME", "CURRENT TIMESTAMP",
            "ROW_NUMBER", "RANK", "DENSE_RANK"
        };
        
        _systemTables = new List<string>
        {
            "SYSCAT.TABLES", "SYSCAT.COLUMNS", "SYSCAT.INDEXES",
            "SYSCAT.VIEWS", "SYSCAT.ROUTINES", "SYSCAT.TRIGGERS"
        };
    }
    
    public async Task LoadLiveSchemaMetadataAsync(IConnectionManager connection)
    {
        Logger.Debug("Loading live schema metadata from database");
        
        try
        {
            // Load table names
            var tablesSql = GetSystemTableQuery("tables");
            using (var tableCmd = connection.CreateCommand(tablesSql))
            {
                using var adapter = connection.CreateDataAdapter(tableCmd);
                var tablesTable = new System.Data.DataTable();
                await Task.Run(() => adapter.Fill(tablesTable));
                
                foreach (System.Data.DataRow row in tablesTable.Rows)
                {
                    var schema = GetColumnValue(row, "TABSCHEMA", "SchemaName")?.Trim() ?? string.Empty;
                    var name = GetColumnValue(row, "TABNAME", "TableName")?.Trim() ?? string.Empty;
                    _tableNames.Add($"{schema}.{name}");
                }
                Logger.Debug("Loaded {Count} table names", _tableNames.Count);
            }
            
            // Load view names
            var viewsSql = GetSystemTableQuery("views");
            using (var viewCmd = connection.CreateCommand(viewsSql))
            {
                using var adapter = connection.CreateDataAdapter(viewCmd);
                var viewsTable = new System.Data.DataTable();
                await Task.Run(() => adapter.Fill(viewsTable));
                
                foreach (System.Data.DataRow row in viewsTable.Rows)
                {
                    var schema = GetColumnValue(row, "TABSCHEMA", "SchemaName")?.Trim() ?? string.Empty;
                    var name = GetColumnValue(row, "TABNAME", "TableName")?.Trim() ?? string.Empty;
                    _viewNames.Add($"{schema}.{name}");
                }
                Logger.Debug("Loaded {Count} view names", _viewNames.Count);
            }
            
            // Load procedure names
            var proceduresSql = GetSystemTableQuery("procedures");
            using (var procCmd = connection.CreateCommand(proceduresSql))
            {
                using var adapter = connection.CreateDataAdapter(procCmd);
                var procTable = new System.Data.DataTable();
                await Task.Run(() => adapter.Fill(procTable));
                
                foreach (System.Data.DataRow row in procTable.Rows)
                {
                    var schema = GetColumnValue(row, "ROUTINESCHEMA", "RoutineSchema")?.Trim() ?? string.Empty;
                    var name = GetColumnValue(row, "ROUTINENAME", "RoutineName")?.Trim() ?? string.Empty;
                    _procedureNames.Add($"{schema}.{name}");
                }
                Logger.Debug("Loaded {Count} procedure names", _procedureNames.Count);
            }
            
            // Extract unique schema names from all loaded objects
            _schemaNames = _tableNames
                .Concat(_viewNames)
                .Concat(_procedureNames)
                .Where(obj => obj.Contains('.'))
                .Select(obj => obj.Split('.')[0])
                .Distinct()
                .OrderBy(s => s)
                .ToList();
            Logger.Debug("Extracted {Count} unique schema names", _schemaNames.Count);
            
            // Load function names
            var functionsSql = GetSystemTableQuery("functions");
            using (var funcCmd = connection.CreateCommand(functionsSql))
            {
                using var adapter = connection.CreateDataAdapter(funcCmd);
                var funcTable = new System.Data.DataTable();
                await Task.Run(() => adapter.Fill(funcTable));
                
                foreach (System.Data.DataRow row in funcTable.Rows)
                {
                    var schema = GetColumnValue(row, "ROUTINESCHEMA", "RoutineSchema")?.Trim() ?? string.Empty;
                    var name = GetColumnValue(row, "ROUTINENAME", "RoutineName")?.Trim() ?? string.Empty;
                    _functionNames.Add($"{schema}.{name}");
                }
                Logger.Debug("Loaded {Count} function names", _functionNames.Count);
            }
            
            Logger.Info("Live schema metadata loaded - {Tables} tables, {Views} views, {Procedures} procedures, {Functions} functions",
                        _tableNames.Count, _viewNames.Count, _procedureNames.Count, _functionNames.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load live schema metadata");
        }
    }
    
    /// <summary>
    /// Get provider-specific system table query.
    /// </summary>
    private string GetSystemTableQuery(string objectType)
    {
        // These queries will be moved to JSON in a future update
        return _currentProvider.ToUpperInvariant() switch
        {
            "POSTGRESQL" or "POSTGRES" => objectType switch
            {
                "tables" => "SELECT schemaname AS TABSCHEMA, tablename AS TABNAME FROM pg_tables WHERE schemaname NOT IN ('pg_catalog', 'information_schema') LIMIT 500",
                "views" => "SELECT schemaname AS TABSCHEMA, viewname AS TABNAME FROM pg_views WHERE schemaname NOT IN ('pg_catalog', 'information_schema') LIMIT 500",
                "procedures" => "SELECT routine_schema AS ROUTINESCHEMA, routine_name AS ROUTINENAME FROM information_schema.routines WHERE routine_type = 'PROCEDURE' LIMIT 500",
                "functions" => "SELECT routine_schema AS ROUTINESCHEMA, routine_name AS ROUTINENAME FROM information_schema.routines WHERE routine_type = 'FUNCTION' LIMIT 500",
                _ => string.Empty
            },
            _ => objectType switch // Default to DB2
            {
                "tables" => "SELECT TABSCHEMA, TABNAME FROM SYSCAT.TABLES WHERE TYPE = 'T' FETCH FIRST 500 ROWS ONLY",
                "views" => "SELECT TABSCHEMA, TABNAME FROM SYSCAT.TABLES WHERE TYPE = 'V' FETCH FIRST 500 ROWS ONLY",
                "procedures" => "SELECT ROUTINESCHEMA, ROUTINENAME FROM SYSCAT.ROUTINES WHERE ROUTINETYPE = 'P' FETCH FIRST 500 ROWS ONLY",
                "functions" => "SELECT ROUTINESCHEMA, ROUTINENAME FROM SYSCAT.ROUTINES WHERE ROUTINETYPE = 'F' FETCH FIRST 500 ROWS ONLY",
                _ => string.Empty
            }
        };
    }
    
    /// <summary>
    /// Get column value with fallback column name.
    /// </summary>
    private string? GetColumnValue(System.Data.DataRow row, string primaryColumn, string fallbackColumn)
    {
        if (row.Table.Columns.Contains(primaryColumn))
            return row[primaryColumn]?.ToString();
        if (row.Table.Columns.Contains(fallbackColumn))
            return row[fallbackColumn]?.ToString();
        return null;
    }
    
    public List<ICompletionData> GetCompletions(CompletionContext context)
    {
        var completions = new List<ICompletionData>();
        
        try
        {
            var sqlContext = DetermineSqlContext(context.Text, context.CaretPosition);
            Logger.Debug("SQL context: {Context}", sqlContext);
            
            completions = sqlContext switch
            {
                SqlContext.Keyword => GetKeywordCompletions(),
                SqlContext.TableName => GetTableNameCompletions(),
                SqlContext.ColumnName => GetColumnNameCompletions(context.Text, context.CaretPosition),
                SqlContext.Function => GetFunctionCompletions(),
                SqlContext.DataType => GetDataTypeCompletions(),
                SqlContext.SystemCatalog => GetSystemCatalogCompletions(),
                SqlContext.Snippet => GetSnippetCompletions(),
                SqlContext.SchemaObject => GetSchemaObjectCompletions(context.Text, context.CaretPosition),
                _ => GetGeneralCompletions()
            };
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate completions");
        }
        
        return completions;
    }
    
    public async Task<FunctionSignature?> GetSignatureHintAsync(string functionName)
    {
        Logger.Debug("Getting signature hint for function: {Function}", functionName);
        return await Task.FromResult<FunctionSignature?>(null);
    }
    
    private SqlContext DetermineSqlContext(string text, int caretPosition)
    {
        if (caretPosition == 0 || string.IsNullOrEmpty(text))
            return SqlContext.Keyword;
        
        var textBeforeCaret = text.Substring(0, Math.Min(caretPosition, text.Length));
        
        // Check if we're typing after a schema name followed by a period
        // e.g., "MYSCHEMA." <- caret here
        var schemaObjectMatch = Regex.Match(textBeforeCaret, @"(\w+)\.$");
        if (schemaObjectMatch.Success)
        {
            var possibleSchema = schemaObjectMatch.Groups[1].Value;
            // Check if this is a known schema
            if (_schemaNames.Contains(possibleSchema, StringComparer.OrdinalIgnoreCase))
            {
                Logger.Debug("Detected schema.object context for schema: {Schema}", possibleSchema);
                return SqlContext.SchemaObject;
            }
        }
        
        var lastWords = GetLastWords(textBeforeCaret, 3);
        
        if (lastWords.Count > 0)
        {
            var lastWord = lastWords[^1].ToUpperInvariant();
            var secondLastWord = lastWords.Count > 1 ? lastWords[^2].ToUpperInvariant() : string.Empty;
            
            if (lastWord == "FROM" || lastWord == "JOIN" || lastWord == "TABLE" || lastWord == "INTO")
                return SqlContext.TableName;
            
            if (lastWord == "WHERE" || lastWord == "AND" || lastWord == "OR" || lastWord == "ON")
                return SqlContext.ColumnName;
            
            if (lastWord == "SELECT")
                return SqlContext.ColumnName;
            
            if (lastWord == "AS" && secondLastWord == "TYPE")
                return SqlContext.DataType;
            
            if (textBeforeCaret.Contains("SYSCAT.", StringComparison.OrdinalIgnoreCase) ||
                textBeforeCaret.Contains("pg_catalog.", StringComparison.OrdinalIgnoreCase) ||
                textBeforeCaret.Contains("information_schema.", StringComparison.OrdinalIgnoreCase))
                return SqlContext.SystemCatalog;
        }
        
        return SqlContext.Keyword;
    }
    
    private List<string> GetLastWords(string text, int count)
    {
        return Regex.Split(text, @"[^\w\.]+")
            .Where(w => !string.IsNullOrWhiteSpace(w))
            .TakeLast(count)
            .ToList();
    }
    
    private List<ICompletionData> GetKeywordCompletions()
    {
        return _keywords
            .Select(k => new SqlKeywordCompletionData
            {
                Text = k,
                Description = $"SQL Keyword: {k}",
                Priority = 1.0
            })
            .Cast<ICompletionData>()
            .ToList();
    }
    
    private List<ICompletionData> GetTableNameCompletions()
    {
        var completions = new List<ICompletionData>();
        
        completions.AddRange(_tableNames.Select(t => new SqlTableCompletionData
        {
            Text = t,
            Description = $"Table: {t}",
            Priority = 2.0
        }));
        
        completions.AddRange(_viewNames.Select(v => new SqlViewCompletionData
        {
            Text = v,
            Description = $"View: {v}",
            Priority = 2.0
        }));
        
        return completions;
    }
    
    private List<ICompletionData> GetColumnNameCompletions(string text, int caretPosition)
    {
        var completions = new List<ICompletionData>();
        var tableNames = ExtractTableNamesFromQuery(text);
        
        foreach (var tableName in tableNames)
        {
            if (_tableColumns.TryGetValue(tableName, out var columns))
            {
                completions.AddRange(columns.Select(c => new SqlColumnCompletionData
                {
                    ColumnName = c.Name,
                    DataType = c.DataType,
                    IsNullable = c.IsNullable,
                    IsPrimaryKey = c.IsPrimaryKey,
                    TableName = tableName,
                    Priority = 3.0
                }));
            }
        }
        
        if (completions.Count == 0)
        {
            completions.Add(new SqlKeywordCompletionData
            {
                Text = "*",
                Description = "Select all columns",
                Priority = 10.0
            });
        }
        
        return completions;
    }
    
    private List<ICompletionData> GetFunctionCompletions()
    {
        var completions = new List<ICompletionData>();
        
        completions.AddRange(_functions.Select(f => new SqlFunctionCompletionData
        {
            Text = f,
            Description = $"Function: {f}",
            Priority = 2.0
        }));
        
        completions.AddRange(_functionNames.Select(f => new SqlFunctionCompletionData
        {
            Text = f,
            Description = $"User Function: {f}",
            Priority = 2.5
        }));
        
        return completions;
    }
    
    private List<ICompletionData> GetDataTypeCompletions()
    {
        return _dataTypes
            .Select(dt => new SqlDataTypeCompletionData
            {
                Text = dt,
                Description = $"Data Type: {dt}",
                Priority = 1.5
            })
            .Cast<ICompletionData>()
            .ToList();
    }
    
    private List<ICompletionData> GetSystemCatalogCompletions()
    {
        return _systemTables
            .Select(st => new SqlSystemTableCompletionData
            {
                Text = st,
                Description = $"System Table: {st}",
                Priority = 2.0
            })
            .Cast<ICompletionData>()
            .ToList();
    }
    
    private List<ICompletionData> GetSnippetCompletions()
    {
        return _snippets
            .Select(s => new SqlSnippetCompletionData
            {
                Trigger = s.Value.Trigger,
                Template = s.Value.Template,
                Description = s.Value.Description,
                Text = s.Value.Trigger,
                Priority = 1.5
            })
            .Cast<ICompletionData>()
            .ToList();
    }
    
    private List<ICompletionData> GetGeneralCompletions()
    {
        var completions = new List<ICompletionData>();
        
        // Add schemas at the top with highest priority
        completions.AddRange(_schemaNames.Select(s => new SqlSchemaCompletionData
        {
            Text = s,
            Description = $"Schema: {s}",
            Priority = 3.0 // Higher priority than tables/keywords
        }));
        
        completions.AddRange(GetKeywordCompletions().Take(20));
        completions.AddRange(GetSnippetCompletions().Take(10));
        completions.AddRange(GetTableNameCompletions().Take(10));
        return completions;
    }
    
    /// <summary>
    /// Get completions for objects within a specific schema (after schema.period)
    /// </summary>
    private List<ICompletionData> GetSchemaObjectCompletions(string text, int caretPosition)
    {
        var completions = new List<ICompletionData>();
        
        try
        {
            var textBeforeCaret = text.Substring(0, Math.Min(caretPosition, text.Length));
            var schemaObjectMatch = Regex.Match(textBeforeCaret, @"(\w+)\.$");
            
            if (schemaObjectMatch.Success)
            {
                var schemaName = schemaObjectMatch.Groups[1].Value;
                Logger.Debug("Showing objects for schema: {Schema}", schemaName);
                
                // Filter tables by schema
                var schemaTables = _tableNames
                    .Where(t => t.StartsWith($"{schemaName}.", StringComparison.OrdinalIgnoreCase))
                    .Select(t => t.Substring(t.IndexOf('.') + 1)) // Remove schema prefix
                    .Select(t => new SqlTableCompletionData
                    {
                        Text = t,
                        Description = $"Table: {schemaName}.{t}",
                        Priority = 3.0
                    });
                completions.AddRange(schemaTables);
                
                // Filter views by schema
                var schemaViews = _viewNames
                    .Where(v => v.StartsWith($"{schemaName}.", StringComparison.OrdinalIgnoreCase))
                    .Select(v => v.Substring(v.IndexOf('.') + 1)) // Remove schema prefix
                    .Select(v => new SqlViewCompletionData
                    {
                        Text = v,
                        Description = $"View: {schemaName}.{v}",
                        Priority = 2.5
                    });
                completions.AddRange(schemaViews);
                
                // Filter procedures by schema
                var schemaProcedures = _procedureNames
                    .Where(p => p.StartsWith($"{schemaName}.", StringComparison.OrdinalIgnoreCase))
                    .Select(p => p.Substring(p.IndexOf('.') + 1)) // Remove schema prefix
                    .Select(p => new SqlProcedureCompletionData
                    {
                        Text = p,
                        Description = $"Procedure: {schemaName}.{p}",
                        Priority = 2.0
                    });
                completions.AddRange(schemaProcedures);
                
                Logger.Debug("Found {Count} objects in schema {Schema}", completions.Count, schemaName);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get schema object completions");
        }
        
        return completions;
    }
    
    private List<string> ExtractTableNamesFromQuery(string sql)
    {
        var tableNames = new List<string>();
        
        try
        {
            var fromMatches = Regex.Matches(sql, @"FROM\s+([A-Z0-9_]+\.?[A-Z0-9_]+)", RegexOptions.IgnoreCase);
            var joinMatches = Regex.Matches(sql, @"JOIN\s+([A-Z0-9_]+\.?[A-Z0-9_]+)", RegexOptions.IgnoreCase);
            
            foreach (Match match in fromMatches)
            {
                if (match.Groups.Count > 1)
                    tableNames.Add(match.Groups[1].Value);
            }
            
            foreach (Match match in joinMatches)
            {
                if (match.Groups.Count > 1)
                    tableNames.Add(match.Groups[1].Value);
            }
        }
        catch (Exception ex)
        {
            Logger.Warn(ex, "Failed to extract table names from query");
        }
        
        return tableNames.Distinct().ToList();
    }
}

/// <summary>
/// SQL context types for intelligent completion.
/// </summary>
public enum SqlContext
{
    Keyword,
    TableName,
    ColumnName,
    Function,
    DataType,
    SystemCatalog,
    Snippet,
    General,
    SchemaObject
}

#region Legacy Compatibility

/// <summary>
/// Legacy keywords metadata structure (for backward compatibility with db2_12.1_keywords.json).
/// </summary>
public class LegacyKeywordsMetadata
{
    public string Provider { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public LegacyKeywordCategories? Keywords { get; set; }
    
    public List<string> GetAllKeywords()
    {
        var all = new List<string>();
        if (Keywords != null)
        {
            if (Keywords.Statements != null) all.AddRange(Keywords.Statements);
            if (Keywords.Clauses != null) all.AddRange(Keywords.Clauses);
            if (Keywords.Operators != null) all.AddRange(Keywords.Operators);
            if (Keywords.Constraints != null) all.AddRange(Keywords.Constraints);
            if (Keywords.Modifiers != null) all.AddRange(Keywords.Modifiers);
        }
        return all.Distinct().ToList();
    }
}

public class LegacyKeywordCategories
{
    public List<string>? Statements { get; set; }
    public List<string>? Clauses { get; set; }
    public List<string>? Datatypes { get; set; }
    public List<string>? Functions { get; set; }
    public List<string>? Operators { get; set; }
    
    [System.Text.Json.Serialization.JsonPropertyName("system_tables")]
    public List<string>? SystemTables { get; set; }
    
    public List<string>? Constraints { get; set; }
    public List<string>? Modifiers { get; set; }
}

/// <summary>
/// Alias for backward compatibility - redirects to SqlIntelliSenseProvider.
/// </summary>
public class Db2IntelliSenseProvider : SqlIntelliSenseProvider
{
    public Db2IntelliSenseProvider() : base()
    {
    }
}

#endregion
