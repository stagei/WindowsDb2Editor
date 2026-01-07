using System.IO;
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
    private readonly ConnectionStorageService _connectionStorageService;
    private readonly PreferencesService _preferencesService;
    private readonly ConnectionHistoryService _historyService;

    public MainWindow()
    {
        InitializeComponent();
        Logger.Info("MainWindow initializing");

        // Initialize services
        var configService = new ConfigurationService();
        _themeService = new ThemeService(configService);
        _themeService.InitializeTheme();
        _connectionStorageService = new ConnectionStorageService();
        _preferencesService = new PreferencesService();
        _historyService = new ConnectionHistoryService();

        RegisterKeyboardShortcuts();
        UpdateWelcomePanelVisibility();
        UpdateThemeMenuText();
        PopulateRecentConnections();

        Logger.Info("MainWindow initialized successfully");
    }
    
    private void UpdateWelcomePanelVisibility()
    {
        var hasConnections = ConnectionTabs.Items.Count > 0;
        WelcomePanel.Visibility = hasConnections ? Visibility.Collapsed : Visibility.Visible;
        ConnectionTabs.Visibility = hasConnections ? Visibility.Visible : Visibility.Collapsed;
        
        if (!hasConnections)
        {
            WelcomePanel.LoadRecentConnections();
        }
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

        // Ctrl+F4 - Close Current Tab (alternate shortcut)
        var closeTabAltCommand = new RoutedCommand();
        closeTabAltCommand.InputGestures.Add(new KeyGesture(Key.F4, ModifierKeys.Control));
        CommandBindings.Add(new CommandBinding(closeTabAltCommand, (s, e) => CloseTab_Click(s, null!)));

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

        // Ctrl+Plus - Increase font size
        var increaseFontCommand = new RoutedCommand();
        increaseFontCommand.InputGestures.Add(new KeyGesture(Key.OemPlus, ModifierKeys.Control));
        increaseFontCommand.InputGestures.Add(new KeyGesture(Key.Add, ModifierKeys.Control));
        CommandBindings.Add(new CommandBinding(increaseFontCommand, (s, e) => IncreaseFontSize()));

        // Ctrl+Minus - Decrease font size
        var decreaseFontCommand = new RoutedCommand();
        decreaseFontCommand.InputGestures.Add(new KeyGesture(Key.OemMinus, ModifierKeys.Control));
        decreaseFontCommand.InputGestures.Add(new KeyGesture(Key.Subtract, ModifierKeys.Control));
        CommandBindings.Add(new CommandBinding(decreaseFontCommand, (s, e) => DecreaseFontSize()));

        // Ctrl+Shift+Plus - Increase TreeView spacing
        var increaseSpacingCommand = new RoutedCommand();
        increaseSpacingCommand.InputGestures.Add(new KeyGesture(Key.OemPlus, ModifierKeys.Control | ModifierKeys.Shift));
        increaseSpacingCommand.InputGestures.Add(new KeyGesture(Key.Add, ModifierKeys.Control | ModifierKeys.Shift));
        CommandBindings.Add(new CommandBinding(increaseSpacingCommand, (s, e) => IncreaseTreeViewSpacing()));

        // Ctrl+Shift+Minus - Decrease TreeView spacing
        var decreaseSpacingCommand = new RoutedCommand();
        decreaseSpacingCommand.InputGestures.Add(new KeyGesture(Key.OemMinus, ModifierKeys.Control | ModifierKeys.Shift));
        decreaseSpacingCommand.InputGestures.Add(new KeyGesture(Key.Subtract, ModifierKeys.Control | ModifierKeys.Shift));
        CommandBindings.Add(new CommandBinding(decreaseSpacingCommand, (s, e) => DecreaseTreeViewSpacing()));

        // Ctrl+Shift+Q - Show SQL Query History
        var sqlHistoryCommand = new RoutedCommand();
        sqlHistoryCommand.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control | ModifierKeys.Shift));
        CommandBindings.Add(new CommandBinding(sqlHistoryCommand, (s, e) => ShowSqlQueryHistory()));

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
            
            // Refresh recent connections menu
            PopulateRecentConnections();
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

            // Record connection usage in history
            _historyService.RecordConnectionUsed(connection.Name);
            
            UpdateWelcomePanelVisibility();
            
            // RBAC: Update menu visibility based on user's access level (if determined)
            if (connection.IsAccessLevelDetermined)
            {
                UpdateMenuVisibilityForAccessLevel(connection.Permissions);
            }
            
            Logger.Info($"Connection tab added successfully: {connection.GetDisplayName()}");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to add connection tab");
            MessageBox.Show($"Failed to create connection tab: {ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    /// <summary>
    /// Create a new tab with SQL content (used for DDL generation)
    /// </summary>
    public void CreateNewTabWithSql(string sqlContent, string tabName)
    {
        Logger.Info("Creating new tab with SQL content: {TabName}", tabName);
        
        try
        {
            // Get the currently active tab's connection
            if (ConnectionTabs.SelectedItem is TabItem selectedTab && 
                selectedTab.Content is Controls.ConnectionTabControl activeTab)
            {
                var connection = activeTab.Connection;
                
                // Create a new tab with the same connection
                var newTabControl = new Controls.ConnectionTabControl(connection);
                
                var newTabItem = new TabItem
                {
                    Header = CreateTabHeader($"{tabName} - {connection.GetDisplayName()}"),
                    Content = newTabControl
                };

                ConnectionTabs.Items.Add(newTabItem);
                ConnectionTabs.SelectedItem = newTabItem;
                
                // Set the SQL content
                newTabControl.SetSqlEditorText(sqlContent);
                
                Logger.Info("New tab created successfully with SQL content");
            }
            else
            {
                Logger.Warn("No active connection tab found");
                MessageBox.Show("No active connection found.", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to create new tab with SQL");
            MessageBox.Show($"Failed to create new tab: {ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    /// <summary>
    /// Create a new tab with table details content (same content as TableDetailsDialog)
    /// </summary>
    public void CreateTabWithTableDetails(Data.DB2ConnectionManager connectionManager, string fullTableName, string displayName)
    {
        Logger.Info("Creating tab with table details: {Table}", fullTableName);
        
        try
        {
            // Get the currently active tab's connection
            if (ConnectionTabs.SelectedItem is TabItem selectedTab && 
                selectedTab.Content is Controls.ConnectionTabControl activeTab)
            {
                var connection = activeTab.Connection;
                
                // Create a TableDetailsPanel as the tab content
                var detailsPanel = new Controls.TableDetailsPanel(connectionManager, fullTableName);
                
                var newTabItem = new TabItem
                {
                    Header = CreateTabHeader($"📋 {displayName} - {connection.GetDisplayName()}"),
                    Content = detailsPanel,
                    Tag = new { Type = "TableDetails", TableName = fullTableName }
                };

                ConnectionTabs.Items.Add(newTabItem);
                ConnectionTabs.SelectedItem = newTabItem;
                
                Logger.Info("New tab created with table details content");
            }
            else
            {
                Logger.Warn("No active connection tab found");
                MessageBox.Show("No active connection found.", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to create tab with table details");
            MessageBox.Show($"Failed to create tab: {ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    /// <summary>
    /// Create a new tab with view details content
    /// </summary>
    public void CreateTabWithViewDetails(Data.DB2ConnectionManager connectionManager, string schema, string viewName)
    {
        Logger.Info("Creating tab with view details: {Schema}.{View}", schema, viewName);
        
        try
        {
            if (ConnectionTabs.SelectedItem is TabItem selectedTab && 
                selectedTab.Content is Controls.ConnectionTabControl activeTab)
            {
                var connection = activeTab.Connection;
                var detailsPanel = new Controls.ViewDetailsPanel(connectionManager, schema, viewName);
                
                var newTabItem = new TabItem
                {
                    Header = CreateTabHeader($"👁️ {viewName?.Trim()} - {connection.GetDisplayName()}"),
                    Content = detailsPanel,
                    Tag = new { Type = "ViewDetails", Schema = schema, ViewName = viewName }
                };

                ConnectionTabs.Items.Add(newTabItem);
                ConnectionTabs.SelectedItem = newTabItem;
                Logger.Info("New tab created with view details content");
            }
            else
            {
                MessageBox.Show("No active connection found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to create tab with view details");
            MessageBox.Show($"Failed to create tab: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    /// <summary>
    /// Create a new tab with routine (procedure/function) details content
    /// </summary>
    public void CreateTabWithRoutineDetails(Data.DB2ConnectionManager connectionManager, string schema, string routineName, string routineType)
    {
        Logger.Info("Creating tab with routine details: {Schema}.{Routine} ({Type})", schema, routineName, routineType);
        
        try
        {
            if (ConnectionTabs.SelectedItem is TabItem selectedTab && 
                selectedTab.Content is Controls.ConnectionTabControl activeTab)
            {
                var connection = activeTab.Connection;
                var detailsPanel = new Controls.RoutineDetailsPanel(connectionManager, schema, routineName, routineType);
                
                var icon = routineType == "F" ? "🔧" : "⚙️";
                var typeLabel = routineType == "F" ? "Func" : "Proc";
                var newTabItem = new TabItem
                {
                    Header = CreateTabHeader($"{icon} {routineName?.Trim()} - {connection.GetDisplayName()}"),
                    Content = detailsPanel,
                    Tag = new { Type = "RoutineDetails", Schema = schema, RoutineName = routineName, RoutineType = routineType }
                };

                ConnectionTabs.Items.Add(newTabItem);
                ConnectionTabs.SelectedItem = newTabItem;
                Logger.Info("New tab created with routine details content");
            }
            else
            {
                MessageBox.Show("No active connection found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to create tab with routine details");
            MessageBox.Show($"Failed to create tab: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    /// <summary>
    /// Create a new tab with package details content
    /// </summary>
    public void CreateTabWithPackageDetails(Data.DB2ConnectionManager connectionManager, Models.PackageInfo package)
    {
        Logger.Info("Creating tab with package details: {Schema}.{Package}", package.PackageSchema, package.PackageName);
        
        try
        {
            if (ConnectionTabs.SelectedItem is TabItem selectedTab && 
                selectedTab.Content is Controls.ConnectionTabControl activeTab)
            {
                var connection = activeTab.Connection;
                var detailsPanel = new Controls.PackageDetailsPanel(connectionManager, package);
                
                var newTabItem = new TabItem
                {
                    Header = CreateTabHeader($"📦 {package.PackageName?.Trim()} - {connection.GetDisplayName()}"),
                    Content = detailsPanel,
                    Tag = new { Type = "PackageDetails", Package = package }
                };

                ConnectionTabs.Items.Add(newTabItem);
                ConnectionTabs.SelectedItem = newTabItem;
                Logger.Info("New tab created with package details content");
            }
            else
            {
                MessageBox.Show("No active connection found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to create tab with package details");
            MessageBox.Show($"Failed to create tab: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    /// <summary>
    /// Update menu visibility based on user's access level - RBAC
    /// </summary>
    private void UpdateMenuVisibilityForAccessLevel(Models.UserPermissions? permissions)
    {
        if (permissions == null)
        {
            Logger.Debug("No permissions determined, skipping menu visibility update");
            return;
        }
        
        Logger.Info("Updating menu visibility for access level: {Level}", permissions.AccessLevel);
        
        // For now, just log what would be hidden
        // Full implementation would iterate through all menu items and check Tag property
        // This is a simplified version that ensures builds succeed
        
        Logger.Debug("Access level: {Level} - Badge: {Badge}", 
            permissions.AccessLevel, permissions.AccessLevelBadge);
        Logger.Debug("User can execute DDL: {DDL}, DML: {DML}", 
            permissions.CanExecuteDDL, permissions.CanExecuteDML);
        
        // TODO: Full menu visibility implementation
        // Would iterate MainMenu items and check Tag="AccessLevel:XXX"
        // For now, all menus remain visible (graceful degradation)
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
        
        // Add context menu for undock/close options
        var contextMenu = new ContextMenu();
        
        var undockItem = new MenuItem { Header = "🪟 Undock to Floating Window" };
        undockItem.Click += (s, e) =>
        {
            var menuItem = s as MenuItem;
            var menu = menuItem?.Parent as ContextMenu;
            var panel = menu?.PlacementTarget as StackPanel;
            var tabItem = panel?.Parent as TabItem;
            if (tabItem != null)
            {
                UndockTab(tabItem, title);
            }
        };
        contextMenu.Items.Add(undockItem);
        
        contextMenu.Items.Add(new Separator());
        
        var closeItem = new MenuItem { Header = "❌ Close Tab" };
        closeItem.Click += (s, e) =>
        {
            var menuItem = s as MenuItem;
            var menu = menuItem?.Parent as ContextMenu;
            var panel = menu?.PlacementTarget as StackPanel;
            var tabItem = panel?.Parent as TabItem;
            if (tabItem != null)
            {
                CloseTab(tabItem);
            }
        };
        contextMenu.Items.Add(closeItem);
        
        stackPanel.ContextMenu = contextMenu;

        return stackPanel;
    }
    
    /// <summary>
    /// Undock a tab to a floating window
    /// </summary>
    private void UndockTab(TabItem tabItem, string title)
    {
        Logger.Info("Undocking tab: {Title}", title);
        
        try
        {
            var content = tabItem.Content;
            if (content == null)
            {
                Logger.Warn("Tab has no content to undock");
                return;
            }
            
            // Remove the tab from the TabControl first to fully disconnect it
            ConnectionTabs.Items.Remove(tabItem);
            
            // Remove content from tab (content can only have one parent)
            tabItem.Content = null;
            
            // Force a layout update to ensure the element is fully disconnected
            tabItem.UpdateLayout();
            
            // Create floating window
            var floatingWindow = new Window
            {
                Title = title,
                Width = 1200,
                Height = 800,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            
            // Apply ModernWPF theme
            Services.ThemedWindowHelper.ApplyTheme(floatingWindow);
            
            // Store reference to original tab info for potential re-docking
            floatingWindow.Tag = new FloatingWindowInfo { OriginalTitle = title, TabItem = tabItem };
            
            // Add context menu with re-dock option
            var dockButton = new Button
            {
                Content = "📌 Dock Back",
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 5, 5, 0),
                Padding = new Thickness(10, 5, 10, 5)
            };
            
            dockButton.Click += (s, e) =>
            {
                RedockWindow(floatingWindow);
            };
            
            // Wrap content in a grid to add dock button
            if (content is UIElement uiContent)
            {
                var grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                
                // Dock button bar
                var buttonBar = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Background = (System.Windows.Media.Brush)FindResource("SystemControlBackgroundAltHighBrush")
                };
                buttonBar.Children.Add(dockButton);
                Grid.SetRow(buttonBar, 0);
                grid.Children.Add(buttonBar);
                
                // Main content
                Grid.SetRow(uiContent, 1);
                grid.Children.Add(uiContent);
                
                floatingWindow.Content = grid;
            }
            else
            {
                floatingWindow.Content = content;
            }
            
            UpdateWelcomePanelVisibility();
            
            // Handle window close - cleanup
            floatingWindow.Closed += (s, e) =>
            {
                // If window was closed without re-docking, content is lost
                // That's expected behavior for "close window"
                Logger.Info("Floating window closed: {Title}", title);
            };
            
            floatingWindow.Show();
            Logger.Info("Tab undocked to floating window: {Title}", title);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to undock tab");
            MessageBox.Show($"Failed to undock tab: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    /// <summary>
    /// Re-dock a floating window back to a tab
    /// </summary>
    private void RedockWindow(Window floatingWindow)
    {
        Logger.Info("Re-docking window: {Title}", floatingWindow.Title);
        
        try
        {
            var info = floatingWindow.Tag as FloatingWindowInfo;
            var title = info?.OriginalTitle ?? floatingWindow.Title;
            
            // Extract the main content from the grid wrapper
            UIElement? content = null;
            if (floatingWindow.Content is Grid grid && grid.Children.Count >= 2)
            {
                content = grid.Children[1] as UIElement;
                if (content != null)
                {
                    grid.Children.Remove(content);
                }
            }
            else
            {
                content = floatingWindow.Content as UIElement;
            }
            
            if (content == null)
            {
                Logger.Warn("No content to re-dock");
                return;
            }
            
            // Clear window content
            floatingWindow.Content = null;
            
            // Create new tab with the content
            var newTabItem = new TabItem
            {
                Header = CreateTabHeader(title),
                Content = content
            };
            
            ConnectionTabs.Items.Add(newTabItem);
            ConnectionTabs.SelectedItem = newTabItem;
            UpdateWelcomePanelVisibility();
            
            // Close the floating window
            floatingWindow.Close();
            
            Logger.Info("Window re-docked as tab: {Title}", title);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to re-dock window");
            MessageBox.Show($"Failed to re-dock: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    /// <summary>
    /// Info stored in floating window's Tag for re-docking
    /// </summary>
    private class FloatingWindowInfo
    {
        public string OriginalTitle { get; set; } = "";
        public TabItem? TabItem { get; set; }
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
            UpdateWelcomePanelVisibility();

            Logger.Info("Tab closed successfully");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error closing tab");
        }
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

    /// <summary>
    /// Increase font size (Ctrl+Plus) - applies to entire application
    /// </summary>
    private void IncreaseFontSize()
    {
        App.PreferencesService?.IncreaseFontSize();
        
        // Apply to entire application
        if (App.PreferencesService != null)
        {
            GlobalFontService.ApplyAllPreferences(App.PreferencesService.Preferences);
        }
        
        RefreshAllConnectionTabs();
        Logger.Info("All font sizes increased - UI: {Size}", App.PreferencesService?.Preferences.UIFontSize);
    }

    /// <summary>
    /// Decrease font size (Ctrl+Minus) - applies to entire application
    /// </summary>
    private void DecreaseFontSize()
    {
        App.PreferencesService?.DecreaseFontSize();
        
        // Apply to entire application
        if (App.PreferencesService != null)
        {
            GlobalFontService.ApplyAllPreferences(App.PreferencesService.Preferences);
        }
        
        RefreshAllConnectionTabs();
        Logger.Info("All font sizes decreased - UI: {Size}", App.PreferencesService?.Preferences.UIFontSize);
    }

    /// <summary>
    /// Increase TreeView spacing (Ctrl+Shift+Plus)
    /// </summary>
    private void IncreaseTreeViewSpacing()
    {
        App.PreferencesService?.IncreaseTreeViewSpacing();
        RefreshAllConnectionTabs();
        Logger.Info("TreeView spacing increased to {Spacing}", App.PreferencesService?.Preferences.TreeViewItemSpacing);
    }

    /// <summary>
    /// Decrease TreeView spacing (Ctrl+Shift+Minus)
    /// </summary>
    private void DecreaseTreeViewSpacing()
    {
        App.PreferencesService?.DecreaseTreeViewSpacing();
        RefreshAllConnectionTabs();
        Logger.Info("TreeView spacing decreased to {Spacing}", App.PreferencesService?.Preferences.TreeViewItemSpacing);
    }

    /// <summary>
    /// Show SQL Query History dialog (Ctrl+Shift+Q)
    /// </summary>
    private void ShowSqlQueryHistory()
    {
        Logger.Info("Opening SQL Query History dialog");
        var dialog = new SqlQueryHistoryDialog { Owner = this };
        dialog.ShowDialog();
    }

    /// <summary>
    /// Refresh all connection tabs to apply new preferences
    /// </summary>
    private void RefreshAllConnectionTabs()
    {
        foreach (TabItem tab in ConnectionTabs.Items)
        {
            if (tab.Content is ConnectionTabControl connectionTab)
            {
                connectionTab.ApplyGridPreferences();
            }
        }
    }

    private void ToggleDarkMode_Click(object sender, RoutedEventArgs e)
    {
        _themeService.ToggleTheme();
        _themeService.SaveThemePreference();
        
        // Apply theme-appropriate colors to editor and grids
        _themeService.ApplyThemeColorsToPreferences();
        
        // Refresh all open connection tabs to apply new colors
        RefreshAllConnectionTabs();
        
        // Apply global font size (which also refreshes grids)
        if (App.PreferencesService != null)
        {
            GlobalFontService.ApplyAllPreferences(App.PreferencesService.Preferences);
        }
        
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
        Logger.Info("Query history requested - now integrated into each connection tab");
        MessageBox.Show("Query History is now integrated into each connection tab!\n\n" +
                       "Look for the '📜 History' tab in the left panel of any open connection.\n\n" +
                       "Features:\n" +
                       "• Search queries by text\n" +
                       "• Filter by connection\n" +
                       "• Double-click to load query\n" +
                       "• Encrypted SQL storage",
                       "Query History", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void LockMonitor_Click(object sender, RoutedEventArgs e)
    {
        OpenPanelAsTab<LockMonitorPanel>("Lock Monitor", "🔒");
    }
    
    private void StatisticsManager_Click(object sender, RoutedEventArgs e)
    {
        OpenPanelAsTab<StatisticsManagerPanel>("Statistics Manager", "📊");
    }
    
    private void ActiveSessions_Click(object sender, RoutedEventArgs e)
    {
        OpenPanelAsTab<ActiveSessionsPanel>("Active Sessions", "👥");
    }
    
    private void CdcManager_Click(object sender, RoutedEventArgs e)
    {
        OpenPanelAsTab<CdcManagerPanel>("CDC Manager", "🔄");
    }
    
    private void UnusedObjects_Click(object sender, RoutedEventArgs e)
    {
        OpenPanelAsTab<UnusedObjectsPanel>("Unused Objects", "🗑️");
    }
    
    private void SourceBrowser_Click(object sender, RoutedEventArgs e)
    {
        OpenPanelAsTab<SourceCodeBrowserPanel>("Source Code Browser", "📄");
    }
    
    // DDL Generator is now accessed via context menu on individual objects in the Object Browser
    // Right-click any object and select "Generate DDL" → "Generate CREATE Statement..." or "Generate DROP Statement..."
    private void DdlGenerator_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(
            "DDL Generation is now available via context menu!\n\n" +
            "Right-click any database object in the Object Browser and select:\n" +
            "• Generate DDL → Generate CREATE Statement...\n" +
            "• Generate DDL → Generate DROP Statement...\n\n" +
            "The generated DDL will open in a new tab.",
            "DDL Generator", 
            MessageBoxButton.OK, 
            MessageBoxImage.Information);
    }
    
    private void CommentManager_Click(object sender, RoutedEventArgs e)
    {
        OpenPanelAsTab<CommentManagerPanel>("Comment Manager", "💬");
    }
    
    private void PackageAnalyzer_Click(object sender, RoutedEventArgs e)
    {
        OpenPanelAsTab<PackageAnalyzerPanel>("Package Analyzer", "📦");
    }
    
    private void DependencyAnalyzer_Click(object sender, RoutedEventArgs e)
    {
        OpenPanelAsTab<DependencyGraphPanel>("Dependency Analyzer", "🔗");
    }
    
    private void MigrationAssistant_Click(object sender, RoutedEventArgs e)
    {
        OpenPanelAsTab<MigrationAssistantPanel>("Migration Assistant", "🚀");
    }
    
    private void MermaidDesigner_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Opening Mermaid Visual Designer");
        
        if (ConnectionTabs.SelectedItem is not TabItem selectedTab || selectedTab.Content is not ConnectionTabControl activeTab)
        {
            MessageBox.Show("Please connect to a database first.", "No Active Connection", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        
        var connectionManager = activeTab.ConnectionManager;
        var schema = activeTab.Connection.Database ?? "FK";
        
        var designerWindow = new MermaidDesignerWindow(connectionManager, schema);
        designerWindow.Owner = this;
        designerWindow.ShowDialog();
    }
    
    private void CrossDatabaseComparison_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Opening Cross-Database Schema Comparison dialog");
        
        try
        {
            // The new dialog doesn't require an active connection - it uses connection profiles
            var dialog = new CrossDatabaseComparisonDialog();
            dialog.Owner = this;
            dialog.ShowDialog();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to open Cross-Database Comparison dialog");
            MessageBox.Show($"Failed to open Cross-Database Comparison: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void OpenMonitorPanel<T>(string title, int width, int height) where T : UserControl, new()
    {
        Logger.Info("Opening {Panel}", title);
        
        if (ConnectionTabs.SelectedItem is not TabItem selectedTab || selectedTab.Content is not ConnectionTabControl activeTab)
        {
            MessageBox.Show("No active database connection.\n\nPlease connect to a database first.",
                title, MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        
        if (activeTab.ConnectionManager == null)
        {
            MessageBox.Show("Connection is not active.", title, MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        
        try
        {
            var connectionName = GetTabHeaderText(selectedTab);
            var window = new Window
            {
                Title = $"{title} - {connectionName}",
                Width = width,
                Height = height,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };
            
            // Apply ModernWPF theme-aware styling
            ModernWpf.Controls.Primitives.WindowHelper.SetUseModernWindowStyle(window, true);
            window.SetResourceReference(Window.BackgroundProperty, "SystemControlBackgroundAltHighBrush");
            window.SetResourceReference(Window.ForegroundProperty, "SystemControlForegroundBaseHighBrush");
            
            var panel = new T();
            window.Content = panel;
            
            window.Loaded += async (s, args) =>
            {
                try
                {
                    var initMethod = panel.GetType().GetMethod("InitializeAsync");
                    if (initMethod != null)
                    {
                        var task = initMethod.Invoke(panel, new object[] { activeTab.ConnectionManager }) as Task;
                        if (task != null) await task;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "Failed to initialize {Panel}", title);
                    MessageBox.Show($"Failed to initialize:\n\n{ex.Message}",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    window.Close();
                }
            };
            
            window.Show(); // Non-modal so user can still interact with main window
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to open {Panel}", title);
            MessageBox.Show($"Failed to open:\n\n{ex.Message}",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    /// <summary>
    /// Open a tool panel as a docked tab in the main window
    /// </summary>
    private void OpenPanelAsTab<T>(string title, string icon) where T : UserControl, new()
    {
        Logger.Info("Opening {Panel} as tab", title);
        
        if (ConnectionTabs.SelectedItem is not TabItem selectedTab || selectedTab.Content is not ConnectionTabControl activeTab)
        {
            MessageBox.Show("No active database connection.\n\nPlease connect to a database first.",
                title, MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        
        if (activeTab.ConnectionManager == null)
        {
            MessageBox.Show("Connection is not active.", title, MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        
        try
        {
            var connection = activeTab.Connection;
            var panel = new T();
            
            var newTabItem = new TabItem
            {
                Header = CreateTabHeader($"{icon} {title} - {connection.GetDisplayName()}"),
                Content = panel,
                Tag = new { Type = "ToolPanel", ToolName = title }
            };

            ConnectionTabs.Items.Add(newTabItem);
            ConnectionTabs.SelectedItem = newTabItem;
            
            // Initialize the panel asynchronously
            _ = Task.Run(async () =>
            {
                try
                {
                    await Dispatcher.InvokeAsync(async () =>
                    {
                        var initMethod = panel.GetType().GetMethod("InitializeAsync");
                        if (initMethod != null)
                        {
                            var task = initMethod.Invoke(panel, new object[] { activeTab.ConnectionManager }) as Task;
                            if (task != null) await task;
                        }
                    });
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "Failed to initialize {Panel}", title);
                    Dispatcher.Invoke(() => MessageBox.Show($"Failed to initialize:\n\n{ex.Message}",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error));
                }
            });
            
            Logger.Info("New tab created with {Panel}", title);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to open {Panel} as tab", title);
            MessageBox.Show($"Failed to open:\n\n{ex.Message}",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void DatabaseLoadMonitor_Click(object sender, RoutedEventArgs e)
    {
        OpenPanelAsTab<DatabaseLoadMonitorPanel>("Database Load Monitor", "📈");
    }

    private void Settings_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Settings dialog requested");

        var settingsDialog = new Dialogs.NewSettingsDialog(_preferencesService)
        {
            Owner = this
        };

        if (settingsDialog.ShowDialog() == true)
        {
            Logger.Info("Settings saved successfully");
            
            // Refresh all connection tabs with new preferences
            RefreshAllConnectionTabs();
        }
    }

    // WelcomePanel Event Handlers
    private void WelcomePanel_ConnectionRequested(object? sender, Models.DB2Connection connection)
    {
        Logger.Info("Connection requested from welcome panel: {ConnectionName}", connection.Name);
        AddConnectionTab(connection);
    }
    
    private void WelcomePanel_NewConnectionRequested(object? sender, EventArgs e)
    {
        Logger.Info("New connection requested from welcome panel");
        NewConnection_Click(sender ?? this, new RoutedEventArgs());
    }
    
    private void WelcomePanel_ManageConnectionsRequested(object? sender, EventArgs e)
    {
        Logger.Info("Manage connections requested from welcome panel");
        // TODO: Create a connection management dialog
        MessageBox.Show(
            "Connection management coming soon!\n\n" +
            "For now, use File → New Connection to create connections,\n" +
            "and right-click on recent connections to edit or delete them.",
            "Manage Connections",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
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
    
    #region Edit Menu Handlers
    
    private void EditUndo_Click(object sender, RoutedEventArgs e)
    {
        if (GetActiveEditor() is { } editor && editor.CanUndo)
            editor.Undo();
    }
    
    private void EditRedo_Click(object sender, RoutedEventArgs e)
    {
        if (GetActiveEditor() is { } editor && editor.CanRedo)
            editor.Redo();
    }
    
    private void EditCut_Click(object sender, RoutedEventArgs e)
    {
        if (GetActiveEditor() is { } editor)
            editor.Cut();
    }
    
    private void EditCopy_Click(object sender, RoutedEventArgs e)
    {
        if (GetActiveEditor() is { } editor)
            editor.Copy();
    }
    
    private void EditPaste_Click(object sender, RoutedEventArgs e)
    {
        if (GetActiveEditor() is { } editor)
            editor.Paste();
    }
    
    private void EditSelectAll_Click(object sender, RoutedEventArgs e)
    {
        if (GetActiveEditor() is { } editor)
            editor.SelectAll();
    }
    
    private void EditFind_Click(object sender, RoutedEventArgs e)
    {
        if (GetActiveTabControl() is { } tabControl)
            tabControl.ShowFindDialog();
    }
    
    private void EditReplace_Click(object sender, RoutedEventArgs e)
    {
        if (GetActiveTabControl() is { } tabControl)
            tabControl.ShowReplaceDialog();
    }
    
    private void EditFormatSql_Click(object sender, RoutedEventArgs e)
    {
        if (GetActiveTabControl() is { } tabControl)
            tabControl.FormatSql();
    }
    
    private ICSharpCode.AvalonEdit.TextEditor? GetActiveEditor()
    {
        if (ConnectionTabs.SelectedItem is TabItem selectedTab && 
            selectedTab.Content is ConnectionTabControl tabControl)
        {
            return tabControl.SqlEditor;
        }
        return null;
    }
    
    private ConnectionTabControl? GetActiveTabControl()
    {
        if (ConnectionTabs.SelectedItem is TabItem selectedTab && 
            selectedTab.Content is ConnectionTabControl tabControl)
        {
            return tabControl;
        }
        return null;
    }
    
    #endregion
    
    #region Tools Menu Handlers
    
    private void ManageConnections_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Manage connections clicked");
        WelcomePanel_ManageConnectionsRequested(sender, e);
    }
    
    private void ExecuteSqlScript_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Execute SQL script clicked");
        if (GetActiveTabControl() is { } tabControl)
        {
            tabControl.OpenSqlFile();
        }
        else
        {
            MessageBox.Show("Please connect to a database first.", "No Active Connection", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
    
    private void GenerateSchemaDdl_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Generate Schema DDL clicked");
        DdlGenerator_Click(sender, e);
    }
    
    #endregion
    
    #region Help Menu Handlers
    
    private void KeyboardShortcuts_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Keyboard shortcuts dialog requested");
        var shortcuts = @"
╔══════════════════════════════════════════════════════════════════╗
║                    KEYBOARD SHORTCUTS                            ║
╠══════════════════════════════════════════════════════════════════╣
║ FILE OPERATIONS                                                  ║
╟──────────────────────────────────────────────────────────────────╢
║ Ctrl+N         New Connection                                    ║
║ Ctrl+W/F4      Close Current Tab                                 ║
║ Ctrl+O         Open SQL Script                                   ║
║ Ctrl+S         Save SQL Script                                   ║
║ Alt+F4         Exit Application                                  ║
╟──────────────────────────────────────────────────────────────────╢
║ EDITING                                                          ║
╟──────────────────────────────────────────────────────────────────╢
║ Ctrl+Z         Undo                                              ║
║ Ctrl+Y         Redo                                              ║
║ Ctrl+X         Cut                                               ║
║ Ctrl+C         Copy                                              ║
║ Ctrl+V         Paste                                             ║
║ Ctrl+A         Select All                                        ║
║ Ctrl+F         Find                                              ║
║ Ctrl+H         Find and Replace                                  ║
║ Ctrl+Shift+F   Format SQL                                        ║
╟──────────────────────────────────────────────────────────────────╢
║ QUERY EXECUTION                                                  ║
╟──────────────────────────────────────────────────────────────────╢
║ F5             Execute Query                                     ║
║ Ctrl+Enter     Execute Current Statement                         ║
╟──────────────────────────────────────────────────────────────────╢
║ VIEW                                                             ║
╟──────────────────────────────────────────────────────────────────╢
║ Ctrl+D         Toggle Dark/Light Theme                           ║
║ Ctrl+Shift+C   Database Comparison                               ║
║ Ctrl+Shift+Q   Show SQL Query History                            ║
╚══════════════════════════════════════════════════════════════════╝";
        
        MessageBox.Show(shortcuts, "Keyboard Shortcuts", MessageBoxButton.OK, MessageBoxImage.Information);
    }
    
    private void Documentation_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Documentation clicked");
        try
        {
            var docPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MarkdownDoc");
            if (Directory.Exists(docPath))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = docPath,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("Documentation folder not found.\n\nCheck the MarkdownDoc folder in the application directory.", 
                    "Documentation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to open documentation");
            MessageBox.Show($"Failed to open documentation: {ex.Message}", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void ViewLogs_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("View logs clicked");
        try
        {
            var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            if (Directory.Exists(logPath))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = logPath,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("Logs folder not found.", "View Logs", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to open logs folder");
            MessageBox.Show($"Failed to open logs: {ex.Message}", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void OpenDataFolder_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Open data folder clicked");
        try
        {
            var dataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "WindowsDb2Editor");
            
            if (!Directory.Exists(dataPath))
                Directory.CreateDirectory(dataPath);
                
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = dataPath,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to open data folder");
            MessageBox.Show($"Failed to open data folder: {ex.Message}", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    #endregion

    /// <summary>
    /// Populate the Recent Connections menu with saved connections
    /// </summary>
    private void PopulateRecentConnections()
    {
        Logger.Debug("Populating recent connections menu");
        
        try
        {
            var recentHistories = _historyService.GetRecentConnections(10);
            
            // Clear existing items
            RecentConnectionsMenuItem.Items.Clear();
            
            if (recentHistories.Count == 0)
            {
                var noConnectionsItem = new MenuItem
                {
                    Header = "(No recent connections)",
                    IsEnabled = false
                };
                RecentConnectionsMenuItem.Items.Add(noConnectionsItem);
                Logger.Debug("No recent connections found");
            }
            else
            {
                Logger.Info("Found {Count} recent connections", recentHistories.Count);
                
                foreach (var history in recentHistories)
                {
                    // Get full connection with decrypted password
                    var connection = _connectionStorageService.GetConnection(history.ProfileName);
                    if (connection == null) continue;
                    
                    var menuItem = new MenuItem
                    {
                        Header = $"{connection.Name} ({connection.Database}@{connection.Server}) - {history.LastUsedRelative}",
                        Tag = connection
                    };
                    
                    // Create submenu with options
                    var connectItem = new MenuItem { Header = "🔗 Connect", Tag = connection };
                    connectItem.Click += RecentConnection_Click;
                    
                    var editItem = new MenuItem { Header = "✏️ Edit", Tag = connection };
                    editItem.Click += RecentConnectionEdit_Click;
                    
                    var copyItem = new MenuItem { Header = "📋 Duplicate", Tag = connection };
                    copyItem.Click += RecentConnectionCopy_Click;
                    
                    var testItem = new MenuItem { Header = "🧪 Test Connection", Tag = connection };
                    testItem.Click += RecentConnectionTest_Click;
                    
                    var deleteItem = new MenuItem { Header = "🗑️ Delete", Tag = connection };
                    deleteItem.Click += RecentConnectionDelete_Click;
                    
                    menuItem.Items.Add(connectItem);
                    menuItem.Items.Add(new Separator());
                    menuItem.Items.Add(editItem);
                    menuItem.Items.Add(copyItem);
                    menuItem.Items.Add(testItem);
                    menuItem.Items.Add(new Separator());
                    menuItem.Items.Add(deleteItem);
                    
                    // Double-click on main item = connect
                    menuItem.Click += RecentConnection_Click;
                    
                    RecentConnectionsMenuItem.Items.Add(menuItem);
                }
                
                // Add separator and clear history option
                RecentConnectionsMenuItem.Items.Add(new Separator());
                
                var clearHistoryItem = new MenuItem
                {
                    Header = "🗑️ Clear History"
                };
                clearHistoryItem.Click += ClearRecentConnections_Click;
                RecentConnectionsMenuItem.Items.Add(clearHistoryItem);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to populate recent connections");
        }
    }
    
    /// <summary>
    /// Handle click on a recent connection menu item
    /// </summary>
    private void RecentConnection_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem menuItem || menuItem.Tag is not Models.DB2Connection connection)
        {
            return;
        }
        
        Logger.Info("Opening recent connection: {Name}", connection.Name);
        
        try
        {
            // Open tab with this connection
            AddConnectionTab(connection);
            
            // Refresh menu to update order
            PopulateRecentConnections();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to open recent connection: {Name}", connection.Name);
            MessageBox.Show($"Failed to open connection:\n\n{ex.Message}",
                "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void RecentConnectionEdit_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem menuItem || menuItem.Tag is not Models.DB2Connection connection)
            return;
        
        Logger.Info("Editing recent connection: {Name}", connection.Name);
        
        var dialog = new Dialogs.ConnectionDialog();
        dialog.LoadConnection(connection);
        dialog.Owner = this;
        
        if (dialog.ShowDialog() == true && dialog.Connection != null)
        {
            // Update connection - delete old if name changed, save new
            if (connection.Name != dialog.Connection.Name)
            {
                _connectionStorageService.DeleteConnection(connection.Name);
            }
            
            _connectionStorageService.SaveConnection(dialog.Connection);
            PopulateRecentConnections();
            
            MessageBox.Show(
                $"Connection '{dialog.Connection.Name}' updated successfully.",
                "Connection Updated",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
    
    private void RecentConnectionCopy_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem menuItem || menuItem.Tag is not Models.DB2Connection connection)
            return;
        
        Logger.Info("Duplicating recent connection: {Name}", connection.Name);
        
        var copy = new Models.DB2Connection
        {
            Name = $"{connection.Name} (Copy)",
            Server = connection.Server,
            Port = connection.Port,
            Database = connection.Database,
            Username = connection.Username,
            Password = connection.Password,
            SavePassword = connection.SavePassword,
            IsReadOnly = connection.IsReadOnly,
            AutoCommit = connection.AutoCommit
        };
        
        var dialog = new Dialogs.ConnectionDialog();
        dialog.LoadConnection(copy);
        dialog.Owner = this;
        
        if (dialog.ShowDialog() == true && dialog.Connection != null)
        {
            _connectionStorageService.SaveConnection(dialog.Connection);
            PopulateRecentConnections();
            
            MessageBox.Show(
                $"Connection '{dialog.Connection.Name}' created successfully.",
                "Connection Duplicated",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
    
    private async void RecentConnectionTest_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem menuItem || menuItem.Tag is not Models.DB2Connection connection)
            return;
        
        Logger.Info("Testing recent connection: {Name}", connection.Name);
        
        try
        {
            var connectionManager = new Data.DB2ConnectionManager(connection);
            var result = await connectionManager.TestConnectionAsync();
            
            if (result)
            {
                MessageBox.Show(
                    $"✅ Connection to '{connection.Name}' successful!",
                    "Connection Test",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(
                    $"❌ Connection to '{connection.Name}' failed.",
                    "Connection Test",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Connection test failed for {Name}", connection.Name);
            MessageBox.Show(
                $"❌ Connection test failed:\n\n{ex.Message}",
                "Connection Test Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
    
    private void RecentConnectionDelete_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuItem menuItem || menuItem.Tag is not Models.DB2Connection connection)
            return;
        
        Logger.Info("Deleting recent connection: {Name}", connection.Name);
        
        var result = MessageBox.Show(
            $"Permanently delete connection '{connection.Name}'?\n\n" +
            "This action cannot be undone.",
            "Delete Connection",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
        
        if (result == MessageBoxResult.Yes)
        {
            _connectionStorageService.DeleteConnection(connection.Name);
            _historyService.RemoveConnection(connection.Name);
            PopulateRecentConnections();
            
            MessageBox.Show(
                $"Connection '{connection.Name}' deleted successfully.",
                "Connection Deleted",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
    
    /// <summary>
    /// Clear all recent connections
    /// </summary>
    private void ClearRecentConnections_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Clear recent connections requested");
        
        var result = MessageBox.Show(
            "Are you sure you want to clear all recent connections?\n\nThis will remove all saved connection information including passwords.",
            "Clear Recent Connections",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);
        
        if (result == MessageBoxResult.Yes)
        {
            try
            {
                var filePath = _connectionStorageService.GetConnectionsFilePath();
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Logger.Info("Connections file deleted: {Path}", filePath);
                }
                
                PopulateRecentConnections();
                
                MessageBox.Show("Recent connections cleared successfully.",
                    "Clear Recent Connections", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to clear recent connections");
                MessageBox.Show($"Failed to clear recent connections:\n\n{ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    /// <summary>
    /// Auto-connect to a profile and open a details dialog for any object type
    /// Used for automated testing and debugging
    /// </summary>
    public async Task AutoConnectAndOpenAsync(Models.DB2Connection connection, string elementName, string? objectType = null, string? tabName = null)
    {
        Logger.Info("Auto-connecting to {Profile} and opening {Type}: {Element}, Tab: {Tab}", connection.Name, objectType ?? "table", elementName, tabName ?? "default");
        
        try
        {
            // Add connection tab
            AddConnectionTab(connection);
            
            // Wait for connection to establish
            await Task.Delay(2000);
            
            // Find the newly added tab
            if (ConnectionTabs.Items.Count > 0)
            {
                var tabItem = ConnectionTabs.Items[ConnectionTabs.Items.Count - 1] as TabItem;
                if (tabItem?.Content is ConnectionTabControl tabControl)
                {
                    Logger.Info("Tab found, waiting for connection to open...");
                    await Task.Delay(3000); // Wait for connection to be fully established
                    
                    // Determine object type and open appropriate dialog
                    Window? dialog = (objectType?.ToLowerInvariant()) switch
                    {
                        "table" or null => OpenTableDialog(tabControl, elementName, tabName),
                        "view" => await OpenViewDialogAsync(tabControl, elementName, tabName),
                        "procedure" => await OpenProcedureDialogAsync(tabControl, elementName, tabName),
                        "function" => await OpenFunctionDialogAsync(tabControl, elementName, tabName),
                        "index" => await OpenIndexDialogAsync(tabControl, elementName, tabName),
                        "trigger" => await OpenTriggerDialogAsync(tabControl, elementName, tabName),
                        "sequence" => await OpenSequenceDialogAsync(tabControl, elementName, tabName),
                        "synonym" => await OpenSynonymDialogAsync(tabControl, elementName, tabName),
                        "type" => await OpenTypeDialogAsync(tabControl, elementName, tabName),
                        "package" => await OpenPackageDialogAsync(tabControl, elementName, tabName),
                        "user" or "role" or "group" => await OpenUserDialogAsync(tabControl, elementName, objectType, tabName),
                        "mermaid" => await OpenMermaidDesignerAsync(tabControl, elementName, tabName),
                        _ => throw new ArgumentException($"Unknown object type: {objectType}")
                    };
                    
                    if (dialog != null)
                    {
                        dialog.Owner = this;
                        dialog.Show();
                        
                        // Activate tab after dialog is fully loaded and data populated
                        if (!string.IsNullOrEmpty(tabName))
                        {
                            await Task.Delay(2000); // Wait for async data loading
                            
                            // Activate tab based on dialog type
                            if (dialog is Dialogs.TableDetailsDialog tableDialog)
                            {
                                tableDialog.ActivateTab(tabName);
                            }
                            else if (dialog is Dialogs.ObjectDetailsDialog objectDialog)
                            {
                                objectDialog.ActivateTab(tabName);
                            }
                            else if (dialog is Dialogs.PackageDetailsDialog packageDialog)
                            {
                                packageDialog.ActivateTab(tabName);
                            }
                            else if (dialog is Dialogs.UserDetailsDialog userDialog)
                            {
                                userDialog.ActivateTab(tabName);
                            }
                            else if (dialog is Dialogs.MermaidDesignerWindow mermaidDialog)
                            {
                                // Mermaid designer has no tabs, but may need initial data load
                                await Task.Delay(1000);
                            }
                        }
                        
                        Logger.Info("{Type} dialog opened successfully on tab: {Tab}", objectType ?? "table", tabName ?? "default");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to auto-connect and open element");
            throw;
        }
    }
    
    private Window OpenTableDialog(ConnectionTabControl tabControl, string fullName, string? tabName)
    {
        var dialog = new Dialogs.TableDetailsDialog(tabControl.ConnectionManager, fullName);
        if (!string.IsNullOrEmpty(tabName))
        {
            // Need to activate tab after dialog loads
            dialog.Loaded += (s, e) => dialog.ActivateTab(tabName);
        }
        return dialog;
    }
    
    private async Task<Window> OpenViewDialogAsync(ConnectionTabControl tabControl, string fullName, string? tabName)
    {
        var parts = fullName.Split('.');
        var databaseObject = new Models.DatabaseObject
        {
            SchemaName = parts.Length == 2 ? parts[0] : "UNKNOWN",
            Name = parts.Length == 2 ? parts[1] : fullName,
            FullName = fullName,
            Type = Models.ObjectType.Views,
            Icon = "👁️"
        };
        
        var dialog = new Dialogs.ObjectDetailsDialog(tabControl.ConnectionManager, databaseObject, tabControl.Connection);
        if (!string.IsNullOrEmpty(tabName))
        {
            dialog.Loaded += (s, e) => dialog.ActivateTab(tabName);
        }
        return dialog;
    }
    
    private async Task<Window> OpenProcedureDialogAsync(ConnectionTabControl tabControl, string fullName, string? tabName)
    {
        var parts = fullName.Split('.');
        var databaseObject = new Models.DatabaseObject
        {
            SchemaName = parts.Length == 2 ? parts[0] : "UNKNOWN",
            Name = parts.Length == 2 ? parts[1] : fullName,
            FullName = fullName,
            Type = Models.ObjectType.Procedures,
            Icon = "⚙️"
        };
        
        var dialog = new Dialogs.ObjectDetailsDialog(tabControl.ConnectionManager, databaseObject, tabControl.Connection);
        if (!string.IsNullOrEmpty(tabName))
        {
            dialog.Loaded += (s, e) => dialog.ActivateTab(tabName);
        }
        return dialog;
    }
    
    private async Task<Window> OpenFunctionDialogAsync(ConnectionTabControl tabControl, string fullName, string? tabName)
    {
        var parts = fullName.Split('.');
        var databaseObject = new Models.DatabaseObject
        {
            SchemaName = parts.Length == 2 ? parts[0] : "UNKNOWN",
            Name = parts.Length == 2 ? parts[1] : fullName,
            FullName = fullName,
            Type = Models.ObjectType.Functions,
            Icon = "🔧"
        };
        
        var dialog = new Dialogs.ObjectDetailsDialog(tabControl.ConnectionManager, databaseObject, tabControl.Connection);
        if (!string.IsNullOrEmpty(tabName))
        {
            dialog.Loaded += (s, e) => dialog.ActivateTab(tabName);
        }
        return dialog;
    }
    
    private async Task<Window> OpenIndexDialogAsync(ConnectionTabControl tabControl, string fullName, string? tabName)
    {
        var parts = fullName.Split('.');
        var databaseObject = new Models.DatabaseObject
        {
            SchemaName = parts.Length == 2 ? parts[0] : "UNKNOWN",
            Name = parts.Length == 2 ? parts[1] : fullName,
            FullName = fullName,
            Type = Models.ObjectType.Indexes,
            Icon = "📇"
        };
        
        var dialog = new Dialogs.ObjectDetailsDialog(tabControl.ConnectionManager, databaseObject, tabControl.Connection);
        if (!string.IsNullOrEmpty(tabName))
        {
            dialog.Loaded += (s, e) => dialog.ActivateTab(tabName);
        }
        return dialog;
    }
    
    private async Task<Window> OpenTriggerDialogAsync(ConnectionTabControl tabControl, string fullName, string? tabName)
    {
        var parts = fullName.Split('.');
        var databaseObject = new Models.DatabaseObject
        {
            SchemaName = parts.Length == 2 ? parts[0] : "UNKNOWN",
            Name = parts.Length == 2 ? parts[1] : fullName,
            FullName = fullName,
            Type = Models.ObjectType.Triggers,
            Icon = "⚡"
        };
        
        var dialog = new Dialogs.ObjectDetailsDialog(tabControl.ConnectionManager, databaseObject, tabControl.Connection);
        if (!string.IsNullOrEmpty(tabName))
        {
            dialog.Loaded += (s, e) => dialog.ActivateTab(tabName);
        }
        return dialog;
    }
    
    private async Task<Window> OpenSequenceDialogAsync(ConnectionTabControl tabControl, string fullName, string? tabName)
    {
        var parts = fullName.Split('.');
        var databaseObject = new Models.DatabaseObject
        {
            SchemaName = parts.Length == 2 ? parts[0] : "UNKNOWN",
            Name = parts.Length == 2 ? parts[1] : fullName,
            FullName = fullName,
            Type = Models.ObjectType.Sequences,
            Icon = "🔢"
        };
        
        var dialog = new Dialogs.ObjectDetailsDialog(tabControl.ConnectionManager, databaseObject, tabControl.Connection);
        if (!string.IsNullOrEmpty(tabName))
        {
            dialog.Loaded += (s, e) => dialog.ActivateTab(tabName);
        }
        return dialog;
    }
    
    private async Task<Window> OpenSynonymDialogAsync(ConnectionTabControl tabControl, string fullName, string? tabName)
    {
        var parts = fullName.Split('.');
        var databaseObject = new Models.DatabaseObject
        {
            SchemaName = parts.Length == 2 ? parts[0] : "UNKNOWN",
            Name = parts.Length == 2 ? parts[1] : fullName,
            FullName = fullName,
            Type = Models.ObjectType.Synonyms,
            Icon = "🔗"
        };
        
        var dialog = new Dialogs.ObjectDetailsDialog(tabControl.ConnectionManager, databaseObject, tabControl.Connection);
        if (!string.IsNullOrEmpty(tabName))
        {
            dialog.Loaded += (s, e) => dialog.ActivateTab(tabName);
        }
        return dialog;
    }
    
    private async Task<Window> OpenTypeDialogAsync(ConnectionTabControl tabControl, string fullName, string? tabName)
    {
        var parts = fullName.Split('.');
        var databaseObject = new Models.DatabaseObject
        {
            SchemaName = parts.Length == 2 ? parts[0] : "UNKNOWN",
            Name = parts.Length == 2 ? parts[1] : fullName,
            FullName = fullName,
            Type = Models.ObjectType.Types,
            Icon = "📦"
        };
        
        var dialog = new Dialogs.ObjectDetailsDialog(tabControl.ConnectionManager, databaseObject, tabControl.Connection);
        if (!string.IsNullOrEmpty(tabName))
        {
            dialog.Loaded += (s, e) => dialog.ActivateTab(tabName);
        }
        return dialog;
    }
    
    private async Task<Window> OpenPackageDialogAsync(ConnectionTabControl tabControl, string fullName, string? tabName)
    {
        var parts = fullName.Split('.');
        var packageInfo = new Models.PackageInfo
        {
            PackageSchema = parts.Length == 2 ? parts[0] : "NULLID",
            PackageName = parts.Length == 2 ? parts[1] : fullName
        };
        
        var dialog = new Dialogs.PackageDetailsDialog(tabControl.ConnectionManager, packageInfo);
        if (!string.IsNullOrEmpty(tabName))
        {
            dialog.Loaded += (s, e) => dialog.ActivateTab(tabName);
        }
        return dialog;
    }
    
    private async Task<Window> OpenMermaidDesignerAsync(ConnectionTabControl tabControl, string schemaName, string? tabName)
    {
        Logger.Info("Opening Mermaid Designer for schema: {Schema}", schemaName);
        var dialog = new Dialogs.MermaidDesignerWindow(tabControl.ConnectionManager, schemaName);
        return dialog;
    }
    
    private async Task<Window> OpenUserDialogAsync(ConnectionTabControl tabControl, string principalName, string? type, string? tabName)
    {
        var principalType = type?.ToLowerInvariant() switch
        {
            "role" => Models.SecurityPrincipalType.Role,
            "group" => Models.SecurityPrincipalType.Group,
            _ => Models.SecurityPrincipalType.User
        };
        
        var principal = new Models.SecurityPrincipal
        {
            Name = principalName,
            Type = principalType,
            Icon = principalType switch
            {
                Models.SecurityPrincipalType.User => "👤",
                Models.SecurityPrincipalType.Role => "🎭",
                Models.SecurityPrincipalType.Group => "👥",
                _ => "❓"
            }
        };
        
        var dialog = new Dialogs.UserDetailsDialog(tabControl.ConnectionManager, principal);
        if (!string.IsNullOrEmpty(tabName))
        {
            dialog.Loaded += (s, e) => dialog.ActivateTab(tabName);
        }
        return dialog;
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