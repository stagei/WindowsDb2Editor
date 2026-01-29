using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Microsoft.Win32;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Models;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.Dialogs;

/// <summary>
/// Dialog for Missing FK Discovery feature.
/// Allows users to select tables, configure options, and start batch job.
/// </summary>
public partial class MissingFKDiscoveryDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    private readonly IConnectionManager _connectionManager;
    private readonly MissingFKSqlTranslationService _sqlTranslationService;
    private readonly MissingFKMetadataService _metadataService;
    private readonly MissingFKIgnoreService _ignoreService;
    private readonly MissingFKIgnoreHistoryService _ignoreHistoryService;
    private readonly MissingFKJobStatusService _jobStatusService;
    private readonly MissingFKSearchHistoryService _searchHistoryService;
    private readonly string _connectionProfile;
    
    private List<string> _availableSchemas = new();
    private List<TableSelectionItem> _allTables = new();
    private List<TableSelectionItem> _selectedTables = new();
    private string _outputFolder = string.Empty;
    private string _ignoreJsonPath = string.Empty;
    private string _ignoreFilesFolder = string.Empty;
    private string _jobId = string.Empty;
    private Process? _runningJobProcess;
    private FileSystemWatcher? _statusFileWatcher;
    private FileSystemWatcher? _logFileWatcher;
    private Timer? _statusPollTimer;
    private Timer? _logPollTimer;
    private MissingFKIgnoreModel _currentIgnoreModel = new();
    private ObservableCollection<MissingFKIgnoreColumn> _ignoreColumnsCollection = new();
    
    public MissingFKDiscoveryDialog(IConnectionManager connectionManager, string connectionProfile)
    {
        Logger.Debug("MissingFKDiscoveryDialog constructor started");
        
        try
        {
            InitializeComponent();
            Logger.Debug("InitializeComponent completed");
            
            _connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));
            _connectionProfile = connectionProfile ?? throw new ArgumentNullException(nameof(connectionProfile));
            Logger.Debug("Connection manager and profile set: {Profile}", _connectionProfile);
            
            // Initialize services
            Logger.Debug("Initializing services...");
            var sqlMermaidService = new SqlMermaidIntegrationService();
            Logger.Debug("SqlMermaidIntegrationService created");
            
            _sqlTranslationService = new MissingFKSqlTranslationService(sqlMermaidService);
            Logger.Debug("MissingFKSqlTranslationService created");
            
            _metadataService = new MissingFKMetadataService(_sqlTranslationService, _connectionManager);
            Logger.Debug("MissingFKMetadataService created");
            
            _ignoreService = new MissingFKIgnoreService();
            Logger.Debug("MissingFKIgnoreService created");
            
            _ignoreHistoryService = new MissingFKIgnoreHistoryService();
            Logger.Debug("MissingFKIgnoreHistoryService created");
            
            _jobStatusService = new MissingFKJobStatusService();
            Logger.Debug("MissingFKJobStatusService created");
            
            _searchHistoryService = new MissingFKSearchHistoryService();
            Logger.Debug("MissingFKSearchHistoryService created");
            
            // Set default output folder to user's data folder MissingFK/Projects
            // Note: The actual project subfolder will be created when starting a job
            _outputFolder = UserDataFolderHelper.GetMissingFKProjectsFolder();
            Logger.Debug("Output base folder path: {Path}", _outputFolder);
            
            OutputFolderTextBox.Text = _outputFolder;
            Logger.Debug("Output folder textbox set");
            
            // Create dedicated folder for ignore patterns
            _ignoreFilesFolder = UserDataFolderHelper.GetMissingFKIgnorePatternsFolder();
            Logger.Info("Ignore patterns folder: {Path}", _ignoreFilesFolder);
            
            // Initialize ignore model summary
            Logger.Debug("Updating ignore rules summary...");
            UpdateIgnoreRulesSummary();
            Logger.Debug("Ignore rules summary updated");
            
            Loaded += MissingFKDiscoveryDialog_Loaded;
            Closing += MissingFKDiscoveryDialog_Closing;
            
            Logger.Info("MissingFKDiscoveryDialog constructor completed successfully");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Exception in MissingFKDiscoveryDialog constructor");
            throw;
        }
    }
    
    private void MissingFKDiscoveryDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        StopJobStatusMonitoring();
        StopLogFileMonitoring();
    }
    
    private async void MissingFKDiscoveryDialog_Loaded(object sender, RoutedEventArgs e)
    {
        Logger.Debug("MissingFKDiscoveryDialog_Loaded event started");
        
        try
        {
            Logger.Info("Loading schemas for Missing FK Discovery");
            ShowLoading("Loading schemas from database...");
            
            // Apply UI styles
            Logger.Debug("Applying UI styles");
            UIStyleService.ApplyStyles(this);
            
            // Only load schemas initially, not tables
            Logger.Debug("Calling LoadSchemasAsync");
            await LoadSchemasAsync();
            Logger.Debug("LoadSchemasAsync completed, populating schema combobox");
            
            PopulateSchemaComboBox();
            Logger.Debug("PopulateSchemaComboBox completed");
            
            HideLoading();
            StatusText.Text = "Ready - Please select a schema to load tables";
            TablesList.ItemsSource = new List<TableSelectionItem>(); // Empty list until schema selected
            
            // Check if a job is already running and update button state accordingly
            CheckExistingJobLock();
            
            Logger.Info("MissingFKDiscoveryDialog loaded successfully");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load schemas in MissingFKDiscoveryDialog_Loaded");
            HideLoading();
            MessageBox.Show(
                $"An error occurred:\n{ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
    
    private Storyboard? _spinnerStoryboard;

    private void ShowLoading(string message)
    {
        LoadingText.Text = message;
        LoadingOverlay.Visibility = Visibility.Visible;
        StartSpinnerAnimation();
    }
    
    private void HideLoading()
    {
        StopSpinnerAnimation();
        LoadingOverlay.Visibility = Visibility.Collapsed;
    }

    private void StartSpinnerAnimation()
    {
        try
        {
            _spinnerStoryboard = new Storyboard();
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(1),
                RepeatBehavior = RepeatBehavior.Forever
            };
            Storyboard.SetTarget(animation, SpinnerRotateTransform);
            Storyboard.SetTargetProperty(animation, new PropertyPath(System.Windows.Media.RotateTransform.AngleProperty));
            _spinnerStoryboard.Children.Add(animation);
            _spinnerStoryboard.Begin();
        }
        catch (Exception ex)
        {
            Logger.Debug(ex, "Failed to start spinner animation");
        }
    }

    private void StopSpinnerAnimation()
    {
        try
        {
            _spinnerStoryboard?.Stop();
            _spinnerStoryboard = null;
        }
        catch (Exception ex)
        {
            Logger.Debug(ex, "Failed to stop spinner animation");
        }
    }

    /// <summary>
    /// Check if a job is already running and update UI accordingly.
    /// Also provides option to clear stale locks.
    /// </summary>
    private void CheckExistingJobLock()
    {
        try
        {
            if (_jobStatusService.IsJobLocked(_outputFolder))
            {
                var lockInfo = _jobStatusService.GetLockInfo(_outputFolder);
                if (lockInfo != null)
                {
                    var lockAge = DateTime.UtcNow - lockInfo.StartedAtUtc;
                    
                    // If lock is more than 1 hour old, offer to clear it
                    if (lockAge.TotalHours > 1)
                    {
                        var result = MessageBox.Show(
                            $"A job lock exists from {lockAge.TotalHours:F1} hours ago (Job ID: {lockInfo.JobId}).\n\n" +
                            "This may be a stale lock from a previously interrupted job.\n\n" +
                            "Do you want to clear the stale lock and enable starting a new job?",
                            "Stale Job Lock Detected",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);
                        
                        if (result == MessageBoxResult.Yes)
                        {
                            _jobStatusService.ReleaseJobLock(_outputFolder);
                            Logger.Info("User cleared stale job lock (age: {Age} hours)", lockAge.TotalHours);
                            StatusText.Text = "Stale job lock cleared. Ready to start a new job.";
                            return;
                        }
                    }
                    
                    // Job is running - disable button and show status
                    StartBatchJobButton.IsEnabled = false;
                    StartBatchJobButton.Content = "Job Running...";
                    StatusText.Text = $"A job is already running (started {lockAge.TotalMinutes:F0} minutes ago)";
                    Logger.Info("Existing job lock detected - Job ID: {JobId}, Age: {Age} minutes", 
                        lockInfo.JobId, lockAge.TotalMinutes);
                    
                    // Update job ID for monitoring and start monitoring the running job
                    _jobId = lockInfo.JobId;
                    StartJobStatusMonitoring(_jobId, _outputFolder);
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to check existing job lock");
        }
    }
    
    /// <summary>
    /// Load only schemas (not tables) for initial population.
    /// </summary>
    private async Task LoadSchemasAsync()
    {
        Logger.Debug("Loading schemas");
        
        var provider = _connectionManager.ConnectionInfo.ProviderType?.ToUpperInvariant() ?? "DB2";
        var version = "12.1";
        
        // Use MetadataHandler to get SQL for listing schemas
        var metadataHandler = App.MetadataHandler ?? throw new InvalidOperationException("MetadataHandler not initialized");
        var schemasSql = metadataHandler.GetQuery(provider, version, "GetSchemasStatement");
        var schemasResult = await _connectionManager.ExecuteQueryAsync(schemasSql);
        
        var schemas = new List<string>();
        foreach (DataRow schemaRow in schemasResult.Rows)
        {
            var schema = schemaRow["SCHEMANAME"]?.ToString()?.Trim() ?? string.Empty;
            if (!string.IsNullOrEmpty(schema) && !schema.StartsWith("SYS", StringComparison.OrdinalIgnoreCase))
            {
                schemas.Add(schema);
            }
        }
        
        // Store schemas for later use
        _availableSchemas = schemas.OrderBy(s => s).ToList();
        
        Logger.Info("Loaded {Count} schemas", _availableSchemas.Count);
    }
    
    /// <summary>
    /// Load tables for a specific schema.
    /// </summary>
    private async Task LoadTablesForSchemaAsync(string schema)
    {
        Logger.Debug("Loading tables for schema: {Schema}", schema);
        
        if (string.IsNullOrEmpty(schema) || schema == "All")
        {
            _allTables.Clear();
            RefreshTableList();
            return;
        }
        
        var provider = _connectionManager.ConnectionInfo.ProviderType?.ToUpperInvariant() ?? "DB2";
        var version = "12.1";
        
        // Use MetadataHandler to get SQL for listing tables
        var metadataHandler = App.MetadataHandler ?? throw new InvalidOperationException("MetadataHandler not initialized");
        var sql = metadataHandler.GetQuery(provider, version, "GetTablesForSchema");
        
        // Get tables for this schema
        var tablesSql = ReplaceParameters(sql, schema);
        var tablesResult = await _connectionManager.ExecuteQueryAsync(tablesSql);
        
        _allTables.Clear();
        foreach (DataRow row in tablesResult.Rows)
        {
            var tableName = row["TABNAME"]?.ToString()?.Trim() ?? string.Empty;
            if (!string.IsNullOrEmpty(tableName))
            {
                _allTables.Add(new TableSelectionItem
                {
                    Schema = schema,
                    TableName = tableName,
                    IsSelected = false
                });
            }
        }
        
        Logger.Info("Loaded {Count} tables from schema {Schema}", _allTables.Count, schema);
        RefreshTableList();
        StatusText.Text = $"Ready - {_allTables.Count} tables loaded from schema {schema}";
    }
    
    private void PopulateSchemaComboBox()
    {
        SchemaComboBox.Items.Clear();
        SchemaComboBox.Items.Add("All");
        foreach (var schema in _availableSchemas)
        {
            SchemaComboBox.Items.Add(schema);
        }
        SchemaComboBox.SelectedIndex = 0;
    }
    
    private async void RefreshTableList()
    {
        var searchText = SearchTextBox.Text?.Trim() ?? string.Empty;
        var selectedSchema = SchemaComboBox.SelectedItem?.ToString() ?? "All";
        var searchType = (SearchTypeComboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "TableName";
        var patternType = (PatternTypeComboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "Standard";
        
        List<TableSelectionItem> filtered;
        
        if (string.IsNullOrEmpty(searchText))
        {
            // No search - show all tables in selected schema
            filtered = _allTables.Where(t =>
                selectedSchema == "All" || t.Schema == selectedSchema
            ).ToList();
        }
        else if (searchType == "ColumnName")
        {
            // Search by column name - need to query database
            filtered = await SearchTablesByColumnNameAsync(searchText, selectedSchema, patternType);
        }
        else
        {
            // Search by table name
            if (patternType == "Regex")
            {
                try
                {
                    var regex = new System.Text.RegularExpressions.Regex(searchText, 
                        System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    filtered = _allTables.Where(t =>
                        (regex.IsMatch(t.TableName) || regex.IsMatch(t.Schema)) &&
                        (selectedSchema == "All" || t.Schema == selectedSchema)
                    ).ToList();
                }
                catch (Exception ex)
                {
                    Logger.Warn(ex, "Invalid regex pattern: {Pattern}", searchText);
                    MessageBox.Show($"Invalid regex pattern: {ex.Message}", "Regex Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    filtered = _allTables.Where(t =>
                        selectedSchema == "All" || t.Schema == selectedSchema
                    ).ToList();
                }
            }
            else
            {
                // Standard search
                var searchLower = searchText.ToLowerInvariant();
                filtered = _allTables.Where(t =>
                    (t.TableName.ToLowerInvariant().Contains(searchLower) || t.Schema.ToLowerInvariant().Contains(searchLower)) &&
                    (selectedSchema == "All" || t.Schema == selectedSchema)
                ).ToList();
            }
        }
        
        TablesList.ItemsSource = filtered;
        
        // Enable/disable "Add All from Search" button based on whether search is active
        AddAllFromSearchButton.IsEnabled = !string.IsNullOrWhiteSpace(searchText) && filtered.Count > 0;
        
        // Auto-save search pattern to history
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            try
            {
                _searchHistoryService.SaveSearchPattern(searchText, searchType, patternType);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "Failed to save search pattern to history");
            }
        }
        
        UpdateSelectionCounts();
    }
    
    private async Task<List<TableSelectionItem>> SearchTablesByColumnNameAsync(string searchPattern, string selectedSchema, string patternType)
    {
        try
        {
            Logger.Debug("Searching tables by column name: {Pattern}, Schema: {Schema}, Type: {Type}", 
                searchPattern, selectedSchema, patternType);
            
            var matchingTables = new HashSet<TableSelectionItem>();
            var schemasToSearch = selectedSchema == "All" 
                ? _availableSchemas 
                : new List<string> { selectedSchema };
            
            foreach (var schema in schemasToSearch)
            {
                // Get all tables in this schema
                var schemaTables = _allTables.Where(t => t.Schema == schema).ToList();
                
                foreach (var table in schemaTables)
                {
                    try
                    {
                        // Get columns for this table
                        var sqlTemplate = await _sqlTranslationService.GetTranslatedStatementAsync(_connectionManager, "GetTableColumnsForMissingFK");
                        // Replace parameters - need to handle multiple ? placeholders
                        var parts = sqlTemplate.Split('?');
                        if (parts.Length >= 3)
                        {
                            var sql = $"{parts[0]}'{schema}'{parts[1]}'{table.TableName}'{parts[2]}";
                            if (parts.Length > 3)
                                sql += string.Join("", parts.Skip(3));
                            
                            var columnsResult = await _connectionManager.ExecuteQueryAsync(sql);
                            
                            var columnNames = new List<string>();
                            
                            foreach (DataRow row in columnsResult.Rows)
                            {
                                var colName = row["COLUMN_NAME"]?.ToString()?.Trim() ?? 
                                             row["COLNAME"]?.ToString()?.Trim() ?? string.Empty;
                                if (!string.IsNullOrEmpty(colName))
                                    columnNames.Add(colName);
                            }
                            
                            // Check if any column matches the pattern
                            bool matches = false;
                            if (patternType == "Regex")
                            {
                                try
                                {
                                    var regex = new System.Text.RegularExpressions.Regex(searchPattern, 
                                        System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                                    matches = columnNames.Any(col => regex.IsMatch(col));
                                }
                                catch
                                {
                                    // Invalid regex - skip this table
                                    continue;
                                }
                            }
                            else
                            {
                                var patternLower = searchPattern.ToLowerInvariant();
                                matches = columnNames.Any(col => col.ToLowerInvariant().Contains(patternLower));
                            }
                            
                            if (matches)
                            {
                                matchingTables.Add(table);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Debug(ex, "Failed to get columns for {Schema}.{Table}", schema, table.TableName);
                        // Continue with next table
                    }
                }
            }
            
            Logger.Info("Found {Count} tables matching column name pattern '{Pattern}'", matchingTables.Count, searchPattern);
            return matchingTables.ToList();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to search tables by column name");
            MessageBox.Show($"Failed to search by column name:\n{ex.Message}", "Search Error",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return _allTables.Where(t => selectedSchema == "All" || t.Schema == selectedSchema).ToList();
        }
    }
    
    private void UpdateSelectionCounts()
    {
        var selected = _allTables.Where(t => t.IsSelected).ToList();
        LeftSelectionCountText.Text = $"Available: {_allTables.Count} | Selected: {selected.Count}";
        
        _selectedTables = selected;
        SelectedTablesList.ItemsSource = _selectedTables.Select(t => new TableReference
        {
            Schema = t.Schema,
            Name = t.TableName
        }).ToList();
    }
    
    private async void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        RefreshTableList();
    }
    
    private void SearchTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Guard against event firing during InitializeComponent before controls are ready
        if (SearchTextBox == null || SearchTypeComboBox == null)
        {
            Logger.Debug("SearchTypeComboBox_SelectionChanged called before controls initialized - ignoring");
            return;
        }
        
        // Update placeholder text based on search type
        var searchType = (SearchTypeComboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "TableName";
        var placeholderText = searchType == "ColumnName" 
            ? "Enter column name pattern..." 
            : "Enter table name pattern...";
        
        // Update placeholder - ModernWpf ControlHelper is an attached property set in XAML
        // We can't easily change it programmatically, so we'll just update the tooltip
        // The XAML already has a default placeholder, and users will see the tooltip on hover
        SearchTextBox.ToolTip = placeholderText;
        Logger.Debug("Search type changed to: {SearchType}", searchType);
        
        // Refresh search if there's already text
        if (!string.IsNullOrWhiteSpace(SearchTextBox.Text))
        {
            RefreshTableList();
        }
    }
    
    private void SearchHistoryButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var history = _searchHistoryService.GetHistory();
            if (history.Count == 0)
            {
                MessageBox.Show("No previous search patterns found.", "No History",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            // Create simple selection dialog
            var dialog = new Window
            {
                Title = "Select Previous Search Pattern",
                Width = 600,
                Height = 400,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };
            
            var listBox = new ListBox
            {
                Margin = new Thickness(10)
            };
            
            listBox.ItemsSource = history.Select(h => new
            {
                Display = $"{h.Pattern} ({h.SearchType}, {h.PatternType}) - {h.SavedAt:yyyy-MM-dd HH:mm}",
                Item = h
            });
            
            var okButton = new Button
            {
                Content = "Load",
                Width = 75,
                Height = 25,
                Margin = new Thickness(5),
                IsDefault = true
            };
            
            var cancelButton = new Button
            {
                Content = "Cancel",
                Width = 75,
                Height = 25,
                Margin = new Thickness(5),
                IsCancel = true
            };
            
            okButton.Click += (s, args) =>
            {
                if (listBox.SelectedItem != null)
                {
                    var selected = ((dynamic)listBox.SelectedItem).Item;
                    SearchTextBox.Text = selected.Pattern;
                    
                    // Set search type
                    var searchTypeItem = SearchTypeComboBox.Items.Cast<ComboBoxItem>()
                        .FirstOrDefault(item => item.Tag?.ToString() == selected.SearchType);
                    if (searchTypeItem != null)
                        SearchTypeComboBox.SelectedItem = searchTypeItem;
                    
                    // Set pattern type
                    var patternTypeItem = PatternTypeComboBox.Items.Cast<ComboBoxItem>()
                        .FirstOrDefault(item => item.Tag?.ToString() == selected.PatternType);
                    if (patternTypeItem != null)
                        PatternTypeComboBox.SelectedItem = patternTypeItem;
                    
                    dialog.DialogResult = true;
                    dialog.Close();
                }
            };
            
            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(10)
            };
            buttonPanel.Children.Add(okButton);
            buttonPanel.Children.Add(cancelButton);
            
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            
            Grid.SetRow(listBox, 0);
            Grid.SetRow(buttonPanel, 1);
            grid.Children.Add(listBox);
            grid.Children.Add(buttonPanel);
            
            dialog.Content = grid;
            listBox.Focus();
            
            if (dialog.ShowDialog() == true)
            {
                // Trigger search refresh
                RefreshTableList();
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to show search history");
            MessageBox.Show($"Failed to load search history:\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
    
    private async void SchemaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedSchema = SchemaComboBox.SelectedItem?.ToString() ?? "All";
        
        if (selectedSchema == "All")
        {
            _allTables.Clear();
            TablesList.ItemsSource = new List<TableSelectionItem>();
            StatusText.Text = "Please select a specific schema to load tables";
            UpdateSelectionCounts();
        }
        else
        {
            try
            {
                ShowLoading($"Loading tables from schema {selectedSchema}...");
                await LoadTablesForSchemaAsync(selectedSchema);
                HideLoading();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to load tables for schema: {Schema}", selectedSchema);
                HideLoading();
                MessageBox.Show(
                    $"Failed to load tables from schema {selectedSchema}:\n{ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
    
    private void AddSelected_Click(object sender, RoutedEventArgs e)
    {
        var selected = _allTables.Where(t => t.IsSelected).ToList();
        foreach (var table in selected)
        {
            if (!_selectedTables.Contains(table))
            {
                _selectedTables.Add(table);
            }
        }
        UpdateSelectionCounts();
    }
    
    private void AddAllInSchema_Click(object sender, RoutedEventArgs e)
    {
        var selectedSchema = SchemaComboBox.SelectedItem?.ToString() ?? "All";
        if (selectedSchema == "All")
        {
            foreach (var table in _allTables)
            {
                if (!_selectedTables.Contains(table))
                {
                    _selectedTables.Add(table);
                }
            }
        }
        else
        {
            foreach (var table in _allTables.Where(t => t.Schema == selectedSchema))
            {
                if (!_selectedTables.Contains(table))
                {
                    _selectedTables.Add(table);
                }
            }
        }
        UpdateSelectionCounts();
    }
    
    private async void AddAllFromSearch_Click(object sender, RoutedEventArgs e)
    {
        var searchText = SearchTextBox.Text?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(searchText))
        {
            MessageBox.Show("Please enter a search term first.", "No Search Term",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        
        // Get currently filtered tables (from RefreshTableList result)
        var filteredTables = TablesList.ItemsSource as IEnumerable<TableSelectionItem> ?? 
                            Enumerable.Empty<TableSelectionItem>();
        var matchingTables = filteredTables.ToList();
        
        if (matchingTables.Count == 0)
        {
            MessageBox.Show($"No tables found matching '{SearchTextBox.Text}'.", "No Matches",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        
        var addedCount = 0;
        foreach (var table in matchingTables)
        {
            // Mark as selected in _allTables (this is what UpdateSelectionCounts uses)
            if (!table.IsSelected)
            {
                table.IsSelected = true;
                addedCount++;
            }
        }
        
        // Refresh the table list to show checkboxes as checked
        RefreshTableList();
        UpdateSelectionCounts();
        
        Logger.Info("Added {AddedCount} tables from search '{SearchText}' (total matching: {TotalCount})", 
            addedCount, searchText, matchingTables.Count);
        
        StatusText.Text = $"Added {addedCount} table(s) matching '{SearchTextBox.Text}'";
    }
    
    
    private void RemoveSelected_Click(object sender, RoutedEventArgs e)
    {
        var selected = _allTables.Where(t => t.IsSelected).ToList();
        foreach (var table in selected)
        {
            _selectedTables.Remove(table);
            table.IsSelected = false;
        }
        RefreshTableList();
        UpdateSelectionCounts();
    }
    
    private async void FollowFK_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            FollowFKButton.IsEnabled = false;
            ShowLoading("Following outgoing foreign keys...");
            Logger.Debug("Following outgoing foreign keys...");
            
            var selected = _selectedTables.ToList();
            if (selected.Count == 0)
            {
                MessageBox.Show("Please select at least one table first.", "No Selection",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            Logger.Debug("Following foreign keys from {Count} selected tables", selected.Count);
            
            var relatedTables = new HashSet<string>();
            var provider = _connectionManager.ConnectionInfo.ProviderType?.ToUpperInvariant() ?? "DB2";
            var version = "12.1";
            var metadataHandler = App.MetadataHandler ?? throw new InvalidOperationException("MetadataHandler not initialized");
            
            foreach (var table in selected)
            {
                Logger.Debug("Querying outgoing FKs for {Schema}.{Table}", table.Schema, table.TableName);
                
                var fkQuery = metadataHandler.GetQuery(provider, version, "GetForeignKeysForTable");
                fkQuery = ReplaceParameters(fkQuery, table.Schema, table.TableName);
                var fkResults = await _connectionManager.ExecuteQueryAsync(fkQuery);
                
                foreach (DataRow row in fkResults.Rows)
                {
                    // GetForeignKeysForTable returns REF_SCHEMA and REF_TABLE columns
                    var refSchema = row["REF_SCHEMA"]?.ToString()?.Trim() ?? string.Empty;
                    var refTable = row["REF_TABLE"]?.ToString()?.Trim() ?? string.Empty;
                    var key = $"{refSchema}.{refTable}";
                    
                    Logger.Debug("FK result row: REF_SCHEMA={RefSchema}, REF_TABLE={RefTable}", refSchema, refTable);
                    
                    if (!string.IsNullOrEmpty(refSchema) && !string.IsNullOrEmpty(refTable) &&
                        !relatedTables.Contains(key) && !selected.Any(t => t.FullName == key))
                    {
                        relatedTables.Add(key);
                        Logger.Debug("Found related table via FK: {Schema}.{Table}", refSchema, refTable);
                    }
                }
            }
            
            // Add related tables to selection
            var addedCount = 0;
            foreach (var relatedKey in relatedTables)
            {
                var parts = relatedKey.Split('.');
                if (parts.Length != 2) continue;
                
                var table = _allTables.FirstOrDefault(t => t.Schema == parts[0].Trim() && t.TableName == parts[1].Trim());
                if (table != null && !_selectedTables.Contains(table))
                {
                    _selectedTables.Add(table);
                    addedCount++;
                    Logger.Info("Added table to selection via FK: {Table}", relatedKey);
                }
            }
            
            UpdateSelectionCounts();
            
            if (addedCount > 0)
            {
                Logger.Info("Added {Count} table(s) via foreign key relationships", addedCount);
                StatusText.Text = $"Added {addedCount} table(s) via outgoing foreign keys";
            }
            else
            {
                StatusText.Text = "No additional tables found via outgoing foreign keys";
            }
            
            Logger.Info("FK navigation complete - Added {Count} tables", addedCount);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to follow foreign keys");
            MessageBox.Show($"Error following foreign keys:\n\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            HideLoading();
            FollowFKButton.IsEnabled = true;
        }
    }
    
    private async void FollowIncomingFK_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            FollowIncomingFKButton.IsEnabled = false;
            ShowLoading("Following incoming foreign keys...");
            Logger.Debug("Following incoming foreign keys...");
            
            var selected = _selectedTables.ToList();
            if (selected.Count == 0)
            {
                MessageBox.Show("Please select at least one table first.", "No Selection",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            Logger.Debug("Following incoming foreign keys for {Count} selected tables", selected.Count);
            
            var relatedTables = new HashSet<string>();
            var provider = _connectionManager.ConnectionInfo.ProviderType?.ToUpperInvariant() ?? "DB2";
            var version = "12.1";
            var metadataHandler = App.MetadataHandler ?? throw new InvalidOperationException("MetadataHandler not initialized");
            
            foreach (var table in selected)
            {
                Logger.Debug("Querying incoming FKs for {Schema}.{Table}", table.Schema, table.TableName);
                
                var incomingFkQuery = metadataHandler.GetQuery(provider, version, "GetIncomingForeignKeys");
                incomingFkQuery = ReplaceParameters(incomingFkQuery, table.Schema, table.TableName);
                var fkResults = await _connectionManager.ExecuteQueryAsync(incomingFkQuery);
                
                foreach (DataRow row in fkResults.Rows)
                {
                    var fromSchema = row["REFERENCING_SCHEMA"]?.ToString()?.Trim() ?? string.Empty;
                    var fromTable = row["REFERENCING_TABLE"]?.ToString()?.Trim() ?? string.Empty;
                    var key = $"{fromSchema}.{fromTable}";
                    
                    if (!string.IsNullOrEmpty(fromSchema) && !string.IsNullOrEmpty(fromTable) &&
                        !relatedTables.Contains(key) && !selected.Any(t => t.FullName == key))
                    {
                        relatedTables.Add(key);
                        Logger.Debug("Found table with incoming FK: {Schema}.{Table}", fromSchema, fromTable);
                    }
                }
            }
            
            // Add related tables to selection
            var addedCount = 0;
            foreach (var relatedKey in relatedTables)
            {
                var parts = relatedKey.Split('.');
                if (parts.Length != 2) continue;
                
                var table = _allTables.FirstOrDefault(t => t.Schema == parts[0].Trim() && t.TableName == parts[1].Trim());
                if (table != null && !_selectedTables.Contains(table))
                {
                    _selectedTables.Add(table);
                    addedCount++;
                    Logger.Info("Added table to selection via incoming FK: {Table}", relatedKey);
                }
            }
            
            UpdateSelectionCounts();
            
            if (addedCount > 0)
            {
                Logger.Info("Added {Count} table(s) via incoming foreign key relationships", addedCount);
                StatusText.Text = $"Added {addedCount} table(s) via incoming foreign keys";
            }
            else
            {
                StatusText.Text = "No additional tables found via incoming foreign keys";
            }
            
            Logger.Info("Incoming FK navigation complete - Added {Count} tables", addedCount);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to follow incoming foreign keys");
            MessageBox.Show($"Error following incoming foreign keys:\n\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            HideLoading();
            FollowIncomingFKButton.IsEnabled = true;
        }
    }
    
    private void ClearSelection_Click(object sender, RoutedEventArgs e)
    {
        _selectedTables.Clear();
        foreach (var table in _allTables)
        {
            table.IsSelected = false;
        }
        RefreshTableList();
        UpdateSelectionCounts();
    }
    
    private void BrowseOutputFolder_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new System.Windows.Forms.FolderBrowserDialog
        {
            SelectedPath = _outputFolder,
            Description = "Select output folder for Missing FK Discovery"
        };
        
        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            _outputFolder = dialog.SelectedPath;
            OutputFolderTextBox.Text = _outputFolder;
        }
    }
    
    private void BrowseIgnoreJson_Click(object sender, RoutedEventArgs e)
    {
        // Ensure ignore files folder exists
        if (string.IsNullOrEmpty(_ignoreFilesFolder))
        {
            _ignoreFilesFolder = UserDataFolderHelper.GetMissingFKIgnorePatternsFolder();
        }
        
        var dialog = new OpenFileDialog
        {
            Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
            Title = "Select Ignore JSON File",
            InitialDirectory = _ignoreFilesFolder,
            RestoreDirectory = true
        };
        
        if (dialog.ShowDialog() == true)
        {
            _ignoreJsonPath = dialog.FileName;
            IgnoreJsonTextBox.Text = _ignoreJsonPath;
            Logger.Info("Selected ignore JSON file: {Path}", _ignoreJsonPath);
        }
    }
    
    private async void GenerateInputJson_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_selectedTables.Count == 0)
            {
                MessageBox.Show("Please select at least one table.", "No Tables Selected",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (string.IsNullOrEmpty(_outputFolder) || !Directory.Exists(_outputFolder))
            {
                MessageBox.Show("Please select a valid output folder.", "Invalid Output Folder",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            ShowLoading("Generating input JSON...");
            var generateButton = sender as Button;
            generateButton!.IsEnabled = false;
            
            // Generate job ID
            _jobId = $"{DateTime.UtcNow:yyyy-MM-ddTHH-mm-ssZ}_{Guid.NewGuid():N}";
            
            // Collect metadata for selected tables
            var tableRefs = _selectedTables.Select(t => new TableReference
            {
                Schema = t.Schema,
                Name = t.TableName
            }).ToList();
            
            Logger.Info("Collecting metadata for {Count} tables", tableRefs.Count);
            var tablesMetadata = await _metadataService.CollectTableMetadataAsync(tableRefs);
            
            // Create project-specific folder with timestamp and table names
            var schema = _selectedTables.FirstOrDefault()?.Schema ?? "UNKNOWN";
            var tableNames = _selectedTables.Select(t => t.TableName);
            _outputFolder = UserDataFolderHelper.CreateProjectFolder(schema, tableNames);
            OutputFolderTextBox.Text = _outputFolder;
            Logger.Info("Created project folder: {Path}", _outputFolder);
            
            // Create input model
            var inputModel = new MissingFKInputModel
            {
                JobId = _jobId,
                GeneratedAtUtc = DateTime.UtcNow,
                ConnectionProfile = _connectionProfile,
                Provider = _connectionManager.ConnectionInfo.ProviderType?.ToUpperInvariant() ?? "DB2",
                ProviderVersion = "12.1", // TODO: Get actual version from connection
                Options = new MissingFKOptions
                {
                    MinRowCount = int.TryParse(MinRowCountTextBox.Text, out var minRow) ? minRow : 100,
                    MinMatchRatio = double.TryParse(MinMatchRatioTextBox.Text, out var minMatch) ? minMatch : 0.95,
                    StrongMatchRatio = 0.99,
                    MaxParallelTables = int.TryParse(MaxParallelTablesTextBox.Text, out var maxPar) ? maxPar : 4,
                    ExportFormat = "csv",
                    IncludeNullsInMatch = false
                },
                SelectedTables = tableRefs,
                Tables = tablesMetadata
            };
            
            // Save input JSON to project folder
            var inputPath = Path.Combine(_outputFolder, "missing_fk_input.json");
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            
            var json = JsonSerializer.Serialize(inputModel, options);
            await File.WriteAllTextAsync(inputPath, json, Encoding.UTF8);
            
            Logger.Info("Input JSON generated in project folder: {Path}", inputPath);
            StatusText.Text = $"Input JSON generated: {inputPath}";
            StartBatchJobButton.IsEnabled = true;
            
            MessageBox.Show($"Input JSON generated successfully:\n{inputPath}\n\nProject folder:\n{_outputFolder}", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate input JSON");
            MessageBox.Show($"Failed to generate input JSON:\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            HideLoading();
            if (sender is Button btn)
            {
                btn.IsEnabled = true;
            }
        }
    }
    
    private void StartBatchJob_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Disable button immediately to prevent multiple clicks
            StartBatchJobButton.IsEnabled = false;
            
            // Check if job is already running (via lock file)
            if (_jobStatusService.IsJobLocked(_outputFolder))
            {
                StartBatchJobButton.IsEnabled = false; // Keep disabled
                MessageBox.Show(
                    "A Missing FK Discovery job is already running. Please wait for it to complete before starting another job.\n\n" +
                    "Check the job status below or use 'View Job Log' to see progress.",
                    "Job Already Running",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }
            
            // Check if input JSON exists
            var inputPath = Path.Combine(_outputFolder, "missing_fk_input.json");
            if (!File.Exists(inputPath))
            {
                StartBatchJobButton.IsEnabled = true; // Re-enable if validation fails
                MessageBox.Show("Please generate input JSON first.", "Input JSON Not Found",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            // Save ignore JSON if configured
            if (_currentIgnoreModel.IgnoreColumns.Count > 0 || 
                _currentIgnoreModel.IgnoreColumnPatterns.Count > 0 || 
                _currentIgnoreModel.IgnoreDataTypes.Count > 0)
            {
                // Save to ignore JSON file
                if (string.IsNullOrEmpty(_ignoreJsonPath))
                {
                    _ignoreJsonPath = Path.Combine(_outputFolder, "missing_fk_ignore.json");
                }
                
                var ignoreJson = JsonSerializer.Serialize(_currentIgnoreModel, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(_ignoreJsonPath, ignoreJson, Encoding.UTF8);
                IgnoreJsonTextBox.Text = _ignoreJsonPath;
            }
            
            // Acquire job lock
            if (!_jobStatusService.AcquireJobLock(_outputFolder, _jobId))
            {
                StartBatchJobButton.IsEnabled = true; // Re-enable if lock fails
                MessageBox.Show(
                    "Failed to acquire job lock. Another job may be running.\n\n" +
                    "Please wait a moment and try again, or check if a job is already running.",
                    "Job Lock Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }
            
            // Log ignore rules being used
            var ignoreRulesInfo = "No ignore rules";
            if (!string.IsNullOrEmpty(_ignoreJsonPath) && File.Exists(_ignoreJsonPath))
            {
                try
                {
                    var ignoreJson = File.ReadAllText(_ignoreJsonPath);
                    var ignoreModel = JsonSerializer.Deserialize<MissingFKIgnoreModel>(ignoreJson);
                    if (ignoreModel != null)
                    {
                        var ruleCount = ignoreModel.IgnoreTables.Count + 
                                       ignoreModel.IgnoreColumns.Count + 
                                       ignoreModel.IgnoreColumnPatterns.Count + 
                                       ignoreModel.IgnoreDataTypes.Count;
                        ignoreRulesInfo = $"Using {ruleCount} ignore rule(s) from {Path.GetFileName(_ignoreJsonPath)}";
                        Logger.Info("Ignore rules loaded: {TableRules} tables, {ColumnRules} columns, {PatternRules} patterns, {DataTypeRules} data types",
                            ignoreModel.IgnoreTables.Count, ignoreModel.IgnoreColumns.Count,
                            ignoreModel.IgnoreColumnPatterns.Count, ignoreModel.IgnoreDataTypes.Count);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warn(ex, "Failed to parse ignore JSON for info display");
                }
            }
            
            // Build command line arguments
            var args = new List<string>
            {
                "-profile", _connectionProfile,
                "-command", "missing-fk-scan",
                "-input", inputPath,
                "-out", _outputFolder
            };
            
            if (!string.IsNullOrEmpty(_ignoreJsonPath) && File.Exists(_ignoreJsonPath))
            {
                args.Add("-ignore");
                args.Add(_ignoreJsonPath);
            }
            
            // Get executable path
            var exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            if (string.IsNullOrEmpty(exePath))
            {
                exePath = Process.GetCurrentProcess().MainModule?.FileName ?? "WindowsDb2Editor.exe";
            }
            
            Logger.Info("Starting detached batch job: {Exe} {Args}", exePath, string.Join(" ", args));
            
            var processInfo = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = string.Join(" ", args.Select(a => $"\"{a}\"")),
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            
            _runningJobProcess = Process.Start(processInfo);
            if (_runningJobProcess != null)
            {
                Logger.Info("Batch job started with PID: {Pid}", _runningJobProcess.Id);
                
                // Show clear indication that job started
                var ignoreInfo = string.IsNullOrEmpty(_ignoreJsonPath) 
                    ? "No ignore rules" 
                    : $"Ignore rules: {Path.GetFileName(_ignoreJsonPath)}";
                
                StatusText.Text = $"✓ Batch job STARTED - Job ID: {_jobId}";
                JobStatusText.Text = $"Job running (PID: {_runningJobProcess.Id}) - {ignoreInfo}";
                JobStatusText.Foreground = System.Windows.Media.Brushes.Green;
                
                // Show notification
                MessageBox.Show(
                    $"Missing FK Discovery batch job has been started!\n\n" +
                    $"Job ID: {_jobId}\n" +
                    $"Process ID: {_runningJobProcess.Id}\n" +
                    $"{ignoreInfo}\n\n" +
                    $"The job is running in the background. You can monitor progress using the 'View Job Log' button.\n" +
                    $"Status will update automatically as the job progresses.",
                    "Job Started",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                
                // Start status monitoring
                StartJobStatusMonitoring(_jobId, _outputFolder);
                
                // Start log file monitoring and switch to Job Progress tab
                StartLogFileMonitoring(_jobId, _outputFolder);
                MainTabControl.SelectedItem = JobProgressTab;
                
                // Update UI - disable button to prevent multiple starts
                StartBatchJobButton.IsEnabled = false;
                StartBatchJobButton.Content = "Job Running...";
                ViewJobLogButton.IsEnabled = true;
                
                Logger.Info("Job status monitoring started");
            }
            else
            {
                _jobStatusService.ReleaseJobLock(_outputFolder);
                throw new InvalidOperationException("Failed to start batch job process");
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to start batch job");
            _jobStatusService.ReleaseJobLock(_outputFolder);
            MessageBox.Show($"Failed to start batch job:\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void OpenOutputFolder_Click(object sender, RoutedEventArgs e)
    {
        if (Directory.Exists(_outputFolder))
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = _outputFolder,
                UseShellExecute = true
            });
        }
        else
        {
            MessageBox.Show("Output folder does not exist.", "Error",
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
    
    private void OpenJobLog_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(_jobId))
        {
            MessageBox.Show("No job has been started yet.", "No Job",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        
        var logPath = MissingFKJobStatusService.GetLogFilePath(_outputFolder, _jobId);
        if (File.Exists(logPath))
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = logPath,
                UseShellExecute = true
            });
        }
        else
        {
            MessageBox.Show($"Job log not found:\n{logPath}", "Log Not Found",
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
    
    private void EditIgnoreRules_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var dialog = new MissingFKIgnoreRulesDialog(_currentIgnoreModel)
            {
                Owner = this
            };
            
            if (dialog.ShowDialog() == true)
            {
                _currentIgnoreModel = dialog.IgnoreModel;
                UpdateIgnoreRulesSummary();
                
                // Update ignore JSON path if needed
                if (!string.IsNullOrEmpty(_ignoreJsonPath))
                {
                    var ignoreJson = JsonSerializer.Serialize(_currentIgnoreModel, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });
                    File.WriteAllText(_ignoreJsonPath, ignoreJson, Encoding.UTF8);
                }
                
                Logger.Info("Ignore rules updated from editor dialog");
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to open ignore rules editor");
            MessageBox.Show($"Failed to open ignore rules editor:\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void UpdateIgnoreRulesSummary()
    {
        var parts = new List<string>();
        if (_currentIgnoreModel.IgnoreColumns.Count > 0)
            parts.Add($"{_currentIgnoreModel.IgnoreColumns.Count} column(s)");
        if (_currentIgnoreModel.IgnoreColumnPatterns.Count > 0)
            parts.Add($"{_currentIgnoreModel.IgnoreColumnPatterns.Count} pattern(s)");
        if (_currentIgnoreModel.IgnoreDataTypes.Count > 0)
            parts.Add($"{_currentIgnoreModel.IgnoreDataTypes.Count} data type(s)");
        
        if (parts.Count > 0)
        {
            IgnoreRulesSummaryText.Text = string.Join(", ", parts);
            IgnoreRulesSummaryText.FontStyle = FontStyles.Normal;
            IgnoreRulesSummaryText.Foreground = System.Windows.Media.Brushes.White;
        }
        else
        {
            IgnoreRulesSummaryText.Text = "No ignore rules configured";
            IgnoreRulesSummaryText.FontStyle = FontStyles.Italic;
            IgnoreRulesSummaryText.Foreground = System.Windows.Media.Brushes.Gray;
        }
    }
    
    // Old methods removed - functionality moved to MissingFKIgnoreRulesDialog
    
    private void ViewJobLog_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(_jobId))
        {
            MessageBox.Show("No job has been started yet.", "No Job",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        
        var logPath = MissingFKJobStatusService.GetLogFilePath(_outputFolder, _jobId);
        if (!File.Exists(logPath))
        {
            MessageBox.Show($"Job log not found:\n{logPath}", "Log Not Found",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        
        var logViewer = new MissingFKJobLogViewerDialog(logPath, _jobId);
        logViewer.Owner = this;
        logViewer.Show();
    }
    
    private void StartJobStatusMonitoring(string jobId, string outputFolder)
    {
        try
        {
            Logger.Debug("Starting job status monitoring for job: {JobId}", jobId);
            
            // Create FileSystemWatcher for status file
            _statusFileWatcher = new FileSystemWatcher(outputFolder)
            {
                Filter = $"missing_fk_status_{jobId}.json",
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.CreationTime | NotifyFilters.Size
            };
            
            _statusFileWatcher.Created += OnStatusFileChanged;
            _statusFileWatcher.Changed += OnStatusFileChanged;
            _statusFileWatcher.Deleted += OnStatusFileDeleted;
            _statusFileWatcher.EnableRaisingEvents = true;
            
            // Also start polling timer as fallback (every 2 seconds)
            _statusPollTimer = new Timer(CheckJobStatus, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));
            
            Logger.Info("Job status monitoring started for job: {JobId}", jobId);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to start job status monitoring");
        }
    }
    
    private void StopJobStatusMonitoring()
    {
        try
        {
            if (_statusFileWatcher != null)
            {
                _statusFileWatcher.EnableRaisingEvents = false;
                _statusFileWatcher.Dispose();
                _statusFileWatcher = null;
            }
            
            if (_statusPollTimer != null)
            {
                _statusPollTimer.Dispose();
                _statusPollTimer = null;
            }
            
            Logger.Debug("Job status monitoring stopped");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error stopping job status monitoring");
        }
    }
    
    private void StartLogFileMonitoring(string jobId, string outputFolder)
    {
        try
        {
            Logger.Debug("Starting log file monitoring for job: {JobId}", jobId);
            
            var logPath = MissingFKJobStatusService.GetLogFilePath(outputFolder, jobId);
            
            // Create FileSystemWatcher for log file
            _logFileWatcher = new FileSystemWatcher(outputFolder)
            {
                Filter = $"missing_fk_job_{jobId}.log",
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size
            };
            
            _logFileWatcher.Changed += OnLogFileChanged;
            _logFileWatcher.EnableRaisingEvents = true;
            
            // Load initial log content if file exists
            if (File.Exists(logPath))
            {
                LoadLogFileContent(logPath);
            }
            
            // Also poll every 1 second to catch updates
            _logPollTimer = new Timer(_ => 
            {
                if (File.Exists(logPath))
                {
                    Dispatcher.Invoke(() => LoadLogFileContent(logPath));
                }
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            
            Logger.Info("Log file monitoring started for job: {JobId}", jobId);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to start log file monitoring");
        }
    }
    
    private void StopLogFileMonitoring()
    {
        try
        {
            if (_logFileWatcher != null)
            {
                _logFileWatcher.EnableRaisingEvents = false;
                _logFileWatcher.Dispose();
                _logFileWatcher = null;
            }
            
            if (_logPollTimer != null)
            {
                _logPollTimer.Dispose();
                _logPollTimer = null;
            }
            
            Logger.Debug("Log file monitoring stopped");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error stopping log file monitoring");
        }
    }
    
    private void OnLogFileChanged(object sender, FileSystemEventArgs e)
    {
        try
        {
            // Small delay to ensure file write is complete
            Task.Delay(100).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() => LoadLogFileContent(e.FullPath));
            });
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error handling log file change");
        }
    }
    
    private void LoadLogFileContent(string logPath)
    {
        try
        {
            if (!File.Exists(logPath))
            {
                JobLogTextBox.Text = "Log file not found. Waiting for job to start...";
                return;
            }
            
            // Read file with retry logic (file might be locked)
            string content = string.Empty;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    content = File.ReadAllText(logPath, Encoding.UTF8);
                    break;
                }
                catch (IOException)
                {
                    if (i < 2) Thread.Sleep(100);
                    else content = "Unable to read log file (file may be locked)";
                }
            }
            
            JobLogTextBox.Text = content;
            
            // Auto-scroll to bottom
            JobLogTextBox.CaretIndex = JobLogTextBox.Text.Length;
            JobLogTextBox.ScrollToEnd();
            
            // Update status text
            var lines = content.Split('\n');
            var lastLine = lines.LastOrDefault(l => !string.IsNullOrWhiteSpace(l));
            if (!string.IsNullOrEmpty(lastLine))
            {
                JobProgressStatusText.Text = $"Job running... Last update: {lastLine.Trim()}";
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error loading log file content");
            JobLogTextBox.Text = $"Error reading log file: {ex.Message}";
        }
    }
    
    private void RefreshJobLog_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(_jobId)) return;
        
        var logPath = MissingFKJobStatusService.GetLogFilePath(_outputFolder, _jobId);
        LoadLogFileContent(logPath);
    }
    
    private void ClearJobLog_Click(object sender, RoutedEventArgs e)
    {
        JobLogTextBox.Clear();
        JobProgressStatusText.Text = "Log cleared. Waiting for updates...";
    }
    
    private void OnStatusFileChanged(object sender, FileSystemEventArgs e)
    {
        Dispatcher.Invoke(() => CheckJobStatus(null));
    }
    
    private void OnStatusFileDeleted(object sender, FileSystemEventArgs e)
    {
        Dispatcher.Invoke(() =>
        {
            Logger.Info("Status file deleted - job completed");
            JobStatusText.Text = "Job completed successfully";
            JobStatusText.Foreground = System.Windows.Media.Brushes.Green;
            StartBatchJobButton.IsEnabled = true;
            StartBatchJobButton.Content = "Start Batch Job";
            ViewJobLogButton.IsEnabled = true; // Keep enabled so user can view final log
            
            // Show completion notification
            ShowJobCompletionNotification(true, "Missing FK Discovery job completed successfully.");
            
            // Update log one final time
            var logPath = MissingFKJobStatusService.GetLogFilePath(_outputFolder, _jobId);
            if (File.Exists(logPath))
            {
                LoadLogFileContent(logPath);
            }
            JobProgressStatusText.Text = "Job completed successfully";
            
            StopJobStatusMonitoring();
            StopLogFileMonitoring();
        });
    }
    
    private void CheckJobStatus(object? state)
    {
        try
        {
            if (string.IsNullOrEmpty(_jobId)) return;
            
            var status = _jobStatusService.ReadStatusFile(_outputFolder, _jobId);
            if (status == null)
            {
                // Status file doesn't exist - job may have completed
                if (_jobStatusService.IsJobRunning(_outputFolder, _jobId))
                {
                    // Still running, just no status file yet
                    return;
                }
                
                // Job completed
                Dispatcher.Invoke(() =>
                {
                    JobStatusText.Text = "Job completed successfully";
                    JobStatusText.Foreground = System.Windows.Media.Brushes.Green;
                    StartBatchJobButton.IsEnabled = true;
                    StartBatchJobButton.Content = "Start Batch Job";
                    ViewJobLogButton.IsEnabled = true; // Keep enabled so user can view final log
                    
                    // Update log one final time
                    var logPath = MissingFKJobStatusService.GetLogFilePath(_outputFolder, _jobId);
                    if (File.Exists(logPath))
                    {
                        LoadLogFileContent(logPath);
                    }
                    JobProgressStatusText.Text = "Job completed successfully";
                    
                    StopJobStatusMonitoring();
                    StopLogFileMonitoring();
                });
                return;
            }
            
            Dispatcher.Invoke(() =>
            {
                var progress = status.Progress;
                var statusText = $"Job {status.Status} - {progress.Phase}";
                if (progress.TotalTables > 0)
                {
                    statusText += $" ({progress.TablesScanned}/{progress.TotalTables} tables)";
                }
                if (!string.IsNullOrEmpty(progress.CurrentTable))
                {
                    statusText += $" - {progress.CurrentTable}";
                }
                
                JobStatusText.Text = statusText;
                StartBatchJobButton.IsEnabled = false;
                ViewJobLogButton.IsEnabled = true;
                
            if (status.Status == "error")
            {
                JobStatusText.Text = $"Job ERROR: {status.Error ?? "Unknown error"}";
                JobStatusText.Foreground = System.Windows.Media.Brushes.Red;
                StartBatchJobButton.IsEnabled = true;
                StartBatchJobButton.Content = "Start Batch Job";
                ViewJobLogButton.IsEnabled = true;
                ShowJobCompletionNotification(false, status.Error ?? "Job failed with unknown error.");
                StopJobStatusMonitoring();
            }
            else if (status.Status == "completed")
            {
                JobStatusText.Text = "Job completed successfully";
                JobStatusText.Foreground = System.Windows.Media.Brushes.Green;
                StartBatchJobButton.IsEnabled = true;
                StartBatchJobButton.Content = "Start Batch Job";
            }
            });
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error checking job status");
        }
    }
    
    private void ShowJobCompletionNotification(bool success, string message)
    {
        var title = success ? "Job Completed" : "Job Failed";
        var details = $"{message}\n\nJob ID: {_jobId}\nOutput folder: {_outputFolder}";
        
        // Use NotificationService to create tray notification
        var notificationService = new NotificationService();
        notificationService.ShowJobCompletion("Missing FK Discovery", success, details);
        
        // Also show MessageBox as fallback if tray icon is not running
        var icon = success ? MessageBoxImage.Information : MessageBoxImage.Error;
        MessageBox.Show(
            details,
            title,
            MessageBoxButton.OK,
            icon);
    }
    
    private string ReplaceParameters(string sql, params string[] values)
    {
        var result = sql;
        foreach (var value in values)
        {
            var index = result.IndexOf('?');
            if (index >= 0)
            {
                var quotedValue = $"'{value.Replace("'", "''")}'";
                result = result.Substring(0, index) + quotedValue + result.Substring(index + 1);
            }
        }
        return result;
    }
}
