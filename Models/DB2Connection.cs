namespace WindowsDb2Editor.Models;

/// <summary>
/// Model for DB2 database connection information
/// </summary>
public class DB2Connection
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Server { get; set; } = string.Empty;
    public int Port { get; set; } = 50000;
    public string Database { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool SavePassword { get; set; } = false;
    public int ConnectionTimeout { get; set; } = 30;

    /// <summary>
    /// Get connection string (with password masked for logging)
    /// </summary>
    public string GetConnectionString(bool maskPassword = false)
    {
        var pwd = maskPassword ? "***" : Password;
        return $"Server={Server}:{Port};Database={Database};UID={Username};PWD={pwd};ConnectTimeout={ConnectionTimeout};";
    }

    /// <summary>
    /// Get display name for the connection
    /// </summary>
    public string GetDisplayName()
    {
        return string.IsNullOrEmpty(Name) 
            ? $"{Database} @ {Server}" 
            : $"{Name} ({Database} @ {Server})";
    }

    /// <summary>
    /// Validate connection information
    /// </summary>
    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Server) &&
               !string.IsNullOrWhiteSpace(Database) &&
               !string.IsNullOrWhiteSpace(Username) &&
               Port > 0 && Port <= 65535;
    }
}

