using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using NLog;
using WindowsDb2Editor.Services;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Dialogs
{
    public partial class SettingsDialog : Window
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly PreferencesService _preferencesService;

        public SettingsDialog(PreferencesService preferencesService)
        {
            InitializeComponent();
            Logger.Debug("SettingsDialog initialized");

            _preferencesService = preferencesService;

            // Set up event handlers
            FontSizeSlider.ValueChanged += FontSizeSlider_ValueChanged;
            TabSizeSlider.ValueChanged += TabSizeSlider_ValueChanged;

            // Load current preferences
            LoadCurrentSettings();

            // Display data folder path
            DataFolderPathText.Text = $"Location: {AppDataHelper.GetAppDataFolder()}";
        }

        private void LoadCurrentSettings()
        {
            try
            {
                Logger.Debug("Loading current settings into dialog");
                var prefs = _preferencesService.Preferences;

                // Editor settings
                SetComboBoxValue(ThemeComboBox, prefs.DefaultTheme);
                SetComboBoxValue(FontFamilyComboBox, prefs.FontFamily);
                FontSizeSlider.Value = prefs.FontSize;
                FontSizeText.Text = prefs.FontSize.ToString();
                TabSizeSlider.Value = prefs.TabSize;
                TabSizeText.Text = prefs.TabSize.ToString();

                // Database settings
                MaxRowsTextBox.Text = prefs.MaxRowsPerQuery.ToString();
                CommandTimeoutTextBox.Text = prefs.CommandTimeout.ToString();
                HandleDecimalErrorsCheckBox.IsChecked = prefs.HandleDecimalErrorsGracefully;
                AutoRefreshObjectsCheckBox.IsChecked = prefs.AutoRefreshObjectsOnConnect;

                // Logging settings
                SetComboBoxValue(LogLevelComboBox, prefs.LogLevel);

                Logger.Info("Settings loaded successfully");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error loading settings");
                MessageBox.Show($"Error loading settings: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetComboBoxValue(ComboBox comboBox, string value)
        {
            foreach (ComboBoxItem item in comboBox.Items)
            {
                if (item.Tag?.ToString() == value)
                {
                    comboBox.SelectedItem = item;
                    break;
                }
            }
        }

        private string GetComboBoxValue(ComboBox comboBox)
        {
            return (comboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? string.Empty;
        }

        private void FontSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (FontSizeText != null)
            {
                FontSizeText.Text = ((int)e.NewValue).ToString();
            }
        }

        private void TabSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TabSizeText != null)
            {
                TabSizeText.Text = ((int)e.NewValue).ToString();
            }
        }

        private void OpenLogsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Logger.Info("Opening logs folder");
                var logsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                
                if (!Directory.Exists(logsFolder))
                {
                    Directory.CreateDirectory(logsFolder);
                    Logger.Info("Created logs folder: {Folder}", logsFolder);
                }

                Process.Start("explorer.exe", logsFolder);
                Logger.Info("Opened logs folder in Explorer");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error opening logs folder");
                MessageBox.Show($"Error opening logs folder: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenDataFolderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Logger.Info("Opening data folder");
                var dataFolder = AppDataHelper.GetAppDataFolder();
                
                if (!Directory.Exists(dataFolder))
                {
                    Directory.CreateDirectory(dataFolder);
                    Logger.Info("Created data folder: {Folder}", dataFolder);
                }

                Process.Start("explorer.exe", dataFolder);
                Logger.Info("Opened data folder in Explorer");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error opening data folder");
                MessageBox.Show($"Error opening data folder: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetToDefaults_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Logger.Info("Resetting settings to defaults");
                
                var result = MessageBox.Show(
                    "Are you sure you want to reset all settings to defaults?", 
                    "Confirm Reset", 
                    MessageBoxButton.YesNo, 
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    var defaults = new UserPreferences();
                    
                    // Editor settings
                    SetComboBoxValue(ThemeComboBox, defaults.DefaultTheme);
                    SetComboBoxValue(FontFamilyComboBox, defaults.FontFamily);
                    FontSizeSlider.Value = defaults.FontSize;
                    TabSizeSlider.Value = defaults.TabSize;

                    // Database settings
                    MaxRowsTextBox.Text = defaults.MaxRowsPerQuery.ToString();
                    CommandTimeoutTextBox.Text = defaults.CommandTimeout.ToString();
                    HandleDecimalErrorsCheckBox.IsChecked = defaults.HandleDecimalErrorsGracefully;
                    AutoRefreshObjectsCheckBox.IsChecked = defaults.AutoRefreshObjectsOnConnect;

                    // Logging settings
                    SetComboBoxValue(LogLevelComboBox, defaults.LogLevel);

                    Logger.Info("Settings reset to defaults");
                    MessageBox.Show("Settings have been reset to defaults.", "Reset Complete", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error resetting settings");
                MessageBox.Show($"Error resetting settings: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Logger.Info("Saving settings");

                // Validate inputs
                if (!int.TryParse(MaxRowsTextBox.Text, out int maxRows) || maxRows < 1)
                {
                    MessageBox.Show("Max Rows Per Query must be a positive integer.", "Invalid Input", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    MaxRowsTextBox.Focus();
                    return;
                }

                if (!int.TryParse(CommandTimeoutTextBox.Text, out int commandTimeout) || commandTimeout < 0)
                {
                    MessageBox.Show("Command Timeout must be a non-negative integer.", "Invalid Input", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    CommandTimeoutTextBox.Focus();
                    return;
                }

                // Update preferences
                _preferencesService.Preferences.DefaultTheme = GetComboBoxValue(ThemeComboBox);
                _preferencesService.Preferences.FontFamily = GetComboBoxValue(FontFamilyComboBox);
                _preferencesService.Preferences.FontSize = (int)FontSizeSlider.Value;
                _preferencesService.Preferences.TabSize = (int)TabSizeSlider.Value;
                _preferencesService.Preferences.MaxRowsPerQuery = maxRows;
                _preferencesService.Preferences.CommandTimeout = commandTimeout;
                _preferencesService.Preferences.HandleDecimalErrorsGracefully = HandleDecimalErrorsCheckBox.IsChecked ?? true;
                _preferencesService.Preferences.AutoRefreshObjectsOnConnect = AutoRefreshObjectsCheckBox.IsChecked ?? true;
                _preferencesService.Preferences.LogLevel = GetComboBoxValue(LogLevelComboBox);

                // Save to file
                _preferencesService.SavePreferences();

                Logger.Info("Settings saved successfully");
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error saving settings");
                MessageBox.Show($"Error saving settings: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Logger.Debug("Settings dialog cancelled");
            DialogResult = false;
            Close();
        }
    }
}
