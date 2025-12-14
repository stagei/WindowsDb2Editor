using NLog;
using System.Windows;
using WindowsDb2Editor.Data;

namespace WindowsDb2Editor.Dialogs;

public partial class DatabaseComparisonDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly DB2ConnectionManager _connectionManager;

    public DatabaseComparisonDialog(DB2ConnectionManager connectionManager)
    {
        InitializeComponent();
        _connectionManager = connectionManager;

        Loaded += async (s, e) => await LoadSchemasAsync();
    }

    private async Task LoadSchemasAsync()
    {
        try
        {
            var sql = "SELECT SCHEMANAME FROM SYSCAT.SCHEMATA WHERE SCHEMANAME NOT LIKE 'SYS%' ORDER BY SCHEMANAME";
            var result = await _connectionManager.ExecuteQueryAsync(sql);

            var schemas = new List<string>();
            foreach (System.Data.DataRow row in result.Rows)
            {
                schemas.Add(row["SCHEMANAME"]?.ToString() ?? "");
            }

            SourceComboBox.ItemsSource = schemas;
            TargetComboBox.ItemsSource = schemas;

            Logger.Info("Loaded {Count} schemas for comparison", schemas.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error loading schemas");
            MessageBox.Show($"Error loading schemas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void Compare_Click(object sender, RoutedEventArgs e)
    {
        if (SourceComboBox.SelectedItem == null || TargetComboBox.SelectedItem == null)
        {
            MessageBox.Show("Please select both source and target schemas", "Select Schemas", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            var source = SourceComboBox.SelectedItem.ToString();
            var target = TargetComboBox.SelectedItem.ToString();

            Logger.Info("Comparing schemas: {Source} vs {Target}", source, target);

            // TODO: Use DatabaseComparisonService here
            SummaryText.Text = $"Comparison between {source} and {target}\n\n[DatabaseComparisonService integration pending]";
            OnlyInSourceList.Items.Add("(Comparison results will appear here)");
            MigrationDdlTextBox.Text = "-- Migration DDL statements will be generated here by DatabaseComparisonService";

            ResultsTabControl.Visibility = Visibility.Visible;

            Logger.Info("Comparison placeholder completed");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error during comparison");
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}

