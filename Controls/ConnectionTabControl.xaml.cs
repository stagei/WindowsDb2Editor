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

    public ConnectionTabControl(DB2Connection connection)
    {
        InitializeComponent();
        Logger.Debug($"ConnectionTabControl initializing for {connection.GetDisplayName()}");

        _connection = connection;
        _connectionManager = new DB2ConnectionManager(connection);
        _formatterService = new SqlFormatterService();
        _queryHistoryService = new QueryHistoryService();
        _exportService = new ExportService();

        InitializeSqlEditor();
        RegisterKeyboardShortcuts();
        _ = ConnectToDatabase();
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

            // Load database objects
            await LoadDatabaseObjectsAsync();
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
            var schemas = await _connectionManager.GetSchemasAsync();
            Logger.Info($"Loaded {schemas.Count} schemas");

            DatabaseTreeView.Items.Clear();

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

    private async Task ExecuteSql(string sql)
    {
        sql = sql?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(sql))
        {
            Logger.Debug("Empty SQL query");
            StatusText.Text = "No SQL to execute";
            return;
        }

        Logger.Info($"Executing SQL: {sql.Substring(0, Math.Min(50, sql.Length))}...");
        StatusText.Text = "Executing query...";
        ExecuteButton.IsEnabled = false;

        var stopwatch = Stopwatch.StartNew();

        try
        {
            var dataTable = await _connectionManager.ExecuteQueryAsync(sql);
            ResultsGrid.ItemsSource = dataTable.DefaultView;

            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMilliseconds;

            RowCountText.Text = $"{dataTable.Rows.Count} rows";
            ExecutionTimeText.Text = $"Executed in {elapsed}ms";
            StatusText.Text = "Query completed successfully";

            Logger.Info($"Query executed successfully: {dataTable.Rows.Count} rows in {elapsed}ms");

            // Save to query history
            _queryHistoryService.AddQuery(sql, _connection.Database, true, elapsed, dataTable.Rows.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Query execution failed");
            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMilliseconds;

            StatusText.Text = "Query failed";
            RowCountText.Text = "Error";
            ExecutionTimeText.Text = "";

            // Save failed query to history
            _queryHistoryService.AddQuery(sql, _connection.Database, false, elapsed);

            MessageBox.Show($"Query execution error:\n\n{ex.Message}", "Query Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            ExecuteButton.IsEnabled = true;
        }
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

    private async void Export_Click(object sender, RoutedEventArgs e)
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

        var saveFileDialog = new Microsoft.Win32.SaveFileDialog
        {
            Filter = "CSV Files (*.csv)|*.csv|TSV Files (*.tsv)|*.tsv|JSON Files (*.json)|*.json|SQL Files (*.sql)|*.sql",
            DefaultExt = ".csv",
            FileName = $"export_{DateTime.Now:yyyyMMdd_HHmmss}"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            try
            {
                Logger.Info($"Exporting to: {saveFileDialog.FileName}");
                StatusText.Text = "Exporting...";

                var extension = System.IO.Path.GetExtension(saveFileDialog.FileName).ToLowerInvariant();

                switch (extension)
                {
                    case ".csv":
                        await _exportService.ExportToCsvAsync(dataTable, saveFileDialog.FileName);
                        break;
                    case ".tsv":
                        await _exportService.ExportToTsvAsync(dataTable, saveFileDialog.FileName);
                        break;
                    case ".json":
                        await _exportService.ExportToJsonAsync(dataTable, saveFileDialog.FileName);
                        break;
                    case ".sql":
                        var tableName = "exported_table"; // Could prompt user for table name
                        await _exportService.ExportToSqlAsync(dataTable, tableName, saveFileDialog.FileName);
                        break;
                    default:
                        await _exportService.ExportToCsvAsync(dataTable, saveFileDialog.FileName);
                        break;
                }

                StatusText.Text = $"Exported {dataTable.Rows.Count} rows";
                Logger.Info($"Successfully exported {dataTable.Rows.Count} rows to {saveFileDialog.FileName}");

                MessageBox.Show($"Successfully exported {dataTable.Rows.Count} rows to:\n{saveFileDialog.FileName}",
                    "Export Complete", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Export failed");
                StatusText.Text = "Export failed";
                MessageBox.Show($"Export failed:\n\n{ex.Message}", "Export Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

    public void Cleanup()
    {
        Logger.Debug("Cleaning up ConnectionTabControl");
        _connectionManager?.Dispose();
    }
}

