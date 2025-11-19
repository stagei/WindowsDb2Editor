using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.Dialogs;

public partial class DdlGeneratorDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly DdlGeneratorService _ddlService;
    private readonly MetadataLoaderService _metadataService;
    private readonly DB2ConnectionManager _connectionManager;
    
    public DdlGeneratorDialog(DB2ConnectionManager connectionManager)
    {
        InitializeComponent();
        _connectionManager = connectionManager;
        _ddlService = new DdlGeneratorService();
        _metadataService = new MetadataLoaderService();
        _ = LoadSchemasAsync();
    }
    
    private async Task LoadSchemasAsync()
    {
        try
        {
            var schemas = await _metadataService.GetAllSchemasAsync(_connectionManager);
            SchemaComboBox.ItemsSource = schemas;
            if (schemas.Count > 0) SchemaComboBox.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load schemas");
        }
    }
    
    private async void SchemaComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (SchemaComboBox.SelectedItem == null) return;
        var schema = SchemaComboBox.SelectedItem.ToString() ?? string.Empty;
        
        try
        {
            var tables = await _metadataService.GetTablesAsync(_connectionManager, schema);
            TableComboBox.ItemsSource = tables.Select(t => t.Name).ToList();
            if (tables.Count > 0) TableComboBox.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load tables");
        }
    }
    
    private void EntireSchema_Changed(object sender, RoutedEventArgs e)
    {
        TableComboBox.IsEnabled = EntireSchemaCheckBox.IsChecked != true;
    }
    
    private async void GenerateButton_Click(object sender, RoutedEventArgs e)
    {
        if (SchemaComboBox.SelectedItem == null) return;
        
        var schema = SchemaComboBox.SelectedItem.ToString() ?? string.Empty;
        
        try
        {
            GenerateButton.IsEnabled = false;
            DdlEditor.Text = "-- Generating DDL...";
            
            string ddl;
            if (EntireSchemaCheckBox.IsChecked == true)
            {
                ddl = await _ddlService.GenerateSchemaDdlAsync(_connectionManager, schema);
            }
            else
            {
                var table = TableComboBox.SelectedItem?.ToString() ?? string.Empty;
                ddl = await _ddlService.GenerateTableDdlAsync(_connectionManager, schema, table);
            }
            
            DdlEditor.Text = ddl;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate DDL");
            MessageBox.Show($"Failed to generate DDL:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            GenerateButton.IsEnabled = true;
        }
    }
    
    private void CopyButton_Click(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(DdlEditor.Text);
        MessageBox.Show("DDL copied to clipboard", "Copy", MessageBoxButton.OK, MessageBoxImage.Information);
    }
    
    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        var saveDialog = new SaveFileDialog { Filter = "SQL Files (*.sql)|*.sql", FileName = "ddl.sql" };
        if (saveDialog.ShowDialog() == true)
        {
            await File.WriteAllTextAsync(saveDialog.FileName, DdlEditor.Text);
            MessageBox.Show("DDL saved", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

