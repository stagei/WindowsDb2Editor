using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Models;
using ICSharpCode.AvalonEdit.CodeCompletion;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using IBM.Data.Db2;

namespace WindowsDb2Editor.Services;

/// <summary>
/// DB2-specific IntelliSense provider.
/// </summary>
public class Db2IntelliSenseProvider : IIntelliSenseProvider
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    // Metadata from JSON files
    private List<string> _keywords = new();
    private List<string> _dataTypes = new();
    private List<string> _functions = new();
    private List<string> _systemTables = new();
    private Dictionary<string, SqlStatementTemplate> _statementTemplates = new();
    
    // Live schema metadata
    private Dictionary<string, List<ColumnInfo>> _tableColumns = new();
    private List<string> _tableNames = new();
    private List<string> _viewNames = new();
    private List<string> _procedureNames = new();
    private List<string> _functionNames = new();
    
    public async Task LoadMetadataAsync(string keywordsFile, string statementsFile, string metadataFile)
    {
        Logger.Debug("Loading metadata from JSON files");
        
        try
        {
            // Load keywords from db2_12.1_keywords.json
            if (File.Exists(keywordsFile))
            {
                var keywordsJson = await File.ReadAllTextAsync(keywordsFile);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var keywordsData = JsonSerializer.Deserialize<KeywordsMetadata>(keywordsJson, options);
                
                if (keywordsData?.Keywords != null)
                {
                    // Get all keywords from nested structure
                    _keywords = keywordsData.GetAllKeywords();
                    _dataTypes = keywordsData.Keywords.Datatypes ?? new List<string>();
                    _functions = keywordsData.Keywords.Functions ?? new List<string>();
                    _systemTables = keywordsData.Keywords.SystemTables ?? new List<string>();
                    
                    Logger.Info("Loaded {KeywordCount} keywords, {DataTypeCount} data types, " +
                              "{FunctionCount} functions, {SystemTableCount} system tables",
                              _keywords.Count, _dataTypes.Count, _functions.Count, _systemTables.Count);
                }
                else
                {
                    Logger.Warn("Keywords file loaded but structure is null: {File}", keywordsFile);
                    LoadDefaultKeywords();
                }
            }
            else
            {
                Logger.Warn("Keywords file not found: {File}, loading defaults", keywordsFile);
                LoadDefaultKeywords();
            }
            
            // Load statement templates
            if (File.Exists(statementsFile))
            {
                // Statement templates will be parsed in future version
                Logger.Debug("Statement templates file found: {File}", statementsFile);
            }
            else
            {
                Logger.Debug("Statements file not found: {File}", statementsFile);
            }
            
            Logger.Info("Metadata loading complete - Total: {Keywords} keywords, {DataTypes} datatypes, {Functions} functions, {SystemTables} system tables",
                _keywords.Count, _dataTypes.Count, _functions.Count, _systemTables.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load metadata files");
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
            "SYSCAT.VIEWS", "SYSCAT.ROUTINES", "SYSCAT.TRIGGERS",
            "SYSIBM.SYSDUMMY1"
        };
    }
    
    public async Task LoadLiveSchemaMetadataAsync(DB2ConnectionManager connection)
    {
        Logger.Debug("Loading live schema metadata from database");
        
        try
        {
            // Load table names from SYSCAT.TABLES
            var tablesSql = "SELECT TABSCHEMA, TABNAME FROM SYSCAT.TABLES WHERE TYPE = 'T' FETCH FIRST 500 ROWS ONLY";
            using (var tableCmd = connection.CreateCommand(tablesSql))
            {
                using var tableAdapter = new DB2DataAdapter((DB2Command)tableCmd);
                var tablesTable = new System.Data.DataTable();
                await Task.Run(() => tableAdapter.Fill(tablesTable));
                
                foreach (System.Data.DataRow row in tablesTable.Rows)
                {
                    // Use raw DB2 column names for IntelliSense (internal use only)
                    var schema = (row.Table.Columns.Contains("SchemaName") ? row["SchemaName"] : row["TABSCHEMA"])?.ToString()?.Trim() ?? string.Empty;
                    var name = (row.Table.Columns.Contains("TableName") ? row["TableName"] : row["TABNAME"])?.ToString()?.Trim() ?? string.Empty;
                    _tableNames.Add($"{schema}.{name}");
                }
                Logger.Debug("Loaded {Count} table names", _tableNames.Count);
            }
            
            // Load view names from SYSCAT.TABLES
            var viewsSql = "SELECT TABSCHEMA, TABNAME FROM SYSCAT.TABLES WHERE TYPE = 'V' FETCH FIRST 500 ROWS ONLY";
            using (var viewCmd = connection.CreateCommand(viewsSql))
            {
                using var viewAdapter = new DB2DataAdapter((DB2Command)viewCmd);
                var viewsTable = new System.Data.DataTable();
                await Task.Run(() => viewAdapter.Fill(viewsTable));
                
                foreach (System.Data.DataRow row in viewsTable.Rows)
                {
                    // Use raw DB2 column names for IntelliSense (internal use only)
                    var schema = (row.Table.Columns.Contains("SchemaName") ? row["SchemaName"] : row["TABSCHEMA"])?.ToString()?.Trim() ?? string.Empty;
                    var name = (row.Table.Columns.Contains("TableName") ? row["TableName"] : row["TABNAME"])?.ToString()?.Trim() ?? string.Empty;
                    _viewNames.Add($"{schema}.{name}");
                }
                Logger.Debug("Loaded {Count} view names", _viewNames.Count);
            }
            
            // Load procedure names from SYSCAT.ROUTINES
            var proceduresSql = "SELECT ROUTINESCHEMA, ROUTINENAME FROM SYSCAT.ROUTINES WHERE ROUTINETYPE = 'P' FETCH FIRST 500 ROWS ONLY";
            using (var procCmd = connection.CreateCommand(proceduresSql))
            {
                using var procAdapter = new DB2DataAdapter((DB2Command)procCmd);
                var procTable = new System.Data.DataTable();
                await Task.Run(() => procAdapter.Fill(procTable));
                
                foreach (System.Data.DataRow row in procTable.Rows)
                {
                    // Use raw DB2 column names for IntelliSense (internal use only)
                    var schema = (row.Table.Columns.Contains("RoutineSchema") ? row["RoutineSchema"] : row["ROUTINESCHEMA"])?.ToString()?.Trim() ?? string.Empty;
                    var name = (row.Table.Columns.Contains("RoutineName") ? row["RoutineName"] : row["ROUTINENAME"])?.ToString()?.Trim() ?? string.Empty;
                    _procedureNames.Add($"{schema}.{name}");
                }
                Logger.Debug("Loaded {Count} procedure names", _procedureNames.Count);
            }
            
            // Load function names from SYSCAT.ROUTINES
            var functionsSql = "SELECT ROUTINESCHEMA, ROUTINENAME FROM SYSCAT.ROUTINES WHERE ROUTINETYPE = 'F' FETCH FIRST 500 ROWS ONLY";
            using (var funcCmd = connection.CreateCommand(functionsSql))
            {
                using var funcAdapter = new DB2DataAdapter((DB2Command)funcCmd);
                var funcTable = new System.Data.DataTable();
                await Task.Run(() => funcAdapter.Fill(funcTable));
                
                foreach (System.Data.DataRow row in funcTable.Rows)
                {
                    // Use raw DB2 column names for IntelliSense (internal use only)
                    var schema = (row.Table.Columns.Contains("RoutineSchema") ? row["RoutineSchema"] : row["ROUTINESCHEMA"])?.ToString()?.Trim() ?? string.Empty;
                    var name = (row.Table.Columns.Contains("RoutineName") ? row["RoutineName"] : row["ROUTINENAME"])?.ToString()?.Trim() ?? string.Empty;
                    _functionNames.Add($"{schema}.{name}");
                }
                Logger.Debug("Loaded {Count} function names", _functionNames.Count);
            }
            
            Logger.Info("Live schema metadata loaded successfully - {Tables} tables, {Views} views, {Procedures} procedures, {Functions} functions",
                        _tableNames.Count, _viewNames.Count, _procedureNames.Count, _functionNames.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load live schema metadata");
        }
    }
    
    public List<ICompletionData> GetCompletions(CompletionContext context)
    {
        var completions = new List<ICompletionData>();
        
        try
        {
            // Determine SQL context
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
        // Future: Query SYSCAT.ROUTINEPARMS for function parameters
        Logger.Debug("Getting signature hint for function: {Function}", functionName);
        
        // Placeholder for now
        return await Task.FromResult<FunctionSignature?>(null);
    }
    
    private SqlContext DetermineSqlContext(string text, int caretPosition)
    {
        if (caretPosition == 0 || string.IsNullOrEmpty(text))
        {
            return SqlContext.Keyword;
        }
        
        // Get text before caret
        var textBeforeCaret = text.Substring(0, Math.Min(caretPosition, text.Length));
        var lastWords = GetLastWords(textBeforeCaret, 3);
        
        // Analyze context based on last keywords
        if (lastWords.Count > 0)
        {
            var lastWord = lastWords[^1].ToUpperInvariant();
            var secondLastWord = lastWords.Count > 1 ? lastWords[^2].ToUpperInvariant() : string.Empty;
            
            // After FROM or JOIN → suggest table names
            if (lastWord == "FROM" || lastWord == "JOIN" || lastWord == "TABLE" || lastWord == "INTO")
            {
                return SqlContext.TableName;
            }
            
            // After WHERE, AND, OR → suggest column names
            if (lastWord == "WHERE" || lastWord == "AND" || lastWord == "OR" || lastWord == "ON")
            {
                return SqlContext.ColumnName;
            }
            
            // After SELECT → suggest columns or *
            if (lastWord == "SELECT")
            {
                return SqlContext.ColumnName;
            }
            
            // After AS keyword for data type in CREATE TABLE
            if ((lastWord == "AS" && secondLastWord == "TYPE") || 
                (new[] { "INT", "INTEGER", "VARCHAR", "DECIMAL", "DATE", "TIME" }.Contains(secondLastWord)))
            {
                return SqlContext.DataType;
            }
            
            // In SYSCAT context
            if (textBeforeCaret.Contains("SYSCAT.", StringComparison.OrdinalIgnoreCase))
            {
                return SqlContext.SystemCatalog;
            }
            
            // Check if in function call
            if (Regex.IsMatch(textBeforeCaret, @"\b[A-Z_]+\s*\($", RegexOptions.IgnoreCase))
            {
                return SqlContext.Function;
            }
        }
        
        return SqlContext.Keyword;
    }
    
    private List<string> GetLastWords(string text, int count)
    {
        var words = Regex.Split(text, @"[^\w\.]+")
            .Where(w => !string.IsNullOrWhiteSpace(w))
            .ToList();
        
        return words.TakeLast(count).ToList();
    }
    
    private List<ICompletionData> GetKeywordCompletions()
    {
        Logger.Debug("Generating keyword completions");
        return _keywords
            .Select(k => new Db2KeywordCompletionData
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
        Logger.Debug("Generating table name completions");
        var completions = new List<ICompletionData>();
        
        // Add table names
        completions.AddRange(_tableNames
            .Select(t => new Db2TableCompletionData
            {
                Text = t,
                Description = $"Table: {t}",
                Priority = 2.0
            }));
        
        // Add view names
        completions.AddRange(_viewNames
            .Select(v => new Db2ViewCompletionData
            {
                Text = v,
                Description = $"View: {v}",
                Priority = 2.0
            }));
        
        return completions;
    }
    
    private List<ICompletionData> GetColumnNameCompletions(string text, int caretPosition)
    {
        Logger.Debug("Generating column name completions");
        var completions = new List<ICompletionData>();
        
        // Try to find table name in FROM clause
        var tableNames = ExtractTableNamesFromQuery(text);
        
        foreach (var tableName in tableNames)
        {
            if (_tableColumns.TryGetValue(tableName, out var columns))
            {
                completions.AddRange(columns.Select(c => new Db2ColumnCompletionData
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
        
        // If no specific columns found, suggest all keywords
        if (completions.Count == 0)
        {
            completions.Add(new Db2KeywordCompletionData
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
        Logger.Debug("Generating function completions");
        var completions = new List<ICompletionData>();
        
        // Add built-in functions
        completions.AddRange(_functions
            .Select(f => new Db2FunctionCompletionData
            {
                Text = f,
                Description = $"Function: {f}",
                Priority = 2.0
            }));
        
        // Add user-defined functions
        completions.AddRange(_functionNames
            .Select(f => new Db2FunctionCompletionData
            {
                Text = f,
                Description = $"User Function: {f}",
                Priority = 2.5
            }));
        
        return completions;
    }
    
    private List<ICompletionData> GetDataTypeCompletions()
    {
        Logger.Debug("Generating data type completions");
        return _dataTypes
            .Select(dt => new Db2KeywordCompletionData
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
        Logger.Debug("Generating system catalog completions");
        return _systemTables
            .Select(st => new Db2TableCompletionData
            {
                Text = st,
                Description = $"System Table: {st}",
                Priority = 2.0
            })
            .Cast<ICompletionData>()
            .ToList();
    }
    
    private List<ICompletionData> GetGeneralCompletions()
    {
        Logger.Debug("Generating general completions");
        var completions = new List<ICompletionData>();
        
        // Mix of keywords and common suggestions
        completions.AddRange(GetKeywordCompletions().Take(20));
        completions.AddRange(GetTableNameCompletions().Take(10));
        
        return completions;
    }
    
    private List<string> ExtractTableNamesFromQuery(string sql)
    {
        var tableNames = new List<string>();
        
        try
        {
            // Simple regex to find table names after FROM and JOIN
            var fromMatches = Regex.Matches(sql, @"FROM\s+([A-Z0-9_]+\.?[A-Z0-9_]+)", RegexOptions.IgnoreCase);
            var joinMatches = Regex.Matches(sql, @"JOIN\s+([A-Z0-9_]+\.?[A-Z0-9_]+)", RegexOptions.IgnoreCase);
            
            foreach (Match match in fromMatches)
            {
                if (match.Groups.Count > 1)
                {
                    tableNames.Add(match.Groups[1].Value);
                }
            }
            
            foreach (Match match in joinMatches)
            {
                if (match.Groups.Count > 1)
                {
                    tableNames.Add(match.Groups[1].Value);
                }
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
    General
}

/// <summary>
/// Keywords metadata structure matching db2_12.1_keywords.json.
/// </summary>
public class KeywordsMetadata
{
    public string Provider { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public KeywordCategories? Keywords { get; set; }
    
    /// <summary>
    /// Get all keywords as a flat list.
    /// </summary>
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

/// <summary>
/// Nested keyword categories.
/// </summary>
public class KeywordCategories
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
/// SQL statement template.
/// </summary>
public class SqlStatementTemplate
{
    public string Name { get; set; } = string.Empty;
    public string Sql { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Parameters { get; set; } = new();
}

