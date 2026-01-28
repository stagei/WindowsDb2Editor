using System;
using System.IO;
using System.Threading;
using System.Windows;
using NLog;
using WindowsDb2EditorTray.Services;

namespace WindowsDb2EditorTray;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private static Mutex? _mutex;
    private TrayIconManager? _trayIconManager;
    private NotificationFolderWatcher? _folderWatcher;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Single instance check
        const string mutexName = "WindowsDb2EditorTray_SingleInstance";
        _mutex = new Mutex(true, mutexName, out bool isNewInstance);

        if (!isNewInstance)
        {
            Logger.Warn("Another instance of WindowsDb2EditorTray is already running. Exiting.");
            Shutdown();
            return;
        }

        try
        {
            // Initialize NLog configuration
            var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nlog.config");
            if (File.Exists(configPath))
            {
                LogManager.LoadConfiguration(configPath);
            }

            // Initialize logging
            var logDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "WindowsDb2Editor",
                "Logs");
            Directory.CreateDirectory(logDir);

            Logger.Info("===== WindowsDb2EditorTray Starting =====");
            Logger.Info("Version: 1.0.0");

            // Initialize tray icon manager
            Logger.Debug("Initializing tray icon manager");
            _trayIconManager = new TrayIconManager();
            _trayIconManager.Initialize();
            Logger.Info("Tray icon manager initialized");

            // Initialize folder watcher
            Logger.Debug("Initializing notification folder watcher");
            var notificationsFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "WindowsDb2Editor",
                "Notifications");
            Directory.CreateDirectory(notificationsFolder);

            _folderWatcher = new NotificationFolderWatcher(notificationsFolder, _trayIconManager);
            _folderWatcher.Start();
            Logger.Info("Notification folder watcher started");

            // Create and hide main window (tray icon only)
            var mainWindow = new MainWindow();
            mainWindow.WindowState = WindowState.Minimized;
            mainWindow.Hide();
            MainWindow = mainWindow;

            Logger.Info("Application startup completed successfully");
        }
        catch (Exception ex)
        {
            Logger.Fatal(ex, "Fatal error during application startup");
            MessageBox.Show(
                $"Fatal error during application startup:\n\n{ex.Message}\n\nThe application will now close.",
                "Fatal Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Shutdown(1);
        }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Logger.Info("===== WindowsDb2EditorTray Shutting Down =====");

        _folderWatcher?.Stop();
        _folderWatcher?.Dispose();

        _trayIconManager?.Dispose();

        _mutex?.ReleaseMutex();
        _mutex?.Dispose();

        base.OnExit(e);
    }
}
