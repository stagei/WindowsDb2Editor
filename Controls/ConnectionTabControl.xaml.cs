using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Dialogs;
using WindowsDb2Editor.Models;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.Controls;

public partial class ConnectionTabControl : UserControl
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly DB2ConnectionManager _connectionManager;
    private readonly DB2Connection _connection;
    private readonly SqlFormatterService _formatterService;
    private readonly QueryHistoryService _queryHistoryService;
    private readonly ExportService _exportService;
    private readonly PreferencesService _preferencesService;
    
    // Pagination state
    private string _lastExecutedSql = string.Empty;
    private int _currentPage = 1;
    private int _currentOffset = 0;
    
    // Cell copy state (Issue #1 fix)
    private DataGridCellInfo? _lastClickedCell;
    private System.Windows.Point _lastRightClickPosition;

    /// <summary>
    /// Public property to access the connection manager for monitoring and management
    /// </summary>
    public DB2ConnectionManager ConnectionManager => _connectionManager;

    public ConnectionTabControl(DB2Connection connection)
    {
        InitializeComponent();
        Logger.Debug($"ConnectionTabControl initializing for {connection.GetDisplayName()}");

        _connection = connection;
        _connectionManager = new DB2ConnectionManager(connection);
        _formatterService = new SqlFormatterService();
        _queryHistoryService = new QueryHistoryService();
        _exportService = new ExportService();
        _preferencesService = new PreferencesService();

        InitializeSqlEditor();
        RegisterKeyboardShortcuts();
        RegisterResultsGridEvents();
        _ = ConnectToDatabase();
        
        Logger.Debug($"Pagination enabled with max rows: {_preferencesService.Preferences.MaxRowsPerQuery}");
    }
    
    /// <summary>
    /// Register results grid event handlers (Issue #1 fix)
    /// </summary>
    private void RegisterResultsGridEvents()
    {
        Logger.Debug("Registering ResultsGrid event handlers");
        
        // Capture cell info on right-click before context menu opens
        ResultsGrid.PreviewMouseRightButtonDown += ResultsGrid_PreviewMouseRightButtonDown;
    }
    
    /// <summary>
    /// Capture clicked cell before context menu opens (Issue #1 fix)
    /// </summary>
    private void ResultsGrid_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
        Logger.Debug("Right-click detected on results grid");
        
        try
        {
            // Get the clicked element
            var dep = (DependencyObject)e.OriginalSource;
            
            // Walk up the visual tree to find DataGridCell
            while (dep != null && !(dep is DataGridCell))
            {
                dep = System.Windows.Media.VisualTreeHelper.GetParent(dep);
            }
            
            if (dep is DataGridCell cell)
            {
                _lastClickedCell = new DataGridCellInfo(cell);
                _lastRightClickPosition = e.GetPosition(ResultsGrid);
                
                var rowIndex = ResultsGrid.Items.IndexOf(cell.DataContext);
                Logger.Debug("Captured cell info - Column: {Column}, Row: {Row}", 
                    cell.Column?.Header, rowIndex);
            }
            else
            {
                Logger.Debug("Right-click not on a DataGridCell");
                _lastClickedCell = null;
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error capturing cell info on right-click");
            _lastClickedCell = null;
        }
    }

    private void RegisterKeyboardShortcuts()
    {
        Logger.Debug("Registering editor keyboard shortcuts");

        SqlEditor.PreviewKeyDown += (s, e) =>
        {
            // F5 - Execute Query
            if (e.Key == Key.F5)
            {
                e.Handled = true;
                _ = ExecuteQuery();
            }
            // Ctrl+Enter - Execute Current Statement
            else if (e.Key == Key.Enter && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                e.Handled = true;
                _ = ExecuteCurrentStatement();
            }
            // Ctrl+Shift+F - Format SQL
            else if (e.Key == Key.F && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control 
                     && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                e.Handled = true;
                FormatSql();
            }
            // Ctrl+S - Save Script
            else if (e.Key == Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                e.Handled = true;
                SaveScript();
            }
            // Ctrl+O - Open Script
            else if (e.Key == Key.O && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                e.Handled = true;
                OpenScript();
            }
            // Ctrl+Shift+C - Commit Transaction
            else if (e.Key == Key.C && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                e.Handled = true;
                _ = CommitTransaction();
            }
            // Ctrl+Shift+R - Rollback Transaction
            else if (e.Key == Key.R && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                e.Handled = true;
                _ = RollbackTransaction();
            }
        };
    }

    private void InitializeSqlEditor()
    {
        Logger.Debug("Initializing SQL Editor");

        try
        {
            // Load custom DB2 SQL syntax highlighting
            var xshdPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "DB2SQL.xshd");
            
            if (File.Exists(xshdPath))
            {
                using var reader = new XmlTextReader(xshdPath);
                var definition = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                SqlEditor.SyntaxHighlighting = definition;
                Logger.Debug("DB2 SQL syntax highlighting loaded");
            }
            else
            {
                Logger.Warn($"Syntax highlighting file not found: {xshdPath}");
                // Use SQL as fallback
                SqlEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("SQL");
            }

            // Set editor options
            SqlEditor.Options.EnableHyperlinks = false;
            SqlEditor.Options.EnableEmailHyperlinks = false;
            SqlEditor.Options.ShowTabs = true;
            SqlEditor.Options.ShowSpaces = false;

            // Set initial SQL
            SqlEditor.Text = "-- Enter your SQL query here\nSELECT * FROM YOUR_TABLE;";

            Logger.Info("SQL Editor initialized successfully");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to initialize SQL Editor");
        }
    }

    private async Task ConnectToDatabase()
    {
        Logger.Info($"Connecting to database: {_connection.GetDisplayName()}");
        StatusText.Text = "Connecting...";

        try
        {
            await _connectionManager.OpenAsync();
            StatusText.Text = $"Connected to {_connection.GetDisplayName()}";
            Logger.Info("Database connection established");

            // RBAC: Update access level indicator
            UpdateAccessLevelIndicator();
            
            // Feature #2: Update commit/rollback button visibility
            UpdateTransactionButtonsVisibility();

            // Load database objects
            await LoadDatabaseObjectsAsync();
            
            // Load query history for this connection
            RefreshQueryHistory();
            
            // Feature #5: Background metadata collection (non-blocking)
            _ = Task.Run(async () =>
            {
                try
                {
                    Logger.Info("Starting background metadata collection");
                    var metadataService = new DB2MetadataService();
                    await metadataService.CollectMetadataAsync(_connectionManager, _connection.Name ?? _connection.GetDisplayName());
                    Logger.Info("Background metadata collection completed");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "Background metadata collection failed");
                    // Don't show error to user - non-critical background task
                }
            });
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to connect to database");
            StatusText.Text = "Connection failed";
            MessageBox.Show($"Failed to connect: {ex.Message}", "Connection Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task LoadDatabaseObjectsAsync()
    {
        Logger.Debug("Loading database objects into TreeView");

        try
        {
            DatabaseTreeView.Items.Clear();
            
            // Add a loading indicator
            var loadingNode = new TreeViewItem
            {
                Header = "⏳ Loading schemas...",
                IsEnabled = false
            };
            DatabaseTreeView.Items.Add(loadingNode);
            
            var schemas = await _connectionManager.GetSchemasAsync();
            Logger.Info($"Loaded {schemas.Count} schemas");

            DatabaseTreeView.Items.Clear();

            if (schemas.Count == 0)
            {
                var noSchemaNode = new TreeViewItem
                {
                    Header = "⚠ No schemas found or insufficient permissions",
                    IsEnabled = false
                };
                DatabaseTreeView.Items.Add(noSchemaNode);
                Logger.Warn("No schemas returned from database");
                return;
            }

            foreach (var schema in schemas.Take(20)) // Limit to first 20 schemas for performance
            {
                var schemaNode = new TreeViewItem
                {
                    Header = $"📁 {schema}",
                    Tag = schema
                };

                // Add a placeholder to make it expandable
                schemaNode.Items.Add("Loading...");
                schemaNode.Expanded += SchemaNode_Expanded;

                DatabaseTreeView.Items.Add(schemaNode);
            }

            Logger.Debug($"Added {DatabaseTreeView.Items.Count} schemas to TreeView");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load database objects");
            
            DatabaseTreeView.Items.Clear();
            var errorNode = new TreeViewItem
            {
                Header = $"❌ Error loading schemas: {ex.Message}",
                IsEnabled = false
            };
            DatabaseTreeView.Items.Add(errorNode);
            
            StatusText.Text = "Failed to load database objects";
        }
    }

    private async void SchemaNode_Expanded(object sender, RoutedEventArgs e)
    {
        if (sender is not TreeViewItem schemaNode) return;
        if (schemaNode.Tag is not string schema) return;

        // Check if already loaded
        if (schemaNode.Items.Count == 1 && schemaNode.Items[0] is string placeholder && placeholder == "Loading...")
        {
            Logger.Debug($"Expanding schema: {schema}");
            schemaNode.Items.Clear();

            try
            {
                var tables = await _connectionManager.GetTablesAsync(schema);
                Logger.Debug($"Found {tables.Count} tables in schema {schema}");

                foreach (var table in tables)
                {
                    var tableNode = new TreeViewItem
                    {
                        Header = $"📄 {table}",
                        Tag = $"{schema}.{table}"
                    };

                    tableNode.MouseDoubleClick += TableNode_DoubleClick;
                    tableNode.ContextMenu = CreateTableContextMenu($"{schema}.{table}");
                    schemaNode.Items.Add(tableNode);
                }

                if (tables.Count == 0)
                {
                    schemaNode.Items.Add(new TreeViewItem { Header = "(No tables)", IsEnabled = false });
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to load tables for schema {schema}");
                schemaNode.Items.Add(new TreeViewItem { Header = "(Error loading tables)", IsEnabled = false });
            }
        }
    }

    private void TableNode_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (sender is TreeViewItem tableNode && tableNode.Tag is string fullTableName)
        {
            Logger.Debug($"Table double-clicked: {fullTableName}");
            SqlEditor.AppendText($"SELECT * FROM {fullTableName};\n");
        }
    }

    /// <summary>
    /// Create context menu for table nodes with various options
    /// </summary>
    private ContextMenu CreateTableContextMenu(string fullTableName)
    {
        Logger.Debug("Creating context menu for table: {Table}", fullTableName);
        
        var contextMenu = new ContextMenu();

        // View Table Details
        var detailsMenuItem = new MenuItem
        {
            Header = "📋 View Table Details...",
            FontWeight = FontWeights.Bold
        };
        detailsMenuItem.Click += (s, e) => ViewTableDetails(fullTableName);
        contextMenu.Items.Add(detailsMenuItem);

        contextMenu.Items.Add(new Separator());

        // SELECT Top 1000
        var selectTopMenuItem = new MenuItem
        {
            Header = "🔍 SELECT Top 1000 Rows"
        };
        selectTopMenuItem.Click += (s, e) => SelectTopRows(fullTableName, 1000);
        contextMenu.Items.Add(selectTopMenuItem);

        // SELECT Top 100
        var selectTop100MenuItem = new MenuItem
        {
            Header = "🔍 SELECT Top 100 Rows"
        };
        selectTop100MenuItem.Click += (s, e) => SelectTopRows(fullTableName, 100);
        contextMenu.Items.Add(selectTop100MenuItem);

        // SELECT All
        var selectAllMenuItem = new MenuItem
        {
            Header = "🔍 SELECT * (All Rows)"
        };
        selectAllMenuItem.Click += (s, e) => SelectAllRows(fullTableName);
        contextMenu.Items.Add(selectAllMenuItem);

        contextMenu.Items.Add(new Separator());

        // View Foreign Keys
        var foreignKeysMenuItem = new MenuItem
        {
            Header = "🔗 View Foreign Keys..."
        };
        foreignKeysMenuItem.Click += (s, e) => ViewForeignKeys(fullTableName);
        contextMenu.Items.Add(foreignKeysMenuItem);

        // View Indexes
        var indexesMenuItem = new MenuItem
        {
            Header = "📊 View Indexes..."
        };
        indexesMenuItem.Click += (s, e) => ViewIndexes(fullTableName);
        contextMenu.Items.Add(indexesMenuItem);

        // View DDL
        var ddlMenuItem = new MenuItem
        {
            Header = "📝 View DDL Script..."
        };
        ddlMenuItem.Click += (s, e) => ViewDDL(fullTableName);
        contextMenu.Items.Add(ddlMenuItem);

        contextMenu.Items.Add(new Separator());

        // Copy Table Name
        var copyNameMenuItem = new MenuItem
        {
            Header = "📄 Copy Table Name"
        };
        copyNameMenuItem.Click += (s, e) => CopyTableName(fullTableName);
        contextMenu.Items.Add(copyNameMenuItem);

        // Copy SELECT Statement
        var copySelectMenuItem = new MenuItem
        {
            Header = "📄 Copy SELECT Statement"
        };
        copySelectMenuItem.Click += (s, e) => CopySelectStatement(fullTableName);
        contextMenu.Items.Add(copySelectMenuItem);

        contextMenu.Items.Add(new Separator());

        // Refresh
        var refreshMenuItem = new MenuItem
        {
            Header = "🔄 Refresh"
        };
        refreshMenuItem.Click += (s, e) => RefreshTable(fullTableName);
        contextMenu.Items.Add(refreshMenuItem);

        return contextMenu;
    }

    private void ViewTableDetails(string fullTableName)
    {
        Logger.Info("Opening table details dialog for: {Table}", fullTableName);
        
        try
        {
            var dialog = new Dialogs.TableDetailsDialog(_connectionManager, fullTableName)
            {
                Owner = Window.GetWindow(this)
            };

            if (dialog.ShowDialog() == true)
            {
                // Check if user wants to query the table
                if (dialog.ShouldQueryTable)
                {
                    SelectTopRows(fullTableName, _preferencesService.Preferences.MaxRowsPerQuery);
                }
                // Check if user selected a related table to view
                else if (!string.IsNullOrEmpty(dialog.SelectedRelatedTable))
                {
                    ViewTableDetails(dialog.SelectedRelatedTable);
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to open table details dialog");
            MessageBox.Show($"Error opening table details:\n\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void SelectTopRows(string fullTableName, int topRows)
    {
        Logger.Info("Generating SELECT TOP {Count} for table: {Table}", topRows, fullTableName);
        
        var sql = $"SELECT * FROM {fullTableName} FETCH FIRST {topRows} ROWS ONLY";
        SqlEditor.Text = sql;
        SqlEditor.SelectAll();
        SqlEditor.Focus();
        
        StatusText.Text = $"Query generated for {fullTableName}";
    }

    private void SelectAllRows(string fullTableName)
    {
        Logger.Info("Generating SELECT ALL for table: {Table}", fullTableName);
        
        var sql = $"SELECT * FROM {fullTableName}";
        SqlEditor.Text = sql;
        SqlEditor.SelectAll();
        SqlEditor.Focus();
        
        StatusText.Text = $"Query generated for {fullTableName}";
    }

    private void ViewForeignKeys(string fullTableName)
    {
        Logger.Info("Opening table details (Foreign Keys tab) for: {Table}", fullTableName);
        
        try
        {
            var dialog = new Dialogs.TableDetailsDialog(_connectionManager, fullTableName)
            {
                Owner = Window.GetWindow(this)
            };
            
            // Switch to Foreign Keys tab after loading
            dialog.Loaded += (s, e) =>
            {
                if (dialog.DetailsTabControl.Items.Count > 1)
                {
                    dialog.DetailsTabControl.SelectedIndex = 1; // Foreign Keys tab
                }
            };
            
            dialog.ShowDialog();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to view foreign keys");
            MessageBox.Show($"Error viewing foreign keys:\n\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ViewIndexes(string fullTableName)
    {
        Logger.Info("Opening table details (Indexes tab) for: {Table}", fullTableName);
        
        try
        {
            var dialog = new Dialogs.TableDetailsDialog(_connectionManager, fullTableName)
            {
                Owner = Window.GetWindow(this)
            };
            
            // Switch to Indexes tab after loading
            dialog.Loaded += (s, e) =>
            {
                if (dialog.DetailsTabControl.Items.Count > 2)
                {
                    dialog.DetailsTabControl.SelectedIndex = 2; // Indexes tab
                }
            };
            
            dialog.ShowDialog();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to view indexes");
            MessageBox.Show($"Error viewing indexes:\n\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ViewDDL(string fullTableName)
    {
        Logger.Info("Opening table details (DDL tab) for: {Table}", fullTableName);
        
        try
        {
            var dialog = new Dialogs.TableDetailsDialog(_connectionManager, fullTableName)
            {
                Owner = Window.GetWindow(this)
            };
            
            // Switch to DDL tab after loading
            dialog.Loaded += (s, e) =>
            {
                if (dialog.DetailsTabControl.Items.Count > 3)
                {
                    dialog.DetailsTabControl.SelectedIndex = 3; // DDL tab
                }
            };
            
            dialog.ShowDialog();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to view DDL");
            MessageBox.Show($"Error viewing DDL:\n\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void CopyTableName(string fullTableName)
    {
        Logger.Debug("Copying table name to clipboard: {Table}", fullTableName);
        
        try
        {
            Clipboard.SetText(fullTableName);
            StatusText.Text = $"Table name copied: {fullTableName}";
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to copy table name");
            MessageBox.Show($"Failed to copy to clipboard:\n\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void CopySelectStatement(string fullTableName)
    {
        Logger.Debug("Copying SELECT statement to clipboard for: {Table}", fullTableName);
        
        try
        {
            var sql = $"SELECT * FROM {fullTableName};";
            Clipboard.SetText(sql);
            StatusText.Text = $"SELECT statement copied";
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to copy SELECT statement");
            MessageBox.Show($"Failed to copy to clipboard:\n\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void RefreshTable(string fullTableName)
    {
        Logger.Info("Refresh requested for table: {Table}", fullTableName);
        
        StatusText.Text = $"Refreshed: {fullTableName}";
        
        // Could reload table details if needed in the future
    }

    private async void Execute_Click(object sender, RoutedEventArgs e)
    {
        await ExecuteQuery();
    }

    private async Task ExecuteQuery()
    {
        await ExecuteSql(SqlEditor.Text);
    }

    private async Task ExecuteCurrentStatement()
    {
        Logger.Debug("Execute current statement requested");

        var sql = GetCurrentStatement();
        if (string.IsNullOrWhiteSpace(sql))
        {
            StatusText.Text = "No SQL statement at cursor";
            return;
        }

        await ExecuteSql(sql);
    }

    private string GetCurrentStatement()
    {
        var text = SqlEditor.Text;
        var caretOffset = SqlEditor.CaretOffset;

        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }

        // Find statement boundaries (separated by semicolons)
        var statements = text.Split(';');
        var currentPos = 0;

        foreach (var statement in statements)
        {
            currentPos += statement.Length;
            if (caretOffset <= currentPos)
            {
                return statement.Trim();
            }
            currentPos++; // Account for semicolon
        }

        return statements.LastOrDefault()?.Trim() ?? string.Empty;
    }

    private async Task ExecuteSql(string sql, bool resetPagination = true)
    {
        sql = sql?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(sql))
        {
            Logger.Debug("Empty SQL query");
            StatusText.Text = "No SQL to execute";
            return;
        }

        // Reset pagination if this is a new query
        if (resetPagination)
        {
            _currentPage = 1;
            _currentOffset = 0;
            _lastExecutedSql = sql;
            Logger.Debug("Pagination reset to page 1");
        }

        Logger.Info($"Executing SQL (Page {_currentPage}, Offset {_currentOffset}): {sql.Substring(0, Math.Min(50, sql.Length))}...");
        StatusText.Text = "Executing query...";
        ExecuteButton.IsEnabled = false;
        PreviousButton.IsEnabled = false;
        NextButton.IsEnabled = false;

        var stopwatch = Stopwatch.StartNew();

        try
        {
            var maxRows = _preferencesService.Preferences.MaxRowsPerQuery;
            var handleDecimalErrors = _preferencesService.Preferences.HandleDecimalErrorsGracefully;
            
            var dataTable = await _connectionManager.ExecuteQueryAsync(sql, maxRows, _currentOffset, handleDecimalErrors);
            ResultsGrid.ItemsSource = dataTable.DefaultView;

            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMilliseconds;

            var rowsReturned = dataTable.Rows.Count;
            var rowFrom = _currentOffset + 1;
            var rowTo = _currentOffset + rowsReturned;
            
            RowCountText.Text = $"{rowsReturned} rows (Rows {rowFrom:N0}-{rowTo:N0})";
            ExecutionTimeText.Text = $"Executed in {elapsed}ms";
            StatusText.Text = "Query completed successfully";
            PageInfoText.Text = $"Page {_currentPage}";

            // Enable/disable pagination buttons
            PreviousButton.IsEnabled = _currentPage > 1;
            NextButton.IsEnabled = rowsReturned >= maxRows; // If we got a full page, there might be more
            
            Logger.Debug("Pagination buttons updated - Previous: {PrevEnabled}, Next: {NextEnabled}", 
                PreviousButton.IsEnabled, NextButton.IsEnabled);

            Logger.Info($"Query executed successfully: {rowsReturned} rows in {elapsed}ms (Page {_currentPage})");

            // Save to query history with connection name
            _queryHistoryService.AddQuery(sql, _connection.GetDisplayName(), _connection.Database, true, elapsed, rowsReturned);
            
            // Refresh history display
            RefreshQueryHistory();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Query execution failed");
            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMilliseconds;

            StatusText.Text = "Query failed";
            RowCountText.Text = "Error - No results";
            ExecutionTimeText.Text = "";
            PageInfoText.Text = "Page -";
            PreviousButton.IsEnabled = false;
            NextButton.IsEnabled = false;

            // Save failed query to history
            _queryHistoryService.AddQuery(sql, _connection.GetDisplayName(), _connection.Database, false, elapsed);
            
            // Refresh history display
            RefreshQueryHistory();

            MessageBox.Show($"Query execution error:\n\n{ex.Message}", "Query Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            ExecuteButton.IsEnabled = true;
        }
    }
    
    private async void Previous_Click(object sender, RoutedEventArgs e)
    {
        if (_currentPage <= 1 || string.IsNullOrEmpty(_lastExecutedSql))
        {
            return;
        }

        Logger.Debug($"Loading previous page (current: {_currentPage})");
        
        _currentPage--;
        _currentOffset = (_currentPage - 1) * _preferencesService.Preferences.MaxRowsPerQuery;
        
        await ExecuteSql(_lastExecutedSql, resetPagination: false);
    }

    private async void Next_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(_lastExecutedSql))
        {
            return;
        }

        Logger.Debug($"Loading next page (current: {_currentPage})");
        
        _currentPage++;
        _currentOffset = (_currentPage - 1) * _preferencesService.Preferences.MaxRowsPerQuery;
        
        await ExecuteSql(_lastExecutedSql, resetPagination: false);
    }

    private void Format_Click(object sender, RoutedEventArgs e)
    {
        FormatSql();
    }

    private void FormatSql()
    {
        var sql = SqlEditor.Text;
        if (string.IsNullOrWhiteSpace(sql))
        {
            return;
        }

        Logger.Info("Formatting SQL");

        try
        {
            var formattedSql = _formatterService.FormatSql(sql);
            SqlEditor.Text = formattedSql;
            StatusText.Text = "SQL formatted successfully";
            Logger.Debug("SQL formatting completed");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "SQL formatting failed");
            MessageBox.Show($"Formatting error:\n\n{ex.Message}", "Format Error",
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void Clear_Click(object sender, RoutedEventArgs e)
    {
        Logger.Debug("Clearing editor and results");
        SqlEditor.Clear();
        ResultsGrid.ItemsSource = null;
        RowCountText.Text = "No results";
        ExecutionTimeText.Text = "";
        StatusText.Text = "Ready";
    }

    private void Export_Click(object sender, RoutedEventArgs e)
    {
        Logger.Debug("Export button clicked");

        if (ResultsGrid.ItemsSource == null)
        {
            MessageBox.Show("No results to export. Execute a query first.", "Export",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var dataView = ResultsGrid.ItemsSource as System.Data.DataView;
        if (dataView == null)
        {
            Logger.Warn("Unable to export: ItemsSource is not a DataView");
            return;
        }

        var dataTable = dataView.Table;
        if (dataTable == null)
        {
            Logger.Warn("Unable to export: DataView.Table is null");
            StatusText.Text = "No data to export";
            MessageBox.Show("No data available to export.", "Export Failed", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Show export dialog
        var exportDialog = new ExportToFileDialog(dataTable)
        {
            Owner = Window.GetWindow(this)
        };

        if (exportDialog.ShowDialog() == true)
        {
            StatusText.Text = $"Exported {dataTable.Rows.Count} rows";
            Logger.Info("Export to file completed successfully");
        }
    }

    private void Open_Click(object sender, RoutedEventArgs e)
    {
        OpenScript();
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        SaveScript();
    }

    private void OpenScript()
    {
        Logger.Debug("Open script requested");

        var openFileDialog = new Microsoft.Win32.OpenFileDialog
        {
            Filter = "SQL Files (*.sql)|*.sql|Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
            DefaultExt = ".sql",
            Title = "Open SQL Script"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            try
            {
                Logger.Info($"Opening script: {openFileDialog.FileName}");
                var content = File.ReadAllText(openFileDialog.FileName);
                SqlEditor.Text = content;
                StatusText.Text = $"Loaded: {Path.GetFileName(openFileDialog.FileName)}";
                Logger.Info($"Script loaded successfully: {content.Length} characters");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to open script: {openFileDialog.FileName}");
                MessageBox.Show($"Failed to open script:\n\n{ex.Message}", "Open Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void SaveScript()
    {
        Logger.Debug("Save script requested");

        if (string.IsNullOrWhiteSpace(SqlEditor.Text))
        {
            MessageBox.Show("No SQL to save.", "Save Script",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var saveFileDialog = new Microsoft.Win32.SaveFileDialog
        {
            Filter = "SQL Files (*.sql)|*.sql|Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
            DefaultExt = ".sql",
            FileName = $"script_{DateTime.Now:yyyyMMdd_HHmmss}.sql",
            Title = "Save SQL Script"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            try
            {
                Logger.Info($"Saving script: {saveFileDialog.FileName}");
                File.WriteAllText(saveFileDialog.FileName, SqlEditor.Text);
                StatusText.Text = $"Saved: {Path.GetFileName(saveFileDialog.FileName)}";
                Logger.Info($"Script saved successfully: {SqlEditor.Text.Length} characters");

                MessageBox.Show($"Script saved successfully:\n{saveFileDialog.FileName}",
                    "Save Complete", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to save script: {saveFileDialog.FileName}");
                MessageBox.Show($"Failed to save script:\n\n{ex.Message}", "Save Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    /// <summary>
    /// Set query text in the editor (used for rerunning queries from history)
    /// </summary>
    public void SetQueryText(string queryText)
    {
        Logger.Debug("Setting query text from external source");
        SqlEditor.Text = queryText;
        SqlEditor.Focus();
    }

    /// <summary>
    /// Refresh query history display
    /// </summary>
    private void RefreshQueryHistory()
    {
        try
        {
            Logger.Debug("Refreshing query history display");
            
            var searchTerm = HistorySearchBox?.Text ?? string.Empty;
            var showAllConnections = ShowAllConnectionsCheckBox?.IsChecked == true;
            
            var connectionFilter = showAllConnections ? null : _connection.GetDisplayName();
            
            var historyItems = _queryHistoryService.SearchHistory(searchTerm, connectionFilter);
            
            var viewModels = historyItems.Select(item =>
            {
                var decryptedSql = _queryHistoryService.GetDecryptedSql(item);
                return Models.QueryHistoryViewModel.FromHistoryItem(item, decryptedSql);
            }).ToList();
            
            QueryHistoryListBox.ItemsSource = viewModels;
            
            Logger.Debug("History refreshed with {Count} items", viewModels.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to refresh query history");
        }
    }

    /// <summary>
    /// Handle search text changed in history
    /// </summary>
    private void HistorySearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        RefreshQueryHistory();
    }

    /// <summary>
    /// Handle show all connections checkbox changed
    /// </summary>
    private void ShowAllConnections_Changed(object sender, RoutedEventArgs e)
    {
        RefreshQueryHistory();
    }

    /// <summary>
    /// Handle double-click on history item to load query
    /// </summary>
    private void HistoryItem_DoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (QueryHistoryListBox.SelectedItem is Models.QueryHistoryViewModel viewModel)
        {
            Logger.Info("Loading query from history: {Preview}", viewModel.SqlPreview);
            
            SqlEditor.Text = viewModel.SqlStatement;
            SqlEditor.SelectAll();
            SqlEditor.Focus();
            
            StatusText.Text = $"Query loaded from history ({viewModel.ExecutedAt})";
        }
    }

    /// <summary>
    /// Open dialog to copy results to clipboard in various formats
    /// </summary>
    private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
    {
        Logger.Debug("Copy to clipboard requested");

        if (ResultsGrid.ItemsSource == null)
        {
            MessageBox.Show("No results to copy. Execute a query first.", "Copy to Clipboard",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var dataView = ResultsGrid.ItemsSource as System.Data.DataView;
        if (dataView?.Table == null)
        {
            Logger.Warn("Unable to copy: ItemsSource is not a DataView or Table is null");
            MessageBox.Show("No data available to copy.", "Copy Failed",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            var dialog = new Dialogs.ExportToClipboardDialog(dataView.Table)
            {
                Owner = Window.GetWindow(this)
            };

            if (dialog.ShowDialog() == true)
            {
                StatusText.Text = "Data copied to clipboard";
                Logger.Info("Data successfully copied to clipboard");
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to open copy to clipboard dialog");
            MessageBox.Show($"Error opening copy dialog:\n\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// Copy selected cell value to clipboard (Issue #1 fix - uses cached cell info)
    /// </summary>
    private void CopyCell_Click(object sender, RoutedEventArgs e)
    {
        Logger.Debug("Copy cell requested");

        try
        {
            // Use cached cell info instead of SelectedCells (Issue #1 fix)
            if (_lastClickedCell == null || !_lastClickedCell.HasValue)
            {
                Logger.Warn("No cell clicked - attempting fallback to SelectedCells");
                
                // Fallback to selected cells if no cached cell
                var selectedCells = ResultsGrid.SelectedCells;
                if (selectedCells.Count == 0)
                {
                    MessageBox.Show("No cell selected.", "Copy Cell",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                
                _lastClickedCell = selectedCells[0];
            }

            var cellInfo = _lastClickedCell.Value;
            var cellValue = cellInfo.Item;
            var columnName = cellInfo.Column?.Header?.ToString() ?? string.Empty;

            Logger.Debug("Copying cell - Column: {Column}, HasValue: {HasValue}", 
                columnName, cellValue != null);

            if (cellValue != null)
            {
                var property = cellValue.GetType().GetProperty(columnName);
                if (property != null)
                {
                    var value = property.GetValue(cellValue)?.ToString() ?? string.Empty;
                    Clipboard.SetText(value);
                    StatusText.Text = $"Cell value copied: {value.Substring(0, Math.Min(30, value.Length))}...";
                    Logger.Info("Cell value copied to clipboard");
                }
                else
                {
                    // Try DataRowView
                    if (cellValue is System.Data.DataRowView rowView)
                    {
                        var value = rowView[columnName]?.ToString() ?? string.Empty;
                        Clipboard.SetText(value);
                        StatusText.Text = $"Cell value copied";
                        Logger.Info("Cell value copied to clipboard");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to copy cell");
            MessageBox.Show($"Failed to copy cell:\n\n{ex.Message}", "Copy Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// Copy selected row to clipboard as CSV
    /// </summary>
    private void CopyRow_Click(object sender, RoutedEventArgs e)
    {
        Logger.Debug("Copy row requested");

        try
        {
            if (ResultsGrid.SelectedItem == null)
            {
                MessageBox.Show("No row selected.", "Copy Row",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var dataView = ResultsGrid.ItemsSource as System.Data.DataView;
            if (dataView?.Table == null)
            {
                Logger.Warn("Unable to copy row: ItemsSource is not a DataView");
                return;
            }

            var rowView = ResultsGrid.SelectedItem as System.Data.DataRowView;
            if (rowView == null)
            {
                Logger.Warn("Selected item is not a DataRowView");
                return;
            }

            // Build CSV row
            var values = new List<string>();
            foreach (System.Data.DataColumn column in dataView.Table.Columns)
            {
                var value = rowView[column.ColumnName]?.ToString() ?? string.Empty;
                // Escape CSV if needed
                if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
                {
                    value = $"\"{value.Replace("\"", "\"\"")}\"";
                }
                values.Add(value);
            }

            var csvRow = string.Join(",", values);
            Clipboard.SetText(csvRow);
            StatusText.Text = "Row copied to clipboard as CSV";
            Logger.Info("Row copied to clipboard");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to copy row");
            MessageBox.Show($"Failed to copy row:\n\n{ex.Message}", "Copy Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// Copy selected cells to clipboard with format options
    /// </summary>
    private void CopySelection_Click(object sender, RoutedEventArgs e)
    {
        Logger.Debug("Copy selection requested");

        try
        {
            var selectedCells = ResultsGrid.SelectedCells;
            if (selectedCells.Count == 0)
            {
                MessageBox.Show("No cells selected.", "Copy Selection",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var dataView = ResultsGrid.ItemsSource as System.Data.DataView;
            if (dataView?.Table == null)
            {
                Logger.Warn("Unable to copy selection: ItemsSource is not a DataView");
                return;
            }

            // Group selected cells by row and column
            var selectedRows = new HashSet<int>();
            var selectedColumns = new HashSet<int>();
            
            foreach (var cellInfo in selectedCells)
            {
                var rowItem = cellInfo.Item;
                var rowIndex = dataView.Table.Rows.IndexOf(((System.Data.DataRowView)rowItem).Row);
                var columnIndex = cellInfo.Column.DisplayIndex;
                
                selectedRows.Add(rowIndex);
                selectedColumns.Add(columnIndex);
            }

            Logger.Debug("Selection spans {RowCount} rows and {ColumnCount} columns", 
                selectedRows.Count, selectedColumns.Count);

            // Get sorted list of columns and their names
            var sortedColumns = selectedColumns.OrderBy(c => c).ToList();
            var columnHeaders = sortedColumns
                .Select(colIndex => ResultsGrid.Columns[colIndex].Header?.ToString() ?? $"Column{colIndex}")
                .ToList();

            // Create a DataTable with only the selected data
            var selectionTable = new System.Data.DataTable();
            foreach (var header in columnHeaders)
            {
                selectionTable.Columns.Add(header);
            }

            var sortedRows = selectedRows.OrderBy(r => r).ToList();
            foreach (var rowIndex in sortedRows)
            {
                var sourceRow = dataView.Table.Rows[rowIndex];
                var newRow = selectionTable.NewRow();
                
                for (int i = 0; i < sortedColumns.Count; i++)
                {
                    var colIndex = sortedColumns[i];
                    var columnName = dataView.Table.Columns[colIndex].ColumnName;
                    newRow[i] = sourceRow[columnName];
                }
                
                selectionTable.Rows.Add(newRow);
            }

            // Show copy selection dialog
            var copyDialog = new CopySelectionDialog(selectionTable, columnHeaders)
            {
                Owner = Window.GetWindow(this)
            };

            if (copyDialog.ShowDialog() == true)
            {
                StatusText.Text = $"Copied {sortedRows.Count} row(s) × {sortedColumns.Count} column(s) to clipboard";
                Logger.Info("Selection copied successfully");
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to copy selection");
            MessageBox.Show($"Failed to copy selection:\n\n{ex.Message}", "Copy Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    /// <summary>
    /// Update access level indicator badge - RBAC
    /// </summary>
    private void UpdateAccessLevelIndicator()
    {
        Logger.Debug("Updating access level indicator");
        
        if (_connection.Permissions == null)
        {
            Logger.Warn("Permissions not determined, hiding access level badge");
            AccessLevelBadge.Visibility = Visibility.Collapsed;
            return;
        }
        
        var permissions = _connection.Permissions;
        
        // Update badge text
        AccessLevelText.Text = permissions.AccessLevelBadge;
        AccessLevelText.ToolTip = permissions.PermissionsTooltip;
        
        // Update badge color
        var color = permissions.BadgeColor switch
        {
            "Green" => System.Windows.Media.Colors.Green,
            "Orange" => System.Windows.Media.Colors.Orange,
            "Red" => System.Windows.Media.Colors.Red,
            _ => System.Windows.Media.Colors.Gray
        };
        
        AccessLevelBadge.Background = new System.Windows.Media.SolidColorBrush(color);
        AccessLevelBadge.Visibility = Visibility.Visible;
        
        Logger.Info("Access level indicator updated: {Badge} ({Color})", 
            permissions.AccessLevelBadge, permissions.BadgeColor);
        
        // Log user permissions for transparency
        Logger.Info("User permissions - DDL: {DDL}, DML: {DML}, Force: {Force}, Stats: {Stats}, CDC: {CDC}, Drop: {Drop}",
            permissions.CanExecuteDDL, permissions.CanExecuteDML, permissions.CanForceDisconnect,
            permissions.CanModifyStatistics, permissions.CanModifyCDC, permissions.CanDropObjects);
    }
    
    private void UpdateTransactionButtonsVisibility()
    {
        var showButtons = !_connection.AutoCommit;
        CommitButton.Visibility = showButtons ? Visibility.Visible : Visibility.Collapsed;
        RollbackButton.Visibility = showButtons ? Visibility.Visible : Visibility.Collapsed;
        Logger.Debug("Transaction buttons visibility: {Visible} (AutoCommit: {AutoCommit})", showButtons, _connection.AutoCommit);
    }
    
    private async Task CommitTransaction()
    {
        try
        {
            await _connectionManager.CommitAsync();
            StatusText.Text = "Transaction committed";
            Logger.Info("Transaction committed successfully");
            MessageBox.Show("Transaction committed successfully.", "Commit", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to commit transaction");
            MessageBox.Show($"Failed to commit:\n\n{ex.Message}", "Commit Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private async Task RollbackTransaction()
    {
        try
        {
            await _connectionManager.RollbackAsync();
            StatusText.Text = "Transaction rolled back";
            Logger.Info("Transaction rolled back successfully");
            MessageBox.Show("Transaction rolled back successfully.", "Rollback", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to rollback transaction");
            MessageBox.Show($"Failed to rollback:\n\n{ex.Message}", "Rollback Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private async void Commit_Click(object sender, RoutedEventArgs e) => await CommitTransaction();
    
    private async void Rollback_Click(object sender, RoutedEventArgs e) => await RollbackTransaction();

    public void Cleanup()
    {
        Logger.Debug("Cleaning up ConnectionTabControl");
        _connectionManager?.Dispose();
    }
}

