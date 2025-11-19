using System;

namespace WindowsDb2Editor.Models;

/// <summary>
/// Represents database lock information
/// </summary>
public class LockInfo
{
    public string TabSchema { get; set; } = string.Empty;
    public string TabName { get; set; } = string.Empty;
    public string LockStatus { get; set; } = string.Empty;
    public string LockMode { get; set; } = string.Empty;
    public string PrimaryAuthId { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string ApplicationName { get; set; } = string.Empty;
    public int AgentId { get; set; }
    
    /// <summary>
    /// Get friendly description of lock mode
    /// </summary>
    public string LockModeDescription
    {
        get
        {
            return LockMode switch
            {
                "IS" => "Intent Share",
                "IX" => "Intent Exclusive",
                "S" => "Share",
                "U" => "Update",
                "X" => "Exclusive",
                "SIX" => "Share with Intent Exclusive",
                "Z" => "Super Exclusive",
                "K" => "Row Lock",
                "N" => "Next Key Lock",
                _ => LockMode
            };
        }
    }
    
    /// <summary>
    /// Get lock severity level for color coding
    /// </summary>
    public string LockSeverity
    {
        get
        {
            return LockMode switch
            {
                "X" or "Z" => "High",        // Exclusive locks
                "U" or "SIX" => "Medium",    // Update locks
                "S" or "IX" or "IS" => "Low", // Share locks
                _ => "Unknown"
            };
        }
    }
}

/// <summary>
/// Filter for lock monitoring
/// </summary>
public class LockMonitorFilter
{
    public string? SchemaFilter { get; set; } = "*";
    public string? TableFilter { get; set; } = "*";
    public bool ShowOnlyBlocked { get; set; } = false;
    public bool ExcludeSystemTables { get; set; } = true;
}

