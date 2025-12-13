using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using NLog;
using WindowsDb2Editor.Data;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Model for table metadata
/// </summary>
public class TableMetadata
{
    public string Schema { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public List<ColumnMetadata> Columns { get; set; } = new();
    public List<IndexMetadata> Indexes { get; set; } = new();
}

/// <summary>
/// Model for column metadata
/// </summary>
public class ColumnMetadata
{
    public string Name { get; set; } = string.Empty;
    public string DataType { get; set; } = string.Empty;
    public int? Length { get; set; }
    public int? Scale { get; set; }
    public bool Nullable { get; set; }
    public string? DefaultValue { get; set; }
    public string? Comments { get; set; }
}

/// <summary>
/// Model for index metadata
/// </summary>
public class IndexMetadata
{
    public string Name { get; set; } = string.Empty;
    public bool IsUnique { get; set; }
    public bool IsPrimary { get; set; }
    public List<string> Columns { get; set; } = new();
}

/// <summary>
/// Service for loading and caching metadata
/// </summary>
public class MetadataLoaderService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly MetadataHandler _metadataHandler;
    private readonly string _metadataFolder;
    private readonly Dictionary<string, TableMetadata> _cache = new();
    
    public MetadataLoaderService()
    {
        _metadataHandler = App.MetadataHandler ?? throw new InvalidOperationException("MetadataHandler not initialized");
        
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        _metadataFolder = Path.Combine(appData, "WindowsDb2Editor", "metadata");
        
        Logger.Debug("Metadata folder: {Folder}", _metadataFolder);
    }
    
    /// <summary>
    /// Get all schemas
    /// </summary>
    public async Task<List<string>> GetAllSchemasAsync(DB2ConnectionManager connectionManager)
    {
        Logger.Debug("Getting all schemas");
        
        try
        {
            var sql = @"
                SELECT DISTINCT SCHEMANAME
                FROM SYSCAT.SCHEMATA
                WHERE SCHEMANAME NOT IN ('SYSIBM', 'SYSIBMADM', 'SYSCAT', 'SYSPROC')
                ORDER BY SCHEMANAME
            ";
            
            var result = await connectionManager.ExecuteQueryAsync(sql);
            var schemas = new List<string>();
            
            foreach (DataRow row in result.Rows)
            {
                schemas.Add(row["SCHEMANAME"]?.ToString() ?? string.Empty);
            }
            
            Logger.Info("Found {Count} schemas", schemas.Count);
            return schemas;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get schemas");
            return new List<string>();
        }
    }
    
    /// <summary>
    /// Get all tables for a schema
    /// </summary>
    public async Task<List<TableMetadata>> GetTablesAsync(
        DB2ConnectionManager connectionManager,
        string schema)
    {
        Logger.Info("Getting tables for schema: {Schema}", schema);
        
        try
        {
            var sqlTemplate = _metadataHandler.GetQuery("DB2", "12.1", "SERVICE_GetTablesForSchema");
            var sql = sqlTemplate.Replace("?", $"'{schema}'");
            
            Logger.Debug("Using query: SERVICE_GetTablesForSchema");
            var result = await connectionManager.ExecuteQueryAsync(sql);
            var tables = new List<TableMetadata>();
            
            foreach (DataRow row in result.Rows)
            {
                tables.Add(new TableMetadata
                {
                    Schema = row["TABSCHEMA"]?.ToString() ?? string.Empty,
                    Name = row["TABNAME"]?.ToString() ?? string.Empty,
                    Type = row["TYPE"]?.ToString() ?? string.Empty
                });
            }
            
            Logger.Info("Found {Count} tables in schema {Schema}", tables.Count, schema);
            return tables;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get tables for schema {Schema}", schema);
            return new List<TableMetadata>();
        }
    }
    
    /// <summary>
    /// Get table metadata with columns and indexes
    /// </summary>
    public async Task<TableMetadata?> GetTableDetailsAsync(
        DB2ConnectionManager connectionManager,
        string schema,
        string tableName)
    {
        Logger.Info("Getting table details for {Schema}.{Table}", schema, tableName);
        
        try
        {
            var metadata = new TableMetadata
            {
                Schema = schema,
                Name = tableName,
                Type = "TABLE"
            };
            
            // Get columns
            var columnsSqlTemplate = _metadataHandler.GetQuery("DB2", "12.1", "SERVICE_GetColumnMetadataForTable");
            var columnsSql = columnsSqlTemplate.Replace("?", $"'{schema}'", 1).Replace("?", $"'{tableName}'", 1);
            
            Logger.Debug("Using query: SERVICE_GetColumnMetadataForTable");
            var columns = await connectionManager.ExecuteQueryAsync(columnsSql);
            
            foreach (DataRow row in columns.Rows)
            {
                metadata.Columns.Add(new ColumnMetadata
                {
                    Name = row["COLNAME"]?.ToString() ?? string.Empty,
                    DataType = row["TYPENAME"]?.ToString() ?? string.Empty,
                    Length = row["LENGTH"] == DBNull.Value ? null : Convert.ToInt32(row["LENGTH"]),
                    Scale = row["SCALE"] == DBNull.Value ? null : Convert.ToInt32(row["SCALE"]),
                    Nullable = row["NULLS"]?.ToString() == "Y",
                    DefaultValue = row["DEFAULT"] == DBNull.Value ? null : row["DEFAULT"]?.ToString(),
                    Comments = row["REMARKS"] == DBNull.Value ? null : row["REMARKS"]?.ToString()
                });
            }
            
            Logger.Info("Loaded {Count} columns for {Schema}.{Table}", metadata.Columns.Count, schema, tableName);
            return metadata;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get table details for {Schema}.{Table}", schema, tableName);
            return null;
        }
    }
}

