using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    private readonly string _connectionProfile;
    
    private List<TableSelectionItem> _allTables = new();
    private List<TableSelectionItem> _selectedTables = new();
    private string _outputFolder = string.Empty;
    private string _ignoreJsonPath = string.Empty;
    private string _jobId = string.Empty;
    
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
        
        Loaded += MissingFKDiscoveryDialog_Loaded;
    }
    
    private async void MissingFKDiscoveryDialog_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            Logger.Info("Loading tables for Missing FK Discovery");
            ShowLoading("Loading tables from database...");
            
            await LoadTablesAsync();
            PopulateSchemaComboBox();
            RefreshTableList();
            
            HideLoading();
            StatusText.Text = $"Ready - {_allTables.Count} tables loaded";
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load tables");
            HideLoading();
            MessageBox.Show(
                $"Failed to load tables: {ex.Message}",
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
    
    private async Task LoadTablesAsync()
    {
        Logger.Debug("Loading all user tables");
        
        var provider = _connectionManager.ConnectionInfo.ProviderType?.ToUpperInvariant() ?? "DB2";
        var version = "12.1";
        
        // Use MetadataHandler to get SQL for listing tables
        var metadataHandler = App.MetadataHandler ?? throw new InvalidOperationException("MetadataHandler not initialized");
        var sql = metadataHandler.GetQuery(provider, version, "GetTablesForSchema");
        
        // Get all schemas first
        var schemasSql = metadataHandler.GetQuery(provider, version, "GetSchemasStatement");
        var schemasResult = await _connectionManager.ExecuteQueryAsync(schemasSql);
        
        _allTables.Clear();
        foreach (DataRow schemaRow in schemasResult.Rows)
        {
            var schema = schemaRow["SCHEMANAME"]?.ToString()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(schema) || schema.StartsWith("SYS", StringComparison.OrdinalIgnoreCase))
                continue;
            
            // Get tables for this schema
            var tablesSql = ReplaceParameters(sql, schema);
            var tablesResult = await _connectionManager.ExecuteQueryAsync(tablesSql);
            
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
        }
        
        Logger.Info("Loaded {Count} tables from {SchemaCount} schemas", 
            _allTables.Count, _allTables.Select(t => t.Schema).Distinct().Count());
    }
    
    private void PopulateSchemaComboBox()
    {
        var schemas = _allTables.Select(t => t.Schema).Distinct().OrderBy(s => s).ToList();
        SchemaComboBox.Items.Clear();
        SchemaComboBox.Items.Add("All");
        foreach (var schema in schemas)
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
    
    private void SchemaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        RefreshTableList();
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
            var inputPath = Path.Combine(_outputFolder, "missing_fk_input.json");
            if (!File.Exists(inputPath))
            {
                MessageBox.Show("Please generate input JSON first.", "Input JSON Not Found",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
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
            
            var process = Process.Start(processInfo);
            if (process != null)
            {
                Logger.Info("Batch job started with PID: {Pid}", process.Id);
                StatusText.Text = $"Batch job started (Job ID: {_jobId}, PID: {process.Id}). Log: {Path.Combine(_outputFolder, $"job_{_jobId}_log.txt")}";
                
                MessageBox.Show(
                    $"Batch job started successfully.\n\nJob ID: {_jobId}\nProcess ID: {process.Id}\n\n" +
                    $"The job will continue running in the background even if you close this dialog.\n" +
                    $"Check the job log for progress: {Path.Combine(_outputFolder, $"job_{_jobId}_log.txt")}",
                    "Batch Job Started",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                throw new InvalidOperationException("Failed to start batch job process");
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to start batch job");
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
        
        var logPath = Path.Combine(_outputFolder, $"job_{_jobId}_log.txt");
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
