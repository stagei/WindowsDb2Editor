using NLog;
using System.Windows;
using WindowsDb2Editor.Data;

namespace WindowsDb2Editor.Dialogs;

public partial class ViewDetailsDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly DB2ConnectionManager _connectionManager;
    private readonly string _schema;
    private readonly string _viewName;

    public ViewDetailsDialog(DB2ConnectionManager connectionManager, string schema, string viewName)
    {
        InitializeComponent();
        _connectionManager = connectionManager;
        _schema = schema;
        _viewName = viewName;

        ViewNameText.Text = viewName;
        ViewInfoText.Text = $"{schema}.{viewName}";

        Loaded += async (s, e) => await LoadViewDetailsAsync();
    }

    private async Task LoadViewDetailsAsync()
    {
        try
        {
            // Load view definition
            var defSql = $"SELECT TEXT FROM SYSCAT.VIEWS WHERE VIEWSCHEMA = '{_schema}' AND VIEWNAME = '{_viewName}'";
            var defResult = await _connectionManager.ExecuteQueryAsync(defSql);
            if (defResult.Rows.Count > 0)
            {
                DefinitionTextBox.Text = defResult.Rows[0]["TEXT"]?.ToString() ?? "No definition found";
            }

            // Load columns
            var colSql = $"SELECT COLNAME, TYPENAME, NULLS FROM SYSCAT.COLUMNS WHERE TABSCHEMA = '{_schema}' AND TABNAME = '{_viewName}' ORDER BY COLNO";
            var colResult = await _connectionManager.ExecuteQueryAsync(colSql);
            ColumnsGrid.ItemsSource = colResult.DefaultView;

            // Load dependencies (simplified)
            var depSql = $"SELECT DISTINCT BSCHEMA || '.' || BNAME as DEPENDENCY FROM SYSCAT.VIEWDEP WHERE VIEWSCHEMA = '{_schema}' AND VIEWNAME = '{_viewName}'";
            var depResult = await _connectionManager.ExecuteQueryAsync(depSql);
            foreach (System.Data.DataRow row in depResult.Rows)
            {
                DependenciesListBox.Items.Add(row["DEPENDENCY"]?.ToString());
            }

            Logger.Info("View details loaded: {Schema}.{View}", _schema, _viewName);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error loading view details");
            MessageBox.Show($"Error loading view details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ExportContext_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("AI Export functionality requires AI provider configuration", "Export Context", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void RefreshAnalysis_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("AI Analysis requires AI provider configuration (Ollama/OpenAI/Claude)", "Refresh Analysis", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}

