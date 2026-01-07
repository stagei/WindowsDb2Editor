using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.Controls;

public partial class CommentManagerPanel : UserControl
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly CommentService _commentService;
    private readonly MetadataLoaderService _metadataService;
    private DB2ConnectionManager? _connectionManager;
    
    public CommentManagerPanel()
    {
        InitializeComponent();
        _commentService = new CommentService();
        _metadataService = new MetadataLoaderService();
        ApplyGridPreferences();
    }
    
    private void ApplyGridPreferences()
    {
        if (App.PreferencesService != null)
        {
            GridStyleHelper.ApplyGridStyle(CommentsDataGrid, App.PreferencesService.Preferences);
        }
    }
    
    public async Task InitializeAsync(DB2ConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
        try
        {
            var schemas = await _metadataService.GetAllSchemasAsync(_connectionManager);
            SchemaComboBox.ItemsSource = schemas;
            if (schemas.Count > 0) SchemaComboBox.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to initialize");
            MessageBox.Show($"Error:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private async void SchemaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await RefreshDataAsync();
    }
    
    private async Task RefreshDataAsync()
    {
        if (_connectionManager == null || SchemaComboBox.SelectedItem == null) return;
        
        try
        {
            StatusText.Text = "Loading...";
            var schema = SchemaComboBox.SelectedItem.ToString();
            var comments = await _commentService.GetTableCommentsAsync(_connectionManager, schema);
            CommentsDataGrid.ItemsSource = comments;
            StatusText.Text = $"{comments.Count} comments - {DateTime.Now:HH:mm:ss}";
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to refresh");
            MessageBox.Show($"Error:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private async void RefreshButton_Click(object sender, RoutedEventArgs e) => await RefreshDataAsync();
    
    private void GenerateScript_Click(object sender, RoutedEventArgs e)
    {
        var comments = CommentsDataGrid.ItemsSource as List<ObjectComment>;
        if (comments == null) return;
        
        // Only export changed comments, not all comments
        var script = _commentService.GenerateCommentScript(comments, changedOnly: true);
        var modifiedCount = comments.Count(c => c.IsModified);
        var tabTitle = modifiedCount > 0 
            ? $"COMMENT Script ({modifiedCount} modified)" 
            : "COMMENT Script";
        
        // Open the generated script in a new editor tab for the current connection
        if (Window.GetWindow(this) is MainWindow mainWindow)
        {
            mainWindow.CreateNewTabWithSql(script, tabTitle);
            Logger.Info("COMMENT script opened in new tab with {Count} modified comments", modifiedCount);
        }
        else
        {
            // Fallback to dialog if MainWindow not available
            var window = Services.ThemedWindowHelper.CreateScriptWindow(tabTitle, script, 700, 500, Window.GetWindow(this));
            window.ShowDialog();
        }
    }
    
    private async void ExportButton_Click(object sender, RoutedEventArgs e)
    {
        var comments = CommentsDataGrid.ItemsSource as List<ObjectComment>;
        if (comments == null || comments.Count == 0) return;
        
        var saveDialog = new SaveFileDialog { Filter = "CSV Files (*.csv)|*.csv", FileName = $"comments_{DateTime.Now:yyyyMMdd_HHmmss}.csv" };
        if (saveDialog.ShowDialog() == true)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Type,Schema,Table,Column,Comment");
            foreach (var c in comments)
                csv.AppendLine($"{c.ObjectType},{c.Schema},{c.TableName},{c.ColumnName},{c.Comment}");
            await File.WriteAllTextAsync(saveDialog.FileName, csv.ToString());
            MessageBox.Show("Exported", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

