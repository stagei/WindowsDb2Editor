using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Models;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.Dialogs;

/// <summary>
/// Dialog for comparing schemas across different database connections
/// </summary>
public partial class CrossDatabaseComparisonDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly ConnectionProfileService _profileService;
    private readonly MetadataHandler _metadataHandler;
    
    private DB2ConnectionManager? _sourceConnection;
    private DB2ConnectionManager? _targetConnection;
    private List<DB2Connection> _profiles = new();

    public CrossDatabaseComparisonDialog()
    {
        InitializeComponent();
        _profileService = new ConnectionProfileService();
        _metadataHandler = new MetadataHandler();

        Loaded += async (s, e) => 
        {
            // Apply grid preferences to all grids in this dialog
            if (App.PreferencesService != null)
            {
                GridStyleHelper.ApplyGridStylesToWindow(this, App.PreferencesService.Preferences);
            }
            await LoadConnectionProfilesAsync();
        };
    }

    private async Task LoadConnectionProfilesAsync()
    {
        try
        {
            Logger.Info("Loading connection profiles for cross-database comparison");
            
            _profiles = _profileService.LoadAllProfiles();
            
            if (_profiles.Count == 0)
            {
                MessageBox.Show(
                    "No connection profiles found.\n\nPlease create at least two connection profiles using File → New Connection before using this feature.",
                    "No Connection Profiles",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                Close();
                return;
            }
            
            // Populate connection dropdowns
            var profileNames = _profiles.Select(p => p.Name).OrderBy(n => n).ToList();
            
            SourceConnectionComboBox.ItemsSource = profileNames;
            TargetConnectionComboBox.ItemsSource = profileNames;
            
            Logger.Info("Loaded {Count} connection profiles", _profiles.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error loading connection profiles");
            MessageBox.Show($"Error loading connection profiles: {ex.Message}", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void SourceConnection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SourceConnectionComboBox.SelectedItem == null) return;
        
        var profileName = SourceConnectionComboBox.SelectedItem.ToString();
        await ConnectAndLoadSchemasAsync(profileName!, true);
    }

    private async void TargetConnection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (TargetConnectionComboBox.SelectedItem == null) return;
        
        var profileName = TargetConnectionComboBox.SelectedItem.ToString();
        await ConnectAndLoadSchemasAsync(profileName!, false);
    }

    private async Task ConnectAndLoadSchemasAsync(string profileName, bool isSource)
    {
        var infoText = isSource ? SourceInfoText : TargetInfoText;
        var schemaComboBox = isSource ? SourceSchemaComboBox : TargetSchemaComboBox;
        
        try
        {
            infoText.Text = "Connecting...";
            schemaComboBox.ItemsSource = null;
            
            var profile = _profiles.FirstOrDefault(p => p.Name == profileName);
            if (profile == null)
            {
                infoText.Text = "Profile not found";
                return;
            }
            
            Logger.Info("Connecting to {Profile} for {Side} comparison", profileName, isSource ? "source" : "target");
            
            // Create connection
            var connectionManager = new DB2ConnectionManager(profile);
            await connectionManager.OpenAsync();
            
            if (isSource)
            {
                _sourceConnection?.Dispose();
                _sourceConnection = connectionManager;
            }
            else
            {
                _targetConnection?.Dispose();
                _targetConnection = connectionManager;
            }
            
            // Load schemas
            var sql = _metadataHandler.GetQuery("DB2", "12.1", "GetSchemasStatement");
            var result = await connectionManager.ExecuteQueryAsync(sql);
            
            var schemas = new List<string>();
            foreach (DataRow row in result.Rows)
            {
                var schemaName = row["SCHEMANAME"]?.ToString()?.Trim() ?? "";
                if (!string.IsNullOrEmpty(schemaName) && !schemaName.StartsWith("SYS"))
                {
                    schemas.Add(schemaName);
                }
            }
            
            schemaComboBox.ItemsSource = schemas.OrderBy(s => s).ToList();
            infoText.Text = $"Connected - {schemas.Count} schemas available";
            
            // Auto-select if only one schema
            if (schemas.Count == 1)
            {
                schemaComboBox.SelectedIndex = 0;
            }
            
            Logger.Info("Loaded {Count} schemas from {Profile}", schemas.Count, profileName);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error connecting to {Profile}", profileName);
            infoText.Text = $"Connection failed: {ex.Message}";
            
            if (isSource)
            {
                _sourceConnection?.Dispose();
                _sourceConnection = null;
            }
            else
            {
                _targetConnection?.Dispose();
                _targetConnection = null;
            }
        }
    }

    private async void Compare_Click(object sender, RoutedEventArgs e)
    {
        // Validate selections
        if (_sourceConnection == null)
        {
            MessageBox.Show("Please select and connect to a source database.", "Select Source", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        
        if (_targetConnection == null)
        {
            MessageBox.Show("Please select and connect to a target database.", "Select Target", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        
        if (SourceSchemaComboBox.SelectedItem == null)
        {
            MessageBox.Show("Please select a source schema.", "Select Schema", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        
        if (TargetSchemaComboBox.SelectedItem == null)
        {
            MessageBox.Show("Please select a target schema.", "Select Schema", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            CompareButton.IsEnabled = false;
            CompareButton.Content = "⏳ Comparing...";
            ProgressBar.Visibility = Visibility.Visible;
            ProgressBar.IsIndeterminate = true;

            var sourceSchema = SourceSchemaComboBox.SelectedItem.ToString()!;
            var targetSchema = TargetSchemaComboBox.SelectedItem.ToString()!;
            var sourceProfile = SourceConnectionComboBox.SelectedItem.ToString()!;
            var targetProfile = TargetConnectionComboBox.SelectedItem.ToString()!;

            Logger.Info("Comparing {SourceProfile}.{SourceSchema} vs {TargetProfile}.{TargetSchema}", 
                sourceProfile, sourceSchema, targetProfile, targetSchema);

            // Get tables from both databases
            var sourceTablesQuery = _metadataHandler.GetQuery("DB2", "12.1", "GetTablesForSchema")
                .Replace("TRIM(TABSCHEMA) = ?", $"TRIM(TABSCHEMA) = '{sourceSchema}'");
            var targetTablesQuery = _metadataHandler.GetQuery("DB2", "12.1", "GetTablesForSchema")
                .Replace("TRIM(TABSCHEMA) = ?", $"TRIM(TABSCHEMA) = '{targetSchema}'");

            var sourceTables = await _sourceConnection.ExecuteQueryAsync(sourceTablesQuery);
            var targetTables = await _targetConnection.ExecuteQueryAsync(targetTablesQuery);

            // Extract table names
            var sourceTableNames = new HashSet<string>();
            var targetTableNames = new HashSet<string>();

            foreach (DataRow row in sourceTables.Rows)
            {
                sourceTableNames.Add(row["TABNAME"]?.ToString()?.Trim() ?? "");
            }

            foreach (DataRow row in targetTables.Rows)
            {
                targetTableNames.Add(row["TABNAME"]?.ToString()?.Trim() ?? "");
            }

            var onlyInSource = sourceTableNames.Except(targetTableNames).OrderBy(t => t).ToList();
            var onlyInTarget = targetTableNames.Except(sourceTableNames).OrderBy(t => t).ToList();
            var inBoth = sourceTableNames.Intersect(targetTableNames).OrderBy(t => t).ToList();

            // Build summary
            var summary = new StringBuilder();
            summary.AppendLine($"📊 Cross-Database Schema Comparison Results");
            summary.AppendLine($"═══════════════════════════════════════════════════════");
            summary.AppendLine();
            summary.AppendLine($"📥 Source: {sourceProfile} → {sourceSchema}");
            summary.AppendLine($"📤 Target: {targetProfile} → {targetSchema}");
            summary.AppendLine();
            summary.AppendLine($"📋 Table Summary:");
            summary.AppendLine($"   ✅ Tables in both databases: {inBoth.Count}");
            summary.AppendLine($"   ➕ Only in source: {onlyInSource.Count}");
            summary.AppendLine($"   ➖ Only in target: {onlyInTarget.Count}");
            summary.AppendLine();
            summary.AppendLine($"Total tables compared: {sourceTableNames.Count} (source) vs {targetTableNames.Count} (target)");

            SummaryText.Text = summary.ToString();

            // Populate lists
            OnlyInSourceList.Items.Clear();
            OnlyInTargetList.Items.Clear();
            InBothList.Items.Clear();

            OnlyInSourceHeader.Text = $"Tables that exist only in {sourceProfile}.{sourceSchema} ({onlyInSource.Count}):";
            OnlyInTargetHeader.Text = $"Tables that exist only in {targetProfile}.{targetSchema} ({onlyInTarget.Count}):";

            foreach (var table in onlyInSource)
            {
                OnlyInSourceList.Items.Add($"{sourceSchema}.{table}");
            }

            foreach (var table in onlyInTarget)
            {
                OnlyInTargetList.Items.Add($"{targetSchema}.{table}");
            }

            foreach (var table in inBoth)
            {
                InBothList.Items.Add($"{sourceSchema}.{table} ↔ {targetSchema}.{table}");
            }

            // Generate migration DDL
            var ddl = new StringBuilder();
            ddl.AppendLine($"-- Cross-Database Migration DDL");
            ddl.AppendLine($"-- Source: {sourceProfile}.{sourceSchema}");
            ddl.AppendLine($"-- Target: {targetProfile}.{targetSchema}");
            ddl.AppendLine($"-- Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            ddl.AppendLine();

            if (onlyInSource.Count > 0)
            {
                ddl.AppendLine($"-- ═══════════════════════════════════════════════════════");
                ddl.AppendLine($"-- TABLES TO CREATE IN TARGET (exist only in source)");
                ddl.AppendLine($"-- ═══════════════════════════════════════════════════════");
                ddl.AppendLine();

                foreach (var table in onlyInSource)
                {
                    ddl.AppendLine($"-- TODO: Generate CREATE TABLE statement for {targetSchema}.{table}");
                    ddl.AppendLine($"-- Copy structure from {sourceSchema}.{table} in source database");
                    ddl.AppendLine();
                }
            }

            if (onlyInTarget.Count > 0)
            {
                ddl.AppendLine($"-- ═══════════════════════════════════════════════════════");
                ddl.AppendLine($"-- TABLES TO DROP FROM TARGET (don't exist in source)");
                ddl.AppendLine($"-- ═══════════════════════════════════════════════════════");
                ddl.AppendLine();

                foreach (var table in onlyInTarget)
                {
                    ddl.AppendLine($"-- WARNING: Dropping table will lose all data!");
                    ddl.AppendLine($"-- DROP TABLE {targetSchema}.{table};");
                    ddl.AppendLine();
                }
            }

            MigrationDdlTextBox.Text = ddl.ToString();

            ResultsTabControl.Visibility = Visibility.Visible;

            Logger.Info("Comparison completed: {OnlySource} only in source, {OnlyTarget} only in target, {Both} in both",
                onlyInSource.Count, onlyInTarget.Count, inBoth.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error during cross-database comparison");
            MessageBox.Show($"Error during comparison: {ex.Message}", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            CompareButton.IsEnabled = true;
            CompareButton.Content = "🔍 Compare Schemas";
            ProgressBar.Visibility = Visibility.Collapsed;
        }
    }

    private void CopyDdl_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(MigrationDdlTextBox.Text))
        {
            Clipboard.SetText(MigrationDdlTextBox.Text);
            MessageBox.Show("DDL copied to clipboard!", "Copied", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private void SaveDdl_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(MigrationDdlTextBox.Text))
        {
            MessageBox.Show("No DDL to save.", "Save DDL", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var saveDialog = new SaveFileDialog
        {
            Filter = "SQL Files (*.sql)|*.sql|All Files (*.*)|*.*",
            DefaultExt = ".sql",
            FileName = $"migration_{DateTime.Now:yyyyMMdd_HHmmss}.sql"
        };

        if (saveDialog.ShowDialog() == true)
        {
            try
            {
                File.WriteAllText(saveDialog.FileName, MigrationDdlTextBox.Text);
                MessageBox.Show($"DDL saved to:\n{saveDialog.FileName}", "Saved", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error saving DDL file");
                MessageBox.Show($"Error saving file: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        
        // Dispose connections
        _sourceConnection?.Dispose();
        _targetConnection?.Dispose();
    }
}
