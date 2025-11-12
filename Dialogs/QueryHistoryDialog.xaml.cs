using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NLog;
using WindowsDb2Editor.Models;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.Dialogs;

/// <summary>
/// Dialog for viewing query execution history
/// </summary>
public partial class QueryHistoryDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly QueryHistoryService _historyService;
    private List<QueryHistoryItemViewModel> _allItems = new();
    private QueryHistoryItemViewModel? _selectedItem;

    public string? SelectedQuery { get; private set; }
    public bool ShouldRerunQuery { get; private set; }

    public QueryHistoryDialog()
    {
        InitializeComponent();
        Logger.Debug("QueryHistoryDialog initialized");

        _historyService = new QueryHistoryService();
        LoadHistory();
    }

    private void LoadHistory()
    {
        Logger.Debug("Loading query history");

        try
        {
            var history = _historyService.GetHistory();
            _allItems = history
                .OrderByDescending(h => h.ExecutedAt)
                .Select(h => new QueryHistoryItemViewModel(h))
                .ToList();

            HistoryDataGrid.ItemsSource = _allItems;
            Logger.Info($"Loaded {_allItems.Count} history items");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load query history");
            MessageBox.Show($"Failed to load query history:\n\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = SearchTextBox.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(searchText))
        {
            HistoryDataGrid.ItemsSource = _allItems;
            Logger.Debug("Cleared search filter");
        }
        else
        {
            Logger.Debug($"Searching history for: {searchText}");
            var filtered = _allItems
                .Where(item => item.Sql.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                              item.Database.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            HistoryDataGrid.ItemsSource = filtered;
            Logger.Debug($"Found {filtered.Count} matching items");
        }
    }

    private void Refresh_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Refreshing query history");
        LoadHistory();
        SearchTextBox.Clear();
    }

    private void ClearAll_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Clear all history requested");

        var result = MessageBox.Show(
            "Are you sure you want to clear all query history?\n\nThis action cannot be undone.",
            "Clear History",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            try
            {
                _historyService.ClearHistory();
                LoadHistory();
                QueryDetailsTextBox.Clear();
                Logger.Info("Query history cleared successfully");

                MessageBox.Show("Query history cleared successfully.", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to clear history");
                MessageBox.Show($"Failed to clear history:\n\n{ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void HistoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (HistoryDataGrid.SelectedItem is QueryHistoryItemViewModel item)
        {
            _selectedItem = item;
            QueryDetailsTextBox.Text = item.Sql;
            Logger.Debug($"Selected query from {item.ExecutedAt}");
        }
    }

    private void CopyQuery_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedItem == null)
        {
            MessageBox.Show("Please select a query first.", "Copy Query",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        try
        {
            Clipboard.SetText(_selectedItem.Sql);
            Logger.Info("Query copied to clipboard");

            MessageBox.Show("Query copied to clipboard.", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to copy query");
            MessageBox.Show($"Failed to copy query:\n\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void RerunQuery_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedItem == null)
        {
            MessageBox.Show("Please select a query first.", "Rerun Query",
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        Logger.Info("Rerun query requested");
        SelectedQuery = _selectedItem.Sql;
        ShouldRerunQuery = true;
        DialogResult = true;
        Close();
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Logger.Debug("QueryHistoryDialog closed");
        DialogResult = false;
        Close();
    }
}

/// <summary>
/// View model for displaying query history items
/// </summary>
public class QueryHistoryItemViewModel
{
    public DateTime ExecutedAt { get; set; }
    public string Database { get; set; } = string.Empty;
    public string Sql { get; set; } = string.Empty;
    public int? RowCount { get; set; }
    public double ExecutionTimeMs { get; set; }
    public string Status { get; set; } = string.Empty;
    public Brush StatusColor { get; set; } = Brushes.Green;

    public QueryHistoryItemViewModel(QueryHistoryItem item)
    {
        ExecutedAt = item.ExecutedAt;
        Database = item.Database;
        Sql = TruncateQuery(item.Sql);
        RowCount = item.RowCount;
        ExecutionTimeMs = item.ExecutionTimeMs;
        Status = item.Success ? "✓ OK" : "✗ Error";
        StatusColor = item.Success ? Brushes.Green : Brushes.Red;
    }

    private string TruncateQuery(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return string.Empty;
        }

        // Replace newlines with spaces
        var singleLine = query.Replace("\r", " ").Replace("\n", " ").Trim();

        // Remove extra spaces
        singleLine = System.Text.RegularExpressions.Regex.Replace(singleLine, @"\s+", " ");

        // Truncate if too long
        if (singleLine.Length > 100)
        {
            return singleLine.Substring(0, 97) + "...";
        }

        return singleLine;
    }
}

