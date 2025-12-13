using System.Windows;
using NLog;
using WindowsDb2Editor.Data;
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
            var mainWindow = new MainWindow();
            mainWindow.Show();
            
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

        MessageBox.Show(
            $"A fatal error occurred:\n\n{exception?.Message}\n\nThe application will now close.",
            "Fatal Error",
            MessageBoxButton.OK,
            MessageBoxImage.Error);
    }

    private void App_DispatcherUnhandledException(object sender, 
        System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        Logger.Error(e.Exception, "Unhandled dispatcher exception");
        Logger.Debug($"Exception type: {e.Exception.GetType().Name}");
        Logger.Debug($"Stack trace: {e.Exception.StackTrace}");

        MessageBox.Show(
            $"An error occurred:\n\n{e.Exception.Message}",
            "Error",
            MessageBoxButton.OK,
            MessageBoxImage.Error);

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
            var connectionManager = new DB2ConnectionManager(connection);
            await connectionManager.OpenAsync();
            
            Logger.Info("Connection opened successfully");
            
            // Create GUI testing service
            var guiTestingService = new GuiTestingService(MetadataHandler ?? throw new InvalidOperationException("MetadataHandler not initialized"));
            
            // Execute form testing
            var result = await guiTestingService.TestFormAsync(
                connectionManager,
                cliArgs.TestForm,
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
            connectionManager.Close();
            
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

    protected override void OnExit(ExitEventArgs e)
    {
        Logger.Info("===== WindowsDb2Editor Application Shutting Down =====");
        Logger.Debug($"Exit code: {e.ApplicationExitCode}");
        LoggingService.Shutdown();
        base.OnExit(e);
    }
}

