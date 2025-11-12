using Microsoft.Win32;
using ModernWpf;
using NLog;
using System.IO;
using System.Windows;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Service for managing application themes (Dark, Light, System)
/// </summary>
public class ThemeService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly ConfigurationService _configService;
    private AppTheme _currentTheme;

    public enum AppTheme
    {
        Dark,
        Light,
        System
    }

    public AppTheme CurrentTheme => _currentTheme;

    public ThemeService(ConfigurationService configService)
    {
        _configService = configService;
        Logger.Debug("ThemeService initializing");

        // Load saved theme preference
        var savedTheme = _configService.Settings.Editor.DefaultTheme;
        _currentTheme = Enum.TryParse<AppTheme>(savedTheme, out var theme) ? theme : AppTheme.System;

        Logger.Info($"Theme initialized: {_currentTheme}");
    }

    /// <summary>
    /// Initialize theme on application startup
    /// </summary>
    public void InitializeTheme()
    {
        Logger.Debug("Initializing application theme");
        ApplyTheme(_currentTheme);
    }

    /// <summary>
    /// Set theme (Dark, Light, or System)
    /// </summary>
    public void SetTheme(AppTheme theme)
    {
        Logger.Info($"Setting theme to: {theme}");
        _currentTheme = theme;
        ApplyTheme(theme);
    }

    /// <summary>
    /// Toggle between Dark, Light, and System themes
    /// </summary>
    public void ToggleTheme()
    {
        var nextTheme = _currentTheme switch
        {
            AppTheme.Dark => AppTheme.Light,
            AppTheme.Light => AppTheme.System,
            AppTheme.System => AppTheme.Dark,
            _ => AppTheme.System
        };

        Logger.Info($"Toggling theme from {_currentTheme} to {nextTheme}");
        SetTheme(nextTheme);
    }

    /// <summary>
    /// Apply the specified theme
    /// </summary>
    private void ApplyTheme(AppTheme theme)
    {
        ApplicationTheme? actualTheme = theme switch
        {
            AppTheme.Dark => ApplicationTheme.Dark,
            AppTheme.Light => ApplicationTheme.Light,
            AppTheme.System => GetSystemTheme(),
            _ => null
        };

        if (actualTheme.HasValue)
        {
            ThemeManager.Current.ApplicationTheme = actualTheme.Value;
            Logger.Debug($"Applied theme: {actualTheme.Value} (Requested: {theme})");
        }
        else
        {
            // Fallback to system default
            ThemeManager.Current.ApplicationTheme = null;
            Logger.Debug("Applied system default theme");
        }
    }

    /// <summary>
    /// Get current Windows system theme
    /// </summary>
    private ApplicationTheme GetSystemTheme()
    {
        Logger.Debug("Detecting Windows system theme");

        try
        {
            // Read Windows registry to detect system theme
            using var key = Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");

            if (key != null)
            {
                var appsUseLightTheme = key.GetValue("AppsUseLightTheme");
                if (appsUseLightTheme is int value)
                {
                    var isDark = value == 0;
                    Logger.Debug($"System theme detected: {(isDark ? "Dark" : "Light")} (Registry value: {value})");
                    return isDark ? ApplicationTheme.Dark : ApplicationTheme.Light;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Warn(ex, "Failed to detect system theme, defaulting to Dark");
        }

        // Default to Dark if detection fails
        Logger.Debug("Using default Dark theme");
        return ApplicationTheme.Dark;
    }

    /// <summary>
    /// Get theme name for display
    /// </summary>
    public string GetThemeName()
    {
        return _currentTheme switch
        {
            AppTheme.Dark => "Dark Mode",
            AppTheme.Light => "Light Mode",
            AppTheme.System => "System Theme",
            _ => "Unknown"
        };
    }

    /// <summary>
    /// Check if current effective theme is dark
    /// </summary>
    public bool IsEffectiveDarkTheme()
    {
        if (_currentTheme == AppTheme.Dark)
            return true;
        if (_currentTheme == AppTheme.Light)
            return false;

        // For System theme, check actual applied theme
        return GetSystemTheme() == ApplicationTheme.Dark;
    }

    /// <summary>
    /// Save current theme preference to configuration
    /// </summary>
    public void SaveThemePreference()
    {
        Logger.Info($"Saving theme preference: {_currentTheme}");

        try
        {
            // Update configuration
            _configService.Settings.Editor.DefaultTheme = _currentTheme.ToString();

            // Save to appsettings.json
            var settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            var json = System.Text.Json.JsonSerializer.Serialize(_configService.Settings, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(settingsPath, json);

            Logger.Debug($"Theme preference saved to {settingsPath}");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to save theme preference");
        }
    }
}

