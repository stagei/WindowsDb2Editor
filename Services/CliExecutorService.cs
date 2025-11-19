using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Models;
using WindowsDb2Editor.Utils;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Service for executing CLI commands
/// </summary>
public class CliExecutorService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly ConnectionProfileService _profileService;
    private readonly ExportService _exportService;
    
    public CliExecutorService()
    {
        _profileService = new ConnectionProfileService();
        _exportService = new ExportService();
        Logger.Debug("CliExecutorService initialized");
    }
    
    /// <summary>
    /// Execute CLI command
    /// </summary>
    public async Task<int> ExecuteAsync(CliArguments args)
    {
        Logger.Info("CLI Execution started - Profile: {Profile}, Output: {Output}", 
            args.ProfileName, args.OutFile);
        
        try
        {
            // Handle help
            if (args.Help)
            {
                PrintHelp();
                return 0;
            }
            
            // Validate arguments
            if (string.IsNullOrEmpty(args.ProfileName))
            {
                Console.Error.WriteLine("ERROR: -Profile parameter is required");
                Logger.Error("CLI execution failed: No profile specified");
                return 1;
            }
            
            // Load connection profile
            Console.WriteLine($"Loading connection profile: {args.ProfileName}");
            var profile = _profileService.GetProfile(args.ProfileName);
            
            if (profile == null)
            {
                Console.Error.WriteLine($"ERROR: Profile '{args.ProfileName}' not found");
                Logger.Error("CLI execution failed: Profile not found - {Profile}", args.ProfileName);
                return 1;
            }
            
            // Handle metadata collection
            if (args.CollectMetadata)
            {
                return await CollectMetadataAsync(profile);
            }
            
            // Validate SQL and output file for query execution
            if (string.IsNullOrEmpty(args.Sql))
            {
                Console.Error.WriteLine("ERROR: -Sql parameter is required");
                Logger.Error("CLI execution failed: No SQL specified");
                return 1;
            }
            
            if (string.IsNullOrEmpty(args.OutFile))
            {
                Console.Error.WriteLine("ERROR: -Outfile parameter is required");
                Logger.Error("CLI execution failed: No output file specified");
                return 1;
            }
            
            // Execute query and export
            return await ExecuteQueryAndExportAsync(profile, args.Sql, args.OutFile, args.Format ?? "json");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "CLI execution failed");
            Console.Error.WriteLine($"ERROR: {ex.Message}");
            return 1;
        }
    }
    
    /// <summary>
    /// Execute query and export results
    /// </summary>
    private async Task<int> ExecuteQueryAndExportAsync(DB2Connection profile, string sql, string outFile, string format)
    {
        try
        {
            // Connect to database
            Console.WriteLine($"Connecting to {profile.Database} @ {profile.Server}:{profile.Port}");
            using var connectionManager = new DB2ConnectionManager(profile);
            await connectionManager.OpenAsync();
            
            Console.WriteLine("Connected successfully");
            
            // Execute query
            Console.WriteLine("Executing query...");
            var results = await connectionManager.ExecuteQueryAsync(sql);
            
            Console.WriteLine($"Query returned {results.Rows.Count} rows");
            
            // Export results
            Console.WriteLine($"Exporting to {outFile} (format: {format})");
            await ExportResults(results, outFile, format);
            
            Console.WriteLine("Export completed successfully");
            Logger.Info("CLI query execution completed successfully");
            
            return 0;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to execute query and export");
            Console.Error.WriteLine($"ERROR: {ex.Message}");
            return 1;
        }
    }
    
    /// <summary>
    /// Collect metadata for profile
    /// </summary>
    private async Task<int> CollectMetadataAsync(DB2Connection profile)
    {
        try
        {
            Console.WriteLine($"Connecting to {profile.Database} @ {profile.Server}:{profile.Port}");
            using var connectionManager = new DB2ConnectionManager(profile);
            await connectionManager.OpenAsync();
            
            Console.WriteLine("Connected successfully");
            Console.WriteLine("Collecting metadata...");
            
            // Feature #5: Metadata collection
            var metadataService = new DB2MetadataService();
            await metadataService.CollectMetadataAsync(connectionManager, profile.Name);
            
            Console.WriteLine("Metadata collection completed successfully");
            Logger.Info("Metadata collected for profile: {Profile}", profile.Name);
            
            return 0;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to collect metadata");
            Console.Error.WriteLine($"ERROR: {ex.Message}");
            return 1;
        }
    }
    
    /// <summary>
    /// Export results to file based on format
    /// </summary>
    private async Task ExportResults(DataTable dataTable, string outFile, string format)
    {
        Logger.Debug("Exporting to {Format} format: {File}", format, outFile);
        
        switch (format.ToLowerInvariant())
        {
            case "json":
                await _exportService.ExportToJsonAsync(dataTable, outFile);
                break;
                
            case "csv":
                await _exportService.ExportToCsvAsync(dataTable, outFile);
                break;
                
            case "tsv":
                await _exportService.ExportToTsvAsync(dataTable, outFile);
                break;
                
            case "xml":
                await _exportService.ExportToXmlAsync(dataTable, outFile);
                break;
                
            default:
                throw new ArgumentException($"Unknown format: {format}. Supported formats: json, csv, tsv, xml");
        }
        
        Logger.Info("Export completed: {Format} -> {File}", format, outFile);
    }
    
    /// <summary>
    /// Print CLI help message
    /// </summary>
    public static void PrintHelp()
    {
        Console.WriteLine(@"
WindowsDb2Editor - Command Line Interface
==========================================

USAGE:
    WindowsDb2Editor.exe -Profile <name> -Sql <query> -Outfile <path> [-Format <format>]
    WindowsDb2Editor.exe -Profile <name> -CollectMetadata
    WindowsDb2Editor.exe -Help

PARAMETERS:
    -Profile <name>     Connection profile name (required)
    -Sql <query>        SQL query to execute (required for query mode)
    -Outfile <path>     Output file path (required for query mode)
    -Format <format>    Output format: json, csv, tsv, xml (default: json)
    -CollectMetadata    Collect database metadata (Feature #5)
    -Help               Show this help message

EXAMPLES:
    # Export query results to JSON
    WindowsDb2Editor.exe -Profile ""ILOGTST"" -Sql ""SELECT * FROM SYSCAT.TABLES FETCH FIRST 10 ROWS ONLY"" -Outfile ""output.json""
    
    # Export to CSV
    WindowsDb2Editor.exe -Profile ""PRODDB"" -Sql ""SELECT * FROM SYSCAT.COLUMNS WHERE TABNAME = 'CUSTOMERS'"" -Outfile ""columns.csv"" -Format csv
    
    # Collect metadata
    WindowsDb2Editor.exe -Profile ""ILOGTST"" -CollectMetadata

EXIT CODES:
    0   Success
    1   Error occurred

NOTES:
    - Connection profiles are stored in: %LOCALAPPDATA%\WindowsDb2Editor\connection_profiles.json
    - Create profiles in the GUI first, then reference them by name in CLI
    - Passwords are encrypted in saved profiles
");
    }
}

