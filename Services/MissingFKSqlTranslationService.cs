using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using NLog;
using SqlMermaidErdTools.Models;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Service for translating ANSI SQL statements to provider-specific SQL dialects.
/// Ensures database-agnostic SQL storage until execution time.
/// </summary>
public class MissingFKSqlTranslationService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly SqlMermaidIntegrationService _sqlMermaidService;
    private readonly Dictionary<string, string> _translationCache = new(); // Cache key: "provider|statementName"
    private readonly string _configFilesPath;
    
    public MissingFKSqlTranslationService(SqlMermaidIntegrationService sqlMermaidService)
    {
        _sqlMermaidService = sqlMermaidService ?? throw new ArgumentNullException(nameof(sqlMermaidService));
        _configFilesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConfigFiles");
        Logger.Debug("MissingFKSqlTranslationService initialized");
    }
    
    /// <summary>
    /// Get provider-specific SQL statement.
    /// For DB2, loads from db2_missing_fk_sql_statements.json.
    /// For other providers, translates from ANSI SQL.
    /// </summary>
    /// <param name="connectionManager">Connection manager to determine provider</param>
    /// <param name="statementName">Name of the SQL statement to retrieve</param>
    /// <returns>Provider-specific SQL statement</returns>
    public async Task<string> GetTranslatedStatementAsync(IConnectionManager connectionManager, string statementName)
    {
        var provider = connectionManager.ConnectionInfo.ProviderType?.ToUpperInvariant() ?? "DB2";
        var cacheKey = $"{provider}|{statementName}";
        
        Logger.Debug("Getting SQL statement: {Statement} for provider: {Provider}", statementName, provider);
        
        // Check cache first
        if (_translationCache.TryGetValue(cacheKey, out var cachedSql))
        {
            Logger.Debug("Returning cached SQL for {Statement}", statementName);
            return cachedSql;
        }
        
        // For DB2, load from provider-specific file directly
        if (provider == "DB2")
        {
            var db2Sql = LoadProviderSqlStatement("db2", statementName);
            if (!string.IsNullOrEmpty(db2Sql))
            {
                Logger.Debug("Loaded DB2-specific SQL for {Statement} ({Length} chars)", statementName, db2Sql.Length);
                _translationCache[cacheKey] = db2Sql;
                return db2Sql;
            }
            Logger.Warn("DB2-specific SQL not found for {Statement}, falling back to ANSI", statementName);
        }
        
        // Load ANSI SQL from JSON file
        var ansiSql = LoadAnsiSqlStatement(statementName);
        if (string.IsNullOrEmpty(ansiSql))
        {
            throw new KeyNotFoundException($"SQL statement '{statementName}' not found in missing_fk_sql_statements.json");
        }
        
        Logger.Debug("Loaded ANSI SQL for {Statement} ({Length} chars)", statementName, ansiSql.Length);
        
        // Map provider to SqlDialect enum
        var targetDialect = MapProviderToSqlDialect(provider);
        
        // If provider is ANSI-compatible, return ANSI SQL directly
        if (targetDialect == SqlDialect.AnsiSql)
        {
            Logger.Debug("Provider {Provider} uses ANSI SQL directly", provider);
            _translationCache[cacheKey] = ansiSql;
            return ansiSql;
        }
        
        // Translate ANSI SQL to provider-specific dialect
        try
        {
            Logger.Debug("Translating SQL from ANSI to {Dialect}", targetDialect);
            var translatedSql = await _sqlMermaidService.TranslateSqlDialectAsync(
                ansiSql,
                SqlDialect.AnsiSql,
                targetDialect
            );
            
            Logger.Info("Translated SQL statement {Statement} to {Dialect} ({Length} chars)", 
                statementName, targetDialect, translatedSql.Length);
            
            // Cache the translation
            _translationCache[cacheKey] = translatedSql;
            
            return translatedSql;
        }
        catch (Exception ex)
        {
            Logger.Warn(ex, "Failed to translate SQL for {Provider}, falling back to ANSI SQL", provider);
            // Fallback to ANSI SQL if translation fails
            _translationCache[cacheKey] = ansiSql;
            return ansiSql;
        }
    }
    
    /// <summary>
    /// Load provider-specific SQL statement from JSON file.
    /// </summary>
    private string LoadProviderSqlStatement(string provider, string statementName)
    {
        var jsonFilePath = Path.Combine(_configFilesPath, $"{provider.ToLowerInvariant()}_missing_fk_sql_statements.json");
        
        if (!File.Exists(jsonFilePath))
        {
            Logger.Debug("Provider-specific SQL file not found: {Path}", jsonFilePath);
            return string.Empty;
        }
        
        try
        {
            var json = File.ReadAllText(jsonFilePath);
            var doc = JsonDocument.Parse(json);
            
            if (doc.RootElement.TryGetProperty("statements", out var statements))
            {
                if (statements.TryGetProperty(statementName, out var statement))
                {
                    if (statement.TryGetProperty("sql", out var sqlElement))
                    {
                        var sql = sqlElement.GetString() ?? string.Empty;
                        Logger.Debug("Loaded provider-specific SQL statement {Statement} from {File}", statementName, jsonFilePath);
                        return sql;
                    }
                }
            }
            
            Logger.Debug("SQL statement {Statement} not found in {File}", statementName, jsonFilePath);
            return string.Empty;
        }
        catch (Exception ex)
        {
            Logger.Warn(ex, "Failed to load provider-specific SQL statement {Statement} from {File}", statementName, jsonFilePath);
            return string.Empty;
        }
    }
    
    /// <summary>
    /// Load ANSI SQL statement from JSON file.
    /// </summary>
    private string LoadAnsiSqlStatement(string statementName)
    {
        var jsonFilePath = Path.Combine(_configFilesPath, "missing_fk_sql_statements.json");
        
        if (!File.Exists(jsonFilePath))
        {
            Logger.Error("Missing FK SQL statements file not found: {Path}", jsonFilePath);
            throw new FileNotFoundException($"Missing FK SQL statements file not found: {jsonFilePath}");
        }
        
        try
        {
            var json = File.ReadAllText(jsonFilePath);
            var doc = JsonDocument.Parse(json);
            
            if (doc.RootElement.TryGetProperty("statements", out var statements))
            {
                if (statements.TryGetProperty(statementName, out var statement))
                {
                    if (statement.TryGetProperty("sql", out var sqlElement))
                    {
                        var sql = sqlElement.GetString() ?? string.Empty;
                        Logger.Debug("Loaded SQL statement {Statement} from JSON", statementName);
                        return sql;
                    }
                }
            }
            
            Logger.Error("SQL statement {Statement} not found in JSON file", statementName);
            return string.Empty;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load SQL statement {Statement} from JSON", statementName);
            throw;
        }
    }
    
    /// <summary>
    /// Map provider type string to SqlDialect enum.
    /// Note: DB2 is not a target dialect in SqlMermaidErdTools, so DB2 uses ANSI SQL directly.
    /// </summary>
    private SqlDialect MapProviderToSqlDialect(string provider)
    {
        return provider.ToUpperInvariant() switch
        {
            "DB2" => SqlDialect.AnsiSql, // DB2 not supported as target dialect, use ANSI
            "POSTGRESQL" or "POSTGRES" => SqlDialect.PostgreSql,
            "SQLSERVER" or "MSSQL" => SqlDialect.SqlServer,
            "MYSQL" => SqlDialect.MySql,
            "ORACLE" => SqlDialect.Oracle,
            "ANSI" or "ANSISQL" => SqlDialect.AnsiSql,
            _ => SqlDialect.AnsiSql // Default to ANSI for unknown providers
        };
    }
    
    /// <summary>
    /// Clear translation cache (useful for testing or when SQL statements are updated).
    /// </summary>
    public void ClearCache()
    {
        _translationCache.Clear();
        Logger.Debug("Translation cache cleared");
    }
}
