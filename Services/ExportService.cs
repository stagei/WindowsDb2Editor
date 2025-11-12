using System.Data;
using System.IO;
using System.Text;
using NLog;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Service for exporting query results to various formats
/// </summary>
public class ExportService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Export DataTable to CSV file
    /// </summary>
    public async Task ExportToCsvAsync(DataTable dataTable, string filePath)
    {
        Logger.Info($"Exporting {dataTable.Rows.Count} rows to CSV: {filePath}");

        try
        {
            var csv = new StringBuilder();

            // Write header
            var columnNames = dataTable.Columns.Cast<DataColumn>()
                .Select(column => EscapeCsvField(column.ColumnName));
            csv.AppendLine(string.Join(",", columnNames));

            // Write rows
            foreach (DataRow row in dataTable.Rows)
            {
                var fields = row.ItemArray.Select(field => 
                    EscapeCsvField(field?.ToString() ?? string.Empty));
                csv.AppendLine(string.Join(",", fields));
            }

            await File.WriteAllTextAsync(filePath, csv.ToString(), Encoding.UTF8);

            Logger.Info($"Successfully exported {dataTable.Rows.Count} rows to CSV");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to export to CSV");
            throw new Exception($"Failed to export to CSV: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Export DataTable to TSV (Tab-Separated Values) file
    /// </summary>
    public async Task ExportToTsvAsync(DataTable dataTable, string filePath)
    {
        Logger.Info($"Exporting {dataTable.Rows.Count} rows to TSV: {filePath}");

        try
        {
            var tsv = new StringBuilder();

            // Write header
            var columnNames = dataTable.Columns.Cast<DataColumn>()
                .Select(column => column.ColumnName);
            tsv.AppendLine(string.Join("\t", columnNames));

            // Write rows
            foreach (DataRow row in dataTable.Rows)
            {
                var fields = row.ItemArray.Select(field => field?.ToString() ?? string.Empty);
                tsv.AppendLine(string.Join("\t", fields));
            }

            await File.WriteAllTextAsync(filePath, tsv.ToString(), Encoding.UTF8);

            Logger.Info($"Successfully exported {dataTable.Rows.Count} rows to TSV");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to export to TSV");
            throw new Exception($"Failed to export to TSV: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Export DataTable to JSON file
    /// </summary>
    public async Task ExportToJsonAsync(DataTable dataTable, string filePath)
    {
        Logger.Info($"Exporting {dataTable.Rows.Count} rows to JSON: {filePath}");

        try
        {
            var list = new List<Dictionary<string, object?>>();

            foreach (DataRow row in dataTable.Rows)
            {
                var dict = new Dictionary<string, object?>();
                foreach (DataColumn column in dataTable.Columns)
                {
                    dict[column.ColumnName] = row[column];
                }
                list.Add(dict);
            }

            var json = System.Text.Json.JsonSerializer.Serialize(list, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            });

            await File.WriteAllTextAsync(filePath, json, Encoding.UTF8);

            Logger.Info($"Successfully exported {dataTable.Rows.Count} rows to JSON");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to export to JSON");
            throw new Exception($"Failed to export to JSON: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Export DataTable to SQL INSERT statements
    /// </summary>
    public async Task ExportToSqlAsync(DataTable dataTable, string tableName, string filePath)
    {
        Logger.Info($"Exporting {dataTable.Rows.Count} rows to SQL: {filePath}");

        try
        {
            var sql = new StringBuilder();

            foreach (DataRow row in dataTable.Rows)
            {
                var columnNames = string.Join(", ", 
                    dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName));

                var values = string.Join(", ", 
                    row.ItemArray.Select(v => FormatSqlValue(v)));

                sql.AppendLine($"INSERT INTO {tableName} ({columnNames}) VALUES ({values});");
            }

            await File.WriteAllTextAsync(filePath, sql.ToString(), Encoding.UTF8);

            Logger.Info($"Successfully exported {dataTable.Rows.Count} rows to SQL");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to export to SQL");
            throw new Exception($"Failed to export to SQL: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Escape CSV field (handle commas, quotes, newlines)
    /// </summary>
    private string EscapeCsvField(string field)
    {
        if (field.Contains(',') || field.Contains('"') || field.Contains('\n') || field.Contains('\r'))
        {
            return $"\"{field.Replace("\"", "\"\"")}\"";
        }
        return field;
    }

    /// <summary>
    /// Format value for SQL INSERT statement
    /// </summary>
    private string FormatSqlValue(object? value)
    {
        if (value == null || value == DBNull.Value)
        {
            return "NULL";
        }

        if (value is string || value is DateTime)
        {
            return $"'{value.ToString()?.Replace("'", "''")}'";
        }

        return value.ToString() ?? "NULL";
    }
}

