using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.Dialogs;

public partial class SchemaTableSelectionDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    private readonly DB2ConnectionManager _connectionManager;
    private readonly MetadataHandler _metadataHandler;
    private readonly string _defaultSchema;
    private readonly Dictionary<string, List<string>> _schemaTableDict = new();
    
    public List<string> SelectedTables { get; private set; } = new();
    
    public SchemaTableSelectionDialog(DB2ConnectionManager connectionManager, string defaultSchema)
    {
        InitializeComponent();
        
        _connectionManager = connectionManager;
        _metadataHandler = App.MetadataHandler ?? throw new InvalidOperationException("MetadataHandler not initialized");
        _defaultSchema = defaultSchema;
        
        Loaded += SchemaTableSelectionDialog_Loaded;
        FilterTextBox.TextChanged += FilterTextBox_TextChanged;
    }
    
    private async void SchemaTableSelectionDialog_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            Logger.Info("Loading schemas and tables");
            
            await LoadSchemasAndTablesAsync();
            BuildTreeView();
            
            Logger.Info("Loaded {Count} schemas with tables", _schemaTableDict.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load schemas and tables");
            MessageBox.Show(
                $"Failed to load schemas and tables: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Close();
        }
    }
    
    private async Task LoadSchemasAndTablesAsync()
    {
        var sqlTemplate = _metadataHandler.GetQuery("DB2", "12.1", "GetAllSelectableTables");
        var sql = sqlTemplate.Replace("?", $"'{_defaultSchema}'");
        
        Logger.Debug("Using query: GUI_GetAllSelectableTables for schema: {Schema}", _defaultSchema);
        var result = await _connectionManager.ExecuteQueryAsync(sql);
        
        foreach (DataRow row in result.Rows)
        {
            var schema = row["TABSCHEMA"]?.ToString() ?? string.Empty;
            var table = row["TABNAME"]?.ToString() ?? string.Empty;
            
            if (!_schemaTableDict.ContainsKey(schema))
            {
                _schemaTableDict[schema] = new List<string>();
            }
            
            _schemaTableDict[schema].Add(table);
        }
    }
    
    private void BuildTreeView()
    {
        SchemaTreeView.Items.Clear();
        
        var filter = FilterTextBox.Text.ToLowerInvariant();
        
        foreach (var schema in _schemaTableDict.Keys.OrderBy(s => s))
        {
            var tables = _schemaTableDict[schema];
            
            if (!string.IsNullOrEmpty(filter))
            {
                tables = tables.Where(t => t.ToLowerInvariant().Contains(filter)).ToList();
                if (tables.Count == 0) continue;
            }
            
            var schemaItem = new TreeViewItem
            {
                Header = CreateSchemaHeader(schema, tables.Count),
                Tag = schema
            };
            
            foreach (var table in tables.OrderBy(t => t))
            {
                var tableItem = new TreeViewItem
                {
                    Header = CreateTableHeader(table),
                    Tag = $"{schema}.{table}"
                };
                
                schemaItem.Items.Add(tableItem);
            }
            
            schemaItem.Expanded += SchemaItem_Expanded;
            schemaItem.Collapsed += SchemaItem_Collapsed;
            
            SchemaTreeView.Items.Add(schemaItem);
        }
        
        UpdateSelectionCount();
    }
    
    private StackPanel CreateSchemaHeader(string schema, int tableCount)
    {
        var panel = new StackPanel { Orientation = Orientation.Horizontal };
        
        var checkbox = new CheckBox
        {
            Content = $"📁 {schema} ({tableCount} tables)",
            FontWeight = FontWeights.Bold,
            Margin = new Thickness(0, 0, 10, 0)
        };
        
        checkbox.Checked += (s, e) => SelectSchemaTablesChecked(schema, true);
        checkbox.Unchecked += (s, e) => SelectSchemaTablesChecked(schema, false);
        
        panel.Children.Add(checkbox);
        return panel;
    }
    
    private CheckBox CreateTableHeader(string table)
    {
        return new CheckBox
        {
            Content = $"📄 {table}",
            Margin = new Thickness(20, 2, 0, 2)
        };
    }
    
    private void SelectSchemaTablesChecked(string schema, bool isChecked)
    {
        var schemaItem = SchemaTreeView.Items.Cast<TreeViewItem>()
            .FirstOrDefault(item => item.Tag?.ToString() == schema);
        
        if (schemaItem != null)
        {
            foreach (TreeViewItem tableItem in schemaItem.Items)
            {
                var header = tableItem.Header as CheckBox;
                if (header != null)
                {
                    header.IsChecked = isChecked;
                }
            }
        }
        
        UpdateSelectionCount();
    }
    
    private void SchemaItem_Expanded(object sender, RoutedEventArgs e)
    {
        // Could load additional details here if needed
    }
    
    private void SchemaItem_Collapsed(object sender, RoutedEventArgs e)
    {
        // Could cleanup details here if needed
    }
    
    private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        BuildTreeView();
    }
    
    private void SelectAll_Click(object sender, RoutedEventArgs e)
    {
        foreach (TreeViewItem schemaItem in SchemaTreeView.Items)
        {
            var header = schemaItem.Header as StackPanel;
            var checkbox = header?.Children[0] as CheckBox;
            if (checkbox != null)
            {
                checkbox.IsChecked = true;
            }
        }
        
        UpdateSelectionCount();
    }
    
    private void ClearAll_Click(object sender, RoutedEventArgs e)
    {
        foreach (TreeViewItem schemaItem in SchemaTreeView.Items)
        {
            var header = schemaItem.Header as StackPanel;
            var checkbox = header?.Children[0] as CheckBox;
            if (checkbox != null)
            {
                checkbox.IsChecked = false;
            }
        }
        
        UpdateSelectionCount();
    }
    
    private void UpdateSelectionCount()
    {
        var count = 0;
        
        foreach (TreeViewItem schemaItem in SchemaTreeView.Items)
        {
            foreach (TreeViewItem tableItem in schemaItem.Items)
            {
                var header = tableItem.Header as CheckBox;
                if (header?.IsChecked == true)
                {
                    count++;
                }
            }
        }
        
        SelectionCountText.Text = $"Selected: {count} table(s)";
    }
    
    private void OK_Click(object sender, RoutedEventArgs e)
    {
        SelectedTables.Clear();
        
        foreach (TreeViewItem schemaItem in SchemaTreeView.Items)
        {
            foreach (TreeViewItem tableItem in schemaItem.Items)
            {
                var header = tableItem.Header as CheckBox;
                if (header?.IsChecked == true)
                {
                    var fullTableName = tableItem.Tag?.ToString();
                    if (!string.IsNullOrEmpty(fullTableName))
                    {
                        SelectedTables.Add(fullTableName);
                    }
                }
            }
        }
        
        if (SelectedTables.Count == 0)
        {
            MessageBox.Show(
                "Please select at least one table.",
                "No Tables Selected",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return;
        }
        
        Logger.Info("User selected {Count} tables", SelectedTables.Count);
        DialogResult = true;
        Close();
    }
}

