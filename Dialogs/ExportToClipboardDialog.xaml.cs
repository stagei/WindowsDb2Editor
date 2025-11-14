using System.Data;
using System.Windows;
using NLog;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.Dialogs;

public partial class ExportToClipboardDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly DataTable _dataTable;
    private readonly ExportService _exportService;

    public ExportToClipboardDialog(DataTable dataTable)
    {
        InitializeComponent();
        Logger.Debug("ExportToClipboardDialog initialized with {RowCount} rows", dataTable.Rows.Count);

        _dataTable = dataTable;
        _exportService = new ExportService();

        RowCountInfo.Text = $"Ready to export {dataTable.Rows.Count:N0} rows to clipboard";
    }

    private async void Copy_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Starting clipboard export");

        try
        {
            // Disable buttons during export
            CopyButton.IsEnabled = false;
            CancelButton.IsEnabled = false;
            ProgressPanel.Visibility = Visibility.Visible;

            // Determine selected format
            string format;
            if (CsvRadio.IsChecked == true)
            {
                format = "CSV";
                Logger.Debug("Export format: CSV");
            }
            else if (TsvRadio.IsChecked == true)
            {
                format = "TSV";
                Logger.Debug("Export format: TSV");
            }
            else if (JsonRadio.IsChecked == true)
            {
                format = "JSON";
                Logger.Debug("Export format: JSON");
            }
            else if (XmlRadio.IsChecked == true)
            {
                format = "XML";
                Logger.Debug("Export format: XML");
            }
            else
            {
                format = "CSV"; // Default
                Logger.Debug("Export format: CSV (default)");
            }

            // Create progress reporter
            var progress = new Progress<int>(percent =>
            {
                ProgressBar.Value = percent;
                ProgressPercentage.Text = $"{percent}%";
                ProgressText.Text = $"Processing {percent}%...";
            });

            // Export to clipboard
            string result;
            switch (format)
            {
                case "CSV":
                    result = await _exportService.ExportToCsvStringAsync(_dataTable, progress);
                    break;
                case "TSV":
                    result = await _exportService.ExportToTsvStringAsync(_dataTable, progress);
                    break;
                case "JSON":
                    result = await _exportService.ExportToJsonStringAsync(_dataTable, progress);
                    break;
                case "XML":
                    result = await _exportService.ExportToXmlStringAsync(_dataTable, progress);
                    break;
                default:
                    result = await _exportService.ExportToCsvStringAsync(_dataTable, progress);
                    break;
            }

            // Copy to clipboard
            Logger.Debug("Copying result to clipboard ({Length} characters)", result.Length);
            Clipboard.SetText(result);

            Logger.Info("Successfully exported {RowCount} rows to clipboard as {Format}", 
                _dataTable.Rows.Count, format);

            MessageBox.Show(
                $"Successfully copied {_dataTable.Rows.Count:N0} rows to clipboard as {format}!\n\n" +
                $"Size: {result.Length:N0} characters",
                "Copy Successful",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            DialogResult = true;
            Close();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to export to clipboard");

            MessageBox.Show(
                $"Failed to copy to clipboard:\n\n{ex.Message}",
                "Copy Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            // Re-enable buttons on error
            CopyButton.IsEnabled = true;
            CancelButton.IsEnabled = true;
            ProgressPanel.Visibility = Visibility.Collapsed;
        }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Logger.Debug("Export to clipboard cancelled by user");
        DialogResult = false;
        Close();
    }
}





