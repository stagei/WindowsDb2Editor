using NLog;
using System.Windows;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.Dialogs;

public partial class DeepAnalysisDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly DB2ConnectionManager _connectionManager;
    private readonly List<string> _targetObjects;

    public DeepAnalysisDialog(DB2ConnectionManager connectionManager, List<string> targetObjects)
    {
        InitializeComponent();
        _connectionManager = connectionManager;
        _targetObjects = targetObjects;

        TargetInfoText.Text = $"Analyzing: {string.Join(", ", targetObjects)}";

        Loaded += async (s, e) => 
        {
            // Apply grid preferences to all grids in this dialog
            if (App.PreferencesService != null)
            {
                GridStyleHelper.ApplyGridStylesToWindow(this, App.PreferencesService.Preferences);
            }
            await LoadAnalysisAsync();
        };
    }

    private async Task LoadAnalysisAsync()
    {
        try
        {
            Logger.Info("Loading deep analysis for objects: {Objects}", string.Join(", ", _targetObjects));

            // Placeholder implementation - would use DeepAnalysisService
            CommentsTextBox.Text = "Comments would be extracted from SYSCAT.REMARKS here.";
            
            // Sample profiling data (placeholder)
            var sampleProfiling = new List<dynamic>
            {
                new { Column = "ID", DistinctCount = 0, NullCount = 0, DataType = "INTEGER" },
                new { Column = "NAME", DistinctCount = 0, NullCount = 0, DataType = "VARCHAR" }
            };
            ProfilingGrid.ItemsSource = sampleProfiling;

            RelationshipsListBox.Items.Add("Relationships would be extracted from SYSCAT.REFERENCES here");

            Logger.Debug("Deep analysis loaded (placeholder implementation)");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error loading deep analysis");
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ExportMarkdown_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Export to Markdown requires AiExportService configuration", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void OpenInCursor_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Open in Cursor requires ExternalEditorService configuration", "Open in Editor", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}

