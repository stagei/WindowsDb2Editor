using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NLog;
using WindowsDb2Editor.Controls;
using WindowsDb2Editor.Dialogs;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly ThemeService _themeService;

    public MainWindow()
    {
        InitializeComponent();
        Logger.Info("MainWindow initializing");

        // Initialize services
        var configService = new ConfigurationService();
        _themeService = new ThemeService(configService);
        _themeService.InitializeTheme();

        RegisterKeyboardShortcuts();
        UpdatePlaceholderVisibility();
        UpdateThemeMenuText();

        Logger.Info("MainWindow initialized successfully");
    }

    private void RegisterKeyboardShortcuts()
    {
        Logger.Debug("Registering keyboard shortcuts");

        // Ctrl+N - New Connection
        var newConnectionCommand = new RoutedCommand();
        newConnectionCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
        CommandBindings.Add(new CommandBinding(newConnectionCommand, (s, e) => NewConnection_Click(s, null!)));

        // Ctrl+W - Close Tab
        var closeTabCommand = new RoutedCommand();
        closeTabCommand.InputGestures.Add(new KeyGesture(Key.W, ModifierKeys.Control));
        CommandBindings.Add(new CommandBinding(closeTabCommand, (s, e) => CloseTab_Click(s, null!)));

        // Ctrl+D - Toggle Dark Mode
        var toggleDarkModeCommand = new RoutedCommand();
        toggleDarkModeCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
        CommandBindings.Add(new CommandBinding(toggleDarkModeCommand, (s, e) => ToggleDarkMode_Click(s, null!)));

        // Ctrl+Tab - Switch to next tab
        var nextTabCommand = new RoutedCommand();
        nextTabCommand.InputGestures.Add(new KeyGesture(Key.Tab, ModifierKeys.Control));
        CommandBindings.Add(new CommandBinding(nextTabCommand, (s, e) => SwitchToNextTab()));

        // Ctrl+Shift+Tab - Switch to previous tab
        var prevTabCommand = new RoutedCommand();
        prevTabCommand.InputGestures.Add(new KeyGesture(Key.Tab, ModifierKeys.Control | ModifierKeys.Shift));
        CommandBindings.Add(new CommandBinding(prevTabCommand, (s, e) => SwitchToPreviousTab()));

        // Ctrl+H - Query History
        var historyCommand = new RoutedCommand();
        historyCommand.InputGestures.Add(new KeyGesture(Key.H, ModifierKeys.Control));
        CommandBindings.Add(new CommandBinding(historyCommand, (s, e) => QueryHistory_Click(s, null!)));

        Logger.Debug("Keyboard shortcuts registered");
    }

    private void NewConnection_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("New Connection requested");

        var dialog = new ConnectionDialog
        {
            Owner = this
        };

        if (dialog.ShowDialog() == true && dialog.Connection != null)
        {
            Logger.Info($"Creating new connection tab for: {dialog.Connection.GetDisplayName()}");
            AddConnectionTab(dialog.Connection);
        }
        else
        {
            Logger.Debug("Connection dialog cancelled");
        }
    }

    private void AddConnectionTab(Models.DB2Connection connection)
    {
        Logger.Debug($"Adding connection tab: {connection.GetDisplayName()}");

        try
        {
            var tabControl = new ConnectionTabControl(connection);
            
            var tabItem = new TabItem
            {
                Header = CreateTabHeader(connection.GetDisplayName()),
                Content = tabControl
            };

            ConnectionTabs.Items.Add(tabItem);
            ConnectionTabs.SelectedItem = tabItem;

            UpdatePlaceholderVisibility();
            Logger.Info($"Connection tab added successfully: {connection.GetDisplayName()}");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to add connection tab");
            MessageBox.Show($"Failed to create connection tab: {ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private StackPanel CreateTabHeader(string title)
    {
        var stackPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal
        };

        var textBlock = new TextBlock
        {
            Text = title,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0, 0, 10, 0)
        };

        var closeButton = new Button
        {
            Content = "✕",
            Width = 20,
            Height = 20,
            Padding = new Thickness(0),
            VerticalAlignment = VerticalAlignment.Center
        };

        closeButton.Click += (s, e) =>
        {
            var button = s as Button;
            var panel = button?.Parent as StackPanel;
            var tabItem = panel?.Parent as TabItem;

            if (tabItem != null)
            {
                CloseTab(tabItem);
            }

            e.Handled = true;
        };

        stackPanel.Children.Add(textBlock);
        stackPanel.Children.Add(closeButton);

        return stackPanel;
    }

    private void CloseTab_Click(object sender, RoutedEventArgs e)
    {
        if (ConnectionTabs.SelectedItem is TabItem selectedTab)
        {
            Logger.Info("Closing current tab");
            CloseTab(selectedTab);
        }
        else
        {
            Logger.Debug("No tab selected to close");
        }
    }

    private void CloseTab(TabItem tabItem)
    {
        Logger.Debug("Closing tab");

        try
        {
            // Cleanup the connection
            if (tabItem.Content is ConnectionTabControl tabControl)
            {
                tabControl.Cleanup();
            }

            ConnectionTabs.Items.Remove(tabItem);
            UpdatePlaceholderVisibility();

            Logger.Info("Tab closed successfully");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error closing tab");
        }
    }

    private void UpdatePlaceholderVisibility()
    {
        var hasNoTabs = ConnectionTabs.Items.Count == 0;
        PlaceholderGrid.Visibility = hasNoTabs ? Visibility.Visible : Visibility.Collapsed;
        ConnectionTabs.Visibility = hasNoTabs ? Visibility.Collapsed : Visibility.Visible;
        MainStatusBar.Visibility = hasNoTabs ? Visibility.Collapsed : Visibility.Visible;

        UpdateConnectionCount();
        Logger.Debug($"Placeholder visibility: {PlaceholderGrid.Visibility}, Tab count: {ConnectionTabs.Items.Count}");
    }

    private void UpdateConnectionCount()
    {
        var count = ConnectionTabs.Items.Count;
        ConnectionCountText.Text = $"{count} connection{(count != 1 ? "s" : "")}";
        Logger.Debug($"Connection count updated: {count}");
    }

    private void ConnectionTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ConnectionTabs.SelectedItem is TabItem selectedTab)
        {
            var header = GetTabHeaderText(selectedTab);
            StatusBarText.Text = $"Active: {header}";
            Logger.Debug($"Tab selection changed to: {header}");
        }
    }

    private string GetTabHeaderText(TabItem tabItem)
    {
        if (tabItem.Header is StackPanel panel && panel.Children.Count > 0)
        {
            if (panel.Children[0] is TextBlock textBlock)
            {
                return textBlock.Text;
            }
        }
        return "Unknown";
    }

    private void SwitchToNextTab()
    {
        if (ConnectionTabs.Items.Count <= 1) return;

        var currentIndex = ConnectionTabs.SelectedIndex;
        var nextIndex = (currentIndex + 1) % ConnectionTabs.Items.Count;
        ConnectionTabs.SelectedIndex = nextIndex;
        Logger.Debug($"Switched to next tab: {nextIndex}");
    }

    private void SwitchToPreviousTab()
    {
        if (ConnectionTabs.Items.Count <= 1) return;

        var currentIndex = ConnectionTabs.SelectedIndex;
        var prevIndex = currentIndex - 1;
        if (prevIndex < 0)
        {
            prevIndex = ConnectionTabs.Items.Count - 1;
        }
        ConnectionTabs.SelectedIndex = prevIndex;
        Logger.Debug($"Switched to previous tab: {prevIndex}");
    }

    private void ToggleDarkMode_Click(object sender, RoutedEventArgs e)
    {
        _themeService.ToggleTheme();
        _themeService.SaveThemePreference();
        UpdateThemeMenuText();

        Logger.Info($"Theme switched to: {_themeService.GetThemeName()}");
        MessageBox.Show($"Theme: {_themeService.GetThemeName()}", "Theme Changed",
            MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void UpdateThemeMenuText()
    {
        // Update menu item text to show current theme
        var themeName = _themeService.GetThemeName();
        Logger.Debug($"Updating theme menu text: {themeName}");
    }

    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Application exit requested");
        Close();
    }

    private void QueryHistory_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Query history dialog requested");

        var historyDialog = new Dialogs.QueryHistoryDialog
        {
            Owner = this
        };

        if (historyDialog.ShowDialog() == true && historyDialog.ShouldRerunQuery)
        {
            // Get the active tab
            if (ConnectionTabs.SelectedItem is TabItem selectedTab &&
                selectedTab.Content is ConnectionTabControl tabControl &&
                !string.IsNullOrEmpty(historyDialog.SelectedQuery))
            {
                Logger.Info("Rerunning query from history");
                tabControl.SetQueryText(historyDialog.SelectedQuery);
            }
            else
            {
                Logger.Warn("No active connection tab to rerun query");
                MessageBox.Show("Please open a connection first to rerun the query.", "No Connection",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

    private void Settings_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Settings dialog requested");

        var settingsDialog = new Dialogs.SettingsDialog
        {
            Owner = this
        };

        if (settingsDialog.ShowDialog() == true)
        {
            Logger.Info("Settings changed, may require application restart");
        }
    }

    private void About_Click(object sender, RoutedEventArgs e)
    {
        Logger.Debug("About dialog requested");
        MessageBox.Show(
            "WindowsDb2Editor v1.0.0\n\n" +
            "A modern DB2 database editor for Windows 11\n" +
            "Built with .NET 10 and WPF\n\n" +
            "Features:\n" +
            "- Multiple connection tabs\n" +
            "- SQL syntax highlighting\n" +
            "- SQL auto-formatting\n" +
            "- Dark mode support\n" +
            "- Offline deployment ready",
            "About WindowsDb2Editor",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }

    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
        Logger.Info("MainWindow closing");

        // Cleanup all open tabs
        while (ConnectionTabs.Items.Count > 0)
        {
            var tabItem = ConnectionTabs.Items[0] as TabItem;
            if (tabItem != null)
            {
                CloseTab(tabItem);
            }
        }

        base.OnClosing(e);
    }
}