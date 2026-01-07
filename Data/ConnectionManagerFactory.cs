using WindowsDb2Editor.Models;

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
            "POSTGRESQL" or "POSTGRES" => throw new NotImplementedException("PostgreSQL connection manager not yet implemented"),
            "SQLSERVER" or "MSSQL" => throw new NotImplementedException("SQL Server connection manager not yet implemented"),
            "ORACLE" => throw new NotImplementedException("Oracle connection manager not yet implemented"),
            "MYSQL" => throw new NotImplementedException("MySQL connection manager not yet implemented"),
            _ => new DB2ConnectionManager(connection) // Default to DB2
        };
    }
}
