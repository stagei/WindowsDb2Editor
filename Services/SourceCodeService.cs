using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using WindowsDb2Editor.Data;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Model for database source code object
/// </summary>
public class SourceCodeObject
{
    public string ObjectType { get; set; } = string.Empty; // PROCEDURE, FUNCTION, VIEW, TRIGGER
    public string Schema { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? SourceCode { get; set; }
    public DateTime? CreatedTime { get; set; }
    public DateTime? LastRegenTime { get; set; }
    public string? Language { get; set; }
    
    public bool HasSource => !string.IsNullOrWhiteSpace(SourceCode);
    public string DisplayName => $"{Schema}.{Name}";
}

/// <summary>
/// Service for browsing and extracting database source code
/// </summary>
public class SourceCodeService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly MetadataHandler? _metadataHandler;
    
    public SourceCodeService(MetadataHandler? metadataHandler = null)
    {
        _metadataHandler = metadataHandler ?? App.MetadataHandler;
    }
    
    /// <summary>
    /// Get all procedures in a schema
    /// </summary>
    public async Task<List<SourceCodeObject>> GetProceduresAsync(
        DB2ConnectionManager connectionManager,
        string schema)
    {
        Logger.Info("Getting procedures for schema: {Schema}", schema);
        
        try
        {
            var sql = $@"
                SELECT
                    ROUTINETYPE AS OBJECT_TYPE,
                    ROUTINESCHEMA AS SCHEMA,
                    ROUTINENAME AS NAME,
                    TEXT AS SOURCE_CODE,
                    CREATE_TIME,
                    LAST_REGEN_TIME,
                    LANGUAGE
                FROM SYSCAT.ROUTINES
                WHERE ROUTINESCHEMA = '{schema}'
                  AND ROUTINETYPE = 'P'
                ORDER BY ROUTINENAME
            ";
            
            var result = await connectionManager.ExecuteQueryAsync(sql);
            Logger.Info("Found {Count} procedures in schema {Schema}", result.Rows.Count, schema);
            
            return ConvertToSourceCodeObjects(result);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get procedures for schema {Schema}", schema);
            throw;
        }
    }
    
    /// <summary>
    /// Get all functions in a schema
    /// </summary>
    public async Task<List<SourceCodeObject>> GetFunctionsAsync(
        DB2ConnectionManager connectionManager,
        string schema)
    {
        Logger.Info("Getting functions for schema: {Schema}", schema);
        
        try
        {
            var sql = $@"
                SELECT
                    ROUTINETYPE AS OBJECT_TYPE,
                    ROUTINESCHEMA AS SCHEMA,
                    ROUTINENAME AS NAME,
                    TEXT AS SOURCE_CODE,
                    CREATE_TIME,
                    LAST_REGEN_TIME,
                    LANGUAGE
                FROM SYSCAT.ROUTINES
                WHERE ROUTINESCHEMA = '{schema}'
                  AND ROUTINETYPE = 'F'
                ORDER BY ROUTINENAME
            ";
            
            var result = await connectionManager.ExecuteQueryAsync(sql);
            Logger.Info("Found {Count} functions in schema {Schema}", result.Rows.Count, schema);
            
            return ConvertToSourceCodeObjects(result);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get functions for schema {Schema}", schema);
            throw;
        }
    }
    
    /// <summary>
    /// Get all views in a schema
    /// </summary>
    public async Task<List<SourceCodeObject>> GetViewsAsync(
        DB2ConnectionManager connectionManager,
        string schema)
    {
        Logger.Info("Getting views for schema: {Schema}", schema);
        
        try
        {
            var sql = $@"
                SELECT
                    'VIEW' AS OBJECT_TYPE,
                    VIEWSCHEMA AS SCHEMA,
                    VIEWNAME AS NAME,
                    TEXT AS SOURCE_CODE,
                    CAST(NULL AS TIMESTAMP) AS CREATE_TIME,
                    CAST(NULL AS TIMESTAMP) AS LAST_REGEN_TIME,
                    CAST(NULL AS VARCHAR(1)) AS LANGUAGE
                FROM SYSCAT.VIEWS
                WHERE VIEWSCHEMA = '{schema}'
                ORDER BY VIEWNAME
            ";
            
            var result = await connectionManager.ExecuteQueryAsync(sql);
            Logger.Info("Found {Count} views in schema {Schema}", result.Rows.Count, schema);
            
            return ConvertToSourceCodeObjects(result);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get views for schema {Schema}", schema);
            throw;
        }
    }
    
    /// <summary>
    /// Get all triggers for a table
    /// </summary>
    public async Task<List<SourceCodeObject>> GetTriggersAsync(
        DB2ConnectionManager connectionManager,
        string schema,
        string tableName)
    {
        Logger.Info("Getting triggers for {Schema}.{Table}", schema, tableName);
        
        try
        {
            var sql = $@"
                SELECT
                    'TRIGGER' AS OBJECT_TYPE,
                    TRIGSCHEMA AS SCHEMA,
                    TRIGNAME AS NAME,
                    TEXT AS SOURCE_CODE,
                    CREATE_TIME,
                    LAST_REGEN_TIME,
                    CAST(NULL AS VARCHAR(1)) AS LANGUAGE
                FROM SYSCAT.TRIGGERS
                WHERE TABSCHEMA = '{schema}'
                  AND TABNAME = '{tableName}'
                ORDER BY TRIGNAME
            ";
            
            var result = await connectionManager.ExecuteQueryAsync(sql);
            Logger.Info("Found {Count} triggers for {Schema}.{Table}", result.Rows.Count, schema, tableName);
            
            return ConvertToSourceCodeObjects(result);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get triggers for {Schema}.{Table}", schema, tableName);
            throw;
        }
    }
    
    /// <summary>
    /// Convert DataTable to list of SourceCodeObject
    /// </summary>
    private List<SourceCodeObject> ConvertToSourceCodeObjects(DataTable dataTable)
    {
        var objects = new List<SourceCodeObject>();
        
        foreach (DataRow row in dataTable.Rows)
        {
            try
            {
                var obj = new SourceCodeObject
                {
                    ObjectType = row["OBJECT_TYPE"]?.ToString() ?? string.Empty,
                    Schema = row["SCHEMA"]?.ToString() ?? string.Empty,
                    Name = row["NAME"]?.ToString() ?? string.Empty,
                    SourceCode = row["SOURCE_CODE"] == DBNull.Value ? null : row["SOURCE_CODE"]?.ToString(),
                    CreatedTime = row["CREATE_TIME"] == DBNull.Value ? null : Convert.ToDateTime(row["CREATE_TIME"]),
                    LastRegenTime = row["LAST_REGEN_TIME"] == DBNull.Value ? null : Convert.ToDateTime(row["LAST_REGEN_TIME"]),
                    Language = row["LANGUAGE"] == DBNull.Value ? null : row["LANGUAGE"]?.ToString()
                };
                
                objects.Add(obj);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "Failed to convert row to SourceCodeObject");
            }
        }
        
        return objects;
    }
    
    /// <summary>
    /// Export source code to files
    /// </summary>
    public async Task ExportSourceToFilesAsync(
        List<SourceCodeObject> objects,
        string outputDirectory)
    {
        Logger.Info("Exporting {Count} source code objects to {Directory}", objects.Count, outputDirectory);
        
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
            Logger.Debug("Created output directory: {Directory}", outputDirectory);
        }
        
        foreach (var obj in objects.Where(o => o.HasSource))
        {
            try
            {
                var fileName = $"{obj.Schema}.{obj.Name}.{obj.ObjectType}.sql";
                var filePath = Path.Combine(outputDirectory, fileName);
                
                await File.WriteAllTextAsync(filePath, obj.SourceCode ?? string.Empty);
                Logger.Debug("Exported: {File}", fileName);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "Failed to export {Schema}.{Name}", obj.Schema, obj.Name);
            }
        }
        
        Logger.Info("Source code export completed");
    }
}

