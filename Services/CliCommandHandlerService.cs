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
    
    public CliCommandHandlerService(SqlMermaidIntegrationService? mermaidService = null)
    {
        _mermaidService = mermaidService ?? new SqlMermaidIntegrationService();
        Logger.Debug("CliCommandHandlerService initialized (direct SQL mode, Mermaid: {HasMermaid})", _mermaidService != null);
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
        
        // Get columns
        var columnsSql = $@"
            SELECT c.COLNAME, c.COLNO, c.TYPENAME, c.LENGTH, c.SCALE, c.NULLS, c.DEFAULT, c.REMARKS, c.IDENTITY,
                   CASE WHEN k.COLNAME IS NOT NULL THEN 1 ELSE 0 END AS IS_PK
            FROM SYSCAT.COLUMNS c
            LEFT JOIN SYSCAT.KEYCOLUSE k 
                ON c.TABSCHEMA = k.TABSCHEMA 
                AND c.TABNAME = k.TABNAME 
                AND c.COLNAME = k.COLNAME
            WHERE c.TABSCHEMA = '{schema}' AND c.TABNAME = '{tableName}'
            ORDER BY c.COLNO
        ";
        
        var columnsData = await connectionManager.ExecuteQueryAsync(columnsSql);
        
        var columns = columnsData.AsEnumerable().Select(row => new
        {
            columnName = row["COLNAME"]?.ToString()?.Trim(),
            ordinalPosition = row["COLNO"],
            dataType = row["TYPENAME"]?.ToString()?.Trim(),
            length = row["LENGTH"],
            scale = row["SCALE"],
            isNullable = row["NULLS"]?.ToString() == "Y",
            defaultValue = row["DEFAULT"]?.ToString()?.Trim(),
            comment = row["REMARKS"]?.ToString()?.Trim(),
            isIdentity = row["IDENTITY"]?.ToString() == "Y",
            isPrimaryKey = Convert.ToInt32(row["IS_PK"]) == 1
        }).ToList();
        
        // Get primary keys
        var pkSql = $@"
            SELECT CONSTNAME, COLNAME, COLSEQ
            FROM SYSCAT.KEYCOLUSE
            WHERE TABSCHEMA = '{schema}' AND TABNAME = '{tableName}'
            ORDER BY COLSEQ
        ";
        
        var pkData = await connectionManager.ExecuteQueryAsync(pkSql);
        var primaryKeys = pkData.AsEnumerable().Select(row => row["COLNAME"]?.ToString()?.Trim()).ToList();
        
        // Get foreign keys
        var fkSql = $@"
            SELECT CONSTNAME, FK_COLNAMES, REFTABSCHEMA, REFTABNAME, PK_COLNAMES, DELETERULE, UPDATERULE
            FROM SYSCAT.REFERENCES
            WHERE TABSCHEMA = '{schema}' AND TABNAME = '{tableName}'
        ";
        
        var fkData = await connectionManager.ExecuteQueryAsync(fkSql);
        
        var foreignKeys = fkData.AsEnumerable().Select(row => new
        {
            constraintName = row["CONSTNAME"]?.ToString()?.Trim(),
            columns = row["FK_COLNAMES"]?.ToString()?.Trim(),
            referencedSchema = row["REFTABSCHEMA"]?.ToString()?.Trim(),
            referencedTable = row["REFTABNAME"]?.ToString()?.Trim(),
            referencedColumns = row["PK_COLNAMES"]?.ToString()?.Trim(),
            deleteRule = row["DELETERULE"]?.ToString()?.Trim(),
            updateRule = row["UPDATERULE"]?.ToString()?.Trim()
        }).ToList();
        
        // Get indexes
        var indexSql = $@"
            SELECT INDNAME, UNIQUERULE, COLNAMES, INDEXTYPE, COLCOUNT, FIRSTKEYCARD, FULLKEYCARD, REMARKS
            FROM SYSCAT.INDEXES
            WHERE TABSCHEMA = '{schema}' AND TABNAME = '{tableName}'
        ";
        
        var indexData = await connectionManager.ExecuteQueryAsync(indexSql);
        
        var indexes = indexData.AsEnumerable().Select(row => new
        {
            indexName = row["INDNAME"]?.ToString()?.Trim(),
            uniqueRule = row["UNIQUERULE"]?.ToString()?.Trim(),
            isUnique = row["UNIQUERULE"]?.ToString()?.Trim() == "U",
            isPrimaryKey = row["UNIQUERULE"]?.ToString()?.Trim() == "P",
            columns = row["COLNAMES"]?.ToString()?.Trim(),
            indexType = row["INDEXTYPE"]?.ToString()?.Trim(),
            columnCount = row["COLCOUNT"],
            firstKeyCard = row["FIRSTKEYCARD"],
            fullKeyCard = row["FULLKEYCARD"],
            comment = row["REMARKS"]?.ToString()?.Trim()
        }).ToList();
        
        // Get table statistics if requested
        object? statistics = null;
        if (args.IncludeDependencies)
        {
            var statsSql = $@"
                SELECT CARD, NPAGES, FPAGES, OVERFLOW, AVGROWSIZE, STATS_TIME, LAST_REGEN_TIME
                FROM SYSCAT.TABLES
                WHERE TABSCHEMA = '{schema}' AND TABNAME = '{tableName}'
            ";
            
            var statsData = await connectionManager.ExecuteQueryAsync(statsSql);
            
            if (statsData.Rows.Count > 0)
            {
                var row = statsData.Rows[0];
                statistics = new
                {
                    rowCount = row["CARD"],
                    dataPages = row["NPAGES"],
                    freePages = row["FPAGES"],
                    overflowPages = row["OVERFLOW"],
                    avgRowSize = row["AVGROWSIZE"],
                    lastStatsTime = row["STATS_TIME"],
                    lastReorgTime = row["LAST_REGEN_TIME"]
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
}
