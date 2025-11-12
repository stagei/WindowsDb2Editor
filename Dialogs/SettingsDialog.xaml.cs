using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using NLog;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.Dialogs;

/// <summary>
/// Settings dialog for configuring application preferences
/// </summary>
public partial class SettingsDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly ConfigurationService _configService;

    public SettingsDialog()
    {
        InitializeComponent();
        Logger.Debug("SettingsDialog initialized");

        _configService = new ConfigurationService();
        LoadCurrentSettings();
    }

    private void LoadCurrentSettings()
    {
        Logger.Debug("Loading current settings");

        try
        {
            // Editor Settings
            var themePref = _configService.GetSetting("Editor:DefaultTheme", "Dark");
            SelectComboBoxItem(ThemeComboBox, themePref);

            var fontFamily = _configService.GetSetting("Editor:FontFamily", "Consolas");
            SelectComboBoxItem(FontFamilyComboBox, fontFamily);

            FontSizeSlider.Value = _configService.GetSetting("Editor:FontSize", 14);
            TabSizeSlider.Value = _configService.GetSetting("Editor:TabSize", 4);
            WordWrapCheckBox.IsChecked = _configService.GetSetting("Editor:WordWrap", false);

            // Database Settings
            CommandTimeoutSlider.Value = _configService.GetSetting("Database:DefaultCommandTimeout", 30);
            PoolSizeSlider.Value = _configService.GetSetting("Database:PoolSize", 20);
            AutoCommitCheckBox.IsChecked = _configService.GetSetting("Database:AutoCommit", true);

            // Logging Settings
            var logLevel = _configService.GetSetting("Logging:MinLevel", "Info");
            SelectComboBoxItem(LogLevelComboBox, logLevel);

            var logPath = _configService.GetSetting("Logging:LogDirectory", 
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs"));
            LogPathTextBox.Text = logPath;

            LogRetentionSlider.Value = _configService.GetSetting("Logging:RetentionDays", 30);

            Logger.Info("Current settings loaded successfully");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load current settings");
            MessageBox.Show($"Failed to load settings:\n\n{ex.Message}", "Settings Error",
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void SelectComboBoxItem(ComboBox comboBox, string value)
    {
        foreach (ComboBoxItem item in comboBox.Items)
        {
            if (item.Content.ToString()?.Equals(value, StringComparison.OrdinalIgnoreCase) == true ||
                item.Tag?.ToString()?.Equals(value, StringComparison.OrdinalIgnoreCase) == true)
            {
                comboBox.SelectedItem = item;
                return;
            }
        }
        // Default to first item if not found
        if (comboBox.Items.Count > 0)
        {
            comboBox.SelectedIndex = 0;
        }
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Saving settings");

        try
        {
            // Editor Settings
            var selectedTheme = (ThemeComboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "Dark";
            _configService.SetSetting("Editor:DefaultTheme", selectedTheme);

            var selectedFont = (FontFamilyComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Consolas";
            _configService.SetSetting("Editor:FontFamily", selectedFont);

            _configService.SetSetting("Editor:FontSize", (int)FontSizeSlider.Value);
            _configService.SetSetting("Editor:TabSize", (int)TabSizeSlider.Value);
            _configService.SetSetting("Editor:WordWrap", WordWrapCheckBox.IsChecked ?? false);

            // Database Settings
            _configService.SetSetting("Database:DefaultCommandTimeout", (int)CommandTimeoutSlider.Value);
            _configService.SetSetting("Database:PoolSize", (int)PoolSizeSlider.Value);
            _configService.SetSetting("Database:AutoCommit", AutoCommitCheckBox.IsChecked ?? true);

            // Logging Settings
            var selectedLogLevel = (LogLevelComboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "Info";
            _configService.SetSetting("Logging:MinLevel", selectedLogLevel);
            _configService.SetSetting("Logging:LogDirectory", LogPathTextBox.Text);
            _configService.SetSetting("Logging:RetentionDays", (int)LogRetentionSlider.Value);

            DialogResult = true;

            Logger.Info("Settings saved successfully");
            MessageBox.Show(
                "Settings saved successfully.\n\nSome changes may require restarting the application to take effect.",
                "Settings Saved",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            Close();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to save settings");
            MessageBox.Show($"Failed to save settings:\n\n{ex.Message}", "Save Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Logger.Debug("Settings dialog cancelled");
        DialogResult = false;
        Close();
    }

    private void ResetDefaults_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Resetting to default settings");

        var result = MessageBox.Show(
            "Reset all settings to default values?\n\nThis will discard your current settings.",
            "Reset to Defaults",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            try
            {
                // Reset to defaults
                SelectComboBoxItem(ThemeComboBox, "Dark");
                SelectComboBoxItem(FontFamilyComboBox, "Consolas");
                FontSizeSlider.Value = 14;
                TabSizeSlider.Value = 4;
                WordWrapCheckBox.IsChecked = false;

                CommandTimeoutSlider.Value = 30;
                PoolSizeSlider.Value = 20;
                AutoCommitCheckBox.IsChecked = true;

                SelectComboBoxItem(LogLevelComboBox, "Info");
                LogPathTextBox.Text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                LogRetentionSlider.Value = 30;

                Logger.Info("Settings reset to defaults");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to reset settings");
                MessageBox.Show($"Failed to reset settings:\n\n{ex.Message}", "Reset Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void BrowseLogPath_Click(object sender, RoutedEventArgs e)
    {
        Logger.Debug("Browse log path requested");

        var dialog = new System.Windows.Forms.FolderBrowserDialog
        {
            Description = "Select log directory",
            SelectedPath = LogPathTextBox.Text,
            ShowNewFolderButton = true
        };

        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            LogPathTextBox.Text = dialog.SelectedPath;
            Logger.Debug($"Log path selected: {dialog.SelectedPath}");
        }
    }

    private void OpenLogDirectory_Click(object sender, RoutedEventArgs e)
    {
        Logger.Debug("Open log directory requested");

        try
        {
            var logPath = LogPathTextBox.Text;
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
                Logger.Info($"Created log directory: {logPath}");
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = logPath,
                UseShellExecute = true,
                Verb = "open"
            });

            Logger.Info($"Opened log directory: {logPath}");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to open log directory");
            MessageBox.Show($"Failed to open log directory:\n\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

