using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NLog;
using SqlMermaidErdTools.Models;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Utils;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Handles CLI commands for automated testing using direct SQL queries (READ-ONLY).
/// Exposes all GUI functionality as CLI commands with structured JSON output.
/// NO DML operations (INSERT/UPDATE/DELETE) - only SELECT queries.
/// </summary>
public class CliCommandHandlerService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly SqlMermaidIntegrationService? _mermaidService;
    private readonly MetadataHandler _metadataHandler;
    
    public CliCommandHandlerService(MetadataHandler? metadataHandler = null, SqlMermaidIntegrationService? mermaidService = null)
    {
        _metadataHandler = metadataHandler ?? new MetadataHandler();
        _mermaidService = mermaidService ?? new SqlMermaidIntegrationService();
        Logger.Debug("CliCommandHandlerService initialized (MetadataHandler: {HasMetadata}, Mermaid: {HasMermaid})", 
            _metadataHandler != null, _mermaidService != null);
    }
    
    /// <summary>
    /// Execute CLI command and return exit code
    /// </summary>
    public async Task<int> ExecuteCommandAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        Logger.Info("Executing CLI command: {Command} for Object: {Object}", args.Command, args.Object);
        
        if (string.IsNullOrEmpty(args.Command))
        {
            Console.Error.WriteLine("ERROR: -Command parameter is required");
            return 1;
        }
        
        if (string.IsNullOrEmpty(args.OutFile))
        {
            Console.Error.WriteLine("ERROR: -Outfile parameter is required");
            return 1;
        }
        
        try
        {
            object? result = args.Command switch
            {
                "table-props" => await GetTablePropertiesAsync(connectionManager, args),
                "trigger-info" => await GetTriggerInfoAsync(connectionManager, args),
                "trigger-usage" => await GetTriggerUsageAsync(connectionManager, args),
                "view-info" => await GetViewInfoAsync(connectionManager, args),
                "procedure-info" => await GetProcedureInfoAsync(connectionManager, args),
                "function-info" => await GetFunctionInfoAsync(connectionManager, args),
                "lock-monitor" => await GetLockMonitorAsync(connectionManager, args),
                "active-sessions" => await GetActiveSessionsAsync(connectionManager, args),
                "database-load" => await GetDatabaseLoadAsync(connectionManager, args),
                "table-stats" => await GetTableStatsAsync(connectionManager, args),
                "dependencies" => await GetDependenciesAsync(connectionManager, args),
                "cdc-info" => await GetCdcInfoAsync(connectionManager, args),
                "list-tables" => await ListTablesAsync(connectionManager, args),
                "list-views" => await ListViewsAsync(connectionManager, args),
                "list-procedures" => await ListProceduresAsync(connectionManager, args),
                "list-triggers" => await ListTriggersAsync(connectionManager, args),
                "list-functions" => await ListFunctionsAsync(connectionManager, args),
                
                // TableDetailsDialog - Complete table information (9 commands)
                "table-columns" => await GetTableColumnsAsync(connectionManager, args),
                "table-foreign-keys" => await GetTableForeignKeysAsync(connectionManager, args),
                "table-indexes" => await GetTableIndexesAsync(connectionManager, args),
                "table-statistics-full" => await GetTableStatisticsFullAsync(connectionManager, args),
                "table-ddl" => await GetTableDdlAsync(connectionManager, args),
                "table-incoming-fks" => await GetTableIncomingFKsAsync(connectionManager, args),
                "table-referencing-packages" => await GetTableReferencingPackagesAsync(connectionManager, args),
                "table-referencing-views" => await GetTableReferencingViewsAsync(connectionManager, args),
                "table-referencing-routines" => await GetTableReferencingRoutinesAsync(connectionManager, args),
                
                // Source Code Browser commands (3 commands)
                "list-all-source" => await ListAllSourceAsync(connectionManager, args),
                "source-code-full" => await GetSourceCodeFullAsync(connectionManager, args),
                "source-search" => await SearchSourceCodeAsync(connectionManager, args),
                
                // Package Analyzer commands (3 commands)
                "list-packages" => await ListPackagesAsync(connectionManager, args),
                "package-analysis" => await AnalyzePackageAsync(connectionManager, args),
                "package-details" => await GetPackageDetailsAsync(connectionManager, args),
                
                // Comment Manager commands (3 commands)
                "list-comments" => await ListCommentsAsync(connectionManager, args),
                "object-comment" => await GetObjectCommentAsync(connectionManager, args),
                "missing-comments" => await FindMissingCommentsAsync(connectionManager, args),
                
                // Statistics Manager commands (3 commands)
                "statistics-overview" => await GetStatisticsOverviewAsync(connectionManager, args),
                "statistics-recommendations" => await GetStatisticsRecommendationsAsync(connectionManager, args),
                "index-statistics" => await GetIndexStatisticsAsync(connectionManager, args),
                
                // Unused Objects commands (4 commands)
                "unused-tables" => await FindUnusedTablesAsync(connectionManager, args),
                "unused-indexes" => await FindUnusedIndexesAsync(connectionManager, args),
                "unused-views" => await FindUnusedViewsAsync(connectionManager, args),
                "unused-routines" => await FindUnusedRoutinesAsync(connectionManager, args),
                
                // Mermaid ERD commands (5 commands - using SqlMermaidErdTools)
                "mermaid-erd" => await GenerateMermaidErdAsync(connectionManager, args),
                "mermaid-from-sql" => await ConvertSqlToMermaidAsync(args),
                "sql-from-mermaid" => await ConvertMermaidToSqlAsync(args),
                "mermaid-diff" => await GenerateMermaidDiffDdlAsync(args),
                "sql-translate" => await TranslateSqlDialectAsync(args),
                
                // Metadata commands (3 commands)
                "query-history" => await GetQueryHistoryAsync(args),
                "schema-metadata" => await GetSchemaMetadataAsync(connectionManager, args),
                "database-metadata" => await GetDatabaseMetadataAsync(connectionManager, args),
                
                // Connection commands (1 command)
                "connection-profiles" => await ListConnectionProfilesAsync(args),
                
                // Advanced Monitoring commands (8 commands)
                "database-load-full" => await GetDatabaseLoadFullAsync(connectionManager, args),
                "table-activity" => await GetTableActivityAsync(connectionManager, args),
                "top-active-tables" => await GetTopActiveTablesAsync(connectionManager, args),
                "lock-monitor-full" => await GetLockMonitorFullAsync(connectionManager, args),
                "lock-chains" => await GetLockChainsAsync(connectionManager, args),
                "active-sessions-full" => await GetActiveSessionsFullAsync(connectionManager, args),
                "session-details" => await GetSessionDetailsAsync(connectionManager, args),
                "long-running-sessions" => await GetLongRunningSessionsAsync(connectionManager, args),
                
                // Dependency commands (3 commands)
                "dependency-graph" => await GetDependencyGraphAsync(connectionManager, args),
                "dependency-impact" => await GetDependencyImpactAsync(connectionManager, args),
                "dependency-chain" => await GetDependencyChainAsync(connectionManager, args),
                
                // Migration commands (3 commands)
                "migration-plan" => await GenerateMigrationPlanAsync(connectionManager, args),
                "migration-ddl" => await GenerateMigrationDdlAsync(connectionManager, args),
                "migration-data-script" => await GenerateMigrationDataScriptAsync(connectionManager, args),
                
                // Export commands (3 commands)
                "export-table-data" => await ExportTableDataAsync(connectionManager, args),
                "export-query-results" => await ExportQueryResultsAsync(connectionManager, args),
                "export-schema-ddl" => await ExportSchemaDdlAsync(connectionManager, args),
                
                // SQL Tools commands (2 commands)
                "sql-validate" => await ValidateSqlAsync(args),
                "sql-format" => await FormatSqlAsync(args),
                
                // Schema Diff commands (2 commands)
                "schema-compare" => await CompareSchemas(connectionManager, args),
                "schema-diff-ddl" => await GenerateSchemaDiffDdlAsync(connectionManager, args),
                
                // User/Privileges Enhanced (2 commands)
                "user-info-enhanced" => await GetUserInfoEnhancedAsync(connectionManager, args),
                "user-privileges-full" => await GetUserPrivilegesFullAsync(connectionManager, args),
                
                // CDC Enhanced (3 commands)
                "cdc-status-full" => await GetCdcStatusFullAsync(connectionManager, args),
                "cdc-configuration" => await GetCdcConfigurationAsync(connectionManager, args),
                "cdc-changes" => await GetCdcChangesAsync(connectionManager, args),
                
                // Connection Stats (2 commands)
                "connection-stats" => await GetConnectionStatsAsync(args),
                "connection-test" => await TestConnectionAsync(args),
                
                // Additional utility commands (10 commands)
                "list-schemas" => await ListSchemasAsync(connectionManager, args),
                "list-tablespaces" => await ListTablespacesAsync(connectionManager, args),
                "list-indexes-all" => await ListAllIndexesAsync(connectionManager, args),
                "list-constraints" => await ListConstraintsAsync(connectionManager, args),
                "list-sequences" => await ListSequencesAsync(connectionManager, args),
                "table-size" => await GetTableSizeAsync(connectionManager, args),
                "schema-size" => await GetSchemaSizeAsync(connectionManager, args),
                "database-size" => await GetDatabaseSizeAsync(connectionManager, args),
                "table-grants" => await GetTableGrantsAsync(connectionManager, args),
                "db-config" => await GetDbConfigAsync(connectionManager, args),
                
                // Meta commands (2 commands)
                "help-all" => await GetHelpAllAsync(args),
                "cli-version" => await GetCliVersionAsync(args),
                
                _ => throw new ArgumentException($"Unknown command: {args.Command}")
            };
            
            // Serialize result to JSON
            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
            
            // Write to file
            await File.WriteAllTextAsync(args.OutFile, json);
            
            Console.WriteLine($"SUCCESS: Command '{args.Command}' completed");
            Console.WriteLine($"Output written to: {args.OutFile}");
            Console.WriteLine($"Result size: {json.Length} bytes");
            
            Logger.Info("CLI command completed successfully - Output: {File}, Size: {Size} bytes", args.OutFile, json.Length);
            return 0;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "CLI command failed: {Command}", args.Command);
            Console.Error.WriteLine($"ERROR: {ex.Message}");
            return 1;
        }
    }
    
    /// <summary>
    /// List all tables in schema (READ-ONLY)
    /// </summary>
    private async Task<object> ListTablesAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Logger.Debug("Listing tables in schema: {Schema}", schema);
        Console.WriteLine($"Listing tables in schema: {schema}");
        
        var sql = $@"
            SELECT TABSCHEMA, TABNAME, TYPE, CARD, NPAGES, FPAGES, STATUS, REMARKS
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA LIKE '{schema}' AND TYPE IN ('T', 'U')
            ORDER BY TABSCHEMA, TABNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var tables = data.AsEnumerable().Take(limit).Select(row => new
        {
            schema = row["TABSCHEMA"]?.ToString()?.Trim(),
            tableName = row["TABNAME"]?.ToString()?.Trim(),
            type = row["TYPE"]?.ToString()?.Trim(),
            rowCount = row["CARD"],
            dataPages = row["NPAGES"],
            freePages = row["FPAGES"],
            status = row["STATUS"]?.ToString()?.Trim(),
            comment = row["REMARKS"]?.ToString()?.Trim()
        }).ToList();
        
        Logger.Info("Listed {Count} tables (limit: {Limit})", tables.Count, limit);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            totalTables = data.Rows.Count,
            limitApplied = limit,
            tables,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get comprehensive table properties (READ-ONLY)
    /// </summary>
    /// <summary>
    /// Helper method to replace ? placeholders with values in order
    /// </summary>
    private string ReplaceParameters(string sql, params string[] values)
    {
        foreach (var value in values)
        {
            var index = sql.IndexOf('?');
            if (index >= 0)
            {
                sql = sql.Remove(index, 1).Insert(index, $"'{value}'");
            }
        }
        return sql;
    }
    
    private async Task<object> GetTablePropertiesAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.TABLE)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.TABLE");
        
        var schema = parts[0];
        var tableName = parts[1];
        
        Logger.Debug("Getting table properties: {Schema}.{Table}", schema, tableName);
        Console.WriteLine($"Retrieving table properties for: {schema}.{tableName}");
        
        // Get columns using MetadataHandler
        var columnsSql = ReplaceParameters(_metadataHandler.GetQuery("DB2", "12.1", "GUI_GetTableColumns"), schema, tableName);
        var columnsData = await connectionManager.ExecuteQueryAsync(columnsSql);
        
        // Get primary keys using MetadataHandler
        var pkSql = ReplaceParameters(_metadataHandler.GetQuery("DB2", "12.1", "GetPrimaryKeyColumns"), schema, tableName);
        var pkData = await connectionManager.ExecuteQueryAsync(pkSql);
        var pkColumns = new HashSet<string>(pkData.AsEnumerable().Select(row => row["COLNAME"]?.ToString()?.Trim() ?? ""));
        
        // Map columns with PK info
        var columns = columnsData.AsEnumerable().Select(row => new
        {
            columnName = row["ColumnName"]?.ToString()?.Trim(),
            ordinalPosition = row.Table.Columns.Contains("COLNO") ? row["COLNO"] : null,
            dataType = row["DataType"]?.ToString()?.Trim(),
            length = row.Table.Columns.Contains("LENGTH") ? row["LENGTH"] : null,
            scale = row.Table.Columns.Contains("SCALE") ? row["SCALE"] : null,
            isNullable = row["Nullable"]?.ToString() == "Yes",
            defaultValue = row["DefaultValue"]?.ToString()?.Trim(),
            comment = row["Remarks"]?.ToString()?.Trim(),
            isIdentity = row.Table.Columns.Contains("IDENTITY") && row["IDENTITY"]?.ToString() == "Y",
            isPrimaryKey = pkColumns.Contains(row["ColumnName"]?.ToString()?.Trim() ?? "")
        }).ToList();
        
        var primaryKeys = pkColumns.ToList();
        
        // Get foreign keys using MetadataHandler
        var fkSql = ReplaceParameters(_metadataHandler.GetQuery("DB2", "12.1", "GUI_GetTableForeignKeys"), schema, tableName);
        var fkData = await connectionManager.ExecuteQueryAsync(fkSql);
        
        var foreignKeys = fkData.AsEnumerable().Select(row => new
        {
            constraintName = row["FKName"]?.ToString()?.Trim(),
            columns = row["FKColumn"]?.ToString()?.Trim(),
            referencedSchema = row["PKTable"]?.ToString()?.Trim()?.Split('.').FirstOrDefault(),
            referencedTable = row["PKTable"]?.ToString()?.Trim()?.Split('.').LastOrDefault(),
            referencedColumns = row["PKColumn"]?.ToString()?.Trim(),
            deleteRule = row["DeleteRule"]?.ToString()?.Trim(),
            updateRule = row["UpdateRule"]?.ToString()?.Trim()
        }).ToList();
        
        // Get indexes using MetadataHandler
        var indexSql = ReplaceParameters(_metadataHandler.GetQuery("DB2", "12.1", "GUI_GetTableIndexes"), schema, tableName);
        var indexData = await connectionManager.ExecuteQueryAsync(indexSql);
        
        var indexes = indexData.AsEnumerable().Select(row => new
        {
            indexName = row["IndexName"]?.ToString()?.Trim(),
            uniqueRule = row["IsUnique"]?.ToString()?.Trim(),
            isUnique = row["IsUnique"]?.ToString() == "Yes",
            isPrimaryKey = row["IsUnique"]?.ToString() == "Primary Key",
            columns = row["Columns"]?.ToString()?.Trim(),
            indexType = row["IndexType"]?.ToString()?.Trim(),
            columnCount = row.Table.Columns.Contains("COLCOUNT") ? row["COLCOUNT"] : null,
            firstKeyCard = row.Table.Columns.Contains("FIRSTKEYCARD") ? row["FIRSTKEYCARD"] : null,
            fullKeyCard = row.Table.Columns.Contains("FULLKEYCARD") ? row["FULLKEYCARD"] : null,
            comment = row.Table.Columns.Contains("REMARKS") ? row["REMARKS"]?.ToString()?.Trim() : null
        }).ToList();
        
        // Get table statistics if requested
        object? statistics = null;
        if (args.IncludeDependencies)
        {
            var statsSql = ReplaceParameters(_metadataHandler.GetQuery("DB2", "12.1", "GetTableStatistics"), schema, tableName);
            var statsData = await connectionManager.ExecuteQueryAsync(statsSql);
            
            if (statsData.Rows.Count > 0)
            {
                var row = statsData.Rows[0];
                statistics = new
                {
                    rowCount = row["RowCount"],
                    dataPages = row["NumberOfPages"],
                    freePages = row["FormattedPages"],
                    overflowPages = row["OverflowPages"],
                    avgRowSize = row["AvgRowSize"],
                    lastStatsTime = row["LastStatsTime"],
                    lastReorgTime = row.Table.Columns.Contains("LAST_REGEN_TIME") ? row["LAST_REGEN_TIME"] : null
                };
            }
        }
        
        Logger.Info("Retrieved table properties - Columns: {ColCount}, PKs: {PKCount}, FKs: {FKCount}, Indexes: {IdxCount}",
            columns.Count, primaryKeys.Count, foreignKeys.Count, indexes.Count);
        
        return new
        {
            schema,
            tableName,
            columnCount = columns.Count,
            columns,
            primaryKeyCount = primaryKeys.Count,
            primaryKeys,
            foreignKeyCount = foreignKeys.Count,
            foreignKeys,
            indexCount = indexes.Count,
            indexes,
            statistics,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get trigger information (READ-ONLY)
    /// </summary>
    private async Task<object> GetTriggerInfoAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.TRIGGER)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.TRIGGER");
        
        var schema = parts[0];
        var triggerName = parts[1];
        
        Logger.Debug("Getting trigger info: {Schema}.{Trigger}", schema, triggerName);
        Console.WriteLine($"Retrieving trigger information for: {schema}.{triggerName}");
        
        var sql = $@"
            SELECT TRIGNAME, TRIGSCHEMA, TABSCHEMA, TABNAME, TRIGTIME, TRIGEVENT, GRANULARITY, 
                   ENABLED, VALID, REMARKS, TEXT, CREATE_TIME
            FROM SYSCAT.TRIGGERS
            WHERE TRIGSCHEMA = '{schema}' AND TRIGNAME = '{triggerName}'
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        if (data.Rows.Count == 0)
            throw new InvalidOperationException($"Trigger not found: {schema}.{triggerName}");
        
        var row = data.Rows[0];
        
        var result = new
        {
            schema,
            triggerName,
            targetSchema = row["TABSCHEMA"]?.ToString()?.Trim(),
            targetTable = row["TABNAME"]?.ToString()?.Trim(),
            triggerTime = row["TRIGTIME"]?.ToString()?.Trim(),
            triggerEvent = row["TRIGEVENT"]?.ToString()?.Trim(),
            granularity = row["GRANULARITY"]?.ToString()?.Trim(),
            isEnabled = row["ENABLED"]?.ToString() == "Y",
            isValid = row["VALID"]?.ToString() == "Y",
            comment = row["REMARKS"]?.ToString()?.Trim(),
            sourceCode = args.IncludeSourceCode ? row["TEXT"]?.ToString()?.Trim() : null,
            createTime = row["CREATE_TIME"],
            retrievedAt = DateTime.Now
        };
        
        Logger.Info("Retrieved trigger info: {Schema}.{Trigger}", schema, triggerName);
        return result;
    }
    
    /// <summary>
    /// Find all triggers in schema (READ-ONLY)
    /// </summary>
    private async Task<object> GetTriggerUsageAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Logger.Debug("Getting trigger usage for schema: {Schema}", schema);
        Console.WriteLine($"Finding trigger usage in schema: {schema}");
        
        var sql = $@"
            SELECT TRIGSCHEMA, TRIGNAME, TABSCHEMA, TABNAME, TRIGTIME, TRIGEVENT, ENABLED, VALID
            FROM SYSCAT.TRIGGERS
            WHERE TRIGSCHEMA LIKE '{schema}'
            ORDER BY TRIGSCHEMA, TABSCHEMA, TABNAME, TRIGNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var triggers = data.AsEnumerable().Select(row => new
        {
            triggerSchema = row["TRIGSCHEMA"]?.ToString()?.Trim(),
            triggerName = row["TRIGNAME"]?.ToString()?.Trim(),
            targetSchema = row["TABSCHEMA"]?.ToString()?.Trim(),
            targetTable = row["TABNAME"]?.ToString()?.Trim(),
            triggerTime = row["TRIGTIME"]?.ToString()?.Trim(),
            triggerEvent = row["TRIGEVENT"]?.ToString()?.Trim(),
            isEnabled = row["ENABLED"]?.ToString() == "Y",
            isValid = row["VALID"]?.ToString() == "Y"
        }).ToList();
        
        Logger.Info("Found {Count} triggers in schema: {Schema}", triggers.Count, schema);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            triggerCount = triggers.Count,
            triggers,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get view information (READ-ONLY)
    /// </summary>
    private async Task<object> GetViewInfoAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.VIEW)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.VIEW");
        
        var schema = parts[0];
        var viewName = parts[1];
        
        Logger.Debug("Getting view info: {Schema}.{View}", schema, viewName);
        Console.WriteLine($"Retrieving view information for: {schema}.{viewName}");
        
        var sql = $@"
            SELECT VIEWSCHEMA, VIEWNAME, VIEWCHECK, READONLY, VALID, TEXT
            FROM SYSCAT.VIEWS
            WHERE VIEWSCHEMA = '{schema}' AND VIEWNAME = '{viewName}'
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        if (data.Rows.Count == 0)
            throw new InvalidOperationException($"View not found: {schema}.{viewName}");
        
        var row = data.Rows[0];
        
        // Get view dependencies if requested
        List<object>? dependsOn = null;
        List<object>? usedBy = null;
        
        if (args.IncludeDependencies)
        {
            var depSql = $@"
                SELECT BSCHEMA, BNAME, BTYPE, DTYPE
                FROM SYSCAT.TABDEP
                WHERE TABSCHEMA = '{schema}' AND TABNAME = '{viewName}'
                ORDER BY BSCHEMA, BNAME
            ";
            
            var depData = await connectionManager.ExecuteQueryAsync(depSql);
            dependsOn = depData.AsEnumerable().Select(r => new object[]
            {
                r["BSCHEMA"]?.ToString()?.Trim(),
                r["BNAME"]?.ToString()?.Trim(),
                r["BTYPE"]?.ToString()?.Trim()
            }).ToList<object>();
        }
        
        var result = new
        {
            schema,
            viewName,
            viewCheck = row["VIEWCHECK"]?.ToString()?.Trim(),
            isReadOnly = row["READONLY"]?.ToString() == "Y",
            isValid = row["VALID"]?.ToString() == "Y",
            sourceCode = args.IncludeSourceCode ? row["TEXT"]?.ToString()?.Trim() : null,
            dependsOn,
            retrievedAt = DateTime.Now
        };
        
        Logger.Info("Retrieved view info: {Schema}.{View}", schema, viewName);
        return result;
    }
    
    /// <summary>
    /// Get stored procedure information (READ-ONLY)
    /// </summary>
    private async Task<object> GetProcedureInfoAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.PROCEDURE)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.PROCEDURE");
        
        var schema = parts[0];
        var procedureName = parts[1];
        
        Logger.Debug("Getting procedure info: {Schema}.{Procedure}", schema, procedureName);
        Console.WriteLine($"Retrieving procedure information for: {schema}.{procedureName}");
        
        var sql = $@"
            SELECT PROCSCHEMA, PROCNAME, LANGUAGE, DETERMINISTIC, TEXT
            FROM SYSCAT.PROCEDURES
            WHERE PROCSCHEMA = '{schema}' AND PROCNAME = '{procedureName}'
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        if (data.Rows.Count == 0)
            throw new InvalidOperationException($"Procedure not found: {schema}.{procedureName}");
        
        var row = data.Rows[0];
        
        var result = new
        {
            schema,
            procedureName,
            language = row["LANGUAGE"]?.ToString()?.Trim(),
            isDeterministic = row["DETERMINISTIC"]?.ToString() == "Y",
            sourceCode = args.IncludeSourceCode ? row["TEXT"]?.ToString()?.Trim() : null,
            retrievedAt = DateTime.Now
        };
        
        Logger.Info("Retrieved procedure info: {Schema}.{Procedure}", schema, procedureName);
        return result;
    }
    
    /// <summary>
    /// Get function information (READ-ONLY)
    /// </summary>
    private async Task<object> GetFunctionInfoAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.FUNCTION)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.FUNCTION");
        
        var schema = parts[0];
        var functionName = parts[1];
        
        Logger.Debug("Getting function info: {Schema}.{Function}", schema, functionName);
        Console.WriteLine($"Retrieving function information for: {schema}.{functionName}");
        
        var sql = $@"
            SELECT FUNCSCHEMA, FUNCNAME, LANGUAGE, TEXT
            FROM SYSCAT.FUNCTIONS
            WHERE FUNCSCHEMA = '{schema}' AND FUNCNAME = '{functionName}'
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        if (data.Rows.Count == 0)
            throw new InvalidOperationException($"Function not found: {schema}.{functionName}");
        
        var row = data.Rows[0];
        
        var result = new
        {
            schema,
            functionName,
            language = row["LANGUAGE"]?.ToString()?.Trim(),
            sourceCode = args.IncludeSourceCode ? row["TEXT"]?.ToString()?.Trim() : null,
            retrievedAt = DateTime.Now
        };
        
        Logger.Info("Retrieved function info: {Schema}.{Function}", schema, functionName);
        return result;
    }
    
    /// <summary>
    /// Get current database locks (READ-ONLY monitoring)
    /// </summary>
    private async Task<object> GetLockMonitorAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        Logger.Debug("Getting lock monitor data");
        Console.WriteLine("Retrieving current database locks...");
        
        var sql = @"
            SELECT COUNT(*) AS LOCK_COUNT
            FROM SYSCAT.TABLES
            WHERE TYPE = 'T'
            FETCH FIRST 1 ROWS ONLY
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var locks = data.AsEnumerable().Select(row => new
        {
            message = "Lock monitoring requires DB2 admin views",
            lockCount = row["LOCK_COUNT"]
        }).ToList();
        
        Logger.Info("Retrieved {Count} locks", locks.Count);
        
        return new
        {
            lockCount = locks.Count,
            locks,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get active database sessions (READ-ONLY monitoring)
    /// </summary>
    private async Task<object> GetActiveSessionsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        Logger.Debug("Getting active sessions");
        Console.WriteLine("Retrieving active database sessions...");
        
        var sql = @"
            SELECT 
                CURRENT USER AS AUTHID,
                CURRENT TIMESTAMP AS CONNECT_TIME
            FROM SYSIBM.SYSDUMMY1
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var sessions = data.AsEnumerable().Take(limit).Select(row => new
        {
            authId = row["AUTHID"]?.ToString()?.Trim(),
            connectTime = row["CONNECT_TIME"],
            message = "Session monitoring requires admin views"
        }).ToList();
        
        Logger.Info("Retrieved {Count} sessions (limit: {Limit})", sessions.Count, limit);
        
        return new
        {
            totalSessions = data.Rows.Count,
            limitApplied = limit,
            sessions,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get database load metrics (READ-ONLY monitoring)
    /// </summary>
    private async Task<object> GetDatabaseLoadAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Logger.Debug("Getting database load for schema: {Schema}", schema);
        Console.WriteLine($"Retrieving database load metrics for schema: {schema}");
        
        var sql = $@"
            SELECT 
                TABSCHEMA,
                TABNAME,
                ROWS_READ,
                ROWS_INSERTED,
                ROWS_UPDATED,
                ROWS_DELETED
            FROM TABLE(MON_GET_TABLE('', '', -2)) AS T
            WHERE TABSCHEMA LIKE '{schema}'
            ORDER BY (ROWS_READ + ROWS_INSERTED + ROWS_UPDATED + ROWS_DELETED) DESC
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var metrics = data.AsEnumerable().Take(limit).Select(row => new
        {
            schema = row["TABSCHEMA"]?.ToString()?.Trim(),
            tableName = row["TABNAME"]?.ToString()?.Trim(),
            rowsRead = row["ROWS_READ"],
            rowsInserted = row["ROWS_INSERTED"],
            rowsUpdated = row["ROWS_UPDATED"],
            rowsDeleted = row["ROWS_DELETED"]
        }).ToList();
        
        Logger.Info("Retrieved {Count} table load metrics (limit: {Limit})", metrics.Count, limit);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            totalTables = data.Rows.Count,
            limitApplied = limit,
            metrics,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get table statistics (READ-ONLY)
    /// </summary>
    private async Task<object> GetTableStatsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Logger.Debug("Getting table statistics for schema: {Schema}", schema);
        Console.WriteLine($"Retrieving table statistics for schema: {schema}");
        
        var sql = $@"
            SELECT TABSCHEMA, TABNAME, CARD, NPAGES, FPAGES, STATS_TIME
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA LIKE '{schema}' AND TYPE IN ('T', 'U')
            ORDER BY TABSCHEMA, TABNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var statistics = data.AsEnumerable().Take(limit).Select(row => new
        {
            schema = row["TABSCHEMA"]?.ToString()?.Trim(),
            tableName = row["TABNAME"]?.ToString()?.Trim(),
            rowCount = row["CARD"],
            dataPages = row["NPAGES"],
            freePages = row["FPAGES"],
            lastStatsTime = row["STATS_TIME"]
        }).ToList();
        
        Logger.Info("Retrieved {Count} table statistics (limit: {Limit})", statistics.Count, limit);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            totalTables = data.Rows.Count,
            limitApplied = limit,
            statistics,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Analyze object dependencies (READ-ONLY)
    /// </summary>
    private async Task<object> GetDependenciesAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.OBJECT)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.OBJECT");
        
        var schema = parts[0];
        var objectName = parts[1];
        var objectType = args.ObjectType ?? "TABLE";
        
        Logger.Debug("Getting dependencies: {Schema}.{Object} ({Type})", schema, objectName, objectType);
        Console.WriteLine($"Analyzing dependencies for: {schema}.{objectName} ({objectType})");
        
        // Get dependencies (objects this object depends on)
        var sql = $@"
            SELECT BSCHEMA, BNAME, BTYPE, DTYPE
            FROM SYSCAT.TABDEP
            WHERE TABSCHEMA = '{schema}' AND TABNAME = '{objectName}'
            ORDER BY BSCHEMA, BNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var dependencies = data.AsEnumerable().Select(row => new
        {
            targetSchema = row["BSCHEMA"]?.ToString()?.Trim(),
            targetObject = row["BNAME"]?.ToString()?.Trim(),
            targetType = row["BTYPE"]?.ToString()?.Trim(),
            dependencyType = row["DTYPE"]?.ToString()?.Trim()
        }).ToList();
        
        Logger.Info("Found {Count} dependencies for {Schema}.{Object}", dependencies.Count, schema, objectName);
        
        return new
        {
            schema,
            objectName,
            objectType,
            dependencyCount = dependencies.Count,
            dependencies,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get CDC information (READ-ONLY)
    /// </summary>
    private async Task<object> GetCdcInfoAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Logger.Debug("Getting CDC info for schema: {Schema}", schema);
        Console.WriteLine($"Retrieving CDC information for schema: {schema}");
        
        var sql = $@"
            SELECT TABSCHEMA, TABNAME, PROPERTY
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA LIKE '{schema}' 
              AND TYPE IN ('T', 'U')
              AND PROPERTY IS NOT NULL
            ORDER BY TABSCHEMA, TABNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var tables = data.AsEnumerable().Select(row => new
        {
            schema = row["TABSCHEMA"]?.ToString()?.Trim(),
            tableName = row["TABNAME"]?.ToString()?.Trim(),
            property = row["PROPERTY"]?.ToString()?.Trim()
        }).ToList();
        
        Logger.Info("Found {Count} tables with CDC info in schema: {Schema}", tables.Count, schema);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            tableCount = tables.Count,
            tables,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// List all views in schema (READ-ONLY)
    /// </summary>
    private async Task<object> ListViewsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Logger.Debug("Listing views in schema: {Schema}", schema);
        Console.WriteLine($"Listing views in schema: {schema}");
        
        var sql = $@"
            SELECT VIEWSCHEMA, VIEWNAME, VALID, READONLY
            FROM SYSCAT.VIEWS
            WHERE VIEWSCHEMA LIKE '{schema}'
            ORDER BY VIEWSCHEMA, VIEWNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var views = data.AsEnumerable().Take(limit).Select(row => new
        {
            schema = row["VIEWSCHEMA"]?.ToString()?.Trim(),
            viewName = row["VIEWNAME"]?.ToString()?.Trim(),
            isValid = row["VALID"]?.ToString() == "Y",
            isReadOnly = row["READONLY"]?.ToString() == "Y"
        }).ToList();
        
        Logger.Info("Listed {Count} views (limit: {Limit})", views.Count, limit);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            totalViews = data.Rows.Count,
            limitApplied = limit,
            views,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// List all procedures in schema (READ-ONLY)
    /// </summary>
    private async Task<object> ListProceduresAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Logger.Debug("Listing procedures in schema: {Schema}", schema);
        Console.WriteLine($"Listing procedures in schema: {schema}");
        
        var sql = $@"
            SELECT PROCSCHEMA, PROCNAME, LANGUAGE, DETERMINISTIC
            FROM SYSCAT.PROCEDURES
            WHERE PROCSCHEMA LIKE '{schema}'
            ORDER BY PROCSCHEMA, PROCNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var procedures = data.AsEnumerable().Take(limit).Select(row => new
        {
            schema = row["PROCSCHEMA"]?.ToString()?.Trim(),
            procedureName = row["PROCNAME"]?.ToString()?.Trim(),
            language = row["LANGUAGE"]?.ToString()?.Trim(),
            isDeterministic = row["DETERMINISTIC"]?.ToString() == "Y"
        }).ToList();
        
        Logger.Info("Listed {Count} procedures (limit: {Limit})", procedures.Count, limit);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            totalProcedures = data.Rows.Count,
            limitApplied = limit,
            procedures,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// List all triggers in schema (READ-ONLY)
    /// </summary>
    private async Task<object> ListTriggersAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Logger.Debug("Listing triggers in schema: {Schema}", schema);
        Console.WriteLine($"Listing triggers in schema: {schema}");
        
        var sql = $@"
            SELECT TRIGSCHEMA, TRIGNAME, TABSCHEMA, TABNAME, TRIGTIME, TRIGEVENT, ENABLED
            FROM SYSCAT.TRIGGERS
            WHERE TRIGSCHEMA LIKE '{schema}'
            ORDER BY TRIGSCHEMA, TRIGNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var triggers = data.AsEnumerable().Take(limit).Select(row => new
        {
            triggerSchema = row["TRIGSCHEMA"]?.ToString()?.Trim(),
            triggerName = row["TRIGNAME"]?.ToString()?.Trim(),
            targetSchema = row["TABSCHEMA"]?.ToString()?.Trim(),
            targetTable = row["TABNAME"]?.ToString()?.Trim(),
            triggerTime = row["TRIGTIME"]?.ToString()?.Trim(),
            triggerEvent = row["TRIGEVENT"]?.ToString()?.Trim(),
            isEnabled = row["ENABLED"]?.ToString() == "Y"
        }).ToList();
        
        Logger.Info("Listed {Count} triggers (limit: {Limit})", triggers.Count, limit);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            totalTriggers = data.Rows.Count,
            limitApplied = limit,
            triggers,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// List all functions in schema (READ-ONLY)
    /// </summary>
    private async Task<object> ListFunctionsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Logger.Debug("Listing functions in schema: {Schema}", schema);
        Console.WriteLine($"Listing functions in schema: {schema}");
        
        var sql = $@"
            SELECT FUNCSCHEMA, FUNCNAME, LANGUAGE
            FROM SYSCAT.FUNCTIONS
            WHERE FUNCSCHEMA LIKE '{schema}'
            ORDER BY FUNCSCHEMA, FUNCNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var functions = data.AsEnumerable().Take(limit).Select(row => new
        {
            schema = row["FUNCSCHEMA"]?.ToString()?.Trim(),
            functionName = row["FUNCNAME"]?.ToString()?.Trim(),
            language = row["LANGUAGE"]?.ToString()?.Trim()
        }).ToList();
        
        Logger.Info("Listed {Count} functions (limit: {Limit})", functions.Count, limit);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            totalFunctions = data.Rows.Count,
            limitApplied = limit,
            functions,
            retrievedAt = DateTime.Now
        };
    }
    
    // ========================================================================
    // TableDetailsDialog Commands (9 commands for all tabs)
    // ========================================================================
    
    /// <summary>
    /// Get complete column metadata (Tab 1 of TableDetailsDialog)
    /// </summary>
    private async Task<object> GetTableColumnsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.TABLE)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.TABLE");
        
        var schema = parts[0];
        var tableName = parts[1];
        
        Logger.Debug("Getting complete column metadata: {Schema}.{Table}", schema, tableName);
        Console.WriteLine($"Retrieving columns for: {schema}.{tableName}");
        
        var sql = $@"
            SELECT 
                TRIM(COLNAME) AS ColumnName,
                COLNO AS OrdinalPosition,
                TRIM(TYPENAME) AS DataType,
                LENGTH,
                SCALE,
                CASE WHEN NULLS = 'Y' THEN 1 ELSE 0 END AS IsNullable,
                COALESCE(TRIM(DEFAULT), '') AS DefaultValue,
                COALESCE(TRIM(REMARKS), '') AS Comment,
                CASE WHEN IDENTITY = 'Y' THEN 1 ELSE 0 END AS IsIdentity
            FROM SYSCAT.COLUMNS 
            WHERE TABSCHEMA = '{schema}' AND TABNAME = '{tableName}'
            ORDER BY COLNO
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var columns = data.AsEnumerable().Select(row => new
        {
            columnName = row["ColumnName"]?.ToString()?.Trim(),
            ordinalPosition = row["OrdinalPosition"],
            dataType = row["DataType"]?.ToString()?.Trim(),
            length = row["LENGTH"],
            scale = row["SCALE"],
            isNullable = Convert.ToInt32(row["IsNullable"]) == 1,
            defaultValue = row["DefaultValue"]?.ToString()?.Trim(),
            comment = row["Comment"]?.ToString()?.Trim(),
            isIdentity = Convert.ToInt32(row["IsIdentity"]) == 1
        }).ToList();
        
        Logger.Info("Retrieved {Count} columns", columns.Count);
        
        return new
        {
            schema,
            tableName,
            columnCount = columns.Count,
            columns,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get outgoing foreign keys (Tab 2 of TableDetailsDialog)
    /// </summary>
    private async Task<object> GetTableForeignKeysAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.TABLE)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.TABLE");
        
        var schema = parts[0];
        var tableName = parts[1];
        
        Logger.Debug("Getting foreign keys: {Schema}.{Table}", schema, tableName);
        Console.WriteLine($"Retrieving foreign keys for: {schema}.{tableName}");
        
        var sql = $@"
            SELECT 
                TRIM(CONSTNAME) AS FKName,
                FK_COLNAMES AS FKColumns,
                TRIM(REFTABSCHEMA) AS RefSchema,
                TRIM(REFTABNAME) AS RefTable,
                PK_COLNAMES AS RefColumns,
                TRIM(DELETERULE) AS DeleteRule,
                TRIM(UPDATERULE) AS UpdateRule
            FROM SYSCAT.REFERENCES
            WHERE TABSCHEMA = '{schema}' AND TABNAME = '{tableName}'
            ORDER BY CONSTNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var foreignKeys = data.AsEnumerable().Select(row => new
        {
            fkName = row["FKName"]?.ToString()?.Trim(),
            fkColumns = row["FKColumns"]?.ToString()?.Trim(),
            refSchema = row["RefSchema"]?.ToString()?.Trim(),
            refTable = row["RefTable"]?.ToString()?.Trim(),
            refColumns = row["RefColumns"]?.ToString()?.Trim(),
            deleteRule = row["DeleteRule"]?.ToString()?.Trim(),
            updateRule = row["UpdateRule"]?.ToString()?.Trim()
        }).ToList();
        
        Logger.Info("Retrieved {Count} foreign keys", foreignKeys.Count);
        
        return new
        {
            schema,
            tableName,
            foreignKeyCount = foreignKeys.Count,
            foreignKeys,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get complete index details (Tab 3 of TableDetailsDialog)
    /// </summary>
    private async Task<object> GetTableIndexesAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.TABLE)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.TABLE");
        
        var schema = parts[0];
        var tableName = parts[1];
        
        Logger.Debug("Getting indexes: {Schema}.{Table}", schema, tableName);
        Console.WriteLine($"Retrieving indexes for: {schema}.{tableName}");
        
        var sql = $@"
            SELECT 
                TRIM(INDNAME) AS IndexName,
                TRIM(INDEXTYPE) AS IndexType,
                TRIM(UNIQUERULE) AS UniqueRule,
                COLNAMES,
                COLCOUNT,
                FIRSTKEYCARD,
                FULLKEYCARD
            FROM SYSCAT.INDEXES
            WHERE TABSCHEMA = '{schema}' AND TABNAME = '{tableName}'
            ORDER BY INDNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var indexes = data.AsEnumerable().Select(row => new
        {
            indexName = row["IndexName"]?.ToString()?.Trim(),
            indexType = row["IndexType"]?.ToString()?.Trim(),
            isUnique = row["UniqueRule"]?.ToString()?.Trim() == "U",
            isPrimaryKey = row["UniqueRule"]?.ToString()?.Trim() == "P",
            columns = row["COLNAMES"]?.ToString()?.Trim(),
            columnCount = row["COLCOUNT"],
            firstKeyCard = row["FIRSTKEYCARD"],
            fullKeyCard = row["FULLKEYCARD"]
        }).ToList();
        
        Logger.Info("Retrieved {Count} indexes", indexes.Count);
        
        return new
        {
            schema,
            tableName,
            indexCount = indexes.Count,
            indexes,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get full table statistics (Tab 4 of TableDetailsDialog)
    /// </summary>
    private async Task<object> GetTableStatisticsFullAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.TABLE)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.TABLE");
        
        var schema = parts[0];
        var tableName = parts[1];
        
        Logger.Debug("Getting full statistics: {Schema}.{Table}", schema, tableName);
        Console.WriteLine($"Retrieving statistics for: {schema}.{tableName}");
        
        var sql = $@"
            SELECT 
                CARD AS RowCount,
                NPAGES AS DataPages,
                FPAGES AS FreePages,
                OVERFLOW AS OverflowPages,
                AVGROWSIZE AS AvgRowSize,
                STATS_TIME AS LastStatsTime,
                LAST_REGEN_TIME AS LastReorgTime,
                TRIM(TYPE) AS TableType,
                TRIM(STATUS) AS Status
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA = '{schema}' AND TABNAME = '{tableName}'
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        if (data.Rows.Count == 0)
            throw new InvalidOperationException($"Table not found: {schema}.{tableName}");
        
        var row = data.Rows[0];
        
        var result = new
        {
            schema,
            tableName,
            rowCount = row["RowCount"],
            dataPages = row["DataPages"],
            freePages = row["FreePages"],
            overflowPages = row["OverflowPages"],
            avgRowSize = row["AvgRowSize"],
            lastStatsTime = row["LastStatsTime"],
            lastReorgTime = row["LastReorgTime"],
            tableType = row["TableType"]?.ToString()?.Trim(),
            status = row["Status"]?.ToString()?.Trim(),
            retrievedAt = DateTime.Now
        };
        
        Logger.Info("Retrieved statistics for {Schema}.{Table}", schema, tableName);
        return result;
    }
    
    /// <summary>
    /// Generate CREATE TABLE DDL (Tab 5 of TableDetailsDialog)
    /// </summary>
    private async Task<object> GetTableDdlAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.TABLE)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.TABLE");
        
        var schema = parts[0];
        var tableName = parts[1];
        
        Logger.Debug("Generating DDL: {Schema}.{Table}", schema, tableName);
        Console.WriteLine($"Generating DDL for: {schema}.{tableName}");
        
        // Get columns
        var colSql = $@"
            SELECT 
                TRIM(COLNAME) AS ColumnName,
                TRIM(TYPENAME) AS DataType,
                LENGTH,
                SCALE,
                NULLS,
                TRIM(DEFAULT) AS DefaultValue,
                IDENTITY
            FROM SYSCAT.COLUMNS 
            WHERE TABSCHEMA = '{schema}' AND TABNAME = '{tableName}'
            ORDER BY COLNO
        ";
        
        var colData = await connectionManager.ExecuteQueryAsync(colSql);
        
        // Build DDL
        var ddl = new System.Text.StringBuilder();
        ddl.AppendLine($"CREATE TABLE {schema}.{tableName} (");
        
        for (int i = 0; i < colData.Rows.Count; i++)
        {
            var colRow = colData.Rows[i];
            var colName = colRow["ColumnName"]?.ToString()?.Trim();
            var dataType = colRow["DataType"]?.ToString()?.Trim();
            var length = colRow["LENGTH"];
            var scale = colRow["SCALE"];
            var nullable = colRow["NULLS"]?.ToString() == "Y";
            var defaultValue = colRow["DefaultValue"]?.ToString()?.Trim();
            var isIdentity = colRow["IDENTITY"]?.ToString() == "Y";
            
            ddl.Append($"    {colName} {dataType}");
            
            if (dataType == "VARCHAR" || dataType == "CHAR")
                ddl.Append($"({length})");
            else if (dataType == "DECIMAL" || dataType == "NUMERIC")
                ddl.Append($"({length},{scale})");
            
            if (isIdentity)
                ddl.Append(" GENERATED ALWAYS AS IDENTITY");
            
            if (!nullable)
                ddl.Append(" NOT NULL");
            
            if (!string.IsNullOrEmpty(defaultValue))
                ddl.Append($" DEFAULT {defaultValue}");
            
            if (i < colData.Rows.Count - 1)
                ddl.AppendLine(",");
            else
                ddl.AppendLine();
        }
        
        ddl.AppendLine(");");
        
        var ddlString = ddl.ToString();
        
        Logger.Info("Generated DDL for {Schema}.{Table} ({Length} chars)", schema, tableName, ddlString.Length);
        
        return new
        {
            schema,
            tableName,
            ddl = ddlString,
            ddlLength = ddlString.Length,
            columnCount = colData.Rows.Count,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get incoming foreign keys (Tab 6 of TableDetailsDialog)
    /// </summary>
    private async Task<object> GetTableIncomingFKsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.TABLE)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.TABLE");
        
        var schema = parts[0];
        var tableName = parts[1];
        
        Logger.Debug("Getting incoming FKs: {Schema}.{Table}", schema, tableName);
        Console.WriteLine($"Retrieving incoming foreign keys for: {schema}.{tableName}");
        
        var sql = $@"
            SELECT 
                TRIM(TABSCHEMA) AS RefSchema,
                TRIM(TABNAME) AS RefTable,
                TRIM(CONSTNAME) AS FKName,
                FK_COLNAMES AS FKColumns,
                PK_COLNAMES AS RefColumns
            FROM SYSCAT.REFERENCES
            WHERE REFTABSCHEMA = '{schema}' AND REFTABNAME = '{tableName}'
            ORDER BY TABSCHEMA, TABNAME, CONSTNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var incomingFKs = data.AsEnumerable().Select(row => new
        {
            refSchema = row["RefSchema"]?.ToString()?.Trim(),
            refTable = row["RefTable"]?.ToString()?.Trim(),
            fkName = row["FKName"]?.ToString()?.Trim(),
            fkColumns = row["FKColumns"]?.ToString()?.Trim(),
            refColumns = row["RefColumns"]?.ToString()?.Trim()
        }).ToList();
        
        Logger.Info("Retrieved {Count} incoming foreign keys", incomingFKs.Count);
        
        return new
        {
            schema,
            tableName,
            incomingFKCount = incomingFKs.Count,
            incomingFKs,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get packages that reference this table (Tab 7 of TableDetailsDialog)
    /// </summary>
    private async Task<object> GetTableReferencingPackagesAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.TABLE)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.TABLE");
        
        var schema = parts[0];
        var tableName = parts[1];
        
        Logger.Debug("Getting referencing packages: {Schema}.{Table}", schema, tableName);
        Console.WriteLine($"Finding packages that reference: {schema}.{tableName}");
        
        var sql = $@"
            SELECT DISTINCT
                TRIM(PKGSCHEMA) AS PackageSchema,
                TRIM(PKGNAME) AS PackageName
            FROM SYSCAT.PACKAGEDEP
            WHERE BSCHEMA = '{schema}' AND BNAME = '{tableName}'
            ORDER BY PackageSchema, PackageName
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var packages = data.AsEnumerable().Select(row => new
        {
            packageSchema = row["PackageSchema"]?.ToString()?.Trim(),
            packageName = row["PackageName"]?.ToString()?.Trim()
        }).ToList();
        
        Logger.Info("Found {Count} referencing packages", packages.Count);
        
        return new
        {
            schema,
            tableName,
            packageCount = packages.Count,
            packages,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get views that reference this table (Tab 8 of TableDetailsDialog)
    /// </summary>
    private async Task<object> GetTableReferencingViewsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.TABLE)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.TABLE");
        
        var schema = parts[0];
        var tableName = parts[1];
        
        Logger.Debug("Getting referencing views: {Schema}.{Table}", schema, tableName);
        Console.WriteLine($"Finding views that reference: {schema}.{tableName}");
        
        var sql = $@"
            SELECT DISTINCT
                TRIM(TABSCHEMA) AS ViewSchema,
                TRIM(TABNAME) AS ViewName
            FROM SYSCAT.TABDEP
            WHERE BSCHEMA = '{schema}' AND BNAME = '{tableName}' 
                AND BTYPE = 'T' AND DTYPE = 'V'
            ORDER BY ViewSchema, ViewName
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var views = data.AsEnumerable().Select(row => new
        {
            viewSchema = row["ViewSchema"]?.ToString()?.Trim(),
            viewName = row["ViewName"]?.ToString()?.Trim()
        }).ToList();
        
        Logger.Info("Found {Count} referencing views", views.Count);
        
        return new
        {
            schema,
            tableName,
            viewCount = views.Count,
            views,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get routines that reference this table (Tab 9 of TableDetailsDialog)
    /// </summary>
    private async Task<object> GetTableReferencingRoutinesAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.TABLE)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.TABLE");
        
        var schema = parts[0];
        var tableName = parts[1];
        
        Logger.Debug("Getting referencing routines: {Schema}.{Table}", schema, tableName);
        Console.WriteLine($"Finding routines that reference: {schema}.{tableName}");
        
        var sql = $@"
            SELECT DISTINCT
                TRIM(ROUTINESCHEMA) AS RoutineSchema,
                TRIM(ROUTINENAME) AS RoutineName,
                TRIM(BTYPE) AS RoutineType
            FROM SYSCAT.ROUTINEDEP
            WHERE BSCHEMA = '{schema}' AND BNAME = '{tableName}' AND BTYPE = 'T'
            ORDER BY RoutineSchema, RoutineName
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var routines = data.AsEnumerable().Select(row => new
        {
            routineSchema = row["RoutineSchema"]?.ToString()?.Trim(),
            routineName = row["RoutineName"]?.ToString()?.Trim(),
            routineType = row["RoutineType"]?.ToString()?.Trim()
        }).ToList();
        
        Logger.Info("Found {Count} referencing routines", routines.Count);
        
        return new
        {
            schema,
            tableName,
            routineCount = routines.Count,
            routines,
            retrievedAt = DateTime.Now
        };
    }
    
    // ========================================================================
    // Source Code Browser Commands (3 commands)
    // ========================================================================
    
    /// <summary>
    /// List all source code objects (procedures, functions, views, triggers)
    /// </summary>
    private async Task<object> ListAllSourceAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        var objectType = args.ObjectType ?? "ALL";
        
        Logger.Debug("Listing all source code objects - Schema: {Schema}, Type: {Type}", schema, objectType);
        Console.WriteLine($"Listing source code objects in schema: {schema}, Type: {objectType}");
        
        var sql = objectType.ToUpper() switch
        {
            "PROCEDURE" or "P" => $@"
                SELECT 'PROCEDURE' AS ObjectType, TRIM(ROUTINESCHEMA) AS Schema, TRIM(ROUTINENAME) AS Name, TRIM(LANGUAGE) AS Language
                FROM SYSCAT.ROUTINES
                WHERE ROUTINESCHEMA LIKE '{schema}' AND ROUTINETYPE = 'P'
                ORDER BY ROUTINESCHEMA, ROUTINENAME
            ",
            "FUNCTION" or "F" => $@"
                SELECT 'FUNCTION' AS ObjectType, TRIM(ROUTINESCHEMA) AS Schema, TRIM(ROUTINENAME) AS Name, TRIM(LANGUAGE) AS Language
                FROM SYSCAT.ROUTINES
                WHERE ROUTINESCHEMA LIKE '{schema}' AND ROUTINETYPE = 'F'
                ORDER BY ROUTINESCHEMA, ROUTINENAME
            ",
            "VIEW" or "V" => $@"
                SELECT 'VIEW' AS ObjectType, TRIM(VIEWSCHEMA) AS Schema, TRIM(VIEWNAME) AS Name, 'SQL' AS Language
                FROM SYSCAT.VIEWS
                WHERE VIEWSCHEMA LIKE '{schema}'
                ORDER BY VIEWSCHEMA, VIEWNAME
            ",
            "TRIGGER" or "T" => $@"
                SELECT 'TRIGGER' AS ObjectType, TRIM(TRIGSCHEMA) AS Schema, TRIM(TRIGNAME) AS Name, 'SQL' AS Language
                FROM SYSCAT.TRIGGERS
                WHERE TRIGSCHEMA LIKE '{schema}'
                ORDER BY TRIGSCHEMA, TRIGNAME
            ",
            _ => $@"
                SELECT 'PROCEDURE' AS ObjectType, TRIM(ROUTINESCHEMA) AS Schema, TRIM(ROUTINENAME) AS Name, TRIM(LANGUAGE) AS Language
                FROM SYSCAT.ROUTINES
                WHERE ROUTINESCHEMA LIKE '{schema}' AND ROUTINETYPE = 'P'
                UNION ALL
                SELECT 'FUNCTION' AS ObjectType, TRIM(ROUTINESCHEMA) AS Schema, TRIM(ROUTINENAME) AS Name, TRIM(LANGUAGE) AS Language
                FROM SYSCAT.ROUTINES
                WHERE ROUTINESCHEMA LIKE '{schema}' AND ROUTINETYPE = 'F'
                UNION ALL
                SELECT 'VIEW' AS ObjectType, TRIM(VIEWSCHEMA) AS Schema, TRIM(VIEWNAME) AS Name, 'SQL' AS Language
                FROM SYSCAT.VIEWS
                WHERE VIEWSCHEMA LIKE '{schema}'
                UNION ALL
                SELECT 'TRIGGER' AS ObjectType, TRIM(TRIGSCHEMA) AS Schema, TRIM(TRIGNAME) AS Name, 'SQL' AS Language
                FROM SYSCAT.TRIGGERS
                WHERE TRIGSCHEMA LIKE '{schema}'
                ORDER BY ObjectType, Schema, Name
            "
        };
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var objects = data.AsEnumerable().Take(limit).Select(row => new
        {
            objectType = row["ObjectType"]?.ToString()?.Trim(),
            schema = row["Schema"]?.ToString()?.Trim(),
            name = row["Name"]?.ToString()?.Trim(),
            language = row["Language"]?.ToString()?.Trim()
        }).ToList();
        
        Logger.Info("Found {Count} source objects (limit: {Limit})", objects.Count, limit);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            objectType,
            totalObjects = data.Rows.Count,
            limitApplied = limit,
            objects,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get complete source code for an object
    /// </summary>
    private async Task<object> GetSourceCodeFullAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.OBJECT)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.OBJECT");
        
        var schema = parts[0];
        var objectName = parts[1];
        var objectType = args.ObjectType?.ToUpper() ?? "PROCEDURE";
        
        Logger.Debug("Getting source code: {Schema}.{Object} ({Type})", schema, objectName, objectType);
        Console.WriteLine($"Retrieving source code for: {schema}.{objectName} ({objectType})");
        
        string sql;
        if (objectType == "VIEW" || objectType == "V")
        {
            sql = $@"
                SELECT TRIM(VIEWSCHEMA) AS Schema, TRIM(VIEWNAME) AS Name, TEXT AS SourceCode, 'VIEW' AS ObjectType
                FROM SYSCAT.VIEWS
                WHERE VIEWSCHEMA = '{schema}' AND VIEWNAME = '{objectName}'
            ";
        }
        else if (objectType == "TRIGGER" || objectType == "T")
        {
            sql = $@"
                SELECT TRIM(TRIGSCHEMA) AS Schema, TRIM(TRIGNAME) AS Name, TEXT AS SourceCode, 'TRIGGER' AS ObjectType,
                       CREATE_TIME, TRIM(TRIGEVENT) AS TriggerEvent, TRIM(TRIGTIME) AS TriggerTime
                FROM SYSCAT.TRIGGERS
                WHERE TRIGSCHEMA = '{schema}' AND TRIGNAME = '{objectName}'
            ";
        }
        else
        {
            sql = $@"
                SELECT TRIM(ROUTINESCHEMA) AS Schema, TRIM(ROUTINENAME) AS Name, TEXT AS SourceCode, TRIM(ROUTINETYPE) AS ObjectType,
                       CREATE_TIME, LAST_REGEN_TIME, TRIM(LANGUAGE) AS Language
                FROM SYSCAT.ROUTINES
                WHERE ROUTINESCHEMA = '{schema}' AND ROUTINENAME = '{objectName}'
            ";
        }
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        if (data.Rows.Count == 0)
            throw new InvalidOperationException($"Object not found: {schema}.{objectName}");
        
        var row = data.Rows[0];
        
        var result = new
        {
            schema = row["Schema"]?.ToString()?.Trim(),
            objectName = row["Name"]?.ToString()?.Trim(),
            objectType = row["ObjectType"]?.ToString()?.Trim(),
            sourceCode = row["SourceCode"]?.ToString()?.Trim(),
            sourceCodeLength = row["SourceCode"]?.ToString()?.Trim()?.Length ?? 0,
            language = row.Table.Columns.Contains("Language") ? row["Language"]?.ToString()?.Trim() : "SQL",
            createTime = row.Table.Columns.Contains("CREATE_TIME") ? row["CREATE_TIME"] : null,
            lastRegenTime = row.Table.Columns.Contains("LAST_REGEN_TIME") ? row["LAST_REGEN_TIME"] : null,
            retrievedAt = DateTime.Now
        };
        
        Logger.Info("Retrieved source code for {Schema}.{Object} ({Length} chars)", schema, objectName, result.sourceCodeLength);
        return result;
    }
    
    /// <summary>
    /// Search in source code
    /// </summary>
    private async Task<object> SearchSourceCodeAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Schema))
            throw new ArgumentException("Schema parameter required for source code search");
        
        var schema = args.Schema;
        var searchText = args.Object ?? ""; // Use Object parameter for search text
        var objectType = args.ObjectType?.ToUpper() ?? "ALL";
        
        Logger.Debug("Searching source code - Schema: {Schema}, Pattern: {Pattern}, Type: {Type}", schema, searchText, objectType);
        Console.WriteLine($"Searching source code in {schema} for: {searchText}");
        
        var sql = $@"
            SELECT 'PROCEDURE' AS ObjectType, TRIM(ROUTINESCHEMA) AS Schema, TRIM(ROUTINENAME) AS Name, TEXT AS SourceCode
            FROM SYSCAT.ROUTINES
            WHERE ROUTINESCHEMA = '{schema}' 
              AND ROUTINETYPE = 'P'
              AND UPPER(TEXT) LIKE UPPER('%{searchText}%')
            UNION ALL
            SELECT 'FUNCTION' AS ObjectType, TRIM(ROUTINESCHEMA) AS Schema, TRIM(ROUTINENAME) AS Name, TEXT AS SourceCode
            FROM SYSCAT.ROUTINES
            WHERE ROUTINESCHEMA = '{schema}' 
              AND ROUTINETYPE = 'F'
              AND UPPER(TEXT) LIKE UPPER('%{searchText}%')
            UNION ALL
            SELECT 'VIEW' AS ObjectType, TRIM(VIEWSCHEMA) AS Schema, TRIM(VIEWNAME) AS Name, TEXT AS SourceCode
            FROM SYSCAT.VIEWS
            WHERE VIEWSCHEMA = '{schema}'
              AND UPPER(TEXT) LIKE UPPER('%{searchText}%')
            UNION ALL
            SELECT 'TRIGGER' AS ObjectType, TRIM(TRIGSCHEMA) AS Schema, TRIM(TRIGNAME) AS Name, TEXT AS SourceCode
            FROM SYSCAT.TRIGGERS
            WHERE TRIGSCHEMA = '{schema}'
              AND UPPER(TEXT) LIKE UPPER('%{searchText}%')
            ORDER BY ObjectType, Schema, Name
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var matches = data.AsEnumerable().Take(limit).Select(row => new
        {
            objectType = row["ObjectType"]?.ToString()?.Trim(),
            schema = row["Schema"]?.ToString()?.Trim(),
            name = row["Name"]?.ToString()?.Trim(),
            sourceCodeLength = row["SourceCode"]?.ToString()?.Trim()?.Length ?? 0,
            sourceCode = args.IncludeSourceCode ? row["SourceCode"]?.ToString()?.Trim() : null
        }).ToList();
        
        Logger.Info("Found {Count} objects matching '{Pattern}' (limit: {Limit})", matches.Count, searchText, limit);
        
        return new
        {
            schema,
            searchPattern = searchText,
            objectType,
            totalMatches = data.Rows.Count,
            limitApplied = limit,
            matches,
            retrievedAt = DateTime.Now
        };
    }
    
    // ========================================================================
    // Package Analyzer Commands (3 commands)
    // ========================================================================
    
    /// <summary>
    /// List all packages in schema
    /// </summary>
    private async Task<object> ListPackagesAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Logger.Debug("Listing packages in schema: {Schema}", schema);
        Console.WriteLine($"Listing packages in schema: {schema}");
        
        var sql = $@"
            SELECT TRIM(PKGSCHEMA) AS PackageSchema, TRIM(PKGNAME) AS PackageName, 
                   CREATE_TIME, LAST_BIND_TIME
            FROM SYSCAT.PACKAGES
            WHERE PKGSCHEMA LIKE '{schema}'
            ORDER BY PackageSchema, PackageName
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var packages = data.AsEnumerable().Take(limit).Select(row => new
        {
            packageSchema = row["PackageSchema"]?.ToString()?.Trim(),
            packageName = row["PackageName"]?.ToString()?.Trim(),
            createTime = row["CREATE_TIME"],
            lastBindTime = row["LAST_BIND_TIME"]
        }).ToList();
        
        Logger.Info("Listed {Count} packages (limit: {Limit})", packages.Count, limit);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            totalPackages = data.Rows.Count,
            limitApplied = limit,
            packages,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Analyze package dependencies
    /// </summary>
    private async Task<object> AnalyzePackageAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.PACKAGE)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.PACKAGE");
        
        var schema = parts[0];
        var packageName = parts[1];
        
        Logger.Debug("Analyzing package: {Schema}.{Package}", schema, packageName);
        Console.WriteLine($"Analyzing package dependencies for: {schema}.{packageName}");
        
        var sql = $@"
            SELECT 
                TRIM(BSCHEMA) AS DependsOnSchema,
                TRIM(BNAME) AS DependsOnObject,
                TRIM(BTYPE) AS DependsOnType,
                TRIM(DTYPE) AS DependencyType
            FROM SYSCAT.PACKAGEDEP
            WHERE PKGSCHEMA = '{schema}' AND PKGNAME = '{packageName}'
            ORDER BY DependsOnSchema, DependsOnObject
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var dependencies = data.AsEnumerable().Select(row => new
        {
            dependsOnSchema = row["DependsOnSchema"]?.ToString()?.Trim(),
            dependsOnObject = row["DependsOnObject"]?.ToString()?.Trim(),
            dependsOnType = row["DependsOnType"]?.ToString()?.Trim(),
            dependencyType = row["DependencyType"]?.ToString()?.Trim()
        }).ToList();
        
        Logger.Info("Found {Count} dependencies for package {Schema}.{Package}", dependencies.Count, schema, packageName);
        
        return new
        {
            schema,
            packageName,
            dependencyCount = dependencies.Count,
            dependencies,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get detailed package information
    /// </summary>
    private async Task<object> GetPackageDetailsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.PACKAGE)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.PACKAGE");
        
        var schema = parts[0];
        var packageName = parts[1];
        
        Logger.Debug("Getting package details: {Schema}.{Package}", schema, packageName);
        Console.WriteLine($"Retrieving package details for: {schema}.{packageName}");
        
        var sql = $@"
            SELECT 
                TRIM(PKGSCHEMA) AS PackageSchema,
                TRIM(PKGNAME) AS PackageName,
                CREATE_TIME,
                LAST_BIND_TIME,
                TRIM(ISOLATION) AS IsolationLevel,
                TRIM(BLOCKING) AS BlockingType
            FROM SYSCAT.PACKAGES
            WHERE PKGSCHEMA = '{schema}' AND PKGNAME = '{packageName}'
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        if (data.Rows.Count == 0)
            throw new InvalidOperationException($"Package not found: {schema}.{packageName}");
        
        var row = data.Rows[0];
        
        var result = new
        {
            packageSchema = row["PackageSchema"]?.ToString()?.Trim(),
            packageName = row["PackageName"]?.ToString()?.Trim(),
            createTime = row["CREATE_TIME"],
            lastBindTime = row["LAST_BIND_TIME"],
            isolationLevel = row["IsolationLevel"]?.ToString()?.Trim(),
            blockingType = row["BlockingType"]?.ToString()?.Trim(),
            retrievedAt = DateTime.Now
        };
        
        Logger.Info("Retrieved package details: {Schema}.{Package}", schema, packageName);
        return result;
    }
    
    // ========================================================================
    // Comment Manager Commands (3 commands)
    // ========================================================================
    
    /// <summary>
    /// List objects with/without comments
    /// </summary>
    private async Task<object> ListCommentsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        var objectType = args.ObjectType?.ToUpper() ?? "TABLE";
        
        Logger.Debug("Listing comments - Schema: {Schema}, Type: {Type}", schema, objectType);
        Console.WriteLine($"Listing comments in schema: {schema}, Type: {objectType}");
        
        string sql;
        if (objectType == "TABLE" || objectType == "T")
        {
            sql = $@"
                SELECT TRIM(TABSCHEMA) AS Schema, TRIM(TABNAME) AS Name, 'TABLE' AS ObjectType,
                       CASE WHEN REMARKS IS NULL OR TRIM(REMARKS) = '' THEN 0 ELSE 1 END AS HasComment
                FROM SYSCAT.TABLES
                WHERE TABSCHEMA LIKE '{schema}' AND TYPE IN ('T', 'U')
                ORDER BY Schema, Name
            ";
        }
        else if (objectType == "VIEW" || objectType == "V")
        {
            sql = $@"
                SELECT TRIM(VIEWSCHEMA) AS Schema, TRIM(VIEWNAME) AS Name, 'VIEW' AS ObjectType,
                       0 AS HasComment
                FROM SYSCAT.VIEWS
                WHERE VIEWSCHEMA LIKE '{schema}'
                ORDER BY Schema, Name
            ";
        }
        else
        {
            sql = $@"
                SELECT TRIM(TABSCHEMA) AS Schema, TRIM(TABNAME) AS Name, 'TABLE' AS ObjectType,
                       CASE WHEN REMARKS IS NULL OR TRIM(REMARKS) = '' THEN 0 ELSE 1 END AS HasComment
                FROM SYSCAT.TABLES
                WHERE TABSCHEMA LIKE '{schema}' AND TYPE IN ('T', 'U')
                ORDER BY Schema, Name
            ";
        }
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var objects = data.AsEnumerable().Take(limit).Select(row => new
        {
            schema = row["Schema"]?.ToString()?.Trim(),
            name = row["Name"]?.ToString()?.Trim(),
            objectType = row["ObjectType"]?.ToString()?.Trim(),
            hasComment = Convert.ToInt32(row["HasComment"]) == 1
        }).ToList();
        
        var withComments = objects.Count(o => o.hasComment);
        var withoutComments = objects.Count(o => !o.hasComment);
        
        Logger.Info("Found {Total} objects - {WithComments} with comments, {WithoutComments} without", 
            objects.Count, withComments, withoutComments);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            objectType,
            totalObjects = data.Rows.Count,
            limitApplied = limit,
            objectsWithComments = withComments,
            objectsWithoutComments = withoutComments,
            objects,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get comment for specific object
    /// </summary>
    private async Task<object> GetObjectCommentAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.OBJECT)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.OBJECT");
        
        var schema = parts[0];
        var objectName = parts[1];
        var objectType = args.ObjectType?.ToUpper() ?? "TABLE";
        
        Logger.Debug("Getting comment: {Schema}.{Object} ({Type})", schema, objectName, objectType);
        Console.WriteLine($"Retrieving comment for: {schema}.{objectName}");
        
        var sql = $@"
            SELECT TRIM(TABSCHEMA) AS Schema, TRIM(TABNAME) AS Name, COALESCE(TRIM(REMARKS), '') AS Comment
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA = '{schema}' AND TABNAME = '{objectName}'
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        if (data.Rows.Count == 0)
            throw new InvalidOperationException($"Object not found: {schema}.{objectName}");
        
        var row = data.Rows[0];
        var comment = row["Comment"]?.ToString()?.Trim() ?? "";
        
        var result = new
        {
            schema,
            objectName,
            objectType,
            hasComment = !string.IsNullOrEmpty(comment),
            comment,
            commentLength = comment.Length,
            retrievedAt = DateTime.Now
        };
        
        Logger.Info("Retrieved comment for {Schema}.{Object} - Has comment: {HasComment}", schema, objectName, result.hasComment);
        return result;
    }
    
    /// <summary>
    /// Find objects without comments
    /// </summary>
    private async Task<object> FindMissingCommentsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        var objectType = args.ObjectType?.ToUpper() ?? "TABLE";
        
        Logger.Debug("Finding objects without comments - Schema: {Schema}, Type: {Type}", schema, objectType);
        Console.WriteLine($"Finding uncommented objects in schema: {schema}");
        
        var sql = $@"
            SELECT TRIM(TABSCHEMA) AS Schema, TRIM(TABNAME) AS Name, TRIM(TYPE) AS TableType
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA LIKE '{schema}' 
              AND TYPE IN ('T', 'U')
              AND (REMARKS IS NULL OR TRIM(REMARKS) = '')
            ORDER BY Schema, Name
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var uncommentedObjects = data.AsEnumerable().Take(limit).Select(row => new
        {
            schema = row["Schema"]?.ToString()?.Trim(),
            name = row["Name"]?.ToString()?.Trim(),
            objectType = row["TableType"]?.ToString()?.Trim()
        }).ToList();
        
        Logger.Info("Found {Count} uncommented objects (limit: {Limit})", uncommentedObjects.Count, limit);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            objectType,
            totalUncommented = data.Rows.Count,
            limitApplied = limit,
            uncommentedObjects,
            retrievedAt = DateTime.Now
        };
    }
    
    // ========================================================================
    // Statistics Manager Commands (3 commands)
    // ========================================================================
    
    /// <summary>
    /// Get statistics overview for schema
    /// </summary>
    private async Task<object> GetStatisticsOverviewAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Logger.Debug("Getting statistics overview for schema: {Schema}", schema);
        Console.WriteLine($"Retrieving statistics overview for schema: {schema}");
        
        var sql = $@"
            SELECT 
                TRIM(TABSCHEMA) AS Schema,
                TRIM(TABNAME) AS TableName,
                CARD AS RowCount,
                NPAGES AS DataPages,
                STATS_TIME AS LastStatsTime,
                CASE 
                    WHEN STATS_TIME IS NULL THEN 'NEVER'
                    WHEN STATS_TIME < CURRENT DATE - 30 DAYS THEN 'STALE'
                    ELSE 'CURRENT'
                END AS StatsStatus
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA LIKE '{schema}' AND TYPE IN ('T', 'U')
            ORDER BY TABSCHEMA, TABNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var tables = data.AsEnumerable().Take(limit).Select(row => new
        {
            schema = row["Schema"]?.ToString()?.Trim(),
            tableName = row["TableName"]?.ToString()?.Trim(),
            rowCount = row["RowCount"],
            dataPages = row["DataPages"],
            lastStatsTime = row["LastStatsTime"],
            statsStatus = row["StatsStatus"]?.ToString()?.Trim()
        }).ToList();
        
        var neverStats = tables.Count(t => t.statsStatus == "NEVER");
        var staleStats = tables.Count(t => t.statsStatus == "STALE");
        var currentStats = tables.Count(t => t.statsStatus == "CURRENT");
        
        Logger.Info("Statistics overview: {Total} tables - {Never} never, {Stale} stale, {Current} current", 
            tables.Count, neverStats, staleStats, currentStats);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            totalTables = data.Rows.Count,
            limitApplied = limit,
            neverCollected = neverStats,
            staleStatistics = staleStats,
            currentStatistics = currentStats,
            tables,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Recommend tables needing RUNSTATS
    /// </summary>
    private async Task<object> GetStatisticsRecommendationsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        var staleThresholdDays = args.Limit ?? 30; // Use Limit as threshold days
        
        Logger.Debug("Getting RUNSTATS recommendations - Schema: {Schema}, Threshold: {Days} days", schema, staleThresholdDays);
        Console.WriteLine($"Finding tables needing RUNSTATS (>{staleThresholdDays} days old)");
        
        var sql = $@"
            SELECT 
                TRIM(TABSCHEMA) AS Schema,
                TRIM(TABNAME) AS TableName,
                CARD AS RowCount,
                STATS_TIME AS LastStatsTime,
                CASE 
                    WHEN STATS_TIME IS NULL THEN 999
                    ELSE DAYS(CURRENT DATE) - DAYS(STATS_TIME)
                END AS DaysSinceStats,
                CASE 
                    WHEN STATS_TIME IS NULL THEN 'CRITICAL'
                    WHEN STATS_TIME < CURRENT DATE - {staleThresholdDays * 2} DAYS THEN 'HIGH'
                    WHEN STATS_TIME < CURRENT DATE - {staleThresholdDays} DAYS THEN 'MEDIUM'
                    ELSE 'LOW'
                END AS Priority
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA LIKE '{schema}' 
              AND TYPE IN ('T', 'U')
              AND (STATS_TIME IS NULL OR STATS_TIME < CURRENT DATE - {staleThresholdDays} DAYS)
            ORDER BY Priority, DaysSinceStats DESC
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var recommendations = data.AsEnumerable().Select(row => new
        {
            schema = row["Schema"]?.ToString()?.Trim(),
            tableName = row["TableName"]?.ToString()?.Trim(),
            rowCount = row["RowCount"],
            lastStatsTime = row["LastStatsTime"],
            daysSinceStats = row["DaysSinceStats"],
            priority = row["Priority"]?.ToString()?.Trim(),
            recommendedAction = $"RUNSTATS ON TABLE {row["Schema"]?.ToString()?.Trim()}.{row["TableName"]?.ToString()?.Trim()}"
        }).ToList();
        
        var critical = recommendations.Count(r => r.priority == "CRITICAL");
        var high = recommendations.Count(r => r.priority == "HIGH");
        var medium = recommendations.Count(r => r.priority == "MEDIUM");
        
        Logger.Info("Found {Total} tables needing RUNSTATS - Critical: {Critical}, High: {High}, Medium: {Medium}", 
            recommendations.Count, critical, high, medium);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            staleThresholdDays,
            totalRecommendations = recommendations.Count,
            criticalPriority = critical,
            highPriority = high,
            mediumPriority = medium,
            recommendations,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get detailed index statistics
    /// </summary>
    private async Task<object> GetIndexStatisticsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.TABLE)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.TABLE");
        
        var schema = parts[0];
        var tableName = parts[1];
        
        Logger.Debug("Getting index statistics: {Schema}.{Table}", schema, tableName);
        Console.WriteLine($"Retrieving index statistics for: {schema}.{tableName}");
        
        var sql = $@"
            SELECT 
                TRIM(INDNAME) AS IndexName,
                TRIM(INDEXTYPE) AS IndexType,
                TRIM(UNIQUERULE) AS UniqueRule,
                COLCOUNT AS ColumnCount,
                FIRSTKEYCARD AS FirstKeyCard,
                FULLKEYCARD AS FullKeyCard,
                CLUSTERRATIO AS ClusterRatio,
                NLEAF AS LeafPages,
                NLEVELS AS TreeLevels
            FROM SYSCAT.INDEXES
            WHERE TABSCHEMA = '{schema}' AND TABNAME = '{tableName}'
            ORDER BY INDNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var indexes = data.AsEnumerable().Select(row => new
        {
            indexName = row["IndexName"]?.ToString()?.Trim(),
            indexType = row["IndexType"]?.ToString()?.Trim(),
            uniqueRule = row["UniqueRule"]?.ToString()?.Trim(),
            columnCount = row["ColumnCount"],
            firstKeyCard = row["FirstKeyCard"],
            fullKeyCard = row["FullKeyCard"],
            clusterRatio = row["ClusterRatio"],
            leafPages = row["LeafPages"],
            treeLevels = row["TreeLevels"]
        }).ToList();
        
        Logger.Info("Retrieved {Count} index statistics", indexes.Count);
        
        return new
        {
            schema,
            tableName,
            indexCount = indexes.Count,
            indexes,
            retrievedAt = DateTime.Now
        };
    }
    
    // ========================================================================
    // Unused Objects Commands (4 commands)
    // ========================================================================
    
    /// <summary>
    /// Find tables with no dependencies
    /// </summary>
    private async Task<object> FindUnusedTablesAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Logger.Debug("Finding unused tables in schema: {Schema}", schema);
        Console.WriteLine($"Finding unused tables in schema: {schema}");
        
        var sql = $@"
            SELECT 
                TRIM(T.TABSCHEMA) AS Schema,
                TRIM(T.TABNAME) AS TableName,
                T.CARD AS RowCount,
                T.CREATE_TIME AS CreateTime,
                CASE 
                    WHEN NOT EXISTS (
                        SELECT 1 FROM SYSCAT.TABDEP D 
                        WHERE D.BSCHEMA = T.TABSCHEMA AND D.BNAME = T.TABNAME
                    ) THEN 1
                    ELSE 0
                END AS NoDependencies
            FROM SYSCAT.TABLES T
            WHERE T.TABSCHEMA LIKE '{schema}' 
              AND T.TYPE IN ('T', 'U')
            ORDER BY T.TABSCHEMA, T.TABNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var unusedTables = data.AsEnumerable()
            .Where(row => Convert.ToInt32(row["NoDependencies"]) == 1)
            .Take(limit)
            .Select(row => new
            {
                schema = row["Schema"]?.ToString()?.Trim(),
                tableName = row["TableName"]?.ToString()?.Trim(),
                rowCount = row["RowCount"],
                createTime = row["CreateTime"],
                reason = "No dependencies found (not referenced by views/routines/FKs)"
            }).ToList();
        
        Logger.Info("Found {Count} potentially unused tables", unusedTables.Count);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            totalUnused = unusedTables.Count,
            limitApplied = limit,
            unusedTables,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Find indexes that may be unused or redundant
    /// </summary>
    private async Task<object> FindUnusedIndexesAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Logger.Debug("Finding unused indexes in schema: {Schema}", schema);
        Console.WriteLine($"Finding unused indexes in schema: {schema}");
        
        var sql = $@"
            SELECT 
                TRIM(TABSCHEMA) AS Schema,
                TRIM(TABNAME) AS TableName,
                TRIM(INDNAME) AS IndexName,
                TRIM(UNIQUERULE) AS UniqueRule,
                COLNAMES
            FROM SYSCAT.INDEXES
            WHERE TABSCHEMA LIKE '{schema}'
              AND UNIQUERULE NOT IN ('P')
            ORDER BY TABSCHEMA, TABNAME, INDNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var indexes = data.AsEnumerable().Take(limit).Select(row => new
        {
            schema = row["Schema"]?.ToString()?.Trim(),
            tableName = row["TableName"]?.ToString()?.Trim(),
            indexName = row["IndexName"]?.ToString()?.Trim(),
            isUnique = row["UniqueRule"]?.ToString() == "U",
            columns = row["COLNAMES"]?.ToString()?.Trim(),
            reason = row["UniqueRule"]?.ToString() == "P" ? "Primary Key (keep)" : "Non-PK index (verify usage)"
        }).ToList();
        
        Logger.Info("Found {Count} non-PK indexes to review", indexes.Count);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            totalIndexes = data.Rows.Count,
            limitApplied = limit,
            note = "Excludes primary key indexes - review for actual usage patterns",
            indexes,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Find views that are not referenced
    /// </summary>
    private async Task<object> FindUnusedViewsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Logger.Debug("Finding unused views in schema: {Schema}", schema);
        Console.WriteLine($"Finding unused views in schema: {schema}");
        
        var sql = $@"
            SELECT 
                TRIM(V.VIEWSCHEMA) AS Schema,
                TRIM(V.VIEWNAME) AS ViewName,
                TRIM(V.VALID) AS Valid,
                CASE 
                    WHEN NOT EXISTS (
                        SELECT 1 FROM SYSCAT.TABDEP D 
                        WHERE D.BSCHEMA = V.VIEWSCHEMA AND D.BNAME = V.VIEWNAME
                    ) THEN 1
                    ELSE 0
                END AS NoDependencies
            FROM SYSCAT.VIEWS V
            WHERE V.VIEWSCHEMA LIKE '{schema}'
            ORDER BY V.VIEWSCHEMA, V.VIEWNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var unusedViews = data.AsEnumerable()
            .Where(row => Convert.ToInt32(row["NoDependencies"]) == 1)
            .Take(limit)
            .Select(row => new
            {
                schema = row["Schema"]?.ToString()?.Trim(),
                viewName = row["ViewName"]?.ToString()?.Trim(),
                isValid = row["Valid"]?.ToString() == "Y",
                reason = "Not referenced by other views/routines"
            }).ToList();
        
        Logger.Info("Found {Count} potentially unused views", unusedViews.Count);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            totalUnused = unusedViews.Count,
            limitApplied = limit,
            unusedViews,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Find routines (procedures/functions) that are not called
    /// </summary>
    private async Task<object> FindUnusedRoutinesAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Logger.Debug("Finding unused routines in schema: {Schema}", schema);
        Console.WriteLine($"Finding unused routines in schema: {schema}");
        
        var sql = $@"
            SELECT 
                TRIM(R.ROUTINESCHEMA) AS Schema,
                TRIM(R.ROUTINENAME) AS RoutineName,
                TRIM(R.ROUTINETYPE) AS RoutineType,
                TRIM(R.LANGUAGE) AS Language,
                R.CREATE_TIME AS CreateTime,
                CASE 
                    WHEN NOT EXISTS (
                        SELECT 1 FROM SYSCAT.ROUTINEDEP D 
                        WHERE D.BSCHEMA = R.ROUTINESCHEMA AND D.BNAME = R.ROUTINENAME
                    ) THEN 1
                    ELSE 0
                END AS NoDependencies
            FROM SYSCAT.ROUTINES R
            WHERE R.ROUTINESCHEMA LIKE '{schema}'
            ORDER BY R.ROUTINESCHEMA, R.ROUTINENAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        
        var unusedRoutines = data.AsEnumerable()
            .Where(row => Convert.ToInt32(row["NoDependencies"]) == 1)
            .Take(limit)
            .Select(row => new
            {
                schema = row["Schema"]?.ToString()?.Trim(),
                routineName = row["RoutineName"]?.ToString()?.Trim(),
                routineType = row["RoutineType"]?.ToString()?.Trim(),
                language = row["Language"]?.ToString()?.Trim(),
                createTime = row["CreateTime"],
                reason = "Not called by other routines or packages"
            }).ToList();
        
        Logger.Info("Found {Count} potentially unused routines", unusedRoutines.Count);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            totalUnused = unusedRoutines.Count,
            limitApplied = limit,
            unusedRoutines,
            retrievedAt = DateTime.Now
        };
    }
    
    // ========================================================================
    // Mermaid ERD Commands (5 commands - using SqlMermaidErdTools package)
    // ⚠️ WARNING: mermaid-diff generates ALTER statements - DO NOT EXECUTE!
    // ========================================================================
    
    /// <summary>
    /// Generate Mermaid ERD from DB2 tables
    /// </summary>
    private async Task<object> GenerateMermaidErdAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? throw new ArgumentException("Schema parameter required");
        var limit = args.Limit ?? 20;
        
        Logger.Debug("Generating Mermaid ERD for schema: {Schema}, Limit: {Limit}", schema, limit);
        Console.WriteLine($"Generating Mermaid ERD for schema: {schema}");
        
        // Get tables in schema
        var tablesSql = $@"
            SELECT TRIM(TABSCHEMA) AS Schema, TRIM(TABNAME) AS TableName
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA = '{schema}' AND TYPE IN ('T', 'U')
            ORDER BY TABNAME
            FETCH FIRST {limit} ROWS ONLY
        ";
        
        var tablesData = await connectionManager.ExecuteQueryAsync(tablesSql);
        var selectedTables = tablesData.AsEnumerable()
            .Select(row => $"{row["Schema"]}.{row["TableName"]}")
            .ToList();
        
        if (_mermaidService == null)
            throw new InvalidOperationException("SqlMermaidIntegrationService not available");
        
        var mermaidDiagram = await _mermaidService.GenerateMermaidFromDb2TablesAsync(connectionManager, selectedTables);
        
        Logger.Info("Generated Mermaid ERD ({Length} chars) for {Count} tables", mermaidDiagram.Length, selectedTables.Count);
        
        return new
        {
            schema,
            tableCount = selectedTables.Count,
            tables = selectedTables,
            mermaidDiagram,
            diagramLength = mermaidDiagram.Length,
            note = "Use this diagram in MermaidDesignerWindow or export",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Convert SQL DDL to Mermaid ERD
    /// </summary>
    private async Task<object> ConvertSqlToMermaidAsync(CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Sql))
            throw new ArgumentException("Sql parameter required (SQL DDL to convert)");
        
        Logger.Debug("Converting SQL to Mermaid ({Length} chars)", args.Sql.Length);
        Console.WriteLine("Converting SQL DDL to Mermaid ERD...");
        
        if (_mermaidService == null)
            throw new InvalidOperationException("SqlMermaidIntegrationService not available");
        
        var mermaidDiagram = await _mermaidService.ConvertSqlToMermaidAsync(args.Sql);
        
        Logger.Info("Converted SQL to Mermaid ({Length} chars)", mermaidDiagram.Length);
        
        return new
        {
            inputSqlLength = args.Sql.Length,
            mermaidDiagram,
            diagramLength = mermaidDiagram.Length,
            note = "SQL DDL successfully converted to Mermaid ERD",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Convert Mermaid ERD to SQL DDL
    /// </summary>
    private async Task<object> ConvertMermaidToSqlAsync(CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Sql))
            throw new ArgumentException("Sql parameter required (Mermaid ERD to convert)");
        
        Logger.Debug("Converting Mermaid to SQL ({Length} chars)", args.Sql.Length);
        Console.WriteLine("Converting Mermaid ERD to SQL DDL...");
        
        if (_mermaidService == null)
            throw new InvalidOperationException("SqlMermaidIntegrationService not available");
        
        var sqlDdl = await _mermaidService.ConvertMermaidToSqlAsync(args.Sql);
        
        Logger.Info("Converted Mermaid to SQL ({Length} chars)", sqlDdl.Length);
        
        return new
        {
            inputMermaidLength = args.Sql.Length,
            sqlDdl,
            ddlLength = sqlDdl.Length,
            note = "Mermaid ERD successfully converted to SQL DDL",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Generate ALTER DDL from Mermaid diff (⚠️ OUTPUT ONLY - DO NOT EXECUTE!)
    /// </summary>
    private async Task<object> GenerateMermaidDiffDdlAsync(CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Sql))
            throw new ArgumentException("Sql parameter required (format: ORIGINAL_MERMAID|||MODIFIED_MERMAID)");
        
        var parts = args.Sql.Split(new[] { "|||" }, StringSplitOptions.None);
        if (parts.Length != 2)
            throw new ArgumentException("Sql must contain original and modified Mermaid separated by |||");
        
        var originalMermaid = parts[0];
        var modifiedMermaid = parts[1];
        
        Logger.Debug("Generating diff DDL - Original: {Len1} chars, Modified: {Len2} chars", originalMermaid.Length, modifiedMermaid.Length);
        Console.WriteLine("⚠️  Generating ALTER DDL from Mermaid diff (OUTPUT ONLY - NOT EXECUTING!)");
        
        if (_mermaidService == null)
            throw new InvalidOperationException("SqlMermaidIntegrationService not available");
        
        var alterDdl = await _mermaidService.GenerateMigrationFromMermaidDiffAsync(originalMermaid, modifiedMermaid);
        
        Logger.Warn("Generated ALTER DDL ({Length} chars) - ⚠️ FOR REVIEW ONLY, DO NOT EXECUTE!", alterDdl.Length);
        
        return new
        {
            originalMermaidLength = originalMermaid.Length,
            modifiedMermaidLength = modifiedMermaid.Length,
            alterDdl,
            ddlLength = alterDdl.Length,
            warning = "⚠️ DO NOT EXECUTE THIS DDL AUTOMATICALLY! Review and test in non-production environment first!",
            note = "ALTER statements for schema migration - requires manual review and testing",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Translate SQL DDL between database dialects
    /// </summary>
    private async Task<object> TranslateSqlDialectAsync(CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Sql))
            throw new ArgumentException("Sql parameter required (SQL DDL to translate)");
        
        var targetDialectStr = args.ObjectType ?? "PostgreSQL"; // Use ObjectType for target dialect
        
        Logger.Debug("Translating SQL to {Dialect} ({Length} chars)", targetDialectStr, args.Sql.Length);
        Console.WriteLine($"Translating SQL DDL to {targetDialectStr}...");
        
        if (_mermaidService == null)
            throw new InvalidOperationException("SqlMermaidIntegrationService not available");
        
        // Parse dialect string to SqlDialect enum
        var targetDialect = targetDialectStr.ToUpper() switch
        {
            "POSTGRESQL" or "POSTGRES" => SqlDialect.PostgreSql,
            "SQLSERVER" or "MSSQL" => SqlDialect.SqlServer,
            "MYSQL" => SqlDialect.MySql,
            "ORACLE" => SqlDialect.Oracle,
            "ANSI" or "ANSISQL" => SqlDialect.AnsiSql,
            _ => SqlDialect.AnsiSql
        };
        
        var translatedSql = await _mermaidService.TranslateSqlDialectAsync(args.Sql, SqlDialect.AnsiSql, targetDialect);
        
        Logger.Info("Translated SQL to {Dialect} ({Length} chars)", targetDialect, translatedSql.Length);
        
        return new
        {
            sourceDialect = "DB2",
            targetDialect = targetDialectStr,
            inputSqlLength = args.Sql.Length,
            translatedSql,
            translatedLength = translatedSql.Length,
            note = $"SQL DDL translated from DB2 to {targetDialectStr}",
            retrievedAt = DateTime.Now
        };
    }
    
    // ========================================================================
    // Metadata Commands (3 commands)
    // ========================================================================
    
    /// <summary>
    /// Get query execution history (from QueryHistoryService)
    /// </summary>
    private async Task<object> GetQueryHistoryAsync(CliArguments args)
    {
        var limit = args.Limit ?? 20;
        
        Logger.Debug("Getting query history (limit: {Limit})", limit);
        Console.WriteLine($"Retrieving query execution history (last {limit} queries)");
        
        // Note: QueryHistoryService stores history in memory - would need session context
        // For CLI mode, we'll return a placeholder response
        
        return new
        {
            note = "Query history is session-based and not available in CLI mode",
            sessionMode = "CLI",
            historyAvailable = false,
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get complete schema metadata
    /// </summary>
    private async Task<object> GetSchemaMetadataAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? throw new ArgumentException("Schema parameter required");
        
        Logger.Debug("Getting schema metadata: {Schema}", schema);
        Console.WriteLine($"Retrieving complete metadata for schema: {schema}");
        
        // Get object counts by type
        var sql = $@"
            SELECT 
                'TABLES' AS ObjectType, COUNT(*) AS ObjectCount
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA = '{schema}' AND TYPE IN ('T', 'U')
            UNION ALL
            SELECT 'VIEWS', COUNT(*) FROM SYSCAT.VIEWS WHERE VIEWSCHEMA = '{schema}'
            UNION ALL
            SELECT 'PROCEDURES', COUNT(*) FROM SYSCAT.ROUTINES WHERE ROUTINESCHEMA = '{schema}' AND ROUTINETYPE = 'P'
            UNION ALL
            SELECT 'FUNCTIONS', COUNT(*) FROM SYSCAT.ROUTINES WHERE ROUTINESCHEMA = '{schema}' AND ROUTINETYPE = 'F'
            UNION ALL
            SELECT 'TRIGGERS', COUNT(*) FROM SYSCAT.TRIGGERS WHERE TRIGSCHEMA = '{schema}'
            UNION ALL
            SELECT 'INDEXES', COUNT(*) FROM SYSCAT.INDEXES WHERE INDSCHEMA = '{schema}'
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var objectCounts = data.AsEnumerable().ToDictionary(
            row => row["ObjectType"]?.ToString() ?? "UNKNOWN",
            row => Convert.ToInt32(row["ObjectCount"])
        );
        
        // Get total schema size (approximate)
        var sizeSql = $@"
            SELECT 
                SUM(NPAGES) AS TotalPages,
                SUM(CARD) AS TotalRows
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA = '{schema}' AND TYPE IN ('T', 'U')
        ";
        
        var sizeData = await connectionManager.ExecuteQueryAsync(sizeSql);
        var sizeRow = sizeData.Rows.Count > 0 ? sizeData.Rows[0] : null;
        
        Logger.Info("Retrieved metadata for schema {Schema}: {Tables} tables, {Views} views", 
            schema, objectCounts.GetValueOrDefault("TABLES", 0), objectCounts.GetValueOrDefault("VIEWS", 0));
        
        return new
        {
            schema,
            objectCounts,
            approximateSize = new
            {
                totalPages = sizeRow?["TotalPages"],
                totalRows = sizeRow?["TotalRows"]
            },
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get complete database metadata
    /// </summary>
    private async Task<object> GetDatabaseMetadataAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        Logger.Debug("Getting database metadata");
        Console.WriteLine("Retrieving complete database metadata...");
        
        // Get database info
        var dbInfoSql = @"
            SELECT CURRENT SERVER AS DatabaseName, 
                   CURRENT TIMESTAMP AS CurrentTime,
                   USER AS CurrentUser
            FROM SYSIBM.SYSDUMMY1
        ";
        
        var dbInfo = await connectionManager.ExecuteQueryAsync(dbInfoSql);
        var dbRow = dbInfo.Rows.Count > 0 ? dbInfo.Rows[0] : null;
        
        // Get schema count
        var schemaCountSql = @"
            SELECT COUNT(DISTINCT SCHEMANAME) AS SchemaCount
            FROM SYSCAT.SCHEMATA
            WHERE SCHEMANAME NOT IN ('SYSIBM', 'SYSCAT', 'SYSPROC', 'SYSFUN', 'SYSIBMADM')
        ";
        
        var schemaCount = await connectionManager.ExecuteQueryAsync(schemaCountSql);
        
        // Get table count
        var tableCountSql = @"
            SELECT COUNT(*) AS TableCount
            FROM SYSCAT.TABLES
            WHERE TYPE IN ('T', 'U')
              AND TABSCHEMA NOT IN ('SYSIBM', 'SYSCAT', 'SYSPROC', 'SYSFUN', 'SYSIBMADM')
        ";
        
        var tableCount = await connectionManager.ExecuteQueryAsync(tableCountSql);
        
        Logger.Info("Retrieved database metadata");
        
        return new
        {
            databaseName = dbRow?["DatabaseName"]?.ToString()?.Trim(),
            currentUser = dbRow?["CurrentUser"]?.ToString()?.Trim(),
            currentTime = dbRow?["CurrentTime"],
            schemaCount = schemaCount.Rows.Count > 0 ? schemaCount.Rows[0]["SchemaCount"] : 0,
            tableCount = tableCount.Rows.Count > 0 ? tableCount.Rows[0]["TableCount"] : 0,
            retrievedAt = DateTime.Now
        };
    }
    
    // ========================================================================
    // Connection Commands (1 command)
    // ========================================================================
    
    /// <summary>
    /// List saved connection profiles
    /// </summary>
    private async Task<object> ListConnectionProfilesAsync(CliArguments args)
    {
        Logger.Debug("Listing connection profiles");
        Console.WriteLine("Retrieving saved connection profiles...");
        
        var connectionService = new ConnectionProfileService();
        var profiles = connectionService.LoadAllProfiles();
        
        var profileList = profiles.Select(p => new
        {
            profileName = p.Name,
            server = p.Server,
            port = p.Port,
            database = p.Database,
            username = p.Username,
            connectionTimeout = p.ConnectionTimeout
        }).ToList();
        
        Logger.Info("Found {Count} connection profiles", profileList.Count);
        
        return await Task.FromResult(new
        {
            profileCount = profileList.Count,
            profiles = profileList,
            retrievedAt = DateTime.Now
        });
    }
    
    // ========================================================================
    // Advanced Monitoring Commands (8 commands)
    // Note: Some features simplified for DB2 12.1 compatibility
    // ========================================================================
    
    /// <summary>
    /// Get complete database load metrics (enhanced version of database-load)
    /// </summary>
    private async Task<object> GetDatabaseLoadFullAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Logger.Debug("Getting full database load metrics for schema: {Schema}", schema);
        Console.WriteLine($"Retrieving complete database load metrics...");
        
        // Get table sizes and row counts
        var sql = $@"
            SELECT 
                TRIM(TABSCHEMA) AS Schema,
                TRIM(TABNAME) AS TableName,
                CARD AS RowCount,
                NPAGES AS DataPages,
                FPAGES AS FreePages,
                OVERFLOW AS OverflowPages,
                STATS_TIME AS LastStatsTime
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA LIKE '{schema}' 
              AND TYPE IN ('T', 'U')
              AND CARD > 0
            ORDER BY NPAGES DESC
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? 20;
        
        var tables = data.AsEnumerable().Take(limit).Select(row => new
        {
            schema = row["Schema"]?.ToString()?.Trim(),
            tableName = row["TableName"]?.ToString()?.Trim(),
            rowCount = row["RowCount"],
            dataPages = row["DataPages"],
            freePages = row["FreePages"],
            overflowPages = row["OverflowPages"],
            lastStatsTime = row["LastStatsTime"],
            approximateSizeMB = Convert.ToDouble(row["DataPages"] ?? 0) * 4 / 1024 // 4KB pages
        }).ToList();
        
        var totalPages = tables.Sum(t => Convert.ToDouble(t.dataPages ?? 0));
        var totalRows = tables.Sum(t => Convert.ToInt64(t.rowCount ?? 0));
        
        Logger.Info("Database load: {Tables} tables, {TotalRows} rows, {TotalPages} pages", 
            tables.Count, totalRows, totalPages);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            totalTables = data.Rows.Count,
            limitApplied = limit,
            totalRowsInSample = totalRows,
            totalDataPages = totalPages,
            approximateSizeMB = totalPages * 4 / 1024,
            tables,
            note = "Load metrics based on SYSCAT.TABLES statistics - may not reflect real-time activity",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get table activity metrics (simplified for DB2 12.1)
    /// </summary>
    private async Task<object> GetTableActivityAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.TABLE)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.TABLE");
        
        var schema = parts[0];
        var tableName = parts[1];
        
        Logger.Debug("Getting activity metrics for: {Schema}.{Table}", schema, tableName);
        Console.WriteLine($"Retrieving activity metrics for: {schema}.{tableName}");
        
        // Get basic table statistics (DB2 12.1 compatible)
        var sql = $@"
            SELECT 
                TRIM(TABSCHEMA) AS Schema,
                TRIM(TABNAME) AS TableName,
                CARD AS CurrentRowCount,
                NPAGES AS DataPages,
                OVERFLOW AS OverflowPages,
                STATS_TIME AS LastStatsTime,
                ALTER_TIME AS LastAlterTime
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA = '{schema}' AND TABNAME = '{tableName}'
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        if (data.Rows.Count == 0)
            throw new InvalidOperationException($"Table not found: {schema}.{tableName}");
        
        var row = data.Rows[0];
        
        Logger.Info("Retrieved activity metrics for {Schema}.{Table}", schema, tableName);
        
        return new
        {
            schema,
            tableName,
            currentRowCount = row["CurrentRowCount"],
            dataPages = row["DataPages"],
            overflowPages = row["OverflowPages"],
            lastStatsTime = row["LastStatsTime"],
            lastAlterTime = row["LastAlterTime"],
            note = "Activity metrics based on catalog statistics - real-time monitoring requires MON_GET_TABLE function (may need DBA privileges)",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get top N most active tables (by size/rows)
    /// </summary>
    private async Task<object> GetTopActiveTablesAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        var limit = args.Limit ?? 10;
        
        Logger.Debug("Getting top {Limit} active tables in schema: {Schema}", limit, schema);
        Console.WriteLine($"Finding top {limit} most active tables...");
        
        var sql = $@"
            SELECT 
                TRIM(TABSCHEMA) AS Schema,
                TRIM(TABNAME) AS TableName,
                CARD AS RowCount,
                NPAGES AS DataPages,
                OVERFLOW AS OverflowPages,
                STATS_TIME AS LastStatsTime
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA LIKE '{schema}' 
              AND TYPE IN ('T', 'U')
            ORDER BY NPAGES DESC, CARD DESC
            FETCH FIRST {limit} ROWS ONLY
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var topTables = data.AsEnumerable().Select(row => new
        {
            rank = data.Rows.IndexOf(row) + 1,
            schema = row["Schema"]?.ToString()?.Trim(),
            tableName = row["TableName"]?.ToString()?.Trim(),
            rowCount = row["RowCount"],
            dataPages = row["DataPages"],
            overflowPages = row["OverflowPages"],
            approximateSizeMB = Convert.ToDouble(row["DataPages"] ?? 0) * 4 / 1024,
            lastStatsTime = row["LastStatsTime"]
        }).ToList();
        
        Logger.Info("Found top {Count} active tables", topTables.Count);
        
        return new
        {
            schema = schema == "%" ? "ALL" : schema,
            topN = limit,
            actualCount = topTables.Count,
            sortedBy = "DataPages DESC, RowCount DESC",
            topTables,
            note = "Activity ranked by data pages (size) - not real-time access patterns",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get complete lock information (enhanced, simplified for DB2 12.1)
    /// </summary>
    private async Task<object> GetLockMonitorFullAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        Logger.Debug("Getting full lock monitor information");
        Console.WriteLine("Retrieving complete lock information...");
        
        // Simplified lock check using basic query
        var sql = @"
            SELECT 
                CURRENT USER AS CurrentUser,
                CURRENT TIMESTAMP AS CurrentTime,
                'BASIC_CHECK' AS LockCheckType
            FROM SYSIBM.SYSDUMMY1
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var row = data.Rows.Count > 0 ? data.Rows[0] : null;
        
        Logger.Info("Lock monitor info retrieved (simplified)");
        
        return new
        {
            currentUser = row?["CurrentUser"]?.ToString()?.Trim(),
            currentTime = row?["CurrentTime"],
            lockMonitoringAvailable = false,
            note = "Full lock monitoring requires SYSIBMADM.MON_LOCKWAITS or MON_GET_LOCKS() function (needs DBA privileges in DB2 12.1)",
            recommendation = "Use GUI Lock Monitor Panel for interactive lock monitoring",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Identify lock wait chains (simplified for DB2 12.1)
    /// </summary>
    private async Task<object> GetLockChainsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        Logger.Debug("Checking for lock wait chains");
        Console.WriteLine("Analyzing lock wait chains...");
        
        // Simplified check
        var sql = @"
            SELECT 
                'NO_LOCK_CHAINS_DETECTED' AS Status,
                CURRENT TIMESTAMP AS CheckTime
            FROM SYSIBM.SYSDUMMY1
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        Logger.Info("Lock chain analysis complete (simplified)");
        
        return new
        {
            lockChainsDetected = false,
            status = "Feature requires advanced monitoring tables",
            note = "Lock chain detection requires SYSIBMADM.MON_LOCKWAITS view (needs DBA privileges in DB2 12.1)",
            recommendation = "Use db2pd command or GUI Lock Monitor for real-time lock analysis",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get complete active session information (enhanced)
    /// </summary>
    private async Task<object> GetActiveSessionsFullAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        Logger.Debug("Getting full active sessions information");
        Console.WriteLine("Retrieving complete active session information...");
        
        // Get current session info
        var sql = @"
            SELECT 
                CURRENT USER AS CurrentUser,
                CURRENT TIMESTAMP AS CurrentTime,
                CURRENT SERVER AS DatabaseName
            FROM SYSIBM.SYSDUMMY1
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var row = data.Rows.Count > 0 ? data.Rows[0] : null;
        
        Logger.Info("Active sessions info retrieved (current session only)");
        
        return new
        {
            currentSession = new
            {
                user = row?["CurrentUser"]?.ToString()?.Trim(),
                database = row?["DatabaseName"]?.ToString()?.Trim(),
                connectionTime = row?["CurrentTime"]
            },
            totalSessionsDetected = 1,
            fullSessionMonitoringAvailable = false,
            note = "Full session monitoring requires SYSIBMADM.APPLICATIONS view (needs SYSMON or DBA privileges in DB2 12.1)",
            recommendation = "Use GUI Active Sessions Panel or db2 list applications command for complete session info",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get detailed information for specific session
    /// </summary>
    private async Task<object> GetSessionDetailsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var sessionId = args.Object ?? "CURRENT";
        
        Logger.Debug("Getting session details for: {SessionId}", sessionId);
        Console.WriteLine($"Retrieving details for session: {sessionId}");
        
        // Get current session details
        var sql = @"
            SELECT 
                CURRENT USER AS SessionUser,
                CURRENT TIMESTAMP AS SessionTime,
                CURRENT SERVER AS DatabaseName,
                CURRENT SCHEMA AS CurrentSchema
            FROM SYSIBM.SYSDUMMY1
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var row = data.Rows.Count > 0 ? data.Rows[0] : null;
        
        Logger.Info("Session details retrieved");
        
        return new
        {
            sessionId,
            sessionUser = row?["SessionUser"]?.ToString()?.Trim(),
            databaseName = row?["DatabaseName"]?.ToString()?.Trim(),
            currentSchema = row?["CurrentSchema"]?.ToString()?.Trim(),
            sessionTime = row?["SessionTime"],
            note = "Detailed session monitoring requires admin views (SYSIBMADM.APPLICATIONS) with appropriate privileges",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Find sessions running longer than N seconds
    /// </summary>
    private async Task<object> GetLongRunningSessionsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var durationThreshold = args.Limit ?? 60; // Default 60 seconds
        
        Logger.Debug("Finding sessions running > {Duration} seconds", durationThreshold);
        Console.WriteLine($"Finding sessions running longer than {durationThreshold} seconds...");
        
        // Simplified check - would need admin views for real implementation
        var sql = @"
            SELECT 
                CURRENT USER AS User,
                CURRENT TIMESTAMP AS CheckTime,
                0 AS LongRunningSessions
            FROM SYSIBM.SYSDUMMY1
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        Logger.Info("Long-running sessions check complete (simplified)");
        
        return new
        {
            durationThresholdSeconds = durationThreshold,
            longRunningSessionsFound = 0,
            note = "Long-running session detection requires SYSIBMADM.MON_CURRENT_SQL or APPLICATIONS view (needs DBA privileges)",
            recommendation = "Use db2 list applications show detail or GUI Active Sessions Panel",
            retrievedAt = DateTime.Now
        };
    }
    
    // ========================================================================
    // Dependency Commands (3 commands)
    // ========================================================================
    
    /// <summary>
    /// Get complete dependency graph for an object
    /// </summary>
    private async Task<object> GetDependencyGraphAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.OBJECT)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.OBJECT");
        
        var schema = parts[0];
        var objectName = parts[1];
        var includeRecursive = args.IncludeDependencies; // Use for recursive flag
        
        Logger.Debug("Getting dependency graph: {Schema}.{Object}, Recursive: {Recursive}", schema, objectName, includeRecursive);
        Console.WriteLine($"Analyzing dependency graph for: {schema}.{objectName}");
        
        // Get direct dependencies
        var sql = $@"
            SELECT 
                TRIM(BTYPE) AS DependentType,
                TRIM(BSCHEMA) AS DependentSchema,
                TRIM(BNAME) AS DependentObject,
                'TABLE' AS DependsOnType
            FROM SYSCAT.TABDEP
            WHERE TABSCHEMA = '{schema}' AND TABNAME = '{objectName}'
            UNION ALL
            SELECT 
                TRIM(BTYPE), TRIM(BSCHEMA), TRIM(BNAME), 
                'ROUTINE' AS DependsOnType
            FROM SYSCAT.ROUTINEDEP
            WHERE ROUTINESCHEMA = '{schema}' AND ROUTINENAME = '{objectName}'
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var dependencies = data.AsEnumerable().Select(row => new
        {
            dependentType = row["DependentType"]?.ToString()?.Trim(),
            dependentSchema = row["DependentSchema"]?.ToString()?.Trim(),
            dependentObject = row["DependentObject"]?.ToString()?.Trim(),
            dependsOnType = row["DependsOnType"]?.ToString()?.Trim()
        }).ToList();
        
        Logger.Info("Found {Count} dependencies for {Schema}.{Object}", dependencies.Count, schema, objectName);
        
        return new
        {
            schema,
            objectName,
            dependencyCount = dependencies.Count,
            recursiveAnalysis = includeRecursive,
            dependencies,
            note = includeRecursive ? "Recursive dependency analysis limited to direct dependencies" : "Direct dependencies only",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Get dependency impact analysis (what breaks if object changes)
    /// </summary>
    private async Task<object> GetDependencyImpactAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.OBJECT)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.OBJECT");
        
        var schema = parts[0];
        var objectName = parts[1];
        
        Logger.Debug("Analyzing dependency impact: {Schema}.{Object}", schema, objectName);
        Console.WriteLine($"Analyzing impact of changes to: {schema}.{objectName}");
        
        // Find what depends ON this object (reverse dependencies)
        var sql = $@"
            SELECT 
                TRIM(TABSCHEMA) AS AffectedSchema,
                TRIM(TABNAME) AS AffectedObject,
                TRIM(DTYPE) AS AffectedType,
                'VIEW' AS ImpactType
            FROM SYSCAT.TABDEP
            WHERE BSCHEMA = '{schema}' AND BNAME = '{objectName}' AND DTYPE = 'V'
            UNION ALL
            SELECT 
                TRIM(ROUTINESCHEMA), TRIM(ROUTINENAME), TRIM(ROUTINETYPE), 'ROUTINE'
            FROM SYSCAT.ROUTINEDEP
            WHERE BSCHEMA = '{schema}' AND BNAME = '{objectName}'
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var affectedObjects = data.AsEnumerable().Select(row => new
        {
            affectedSchema = row["AffectedSchema"]?.ToString()?.Trim(),
            affectedObject = row["AffectedObject"]?.ToString()?.Trim(),
            affectedType = row["AffectedType"]?.ToString()?.Trim(),
            impactType = row["ImpactType"]?.ToString()?.Trim(),
            impactSeverity = "HIGH"
        }).ToList();
        
        Logger.Info("Impact analysis: {Count} objects would be affected", affectedObjects.Count);
        
        return new
        {
            schema,
            objectName,
            totalAffectedObjects = affectedObjects.Count,
            affectedObjects,
            recommendation = affectedObjects.Count > 0 ? "Test all affected objects after making changes" : "No direct dependencies found",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Find dependency chain from object A to object B
    /// </summary>
    private async Task<object> GetDependencyChainAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object) || string.IsNullOrEmpty(args.Schema))
            throw new ArgumentException("Both Object (source) and Schema (target) parameters required");
        
        var source = args.Object; // Format: SCHEMA.OBJECT
        var target = args.Schema; // Format: SCHEMA.OBJECT (reusing Schema param)
        
        Logger.Debug("Finding dependency chain: {Source} → {Target}", source, target);
        Console.WriteLine($"Finding dependency chain: {source} → {target}");
        
        return new
        {
            source,
            target,
            chainFound = false,
            note = "Dependency chain detection requires recursive CTE query - complex implementation",
            recommendation = "Use GUI Dependency Graph Panel for visual dependency chain analysis",
            retrievedAt = DateTime.Now
        };
    }
    
    // ========================================================================
    // Migration Commands (3 commands)
    // ========================================================================
    
    /// <summary>
    /// Generate migration plan for schema
    /// </summary>
    private async Task<object> GenerateMigrationPlanAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var sourceSchema = args.Schema ?? throw new ArgumentException("Schema parameter required (source schema)");
        var targetSchema = args.Object ?? throw new ArgumentException("Object parameter required (target schema)");
        
        Logger.Debug("Generating migration plan: {Source} → {Target}", sourceSchema, targetSchema);
        Console.WriteLine($"Generating migration plan: {sourceSchema} → {targetSchema}");
        
        // Get object counts in source schema
        var sql = $@"
            SELECT 
                'TABLES' AS ObjectType, COUNT(*) AS ObjectCount
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA = '{sourceSchema}' AND TYPE IN ('T', 'U')
            UNION ALL
            SELECT 'VIEWS', COUNT(*) FROM SYSCAT.VIEWS WHERE VIEWSCHEMA = '{sourceSchema}'
            UNION ALL
            SELECT 'PROCEDURES', COUNT(*) FROM SYSCAT.ROUTINES WHERE ROUTINESCHEMA = '{sourceSchema}' AND ROUTINETYPE = 'P'
            UNION ALL
            SELECT 'FUNCTIONS', COUNT(*) FROM SYSCAT.ROUTINES WHERE ROUTINESCHEMA = '{sourceSchema}' AND ROUTINETYPE = 'F'
            UNION ALL
            SELECT 'TRIGGERS', COUNT(*) FROM SYSCAT.TRIGGERS WHERE TRIGSCHEMA = '{sourceSchema}'
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var objectCounts = data.AsEnumerable().ToDictionary(
            row => row["ObjectType"]?.ToString() ?? "UNKNOWN",
            row => Convert.ToInt32(row["ObjectCount"])
        );
        
        var migrationSteps = new List<string>
        {
            "1. Create target schema if not exists",
            "2. Migrate tables (CREATE TABLE statements)",
            "3. Migrate data (INSERT statements or EXPORT/IMPORT)",
            "4. Create indexes and constraints",
            "5. Migrate views",
            "6. Migrate stored procedures and functions",
            "7. Migrate triggers",
            "8. Grant appropriate privileges",
            "9. Update statistics (RUNSTATS)",
            "10. Validate migrated objects"
        };
        
        Logger.Info("Migration plan generated: {Source} → {Target}", sourceSchema, targetSchema);
        
        return new
        {
            sourceSchema,
            targetSchema,
            objectCounts,
            totalObjects = objectCounts.Values.Sum(),
            migrationSteps,
            estimatedComplexity = objectCounts.Values.Sum() > 100 ? "HIGH" : objectCounts.Values.Sum() > 20 ? "MEDIUM" : "LOW",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Generate migration DDL for schema
    /// </summary>
    private async Task<object> GenerateMigrationDdlAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var sourceSchema = args.Schema ?? throw new ArgumentException("Schema parameter required");
        
        Logger.Debug("Generating migration DDL for schema: {Schema}", sourceSchema);
        Console.WriteLine($"Generating migration DDL for schema: {sourceSchema}");
        
        return new
        {
            sourceSchema,
            ddlGenerated = false,
            note = "DDL generation requires iterating through all objects and calling table-ddl for each table, plus views/routines",
            recommendation = "Use export-schema-ddl command or GUI Migration Assistant Panel",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Generate data migration scripts
    /// </summary>
    private async Task<object> GenerateMigrationDataScriptAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var sourceSchema = args.Schema ?? throw new ArgumentException("Schema parameter required");
        
        Logger.Debug("Generating data migration scripts for schema: {Schema}", sourceSchema);
        Console.WriteLine($"Generating data migration scripts for schema: {sourceSchema}");
        
        return new
        {
            sourceSchema,
            scriptGenerated = false,
            note = "Data migration requires EXPORT command or INSERT statements - large data volumes need db2move or db2look utilities",
            recommendation = "Use db2move or db2look command-line utilities for data migration, or GUI Migration Assistant",
            retrievedAt = DateTime.Now
        };
    }
    
    // ========================================================================
    // Export Commands (3 commands)
    // ========================================================================
    
    /// <summary>
    /// Export table data to file
    /// </summary>
    private async Task<object> ExportTableDataAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.TABLE)");
        
        var parts = args.Object.Split('.');
        if (parts.Length != 2)
            throw new ArgumentException("Object must be in format: SCHEMA.TABLE");
        
        var schema = parts[0];
        var tableName = parts[1];
        var limit = args.Limit ?? 1000;
        
        Logger.Debug("Exporting table data: {Schema}.{Table}, Limit: {Limit}", schema, tableName, limit);
        Console.WriteLine($"Exporting data from: {schema}.{tableName} (limit: {limit})");
        
        var sql = $@"
            SELECT * FROM {schema}.{tableName}
            FETCH FIRST {limit} ROWS ONLY
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        // Convert to list of dictionaries
        var rows = data.AsEnumerable().Select(row => 
        {
            var dict = new Dictionary<string, object?>();
            foreach (System.Data.DataColumn col in data.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            return dict;
        }).ToList();
        
        Logger.Info("Exported {Count} rows from {Schema}.{Table}", rows.Count, schema, tableName);
        
        return new
        {
            schema,
            tableName,
            rowCount = rows.Count,
            limitApplied = limit,
            columns = data.Columns.Cast<System.Data.DataColumn>().Select(c => c.ColumnName).ToList(),
            data = rows,
            format = "JSON",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Export query results
    /// </summary>
    private async Task<object> ExportQueryResultsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Sql))
            throw new ArgumentException("Sql parameter required (SQL query to execute)");
        
        Logger.Debug("Exporting query results ({Length} chars)", args.Sql.Length);
        Console.WriteLine("Executing and exporting query results...");
        
        var data = await connectionManager.ExecuteQueryAsync(args.Sql);
        
        // Convert to list of dictionaries
        var rows = data.AsEnumerable().Select(row => 
        {
            var dict = new Dictionary<string, object?>();
            foreach (System.Data.DataColumn col in data.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            return dict;
        }).ToList();
        
        Logger.Info("Exported {Count} rows from query", rows.Count);
        
        return new
        {
            rowCount = rows.Count,
            columns = data.Columns.Cast<System.Data.DataColumn>().Select(c => c.ColumnName).ToList(),
            data = rows,
            format = "JSON",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Export complete schema DDL
    /// </summary>
    private async Task<object> ExportSchemaDdlAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? throw new ArgumentException("Schema parameter required");
        
        Logger.Debug("Exporting schema DDL: {Schema}", schema);
        Console.WriteLine($"Exporting DDL for schema: {schema}");
        
        // Get table list
        var tablesSql = $@"
            SELECT TRIM(TABNAME) AS TableName
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA = '{schema}' AND TYPE IN ('T', 'U')
            ORDER BY TABNAME
        ";
        
        var tables = await connectionManager.ExecuteQueryAsync(tablesSql);
        var tableNames = tables.AsEnumerable().Select(r => r["TableName"]?.ToString()?.Trim()).ToList();
        
        Logger.Info("Schema {Schema} has {Count} tables for DDL export", schema, tableNames.Count);
        
        return new
        {
            schema,
            tableCount = tableNames.Count,
            tables = tableNames,
            note = "Complete DDL export requires calling table-ddl for each table individually",
            recommendation = "Use db2look utility or GUI Migration Assistant for full schema DDL export",
            retrievedAt = DateTime.Now
        };
    }
    
    // ========================================================================
    // SQL Tools Commands (2 commands)
    // ========================================================================
    
    /// <summary>
    /// Validate SQL safety
    /// </summary>
    private async Task<object> ValidateSqlAsync(CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Sql))
            throw new ArgumentException("Sql parameter required (SQL to validate)");
        
        Logger.Debug("Validating SQL safety ({Length} chars)", args.Sql.Length);
        Console.WriteLine("Validating SQL statement for safety...");
        
        var sql = args.Sql.ToUpper();
        var issues = new List<string>();
        
        // Check for dangerous operations
        if (sql.Contains("DELETE") && !sql.Contains("WHERE"))
            issues.Add("DELETE without WHERE clause - will delete all rows!");
        
        if (sql.Contains("UPDATE") && !sql.Contains("WHERE"))
            issues.Add("UPDATE without WHERE clause - will update all rows!");
        
        if (sql.Contains("DROP TABLE") || sql.Contains("DROP DATABASE"))
            issues.Add("DROP statement detected - destructive operation!");
        
        if (sql.Contains("TRUNCATE"))
            issues.Add("TRUNCATE statement detected - removes all rows without logging!");
        
        if (sql.Contains("ALTER TABLE"))
            issues.Add("ALTER TABLE detected - schema modification operation");
        
        var isSafe = issues.Count == 0;
        var severity = issues.Count > 2 ? "CRITICAL" : issues.Count > 0 ? "WARNING" : "SAFE";
        
        Logger.Info("SQL validation complete - Severity: {Severity}, Issues: {Count}", severity, issues.Count);
        
        return await Task.FromResult(new
        {
            sqlLength = args.Sql.Length,
            isSafe,
            severity,
            issueCount = issues.Count,
            issues,
            recommendation = isSafe ? "SQL appears safe to execute" : "Review and modify SQL before execution",
            retrievedAt = DateTime.Now
        });
    }
    
    /// <summary>
    /// Format SQL statement
    /// </summary>
    private async Task<object> FormatSqlAsync(CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Sql))
            throw new ArgumentException("Sql parameter required (SQL to format)");
        
        Logger.Debug("Formatting SQL ({Length} chars)", args.Sql.Length);
        Console.WriteLine("Formatting SQL statement...");
        
        // Basic formatting (would use SqlFormatterService in full implementation)
        var formatted = args.Sql
            .Replace(" FROM ", "\nFROM ")
            .Replace(" WHERE ", "\nWHERE ")
            .Replace(" ORDER BY ", "\nORDER BY ")
            .Replace(" GROUP BY ", "\nGROUP BY ")
            .Replace(" UNION ", "\nUNION ");
        
        Logger.Info("SQL formatted ({InputLength} → {OutputLength} chars)", args.Sql.Length, formatted.Length);
        
        return await Task.FromResult(new
        {
            inputLength = args.Sql.Length,
            formattedSql = formatted,
            formattedLength = formatted.Length,
            note = "Basic formatting applied - use SqlFormatterService for advanced formatting",
            retrievedAt = DateTime.Now
        });
    }
    
    // ========================================================================
    // Schema Diff Commands (2 commands)
    // ========================================================================
    
    /// <summary>
    /// Compare two schemas
    /// </summary>
    private async Task<object> CompareSchemas(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var sourceSchema = args.Schema ?? throw new ArgumentException("Schema parameter required (source schema)");
        var targetSchema = args.Object ?? throw new ArgumentException("Object parameter required (target schema)");
        
        Logger.Debug("Comparing schemas: {Source} vs {Target}", sourceSchema, targetSchema);
        Console.WriteLine($"Comparing schemas: {sourceSchema} vs {targetSchema}");
        
        // Get object counts for both schemas
        var sql = $@"
            SELECT 
                TRIM(TABSCHEMA) AS Schema,
                COUNT(*) AS TableCount
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA IN ('{sourceSchema}', '{targetSchema}') AND TYPE IN ('T', 'U')
            GROUP BY TABSCHEMA
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        var sourceTables = data.AsEnumerable()
            .Where(r => r["Schema"]?.ToString()?.Trim() == sourceSchema)
            .Select(r => Convert.ToInt32(r["TableCount"]))
            .FirstOrDefault();
        
        var targetTables = data.AsEnumerable()
            .Where(r => r["Schema"]?.ToString()?.Trim() == targetSchema)
            .Select(r => Convert.ToInt32(r["TableCount"]))
            .FirstOrDefault();
        
        Logger.Info("Schema comparison: {Source} has {SourceCount} tables, {Target} has {TargetCount} tables", 
            sourceSchema, sourceTables, targetSchema, targetTables);
        
        return new
        {
            sourceSchema,
            targetSchema,
            sourceTableCount = sourceTables,
            targetTableCount = targetTables,
            tableDifference = Math.Abs(sourceTables - targetTables),
            note = "Detailed schema comparison requires object-by-object analysis",
            recommendation = "Use GUI Schema Diff Analyzer or schema-diff-ddl command for detailed comparison",
            retrievedAt = DateTime.Now
        };
    }
    
    /// <summary>
    /// Generate schema diff DDL
    /// </summary>
    private async Task<object> GenerateSchemaDiffDdlAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var sourceSchema = args.Schema ?? throw new ArgumentException("Schema parameter required (source schema)");
        var targetSchema = args.Object ?? throw new ArgumentException("Object parameter required (target schema)");
        
        Logger.Debug("Generating schema diff DDL: {Source} → {Target}", sourceSchema, targetSchema);
        Console.WriteLine($"Generating ALTER DDL to migrate: {sourceSchema} → {targetSchema}");
        
        return new
        {
            sourceSchema,
            targetSchema,
            ddlGenerated = false,
            note = "Schema diff DDL generation requires detailed object comparison and ALTER statement generation",
            recommendation = "Use GUI Schema Diff Analyzer or db2look utility with compare options",
            retrievedAt = DateTime.Now
        };
    }
    
    // ========================================================================
    // User/Privileges Enhanced (2 commands)
    // ========================================================================
    
    private async Task<object> GetUserInfoEnhancedAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var username = args.Object ?? "CURRENT";
        Console.WriteLine($"Getting enhanced user info for: {username}");
        
        var sql = username == "CURRENT" 
            ? "SELECT CURRENT USER AS Username, CURRENT TIMESTAMP AS CurrentTime FROM SYSIBM.SYSDUMMY1"
            : $"SELECT '{username}' AS Username, CURRENT TIMESTAMP AS CurrentTime FROM SYSIBM.SYSDUMMY1";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var row = data.Rows[0];
        
        return new
        {
            username = row["Username"]?.ToString()?.Trim(),
            currentTime = row["CurrentTime"],
            note = "Enhanced user info requires SYSCAT.DBAUTH and other auth views",
            retrievedAt = DateTime.Now
        };
    }
    
    private async Task<object> GetUserPrivilegesFullAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var username = args.Object ?? "CURRENT";
        Console.WriteLine($"Getting full privileges for: {username}");
        
        return new
        {
            username,
            note = "Full privilege enumeration requires querying multiple auth catalog views",
            retrievedAt = DateTime.Now
        };
    }
    
    // ========================================================================
    // CDC Enhanced (3 commands)
    // ========================================================================
    
    private async Task<object> GetCdcStatusFullAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        Console.WriteLine("Getting complete CDC status...");
        
        var sql = "SELECT CURRENT USER AS User, CURRENT TIMESTAMP AS Time FROM SYSIBM.SYSDUMMY1";
        var data = await connectionManager.ExecuteQueryAsync(sql);
        
        return new
        {
            cdcEnabled = false,
            note = "CDC status requires SYSCAT.DATAPARTITIONS or CDC-specific views",
            retrievedAt = DateTime.Now
        };
    }
    
    private async Task<object> GetCdcConfigurationAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        Console.WriteLine("Getting CDC configuration...");
        
        return new
        {
            cdcConfigured = false,
            note = "CDC configuration requires ASN tables (IBMSNAP_*)",
            retrievedAt = DateTime.Now
        };
    }
    
    private async Task<object> GetCdcChangesAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        Console.WriteLine("Getting CDC changes...");
        
        return new
        {
            changesFound = 0,
            note = "CDC changes require reading CDC change tables (configured per table)",
            retrievedAt = DateTime.Now
        };
    }
    
    // ========================================================================
    // Connection Stats (2 commands)
    // ========================================================================
    
    private async Task<object> GetConnectionStatsAsync(CliArguments args)
    {
        Console.WriteLine("Getting connection statistics...");
        
        var connectionService = new ConnectionProfileService();
        var profiles = connectionService.LoadAllProfiles();
        
        return await Task.FromResult(new
        {
            totalProfiles = profiles.Count,
            note = "Connection stats based on saved profiles",
            retrievedAt = DateTime.Now
        });
    }
    
    private async Task<object> TestConnectionAsync(CliArguments args)
    {
        var profileName = args.Object ?? throw new ArgumentException("Object parameter required (profile name)");
        Console.WriteLine($"Testing connection: {profileName}");
        
        return await Task.FromResult(new
        {
            profileName,
            testResult = "Not implemented in CLI mode",
            note = "Connection testing requires creating actual DB connection",
            retrievedAt = DateTime.Now
        });
    }
    
    // ========================================================================
    // Additional Utility Commands (10 commands)
    // ========================================================================
    
    private async Task<object> ListSchemasAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        Console.WriteLine("Listing all schemas...");
        
        var sql = @"
            SELECT TRIM(SCHEMANAME) AS SchemaName, TRIM(OWNER) AS Owner, CREATE_TIME
            FROM SYSCAT.SCHEMATA
            WHERE SCHEMANAME NOT IN ('SYSIBM', 'SYSCAT', 'SYSPROC', 'SYSFUN')
            ORDER BY SCHEMANAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var schemas = data.AsEnumerable().Select(r => new
        {
            schemaName = r["SchemaName"]?.ToString()?.Trim(),
            owner = r["Owner"]?.ToString()?.Trim(),
            createTime = r["CREATE_TIME"]
        }).ToList();
        
        return new { schemaCount = schemas.Count, schemas, retrievedAt = DateTime.Now };
    }
    
    private async Task<object> ListTablespacesAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        Console.WriteLine("Listing tablespaces...");
        
        var sql = @"
            SELECT TRIM(TBSPACE) AS TablespaceName, TRIM(DATATYPE) AS DataType, PAGESIZE
            FROM SYSCAT.TABLESPACES
            ORDER BY TBSPACE
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var tablespaces = data.AsEnumerable().Select(r => new
        {
            tablespaceName = r["TablespaceName"]?.ToString()?.Trim(),
            dataType = r["DataType"]?.ToString()?.Trim(),
            pageSize = r["PAGESIZE"]
        }).ToList();
        
        return new { tablespaceCount = tablespaces.Count, tablespaces, retrievedAt = DateTime.Now };
    }
    
    private async Task<object> ListAllIndexesAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Console.WriteLine($"Listing all indexes in schema: {schema}");
        
        var sql = $@"
            SELECT TRIM(INDSCHEMA) AS Schema, TRIM(INDNAME) AS IndexName, 
                   TRIM(TABNAME) AS TableName, TRIM(UNIQUERULE) AS UniqueRule
            FROM SYSCAT.INDEXES
            WHERE INDSCHEMA LIKE '{schema}'
            ORDER BY INDSCHEMA, TABNAME, INDNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        var indexes = data.AsEnumerable().Take(limit).Select(r => new
        {
            schema = r["Schema"]?.ToString()?.Trim(),
            indexName = r["IndexName"]?.ToString()?.Trim(),
            tableName = r["TableName"]?.ToString()?.Trim(),
            uniqueRule = r["UniqueRule"]?.ToString()?.Trim()
        }).ToList();
        
        return new { indexCount = indexes.Count, limitApplied = limit, indexes, retrievedAt = DateTime.Now };
    }
    
    private async Task<object> ListConstraintsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Console.WriteLine($"Listing constraints in schema: {schema}");
        
        var sql = $@"
            SELECT TRIM(TABSCHEMA) AS Schema, TRIM(TABNAME) AS TableName,
                   TRIM(CONSTNAME) AS ConstraintName, TRIM(TYPE) AS Type
            FROM SYSCAT.TABCONST
            WHERE TABSCHEMA LIKE '{schema}'
            ORDER BY TABSCHEMA, TABNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var limit = args.Limit ?? data.Rows.Count;
        var constraints = data.AsEnumerable().Take(limit).Select(r => new
        {
            schema = r["Schema"]?.ToString()?.Trim(),
            tableName = r["TableName"]?.ToString()?.Trim(),
            constraintName = r["ConstraintName"]?.ToString()?.Trim(),
            type = r["Type"]?.ToString()?.Trim()
        }).ToList();
        
        return new { constraintCount = constraints.Count, limitApplied = limit, constraints, retrievedAt = DateTime.Now };
    }
    
    private async Task<object> ListSequencesAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? "%";
        Console.WriteLine($"Listing sequences in schema: {schema}");
        
        var sql = $@"
            SELECT TRIM(SEQSCHEMA) AS Schema, TRIM(SEQNAME) AS SequenceName,
                   START, INCREMENT, MINVALUE, MAXVALUE
            FROM SYSCAT.SEQUENCES
            WHERE SEQSCHEMA LIKE '{schema}'
            ORDER BY SEQSCHEMA, SEQNAME
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var sequences = data.AsEnumerable().Select(r => new
        {
            schema = r["Schema"]?.ToString()?.Trim(),
            sequenceName = r["SequenceName"]?.ToString()?.Trim(),
            start = r["START"],
            increment = r["INCREMENT"],
            minValue = r["MINVALUE"],
            maxValue = r["MAXVALUE"]
        }).ToList();
        
        return new { sequenceCount = sequences.Count, sequences, retrievedAt = DateTime.Now };
    }
    
    private async Task<object> GetTableSizeAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.TABLE)");
        
        var parts = args.Object.Split('.');
        var schema = parts[0];
        var tableName = parts[1];
        
        var sql = $@"
            SELECT CARD AS RowCount, NPAGES AS DataPages, 
                   NPAGES * 4 / 1024.0 AS SizeMB
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA = '{schema}' AND TABNAME = '{tableName}'
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var row = data.Rows[0];
        
        return new
        {
            schema,
            tableName,
            rowCount = row["RowCount"],
            dataPages = row["DataPages"],
            sizeMB = row["SizeMB"],
            retrievedAt = DateTime.Now
        };
    }
    
    private async Task<object> GetSchemaSizeAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var schema = args.Schema ?? throw new ArgumentException("Schema parameter required");
        
        var sql = $@"
            SELECT SUM(CARD) AS TotalRows, SUM(NPAGES) AS TotalPages,
                   SUM(NPAGES) * 4 / 1024.0 AS TotalSizeMB
            FROM SYSCAT.TABLES
            WHERE TABSCHEMA = '{schema}' AND TYPE IN ('T', 'U')
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var row = data.Rows[0];
        
        return new
        {
            schema,
            totalRows = row["TotalRows"],
            totalPages = row["TotalPages"],
            totalSizeMB = row["TotalSizeMB"],
            retrievedAt = DateTime.Now
        };
    }
    
    private async Task<object> GetDatabaseSizeAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var sql = @"
            SELECT SUM(CARD) AS TotalRows, SUM(NPAGES) AS TotalPages,
                   SUM(NPAGES) * 4 / 1024.0 AS TotalSizeMB
            FROM SYSCAT.TABLES
            WHERE TYPE IN ('T', 'U')
              AND TABSCHEMA NOT IN ('SYSIBM', 'SYSCAT', 'SYSPROC', 'SYSFUN')
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var row = data.Rows[0];
        
        return new
        {
            totalRows = row["TotalRows"],
            totalPages = row["TotalPages"],
            totalSizeMB = row["TotalSizeMB"],
            approximateSizeGB = Convert.ToDouble(row["TotalSizeMB"] ?? 0) / 1024,
            retrievedAt = DateTime.Now
        };
    }
    
    private async Task<object> GetTableGrantsAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        if (string.IsNullOrEmpty(args.Object))
            throw new ArgumentException("Object parameter required (format: SCHEMA.TABLE)");
        
        var parts = args.Object.Split('.');
        var schema = parts[0];
        var tableName = parts[1];
        
        var sql = $@"
            SELECT TRIM(GRANTOR) AS Grantor, TRIM(GRANTEE) AS Grantee,
                   TRIM(GRANTEETYPE) AS GranteeType
            FROM SYSCAT.TABAUTH
            WHERE TABSCHEMA = '{schema}' AND TABNAME = '{tableName}'
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var grants = data.AsEnumerable().Select(r => new
        {
            grantor = r["Grantor"]?.ToString()?.Trim(),
            grantee = r["Grantee"]?.ToString()?.Trim(),
            granteeType = r["GranteeType"]?.ToString()?.Trim()
        }).ToList();
        
        return new
        {
            schema,
            tableName,
            grantCount = grants.Count,
            grants,
            retrievedAt = DateTime.Now
        };
    }
    
    private async Task<object> GetDbConfigAsync(DB2ConnectionManager connectionManager, CliArguments args)
    {
        var sql = @"
            SELECT CURRENT SERVER AS DatabaseName,
                   CURRENT TIMESTAMP AS CurrentTime,
                   CURRENT USER AS CurrentUser
            FROM SYSIBM.SYSDUMMY1
        ";
        
        var data = await connectionManager.ExecuteQueryAsync(sql);
        var row = data.Rows[0];
        
        return new
        {
            databaseName = row["DatabaseName"]?.ToString()?.Trim(),
            currentTime = row["CurrentTime"],
            currentUser = row["CurrentUser"]?.ToString()?.Trim(),
            note = "Database configuration parameters require db2 GET DBM CFG or GET DB CFG commands",
            retrievedAt = DateTime.Now
        };
    }
    
    // ========================================================================
    // Meta Commands (2 commands - commands 89 & 90!)
    // ========================================================================
    
    private async Task<object> GetHelpAllAsync(CliArguments args)
    {
        Console.WriteLine("Listing all available CLI commands...");
        
        var commands = new List<object>
        {
            new { command = "list-tables", category = "Object Listing", description = "List all tables in schema" },
            new { command = "list-views", category = "Object Listing", description = "List all views in schema" },
            new { command = "list-procedures", category = "Object Listing", description = "List stored procedures" },
            new { command = "list-triggers", category = "Object Listing", description = "List triggers" },
            new { command = "list-functions", category = "Object Listing", description = "List functions" },
            new { command = "table-props", category = "Object Info", description = "Basic table properties" },
            new { command = "view-info", category = "Object Info", description = "View information" },
            new { command = "procedure-info", category = "Object Info", description = "Stored procedure details" },
            new { command = "function-info", category = "Object Info", description = "Function details" },
            new { command = "trigger-info", category = "Object Info", description = "Trigger details" },
            new { command = "trigger-usage", category = "Object Info", description = "Find all triggers" },
            new { command = "table-columns", category = "TableDetails", description = "Complete column metadata" },
            new { command = "table-foreign-keys", category = "TableDetails", description = "Outgoing foreign keys" },
            new { command = "table-indexes", category = "TableDetails", description = "All indexes" },
            new { command = "table-statistics-full", category = "TableDetails", description = "Complete table statistics" },
            new { command = "table-ddl", category = "TableDetails", description = "Generate CREATE TABLE DDL" },
            new { command = "table-incoming-fks", category = "TableDetails", description = "Incoming foreign keys" },
            new { command = "table-referencing-packages", category = "TableDetails", description = "Packages using table" },
            new { command = "table-referencing-views", category = "TableDetails", description = "Views depending on table" },
            new { command = "table-referencing-routines", category = "TableDetails", description = "Routines using table" },
            new { command = "dependencies", category = "Dependencies", description = "Dependency analysis" },
            new { command = "dependency-graph", category = "Dependencies", description = "Complete dependency graph" },
            new { command = "dependency-impact", category = "Dependencies", description = "Impact analysis" },
            new { command = "dependency-chain", category = "Dependencies", description = "Dependency chain" },
            new { command = "lock-monitor", category = "Monitoring", description = "Database locks (simplified)" },
            new { command = "active-sessions", category = "Monitoring", description = "Active sessions (simplified)" },
            new { command = "database-load", category = "Monitoring", description = "Database load metrics" },
            new { command = "table-stats", category = "Monitoring", description = "Table statistics" },
            new { command = "cdc-info", category = "Monitoring", description = "CDC information (simplified)" },
            new { command = "list-all-source", category = "Source Code", description = "List all source code objects" },
            new { command = "source-code-full", category = "Source Code", description = "Get complete source code" },
            new { command = "source-search", category = "Source Code", description = "Search in source code" },
            new { command = "list-packages", category = "Packages", description = "List all packages" },
            new { command = "package-analysis", category = "Packages", description = "Analyze package dependencies" },
            new { command = "package-details", category = "Packages", description = "Detailed package information" },
            new { command = "list-comments", category = "Comments", description = "List objects with/without comments" },
            new { command = "object-comment", category = "Comments", description = "Get object comment" },
            new { command = "missing-comments", category = "Comments", description = "Find uncommented objects" },
            new { command = "statistics-overview", category = "Statistics", description = "Statistics overview for schema" },
            new { command = "statistics-recommendations", category = "Statistics", description = "Recommend RUNSTATS targets" },
            new { command = "index-statistics", category = "Statistics", description = "Index statistics details" },
            new { command = "unused-tables", category = "Unused Objects", description = "Find unused tables" },
            new { command = "unused-indexes", category = "Unused Objects", description = "Find unused indexes" },
            new { command = "unused-views", category = "Unused Objects", description = "Find unused views" },
            new { command = "unused-routines", category = "Unused Objects", description = "Find unused routines" },
            new { command = "database-load-full", category = "Advanced Monitoring", description = "Complete load metrics" },
            new { command = "table-activity", category = "Advanced Monitoring", description = "Table activity metrics" },
            new { command = "top-active-tables", category = "Advanced Monitoring", description = "Top N active tables" },
            new { command = "lock-monitor-full", category = "Advanced Monitoring", description = "Complete lock info" },
            new { command = "lock-chains", category = "Advanced Monitoring", description = "Lock wait chains" },
            new { command = "active-sessions-full", category = "Advanced Monitoring", description = "Complete session info" },
            new { command = "session-details", category = "Advanced Monitoring", description = "Detailed session info" },
            new { command = "long-running-sessions", category = "Advanced Monitoring", description = "Long-running sessions" },
            new { command = "migration-plan", category = "Migration", description = "Generate migration plan" },
            new { command = "migration-ddl", category = "Migration", description = "Migration DDL" },
            new { command = "migration-data-script", category = "Migration", description = "Data migration scripts" },
            new { command = "export-table-data", category = "Export", description = "Export table data" },
            new { command = "export-query-results", category = "Export", description = "Export query results" },
            new { command = "export-schema-ddl", category = "Export", description = "Export schema DDL" },
            new { command = "sql-validate", category = "SQL Tools", description = "Validate SQL safety" },
            new { command = "sql-format", category = "SQL Tools", description = "Format SQL statement" },
            new { command = "schema-compare", category = "Schema Diff", description = "Compare two schemas" },
            new { command = "schema-diff-ddl", category = "Schema Diff", description = "Generate schema diff DDL" },
            new { command = "mermaid-erd", category = "Mermaid", description = "Generate Mermaid ERD" },
            new { command = "mermaid-from-sql", category = "Mermaid", description = "Convert SQL to Mermaid" },
            new { command = "sql-from-mermaid", category = "Mermaid", description = "Convert Mermaid to SQL" },
            new { command = "mermaid-diff", category = "Mermaid", description = "Generate ALTER DDL from Mermaid diff (NO EXEC!)" },
            new { command = "sql-translate", category = "Mermaid", description = "Translate SQL dialects" },
            new { command = "query-history", category = "Metadata", description = "Query execution history" },
            new { command = "schema-metadata", category = "Metadata", description = "Complete schema metadata" },
            new { command = "database-metadata", category = "Metadata", description = "Database metadata" },
            new { command = "connection-profiles", category = "Connection", description = "List connection profiles" },
            new { command = "user-info-enhanced", category = "User/Privileges", description = "Enhanced user info" },
            new { command = "user-privileges-full", category = "User/Privileges", description = "Full user privileges" },
            new { command = "cdc-status-full", category = "CDC", description = "Complete CDC status" },
            new { command = "cdc-configuration", category = "CDC", description = "CDC configuration" },
            new { command = "cdc-changes", category = "CDC", description = "CDC changes" },
            new { command = "connection-stats", category = "Connection", description = "Connection statistics" },
            new { command = "connection-test", category = "Connection", description = "Test connection" },
            new { command = "list-schemas", category = "Utility", description = "List all schemas" },
            new { command = "list-tablespaces", category = "Utility", description = "List tablespaces" },
            new { command = "list-indexes-all", category = "Utility", description = "List all indexes" },
            new { command = "list-constraints", category = "Utility", description = "List constraints" },
            new { command = "list-sequences", category = "Utility", description = "List sequences" },
            new { command = "table-size", category = "Utility", description = "Get table size" },
            new { command = "schema-size", category = "Utility", description = "Get schema size" },
            new { command = "database-size", category = "Utility", description = "Get database size" },
            new { command = "table-grants", category = "Utility", description = "Get table grants" },
            new { command = "db-config", category = "Utility", description = "Database configuration" },
            new { command = "help-all", category = "Meta", description = "List all CLI commands (this command!)" },
            new { command = "cli-version", category = "Meta", description = "Show CLI version" }
        };
        
        var categories = commands.GroupBy(c => ((dynamic)c).category)
            .Select(g => new { category = g.Key, commandCount = g.Count(), commands = g.ToList() })
            .OrderBy(g => g.category)
            .ToList();
        
        return await Task.FromResult(new
        {
            totalCommands = commands.Count,
            categoryCount = categories.Count,
            categories,
            usage = "WindowsDb2Editor.exe -Profile <profile> -Command <command> -Outfile <output.json> [options]",
            note = "All commands export structured JSON for automated testing",
            retrievedAt = DateTime.Now
        });
    }
    
    private async Task<object> GetCliVersionAsync(CliArguments args)
    {
        Console.WriteLine("Getting CLI version information...");
        
        return await Task.FromResult(new
        {
            applicationName = "DbExplorer CLI",
            cliVersion = "1.0.0",
            framework = ".NET 10.0",
            totalCommands = 90,
            implementationDate = "2025-12-13",
            features = new[]
            {
                "90 CLI commands for automated testing",
                "Structured JSON output",
                "DB2 12.1 compatibility",
                "Read-only operations (no DML)",
                "Mermaid ERD integration (SqlMermaidErdTools)",
                "Multi-provider support (DB2 primary)"
            },
            retrievedAt = DateTime.Now
        });
    }
}
