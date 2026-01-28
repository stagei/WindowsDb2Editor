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
    private readonly string _connectionProfile;
    
    private List<string> _availableSchemas = new();
    private List<TableSelectionItem> _allTables = new();
    private List<TableSelectionItem> _selectedTables = new();
    private string _outputFolder = string.Empty;
    private string _ignoreJsonPath = string.Empty;
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
        InitializeComponent();
        
        _connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));
        _connectionProfile = connectionProfile ?? throw new ArgumentNullException(nameof(connectionProfile));
        
        // Initialize services
        var sqlMermaidService = new SqlMermaidIntegrationService();
        _sqlTranslationService = new MissingFKSqlTranslationService(sqlMermaidService);
        _metadataService = new MissingFKMetadataService(_sqlTranslationService, _connectionManager);
        _ignoreService = new MissingFKIgnoreService();
        _ignoreHistoryService = new MissingFKIgnoreHistoryService();
        _jobStatusService = new MissingFKJobStatusService();
        
        // Set default output folder
        _outputFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "WindowsDb2Editor",
            "MissingFK");
        
        if (!Directory.Exists(_outputFolder))
        {
            Directory.CreateDirectory(_outputFolder);
        }
        
        OutputFolderTextBox.Text = _outputFolder;
        
        // Initialize ignore columns grid
        IgnoreColumnsGrid.ItemsSource = _ignoreColumnsCollection;
        
        Loaded += MissingFKDiscoveryDialog_Loaded;
        Closing += MissingFKDiscoveryDialog_Closing;
    }
    
    private void MissingFKDiscoveryDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        StopJobStatusMonitoring();
        StopLogFileMonitoring();
    }
    
    private async void MissingFKDiscoveryDialog_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            Logger.Info("Loading schemas for Missing FK Discovery");
            ShowLoading("Loading schemas from database...");
            
            // Only load schemas initially, not tables
            await LoadSchemasAsync();
            PopulateSchemaComboBox();
            await LoadIgnoreHistoryAsync();
            
            HideLoading();
            StatusText.Text = "Ready - Please select a schema to load tables";
            TablesList.ItemsSource = new List<TableSelectionItem>(); // Empty list until schema selected
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load schemas");
            HideLoading();
            MessageBox.Show(
                $"Failed to load schemas: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
    
    private void ShowLoading(string message)
    {
        LoadingText.Text = message;
        LoadingOverlay.Visibility = Visibility.Visible;
    }
    
    private void HideLoading()
    {
        LoadingOverlay.Visibility = Visibility.Collapsed;
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
    
    private void RefreshTableList()
    {
        var searchText = SearchTextBox.Text?.ToLowerInvariant() ?? string.Empty;
        var selectedSchema = SchemaComboBox.SelectedItem?.ToString() ?? "All";
        
        var filtered = _allTables.Where(t =>
            (string.IsNullOrEmpty(searchText) || t.TableName.ToLowerInvariant().Contains(searchText) || t.Schema.ToLowerInvariant().Contains(searchText)) &&
            (selectedSchema == "All" || t.Schema == selectedSchema)
        ).ToList();
        
        TablesList.ItemsSource = filtered;
        UpdateSelectionCounts();
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
    
    private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        RefreshTableList();
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
        var dialog = new OpenFileDialog
        {
            Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
            Title = "Select Ignore JSON File"
        };
        
        if (dialog.ShowDialog() == true)
        {
            _ignoreJsonPath = dialog.FileName;
            IgnoreJsonTextBox.Text = _ignoreJsonPath;
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
            
            // Save input JSON
            var inputPath = Path.Combine(_outputFolder, "missing_fk_input.json");
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            
            var json = JsonSerializer.Serialize(inputModel, options);
            await File.WriteAllTextAsync(inputPath, json, Encoding.UTF8);
            
            Logger.Info("Input JSON generated: {Path}", inputPath);
            StatusText.Text = $"Input JSON generated: {inputPath}";
            StartBatchJobButton.IsEnabled = true;
            
            MessageBox.Show($"Input JSON generated successfully:\n{inputPath}", "Success",
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
            
            // Save ignore JSON if editor has been used
            if (IgnoreColumnEditorExpander.IsExpanded)
            {
                _currentIgnoreModel.IgnoreColumns = _ignoreColumnsCollection.ToList();
                _currentIgnoreModel.IgnoreColumnPatterns = IgnorePatternsTextBox.Text
                    .Split(new[] { Environment.NewLine, "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim())
                    .Where(p => !string.IsNullOrEmpty(p))
                    .ToList();
                _currentIgnoreModel.IgnoreDataTypes = IgnoreDataTypesTextBox.Text
                    .Split(',')
                    .Select(d => d.Trim())
                    .Where(d => !string.IsNullOrEmpty(d))
                    .ToList();
                
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
    
    private async Task LoadIgnoreHistoryAsync()
    {
        try
        {
            var history = _ignoreHistoryService.GetHistory();
            IgnoreHistoryComboBox.Items.Clear();
            foreach (var item in history)
            {
                IgnoreHistoryComboBox.Items.Add($"{item.Name} ({item.SavedAt:yyyy-MM-dd HH:mm})");
            }
            Logger.Debug("Loaded {Count} ignore configurations from history", history.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load ignore history");
        }
    }
    
    private void IgnoreHistoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (IgnoreHistoryComboBox.SelectedIndex < 0) return;
        
        try
        {
            var history = _ignoreHistoryService.GetHistory();
            if (IgnoreHistoryComboBox.SelectedIndex >= history.Count) return;
            
            var selectedItem = history[IgnoreHistoryComboBox.SelectedIndex];
            var ignoreModel = _ignoreHistoryService.LoadIgnoreConfig(selectedItem.Name);
            
            if (ignoreModel != null)
            {
                _currentIgnoreModel = ignoreModel;
                UpdateIgnoreColumnsGrid();
                IgnorePatternsTextBox.Text = string.Join(Environment.NewLine, ignoreModel.IgnoreColumnPatterns);
                IgnoreDataTypesTextBox.Text = string.Join(", ", ignoreModel.IgnoreDataTypes);
                Logger.Info("Loaded ignore configuration from history: {Name}", selectedItem.Name);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load ignore configuration from history");
            MessageBox.Show($"Failed to load ignore configuration:\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
    
    private async void SaveIgnoreConfig_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Get current ignore model from UI
            _currentIgnoreModel.IgnoreColumns = _ignoreColumnsCollection.ToList();
            _currentIgnoreModel.IgnoreColumnPatterns = IgnorePatternsTextBox.Text
                .Split(new[] { Environment.NewLine, "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .Where(p => !string.IsNullOrEmpty(p))
                .ToList();
            _currentIgnoreModel.IgnoreDataTypes = IgnoreDataTypesTextBox.Text
                .Split(',')
                .Select(d => d.Trim())
                .Where(d => !string.IsNullOrEmpty(d))
                .ToList();
            
            // Prompt for name using simple input dialog
            var inputDialog = new Window
            {
                Title = "Save Ignore Configuration",
                Width = 400,
                Height = 150,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };
            
            var textBox = new TextBox
            {
                Text = $"Ignore_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}",
                Margin = new Thickness(10),
                VerticalContentAlignment = VerticalAlignment.Center
            };
            
            var okButton = new Button
            {
                Content = "OK",
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
            
            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(10)
            };
            stackPanel.Children.Add(okButton);
            stackPanel.Children.Add(cancelButton);
            
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            
            Grid.SetRow(textBox, 0);
            Grid.SetRow(stackPanel, 1);
            grid.Children.Add(textBox);
            grid.Children.Add(stackPanel);
            
            inputDialog.Content = grid;
            
            bool? result = null;
            okButton.Click += (s, args) => { result = true; inputDialog.Close(); };
            cancelButton.Click += (s, args) => { result = false; inputDialog.Close(); };
            
            inputDialog.ShowDialog();
            
            if (result != true || string.IsNullOrWhiteSpace(textBox.Text))
                return;
            
            var name = textBox.Text.Trim();
            _ignoreHistoryService.SaveIgnoreConfig(_currentIgnoreModel, name);
            await LoadIgnoreHistoryAsync();
            
            MessageBox.Show($"Ignore configuration saved: {name}", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to save ignore configuration");
            MessageBox.Show($"Failed to save ignore configuration:\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void UpdateIgnoreColumnsGrid()
    {
        _ignoreColumnsCollection.Clear();
        foreach (var col in _currentIgnoreModel.IgnoreColumns)
        {
            _ignoreColumnsCollection.Add(col);
        }
    }
    
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
        var icon = success ? MessageBoxImage.Information : MessageBoxImage.Error;
        
        MessageBox.Show(
            $"{message}\n\nJob ID: {_jobId}\nOutput folder: {_outputFolder}",
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
