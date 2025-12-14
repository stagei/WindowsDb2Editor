using NLog;
using System.Windows;
using WindowsDb2Editor.Data;

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

        Loaded += async (s, e) => await LoadFunctionDetailsAsync();
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
}

