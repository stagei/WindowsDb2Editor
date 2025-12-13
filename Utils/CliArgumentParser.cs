using NLog;

namespace WindowsDb2Editor.Utils;

/// <summary>
/// Parsed CLI arguments - EXTENDED for automated testing
/// </summary>
public class CliArguments
{
    // Existing parameters
    public string? ProfileName { get; set; }
    public string? Sql { get; set; }
    public string? OutFile { get; set; }
    public string? Format { get; set; } = "json"; // json, csv, tsv, xml
    public bool Help { get; set; }
    public bool CollectMetadata { get; set; }
    
    // NEW: Command-based execution for automated testing
    public string? Command { get; set; } // table-props, trigger-info, view-info, etc.
    public string? Object { get; set; } // SCHEMA.TABLE, SCHEMA.VIEW, etc.
    public string? Schema { get; set; } // Schema filter
    public string? ObjectType { get; set; } // TABLE, VIEW, PROCEDURE, FUNCTION, TRIGGER
    public int? Limit { get; set; } // Result limit (default: no limit)
    public bool IncludeDependencies { get; set; } // Include dependency analysis
    public bool IncludeSourceCode { get; set; } // Include source code
}

/// <summary>
/// Parser for command-line arguments
/// </summary>
public class CliArgumentParser
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    public static CliArguments Parse(string[] args)
    {
        Logger.Debug("Parsing CLI arguments: {Args}", string.Join(" ", args));
        
        var cliArgs = new CliArguments();
        
        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i].ToLowerInvariant())
            {
                case "-profile":
                case "--profile":
                    if (i + 1 < args.Length)
                    {
                        cliArgs.ProfileName = args[++i];
                        Logger.Debug("Profile: {Profile}", cliArgs.ProfileName);
                    }
                    break;
                    
                case "-sql":
                case "--sql":
                    if (i + 1 < args.Length)
                    {
                        cliArgs.Sql = args[++i];
                        Logger.Debug("SQL: {SQL}", cliArgs.Sql?.Substring(0, Math.Min(50, cliArgs.Sql.Length)));
                    }
                    break;
                    
                case "-outfile":
                case "--outfile":
                case "-out":
                    if (i + 1 < args.Length)
                    {
                        cliArgs.OutFile = args[++i];
                        Logger.Debug("OutFile: {OutFile}", cliArgs.OutFile);
                    }
                    break;
                    
                case "-format":
                case "--format":
                    if (i + 1 < args.Length)
                    {
                        cliArgs.Format = args[++i].ToLowerInvariant();
                        Logger.Debug("Format: {Format}", cliArgs.Format);
                    }
                    break;
                    
                case "-collectmetadata":
                case "--collectmetadata":
                case "-metadata":
                    cliArgs.CollectMetadata = true;
                    Logger.Debug("CollectMetadata: true");
                    break;
                    
                case "-help":
                case "--help":
                case "-h":
                case "/?":
                case "/help":
                    cliArgs.Help = true;
                    Logger.Debug("Help requested");
                    break;
                    
                // NEW: Command-based parameters for automated testing
                case "-command":
                case "--command":
                case "-cmd":
                    if (i + 1 < args.Length)
                    {
                        cliArgs.Command = args[++i].ToLowerInvariant();
                        Logger.Debug("Command: {Command}", cliArgs.Command);
                    }
                    break;
                    
                case "-object":
                case "--object":
                case "-obj":
                    if (i + 1 < args.Length)
                    {
                        cliArgs.Object = args[++i];
                        Logger.Debug("Object: {Object}", cliArgs.Object);
                    }
                    break;
                    
                case "-schema":
                case "--schema":
                    if (i + 1 < args.Length)
                    {
                        cliArgs.Schema = args[++i];
                        Logger.Debug("Schema: {Schema}", cliArgs.Schema);
                    }
                    break;
                    
                case "-objecttype":
                case "--objecttype":
                case "-type":
                    if (i + 1 < args.Length)
                    {
                        cliArgs.ObjectType = args[++i].ToUpperInvariant();
                        Logger.Debug("ObjectType: {ObjectType}", cliArgs.ObjectType);
                    }
                    break;
                    
                case "-limit":
                case "--limit":
                    if (i + 1 < args.Length && int.TryParse(args[++i], out int limit))
                    {
                        cliArgs.Limit = limit;
                        Logger.Debug("Limit: {Limit}", cliArgs.Limit);
                    }
                    break;
                    
                case "-includedependencies":
                case "--includedependencies":
                case "-deps":
                    cliArgs.IncludeDependencies = true;
                    Logger.Debug("IncludeDependencies: true");
                    break;
                    
                case "-includesourcecode":
                case "--includesourcecode":
                case "-source":
                    cliArgs.IncludeSourceCode = true;
                    Logger.Debug("IncludeSourceCode: true");
                    break;
            }
        }
        
        Logger.Info("CLI arguments parsed - Profile: {Profile}, Command: {Command}, Object: {Object}, HasSQL: {HasSQL}", 
            cliArgs.ProfileName, cliArgs.Command, cliArgs.Object, !string.IsNullOrEmpty(cliArgs.Sql));
        
        return cliArgs;
    }
}

