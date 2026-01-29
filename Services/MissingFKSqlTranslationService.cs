using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;
using WindowsDb2Editor.Data;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Service for retrieving SQL statements for Missing FK Discovery.
/// Delegates to MetadataHandler which loads provider-specific SQL from config files
/// (e.g., db2_12.1_sql_statements.json, postgresql_X.X_sql_statements.json).
/// </summary>
public class MissingFKSqlTranslationService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly MetadataHandler _metadataHandler;
    private readonly Dictionary<string, string> _cache = new();
    
    public MissingFKSqlTranslationService(MetadataHandler? metadataHandler = null)
    {
        _metadataHandler = metadataHandler ?? new MetadataHandler();
        Logger.Debug("MissingFKSqlTranslationService initialized");
    }
    
    // Legacy constructor for backward compatibility
    public MissingFKSqlTranslationService(SqlMermaidIntegrationService? sqlMermaidService, MetadataHandler? metadataHandler = null)
        : this(metadataHandler)
    {
        // sqlMermaidService no longer needed - MetadataHandler provides provider-specific SQL
    }
    
    /// <summary>
    /// Get provider-specific SQL statement from MetadataHandler.
    /// All provider-specific SQL is centralized in {provider}_{version}_sql_statements.json files.
    /// </summary>
    /// <param name="connectionManager">Connection manager to determine provider</param>
    /// <param name="statementName">Name of the SQL statement to retrieve</param>
    /// <returns>Provider-specific SQL statement</returns>
    public Task<string> GetTranslatedStatementAsync(IConnectionManager connectionManager, string statementName)
    {
        var provider = connectionManager.ConnectionInfo.ProviderType?.ToLowerInvariant() ?? "db2";
        var version = "12.1"; // Currently only DB2 12.1 is supported; extend when adding more providers
        var cacheKey = $"{provider}|{version}|{statementName}";
        
        Logger.Debug("Getting SQL statement: {Statement} for provider: {Provider}", statementName, provider);
        
        // Check cache first
        if (_cache.TryGetValue(cacheKey, out var cachedSql))
        {
            Logger.Debug("Returning cached SQL for {Statement}", statementName);
            return Task.FromResult(cachedSql);
        }
        
        // Use MetadataHandler to load from provider-specific config file
        try
        {
            var sql = _metadataHandler.GetStatement(statementName, provider, version);
            Logger.Debug("Loaded SQL for {Statement} via MetadataHandler ({Length} chars)", statementName, sql.Length);
            _cache[cacheKey] = sql;
            return Task.FromResult(sql);
        }
        catch (KeyNotFoundException ex)
        {
            Logger.Error(ex, "SQL statement {Statement} not found for provider {Provider}", statementName, provider);
            throw;
        }
    }
    
    /// <summary>
    /// Clear cache (useful for testing or when SQL statements are updated).
    /// </summary>
    public void ClearCache()
    {
        _cache.Clear();
        Logger.Debug("Cache cleared");
    }
}
