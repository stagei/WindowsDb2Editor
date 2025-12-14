using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Manages multiple database connections simultaneously for comparison operations.
/// </summary>
public class MultiDatabaseConnectionManager : IDisposable
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    private readonly Dictionary<string, DB2ConnectionManager> _connections = new();
    private bool _disposed = false;
    
    /// <summary>
    /// Add a connection with an alias.
    /// </summary>
    public async Task<bool> AddConnectionAsync(string alias, DB2Connection connectionInfo)
    {
        Logger.Debug("Adding connection: {Alias}", alias);
        
        try
        {
            var connectionManager = new DB2ConnectionManager(connectionInfo);
            await connectionManager.OpenAsync();
            
            _connections[alias] = connectionManager;
            Logger.Info("Connection {Alias} added successfully", alias);
            return true;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to add connection: {Alias}", alias);
            return false;
        }
    }
    
    /// <summary>
    /// Add an already-established connection.
    /// </summary>
    public void AddExistingConnection(string alias, DB2ConnectionManager connectionManager)
    {
        Logger.Debug("Adding existing connection: {Alias}", alias);
        _connections[alias] = connectionManager;
        Logger.Info("Existing connection {Alias} added", alias);
    }
    
    /// <summary>
    /// Get a connection by alias.
    /// </summary>
    public DB2ConnectionManager? GetConnection(string alias)
    {
        return _connections.TryGetValue(alias, out var connection) ? connection : null;
    }
    
    /// <summary>
    /// Get all connection aliases.
    /// </summary>
    public List<string> GetAllAliases()
    {
        return _connections.Keys.ToList();
    }
    
    /// <summary>
    /// Test all connections to ensure they're alive.
    /// </summary>
    public async Task<Dictionary<string, bool>> TestAllConnectionsAsync()
    {
        Logger.Debug("Testing all connections");
        
        var results = new Dictionary<string, bool>();
        
        foreach (var kvp in _connections)
        {
            try
            {
                var isAlive = await kvp.Value.TestConnectionAsync();
                results[kvp.Key] = isAlive;
                Logger.Debug("Connection {Alias}: {Status}", kvp.Key, isAlive ? "Alive" : "Dead");
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "Failed to test connection: {Alias}", kvp.Key);
                results[kvp.Key] = false;
            }
        }
        
        return results;
    }
    
    /// <summary>
    /// Remove a connection.
    /// </summary>
    public void RemoveConnection(string alias)
    {
        if (_connections.Remove(alias))
        {
            Logger.Info("Connection {Alias} removed", alias);
        }
    }
    
    /// <summary>
    /// Get connection count.
    /// </summary>
    public int Count => _connections.Count;
    
    public void Dispose()
    {
        if (!_disposed)
        {
            Logger.Debug("Disposing MultiDatabaseConnectionManager");
            
            // Don't dispose connections as they may be used elsewhere
            // Just clear the references
            _connections.Clear();
            
            _disposed = true;
            Logger.Info("MultiDatabaseConnectionManager disposed");
        }
    }
}

