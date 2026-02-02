using NLog;
using System;
using System.ComponentModel;
using System.Windows;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Centralized font size management service for the application.
/// BaseFontSize drives all UI control scaling; all windows subscribe and auto-adjust.
/// </summary>
public class FontSizeManager : INotifyPropertyChanged
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private static FontSizeManager? _instance;
    private static readonly object _lock = new object();

    // Base font size - everything derives from this (default 12)
    private double _baseFontSize = 12;

    // Legacy per-control sizes (kept for compatibility, synced from BaseFontSize in IncreaseAll/DecreaseAll)
    private double _editorFontSize = 14;
    private double _objectBrowserFontSize = 11;
    private double _dialogFontSize = 12;
    private double _menuFontSize = 12;
    private double _statusBarFontSize = 11;
    private double _propertyWindowFontSize = 12;

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Singleton instance
    /// </summary>
    public static FontSizeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new FontSizeManager();
                        Logger.Debug("FontSizeManager singleton instance created");
                    }
                }
            }
            return _instance;
        }
    }

    private FontSizeManager()
    {
        LoadFromSettings();
        Logger.Debug("FontSizeManager initialized with BaseFontSize: {0}", _baseFontSize);
    }

    /// <summary>
    /// Ensure application resources are set (call once after PreferencesService is ready).
    /// </summary>
    public void EnsureApplicationResources()
    {
        UpdateApplicationResources();
    }

    /// <summary>
    /// Base font size - everything derives from this. Range 8-24.
    /// </summary>
    public double BaseFontSize
    {
        get => _baseFontSize;
        set
        {
            if (Math.Abs(_baseFontSize - value) > 0.01 && value >= 8 && value <= 24)
            {
                Logger.Info("BaseFontSize changing from {Old} to {New}", _baseFontSize, value);
                _baseFontSize = value;
                RecalculateAllSizes();
                UpdateApplicationResources();
                SyncToPreferences();
                NotifyAllWindows();
                SaveToSettings();
            }
        }
    }

    /// <summary>
    /// Grid font size (same as base).
    /// </summary>
    public double GridFontSize => _baseFontSize;

    /// <summary>
    /// Grid row height in pixels (derived from base).
    /// </summary>
    public double GridRowHeight => _baseFontSize * 2 + 1;

    /// <summary>
    /// Height for TextBox, ComboBox (derived from base).
    /// </summary>
    public double ControlHeight => _baseFontSize * 2 + 4;

    /// <summary>
    /// Button min-height (derived from base).
    /// </summary>
    public double ButtonHeight => _baseFontSize * 2 + 6;

    /// <summary>
    /// TreeView item height (derived from base).
    /// </summary>
    public double TreeViewItemHeight => _baseFontSize * 1.8;

    /// <summary>
    /// ListBox item height (derived from base).
    /// </summary>
    public double ListBoxItemHeight => _baseFontSize * 2;

    /// <summary>
    /// Menu item height (derived from base).
    /// </summary>
    public double MenuItemHeight => _baseFontSize * 2;

    /// <summary>
    /// SQL editor font size (Consolas, monospace)
    /// </summary>
    public double EditorFontSize
    {
        get => _editorFontSize;
        set
        {
            if (Math.Abs(_editorFontSize - value) > 0.01 && value >= 8 && value <= 32)
            {
                _editorFontSize = value;
                OnPropertyChanged(nameof(EditorFontSize));
                SaveToSettings();
            }
        }
    }

    /// <summary>
    /// Object browser tree view font size
    /// </summary>
    public double ObjectBrowserFontSize
    {
        get => _objectBrowserFontSize;
        set
        {
            if (Math.Abs(_objectBrowserFontSize - value) > 0.01 && value >= 8 && value <= 24)
            {
                _objectBrowserFontSize = value;
                OnPropertyChanged(nameof(ObjectBrowserFontSize));
                SaveToSettings();
            }
        }
    }

    /// <summary>
    /// Dialog window font size
    /// </summary>
    public double DialogFontSize
    {
        get => _dialogFontSize;
        set
        {
            if (Math.Abs(_dialogFontSize - value) > 0.01 && value >= 8 && value <= 24)
            {
                _dialogFontSize = value;
                OnPropertyChanged(nameof(DialogFontSize));
                SaveToSettings();
            }
        }
    }

    /// <summary>
    /// Menu font size
    /// </summary>
    public double MenuFontSize
    {
        get => _menuFontSize;
        set
        {
            if (Math.Abs(_menuFontSize - value) > 0.01 && value >= 8 && value <= 20)
            {
                _menuFontSize = value;
                OnPropertyChanged(nameof(MenuFontSize));
                SaveToSettings();
            }
        }
    }

    /// <summary>
    /// Status bar font size
    /// </summary>
    public double StatusBarFontSize
    {
        get => _statusBarFontSize;
        set
        {
            if (Math.Abs(_statusBarFontSize - value) > 0.01 && value >= 8 && value <= 20)
            {
                _statusBarFontSize = value;
                OnPropertyChanged(nameof(StatusBarFontSize));
                SaveToSettings();
            }
        }
    }

    /// <summary>
    /// Property window font size
    /// </summary>
    public double PropertyWindowFontSize
    {
        get => _propertyWindowFontSize;
        set
        {
            if (Math.Abs(_propertyWindowFontSize - value) > 0.01 && value >= 8 && value <= 24)
            {
                _propertyWindowFontSize = value;
                OnPropertyChanged(nameof(PropertyWindowFontSize));
                SaveToSettings();
            }
        }
    }

    private void RecalculateAllSizes()
    {
        OnPropertyChanged(nameof(BaseFontSize));
        OnPropertyChanged(nameof(GridFontSize));
        OnPropertyChanged(nameof(GridRowHeight));
        OnPropertyChanged(nameof(ControlHeight));
        OnPropertyChanged(nameof(ButtonHeight));
        OnPropertyChanged(nameof(TreeViewItemHeight));
        OnPropertyChanged(nameof(ListBoxItemHeight));
        OnPropertyChanged(nameof(MenuItemHeight));
        Logger.Debug("All derived sizes recalculated for BaseFontSize: {0}", _baseFontSize);
    }

    private void UpdateApplicationResources()
    {
        try
        {
            var res = Application.Current.Resources;
            res["BaseFontSize"] = BaseFontSize;
            res["GridFontSize"] = GridFontSize;
            res["GridRowHeight"] = GridRowHeight;
            res["ControlHeight"] = ControlHeight;
            res["ButtonHeight"] = ButtonHeight;
            res["TreeViewItemHeight"] = TreeViewItemHeight;
            res["ListBoxItemHeight"] = ListBoxItemHeight;
            res["MenuItemHeight"] = MenuItemHeight;
            Logger.Info("Application resources updated - BaseFontSize: {0}", BaseFontSize);
        }
        catch (Exception ex)
        {
            Logger.Warn(ex, "Failed to update application resources");
        }
    }

    /// <summary>
    /// Sync current sizes to PreferencesService so existing code using preferences still works.
    /// </summary>
    private void SyncToPreferences()
    {
        try
        {
            var prefs = App.PreferencesService?.Preferences;
            if (prefs == null) return;

            prefs.UIFontSize = (int)Math.Round(_baseFontSize);
            prefs.GridFontSize = (int)Math.Round(_baseFontSize);
            prefs.GridCellHeight = (int)Math.Round(GridRowHeight);
            prefs.TreeViewItemSpacing = (int)Math.Max(0, Math.Round(_baseFontSize * 0.3));
            App.PreferencesService?.SavePreferences();
            Logger.Debug("Synced FontSizeManager to Preferences");
        }
        catch (Exception ex)
        {
            Logger.Warn(ex, "Failed to sync FontSizeManager to preferences");
        }
    }

    /// <summary>
    /// Notify all open windows to refresh control styles and optionally resize.
    /// </summary>
    private void NotifyAllWindows()
    {
        try
        {
            var prefs = App.PreferencesService?.Preferences;
            if (prefs == null) return;

            foreach (Window window in Application.Current.Windows)
            {
                if (window == null) continue;

                try
                {
                    UIStyleService.ApplyAllControlStyles(window, _baseFontSize);

                    if (window.WindowState != WindowState.Maximized)
                    {
                        var originalSizeToContent = window.SizeToContent;
                        window.SizeToContent = SizeToContent.WidthAndHeight;
                        window.UpdateLayout();
                        window.SizeToContent = originalSizeToContent;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warn(ex, "Error applying styles to window: {0}", window.GetType().Name);
                }
            }

            GridStyleHelper.RefreshAllGrids(prefs);
            Logger.Debug("Notified all windows - applied control styles");
        }
        catch (Exception ex)
        {
            Logger.Warn(ex, "Error in NotifyAllWindows");
        }
    }

    /// <summary>
    /// Reset all font sizes to defaults
    /// </summary>
    public void ResetToDefaults()
    {
        Logger.Info("Resetting all font sizes to defaults");
        BaseFontSize = 12;
        EditorFontSize = 14;
        ObjectBrowserFontSize = 11;
        DialogFontSize = 12;
        MenuFontSize = 12;
        StatusBarFontSize = 11;
        PropertyWindowFontSize = 12;
    }

    /// <summary>
    /// Increase all font sizes (Ctrl+Plus). BaseFontSize drives global UI scale.
    /// </summary>
    public void IncreaseAll()
    {
        Logger.Info("Increasing all font sizes (BaseFontSize +1)");
        BaseFontSize = Math.Min(24, _baseFontSize + 1);

        EditorFontSize = Math.Min(32, EditorFontSize + 1);
        ObjectBrowserFontSize = Math.Min(24, ObjectBrowserFontSize + 1);
        DialogFontSize = Math.Min(24, DialogFontSize + 1);
        MenuFontSize = Math.Min(20, MenuFontSize + 1);
        StatusBarFontSize = Math.Min(20, StatusBarFontSize + 1);
        PropertyWindowFontSize = Math.Min(24, PropertyWindowFontSize + 1);
    }

    /// <summary>
    /// Decrease all font sizes (Ctrl+Minus).
    /// </summary>
    public void DecreaseAll()
    {
        Logger.Info("Decreasing all font sizes (BaseFontSize -1)");
        BaseFontSize = Math.Max(8, _baseFontSize - 1);

        EditorFontSize = Math.Max(8, EditorFontSize - 1);
        ObjectBrowserFontSize = Math.Max(8, ObjectBrowserFontSize - 1);
        DialogFontSize = Math.Max(8, DialogFontSize - 1);
        MenuFontSize = Math.Max(8, MenuFontSize - 1);
        StatusBarFontSize = Math.Max(8, StatusBarFontSize - 1);
        PropertyWindowFontSize = Math.Max(8, PropertyWindowFontSize - 1);
    }

    private void LoadFromSettings()
    {
        try
        {
            if (App.PreferencesService?.Preferences == null) return;
            var prefs = App.PreferencesService.Preferences;
            _baseFontSize = prefs.UIFontSize;
            _editorFontSize = prefs.FontSize;
            Logger.Debug("Loaded font sizes from preferences - BaseFontSize: {0}", _baseFontSize);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load font sizes from settings");
        }
    }

    private void SaveToSettings()
    {
        try
        {
            SyncToPreferences();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to save font sizes to settings");
        }
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        Logger.Debug("Font size property changed: {Property}", propertyName);
    }
}
