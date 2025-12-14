using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using SqlMermaidErdTools;
using SqlMermaidErdTools.Models;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Integration service that bridges DB2 metadata with SqlMermaidErdTools package.
/// Provides bidirectional SQL ↔ Mermaid conversion, dialect translation, and schema diff.
/// </summary>
public class SqlMermaidIntegrationService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    /// <summary>
    /// Generates SQL DDL CREATE TABLE statements from DB2 table metadata.
    /// This DDL can then be converted to Mermaid using ToMermaid() or translated to other dialects.
    /// </summary>
    public async Task<string> GenerateDdlFromDb2TablesAsync(
        DB2ConnectionManager connectionManager,
        List<string> selectedTables)
    {
        Logger.Info("Generating SQL DDL for {Count} tables", selectedTables.Count);
        Logger.Debug("Selected tables: {Tables}", string.Join(", ", selectedTables));
        
        var ddl = new StringBuilder();
        
        foreach (var fullTableName in selectedTables)
        {
            var parts = fullTableName.Split('.');
            var schema = parts.Length == 2 ? parts[0] : "SYSCAT";
            var tableName = parts.Length == 2 ? parts[1] : fullTableName;
            
            Logger.Debug("Processing table: {Schema}.{Table}", schema, tableName);
            var tableDdl = await GenerateTableDdlAsync(connectionManager, schema, tableName);
            
            if (!string.IsNullOrEmpty(tableDdl))
            {
                ddl.AppendLine(tableDdl);
                ddl.AppendLine();
            }
        }
        
        Logger.Info("DDL generation complete - {Length} characters", ddl.Length);
        return ddl.ToString();
    }
    
    /// <summary>
    /// Generates CREATE TABLE DDL for a single DB2 table.
    /// </summary>
    private async Task<string> GenerateTableDdlAsync(
        DB2ConnectionManager connectionManager,
        string schema,
        string tableName)
    {
        try
        {
            // Get table columns with metadata
            var sql = $@"
                SELECT 
                    c.COLNAME, c.TYPENAME, c.LENGTH, c.SCALE, c.NULLS, c.REMARKS,
                    CASE WHEN k.COLNAME IS NOT NULL THEN 1 ELSE 0 END AS IS_PK
                FROM SYSCAT.COLUMNS c
                LEFT JOIN SYSCAT.KEYCOLUSE k 
                    ON c.TABSCHEMA = k.TABSCHEMA 
                    AND c.TABNAME = k.TABNAME 
                    AND c.COLNAME = k.COLNAME
                WHERE c.TABSCHEMA = '{schema}' 
                  AND c.TABNAME = '{tableName}'
                ORDER BY c.COLNO
            ";
            
            var result = await connectionManager.ExecuteQueryAsync(sql);
            
            if (result.Rows.Count == 0)
            {
                Logger.Warn("No columns found for {Schema}.{Table}", schema, tableName);
                return string.Empty;
            }
            
            var ddl = new StringBuilder();
            ddl.AppendLine($"CREATE TABLE {schema}.{tableName} (");
            
            var columns = new List<string>();
            var pkColumns = new List<string>();
            
            foreach (DataRow row in result.Rows)
            {
                var colName = row["COLNAME"]?.ToString() ?? string.Empty;
                var dataType = FormatDb2DataType(
                    row["TYPENAME"]?.ToString() ?? string.Empty,
                    row["LENGTH"],
                    row["SCALE"]);
                var isNullable = row["NULLS"]?.ToString() == "Y";
                var isPk = Convert.ToInt32(row["IS_PK"]) == 1;
                
                var columnDef = $"    {colName} {dataType}";
                if (!isNullable) columnDef += " NOT NULL";
                
                columns.Add(columnDef);
                
                if (isPk) pkColumns.Add(colName);
            }
            
            ddl.AppendLine(string.Join(",\n", columns));
            
            // Add PRIMARY KEY constraint
            if (pkColumns.Any())
            {
                ddl.AppendLine($",    PRIMARY KEY ({string.Join(", ", pkColumns)})");
            }
            
            ddl.AppendLine(");");
            
            // Get foreign keys
            var fkDdl = await GenerateForeignKeyDdlAsync(connectionManager, schema, tableName);
            if (!string.IsNullOrEmpty(fkDdl))
            {
                ddl.AppendLine();
                ddl.Append(fkDdl);
            }
            
            // Get indexes
            var indexDdl = await GenerateIndexDdlAsync(connectionManager, schema, tableName);
            if (!string.IsNullOrEmpty(indexDdl))
            {
                ddl.AppendLine();
                ddl.Append(indexDdl);
            }
            
            Logger.Debug("Generated DDL for {Schema}.{Table} - {Columns} columns, {PKs} PKs",
                schema, tableName, result.Rows.Count, pkColumns.Count);
            
            return ddl.ToString();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate DDL for {Schema}.{Table}", schema, tableName);
            return string.Empty;
        }
    }
    
    /// <summary>
    /// Generates ALTER TABLE statements for foreign keys.
    /// </summary>
    private async Task<string> GenerateForeignKeyDdlAsync(
        DB2ConnectionManager connectionManager,
        string schema,
        string tableName)
    {
        try
        {
            var sql = $@"
                SELECT 
                    CONSTNAME, FK_COLNAMES, REFTABSCHEMA, REFTABNAME, PK_COLNAMES
                FROM SYSCAT.REFERENCES
                WHERE TABSCHEMA = '{schema}' AND TABNAME = '{tableName}'
            ";
            
            var result = await connectionManager.ExecuteQueryAsync(sql);
            
            if (result.Rows.Count == 0)
                return string.Empty;
            
            var ddl = new StringBuilder();
            
            foreach (DataRow row in result.Rows)
            {
                var fkName = row["CONSTNAME"]?.ToString() ?? string.Empty;
                var fkCols = row["FK_COLNAMES"]?.ToString()?.Replace(" ", "") ?? string.Empty;
                var refSchema = row["REFTABSCHEMA"]?.ToString() ?? string.Empty;
                var refTable = row["REFTABNAME"]?.ToString() ?? string.Empty;
                var refCols = row["PK_COLNAMES"]?.ToString()?.Replace(" ", "") ?? string.Empty;
                
                ddl.AppendLine($"ALTER TABLE {schema}.{tableName}");
                ddl.AppendLine($"    ADD CONSTRAINT {fkName}");
                ddl.AppendLine($"    FOREIGN KEY ({fkCols})");
                ddl.AppendLine($"    REFERENCES {refSchema}.{refTable}({refCols});");
            }
            
            Logger.Debug("Generated {Count} foreign key constraints for {Schema}.{Table}",
                result.Rows.Count, schema, tableName);
            
            return ddl.ToString();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate FK DDL for {Schema}.{Table}", schema, tableName);
            return string.Empty;
        }
    }
    
    /// <summary>
    /// Generates CREATE INDEX statements for a DB2 table.
    /// </summary>
    private async Task<string> GenerateIndexDdlAsync(
        DB2ConnectionManager connectionManager,
        string schema,
        string tableName)
    {
        try
        {
            var sql = $@"
                SELECT 
                    INDNAME, UNIQUERULE, COLNAMES
                FROM SYSCAT.INDEXES
                WHERE TABSCHEMA = '{schema}' 
                  AND TABNAME = '{tableName}'
                  AND UNIQUERULE <> 'P'
                ORDER BY INDNAME
            ";
            
            var result = await connectionManager.ExecuteQueryAsync(sql);
            
            if (result.Rows.Count == 0)
                return string.Empty;
            
            var ddl = new StringBuilder();
            
            foreach (DataRow row in result.Rows)
            {
                var indexName = row["INDNAME"]?.ToString() ?? string.Empty;
                var uniqueRule = row["UNIQUERULE"]?.ToString() ?? string.Empty;
                var colNames = row["COLNAMES"]?.ToString()?.Replace("+", ", ").Replace("-", " DESC, ") ?? string.Empty;
                
                // Clean up column names (remove trailing DESC markers)
                colNames = colNames.TrimEnd(',', ' ', 'D', 'E', 'S', 'C').Trim();
                
                var uniqueClause = uniqueRule == "U" ? "UNIQUE " : "";
                
                ddl.AppendLine($"CREATE {uniqueClause}INDEX {indexName}");
                ddl.AppendLine($"    ON {schema}.{tableName} ({colNames});");
            }
            
            Logger.Debug("Generated {Count} index DDL statements for {Schema}.{Table}",
                result.Rows.Count, schema, tableName);
            
            return ddl.ToString();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate index DDL for {Schema}.{Table}", schema, tableName);
            return string.Empty;
        }
    }
    
    /// <summary>
    /// Converts SQL DDL to Mermaid ERD using SqlMermaidErdTools.
    /// Supports 31+ SQL dialects including DB2.
    /// </summary>
    public async Task<string> ConvertSqlToMermaidAsync(string sqlDdl)
    {
        Logger.Info("Converting SQL DDL to Mermaid ERD");
        Logger.Debug("SQL DDL length: {Length} characters", sqlDdl.Length);
        
        try
        {
            var mermaid = await SqlMermaidErdTools.SqlMermaidErdTools.ToMermaidAsync(sqlDdl);
            Logger.Info("Mermaid ERD generated - {Length} characters", mermaid.Length);
            return mermaid;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to convert SQL to Mermaid");
            throw new InvalidOperationException("Failed to convert SQL to Mermaid ERD. Check SQL syntax.", ex);
        }
    }
    
    /// <summary>
    /// Converts Mermaid ERD to SQL DDL for specified dialect.
    /// Supports: ANSI SQL, SQL Server, PostgreSQL, MySQL.
    /// </summary>
    public async Task<string> ConvertMermaidToSqlAsync(string mermaidErd, SqlDialect targetDialect = SqlDialect.AnsiSql)
    {
        Logger.Info("Converting Mermaid ERD to SQL DDL - Dialect: {Dialect}", targetDialect);
        Logger.Debug("Mermaid ERD length: {Length} characters", mermaidErd.Length);
        
        try
        {
            var sql = await SqlMermaidErdTools.SqlMermaidErdTools.ToSqlAsync(mermaidErd, targetDialect);
            Logger.Info("SQL DDL generated - {Length} characters, Dialect: {Dialect}", sql.Length, targetDialect);
            return sql;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to convert Mermaid to SQL");
            throw new InvalidOperationException($"Failed to convert Mermaid ERD to {targetDialect} SQL. Check Mermaid syntax.", ex);
        }
    }
    
    /// <summary>
    /// Translates SQL from one dialect to another.
    /// Example: DB2 → PostgreSQL, SQL Server → MySQL, etc.
    /// </summary>
    public async Task<string> TranslateSqlDialectAsync(
        string sourceSql,
        SqlDialect sourceDialect,
        SqlDialect targetDialect)
    {
        Logger.Info("Translating SQL: {Source} → {Target}", sourceDialect, targetDialect);
        Logger.Debug("Source SQL length: {Length} characters", sourceSql.Length);
        
        try
        {
            var translated = await SqlMermaidErdTools.SqlMermaidErdTools.TranslateDialectAsync(
                sourceSql,
                sourceDialect,
                targetDialect);
            
            Logger.Info("SQL translation complete - {Length} characters", translated.Length);
            return translated;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to translate SQL from {Source} to {Target}", sourceDialect, targetDialect);
            throw new InvalidOperationException($"Failed to translate SQL from {sourceDialect} to {targetDialect}.", ex);
        }
    }
    
    /// <summary>
    /// Generates ALTER statements from Mermaid diagram changes.
    /// Compares two Mermaid diagrams and produces migration DDL.
    /// </summary>
    public async Task<string> GenerateMigrationFromMermaidDiffAsync(
        string beforeMermaid,
        string afterMermaid,
        SqlDialect targetDialect = SqlDialect.AnsiSql)
    {
        Logger.Info("Generating migration DDL from Mermaid diff - Dialect: {Dialect}", targetDialect);
        Logger.Debug("Before Mermaid: {BeforeLength} chars, After Mermaid: {AfterLength} chars",
            beforeMermaid.Length, afterMermaid.Length);
        
        try
        {
            var alterStatements = await SqlMermaidErdTools.SqlMermaidErdTools.GenerateDiffAlterStatementsAsync(
                beforeMermaid,
                afterMermaid,
                targetDialect);
            
            Logger.Info("Migration DDL generated - {Length} characters", alterStatements.Length);
            return alterStatements;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate migration DDL from Mermaid diff");
            throw new InvalidOperationException("Failed to generate migration DDL. Check Mermaid syntax.", ex);
        }
    }
    
    /// <summary>
    /// Complete workflow: DB2 Tables → SQL DDL → Mermaid ERD.
    /// This is the primary method for generating Mermaid diagrams from live DB2 databases.
    /// </summary>
    public async Task<string> GenerateMermaidFromDb2TablesAsync(
        DB2ConnectionManager connectionManager,
        List<string> selectedTables)
    {
        Logger.Info("Complete workflow: DB2 → DDL → Mermaid for {Count} tables", selectedTables.Count);
        
        // Step 1: Generate DDL from DB2
        var ddl = await GenerateDdlFromDb2TablesAsync(connectionManager, selectedTables);
        
        if (string.IsNullOrWhiteSpace(ddl))
        {
            Logger.Warn("No DDL generated, returning empty Mermaid diagram");
            return "erDiagram\n    -- No tables found";
        }
        
        // Step 2: Convert DDL to Mermaid
        var mermaid = await ConvertSqlToMermaidAsync(ddl);
        
        Logger.Info("Complete workflow finished - Mermaid ERD ready");
        return mermaid;
    }
    
    /// <summary>
    /// Formats DB2 data types to SQL standard format.
    /// </summary>
    private string FormatDb2DataType(string typeName, object? length, object? scale)
    {
        var type = typeName.ToUpperInvariant();
        
        switch (type)
        {
            case "VARCHAR":
            case "CHAR":
            case "CHARACTER":
                return $"{type}({length})";
            
            case "DECIMAL":
            case "NUMERIC":
            case "DEC":
                return $"DECIMAL({length},{scale})";
            
            case "INTEGER":
                return "INT";
            
            case "BIGINT":
            case "SMALLINT":
            case "REAL":
            case "DOUBLE":
            case "FLOAT":
            case "DATE":
            case "TIME":
            case "TIMESTAMP":
            case "BLOB":
            case "CLOB":
                return type;
            
            default:
                return type;
        }
    }
}

