using WindowsDb2Editor.Models;
using WindowsDb2Editor.Data.Providers.DB2;
using WindowsDb2Editor.Data.Providers.PostgreSQL;
using WindowsDb2Editor.Data.Providers.SQLite;

namespace WindowsDb2Editor.Data;

/// <summary>
/// Factory for creating connection managers based on provider type
/// </summary>
public static class ConnectionManagerFactory
{
    /// <summary>
    /// Create an appropriate connection manager based on the connection's provider type
    /// </summary>
    public static IConnectionManager CreateConnectionManager(DatabaseConnection connection)
    {
        var provider = connection.ProviderType?.ToUpperInvariant() ?? "DB2";
        
        return provider switch
        {
            "POSTGRESQL" or "POSTGRES" => new PostgreSQLConnectionManager(connection),
            "SQLITE" => new SQLiteConnectionManager(connection),
            "SQLSERVER" or "MSSQL" => throw new NotImplementedException("SQL Server connection manager not yet implemented"),
            "ORACLE" => throw new NotImplementedException("Oracle connection manager not yet implemented"),
            "MYSQL" => throw new NotImplementedException("MySQL connection manager not yet implemented"),
            _ => new DB2ConnectionManager(connection) // Default to DB2
        };
    }
}
