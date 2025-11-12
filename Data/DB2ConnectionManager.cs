using System.Data;
using IBM.Data.Db2;
using NLog;
using WindowsDb2Editor.Models;
using DB2Conn = IBM.Data.Db2.DB2Connection;

namespace WindowsDb2Editor.Data;

/// <summary>
/// Manager for DB2 database connections
/// Uses Net.IBM.Data.Db2 package for real DB2 connectivity
/// </summary>
public class DB2ConnectionManager : IDisposable
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly Models.DB2Connection _connectionInfo;
    private DB2Conn? _db2Connection;
    private bool _disposed;

    public DB2ConnectionManager(Models.DB2Connection connectionInfo)
    {
        Logger.Debug("DB2ConnectionManager initializing with real DB2 driver");
        _connectionInfo = connectionInfo ?? throw new ArgumentNullException(nameof(connectionInfo));
        Logger.Debug($"Connection: {_connectionInfo.GetConnectionString(maskPassword: true)}");
    }

    /// <summary>
    /// Test database connection
    /// </summary>
    public async Task<bool> TestConnectionAsync()
    {
        Logger.Info("Testing DB2 connection");
        Logger.Debug($"Server: {_connectionInfo.Server}, Database: {_connectionInfo.Database}");

        try
        {
            using var testConnection = new DB2Conn(_connectionInfo.GetConnectionString());
            await testConnection.OpenAsync();
            
            Logger.Info("Connection test successful");
            return true;
        }
        catch (DB2Exception db2Ex)
        {
            Logger.Error(db2Ex, "DB2 connection test failed - SQL State: {SqlState}, Error Code: {ErrorCode}", 
                db2Ex.SqlState, db2Ex.ErrorCode);
            return false;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Connection test failed");
            return false;
        }
    }

    /// <summary>
    /// Open database connection
    /// </summary>
    public async Task OpenAsync()
    {
        if (_db2Connection != null && _db2Connection.State == ConnectionState.Open)
        {
            Logger.Debug("Connection already open");
            return;
        }

        Logger.Info("Opening DB2 connection");
        Logger.Debug($"Connection string: {_connectionInfo.GetConnectionString(maskPassword: true)}");

        try
        {
            _db2Connection = new DB2Conn(_connectionInfo.GetConnectionString());
            await _db2Connection.OpenAsync();
            
            Logger.Info($"DB2 connection opened successfully - State: {_db2Connection.State}");
            Logger.Debug($"DB2 Server Version: {_db2Connection.ServerVersion}");
        }
        catch (DB2Exception db2Ex)
        {
            Logger.Error(db2Ex, "Failed to open DB2 connection - SQL State: {SqlState}, Error Code: {ErrorCode}", 
                db2Ex.SqlState, db2Ex.ErrorCode);
            throw new Exception($"Failed to connect to database: {db2Ex.Message}", db2Ex);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to open DB2 connection");
            throw new Exception($"Failed to connect to database: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Execute SQL query and return DataTable
    /// </summary>
    public async Task<DataTable> ExecuteQueryAsync(string sql, int? maxRows = null, int offset = 0, bool handleDecimalErrors = true)
    {
        if (_db2Connection == null || _db2Connection.State != ConnectionState.Open)
        {
            Logger.Warn("Attempting to execute query without connection");
            throw new InvalidOperationException("Connection is not open");
        }

        Logger.Info("Executing SQL query");
        Logger.Debug($"SQL: {sql.Substring(0, Math.Min(100, sql.Length))}... MaxRows: {maxRows}, Offset: {offset}");

        try
        {
            var startTime = DateTime.Now;

            // Modify SQL to include pagination if maxRows is specified
            var paginatedSql = sql;
            if (maxRows.HasValue)
            {
                // Add FETCH FIRST syntax for DB2 pagination
                paginatedSql = AddPaginationToQuery(sql, maxRows.Value, offset);
                Logger.Debug($"Paginated SQL: {paginatedSql.Substring(0, Math.Min(150, paginatedSql.Length))}...");
            }

            using var command = _db2Connection.CreateCommand();
            command.CommandText = paginatedSql;
            command.CommandTimeout = _connectionInfo.ConnectionTimeout;

            using var adapter = new DB2DataAdapter(command);
            var dataTable = new DataTable("Results");
            
            try
            {
                await Task.Run(() => adapter.Fill(dataTable));
            }
            catch (FormatException fex) when (handleDecimalErrors && fex.Message.Contains("was not in a correct format"))
            {
                Logger.Warn(fex, "DB2Decimal format error detected, attempting to read with DataReader");
                
                // Try alternative method using DataReader with manual conversion
                return await ExecuteQueryWithDataReaderAsync(paginatedSql);
            }

            var elapsed = (DateTime.Now - startTime).TotalMilliseconds;
            Logger.Info($"Query executed successfully in {elapsed}ms, {dataTable.Rows.Count} rows returned");

            return dataTable;
        }
        catch (DB2Exception db2Ex)
        {
            Logger.Error(db2Ex, "DB2 query execution failed - SQL State: {SqlState}, Error Code: {ErrorCode}", 
                db2Ex.SqlState, db2Ex.ErrorCode);
            throw new Exception($"Query failed: {db2Ex.Message}", db2Ex);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Query execution failed");
            throw new Exception($"Query failed: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Add pagination clauses to SQL query for DB2
    /// </summary>
    private string AddPaginationToQuery(string sql, int maxRows, int offset)
    {
        Logger.Debug($"Adding pagination: LIMIT {maxRows} OFFSET {offset}");
        
        // Check if query already has FETCH FIRST or similar clauses
        var upperSql = sql.Trim().ToUpperInvariant();
        
        if (upperSql.Contains("FETCH FIRST") || upperSql.Contains("LIMIT"))
        {
            Logger.Warn("Query already contains pagination clause, not modifying");
            return sql;
        }

        // For DB2, use OFFSET/FETCH NEXT syntax (DB2 11.1+)
        var paginatedSql = $"{sql.TrimEnd().TrimEnd(';')} OFFSET {offset} ROWS FETCH NEXT {maxRows} ROWS ONLY";
        
        return paginatedSql;
    }

    /// <summary>
    /// Execute query using DataReader with graceful decimal handling
    /// </summary>
    private async Task<DataTable> ExecuteQueryWithDataReaderAsync(string sql)
    {
        Logger.Debug("Executing query with DataReader for decimal error handling");
        
        using var command = _db2Connection!.CreateCommand();
        command.CommandText = sql;
        command.CommandTimeout = _connectionInfo.ConnectionTimeout;

        var dataTable = new DataTable("Results");
        
        await using var reader = await command.ExecuteReaderAsync();
        
        // Create columns
        for (int i = 0; i < reader.FieldCount; i++)
        {
            var columnName = reader.GetName(i);
            var columnType = reader.GetFieldType(i);
            dataTable.Columns.Add(columnName, typeof(string)); // Use string to avoid conversion errors
            Logger.Debug($"Column {i}: {columnName} ({columnType.Name})");
        }

        // Read rows
        int rowCount = 0;
        while (await reader.ReadAsync())
        {
            var row = dataTable.NewRow();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                try
                {
                    if (reader.IsDBNull(i))
                    {
                        row[i] = DBNull.Value;
                    }
                    else
                    {
                        // Get value as string to avoid DB2Decimal conversion issues
                        var value = reader.GetValue(i);
                        row[i] = value?.ToString() ?? string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warn(ex, $"Error reading column {i} ({reader.GetName(i)}), setting to error message");
                    row[i] = $"[ERROR: {ex.Message}]";
                }
            }
            dataTable.Rows.Add(row);
            rowCount++;
        }

        Logger.Info($"DataReader method returned {rowCount} rows with graceful error handling");
        return dataTable;
    }

    /// <summary>
    /// Execute SQL command (INSERT, UPDATE, DELETE)
    /// </summary>
    public async Task<int> ExecuteNonQueryAsync(string sql)
    {
        if (_db2Connection == null || _db2Connection.State != ConnectionState.Open)
        {
            Logger.Warn("Attempting to execute command without connection");
            throw new InvalidOperationException("Connection is not open");
        }

        Logger.Info("Executing SQL command");
        Logger.Debug($"SQL: {sql.Substring(0, Math.Min(100, sql.Length))}...");

        try
        {
            var startTime = DateTime.Now;

            using var command = _db2Connection.CreateCommand();
            command.CommandText = sql;
            command.CommandTimeout = _connectionInfo.ConnectionTimeout;

            var affectedRows = await command.ExecuteNonQueryAsync();

            var elapsed = (DateTime.Now - startTime).TotalMilliseconds;
            Logger.Info($"Command executed successfully in {elapsed}ms, {affectedRows} rows affected");

            return affectedRows;
        }
        catch (DB2Exception db2Ex)
        {
            Logger.Error(db2Ex, "DB2 command execution failed - SQL State: {SqlState}, Error Code: {ErrorCode}", 
                db2Ex.SqlState, db2Ex.ErrorCode);
            throw new Exception($"Command failed: {db2Ex.Message}", db2Ex);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Command execution failed");
            throw new Exception($"Command failed: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Close database connection
    /// </summary>
    public void Close()
    {
        if (_db2Connection == null)
        {
            return;
        }

        Logger.Info("Closing DB2 connection");
        
        try
        {
            if (_db2Connection.State != ConnectionState.Closed)
            {
                _db2Connection.Close();
            }
            Logger.Info("DB2 connection closed successfully");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error closing connection");
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        Logger.Debug("Disposing DB2ConnectionManager");
        Close();
        _db2Connection?.Dispose();
        _disposed = true;
    }

    public bool IsConnected => _db2Connection != null && _db2Connection.State == ConnectionState.Open;

    /// <summary>
    /// Execute scalar query (return single value)
    /// </summary>
    public async Task<object?> ExecuteScalarAsync(string sql)
    {
        if (_db2Connection == null || _db2Connection.State != ConnectionState.Open)
        {
            Logger.Warn("Attempting to execute scalar without connection");
            throw new InvalidOperationException("Connection is not open");
        }

        Logger.Info("Executing scalar query");
        Logger.Debug($"SQL: {sql.Substring(0, Math.Min(100, sql.Length))}...");

        try
        {
            var startTime = DateTime.Now;

            using var command = _db2Connection.CreateCommand();
            command.CommandText = sql;
            command.CommandTimeout = _connectionInfo.ConnectionTimeout;

            var result = await command.ExecuteScalarAsync();

            var elapsed = (DateTime.Now - startTime).TotalMilliseconds;
            Logger.Info($"Scalar query executed successfully in {elapsed}ms");

            return result;
        }
        catch (DB2Exception db2Ex)
        {
            Logger.Error(db2Ex, "DB2 scalar query failed - SQL State: {SqlState}, Error Code: {ErrorCode}", 
                db2Ex.SqlState, db2Ex.ErrorCode);
            throw new Exception($"Scalar query failed: {db2Ex.Message}", db2Ex);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Scalar query execution failed");
            throw new Exception($"Scalar query failed: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Get list of tables in current database
    /// </summary>
    public async Task<List<string>> GetTablesAsync(string? schema = null)
    {
        Logger.Info($"Retrieving tables from database{(schema != null ? $" (schema: {schema})" : "")}");

        var tables = new List<string>();

        try
        {
            var sql = schema != null
                ? $"SELECT TABNAME FROM SYSCAT.TABLES WHERE TABSCHEMA = '{schema}' AND TYPE = 'T' ORDER BY TABNAME"
                : "SELECT TABSCHEMA || '.' || TABNAME FROM SYSCAT.TABLES WHERE TYPE = 'T' ORDER BY TABSCHEMA, TABNAME";

            var dataTable = await ExecuteQueryAsync(sql);

            foreach (DataRow row in dataTable.Rows)
            {
                tables.Add(row[0].ToString() ?? string.Empty);
            }

            Logger.Info($"Retrieved {tables.Count} tables");
            return tables;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to retrieve tables");
            throw;
        }
    }

    /// <summary>
    /// Get columns for a specific table
    /// </summary>
    public async Task<DataTable> GetTableColumnsAsync(string tableName)
    {
        Logger.Info($"Retrieving columns for table: {tableName}");

        try
        {
            var sql = $@"SELECT COLNAME, TYPENAME, LENGTH, SCALE, NULLS, DEFAULT, REMARKS 
                        FROM SYSCAT.COLUMNS 
                        WHERE TABNAME = '{tableName}' 
                        ORDER BY COLNO";

            var dataTable = await ExecuteQueryAsync(sql);
            Logger.Info($"Retrieved {dataTable.Rows.Count} columns for {tableName}");
            return dataTable;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, $"Failed to retrieve columns for {tableName}");
            throw;
        }
    }

    /// <summary>
    /// Get list of schemas in current database
    /// </summary>
    public async Task<List<string>> GetSchemasAsync()
    {
        Logger.Info("Retrieving schemas from database");

        var schemas = new List<string>();

        try
        {
            var sql = "SELECT SCHEMANAME FROM SYSCAT.SCHEMATA ORDER BY SCHEMANAME";
            var dataTable = await ExecuteQueryAsync(sql);

            foreach (DataRow row in dataTable.Rows)
            {
                schemas.Add(row[0].ToString() ?? string.Empty);
            }

            Logger.Info($"Retrieved {schemas.Count} schemas");
            return schemas;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to retrieve schemas");
            throw;
        }
    }

    /// <summary>
    /// Get server version information
    /// </summary>
    public string GetServerVersion()
    {
        if (_db2Connection == null)
        {
            return "Not connected";
        }

        try
        {
            var version = _db2Connection.ServerVersion;
            Logger.Debug($"DB2 Server Version: {version}");
            return version;
        }
        catch (Exception ex)
        {
            Logger.Warn(ex, "Failed to get server version");
            return "Unknown";
        }
    }

    /// <summary>
    /// Get list of views in current database
    /// </summary>
    public async Task<List<string>> GetViewsAsync(string? schema = null)
    {
        Logger.Info($"Retrieving views from database{(schema != null ? $" (schema: {schema})" : "")}");

        var views = new List<string>();

        try
        {
            var sql = schema != null
                ? $"SELECT VIEWNAME FROM SYSCAT.VIEWS WHERE VIEWSCHEMA = '{schema}' ORDER BY VIEWNAME"
                : "SELECT VIEWSCHEMA || '.' || VIEWNAME FROM SYSCAT.VIEWS ORDER BY VIEWSCHEMA, VIEWNAME";

            var dataTable = await ExecuteQueryAsync(sql);

            foreach (DataRow row in dataTable.Rows)
            {
                views.Add(row[0].ToString() ?? string.Empty);
            }

            Logger.Info($"Retrieved {views.Count} views");
            return views;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to retrieve views");
            throw;
        }
    }

    /// <summary>
    /// Get list of stored procedures in current database
    /// </summary>
    public async Task<List<string>> GetStoredProceduresAsync(string? schema = null)
    {
        Logger.Info($"Retrieving stored procedures from database{(schema != null ? $" (schema: {schema})" : "")}");

        var procedures = new List<string>();

        try
        {
            var sql = schema != null
                ? $"SELECT PROCNAME FROM SYSCAT.PROCEDURES WHERE PROCSCHEMA = '{schema}' ORDER BY PROCNAME"
                : "SELECT PROCSCHEMA || '.' || PROCNAME FROM SYSCAT.PROCEDURES ORDER BY PROCSCHEMA, PROCNAME";

            var dataTable = await ExecuteQueryAsync(sql);

            foreach (DataRow row in dataTable.Rows)
            {
                procedures.Add(row[0].ToString() ?? string.Empty);
            }

            Logger.Info($"Retrieved {procedures.Count} stored procedures");
            return procedures;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to retrieve stored procedures");
            throw;
        }
    }

    /// <summary>
    /// Get view definition/DDL
    /// </summary>
    public async Task<string> GetViewDefinitionAsync(string viewName)
    {
        Logger.Info($"Retrieving view definition for: {viewName}");

        try
        {
            var sql = $"SELECT TEXT FROM SYSCAT.VIEWS WHERE VIEWNAME = '{viewName}'";
            var result = await ExecuteScalarAsync(sql);

            var definition = result?.ToString() ?? string.Empty;
            Logger.Debug($"Retrieved view definition: {definition.Length} characters");
            return definition;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, $"Failed to retrieve view definition for {viewName}");
            throw;
        }
    }
}

