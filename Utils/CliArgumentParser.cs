using NLog;

namespace WindowsDb2Editor.Utils;

/// <summary>
/// Parsed CLI arguments
/// </summary>
public class CliArguments
{
    public string? ProfileName { get; set; }
    public string? Sql { get; set; }
    public string? OutFile { get; set; }
    public string? Format { get; set; } = "json"; // json, csv, tsv, xml
    public bool Help { get; set; }
    public bool CollectMetadata { get; set; }
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
            }
        }
        
        Logger.Info("CLI arguments parsed - Profile: {Profile}, HasSQL: {HasSQL}, HasOutFile: {HasOutFile}", 
            cliArgs.ProfileName, !string.IsNullOrEmpty(cliArgs.Sql), !string.IsNullOrEmpty(cliArgs.OutFile));
        
        return cliArgs;
    }
}

