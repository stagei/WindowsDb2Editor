using System.Collections.Generic;

namespace WindowsDb2Editor.Models;

/// <summary>
/// User access level based on DB2 DBAUTH privileges
/// </summary>
public enum UserAccessLevel
{
    Low,      // Read-only, not in DBAUTH
    Middle,   // Standard user, in DBAUTH but not DBA
    DBA       // Full access, has DBA authority
}

/// <summary>
/// User permissions determined from SYSCAT.DBAUTH
/// </summary>
public class UserPermissions
{
    public UserAccessLevel AccessLevel { get; set; }
    public string Username { get; set; } = string.Empty;
    public bool CanExecuteDDL { get; set; }
    public bool CanExecuteDML { get; set; }
    public bool CanForceDisconnect { get; set; }
    public bool CanModifyStatistics { get; set; }
    public bool CanModifyCDC { get; set; }
    public bool CanDropObjects { get; set; }
    public List<string> GrantedAuthorities { get; set; } = new();
    
    /// <summary>
    /// Access level badge text for UI
    /// </summary>
    public string AccessLevelBadge
    {
        get
        {
            return AccessLevel switch
            {
                UserAccessLevel.DBA => "🛡️ DBA",
                UserAccessLevel.Middle => "👤 USER",
                UserAccessLevel.Low => "🔒 READ-ONLY",
                _ => "❓ UNKNOWN"
            };
        }
    }
    
    /// <summary>
    /// Badge color for UI
    /// </summary>
    public string BadgeColor
    {
        get
        {
            return AccessLevel switch
            {
                UserAccessLevel.DBA => "Green",
                UserAccessLevel.Middle => "Orange",
                UserAccessLevel.Low => "Red",
                _ => "Gray"
            };
        }
    }
    
    /// <summary>
    /// Tooltip explaining user's permissions
    /// </summary>
    public string PermissionsTooltip
    {
        get
        {
            return AccessLevel switch
            {
                UserAccessLevel.DBA => "Full database administrator access - All operations allowed",
                UserAccessLevel.Middle => "Standard user access - DML allowed, DDL blocked",
                UserAccessLevel.Low => "Read-only access - SELECT statements only",
                _ => "Unknown access level"
            };
        }
    }
}

