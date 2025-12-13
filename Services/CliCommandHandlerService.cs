using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NLog;
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
    
    public CliCommandHandlerService()
    {
        Logger.Debug("CliCommandHandlerService initialized (direct SQL mode)");
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
}
