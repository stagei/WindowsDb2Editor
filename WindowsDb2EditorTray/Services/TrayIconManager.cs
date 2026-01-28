using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using NLog;
using WindowsDb2EditorTray.Models;

namespace WindowsDb2EditorTray.Services;

/// <summary>
/// Manages the system tray icon, context menu, and notifications
/// </summary>
public class TrayIconManager : IDisposable
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private NotifyIcon? _notifyIcon;
    private bool _disposed = false;

    public void Initialize()
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(TrayIconManager));

        try
        {
            Logger.Debug("Initializing tray icon manager");

            // Create notify icon
            _notifyIcon = new NotifyIcon
            {
                Icon = GetApplicationIcon(),
                Text = "WindowsDb2Editor",
                Visible = true
            };

            // Build context menu
            BuildContextMenu();

            // Handle double-click to show main window
            _notifyIcon.DoubleClick += (s, e) => ShowMainWindow();

            Logger.Info("Tray icon manager initialized successfully");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to initialize tray icon manager");
            throw;
        }
    }

    private Icon GetApplicationIcon()
    {
        try
        {
            // Try multiple possible locations for the icon
            var possiblePaths = new[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "dEdge.ico"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "Resources", "dEdge.ico"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Resources", "dEdge.ico"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Resources", "dEdge.ico"),
                Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? "", "Resources", "dEdge.ico")
            };

            foreach (var iconPath in possiblePaths)
            {
                var fullPath = Path.GetFullPath(iconPath);
                if (File.Exists(fullPath))
                {
                    Logger.Debug("Found application icon at: {Path}", fullPath);
                    return new Icon(fullPath);
                }
            }

            // Use default system icon as last resort
            Logger.Warn("Application icon not found in any expected location, using default system icon");
            return SystemIcons.Application;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load application icon, using default");
            return SystemIcons.Application;
        }
    }

    private void BuildContextMenu()
    {
        if (_notifyIcon == null)
            return;

        try
        {
            Logger.Debug("Building context menu");

            var menu = new ContextMenuStrip();

            // Connections submenu
            var connectionsMenu = new ToolStripMenuItem("Connections");
            var profiles = LoadConnectionProfiles();

            if (profiles.Count > 0)
            {
                foreach (var profile in profiles)
                {
                    var profileItem = new ToolStripMenuItem(profile.Name)
                    {
                        Tag = profile
                    };
                    profileItem.Click += (s, e) => LaunchConnection(profile);
                    connectionsMenu.DropDownItems.Add(profileItem);
                }

                connectionsMenu.DropDownItems.Add(new ToolStripSeparator());
            }

            // Manage Connections item
            var manageItem = new ToolStripMenuItem("Manage Connections...");
            manageItem.Click += (s, e) => ShowMainWindow();
            connectionsMenu.DropDownItems.Add(manageItem);

            menu.Items.Add(connectionsMenu);
            menu.Items.Add(new ToolStripSeparator());

            // Show Main Window
            var showWindowItem = new ToolStripMenuItem("Show Main Window");
            showWindowItem.Click += (s, e) => ShowMainWindow();
            menu.Items.Add(showWindowItem);

            // Settings
            var settingsItem = new ToolStripMenuItem("Settings...");
            settingsItem.Click += (s, e) => ShowMainWindowWithSettings();
            menu.Items.Add(settingsItem);

            menu.Items.Add(new ToolStripSeparator());

            // Exit
            var exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += (s, e) => ExitApplication();
            menu.Items.Add(exitItem);

            _notifyIcon.ContextMenuStrip = menu;

            Logger.Debug("Context menu built with {Count} connections", profiles.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to build context menu");
        }
    }

    private List<ConnectionProfile> LoadConnectionProfiles()
    {
        try
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var profilesFile = Path.Combine(appData, "WindowsDb2Editor", "connection_profiles.json");

            if (!File.Exists(profilesFile))
            {
                Logger.Debug("Connection profiles file not found: {File}", profilesFile);
                return new List<ConnectionProfile>();
            }

            var json = File.ReadAllText(profilesFile);
            var profiles = JsonSerializer.Deserialize<List<ConnectionProfile>>(json) ?? new List<ConnectionProfile>();

            Logger.Debug("Loaded {Count} connection profiles", profiles.Count);
            return profiles;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load connection profiles");
            return new List<ConnectionProfile>();
        }
    }

    private void LaunchConnection(ConnectionProfile profile)
    {
        try
        {
            Logger.Info("Launching connection: {ProfileName}", profile.Name);

            var mainAppPath = GetMainApplicationPath();
            if (string.IsNullOrEmpty(mainAppPath) || !File.Exists(mainAppPath))
            {
                Logger.Error("Main application not found: {Path}", mainAppPath);
                ShowNotification("Error", $"Could not find WindowsDb2Editor.exe", ToolTipIcon.Error);
                return;
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = mainAppPath,
                Arguments = $"--profile \"{profile.Name}\"",
                UseShellExecute = true
            };

            Process.Start(startInfo);
            Logger.Info("Launched main application with profile: {ProfileName}", profile.Name);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to launch connection: {ProfileName}", profile.Name);
            ShowNotification("Error", $"Failed to launch connection: {ex.Message}", ToolTipIcon.Error);
        }
    }

    private string GetMainApplicationPath()
    {
        // Try to find WindowsDb2Editor.exe in common locations
        var currentDir = AppDomain.CurrentDomain.BaseDirectory;
        var possiblePaths = new[]
        {
            Path.Combine(currentDir, "WindowsDb2Editor.exe"),
            Path.Combine(currentDir, "..", "WindowsDb2Editor.exe"),
            Path.Combine(currentDir, "..", "..", "WindowsDb2Editor.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "WindowsDb2Editor", "WindowsDb2Editor.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WindowsDb2Editor", "WindowsDb2Editor.exe")
        };

        foreach (var path in possiblePaths)
        {
            var fullPath = Path.GetFullPath(path);
            if (File.Exists(fullPath))
            {
                Logger.Debug("Found main application at: {Path}", fullPath);
                return fullPath;
            }
        }

        Logger.Warn("Main application not found in any expected location");
        return string.Empty;
    }

    private void ShowMainWindow()
    {
        try
        {
            Logger.Debug("Showing main window");
            var mainAppPath = GetMainApplicationPath();
            if (!string.IsNullOrEmpty(mainAppPath) && File.Exists(mainAppPath))
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = mainAppPath,
                    UseShellExecute = true
                };
                Process.Start(startInfo);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to show main window");
        }
    }

    private void ShowMainWindowWithSettings()
    {
        try
        {
            Logger.Debug("Showing main window with settings");
            var mainAppPath = GetMainApplicationPath();
            if (!string.IsNullOrEmpty(mainAppPath) && File.Exists(mainAppPath))
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = mainAppPath,
                    Arguments = "--settings",
                    UseShellExecute = true
                };
                Process.Start(startInfo);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to show settings");
        }
    }

    private void ExitApplication()
    {
        Logger.Info("Exit requested from tray icon");
        Application.Exit();
    }

    public void ShowNotification(string iconType, string title, string message, int timeout = 5000)
    {
        if (_notifyIcon == null || _disposed)
            return;

        try
        {
            var toolTipIcon = iconType.ToUpperInvariant() switch
            {
                "INFO" => ToolTipIcon.Info,
                "WARNING" => ToolTipIcon.Warning,
                "ERROR" => ToolTipIcon.Error,
                "SUCCESS" => ToolTipIcon.Info, // Success uses Info icon
                _ => ToolTipIcon.Info
            };

            _notifyIcon.BalloonTipIcon = toolTipIcon;
            _notifyIcon.BalloonTipTitle = title;
            _notifyIcon.BalloonTipText = message;
            _notifyIcon.ShowBalloonTip(timeout);

            Logger.Debug("Notification shown: {Title} - {Message}", title, message);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to show notification: {Title}", title);
        }
    }

    public void RefreshContextMenu()
    {
        if (_notifyIcon == null || _disposed)
            return;

        Logger.Debug("Refreshing context menu");
        BuildContextMenu();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.Visible = false;
                _notifyIcon.Dispose();
                _notifyIcon = null;
            }
            _disposed = true;
            Logger.Info("Tray icon manager disposed");
        }
    }
}
