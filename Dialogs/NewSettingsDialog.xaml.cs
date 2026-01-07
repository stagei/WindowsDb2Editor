using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using NLog;
using WindowsDb2Editor.Models;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.Dialogs;

public partial class NewSettingsDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly PreferencesService _preferencesService;
    private readonly Dictionary<string, FrameworkElement> _contentPanels = new();
    private UserPreferences _workingPreferences = null!;
    private bool _isDirty = false;

    public NewSettingsDialog(PreferencesService preferencesService)
    {
        InitializeComponent();
        _preferencesService = preferencesService;
        
        // Create working copy of preferences
        ClonePreferences();
        
        // Build all content panels
        BuildContentPanels();
        
        // Select first item
        if (SettingsTreeView.Items.Count > 0 && SettingsTreeView.Items[0] is TreeViewItem firstItem)
        {
            firstItem.IsSelected = true;
        }
        
        Logger.Debug("NewSettingsDialog initialized");
    }

    private void ClonePreferences()
    {
        var json = System.Text.Json.JsonSerializer.Serialize(_preferencesService.Preferences);
        _workingPreferences = System.Text.Json.JsonSerializer.Deserialize<UserPreferences>(json) ?? new UserPreferences();
    }

    #region Content Panel Builders

    private void BuildContentPanels()
    {
        _contentPanels["Appearance"] = BuildAppearancePanel();
        _contentPanels["Theme"] = BuildThemePanel();
        _contentPanels["ColorsAndFonts"] = BuildColorsAndFontsPanel();
        _contentPanels["ObjectBrowser"] = BuildObjectBrowserPanel();
        _contentPanels["DataGrids"] = BuildDataGridsPanel();
        _contentPanels["Editor"] = BuildEditorPanel();
        _contentPanels["EditorGeneral"] = BuildEditorGeneralPanel();
        _contentPanels["EditorFormatting"] = BuildEditorFormattingPanel();
        _contentPanels["SyntaxHighlighting"] = BuildSyntaxHighlightingPanel();
        _contentPanels["Database"] = BuildDatabasePanel();
        _contentPanels["DbConnection"] = BuildDbConnectionPanel();
        _contentPanels["QueryExecution"] = BuildQueryExecutionPanel();
        _contentPanels["ResultSets"] = BuildResultSetsPanel();
        _contentPanels["Keys"] = BuildKeysPanel();
        _contentPanels["Logging"] = BuildLoggingPanel();
        _contentPanels["AI"] = BuildAIPanel();
        _contentPanels["AIProviders"] = BuildAIProvidersPanel();
        _contentPanels["AIModels"] = BuildAIModelsPanel();
    }

    private StackPanel BuildAppearancePanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("Configure the visual appearance of the application including themes, colors, fonts, and UI element styling."));
        panel.Children.Add(CreateSubcategoryLink("Theme", "Choose between Light, Dark, or System theme"));
        panel.Children.Add(CreateSubcategoryLink("Colors and Fonts", "Customize colors and font settings"));
        panel.Children.Add(CreateSubcategoryLink("Object Browser", "Configure the database object browser appearance"));
        panel.Children.Add(CreateSubcategoryLink("Data Grids", "Configure data grid styling and colors"));
        return panel;
    }

    private StackPanel BuildThemePanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("Select the application theme. Changes apply immediately."));
        
        panel.Children.Add(CreateSectionHeader("Application Theme"));
        
        var themeCombo = new ComboBox { Width = 200, Margin = new Thickness(0, 5, 0, 15) };
        themeCombo.Items.Add(new ComboBoxItem { Content = "Dark", Tag = "Dark" });
        themeCombo.Items.Add(new ComboBoxItem { Content = "Light", Tag = "Light" });
        themeCombo.Items.Add(new ComboBoxItem { Content = "System Default", Tag = "System" });
        SetComboBoxValue(themeCombo, _workingPreferences.DefaultTheme);
        themeCombo.SelectionChanged += (s, e) => {
            var newTheme = GetComboBoxValue(themeCombo);
            _workingPreferences.DefaultTheme = newTheme;
            
            // Auto-apply matching color scheme
            bool isDark = newTheme == "Dark" || (newTheme == "System" && IsSystemDarkTheme());
            if (isDark)
            {
                _workingPreferences.EditorBackgroundColor = "#1E1E1E";
                _workingPreferences.EditorForegroundColor = "#D4D4D4";
                _workingPreferences.EditorLineNumberColor = "#858585";
                _workingPreferences.EditorCurrentLineColor = "#2D2D30";
                _workingPreferences.GridBackgroundColor = "#2D2D2D";
                _workingPreferences.GridForegroundColor = "#E0E0E0";
            }
            else
            {
                _workingPreferences.EditorBackgroundColor = "#FFFFFF";
                _workingPreferences.EditorForegroundColor = "#000000";
                _workingPreferences.EditorLineNumberColor = "#2B91AF";
                _workingPreferences.EditorCurrentLineColor = "#E8F2FF";
                _workingPreferences.GridBackgroundColor = "#FFFFFF";
                _workingPreferences.GridForegroundColor = "#000000";
            }
            
            _isDirty = true;
            
            // Rebuild colors panel to reflect changes
            _contentPanels["ColorsAndFonts"] = BuildColorsAndFontsPanel();
        };
        panel.Children.Add(themeCombo);
        
        panel.Children.Add(CreateTip("Tip: Use Ctrl+D to quickly toggle between Dark and Light mode. Theme changes also update editor and grid colors automatically."));
        
        return panel;
    }

    private StackPanel BuildColorsAndFontsPanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("Customize fonts and colors used throughout the application."));
        
        panel.Children.Add(CreateSectionHeader("Global UI Font Size"));
        panel.Children.Add(CreateLabel("Applies to buttons, menus, labels, and all UI elements:"));
        var uiFontSizeSlider = CreateSliderWithValue(8, 24, _workingPreferences.UIFontSize, v => {
            _workingPreferences.UIFontSize = (int)v;
            _workingPreferences.GridFontSize = (int)v; // Keep grid in sync
            _isDirty = true;
        });
        panel.Children.Add(uiFontSizeSlider);
        
        panel.Children.Add(CreateTip("Tip: Use Ctrl++ and Ctrl+- to quickly adjust all font sizes"));
        
        panel.Children.Add(CreateSectionHeader("Editor Font"));
        
        var fontCombo = new ComboBox { Width = 200, Margin = new Thickness(0, 5, 0, 10) };
        foreach (var font in new[] { "Consolas", "Cascadia Code", "Fira Code", "JetBrains Mono", "Source Code Pro", "Courier New" })
        {
            fontCombo.Items.Add(new ComboBoxItem { Content = font, Tag = font });
        }
        SetComboBoxValue(fontCombo, _workingPreferences.FontFamily);
        fontCombo.SelectionChanged += (s, e) => {
            _workingPreferences.FontFamily = GetComboBoxValue(fontCombo);
            _isDirty = true;
        };
        panel.Children.Add(fontCombo);
        
        panel.Children.Add(CreateLabel("Editor Font Size:"));
        var fontSizeSlider = CreateSliderWithValue(8, 36, _workingPreferences.FontSize, v => {
            _workingPreferences.FontSize = (int)v;
            _isDirty = true;
        });
        panel.Children.Add(fontSizeSlider);
        
        panel.Children.Add(CreateSectionHeader("Editor Theme Colors"));
        
        panel.Children.Add(CreateLabel("Background Color:"));
        panel.Children.Add(CreateColorPicker(_workingPreferences.EditorBackgroundColor, c => {
            _workingPreferences.EditorBackgroundColor = c;
            _isDirty = true;
        }));
        
        panel.Children.Add(CreateLabel("Text Color:"));
        panel.Children.Add(CreateColorPicker(_workingPreferences.EditorForegroundColor, c => {
            _workingPreferences.EditorForegroundColor = c;
            _isDirty = true;
        }));
        
        panel.Children.Add(CreateLabel("Line Number Color:"));
        panel.Children.Add(CreateColorPicker(_workingPreferences.EditorLineNumberColor, c => {
            _workingPreferences.EditorLineNumberColor = c;
            _isDirty = true;
        }));
        
        panel.Children.Add(CreateLabel("Current Line Highlight:"));
        panel.Children.Add(CreateColorPicker(_workingPreferences.EditorCurrentLineColor, c => {
            _workingPreferences.EditorCurrentLineColor = c;
            _isDirty = true;
        }));
        
        // Quick preset buttons
        panel.Children.Add(CreateSectionHeader("Editor Color Presets"));
        var presetsPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 5, 0, 10) };
        
        var darkPresetBtn = new Button { Content = "🌙 Dark Mode", Margin = new Thickness(0, 0, 10, 0), Padding = new Thickness(15, 5, 15, 5) };
        darkPresetBtn.Click += (s, e) => {
            _workingPreferences.EditorBackgroundColor = "#1E1E1E";
            _workingPreferences.EditorForegroundColor = "#D4D4D4";
            _workingPreferences.EditorLineNumberColor = "#858585";
            _workingPreferences.EditorCurrentLineColor = "#2D2D30";
            _isDirty = true;
            // Rebuild panel to reflect changes
            _contentPanels["ColorsAndFonts"] = BuildColorsAndFontsPanel();
            if (ContentPanel.Content is StackPanel) ContentPanel.Content = _contentPanels["ColorsAndFonts"];
        };
        presetsPanel.Children.Add(darkPresetBtn);
        
        var lightPresetBtn = new Button { Content = "☀️ Light Mode", Margin = new Thickness(0, 0, 10, 0), Padding = new Thickness(15, 5, 15, 5) };
        lightPresetBtn.Click += (s, e) => {
            _workingPreferences.EditorBackgroundColor = "#FFFFFF";
            _workingPreferences.EditorForegroundColor = "#000000";
            _workingPreferences.EditorLineNumberColor = "#2B91AF";
            _workingPreferences.EditorCurrentLineColor = "#E8F2FF";
            _isDirty = true;
            // Rebuild panel to reflect changes
            _contentPanels["ColorsAndFonts"] = BuildColorsAndFontsPanel();
            if (ContentPanel.Content is StackPanel) ContentPanel.Content = _contentPanels["ColorsAndFonts"];
        };
        presetsPanel.Children.Add(lightPresetBtn);
        
        panel.Children.Add(presetsPanel);
        
        return panel;
    }

    private StackPanel BuildObjectBrowserPanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("Configure the appearance of the database object browser tree view."));
        
        panel.Children.Add(CreateSectionHeader("Font Settings"));
        
        panel.Children.Add(CreateLabel("Font Family:"));
        var fontCombo = new ComboBox { Width = 200, Margin = new Thickness(0, 5, 0, 10) };
        foreach (var font in new[] { "Segoe UI", "Consolas", "Cascadia Code", "Arial", "Tahoma" })
        {
            fontCombo.Items.Add(new ComboBoxItem { Content = font, Tag = font });
        }
        SetComboBoxValue(fontCombo, _workingPreferences.GridFontFamily);
        fontCombo.SelectionChanged += (s, e) => {
            _workingPreferences.GridFontFamily = GetComboBoxValue(fontCombo);
            _isDirty = true;
        };
        panel.Children.Add(fontCombo);
        
        panel.Children.Add(CreateLabel("Font Size:"));
        var fontSizeSlider = CreateSliderWithValue(8, 18, _workingPreferences.GridFontSize, v => {
            _workingPreferences.GridFontSize = (int)v;
            _isDirty = true;
        });
        panel.Children.Add(fontSizeSlider);
        
        panel.Children.Add(CreateSectionHeader("Spacing"));
        
        panel.Children.Add(CreateLabel("Item Vertical Spacing (pixels):"));
        var spacingSlider = CreateSliderWithValue(0, 10, _workingPreferences.TreeViewItemSpacing, v => {
            _workingPreferences.TreeViewItemSpacing = (int)v;
            _isDirty = true;
        });
        panel.Children.Add(spacingSlider);
        
        panel.Children.Add(CreateTip("Tip: Use Ctrl+Shift++ and Ctrl+Shift+- to quickly adjust spacing"));
        
        return panel;
    }

    private StackPanel BuildDataGridsPanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("Configure the appearance of data grids used for query results and data display."));
        
        panel.Children.Add(CreateSectionHeader("Colors"));
        
        panel.Children.Add(CreateLabel("Background Color:"));
        var bgColor = CreateColorPicker(_workingPreferences.GridBackgroundColor, c => {
            _workingPreferences.GridBackgroundColor = c;
            _isDirty = true;
        });
        panel.Children.Add(bgColor);
        
        panel.Children.Add(CreateLabel("Foreground Color:"));
        var fgColor = CreateColorPicker(_workingPreferences.GridForegroundColor, c => {
            _workingPreferences.GridForegroundColor = c;
            _isDirty = true;
        });
        panel.Children.Add(fgColor);
        
        panel.Children.Add(CreateLabel("Selected Row Background:"));
        var selBgColor = CreateColorPicker(_workingPreferences.GridSelectedBackgroundColor, c => {
            _workingPreferences.GridSelectedBackgroundColor = c;
            _isDirty = true;
        });
        panel.Children.Add(selBgColor);
        
        panel.Children.Add(CreateLabel("Selected Row Foreground:"));
        var selFgColor = CreateColorPicker(_workingPreferences.GridSelectedForegroundColor, c => {
            _workingPreferences.GridSelectedForegroundColor = c;
            _isDirty = true;
        });
        panel.Children.Add(selFgColor);
        
        panel.Children.Add(CreateSectionHeader("Layout"));
        
        panel.Children.Add(CreateLabel("Row Height (pixels):"));
        var heightSlider = CreateSliderWithValue(20, 50, _workingPreferences.GridCellHeight, v => {
            _workingPreferences.GridCellHeight = (int)v;
            _isDirty = true;
        });
        panel.Children.Add(heightSlider);
        
        return panel;
    }

    private StackPanel BuildEditorPanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("Configure SQL editor behavior and appearance."));
        panel.Children.Add(CreateSubcategoryLink("General", "General editor settings"));
        panel.Children.Add(CreateSubcategoryLink("Formatting", "SQL formatting options"));
        panel.Children.Add(CreateSubcategoryLink("Syntax Highlighting", "Configure syntax colors"));
        return panel;
    }

    private StackPanel BuildEditorGeneralPanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("General SQL editor settings."));
        
        panel.Children.Add(CreateSectionHeader("Tab Settings"));
        
        panel.Children.Add(CreateLabel("Tab Size (spaces):"));
        var tabSlider = CreateSliderWithValue(2, 8, _workingPreferences.TabSize, v => {
            _workingPreferences.TabSize = (int)v;
            _isDirty = true;
        });
        panel.Children.Add(tabSlider);
        
        return panel;
    }

    private StackPanel BuildEditorFormattingPanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("SQL formatting options."));
        
        var autoFormat = new CheckBox { 
            Content = "Auto-format SQL on execute", 
            IsChecked = _workingPreferences.AutoFormatOnExecute,
            Margin = new Thickness(0, 10, 0, 5)
        };
        autoFormat.Checked += (s, e) => { _workingPreferences.AutoFormatOnExecute = true; _isDirty = true; };
        autoFormat.Unchecked += (s, e) => { _workingPreferences.AutoFormatOnExecute = false; _isDirty = true; };
        panel.Children.Add(autoFormat);
        
        return panel;
    }

    private StackPanel BuildSyntaxHighlightingPanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("Syntax highlighting colors for SQL editor. (Coming soon)"));
        panel.Children.Add(CreateTip("Syntax highlighting customization will be available in a future update."));
        return panel;
    }

    private StackPanel BuildDatabasePanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("Configure database connection and query execution settings."));
        panel.Children.Add(CreateSubcategoryLink("Connection", "Connection timeout and options"));
        panel.Children.Add(CreateSubcategoryLink("Query Execution", "Query execution settings"));
        panel.Children.Add(CreateSubcategoryLink("Result Sets", "Result set display options"));
        return panel;
    }

    private StackPanel BuildDbConnectionPanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("Database connection settings."));
        
        panel.Children.Add(CreateSectionHeader("Connection Options"));
        
        var autoRefresh = new CheckBox { 
            Content = "Auto-refresh objects on connect", 
            IsChecked = _workingPreferences.AutoRefreshObjectsOnConnect,
            Margin = new Thickness(0, 10, 0, 5)
        };
        autoRefresh.Checked += (s, e) => { _workingPreferences.AutoRefreshObjectsOnConnect = true; _isDirty = true; };
        autoRefresh.Unchecked += (s, e) => { _workingPreferences.AutoRefreshObjectsOnConnect = false; _isDirty = true; };
        panel.Children.Add(autoRefresh);
        
        return panel;
    }

    private StackPanel BuildQueryExecutionPanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("Query execution settings and timeouts."));
        
        panel.Children.Add(CreateSectionHeader("Timeouts"));
        
        panel.Children.Add(CreateLabel("Command Timeout (seconds, 0 = no timeout):"));
        var timeoutBox = new TextBox { 
            Text = _workingPreferences.CommandTimeout.ToString(), 
            Width = 100, 
            Margin = new Thickness(0, 5, 0, 15),
            HorizontalAlignment = HorizontalAlignment.Left
        };
        timeoutBox.TextChanged += (s, e) => {
            if (int.TryParse(timeoutBox.Text, out int val) && val >= 0)
            {
                _workingPreferences.CommandTimeout = val;
                _isDirty = true;
            }
        };
        panel.Children.Add(timeoutBox);
        
        panel.Children.Add(CreateSectionHeader("Error Handling"));
        
        var handleDecimal = new CheckBox { 
            Content = "Handle DB2Decimal errors gracefully", 
            IsChecked = _workingPreferences.HandleDecimalErrorsGracefully,
            Margin = new Thickness(0, 10, 0, 5)
        };
        handleDecimal.Checked += (s, e) => { _workingPreferences.HandleDecimalErrorsGracefully = true; _isDirty = true; };
        handleDecimal.Unchecked += (s, e) => { _workingPreferences.HandleDecimalErrorsGracefully = false; _isDirty = true; };
        panel.Children.Add(handleDecimal);
        
        return panel;
    }

    private StackPanel BuildResultSetsPanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("Configure how result sets are displayed."));
        
        panel.Children.Add(CreateSectionHeader("Row Limits"));
        
        panel.Children.Add(CreateLabel("Max Rows Per Query:"));
        var maxRowsBox = new TextBox { 
            Text = _workingPreferences.MaxRowsPerQuery.ToString(), 
            Width = 100, 
            Margin = new Thickness(0, 5, 0, 15),
            HorizontalAlignment = HorizontalAlignment.Left
        };
        maxRowsBox.TextChanged += (s, e) => {
            if (int.TryParse(maxRowsBox.Text, out int val) && val > 0)
            {
                _workingPreferences.MaxRowsPerQuery = val;
                _isDirty = true;
            }
        };
        panel.Children.Add(maxRowsBox);
        
        return panel;
    }

    private StackPanel BuildKeysPanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("Configure keyboard shortcuts for menu items and commands."));
        
        panel.Children.Add(CreateSectionHeader("Quick Actions"));
        panel.Children.Add(CreateHotkeyRow("Increase Font Size", "Ctrl + Plus"));
        panel.Children.Add(CreateHotkeyRow("Decrease Font Size", "Ctrl + Minus"));
        panel.Children.Add(CreateHotkeyRow("Increase TreeView Spacing", "Ctrl + Shift + Plus"));
        panel.Children.Add(CreateHotkeyRow("Decrease TreeView Spacing", "Ctrl + Shift + Minus"));
        panel.Children.Add(CreateHotkeyRow("Toggle Dark Mode", "Ctrl + D"));
        
        panel.Children.Add(CreateSectionHeader("Connection"));
        panel.Children.Add(CreateHotkeyRow("New Connection", "Ctrl + N"));
        panel.Children.Add(CreateHotkeyRow("Close Tab", "Ctrl + W"));
        panel.Children.Add(CreateHotkeyRow("Next Tab", "Ctrl + Tab"));
        panel.Children.Add(CreateHotkeyRow("Previous Tab", "Ctrl + Shift + Tab"));
        
        panel.Children.Add(CreateSectionHeader("Query Execution"));
        panel.Children.Add(CreateHotkeyRow("Execute Query", "F5"));
        panel.Children.Add(CreateHotkeyRow("Execute Selected", "Ctrl + Enter"));
        panel.Children.Add(CreateHotkeyRow("Format SQL", "Ctrl + Shift + F"));
        panel.Children.Add(CreateHotkeyRow("Query History", "Ctrl + H"));
        
        panel.Children.Add(CreateSectionHeader("Edit"));
        panel.Children.Add(CreateHotkeyRow("Find", "Ctrl + F"));
        panel.Children.Add(CreateHotkeyRow("Replace", "Ctrl + R"));
        panel.Children.Add(CreateHotkeyRow("Comment Line", "Ctrl + /"));
        
        panel.Children.Add(CreateTip("Note: Hotkey customization will be available in a future update. These are the current default shortcuts."));
        
        return panel;
    }

    private StackPanel BuildLoggingPanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("Configure application logging level and output."));
        
        panel.Children.Add(CreateSectionHeader("Log Level"));
        
        var logCombo = new ComboBox { Width = 150, Margin = new Thickness(0, 5, 0, 15) };
        logCombo.Items.Add(new ComboBoxItem { Content = "Debug", Tag = "Debug" });
        logCombo.Items.Add(new ComboBoxItem { Content = "Info", Tag = "Info" });
        logCombo.Items.Add(new ComboBoxItem { Content = "Warn", Tag = "Warn" });
        logCombo.Items.Add(new ComboBoxItem { Content = "Error", Tag = "Error" });
        SetComboBoxValue(logCombo, _workingPreferences.LogLevel);
        logCombo.SelectionChanged += (s, e) => {
            _workingPreferences.LogLevel = GetComboBoxValue(logCombo);
            _isDirty = true;
        };
        panel.Children.Add(logCombo);
        
        panel.Children.Add(CreateSectionHeader("Log Location"));
        var logPath = new TextBlock { 
            Text = $"Logs are stored in: {AppDataHelper.GetAppDataFolder()}",
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 5, 0, 0),
            Foreground = (Brush)FindResource("SystemControlForegroundBaseMediumBrush")
        };
        panel.Children.Add(logPath);
        
        return panel;
    }

    private StackPanel BuildAIPanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("Configure AI assistant settings for code analysis and suggestions."));
        panel.Children.Add(CreateSubcategoryLink("Providers", "Configure AI providers (Ollama, OpenAI, Claude)"));
        panel.Children.Add(CreateSubcategoryLink("Models", "Select and configure AI models"));
        return panel;
    }

    private StackPanel BuildAIProvidersPanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("Configure AI service providers."));
        panel.Children.Add(CreateTip("AI provider configuration is available in the main Settings dialog. This will be migrated here in a future update."));
        return panel;
    }

    private StackPanel BuildAIModelsPanel()
    {
        var panel = new StackPanel { Margin = new Thickness(0, 0, 20, 0) };
        panel.Children.Add(CreateDescription("Select and configure AI models."));
        panel.Children.Add(CreateTip("AI model configuration is available in the main Settings dialog. This will be migrated here in a future update."));
        return panel;
    }

    #endregion

    #region UI Helpers

    private TextBlock CreateDescription(string text)
    {
        return new TextBlock {
            Text = text,
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 0, 0, 20),
            Foreground = (Brush)FindResource("SystemControlForegroundBaseMediumBrush")
        };
    }

    private TextBlock CreateSectionHeader(string text)
    {
        return new TextBlock {
            Text = text,
            FontWeight = FontWeights.SemiBold,
            FontSize = 14,
            Margin = new Thickness(0, 15, 0, 10)
        };
    }

    private TextBlock CreateLabel(string text)
    {
        return new TextBlock {
            Text = text,
            Margin = new Thickness(0, 5, 0, 3)
        };
    }

    private TextBlock CreateTip(string text)
    {
        return new TextBlock {
            Text = text,
            FontStyle = FontStyles.Italic,
            FontSize = 11,
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 10, 0, 5),
            Foreground = (Brush)FindResource("SystemControlForegroundBaseMediumBrush")
        };
    }

    private Border CreateSubcategoryLink(string name, string description)
    {
        var border = new Border {
            Margin = new Thickness(0, 5, 0, 5),
            Padding = new Thickness(10),
            Background = (Brush)FindResource("SystemControlBackgroundBaseLowBrush"),
            CornerRadius = new CornerRadius(4),
            Cursor = Cursors.Hand
        };
        
        var stack = new StackPanel();
        stack.Children.Add(new TextBlock { Text = name, FontWeight = FontWeights.SemiBold });
        stack.Children.Add(new TextBlock { 
            Text = description, 
            FontSize = 11,
            Foreground = (Brush)FindResource("SystemControlForegroundBaseMediumBrush")
        });
        border.Child = stack;
        
        border.MouseLeftButtonUp += (s, e) => {
            SelectTreeViewItemByTag(name.Replace(" ", "").Replace("and", "And"));
        };
        
        return border;
    }

    private StackPanel CreateSliderWithValue(int min, int max, int currentValue, Action<double> onChange)
    {
        var panel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 5, 0, 15) };
        
        var slider = new Slider {
            Minimum = min,
            Maximum = max,
            Value = currentValue,
            Width = 200,
            TickFrequency = 1,
            IsSnapToTickEnabled = true,
            VerticalAlignment = VerticalAlignment.Center
        };
        
        var valueText = new TextBlock {
            Text = currentValue.ToString(),
            Width = 30,
            Margin = new Thickness(10, 0, 0, 0),
            VerticalAlignment = VerticalAlignment.Center,
            FontWeight = FontWeights.SemiBold
        };
        
        slider.ValueChanged += (s, e) => {
            valueText.Text = ((int)e.NewValue).ToString();
            onChange?.Invoke(e.NewValue);
        };
        
        panel.Children.Add(slider);
        panel.Children.Add(valueText);
        
        return panel;
    }

    private StackPanel CreateColorPicker(string currentColor, Action<string> onChange)
    {
        var panel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 5, 0, 10) };
        
        var colorBox = new TextBox {
            Text = currentColor,
            Width = 100,
            VerticalAlignment = VerticalAlignment.Center
        };
        
        var colorPreview = new Border {
            Width = 24,
            Height = 24,
            Margin = new Thickness(8, 0, 0, 0),
            BorderBrush = Brushes.Gray,
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(2)
        };
        
        try
        {
            colorPreview.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(currentColor));
        }
        catch
        {
            colorPreview.Background = Brushes.Gray;
        }
        
        var pickButton = new Button {
            Content = "...",
            Width = 30,
            Height = 24,
            Margin = new Thickness(5, 0, 0, 0)
        };
        
        colorBox.TextChanged += (s, e) => {
            try
            {
                var color = (Color)ColorConverter.ConvertFromString(colorBox.Text);
                colorPreview.Background = new SolidColorBrush(color);
                onChange?.Invoke(colorBox.Text);
            }
            catch { }
        };
        
        pickButton.Click += (s, e) => {
            using var dialog = new System.Windows.Forms.ColorDialog();
            try
            {
                var wpfColor = (Color)ColorConverter.ConvertFromString(colorBox.Text);
                dialog.Color = System.Drawing.Color.FromArgb(wpfColor.A, wpfColor.R, wpfColor.G, wpfColor.B);
            }
            catch { }
            
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var hex = $"#{dialog.Color.R:X2}{dialog.Color.G:X2}{dialog.Color.B:X2}";
                colorBox.Text = hex;
            }
        };
        
        panel.Children.Add(colorBox);
        panel.Children.Add(colorPreview);
        panel.Children.Add(pickButton);
        
        return panel;
    }

    private Border CreateHotkeyRow(string command, string hotkey)
    {
        var border = new Border {
            Margin = new Thickness(0, 3, 0, 3),
            Padding = new Thickness(10, 8, 10, 8),
            Background = (Brush)FindResource("SystemControlBackgroundBaseLowBrush"),
            CornerRadius = new CornerRadius(3)
        };
        
        var grid = new Grid();
        grid.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        grid.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition { Width = new GridLength(150) });
        
        var cmdText = new TextBlock { Text = command, VerticalAlignment = VerticalAlignment.Center };
        Grid.SetColumn(cmdText, 0);
        
        var hotkeyBorder = new Border {
            Background = (Brush)FindResource("SystemControlBackgroundChromeMediumBrush"),
            CornerRadius = new CornerRadius(3),
            Padding = new Thickness(8, 4, 8, 4),
            HorizontalAlignment = HorizontalAlignment.Right
        };
        hotkeyBorder.Child = new TextBlock { 
            Text = hotkey, 
            FontFamily = new FontFamily("Consolas"),
            FontSize = 11
        };
        Grid.SetColumn(hotkeyBorder, 1);
        
        grid.Children.Add(cmdText);
        grid.Children.Add(hotkeyBorder);
        border.Child = grid;
        
        return border;
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

    #endregion

    #region Event Handlers

    private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var filter = FilterTextBox.Text.ToLower();
        FilterTreeViewItems(SettingsTreeView.Items, filter);
    }

    private void FilterTreeViewItems(ItemCollection items, string filter)
    {
        foreach (var item in items)
        {
            if (item is TreeViewItem tvi)
            {
                var header = tvi.Header?.ToString()?.ToLower() ?? "";
                var matches = string.IsNullOrEmpty(filter) || header.Contains(filter);
                
                // Check children
                FilterTreeViewItems(tvi.Items, filter);
                var hasVisibleChild = tvi.Items.Cast<TreeViewItem>().Any(c => c.Visibility == Visibility.Visible);
                
                tvi.Visibility = (matches || hasVisibleChild) ? Visibility.Visible : Visibility.Collapsed;
                
                if (!string.IsNullOrEmpty(filter) && (matches || hasVisibleChild))
                {
                    tvi.IsExpanded = true;
                }
            }
        }
    }

    private void SettingsTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (e.NewValue is TreeViewItem tvi && tvi.Tag is string tag)
        {
            ContentTitleText.Text = tvi.Header?.ToString() ?? tag;
            
            if (_contentPanels.TryGetValue(tag, out var panel))
            {
                ContentPanel.Content = panel;
            }
            else
            {
                ContentPanel.Content = new TextBlock { Text = $"Settings for: {tag}" };
            }
        }
    }

    private void SelectTreeViewItemByTag(string tag)
    {
        var item = FindTreeViewItemByTag(SettingsTreeView.Items, tag);
        if (item != null)
        {
            item.IsSelected = true;
            item.BringIntoView();
        }
    }

    private TreeViewItem? FindTreeViewItemByTag(ItemCollection items, string tag)
    {
        foreach (var item in items)
        {
            if (item is TreeViewItem tvi)
            {
                if (tvi.Tag?.ToString() == tag)
                    return tvi;
                
                var found = FindTreeViewItemByTag(tvi.Items, tag);
                if (found != null)
                    return found;
            }
        }
        return null;
    }

    private void NavigateBack_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement navigation history
    }

    private void NavigateForward_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement navigation history
    }

    private void RestoreDefaults_Click(object sender, RoutedEventArgs e)
    {
        var result = MessageBox.Show(
            "Reset all settings to their default values?",
            "Restore Defaults",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);
        
        if (result == MessageBoxResult.Yes)
        {
            _workingPreferences = new UserPreferences();
            _isDirty = true;
            
            // Rebuild panels with new values
            BuildContentPanels();
            
            // Refresh current panel
            if (SettingsTreeView.SelectedItem is TreeViewItem tvi && tvi.Tag is string tag)
            {
                if (_contentPanels.TryGetValue(tag, out var panel))
                {
                    ContentPanel.Content = panel;
                }
            }
            
            MessageBox.Show("Settings restored to defaults.", "Restore Defaults", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private void Apply_Click(object sender, RoutedEventArgs e)
    {
        SavePreferences();
        _isDirty = false;
        MessageBox.Show("Settings applied.", "Apply", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void ApplyAndClose_Click(object sender, RoutedEventArgs e)
    {
        SavePreferences();
        DialogResult = true;
        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        if (_isDirty)
        {
            var result = MessageBox.Show(
                "You have unsaved changes. Discard them?",
                "Unsaved Changes",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            
            if (result != MessageBoxResult.Yes)
                return;
        }
        
        DialogResult = false;
        Close();
    }

    /// <summary>
    /// Check if Windows system theme is dark
    /// </summary>
    private bool IsSystemDarkTheme()
    {
        try
        {
            using var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            if (key != null)
            {
                var value = key.GetValue("AppsUseLightTheme");
                if (value is int intValue)
                {
                    return intValue == 0; // 0 = dark, 1 = light
                }
            }
        }
        catch { }
        return false; // Default to light
    }

    private void SavePreferences()
    {
        // Copy working preferences to actual preferences
        _preferencesService.Preferences.DefaultTheme = _workingPreferences.DefaultTheme;
        _preferencesService.Preferences.FontFamily = _workingPreferences.FontFamily;
        _preferencesService.Preferences.FontSize = _workingPreferences.FontSize;
        _preferencesService.Preferences.TabSize = _workingPreferences.TabSize;
        _preferencesService.Preferences.AutoFormatOnExecute = _workingPreferences.AutoFormatOnExecute;
        _preferencesService.Preferences.MaxRowsPerQuery = _workingPreferences.MaxRowsPerQuery;
        _preferencesService.Preferences.CommandTimeout = _workingPreferences.CommandTimeout;
        _preferencesService.Preferences.HandleDecimalErrorsGracefully = _workingPreferences.HandleDecimalErrorsGracefully;
        _preferencesService.Preferences.AutoRefreshObjectsOnConnect = _workingPreferences.AutoRefreshObjectsOnConnect;
        _preferencesService.Preferences.LogLevel = _workingPreferences.LogLevel;
        _preferencesService.Preferences.GridBackgroundColor = _workingPreferences.GridBackgroundColor;
        _preferencesService.Preferences.GridForegroundColor = _workingPreferences.GridForegroundColor;
        _preferencesService.Preferences.GridSelectedBackgroundColor = _workingPreferences.GridSelectedBackgroundColor;
        _preferencesService.Preferences.GridSelectedForegroundColor = _workingPreferences.GridSelectedForegroundColor;
        _preferencesService.Preferences.GridFontSize = _workingPreferences.GridFontSize;
        _preferencesService.Preferences.GridFontFamily = _workingPreferences.GridFontFamily;
        _preferencesService.Preferences.GridCellHeight = _workingPreferences.GridCellHeight;
        _preferencesService.Preferences.TreeViewItemSpacing = _workingPreferences.TreeViewItemSpacing;
        _preferencesService.Preferences.UIFontSize = _workingPreferences.UIFontSize;
        _preferencesService.Preferences.EditorBackgroundColor = _workingPreferences.EditorBackgroundColor;
        _preferencesService.Preferences.EditorForegroundColor = _workingPreferences.EditorForegroundColor;
        _preferencesService.Preferences.EditorLineNumberColor = _workingPreferences.EditorLineNumberColor;
        _preferencesService.Preferences.EditorCurrentLineColor = _workingPreferences.EditorCurrentLineColor;
        
        _preferencesService.SavePreferences();
        
        // Apply global font size to entire application
        GlobalFontService.ApplyAllPreferences(_preferencesService.Preferences);
        
        Logger.Info("Preferences saved from new settings dialog");
    }

    #endregion
}
