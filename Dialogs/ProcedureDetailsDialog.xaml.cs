using NLog;
using System.Windows;
using WindowsDb2Editor.Data;

namespace WindowsDb2Editor.Dialogs;

public partial class ProcedureDetailsDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly DB2ConnectionManager _connectionManager;
    private readonly string _schema;
    private readonly string _procedureName;

    public ProcedureDetailsDialog(DB2ConnectionManager connectionManager, string schema, string procedureName)
    {
        InitializeComponent();
        _connectionManager = connectionManager;
        _schema = schema;
        _procedureName = procedureName;

        ProcedureNameText.Text = procedureName;
        ProcedureInfoText.Text = $"{schema}.{procedureName}";

        Loaded += async (s, e) => await LoadProcedureDetailsAsync();
    }

    private async Task LoadProcedureDetailsAsync()
    {
        try
        {
            // Load source code
            var sourceSql = $"SELECT TEXT FROM SYSCAT.PROCEDURES WHERE PROCSCHEMA = '{_schema}' AND PROCNAME = '{_procedureName}'";
            var sourceResult = await _connectionManager.ExecuteQueryAsync(sourceSql);
            if (sourceResult.Rows.Count > 0)
            {
                SourceCodeTextBox.Text = sourceResult.Rows[0]["TEXT"]?.ToString() ?? "Source code not available";
            }

            // Load parameters
            var paramSql = $"SELECT PARMNAME, TYPENAME, PARM_MODE, ORDINAL FROM SYSCAT.PROCPARMS WHERE PROCSCHEMA = '{_schema}' AND PROCNAME = '{_procedureName}' ORDER BY ORDINAL";
            var paramResult = await _connectionManager.ExecuteQueryAsync(paramSql);
            ParametersGrid.ItemsSource = paramResult.DefaultView;

            Logger.Info("Procedure details loaded: {Schema}.{Procedure}", _schema, _procedureName);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error loading procedure details");
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

