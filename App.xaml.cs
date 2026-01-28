using System.Windows;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Dialogs;
using WindowsDb2Editor.Models;
using WindowsDb2Editor.Services;
using WindowsDb2Editor.Utils;

namespace WindowsDb2Editor;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    public static MetadataHandler? MetadataHandler { get; private set; }
    public static PreferencesService? PreferencesService { get; private set; }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        try
        {
            // Initialize NLog
            LoggingService.Initialize();
            Logger.Info("===== WindowsDb2Editor Application Starting =====");
            Logger.Info($"Framework: .NET 10");
            Logger.Info($"Version: 1.0.0");
            Logger.Debug($"Startup arguments: {string.Join(" ", e.Args)}");

            // Initialize MetadataHandler (loads ConfigFiles)
            Logger.Debug("Initializing MetadataHandler");
            MetadataHandler = new MetadataHandler();
            Logger.Info("MetadataHandler initialized successfully");
            
            // Initialize PreferencesService
            Logger.Debug("Initializing PreferencesService");
            PreferencesService = new PreferencesService();
            Logger.Info("PreferencesService initialized successfully");
            
            // Apply saved log level preference
            var savedLogLevel = PreferencesService.Preferences.LogLevel;
            Logger.Debug("Applying saved log level: {Level}", savedLogLevel);
            LoggingService.SetLogLevel(savedLogLevel);
            
            // Check and manage Windows startup registry entry
            Logger.Debug("Checking Windows startup configuration");
            var startupManager = new StartupManagerService();
            var isStartupInRegistry = startupManager.IsStartupEnabled();
            var isStartupInPreferences = PreferencesService.Preferences.StartupEnabled;
            
            if (isStartupInPreferences && !isStartupInRegistry)
            {
                Logger.Info("Startup enabled in preferences but not in registry - adding to startup");
                startupManager.EnableStartup();
            }
            else if (!isStartupInPreferences && isStartupInRegistry)
            {
                Logger.Info("Startup disabled in preferences but present in registry - removing from startup");
                startupManager.DisableStartup();
            }
            else
            {
                Logger.Debug("Startup configuration matches preferences: {Enabled}", isStartupInPreferences);
            }
            
            // Start tray icon app if preference is enabled
            if (PreferencesService.Preferences.ShowTrayIcon)
            {
                StartTrayIconApp();
            }
            
            // Initialize theme before GUI mode
            Logger.Debug("Initializing application theme");
            var configService = new ConfigurationService();
            var themeService = new ThemeService(configService);
            themeService.InitializeTheme();
            Logger.Info("Theme initialized: {Theme}", themeService.GetThemeName());

            // Feature #4: Check if CLI mode (has command-line arguments)
            if (e.Args.Length > 0)
            {
                Logger.Info("CLI mode detected - {Count} arguments", e.Args.Length);
                
                var cliArgs = CliArgumentParser.Parse(e.Args);
                
                if (cliArgs.Help)
                {
                    CliExecutorService.PrintHelp();
                    Shutdown(0);
                    return;
                }
                
                // Check if this is GUI mode with auto-open
                if (!string.IsNullOrEmpty(cliArgs.Open) && !string.IsNullOrEmpty(cliArgs.ProfileName))
                {
                    Logger.Info("GUI mode with auto-open - Profile: {Profile}, Element: {Element}", 
                        cliArgs.ProfileName, cliArgs.Open);
                    
                    await LaunchGuiWithAutoOpenAsync(cliArgs);
                    return; // Keep GUI running
                }
                
                // Check if this is GUI form testing mode
                if (!string.IsNullOrEmpty(cliArgs.TestForm))
                {
                    Logger.Info("GUI form testing mode detected - TestForm: {TestForm}, Object: {Object}, Tab: {Tab}", 
                        cliArgs.TestForm, cliArgs.Object, cliArgs.Tab);
                    
                    var guiTestExitCode = await ExecuteGuiTestingAsync(cliArgs);
                    
                    Logger.Info("GUI testing completed with exit code: {ExitCode}", guiTestExitCode);
                    Shutdown(guiTestExitCode);
                    return;
                }
                
                // Execute CLI command (regular mode)
                var executor = new CliExecutorService();
                var exitCode = await executor.ExecuteAsync(cliArgs);
                
                Logger.Info("CLI execution completed with exit code: {ExitCode}", exitCode);
                Shutdown(exitCode);
                return;
            }

            // Normal GUI mode
            Logger.Info("GUI mode - launching main window");
            
            // Set up global exception handling
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;

            Logger.Debug("Global exception handlers registered");
            
            // Create and show main window
            // Note: Taskbar icon comes from ApplicationIcon in .csproj file
            // MainWindow.Icon (set in XAML) controls the window title bar icon
            var mainWindow = new MainWindow();
            mainWindow.Show();
            
            // Apply global font size from preferences
            if (PreferencesService != null)
            {
                GlobalFontService.ApplyGlobalFontSize(PreferencesService.Preferences.UIFontSize);
                Logger.Debug("Global font size applied: {Size}", PreferencesService.Preferences.UIFontSize);
            }
            
            Logger.Info("Application startup completed successfully");
        }
        catch (Exception ex)
        {
            // Critical failure during startup
            Logger.Fatal(ex, "Fatal error during application startup");
            MessageBox.Show(
                $"Fatal error during application startup:\n\n{ex.Message}\n\nThe application will now close.",
                "Fatal Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Shutdown(1);
        }
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        var exception = e.ExceptionObject as Exception;
        Logger.Fatal(exception, "Unhandled exception in application domain");
        Logger.Fatal($"Is terminating: {e.IsTerminating}");

        // Show user-friendly error dialog with expandable details
        if (exception != null)
        {
            ErrorDialog.ShowError(exception, "Fatal Error", isFatal: true);
        }
        else
        {
            ErrorDialog.ShowError(
                "A fatal error occurred. The application will now close.",
                "Fatal Error",
                e.ExceptionObject?.ToString());
        }
    }

    private void App_DispatcherUnhandledException(object sender, 
        System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        Logger.Error(e.Exception, "Unhandled dispatcher exception");
        Logger.Debug($"Exception type: {e.Exception.GetType().Name}");
        Logger.Debug($"Stack trace: {e.Exception.StackTrace}");

        // Show user-friendly error dialog with expandable details
        ErrorDialog.ShowError(e.Exception, "Error", isFatal: false);

        // Mark as handled to prevent application crash
        e.Handled = true;
    }
    
    private async Task<int> ExecuteGuiTestingAsync(CliArguments cliArgs)
    {
        try
        {
            Logger.Info("Starting GUI testing - Form: {Form}, Object: {Object}", cliArgs.TestForm, cliArgs.Object);
            
            // Validate required parameters
            if (string.IsNullOrEmpty(cliArgs.ProfileName))
            {
                Logger.Error("Profile name is required for GUI testing");
                Console.WriteLine("Error: --profile parameter is required");
                return 1;
            }
            
            if (string.IsNullOrEmpty(cliArgs.Object))
            {
                Logger.Error("Object name is required for GUI testing");
                Console.WriteLine("Error: --object parameter is required");
                return 1;
            }
            
            // Load connection profile
            Logger.Debug("Loading connection profile: {Profile}", cliArgs.ProfileName);
            var connectionStorage = new ConnectionStorageService();
            var connection = connectionStorage.GetConnection(cliArgs.ProfileName);
            
            if (connection == null)
            {
                Logger.Error("Connection profile not found: {Profile}", cliArgs.ProfileName);
                Console.WriteLine($"Error: Connection profile '{cliArgs.ProfileName}' not found");
                return 1;
            }
            
            // Create connection manager
            Logger.Debug("Creating connection manager");
            var connectionManager = ConnectionManagerFactory.CreateConnectionManager(connection);
            await connectionManager.OpenAsync();
            
            Logger.Info("Connection opened successfully");
            
            // Create GUI testing service
            var guiTestingService = new GuiTestingService(MetadataHandler ?? throw new InvalidOperationException("MetadataHandler not initialized"));
            
            // Execute form testing
            var result = await guiTestingService.TestFormAsync(
                connectionManager,
                cliArgs.TestForm ?? throw new InvalidOperationException("TestForm not provided"),
                cliArgs.Object,
                cliArgs.Tab);
            
            // Serialize result to JSON
            var json = System.Text.Json.JsonSerializer.Serialize(result, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            });
            
            // Write to output file or console
            if (!string.IsNullOrEmpty(cliArgs.OutFile))
            {
                System.IO.File.WriteAllText(cliArgs.OutFile, json);
                Logger.Info("GUI testing results written to: {OutFile}", cliArgs.OutFile);
                Console.WriteLine($"Results written to: {cliArgs.OutFile}");
            }
            else
            {
                Console.WriteLine(json);
            }
            
            // Close connection
            await connectionManager.CloseAsync();
            
            Logger.Info("GUI testing completed successfully");
            return 0;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "GUI testing failed");
            Console.WriteLine($"Error: {ex.Message}");
            return 1;
        }
    }

    private async Task LaunchGuiWithAutoOpenAsync(CliArguments args)
    {
        Logger.Info("Launching GUI with auto-connect and auto-open");
        
        // Set up global exception handling
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        DispatcherUnhandledException += App_DispatcherUnhandledException;

        Logger.Debug("Global exception handlers registered");
        
        // Create and show main window
        var mainWindow = new MainWindow();
        mainWindow.Show();
        
        Logger.Info("MainWindow shown, now auto-connecting to profile: {Profile}", args.ProfileName);
        
        // Wait a bit for window to fully render
        await Task.Delay(500);
        
        // Auto-connect to profile
        try
        {
            var connectionService = new ConnectionStorageService();
            var profile = connectionService.GetConnection(args.ProfileName!);
            
            if (profile == null)
            {
                Logger.Error("Profile not found: {Profile}", args.ProfileName);
                MessageBox.Show($"Profile '{args.ProfileName}' not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            Logger.Info("Profile loaded, establishing connection...");
            
            // Connect via MainWindow's connection method (pass tab parameter from CLI)
            await mainWindow.AutoConnectAndOpenAsync(profile, args.Open!, args.OpenType, args.Tab);
            
            Logger.Info("Auto-connect and auto-open completed successfully");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to auto-connect and open element");
            MessageBox.Show($"Failed to connect:\n\n{ex.Message}", "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// Start the tray icon application if it's not already running
    /// </summary>
    private void StartTrayIconApp()
    {
        try
        {
            var trayAppName = "WindowsDb2EditorTray.exe";
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var trayAppPath = System.IO.Path.Combine(baseDir, trayAppName);

            // Check if tray app exists
            if (!System.IO.File.Exists(trayAppPath))
            {
                Logger.Debug("Tray icon app not found at: {Path}", trayAppPath);
                return;
            }

            // Check if tray app is already running
            var trayProcesses = System.Diagnostics.Process.GetProcessesByName("WindowsDb2EditorTray");
            if (trayProcesses.Length > 0)
            {
                Logger.Debug("Tray icon app is already running (PID: {Pid})", trayProcesses[0].Id);
                foreach (var p in trayProcesses) p.Dispose();
                return;
            }

            // Start the tray app with working directory set to its location
            Logger.Info("Starting tray icon app: {Path}", trayAppPath);
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = trayAppPath,
                UseShellExecute = true,
                WorkingDirectory = baseDir,
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
            };
            System.Diagnostics.Process.Start(startInfo);
            Logger.Info("Tray icon app started successfully");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to start tray icon app");
            // Don't show error to user - tray icon is optional
        }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Logger.Info("===== WindowsDb2Editor Application Shutting Down =====");
        Logger.Debug($"Exit code: {e.ApplicationExitCode}");
        LoggingService.Shutdown();
        base.OnExit(e);
    }
}

