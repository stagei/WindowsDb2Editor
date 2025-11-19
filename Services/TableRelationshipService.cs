using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using WindowsDb2Editor.Data;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Model for incoming foreign key reference
/// </summary>
public class IncomingForeignKey
{
    public string ReferencingSchema { get; set; } = string.Empty;
    public string ReferencingTable { get; set; } = string.Empty;
    public string ConstraintName { get; set; } = string.Empty;
    public string FkColumns { get; set; } = string.Empty;
    public string PkColumns { get; set; } = string.Empty;
    public string DeleteRule { get; set; } = string.Empty;
    public string UpdateRule { get; set; } = string.Empty;
}

/// <summary>
/// Model for referencing object (package, view, procedure, function)
/// </summary>
public class ReferencingObject
{
    public string ObjectType { get; set; } = string.Empty; // PACKAGE, VIEW, PROCEDURE, FUNCTION
    public string Schema { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime? LastUsed { get; set; }
}

/// <summary>
/// Service for analyzing table relationships and dependencies
/// </summary>
public class TableRelationshipService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    /// <summary>
    /// Get tables that have foreign keys pointing to this table
    /// </summary>
    public async Task<List<IncomingForeignKey>> GetIncomingForeignKeysAsync(
        DB2ConnectionManager connectionManager,
        string schema,
        string tableName)
    {
        Logger.Info("Getting incoming foreign keys for {Schema}.{Table}", schema, tableName);
        
        try
        {
            var sql = $@"
                SELECT 
                    TABSCHEMA AS REFERENCING_SCHEMA,
                    TABNAME AS REFERENCING_TABLE,
                    CONSTNAME,
                    FK_COLNAMES,
                    PK_COLNAMES,
                    DELETERULE,
                    UPDATERULE
                FROM SYSCAT.REFERENCES
                WHERE REFTABSCHEMA = '{schema}' 
                  AND REFTABNAME = '{tableName}'
                ORDER BY TABSCHEMA, TABNAME
            ";
            
            Logger.Debug("Querying incoming foreign keys");
            var result = await connectionManager.ExecuteQueryAsync(sql);
            
            var foreignKeys = new List<IncomingForeignKey>();
            
            foreach (DataRow row in result.Rows)
            {
                foreignKeys.Add(new IncomingForeignKey
                {
                    ReferencingSchema = row["REFERENCING_SCHEMA"]?.ToString() ?? string.Empty,
                    ReferencingTable = row["REFERENCING_TABLE"]?.ToString() ?? string.Empty,
                    ConstraintName = row["CONSTNAME"]?.ToString() ?? string.Empty,
                    FkColumns = row["FK_COLNAMES"]?.ToString() ?? string.Empty,
                    PkColumns = row["PK_COLNAMES"]?.ToString() ?? string.Empty,
                    DeleteRule = row["DELETERULE"]?.ToString() ?? string.Empty,
                    UpdateRule = row["UPDATERULE"]?.ToString() ?? string.Empty
                });
            }
            
            Logger.Info("Found {Count} incoming foreign keys for {Schema}.{Table}", 
                foreignKeys.Count, schema, tableName);
            
            return foreignKeys;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get incoming foreign keys for {Schema}.{Table}", schema, tableName);
            throw;
        }
    }
    
    /// <summary>
    /// Get packages that reference this table
    /// </summary>
    public async Task<List<ReferencingObject>> GetReferencingPackagesAsync(
        DB2ConnectionManager connectionManager,
        string schema,
        string tableName)
    {
        Logger.Info("Getting packages that reference {Schema}.{Table}", schema, tableName);
        
        try
        {
            var sql = $@"
                SELECT DISTINCT 
                    'PACKAGE' AS OBJECT_TYPE,
                    d.TABSCHEMA AS SCHEMA,
                    d.TABNAME AS NAME,
                    p.LASTUSED
                FROM SYSCAT.TABDEP d
                JOIN SYSCAT.PACKAGES p 
                  ON d.TABSCHEMA = p.PKGSCHEMA 
                  AND d.TABNAME = p.PKGNAME
                WHERE d.BSCHEMA = '{schema}' 
                  AND d.BNAME = '{tableName}'
                  AND d.DTYPE = 'O'
                ORDER BY d.TABSCHEMA, d.TABNAME
            ";
            
            Logger.Debug("Querying referencing packages");
            var result = await connectionManager.ExecuteQueryAsync(sql);
            
            var packages = ConvertToReferencingObjects(result);
            
            Logger.Info("Found {Count} packages referencing {Schema}.{Table}", 
                packages.Count, schema, tableName);
            
            return packages;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get referencing packages for {Schema}.{Table}", schema, tableName);
            return new List<ReferencingObject>(); // Return empty list on error
        }
    }
    
    /// <summary>
    /// Get views that reference this table
    /// </summary>
    public async Task<List<ReferencingObject>> GetReferencingViewsAsync(
        DB2ConnectionManager connectionManager,
        string schema,
        string tableName)
    {
        Logger.Info("Getting views that reference {Schema}.{Table}", schema, tableName);
        
        try
        {
            var sql = $@"
                SELECT DISTINCT 
                    'VIEW' AS OBJECT_TYPE,
                    TABSCHEMA AS SCHEMA,
                    TABNAME AS NAME,
                    CAST(NULL AS TIMESTAMP) AS LASTUSED
                FROM SYSCAT.TABDEP
                WHERE BSCHEMA = '{schema}' 
                  AND BNAME = '{tableName}'
                  AND DTYPE = 'V'
                ORDER BY TABSCHEMA, TABNAME
            ";
            
            Logger.Debug("Querying referencing views");
            var result = await connectionManager.ExecuteQueryAsync(sql);
            
            var views = ConvertToReferencingObjects(result);
            
            Logger.Info("Found {Count} views referencing {Schema}.{Table}", 
                views.Count, schema, tableName);
            
            return views;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get referencing views for {Schema}.{Table}", schema, tableName);
            return new List<ReferencingObject>();
        }
    }
    
    /// <summary>
    /// Get procedures and functions that reference this table
    /// </summary>
    public async Task<List<ReferencingObject>> GetReferencingRoutinesAsync(
        DB2ConnectionManager connectionManager,
        string schema,
        string tableName)
    {
        Logger.Info("Getting procedures/functions that reference {Schema}.{Table}", schema, tableName);
        
        try
        {
            var sql = $@"
                SELECT DISTINCT 
                    CASE 
                        WHEN DTYPE = 'P' THEN 'PROCEDURE'
                        WHEN DTYPE = 'F' THEN 'FUNCTION'
                        ELSE DTYPE
                    END AS OBJECT_TYPE,
                    TABSCHEMA AS SCHEMA,
                    TABNAME AS NAME,
                    CAST(NULL AS TIMESTAMP) AS LASTUSED
                FROM SYSCAT.TABDEP
                WHERE BSCHEMA = '{schema}' 
                  AND BNAME = '{tableName}'
                  AND DTYPE IN ('P', 'F')
                ORDER BY OBJECT_TYPE, SCHEMA, NAME
            ";
            
            Logger.Debug("Querying referencing routines");
            var result = await connectionManager.ExecuteQueryAsync(sql);
            
            var routines = ConvertToReferencingObjects(result);
            
            Logger.Info("Found {Count} procedures/functions referencing {Schema}.{Table}", 
                routines.Count, schema, tableName);
            
            return routines;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get referencing routines for {Schema}.{Table}", schema, tableName);
            return new List<ReferencingObject>();
        }
    }
    
    /// <summary>
    /// Convert DataTable to list of ReferencingObject
    /// </summary>
    private List<ReferencingObject> ConvertToReferencingObjects(DataTable dataTable)
    {
        var objects = new List<ReferencingObject>();
        
        foreach (DataRow row in dataTable.Rows)
        {
            try
            {
                objects.Add(new ReferencingObject
                {
                    ObjectType = row["OBJECT_TYPE"]?.ToString() ?? string.Empty,
                    Schema = row["SCHEMA"]?.ToString() ?? string.Empty,
                    Name = row["NAME"]?.ToString() ?? string.Empty,
                    LastUsed = row["LASTUSED"] == DBNull.Value ? null : Convert.ToDateTime(row["LASTUSED"])
                });
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "Failed to convert row to ReferencingObject");
            }
        }
        
        return objects;
    }
}

