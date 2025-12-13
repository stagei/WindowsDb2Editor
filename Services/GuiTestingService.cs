using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Dialogs;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Service for automated GUI form testing
/// Opens dialogs programmatically and extracts their data for validation
/// </summary>
public class GuiTestingService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly MetadataHandler _metadataHandler;
    
    public GuiTestingService(MetadataHandler metadataHandler)
    {
        _metadataHandler = metadataHandler;
        Logger.Debug("GuiTestingService initialized");
    }
    
    /// <summary>
    /// Open a dialog, extract its data, and return as dictionary
    /// </summary>
    public async Task<Dictionary<string, object>> TestFormAsync(
        DB2ConnectionManager connectionManager,
        string formName,
        string objectName,
        string? tabName = null)
    {
        Logger.Info("Testing form: {FormName}, Object: {ObjectName}, Tab: {Tab}", formName, objectName, tabName);
        
        try
        {
            return formName.ToLowerInvariant() switch
            {
                "table-details" => await TestTableDetailsAsync(connectionManager, objectName, tabName),
                "object-details" => await TestObjectDetailsAsync(connectionManager, objectName, tabName),
                "package-details" => await TestPackageDetailsAsync(connectionManager, objectName, tabName),
                "user-details" => await TestUserDetailsAsync(connectionManager, objectName, tabName),
                _ => throw new ArgumentException($"Unknown form name: {formName}")
            };
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to test form: {FormName}", formName);
            return new Dictionary<string, object>
            {
                {"error", ex.Message},
                {"formName", formName},
                {"objectName", objectName},
                {"tabName", tabName ?? "all"}
            };
        }
    }
    
    private async Task<Dictionary<string, object>> TestTableDetailsAsync(
        DB2ConnectionManager connectionManager,
        string objectName,
        string? tabName)
    {
        Logger.Debug("Testing TableDetailsDialog for: {Object}", objectName);
        
        // Parse schema.table
        var parts = objectName.Split('.');
        if (parts.Length != 2)
        {
            throw new ArgumentException($"Object must be in format SCHEMA.TABLE, got: {objectName}");
        }
        
        var schema = parts[0];
        var tableName = parts[1];
        
        // Create dialog on UI thread and extract data
        Dictionary<string, object>? result = null;
        Exception? exception = null;
        
        var tcs = new TaskCompletionSource<bool>();
        
        Application.Current.Dispatcher.Invoke(async () =>
        {
            try
            {
                Logger.Debug("Creating TableDetailsDialog on UI thread");
                var dialog = new TableDetailsDialog(connectionManager, objectName);
                
                Logger.Debug("Dialog created, waiting for async data loading...");
                await Task.Delay(3000); // Give time for async loading
                
                Logger.Debug("Extracting data from dialog");
                result = ExtractTableDetailsData(dialog, tabName);
                
                Logger.Debug("Data extracted, result is null: {IsNull}", result == null);
                
                dialog.Close();
                Logger.Debug("Dialog closed");
                
                tcs.SetResult(true);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Exception during dialog extraction");
                exception = ex;
                tcs.SetResult(false);
            }
        });
        
        await tcs.Task;
        
        if (exception != null)
        {
            return new Dictionary<string, object> { {"error", exception.Message}, {"stackTrace", exception.StackTrace ?? ""} };
        }
        
        Logger.Debug("Returning extracted result");
        return result ?? new Dictionary<string, object> { {"error", "Failed to extract data"} };
    }
    
    private Dictionary<string, object> ExtractTableDetailsData(TableDetailsDialog dialog, string? tabName)
    {
        try
        {
            Logger.Debug("ExtractTableDetailsData called, Tab: {Tab}", tabName ?? "all");
            
            var data = new Dictionary<string, object>
            {
                {"formName", "TableDetailsDialog"},
                {"fullTableName", dialog.Title}, // Dialog title shows table name
                {"extractedAt", DateTime.Now}
            };
            
            Logger.Debug("Base data created, extracting tabs...");
        
        
        // If specific tab requested, extract only that tab
        // Otherwise extract all tabs
        if (string.IsNullOrEmpty(tabName) || tabName == "all")
        {
            data["tabs"] = new Dictionary<string, object>
            {
                {"columns", ExtractDataGridData(dialog.ColumnsGridPublic)},
                {"foreignKeys", ExtractDataGridData(dialog.ForeignKeysGridPublic)},
                {"indexes", ExtractDataGridData(dialog.IndexesGridPublic)},
                {"ddlScript", ExtractTextBoxData(dialog.DDLTextBoxPublic)},
                {"statistics", ExtractStatisticsData(dialog)},
                {"incomingFK", ExtractDataGridData(dialog.IncomingFKGridPublic)},
                {"usedByPackages", ExtractDataGridData(dialog.PackagesGridPublic)},
                {"usedByViews", ExtractDataGridData(dialog.ViewsGridPublic)},
                {"usedByRoutines", ExtractDataGridData(dialog.RoutinesGridPublic)}
            };
        }
        else
        {
            // Extract specific tab
            data["tab"] = tabName;
            data["data"] = tabName switch
            {
                "columns" => ExtractDataGridData(dialog.ColumnsGridPublic),
                "foreign-keys" => ExtractDataGridData(dialog.ForeignKeysGridPublic),
                "indexes" => ExtractDataGridData(dialog.IndexesGridPublic),
                "ddl-script" => ExtractTextBoxData(dialog.DDLTextBoxPublic),
                "statistics" => ExtractStatisticsData(dialog),
                "incoming-fk" => ExtractDataGridData(dialog.IncomingFKGridPublic),
                "used-by-packages" => ExtractDataGridData(dialog.PackagesGridPublic),
                "used-by-views" => ExtractDataGridData(dialog.ViewsGridPublic),
                "used-by-routines" => ExtractDataGridData(dialog.RoutinesGridPublic),
                _ => new { error = $"Unknown tab: {tabName}" }
            };
        }
            
        Logger.Debug("Extraction complete, returning data");
        return data;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to extract TableDetailsDialog data");
            return new Dictionary<string, object>
            {
                {"error", ex.Message},
                {"stackTrace", ex.StackTrace ?? ""}
            };
        }
    }
    
    private object ExtractDataGridData(System.Windows.Controls.DataGrid grid)
    {
        try
        {
            if (grid == null || grid.ItemsSource == null)
            {
                return new { rowCount = 0, rows = Array.Empty<object>() };
            }
            
            var rows = new List<Dictionary<string, object>>();
            
            foreach (var item in grid.ItemsSource)
            {
                if (item is System.Data.DataRowView rowView)
                {
                    var row = new Dictionary<string, object>();
                    foreach (System.Data.DataColumn col in rowView.Row.Table.Columns)
                    {
                        row[col.ColumnName] = rowView[col.ColumnName]?.ToString() ?? "";
                    }
                    rows.Add(row);
                }
            }
            
            return new { rowCount = rows.Count, rows };
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to extract DataGrid data");
            return new { error = ex.Message };
        }
    }
    
    private object ExtractTextBoxData(System.Windows.Controls.TextBox textBox)
    {
        try
        {
            if (textBox == null)
            {
                return new { text = "", length = 0 };
            }
            
            var text = textBox.Text ?? "";
            return new { text, length = text.Length, lineCount = text.Split('\n').Length };
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to extract TextBox data");
            return new { error = ex.Message };
        }
    }
    
    private object ExtractStatisticsData(TableDetailsDialog dialog)
    {
        try
        {
            return new
            {
                rowCount = dialog.RowCountTextPublic.Text,
                columnCount = dialog.ColumnCountTextPublic.Text,
                fkCount = dialog.FKCountTextPublic.Text,
                indexCount = dialog.IndexCountTextPublic.Text,
                tableType = dialog.TableTypeTextPublic.Text,
                tablespace = dialog.TablespaceTextPublic.Text
            };
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to extract statistics data");
            return new { error = ex.Message };
        }
    }
    
    private async Task<Dictionary<string, object>> TestObjectDetailsAsync(
        DB2ConnectionManager connectionManager,
        string objectName,
        string? tabName)
    {
        // TODO: Implement for ObjectDetailsDialog
        Logger.Warn("ObjectDetailsDialog testing not yet implemented");
        return new Dictionary<string, object>
        {
            {"error", "Not yet implemented"},
            {"formName", "ObjectDetailsDialog"},
            {"objectName", objectName}
        };
    }
    
    private async Task<Dictionary<string, object>> TestPackageDetailsAsync(
        DB2ConnectionManager connectionManager,
        string objectName,
        string? tabName)
    {
        // TODO: Implement for PackageDetailsDialog
        Logger.Warn("PackageDetailsDialog testing not yet implemented");
        return new Dictionary<string, object>
        {
            {"error", "Not yet implemented"},
            {"formName", "PackageDetailsDialog"},
            {"objectName", objectName}
        };
    }
    
    private async Task<Dictionary<string, object>> TestUserDetailsAsync(
        DB2ConnectionManager connectionManager,
        string objectName,
        string? tabName)
    {
        // TODO: Implement for UserDetailsDialog
        Logger.Warn("UserDetailsDialog testing not yet implemented");
        return new Dictionary<string, object>
        {
            {"error", "Not yet implemented"},
            {"formName", "UserDetailsDialog"},
            {"objectName", objectName}
        };
    }
}

