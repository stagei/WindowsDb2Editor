using System.Windows;
using NLog;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    protected override void OnStartup(StartupEventArgs e)
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

            // Set up global exception handling
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;

            Logger.Debug("Global exception handlers registered");
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

    protected override void OnExit(ExitEventArgs e)
    {
        Logger.Info("===== WindowsDb2Editor Application Shutting Down =====");
        Logger.Debug($"Exit code: {e.ApplicationExitCode}");
        LoggingService.Shutdown();
        base.OnExit(e);
    }
}

