using System;
using System.Linq;
using System.Text;
using NLog;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Services;

public class DiffBasedDdlGeneratorService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    public string GenerateMigrationScripts(SchemaDiff diff, string targetSchema)
    {
        Logger.Info("Generating migration scripts for {Count} changed tables", diff.TableChanges.Count);
        
        var ddl = new StringBuilder();
        ddl.AppendLine("-- Migration Script Generated from Mermaid Diagram");
        ddl.AppendLine($"-- Generated: {DateTime.Now}");
        ddl.AppendLine($"-- Target Schema: {targetSchema}");
        ddl.AppendLine("-- WARNING: Review carefully before executing!");
        ddl.AppendLine();
        
        foreach (var table in diff.TableChanges.Where(t => t.ChangeType == TableChangeType.Added))
        {
            ddl.AppendLine($"-- Create new table: {targetSchema}.{table.TableName}");
            ddl.AppendLine($"CREATE TABLE {targetSchema}.{table.TableName} (");
            
            var columnDefs = table.AddedColumns.Select(c =>
            {
                var def = $"    {c.ColumnName} {c.NewDataType}";
                if (c.NewNullable == false) def += " NOT NULL";
                return def;
            });
            
            ddl.AppendLine(string.Join(",\n", columnDefs));
            ddl.AppendLine(");");
            ddl.AppendLine();
            
            var pkCols = table.AddedColumns.Where(c => c.IsPrimaryKeyChange).ToList();
            if (pkCols.Any())
            {
                ddl.AppendLine($"ALTER TABLE {targetSchema}.{table.TableName}");
                ddl.AppendLine($"  ADD PRIMARY KEY ({string.Join(", ", pkCols.Select(c => c.ColumnName))});");
                ddl.AppendLine();
            }
        }
        
        foreach (var table in diff.TableChanges.Where(t => t.ChangeType == TableChangeType.Modified))
        {
            if (table.AddedColumns.Any())
            {
                foreach (var col in table.AddedColumns)
                {
                    ddl.AppendLine($"ALTER TABLE {targetSchema}.{table.TableName}");
                    ddl.AppendLine($"  ADD COLUMN {col.ColumnName} {col.NewDataType}");
                    if (col.NewNullable == false) ddl.Append(" NOT NULL");
                    ddl.AppendLine(";");
                    ddl.AppendLine();
                }
            }
            
            if (table.ModifiedColumns.Any())
            {
                foreach (var col in table.ModifiedColumns)
                {
                    ddl.AppendLine($"-- Modify column: {table.TableName}.{col.ColumnName}");
                    if (col.OldDataType != col.NewDataType)
                    {
                        ddl.AppendLine($"ALTER TABLE {targetSchema}.{table.TableName}");
                        ddl.AppendLine($"  ALTER COLUMN {col.ColumnName} SET DATA TYPE {col.NewDataType};");
                    }
                    if (col.OldNullable != col.NewNullable)
                    {
                        ddl.AppendLine($"ALTER TABLE {targetSchema}.{table.TableName}");
                        ddl.AppendLine($"  ALTER COLUMN {col.ColumnName} {(col.NewNullable == false ? "SET NOT NULL" : "DROP NOT NULL")};");
                    }
                    ddl.AppendLine();
                }
            }
            
            if (table.RemovedColumns.Any())
            {
                foreach (var col in table.RemovedColumns)
                {
                    ddl.AppendLine($"-- WARNING: Dropping column will DELETE DATA!");
                    ddl.AppendLine($"-- ALTER TABLE {targetSchema}.{table.TableName} DROP COLUMN {col.ColumnName};");
                    ddl.AppendLine();
                }
            }
        }
        
        foreach (var table in diff.TableChanges.Where(t => t.ChangeType == TableChangeType.Removed))
        {
            ddl.AppendLine($"-- WARNING: Dropping table will DELETE ALL DATA!");
            ddl.AppendLine($"-- DROP TABLE {targetSchema}.{table.TableName};");
            ddl.AppendLine();
        }
        
        return ddl.ToString();
    }
}

