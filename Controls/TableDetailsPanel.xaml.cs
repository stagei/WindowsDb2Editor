using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.Controls;

/// <summary>
/// UserControl for displaying table details - can be embedded in dialogs or tabs
/// </summary>
public partial class TableDetailsPanel : UserControl
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly IConnectionManager _connectionManager;
    private readonly TableRelationshipService _relationshipService;
    private readonly MetadataHandler _metadataHandler;
    private readonly string _fullTableName;
    private readonly string _schema;
    private readonly string _tableName;

    public TableDetailsPanel(IConnectionManager connectionManager, string fullTableName)
    {
        InitializeComponent();
        _connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));
        _relationshipService = new TableRelationshipService();
        _metadataHandler = App.MetadataHandler ?? throw new InvalidOperationException("MetadataHandler not initialized");
        _fullTableName = fullTableName;
        
        // Parse schema and table name
        var parts = fullTableName.Split('.');
        if (parts.Length == 2)
        {
            _schema = parts[0].Trim();
            _tableName = parts[1].Trim();
        }
        else
        {
            _schema = "";
            _tableName = fullTableName.Trim();
        }
        
        Logger.Debug("TableDetailsPanel created for: {Table}", fullTableName);
        
        TableNameText.Text = _tableName;
        TableInfoText.Text = $"Schema: {_schema} • Full Name: {_fullTableName}";
        
        // Apply grid preferences when loaded
        this.Loaded += async (s, e) =>
        {
            if (App.PreferencesService != null)
            {
                GridStyleHelper.ApplyGridStyle(ColumnsGrid, App.PreferencesService.Preferences);
                GridStyleHelper.ApplyGridStyle(ForeignKeysGrid, App.PreferencesService.Preferences);
                GridStyleHelper.ApplyGridStyle(IndexesGrid, App.PreferencesService.Preferences);
                GridStyleHelper.ApplyGridStyle(IncomingFKGrid, App.PreferencesService.Preferences);
                GridStyleHelper.ApplyGridStyle(PackagesGrid, App.PreferencesService.Preferences);
                GridStyleHelper.ApplyGridStyle(ViewsGrid, App.PreferencesService.Preferences);
                GridStyleHelper.ApplyGridStyle(RoutinesGrid, App.PreferencesService.Preferences);
            }
            
            await LoadTableDetailsAsync();
        };
    }
    
    /// <summary>
    /// Activates a specific tab by name
    /// </summary>
    public void ActivateTab(string? tabName)
    {
        if (string.IsNullOrEmpty(tabName))
            return;
            
        Logger.Debug("Activating tab: {Tab}", tabName);
        
        var tab = tabName.ToLowerInvariant() switch
        {
            "columns" => ColumnsTab,
            "foreign-keys" or "foreignkeys" or "fks" => ForeignKeysTab,
            "indexes" => IndexesTab,
            "ddl-script" or "ddlscript" or "ddl" => DdlScriptTab,
            "statistics" or "stats" => StatisticsTab,
            "incoming-fk" or "incomingfk" or "incoming" => IncomingFKTab,
            "packages" or "used-by-packages" => PackagesTab,
            "views" or "used-by-views" => ViewsTab,
            "routines" or "used-by-routines" => RoutinesTab,
            _ => null
        };
        
        if (tab != null)
        {
            DetailsTabControl.SelectedItem = tab;
            Logger.Info("Activated tab: {Tab}", tabName);
        }
    }

    private async Task LoadTableDetailsAsync()
    {
        Logger.Info("Loading details for table: {Table}", _fullTableName);
        
        try
        {
            // Load all details in parallel
            var columnsTask = LoadColumnsAsync();
            var foreignKeysTask = LoadForeignKeysAsync();
            var indexesTask = LoadIndexesAsync();
            var statsTask = LoadStatisticsAsync();
            var ddlTask = GenerateDDLAsync();
            var incomingFKTask = LoadIncomingForeignKeysAsync();
            var packagesTask = LoadReferencingPackagesAsync();
            var viewsTask = LoadReferencingViewsAsync();
            var routinesTask = LoadReferencingRoutinesAsync();
            
            await Task.WhenAll(columnsTask, foreignKeysTask, indexesTask, statsTask, ddlTask, 
                              incomingFKTask, packagesTask, viewsTask, routinesTask);
            
            Logger.Info("Table details loaded successfully");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load table details");
            MessageBox.Show($"Error loading table details:\n\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task LoadColumnsAsync()
    {
        Logger.Debug("Loading columns for: {Table}", _tableName);
        
        try
        {
            var provider = GetProviderFromConnection();
            var version = GetVersionFromConnection();
            var sqlTemplate = _metadataHandler.GetQuery(provider, version, "GetTableColumns_Display");
            var sql = ReplacePlaceholders(sqlTemplate, _schema, _tableName);
            
            var dataTable = await _connectionManager.ExecuteQueryAsync(sql);
            
            Dispatcher.Invoke(() =>
            {
                ColumnsGrid.ItemsSource = dataTable.DefaultView;
                ColumnCountText.Text = dataTable.Rows.Count.ToString();
            });
            
            Logger.Info("Loaded {Count} columns", dataTable.Rows.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load columns");
            Dispatcher.Invoke(() => ColumnCountText.Text = "Error");
        }
    }

    private async Task LoadForeignKeysAsync()
    {
        Logger.Debug("Loading foreign keys for: {Table}", _tableName);
        
        try
        {
            var sqlTemplate = _metadataHandler.GetQuery("DB2", "12.1", "GetTableForeignKeys_Detailed");
            var sql = ReplacePlaceholders(sqlTemplate, _schema, _tableName);
            
            var dataTable = await _connectionManager.ExecuteQueryAsync(sql);
            
            Dispatcher.Invoke(() =>
            {
                ForeignKeysGrid.ItemsSource = dataTable.DefaultView;
                FKCountText.Text = dataTable.Rows.Count.ToString();
            });
            
            Logger.Info("Loaded {Count} foreign keys", dataTable.Rows.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load foreign keys");
            Dispatcher.Invoke(() => FKCountText.Text = "Error");
        }
    }

    private async Task LoadIndexesAsync()
    {
        Logger.Debug("Loading indexes for: {Table}", _tableName);
        
        try
        {
            var provider = GetProviderFromConnection();
            var version = GetVersionFromConnection();
            var sqlTemplate = _metadataHandler.GetQuery(provider, version, "GetTableIndexes_Aggregated");
            var sql = ReplacePlaceholders(sqlTemplate, _schema, _tableName);
            
            var dataTable = await _connectionManager.ExecuteQueryAsync(sql);
            
            Dispatcher.Invoke(() =>
            {
                IndexesGrid.ItemsSource = dataTable.DefaultView;
                IndexCountText.Text = dataTable.Rows.Count.ToString();
            });
            
            Logger.Info("Loaded {Count} indexes", dataTable.Rows.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load indexes");
            Dispatcher.Invoke(() => IndexCountText.Text = "Error");
        }
    }

    private async Task LoadStatisticsAsync()
    {
        Logger.Debug("Loading statistics for: {Table}", _tableName);
        
        try
        {
            // Get row count
            var countSql = $"SELECT COUNT(*) FROM {_fullTableName}";
            var rowCount = await _connectionManager.ExecuteScalarAsync(countSql);
            
            // Get table info
            var infoSqlTemplate = _metadataHandler.GetQuery("DB2", "12.1", "GetTableBasicInfo");
            var infoSql = ReplacePlaceholders(infoSqlTemplate, _schema, _tableName);
            
            var infoTable = await _connectionManager.ExecuteQueryAsync(infoSql);
            
            Dispatcher.Invoke(() =>
            {
                RowCountText.Text = rowCount != null ? Convert.ToInt64(rowCount).ToString("N0") : "Unknown";
                
                if (infoTable.Rows.Count > 0)
                {
                    var type = infoTable.Rows[0]["TableType"]?.ToString() ?? "Unknown";
                    var tbspace = infoTable.Rows[0]["Tablespace"]?.ToString() ?? "Unknown";
                    
                    TableTypeText.Text = type switch
                    {
                        "T" => "Table",
                        "V" => "View",
                        "S" => "System Table",
                        "A" => "Alias",
                        _ => type
                    };
                    
                    TablespaceText.Text = tbspace;
                }
            });
            
            Logger.Info("Statistics loaded - Row count: {Count}", rowCount);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load statistics");
            Dispatcher.Invoke(() => RowCountText.Text = "Error");
        }
    }

    private async Task GenerateDDLAsync()
    {
        Logger.Debug("Generating DDL for: {Table}", _tableName);
        
        try
        {
            // Get columns for DDL
            var provider = GetProviderFromConnection();
            var version = GetVersionFromConnection();
            var columnsSqlTemplate = _metadataHandler.GetQuery(provider, version, "GetTableDdlColumns");
            var columnsSql = ReplacePlaceholders(columnsSqlTemplate, _schema, _tableName);
            
            var columnsTable = await _connectionManager.ExecuteQueryAsync(columnsSql);
            
            // Build DDL
            var ddl = new System.Text.StringBuilder();
            ddl.AppendLine($"-- DDL for table {_fullTableName}");
            ddl.AppendLine();
            ddl.AppendLine($"CREATE TABLE {_fullTableName} (");
            
            for (int i = 0; i < columnsTable.Rows.Count; i++)
            {
                var row = columnsTable.Rows[i];
                var colName = row["COLNAME"].ToString();
                var typeName = row["TYPENAME"].ToString();
                var length = row["LENGTH"].ToString();
                var scale = row["SCALE"].ToString();
                var nulls = row["NULLS"].ToString();
                var defaultVal = row["DEFAULT"]?.ToString();
                
                ddl.Append($"    {colName} {typeName}");
                
                if (typeName == "VARCHAR" || typeName == "CHAR" || typeName == "GRAPHIC" || typeName == "VARGRAPHIC")
                {
                    ddl.Append($"({length})");
                }
                else if (typeName == "DECIMAL" || typeName == "NUMERIC")
                {
                    ddl.Append($"({length},{scale})");
                }
                
                if (nulls == "N")
                {
                    ddl.Append(" NOT NULL");
                }
                
                if (!string.IsNullOrEmpty(defaultVal) && defaultVal != "null")
                {
                    ddl.Append($" DEFAULT {defaultVal}");
                }
                
                if (i < columnsTable.Rows.Count - 1)
                {
                    ddl.AppendLine(",");
                }
                else
                {
                    ddl.AppendLine();
                }
            }
            
            ddl.AppendLine(");");
            
            Dispatcher.Invoke(() =>
            {
                DDLTextBox.Text = ddl.ToString();
            });
            
            Logger.Debug("DDL generated successfully");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate DDL");
            Dispatcher.Invoke(() => DDLTextBox.Text = $"-- Error generating DDL:\n-- {ex.Message}");
        }
    }

    private void ForeignKey_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (ForeignKeysGrid.SelectedItem is DataRowView row)
        {
            var relatedTable = row["PKTable"]?.ToString();
            if (!string.IsNullOrEmpty(relatedTable))
            {
                Logger.Info("Opening related table: {Table}", relatedTable);
                
                // Open the related table in a new tab
                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    var parts = relatedTable.Split('.');
                    var displayName = parts.Length > 1 ? parts[1].Trim() : relatedTable.Trim();
                    mainWindow.CreateTabWithTableDetails(_connectionManager, relatedTable, displayName);
                }
            }
        }
    }

    private void CopyDDL_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Clipboard.SetText(DDLTextBox.Text);
            MessageBox.Show("DDL copied to clipboard!", "Success", 
                MessageBoxButton.OK, MessageBoxImage.Information);
            Logger.Debug("DDL copied to clipboard");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to copy DDL to clipboard");
            MessageBox.Show($"Failed to copy to clipboard:\n\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task LoadIncomingForeignKeysAsync()
    {
        try
        {
            var incomingFKs = await _relationshipService.GetIncomingForeignKeysAsync(_connectionManager, _schema, _tableName);
            Dispatcher.Invoke(() => IncomingFKGrid.ItemsSource = incomingFKs);
            Logger.Info("Loaded {Count} incoming foreign keys", incomingFKs.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load incoming foreign keys");
        }
    }
    
    private async Task LoadReferencingPackagesAsync()
    {
        try
        {
            if (_connectionManager is not DB2ConnectionManager db2Conn) throw new InvalidOperationException("TableDetailsPanel requires DB2ConnectionManager");
            var packages = await _relationshipService.GetReferencingPackagesAsync(db2Conn, _schema, _tableName);
            Dispatcher.Invoke(() => PackagesGrid.ItemsSource = packages);
            Logger.Info("Loaded {Count} referencing packages", packages.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load referencing packages");
        }
    }
    
    private async Task LoadReferencingViewsAsync()
    {
        try
        {
            if (_connectionManager is not DB2ConnectionManager db2Conn2) throw new InvalidOperationException("TableDetailsPanel requires DB2ConnectionManager");
            var views = await _relationshipService.GetReferencingViewsAsync(db2Conn2, _schema, _tableName);
            Dispatcher.Invoke(() => ViewsGrid.ItemsSource = views);
            Logger.Info("Loaded {Count} referencing views", views.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load referencing views");
        }
    }
    
    private async Task LoadReferencingRoutinesAsync()
    {
        try
        {
            var routines = await _relationshipService.GetReferencingRoutinesAsync(_connectionManager, _schema, _tableName);
            Dispatcher.Invoke(() => RoutinesGrid.ItemsSource = routines);
            Logger.Info("Loaded {Count} referencing routines", routines.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load referencing routines");
        }
    }
    
    /// <summary>
    /// Helper method to replace ? placeholders in SQL templates with actual values
    /// </summary>
    private string ReplacePlaceholders(string sqlTemplate, params string[] values)
    {
        var result = sqlTemplate;
        foreach (var value in values)
        {
            var index = result.IndexOf('?');
            if (index >= 0)
            {
                result = result.Remove(index, 1).Insert(index, $"'{value}'");
            }
        }
        return result;
    }
    
    /// <summary>
    /// Get provider type from connection (defaults to "DB2" for backward compatibility)
    /// </summary>
    private string GetProviderFromConnection()
    {
        var provider = _connectionManager.ConnectionInfo.ProviderType?.ToUpperInvariant() ?? "DB2";
        return provider switch
        {
            "POSTGRESQL" or "POSTGRES" => "POSTGRESQL",
            "SQLSERVER" or "MSSQL" => "SQLSERVER",
            "ORACLE" => "ORACLE",
            "MYSQL" => "MYSQL",
            _ => "DB2" // Default to DB2
        };
    }
    
    /// <summary>
    /// Get version from connection (defaults to "12.1" for DB2, can be enhanced with version detection)
    /// </summary>
    private string GetVersionFromConnection()
    {
        // TODO: Implement version detection from connection
        // For now, default to 12.1 for DB2, or provider-specific defaults
        var provider = GetProviderFromConnection();
        return provider switch
        {
            "POSTGRESQL" => "15",
            "SQLSERVER" => "2022",
            "ORACLE" => "21",
            "MYSQL" => "8.0",
            _ => "12.1" // DB2 default
        };
    }
}
