using NLog;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Service for managing application logging using NLog
/// </summary>
public static class LoggingService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Initialize NLog from configuration file
    /// </summary>
    public static void Initialize()
    {
        try
        {
            LogManager.Setup().LoadConfigurationFromFile("nlog.config");
            Logger.Info("Application logging initialized");
            Logger.Debug("NLog configuration loaded from nlog.config");
        }
        catch (Exception ex)
        {
            // If NLog fails, we need to handle it gracefully
            Console.WriteLine($"Failed to initialize logging: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Log informational message
    /// </summary>
    public static void LogInfo(string message)
    {
        Logger.Info(message);
    }

    /// <summary>
    /// Log debug message with context
    /// </summary>
    public static void LogDebug(string message, params object[] args)
    {
        Logger.Debug(message, args);
    }

    /// <summary>
    /// Log warning message
    /// </summary>
    public static void LogWarning(string message)
    {
        Logger.Warn(message);
    }

    /// <summary>
    /// Log error message with optional exception
    /// </summary>
    public static void LogError(string message, Exception? ex = null)
    {
        if (ex != null)
        {
            Logger.Error(ex, message);
        }
        else
        {
            Logger.Error(message);
        }
    }

    /// <summary>
    /// Log fatal error
    /// </summary>
    public static void LogFatal(string message, Exception? ex = null)
    {
        if (ex != null)
        {
            Logger.Fatal(ex, message);
        }
        else
        {
            Logger.Fatal(message);
        }
    }

    /// <summary>
    /// Shutdown NLog and flush logs
    /// </summary>
    public static void Shutdown()
    {
        Logger.Info("Application logging shutting down");
        LogManager.Shutdown();
    }
}

