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
    /// Collect database metadata and save to JSON (version-specific, not database-specific)
    /// </summary>
    public async Task CollectMetadataAsync(DB2ConnectionManager connectionManager, string profileName)
    {
        Logger.Info("Starting metadata collection for profile: {Profile}", profileName);
        
        try
        {
            // Step 1: Get DB2 version
            var version = await GetDB2VersionAsync(connectionManager);
            Logger.Info("DB2 Version detected: {Version}", version);
            
            // Step 2: Check if version-specific metadata file already exists
            var fileName = $"db2_{version}_system_tables.json";
            var filePath = Path.Combine(_metadataFolder, fileName);
            
            if (File.Exists(filePath))
            {
                var fileInfo = new FileInfo(filePath);
                if (fileInfo.Length > 0)
                {
                    Logger.Info("Metadata file for version {Version} already exists, skipping collection: {File}", version, fileName);
                    return;
                }
            }
            
            // Step 3: Get all SYSCAT tables
            var syscatTables = await GetSyscatTablesAsync(connectionManager);
            Logger.Info("Found {Count} SYSCAT tables", syscatTables.Rows.Count);
            
            // Step 4: Build relationship documentation
            var relationships = BuildSyscatRelationships();
            
            // Step 5: Build query patterns
            var queryPatterns = BuildQueryPatterns();
            
            // Step 6: Save to JSON file
            await SaveMetadataWithRelationshipsAsync(fileName, syscatTables, version, relationships, queryPatterns);
            
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
    
    /// <summary>
    /// Build SYSCAT relationship documentation
    /// </summary>
    private Dictionary<string, object> BuildSyscatRelationships()
    {
        Logger.Debug("Building SYSCAT relationship documentation");
        
        return new Dictionary<string, object>
        {
            ["description"] = "DB2 SYSCAT System Catalog Table Relationships",
            ["version"] = "1.0",
            ["relationships"] = new Dictionary<string, object>
            {
                ["SYSCAT.TABLES"] = new Dictionary<string, object>
                {
                    ["primary_keys"] = new[] { "TABSCHEMA", "TABNAME" },
                    ["related_tables"] = new Dictionary<string, object>
                    {
                        ["SYSCAT.COLUMNS"] = new
                        {
                            join_condition = "SYSCAT.TABLES.TABSCHEMA = SYSCAT.COLUMNS.TABSCHEMA AND SYSCAT.TABLES.TABNAME = SYSCAT.COLUMNS.TABNAME",
                            description = "Get all columns for a table",
                            cardinality = "1:N"
                        },
                        ["SYSCAT.INDEXES"] = new
                        {
                            join_condition = "SYSCAT.TABLES.TABSCHEMA = SYSCAT.INDEXES.TABSCHEMA AND SYSCAT.TABLES.TABNAME = SYSCAT.INDEXES.TABNAME",
                            description = "Get all indexes for a table",
                            cardinality = "1:N"
                        },
                        ["SYSCAT.REFERENCES"] = new
                        {
                            join_condition = "SYSCAT.TABLES.TABSCHEMA = SYSCAT.REFERENCES.TABSCHEMA AND SYSCAT.TABLES.TABNAME = SYSCAT.REFERENCES.TABNAME",
                            description = "Get foreign keys FROM this table (referencing other tables)",
                            cardinality = "1:N"
                        },
                        ["SYSCAT.REFERENCES_PK"] = new
                        {
                            join_condition = "SYSCAT.TABLES.TABSCHEMA = SYSCAT.REFERENCES.REFTABSCHEMA AND SYSCAT.TABLES.TABNAME = SYSCAT.REFERENCES.REFTABNAME",
                            description = "Get foreign keys TO this table (referenced by other tables)",
                            cardinality = "1:N"
                        },
                        ["SYSCAT.TABCONST"] = new
                        {
                            join_condition = "SYSCAT.TABLES.TABSCHEMA = SYSCAT.TABCONST.TABSCHEMA AND SYSCAT.TABLES.TABNAME = SYSCAT.TABCONST.TABNAME",
                            description = "Get all constraints (PK, FK, CHECK, UNIQUE) for a table",
                            cardinality = "1:N"
                        },
                        ["SYSCAT.TRIGGERS"] = new
                        {
                            join_condition = "SYSCAT.TABLES.TABSCHEMA = SYSCAT.TRIGGERS.TABSCHEMA AND SYSCAT.TABLES.TABNAME = SYSCAT.TRIGGERS.TABNAME",
                            description = "Get all triggers on a table",
                            cardinality = "1:N"
                        },
                        ["SYSCAT.TABDEP"] = new
                        {
                            join_condition = "SYSCAT.TABLES.TABSCHEMA = SYSCAT.TABDEP.TABSCHEMA AND SYSCAT.TABLES.TABNAME = SYSCAT.TABDEP.TABNAME",
                            description = "Get dependencies (views, routines, packages) that reference this table",
                            cardinality = "1:N"
                        },
                        ["SYSCAT.PACKAGEDEP"] = new
                        {
                            join_condition = "SYSCAT.TABLES.TABSCHEMA = SYSCAT.PACKAGEDEP.BSCHEMA AND SYSCAT.TABLES.TABNAME = SYSCAT.PACKAGEDEP.BNAME",
                            description = "Get packages that use this table",
                            cardinality = "1:N"
                        }
                    }
                },
                ["SYSCAT.COLUMNS"] = new Dictionary<string, object>
                {
                    ["primary_keys"] = new[] { "TABSCHEMA", "TABNAME", "COLNAME" },
                    ["related_tables"] = new Dictionary<string, object>
                    {
                        ["SYSCAT.TABLES"] = new
                        {
                            join_condition = "SYSCAT.COLUMNS.TABSCHEMA = SYSCAT.TABLES.TABSCHEMA AND SYSCAT.COLUMNS.TABNAME = SYSCAT.TABLES.TABNAME",
                            description = "Get table information for a column",
                            cardinality = "N:1"
                        },
                        ["SYSCAT.KEYCOLUSE"] = new
                        {
                            join_condition = "SYSCAT.COLUMNS.TABSCHEMA = SYSCAT.KEYCOLUSE.TABSCHEMA AND SYSCAT.COLUMNS.TABNAME = SYSCAT.KEYCOLUSE.TABNAME AND SYSCAT.COLUMNS.COLNAME = SYSCAT.KEYCOLUSE.COLNAME",
                            description = "Check if column is part of a primary key or unique constraint",
                            cardinality = "1:N"
                        }
                    }
                },
                ["SYSCAT.REFERENCES"] = new Dictionary<string, object>
                {
                    ["description"] = "Foreign key relationships between tables",
                    ["related_tables"] = new Dictionary<string, object>
                    {
                        ["SYSCAT.INDEXES"] = new
                        {
                            join_condition = "SYSCAT.REFERENCES.TABSCHEMA = SYSCAT.INDEXES.TABSCHEMA AND SYSCAT.REFERENCES.TABNAME = SYSCAT.INDEXES.TABNAME AND EXISTS (SELECT 1 FROM SYSCAT.KEYCOLUSE WHERE SYSCAT.KEYCOLUSE.CONSTNAME = SYSCAT.REFERENCES.CONSTNAME AND SYSCAT.INDEXES.COLNAMES LIKE '%' || SYSCAT.KEYCOLUSE.COLNAME || '%')",
                            description = "Check if foreign key columns are indexed (for performance)",
                            cardinality = "1:N"
                        }
                    }
                },
                ["SYSCAT.INDEXES"] = new Dictionary<string, object>
                {
                    ["primary_keys"] = new[] { "INDSCHEMA", "INDNAME" },
                    ["related_tables"] = new Dictionary<string, object>
                    {
                        ["SYSCAT.TABLES"] = new
                        {
                            join_condition = "SYSCAT.INDEXES.TABSCHEMA = SYSCAT.TABLES.TABSCHEMA AND SYSCAT.INDEXES.TABNAME = SYSCAT.TABLES.TABNAME",
                            description = "Get table that this index belongs to",
                            cardinality = "N:1"
                        },
                        ["SYSCAT.INDEXCOLUSE"] = new
                        {
                            join_condition = "SYSCAT.INDEXES.INDSCHEMA = SYSCAT.INDEXCOLUSE.INDSCHEMA AND SYSCAT.INDEXES.INDNAME = SYSCAT.INDEXCOLUSE.INDNAME",
                            description = "Get individual columns in the index with ordering",
                            cardinality = "1:N"
                        }
                    }
                },
                ["SYSCAT.PACKAGES"] = new Dictionary<string, object>
                {
                    ["primary_keys"] = new[] { "PKGSCHEMA", "PKGNAME" },
                    ["related_tables"] = new Dictionary<string, object>
                    {
                        ["SYSCAT.PACKAGEDEP"] = new
                        {
                            join_condition = "SYSCAT.PACKAGES.PKGSCHEMA = SYSCAT.PACKAGEDEP.PKGSCHEMA AND SYSCAT.PACKAGES.PKGNAME = SYSCAT.PACKAGEDEP.PKGNAME",
                            description = "Get all table/view/routine dependencies for this package",
                            cardinality = "1:N"
                        },
                        ["SYSCAT.STATEMENTS"] = new
                        {
                            join_condition = "SYSCAT.PACKAGES.PKGSCHEMA = SYSCAT.STATEMENTS.PKGSCHEMA AND SYSCAT.PACKAGES.PKGNAME = SYSCAT.STATEMENTS.PKGNAME",
                            description = "Get all SQL statements in this package",
                            cardinality = "1:N"
                        }
                    }
                }
            }
        };
    }
    
    /// <summary>
    /// Build common query patterns for SYSCAT tables
    /// </summary>
    private Dictionary<string, object> BuildQueryPatterns()
    {
        Logger.Debug("Building SYSCAT query patterns");
        
        return new Dictionary<string, object>
        {
            ["description"] = "Common query patterns for DB2 SYSCAT system catalog",
            ["patterns"] = new Dictionary<string, object>
            {
                ["find_foreign_keys_to_table"] = new
                {
                    description = "Find all foreign keys that reference a specific table",
                    query = @"SELECT 
    R.CONSTNAME AS FK_NAME,
    R.TABSCHEMA AS FK_SCHEMA,
    R.TABNAME AS FK_TABLE,
    R.REFTABSCHEMA AS PK_SCHEMA,
    R.REFTABNAME AS PK_TABLE,
    R.REFKEYNAME AS PK_CONSTRAINT,
    R.DELETERULE,
    R.UPDATERULE,
    K.COLNAME AS FK_COLUMN
FROM SYSCAT.REFERENCES R
INNER JOIN SYSCAT.KEYCOLUSE K 
    ON R.CONSTNAME = K.CONSTNAME 
    AND R.TABSCHEMA = K.TABSCHEMA 
    AND R.TABNAME = K.TABNAME
WHERE R.REFTABSCHEMA = ? AND R.REFTABNAME = ?
ORDER BY R.CONSTNAME, K.COLSEQ",
                    parameters = new[] { "REFTABSCHEMA", "REFTABNAME" }
                },
                ["find_foreign_keys_from_table"] = new
                {
                    description = "Find all foreign keys FROM a specific table (that reference other tables)",
                    query = @"SELECT 
    R.CONSTNAME AS FK_NAME,
    R.TABSCHEMA AS FK_SCHEMA,
    R.TABNAME AS FK_TABLE,
    R.REFTABSCHEMA AS PK_SCHEMA,
    R.REFTABNAME AS PK_TABLE,
    K.COLNAME AS FK_COLUMN
FROM SYSCAT.REFERENCES R
INNER JOIN SYSCAT.KEYCOLUSE K 
    ON R.CONSTNAME = K.CONSTNAME
WHERE R.TABSCHEMA = ? AND R.TABNAME = ?
ORDER BY R.CONSTNAME, K.COLSEQ",
                    parameters = new[] { "TABSCHEMA", "TABNAME" }
                },
                ["check_if_fk_is_indexed"] = new
                {
                    description = "Check if foreign key columns are indexed (important for performance)",
                    query = @"SELECT 
    R.CONSTNAME AS FK_NAME,
    R.TABSCHEMA,
    R.TABNAME,
    K.COLNAME AS FK_COLUMN,
    I.INDNAME AS INDEX_NAME,
    CASE WHEN I.INDNAME IS NULL THEN 'NO' ELSE 'YES' END AS IS_INDEXED
FROM SYSCAT.REFERENCES R
INNER JOIN SYSCAT.KEYCOLUSE K 
    ON R.CONSTNAME = K.CONSTNAME
LEFT JOIN SYSCAT.INDEXES I 
    ON R.TABSCHEMA = I.TABSCHEMA 
    AND R.TABNAME = I.TABNAME 
    AND I.COLNAMES LIKE K.COLNAME || '%'
WHERE R.TABSCHEMA = ? AND R.TABNAME = ?
ORDER BY R.CONSTNAME, K.COLSEQ",
                    parameters = new[] { "TABSCHEMA", "TABNAME" },
                    note = "Unindexed foreign keys can cause performance issues during JOINs and DELETE/UPDATE cascade operations"
                },
                ["find_packages_using_table"] = new
                {
                    description = "Find all DB2 packages that reference a specific table",
                    query = @"SELECT DISTINCT
    PD.PKGSCHEMA,
    PD.PKGNAME,
    P.OWNER,
    P.VALID,
    P.CREATE_TIME,
    P.LAST_BIND_TIME
FROM SYSCAT.PACKAGEDEP PD
INNER JOIN SYSCAT.PACKAGES P 
    ON PD.PKGSCHEMA = P.PKGSCHEMA 
    AND PD.PKGNAME = P.PKGNAME
WHERE PD.BTYPE = 'T' 
    AND PD.BSCHEMA = ? 
    AND PD.BNAME = ?
ORDER BY PD.PKGSCHEMA, PD.PKGNAME",
                    parameters = new[] { "BSCHEMA", "BNAME" },
                    note = "BTYPE = 'T' means Table dependency"
                },
                ["find_views_using_table"] = new
                {
                    description = "Find all views that depend on a specific table",
                    query = @"SELECT DISTINCT
    TD.BSCHEMA AS VIEW_SCHEMA,
    TD.BNAME AS VIEW_NAME,
    V.OWNER,
    V.READONLY,
    V.VALID
FROM SYSCAT.TABDEP TD
INNER JOIN SYSCAT.VIEWS V 
    ON TD.BSCHEMA = V.VIEWSCHEMA 
    AND TD.BNAME = V.VIEWNAME
WHERE TD.BTYPE = 'V' 
    AND TD.TABSCHEMA = ? 
    AND TD.TABNAME = ?
ORDER BY TD.BSCHEMA, TD.BNAME",
                    parameters = new[] { "TABSCHEMA", "TABNAME" }
                },
                ["find_procedures_using_table"] = new
                {
                    description = "Find all procedures/functions that reference a specific table",
                    query = @"SELECT DISTINCT
    RD.BSCHEMA AS ROUTINE_SCHEMA,
    RD.BNAME AS ROUTINE_NAME,
    R.ROUTINETYPE,
    R.LANGUAGE,
    R.VALID
FROM SYSCAT.ROUTINEDEP RD
INNER JOIN SYSCAT.ROUTINES R 
    ON RD.BSCHEMA = R.ROUTINESCHEMA 
    AND RD.BNAME = R.ROUTINENAME
WHERE RD.BTYPE IN ('T', 'V') 
    AND RD.TABSCHEMA = ? 
    AND RD.TABNAME = ?
ORDER BY RD.BSCHEMA, RD.BNAME",
                    parameters = new[] { "TABSCHEMA", "TABNAME" }
                },
                ["get_complete_table_definition"] = new
                {
                    description = "Get complete table definition including columns, keys, indexes, and constraints",
                    query = @"-- Get table properties
SELECT * FROM SYSCAT.TABLES WHERE TABSCHEMA = ? AND TABNAME = ?;

-- Get columns
SELECT * FROM SYSCAT.COLUMNS WHERE TABSCHEMA = ? AND TABNAME = ? ORDER BY COLNO;

-- Get constraints
SELECT * FROM SYSCAT.TABCONST WHERE TABSCHEMA = ? AND TABNAME = ?;

-- Get indexes
SELECT * FROM SYSCAT.INDEXES WHERE TABSCHEMA = ? AND TABNAME = ?;

-- Get foreign keys (outgoing)
SELECT * FROM SYSCAT.REFERENCES WHERE TABSCHEMA = ? AND TABNAME = ?;

-- Get triggers
SELECT * FROM SYSCAT.TRIGGERS WHERE TABSCHEMA = ? AND TABNAME = ?;",
                    parameters = new[] { "TABSCHEMA", "TABNAME" }
                }
            }
        };
    }
    
    /// <summary>
    /// Save metadata with relationships and query patterns
    /// </summary>
    private async Task SaveMetadataWithRelationshipsAsync(
        string fileName, 
        DataTable syscatTables, 
        string version,
        Dictionary<string, object> relationships,
        Dictionary<string, object> queryPatterns)
    {
        var filePath = Path.Combine(_metadataFolder, fileName);
        Logger.Debug("Saving metadata with relationships to: {File}", filePath);
        
        // Convert DataTable to JSON
        var rows = ConvertToList(syscatTables);
        
        var metadata = new
        {
            CollectedAt = DateTime.UtcNow,
            DB2Version = version,
            Description = "DB2 System Catalog (SYSCAT) metadata including table relationships and query patterns",
            SystemTables = new
            {
                Count = syscatTables.Rows.Count,
                Columns = syscatTables.Columns.Cast<DataColumn>().Select(c => new
                {
                    c.ColumnName,
                    DataType = c.DataType.Name
                }).ToList(),
                Data = rows
            },
            Relationships = relationships,
            QueryPatterns = queryPatterns
        };
        
        var options = new JsonSerializerOptions 
        { 
            WriteIndented = true 
        };
        var json = JsonSerializer.Serialize(metadata, options);
        
        await File.WriteAllTextAsync(filePath, json);
        Logger.Info("Metadata saved with relationships: {File} ({Size} bytes)", fileName, json.Length);
    }
}

