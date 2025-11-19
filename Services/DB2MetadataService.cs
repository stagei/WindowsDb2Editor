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
/// Service for collecting and caching DB2 metadata
/// </summary>
public class DB2MetadataService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly string _metadataFolder;
    
    public DB2MetadataService()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        _metadataFolder = Path.Combine(appData, "WindowsDb2Editor", "metadata");
        
        if (!Directory.Exists(_metadataFolder))
        {
            Directory.CreateDirectory(_metadataFolder);
            Logger.Debug("Created metadata folder: {Folder}", _metadataFolder);
        }
        
        Logger.Debug("Metadata folder: {Folder}", _metadataFolder);
    }
    
    /// <summary>
    /// Collect database metadata and save to JSON
    /// </summary>
    public async Task CollectMetadataAsync(DB2ConnectionManager connectionManager, string profileName)
    {
        Logger.Info("Starting metadata collection for profile: {Profile}", profileName);
        
        try
        {
            // Step 1: Get DB2 version
            var version = await GetDB2VersionAsync(connectionManager);
            Logger.Info("DB2 Version detected: {Version}", version);
            
            // Step 2: Get all SYSCAT tables
            var syscatTables = await GetSyscatTablesAsync(connectionManager);
            Logger.Info("Found {Count} SYSCAT tables", syscatTables.Rows.Count);
            
            // Step 3: Save to JSON file
            var fileName = $"db2_syscat_{version}_{SanitizeFileName(profileName)}.json";
            await SaveMetadataAsync(fileName, syscatTables, version);
            
            Logger.Info("Metadata collection completed successfully");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to collect metadata");
            throw;
        }
    }
    
    /// <summary>
    /// Get DB2 version information
    /// </summary>
    private async Task<string> GetDB2VersionAsync(DB2ConnectionManager connectionManager)
    {
        Logger.Debug("Querying DB2 version");
        
        try
        {
            const string versionSql = @"
                SELECT PROD_RELEASE 
                FROM SYSIBMADM.ENV_PROD_INFO 
                WHERE LICENSE_INSTALLED = 'Y'
                FETCH FIRST 1 ROW ONLY
            ";
            
            var result = await connectionManager.ExecuteQueryAsync(versionSql);
            
            if (result.Rows.Count > 0)
            {
                var version = result.Rows[0]["PROD_RELEASE"].ToString()?.Trim();
                Logger.Debug("Raw version string: {Version}", version);
                
                // Clean up version string (e.g., "DB2 v11.5.0.0" -> "11.5")
                version = CleanVersionString(version ?? "unknown");
                return version;
            }
            
            Logger.Warn("No version information found, using 'unknown'");
            return "unknown";
        }
        catch (Exception ex)
        {
            Logger.Warn(ex, "Failed to get DB2 version, using 'unknown'");
            return "unknown";
        }
    }
    
    /// <summary>
    /// Clean version string to extract major.minor version
    /// </summary>
    private string CleanVersionString(string version)
    {
        // Extract major.minor version (e.g., "11.5" from "DB2 v11.5.0.0")
        var match = System.Text.RegularExpressions.Regex.Match(version, @"(\d+\.\d+)");
        if (match.Success)
        {
            return match.Groups[1].Value;
        }
        
        // Fallback: remove special characters
        return version.Replace(" ", "_").Replace(".", "_").Replace("/", "_");
    }
    
    /// <summary>
    /// Get all SYSCAT tables
    /// </summary>
    private async Task<DataTable> GetSyscatTablesAsync(DB2ConnectionManager connectionManager)
    {
        Logger.Debug("Querying SYSCAT.TABLES");
        
        const string tablesSql = @"
            SELECT * 
            FROM SYSCAT.TABLES 
            WHERE TABSCHEMA = 'SYSCAT'
            ORDER BY TABNAME
        ";
        
        return await connectionManager.ExecuteQueryAsync(tablesSql);
    }
    
    /// <summary>
    /// Save metadata to JSON file
    /// </summary>
    private async Task SaveMetadataAsync(string fileName, DataTable dataTable, string version)
    {
        var filePath = Path.Combine(_metadataFolder, fileName);
        Logger.Debug("Saving metadata to: {File}", filePath);
        
        // Check if file already exists and has data
        if (File.Exists(filePath))
        {
            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Length > 0)
            {
                Logger.Info("Metadata file already exists and contains data, skipping: {File}", fileName);
                return;
            }
        }
        
        // Convert DataTable to JSON
        var rows = new List<Dictionary<string, object?>>();
        
        foreach (DataRow row in dataTable.Rows)
        {
            var dict = new Dictionary<string, object?>();
            foreach (DataColumn column in dataTable.Columns)
            {
                var value = row[column];
                dict[column.ColumnName] = value == DBNull.Value ? null : value;
            }
            rows.Add(dict);
        }
        
        var metadata = new
        {
            CollectedAt = DateTime.UtcNow,
            DB2Version = version,
            RowCount = dataTable.Rows.Count,
            Columns = dataTable.Columns.Cast<DataColumn>().Select(c => new
            {
                c.ColumnName,
                DataType = c.DataType.Name
            }).ToList(),
            Data = rows
        };
        
        var options = new JsonSerializerOptions 
        { 
            WriteIndented = true 
        };
        var json = JsonSerializer.Serialize(metadata, options);
        
        await File.WriteAllTextAsync(filePath, json);
        Logger.Info("Metadata saved: {File} ({Size} bytes)", fileName, json.Length);
    }
    
    /// <summary>
    /// Collect table-specific metadata
    /// </summary>
    public async Task CollectTableMetadataAsync(
        DB2ConnectionManager connectionManager, 
        string tableName, 
        string schemaName,
        string version,
        string profileName)
    {
        Logger.Info("Collecting metadata for table: {Schema}.{Table}", schemaName, tableName);
        
        try
        {
            // Get column information
            var columnsSql = $@"
                SELECT * 
                FROM SYSCAT.COLUMNS 
                WHERE TABNAME = '{tableName}' 
                  AND TABSCHEMA = '{schemaName}'
                ORDER BY COLNO
            ";
            
            var columns = await connectionManager.ExecuteQueryAsync(columnsSql);
            
            // Get index information
            var indexesSql = $@"
                SELECT * 
                FROM SYSCAT.INDEXES 
                WHERE TABNAME = '{tableName}' 
                  AND TABSCHEMA = '{schemaName}'
            ";
            
            var indexes = await connectionManager.ExecuteQueryAsync(indexesSql);
            
            // Save combined metadata
            var fileName = $"db2_table_{schemaName}_{tableName}_{version}_{SanitizeFileName(profileName)}.json";
            var filePath = Path.Combine(_metadataFolder, fileName);
            
            var metadata = new
            {
                Table = new { Schema = schemaName, Name = tableName },
                CollectedAt = DateTime.UtcNow,
                DB2Version = version,
                Columns = ConvertToList(columns),
                Indexes = ConvertToList(indexes)
            };
            
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(metadata, options);
            await File.WriteAllTextAsync(filePath, json);
            
            Logger.Info("Table metadata saved: {File}", fileName);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to collect table metadata for {Schema}.{Table}", schemaName, tableName);
            throw;
        }
    }
    
    /// <summary>
    /// Convert DataTable to list of dictionaries
    /// </summary>
    private List<Dictionary<string, object?>> ConvertToList(DataTable dataTable)
    {
        var list = new List<Dictionary<string, object?>>();
        
        foreach (DataRow row in dataTable.Rows)
        {
            var dict = new Dictionary<string, object?>();
            foreach (DataColumn column in dataTable.Columns)
            {
                var value = row[column];
                dict[column.ColumnName] = value == DBNull.Value ? null : value;
            }
            list.Add(dict);
        }
        
        return list;
    }
    
    /// <summary>
    /// Sanitize filename to remove invalid characters
    /// </summary>
    private string SanitizeFileName(string fileName)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        return string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
    }
}

