using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using WindowsDb2Editor.Data;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Service for generating DDL statements
/// </summary>
public class DdlGeneratorService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    /// <summary>
    /// Generate complete DDL for a table
    /// </summary>
    public async Task<string> GenerateTableDdlAsync(
        DB2ConnectionManager connectionManager,
        string schema,
        string tableName)
    {
        Logger.Info("Generating DDL for table: {Schema}.{Table}", schema, tableName);
        
        try
        {
            var ddl = new StringBuilder();
            ddl.AppendLine($"-- DDL for table {schema}.{tableName}");
            ddl.AppendLine($"-- Generated: {DateTime.Now}");
            ddl.AppendLine();
            
            // Get table info
            var tableInfo = await GetTableInfoAsync(connectionManager, schema, tableName);
            
            // Get columns
            var columns = await GetColumnsAsync(connectionManager, schema, tableName);
            
            // Generate CREATE TABLE
            ddl.AppendLine(GenerateCreateTableStatement(schema, tableName, columns, tableInfo));
            ddl.AppendLine();
            
            // Get and generate primary key
            var primaryKey = await GetPrimaryKeyAsync(connectionManager, schema, tableName);
            if (!string.IsNullOrEmpty(primaryKey))
            {
                ddl.AppendLine(primaryKey);
                ddl.AppendLine();
            }
            
            // Get and generate foreign keys
            var foreignKeys = await GetForeignKeysAsync(connectionManager, schema, tableName);
            if (!string.IsNullOrEmpty(foreignKeys))
            {
                ddl.AppendLine(foreignKeys);
                ddl.AppendLine();
            }
            
            // Get and generate indexes
            var indexes = await GetIndexesAsync(connectionManager, schema, tableName);
            if (!string.IsNullOrEmpty(indexes))
            {
                ddl.AppendLine(indexes);
                ddl.AppendLine();
            }
            
            Logger.Info("DDL generated successfully for {Schema}.{Table}", schema, tableName);
            return ddl.ToString();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate DDL for {Schema}.{Table}", schema, tableName);
            throw;
        }
    }
    
    /// <summary>
    /// Get table information
    /// </summary>
    private async Task<DataRow?> GetTableInfoAsync(DB2ConnectionManager connectionManager, string schema, string tableName)
    {
        var sql = $@"
            SELECT * FROM SYSCAT.TABLES
            WHERE TABSCHEMA = '{schema}'
              AND TABNAME = '{tableName}'
        ";
        
        var result = await connectionManager.ExecuteQueryAsync(sql);
        return result.Rows.Count > 0 ? result.Rows[0] : null;
    }
    
    /// <summary>
    /// Get column definitions
    /// </summary>
    private async Task<DataTable> GetColumnsAsync(DB2ConnectionManager connectionManager, string schema, string tableName)
    {
        var sql = $@"
            SELECT * FROM SYSCAT.COLUMNS
            WHERE TABSCHEMA = '{schema}'
              AND TABNAME = '{tableName}'
            ORDER BY COLNO
        ";
        
        return await connectionManager.ExecuteQueryAsync(sql);
    }
    
    /// <summary>
    /// Generate CREATE TABLE statement
    /// </summary>
    private string GenerateCreateTableStatement(string schema, string tableName, DataTable columns, DataRow? tableInfo)
    {
        var ddl = new StringBuilder();
        ddl.AppendLine($"CREATE TABLE {schema}.{tableName} (");
        
        var columnDefs = new List<string>();
        
        foreach (DataRow column in columns.Rows)
        {
            var colName = column["COLNAME"]?.ToString();
            var typeName = column["TYPENAME"]?.ToString();
            var length = column["LENGTH"]?.ToString();
            var scale = column["SCALE"]?.ToString();
            var nulls = column["NULLS"]?.ToString();
            var defaultVal = column["DEFAULT"]?.ToString();
            
            var colDef = new StringBuilder();
            colDef.Append($"   \"{colName}\" {typeName}");
            
            // Add length/scale for appropriate types
            if (typeName == "VARCHAR" || typeName == "CHAR")
            {
                colDef.Append($"({length})");
            }
            else if (typeName == "DECIMAL" || typeName == "NUMERIC")
            {
                colDef.Append($"({length},{scale})");
            }
            
            // Add NOT NULL
            if (nulls == "N")
            {
                colDef.Append(" NOT NULL");
            }
            
            // Add DEFAULT
            if (!string.IsNullOrEmpty(defaultVal))
            {
                colDef.Append($" WITH DEFAULT {defaultVal}");
            }
            
            columnDefs.Add(colDef.ToString());
        }
        
        ddl.AppendLine(string.Join(",\n", columnDefs));
        ddl.Append("   )");
        
        // Add DATA CAPTURE
        if (tableInfo != null)
        {
            var dataCapture = tableInfo["DATACAPTURE"]?.ToString();
            if (dataCapture == "Y" || dataCapture == "L")
            {
                ddl.AppendLine();
                ddl.Append("   DATA CAPTURE CHANGES");
            }
            
            var tbspace = tableInfo["TBSPACE"]?.ToString();
            if (!string.IsNullOrEmpty(tbspace))
            {
                ddl.AppendLine();
                ddl.Append($"IN {tbspace}");
            }
        }
        
        ddl.AppendLine(";");
        
        return ddl.ToString();
    }
    
    /// <summary>
    /// Get and generate primary key DDL
    /// </summary>
    private async Task<string> GetPrimaryKeyAsync(DB2ConnectionManager connectionManager, string schema, string tableName)
    {
        var sql = $@"
            SELECT CONSTNAME, COLNAME
            FROM SYSCAT.KEYCOLUSE
            WHERE TABSCHEMA = '{schema}'
              AND TABNAME = '{tableName}'
            ORDER BY COLSEQ
        ";
        
        var result = await connectionManager.ExecuteQueryAsync(sql);
        
        if (result.Rows.Count == 0)
            return string.Empty;
        
        var constraintName = result.Rows[0]["CONSTNAME"]?.ToString();
        var columns = new List<string>();
        
        foreach (DataRow row in result.Rows)
        {
            columns.Add(row["COLNAME"]?.ToString() ?? string.Empty);
        }
        
        return $"-- Primary Key\nALTER TABLE {schema}.{tableName} ADD CONSTRAINT {constraintName} PRIMARY KEY ({string.Join(", ", columns)});";
    }
    
    /// <summary>
    /// Get and generate foreign keys DDL
    /// </summary>
    private async Task<string> GetForeignKeysAsync(DB2ConnectionManager connectionManager, string schema, string tableName)
    {
        var sql = $@"
            SELECT *
            FROM SYSCAT.REFERENCES
            WHERE TABSCHEMA = '{schema}'
              AND TABNAME = '{tableName}'
        ";
        
        var result = await connectionManager.ExecuteQueryAsync(sql);
        
        if (result.Rows.Count == 0)
            return string.Empty;
        
        var ddl = new StringBuilder();
        ddl.AppendLine("-- Foreign Keys");
        
        foreach (DataRow row in result.Rows)
        {
            var constName = row["CONSTNAME"]?.ToString();
            var refSchema = row["REFTABSCHEMA"]?.ToString();
            var refTable = row["REFTABNAME"]?.ToString();
            var fkCols = row["FK_COLNAMES"]?.ToString();
            var pkCols = row["PK_COLNAMES"]?.ToString();
            var deleteRule = row["DELETERULE"]?.ToString();
            var updateRule = row["UPDATERULE"]?.ToString();
            
            ddl.AppendLine($"ALTER TABLE {schema}.{tableName}");
            ddl.AppendLine($"  ADD CONSTRAINT {constName}");
            ddl.AppendLine($"  FOREIGN KEY ({fkCols})");
            ddl.AppendLine($"  REFERENCES {refSchema}.{refTable} ({pkCols})");
            ddl.AppendLine($"  ON DELETE {deleteRule}");
            ddl.AppendLine($"  ON UPDATE {updateRule};");
            ddl.AppendLine();
        }
        
        return ddl.ToString();
    }
    
    /// <summary>
    /// Get and generate indexes DDL
    /// </summary>
    private async Task<string> GetIndexesAsync(DB2ConnectionManager connectionManager, string schema, string tableName)
    {
        var sql = $@"
            SELECT DISTINCT
                i.INDNAME, i.UNIQUERULE, i.INDEXTYPE,
                ic.COLNAME, ic.COLORDER
            FROM SYSCAT.INDEXES i
            LEFT JOIN SYSCAT.INDEXCOLUSE ic
              ON i.INDSCHEMA = ic.INDSCHEMA AND i.INDNAME = ic.INDNAME
            WHERE i.TABSCHEMA = '{schema}'
              AND i.TABNAME = '{tableName}'
              AND i.UNIQUERULE <> 'P'
            ORDER BY i.INDNAME, ic.COLSEQ
        ";
        
        var result = await connectionManager.ExecuteQueryAsync(sql);
        
        if (result.Rows.Count == 0)
            return string.Empty;
        
        var ddl = new StringBuilder();
        ddl.AppendLine("-- Indexes");
        
        var indexGroups = result.AsEnumerable()
            .GroupBy(r => r["INDNAME"]?.ToString());
        
        foreach (var group in indexGroups)
        {
            var indexName = group.Key;
            var firstRow = group.First();
            var uniqueRule = firstRow["UNIQUERULE"]?.ToString();
            
            var columns = group.Select(r => new
            {
                Name = r["COLNAME"]?.ToString(),
                Order = r["COLORDER"]?.ToString()
            }).ToList();
            
            var columnDefs = columns.Select(c => $"{c.Name} {(c.Order == "D" ? "DESC" : "ASC")}");
            
            var unique = uniqueRule == "U" ? "UNIQUE " : "";
            ddl.AppendLine($"CREATE {unique}INDEX {indexName} ON {schema}.{tableName} ({string.Join(", ", columnDefs)});");
        }
        
        return ddl.ToString();
    }
    
    /// <summary>
    /// Generate DDL for entire schema
    /// </summary>
    public async Task<string> GenerateSchemaDdlAsync(
        DB2ConnectionManager connectionManager,
        string schema)
    {
        Logger.Info("Generating DDL for entire schema: {Schema}", schema);
        
        try
        {
            var ddl = new StringBuilder();
            ddl.AppendLine($"-- DDL for schema {schema}");
            ddl.AppendLine($"-- Generated: {DateTime.Now}");
            ddl.AppendLine();
            
            // Get all tables in schema
            var tablesSql = $@"
                SELECT TABNAME
                FROM SYSCAT.TABLES
                WHERE TABSCHEMA = '{schema}'
                  AND TYPE = 'T'
                ORDER BY TABNAME
            ";
            
            var tables = await connectionManager.ExecuteQueryAsync(tablesSql);
            Logger.Info("Found {Count} tables in schema {Schema}", tables.Rows.Count, schema);
            
            // Generate DDL for each table
            foreach (DataRow table in tables.Rows)
            {
                var tableName = table["TABNAME"]?.ToString() ?? string.Empty;
                var tableDdl = await GenerateTableDdlAsync(connectionManager, schema, tableName);
                ddl.AppendLine(tableDdl);
                ddl.AppendLine("-- " + new string('-', 80));
                ddl.AppendLine();
            }
            
            Logger.Info("Schema DDL generated for {Count} tables", tables.Rows.Count);
            return ddl.ToString();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate schema DDL for {Schema}", schema);
            throw;
        }
    }
}

