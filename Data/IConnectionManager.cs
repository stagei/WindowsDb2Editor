using System.Data;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Data;

/// <summary>
/// Database-agnostic interface for connection management.
/// Implementations exist for DB2, PostgreSQL, SQL Server, etc.
/// </summary>
public interface IConnectionManager : IDisposable
{
    /// <summary>Gets the connection information used to create this manager</summary>
    IConnectionInfo ConnectionInfo { get; }
    
    /// <summary>Whether the connection is currently open</summary>
    bool IsConnected { get; }
    
    /// <summary>Test if the database is reachable</summary>
    Task<bool> TestConnectionAsync();
    
    /// <summary>Open the database connection</summary>
    Task OpenAsync(CancellationToken cancellationToken = default);
    
    /// <summary>Close the database connection</summary>
    Task CloseAsync();
    
    /// <summary>Reconnect to the database</summary>
    Task<bool> ReconnectAsync();
    
    /// <summary>Ensure connection is open, reconnecting if needed</summary>
    Task<bool> EnsureConnectedAsync();
    
    /// <summary>Execute a query and return results as DataTable</summary>
    Task<DataTable> ExecuteQueryAsync(string sql, int? maxRows = null, int offset = 0, bool handleDecimalErrors = true);
    
    /// <summary>Execute a query and return a single scalar value</summary>
    Task<object?> ExecuteScalarAsync(string sql);
    
    /// <summary>Execute a non-query command (INSERT, UPDATE, DELETE, DDL)</summary>
    Task<int> ExecuteNonQueryAsync(string sql);
    
    /// <summary>Begin a transaction</summary>
    Task BeginTransactionAsync();
    
    /// <summary>Commit the current transaction</summary>
    Task CommitAsync();
    
    /// <summary>Rollback the current transaction</summary>
    Task RollbackAsync();
    
    /// <summary>Cancel any running query</summary>
    void CancelQuery();
}
