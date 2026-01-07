using NLog;
using System.Windows;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.Dialogs;

public partial class FunctionDetailsDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly DB2ConnectionManager _connectionManager;
    private readonly string _schema;
    private readonly string _functionName;

    public FunctionDetailsDialog(DB2ConnectionManager connectionManager, string schema, string functionName)
    {
        InitializeComponent();
        _connectionManager = connectionManager;
        _schema = schema;
        _functionName = functionName;

        FunctionNameText.Text = functionName;
        FunctionInfoText.Text = $"{schema}.{functionName}";

        Loaded += async (s, e) => 
        {
            // Apply grid preferences to all grids in this dialog
            if (App.PreferencesService != null)
            {
                GridStyleHelper.ApplyGridStylesToWindow(this, App.PreferencesService.Preferences);
            }
            await LoadFunctionDetailsAsync();
        };
    }

    private async Task LoadFunctionDetailsAsync()
    {
        try
        {
            // Load source code
            var sourceSql = $"SELECT TEXT FROM SYSCAT.FUNCTIONS WHERE FUNCSCHEMA = '{_schema}' AND FUNCNAME = '{_functionName}'";
            var sourceResult = await _connectionManager.ExecuteQueryAsync(sourceSql);
            if (sourceResult.Rows.Count > 0)
            {
                SourceCodeTextBox.Text = sourceResult.Rows[0]["TEXT"]?.ToString() ?? "Source code not available";
            }

            // Load parameters
            var paramSql = $"SELECT PARMNAME, TYPENAME, PARM_MODE, ORDINAL FROM SYSCAT.FUNCPARMS WHERE FUNCSCHEMA = '{_schema}' AND FUNCNAME = '{_functionName}' ORDER BY ORDINAL";
            var paramResult = await _connectionManager.ExecuteQueryAsync(paramSql);
            ParametersGrid.ItemsSource = paramResult.DefaultView;

            Logger.Info("Function details loaded: {Schema}.{Function}", _schema, _functionName);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error loading function details");
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ExportContext_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("AI Export requires configuration", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void AnalyzeCode_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("AI Code Analysis requires provider configuration", "Analyze", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
    
    private void DockAsTab_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Docking FunctionDetailsDialog as tab: {Schema}.{Function}", _schema, _functionName);
        
        try
        {
            if (System.Windows.Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.CreateTabWithRoutineDetails(_connectionManager, _schema, _functionName, "F");
                Close();
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to dock as tab");
            MessageBox.Show($"Failed to dock as tab: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

