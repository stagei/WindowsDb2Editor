using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Services;

/// <summary>
/// REFACTORED: Now uses SqlMermaidErdTools for Mermaid generation instead of custom implementation.
/// DB2 metadata extraction is preserved, but Mermaid generation uses SqlMermaidErdTools.ToMermaid().
/// </summary>
public class MermaidDiagramGeneratorService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly SqlMermaidIntegrationService _sqlMermaidService;
    
    public MermaidDiagramGeneratorService()
    {
        _sqlMermaidService = new SqlMermaidIntegrationService();
    }
    
    /// <summary>
    /// REFACTORED: Now uses SqlMermaidErdTools for superior Mermaid generation.
    /// Process: Database metadata → SQL DDL → SqlMermaidErdTools.ToMermaid() → Mermaid ERD
    /// </summary>
    public async Task<string> GenerateMermaidDiagramAsync(
        IConnectionManager connectionManager,
        List<string> selectedTables)
    {
        Logger.Info("Generating Mermaid diagram for {Count} tables using SqlMermaidErdTools", selectedTables.Count);
        
        try
        {
            // Use SqlMermaidIntegrationService which uses SqlMermaidErdTools
            var mermaid = await _sqlMermaidService.GenerateMermaidFromDb2TablesAsync(
                connectionManager,
                selectedTables);
            
            Logger.Info("Mermaid diagram generated successfully via SqlMermaidErdTools");
            return mermaid;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate Mermaid via SqlMermaidErdTools, falling back to legacy method");
            
            // Fallback to legacy method if SqlMermaidErdTools fails (DB2-specific)
            if (connectionManager is not DB2ConnectionManager db2Conn)
                throw new InvalidOperationException("MermaidDiagramGeneratorService requires DB2ConnectionManager");
            return await GenerateMermaidDiagramLegacyAsync(db2Conn, selectedTables);
        }
    }
    
    /// <summary>
    /// LEGACY METHOD: Preserved as fallback in case SqlMermaidErdTools fails.
    /// Uses old custom Mermaid generation logic.
    /// </summary>
    private async Task<string> GenerateMermaidDiagramLegacyAsync(
        DB2ConnectionManager connectionManager,
        List<string> selectedTables)
    {
        Logger.Warn("Using legacy Mermaid generation method (fallback)");
        
        var tables = new List<MermaidTable>();
        
        foreach (var fullTableName in selectedTables)
        {
            var parts = fullTableName.Split('.');
            var schema = parts.Length == 2 ? parts[0] : "SYSCAT";
            var tableName = parts.Length == 2 ? parts[1] : fullTableName;
            
            var table = await GetTableStructureAsync(connectionManager, schema, tableName);
            if (table != null)
            {
                tables.Add(table);
            }
        }
        
        var relationships = await GetRelationshipsAsync(connectionManager, tables);
        
        return BuildMermaidERDiagramLegacy(tables, relationships);
    }
    
    private async Task<MermaidTable?> GetTableStructureAsync(
        DB2ConnectionManager connectionManager,
        string schema,
        string tableName)
    {
        try
        {
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
            
            var table = new MermaidTable
            {
                Schema = schema,
                Name = tableName
            };
            
            foreach (DataRow row in result.Rows)
            {
                var column = new MermaidColumn
                {
                    Name = row["COLNAME"]?.ToString() ?? string.Empty,
                    DataType = FormatDataType(
                        row["TYPENAME"]?.ToString() ?? string.Empty,
                        row["LENGTH"],
                        row["SCALE"]),
                    IsPrimaryKey = Convert.ToInt32(row["IS_PK"]) == 1,
                    IsNullable = row["NULLS"]?.ToString() == "Y",
                    Comment = row["REMARKS"]?.ToString()
                };
                
                table.Columns.Add(column);
            }
            
            Logger.Debug("Loaded structure for {Schema}.{Table} - {Count} columns", schema, tableName, table.Columns.Count);
            return table;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get structure for {Schema}.{Table}", schema, tableName);
            return null;
        }
    }
    
    private async Task<List<MermaidRelationship>> GetRelationshipsAsync(
        DB2ConnectionManager connectionManager,
        List<MermaidTable> tables)
    {
        var relationships = new List<MermaidRelationship>();
        var schemas = tables.Select(t => t.Schema).Distinct().ToList();
        var schemaFilter = string.Join("','", schemas);
        
        try
        {
            var sql = $@"
                SELECT 
                    TABSCHEMA, TABNAME,
                    REFTABSCHEMA, REFTABNAME,
                    CONSTNAME, FK_COLNAMES
                FROM SYSCAT.REFERENCES
                WHERE TABSCHEMA IN ('{schemaFilter}')
                  AND REFTABSCHEMA IN ('{schemaFilter}')
            ";
            
            var result = await connectionManager.ExecuteQueryAsync(sql);
            
            foreach (DataRow row in result.Rows)
            {
                var fromTable = row["TABNAME"]?.ToString() ?? string.Empty;
                var toTable = row["REFTABNAME"]?.ToString() ?? string.Empty;
                
                if (tables.Any(t => t.Name == fromTable) && tables.Any(t => t.Name == toTable))
                {
                    relationships.Add(new MermaidRelationship
                    {
                        FromTable = fromTable,
                        ToTable = toTable,
                        Cardinality = "||--o{",
                        Label = row["CONSTNAME"]?.ToString()
                    });
                }
            }
            
            Logger.Info("Found {Count} relationships", relationships.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get relationships");
        }
        
        return relationships;
    }
    
    /// <summary>
    /// LEGACY: Old custom Mermaid generation logic. Kept as fallback only.
    /// Primary method now uses SqlMermaidErdTools for better quality and compatibility.
    /// </summary>
    private string BuildMermaidERDiagramLegacy(List<MermaidTable> tables, List<MermaidRelationship> relationships)
    {
        Logger.Debug("Using legacy Mermaid ERD builder");
        
        var mermaid = new StringBuilder();
        mermaid.AppendLine("erDiagram");
        mermaid.AppendLine();
        
        foreach (var rel in relationships)
        {
            var line = $"    {rel.FromTable} {rel.Cardinality} {rel.ToTable}";
            if (!string.IsNullOrEmpty(rel.Label))
                line += $" : \"{rel.Label}\"";
            mermaid.AppendLine(line);
        }
        
        if (relationships.Any())
            mermaid.AppendLine();
        
        foreach (var table in tables)
        {
            mermaid.AppendLine($"    {table.Name} {{");
            
            foreach (var col in table.Columns)
            {
                var marker = col.IsPrimaryKey ? "PK" : col.IsForeignKey ? "FK" : "";
                var comment = !string.IsNullOrEmpty(col.Comment) ? $"\"{col.Comment}\"" : "";
                mermaid.AppendLine($"        {col.DataType} {col.Name} {marker} {comment}".TrimEnd());
            }
            
            mermaid.AppendLine("    }");
        }
        
        return mermaid.ToString();
    }
    
    private string FormatDataType(string typeName, object? length, object? scale)
    {
        var type = typeName.ToUpperInvariant();
        
        if (type == "VARCHAR" || type == "CHAR")
        {
            return $"{type}({length})";
        }
        else if (type == "DECIMAL" || type == "NUMERIC")
        {
            return $"{type}({length},{scale})";
        }
        
        return type;
    }
    
    public Dictionary<string, MermaidTable> ParseMermaidDiagram(string mermaidCode)
    {
        var tables = new Dictionary<string, MermaidTable>();
        var lines = mermaidCode.Split('\n');
        MermaidTable? currentTable = null;
        
        foreach (var line in lines)
        {
            var trimmed = line.Trim();
            
            if (Regex.IsMatch(trimmed, @"^(\w+)\s*\{"))
            {
                var match = Regex.Match(trimmed, @"^(\w+)\s*\{");
                currentTable = new MermaidTable { Name = match.Groups[1].Value };
                tables[currentTable.Name] = currentTable;
            }
            else if (trimmed == "}")
            {
                currentTable = null;
            }
            else if (currentTable != null && !string.IsNullOrWhiteSpace(trimmed))
            {
                var columnMatch = Regex.Match(trimmed, @"^(\S+)\s+(\w+)\s*(PK|FK)?\s*""?([^""]*)""?");
                if (columnMatch.Success)
                {
                    currentTable.Columns.Add(new MermaidColumn
                    {
                        DataType = columnMatch.Groups[1].Value,
                        Name = columnMatch.Groups[2].Value,
                        IsPrimaryKey = columnMatch.Groups[3].Value == "PK",
                        IsForeignKey = columnMatch.Groups[3].Value == "FK",
                        Comment = columnMatch.Groups[4].Value
                    });
                }
            }
        }
        
        return tables;
    }
}

