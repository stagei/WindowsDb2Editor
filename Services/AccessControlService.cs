using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Service for determining user access levels based on DB2 DBAUTH
/// </summary>
public class AccessControlService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    /// <summary>
    /// Determine user's access level by querying SYSCAT.DBAUTH
    /// </summary>
    public async Task<UserPermissions> DetermineAccessLevelAsync(
        DB2ConnectionManager connectionManager,
        string fullUsername)
    {
        Logger.Info("Determining access level for user: {User}", fullUsername);
        
        try
        {
            // Parse username (remove domain prefix)
            var username = ParseUsernameWithoutDomain(fullUsername);
            Logger.Debug("Username without domain: {Username}", username);
            
            // Query SYSCAT.DBAUTH
            var sql = $@"
                SELECT * FROM SYSCAT.DBAUTH 
                WHERE GRANTEE = '{username}'
            ";
            
            Logger.Debug("Querying DBAUTH for user: {Username}", username);
            var result = await connectionManager.ExecuteQueryAsync(sql);
            
            var permissions = new UserPermissions { Username = username };
            
            if (result.Rows.Count == 0)
            {
                // User not in DBAUTH - LOW level (read-only)
                permissions.AccessLevel = UserAccessLevel.Low;
                permissions.CanExecuteDDL = false;
                permissions.CanExecuteDML = false;
                permissions.CanForceDisconnect = false;
                permissions.CanModifyStatistics = false;
                permissions.CanModifyCDC = false;
                permissions.CanDropObjects = false;
                
                Logger.Info("User {Username} assigned LOW access level (not in DBAUTH)", username);
                Logger.Warn("User {Username} has READ-ONLY access (not in DBAUTH table)", username);
            }
            else
            {
                var row = result.Rows[0];
                var dbAdmAuth = row["DBADMAUTH"]?.ToString();
                
                Logger.Debug("DBAUTH row found for {Username} - DBADMAUTH: {Auth}", username, dbAdmAuth);
                
                if (dbAdmAuth == "Y")
                {
                    // DBA level - full access
                    permissions.AccessLevel = UserAccessLevel.DBA;
                    permissions.CanExecuteDDL = true;
                    permissions.CanExecuteDML = true;
                    permissions.CanForceDisconnect = true;
                    permissions.CanModifyStatistics = true;
                    permissions.CanModifyCDC = true;
                    permissions.CanDropObjects = true;
                    
                    Logger.Info("User {Username} assigned DBA access level (DBADMAUTH = Y)", username);
                }
                else
                {
                    // Middle level - standard user
                    permissions.AccessLevel = UserAccessLevel.Middle;
                    permissions.CanExecuteDDL = false;
                    permissions.CanExecuteDML = true;  // Can do DML but not DDL
                    permissions.CanForceDisconnect = false;
                    permissions.CanModifyStatistics = false;
                    permissions.CanModifyCDC = false;
                    permissions.CanDropObjects = false;
                    
                    Logger.Info("User {Username} assigned MIDDLE access level (in DBAUTH, not DBA)", username);
                }
                
                // Log all granted authorities
                Logger.Debug("Checking granted authorities for {Username}", username);
                foreach (DataColumn col in result.Columns)
                {
                    if (col.ColumnName.EndsWith("AUTH"))
                    {
                        var value = row[col]?.ToString();
                        if (value == "Y")
                        {
                            permissions.GrantedAuthorities.Add(col.ColumnName);
                            Logger.Debug("Authority granted: {Authority} = Y", col.ColumnName);
                        }
                    }
                }
                
                Logger.Info("User {Username} has {Count} granted authorities", 
                    username, permissions.GrantedAuthorities.Count);
            }
            
            return permissions;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to determine access level for user: {User}", fullUsername);
            Logger.Warn("SECURITY: Defaulting to LOW (read-only) access level due to error");
            
            // Default to LOW (read-only) on error for security
            return new UserPermissions
            {
                Username = fullUsername,
                AccessLevel = UserAccessLevel.Low,
                CanExecuteDDL = false,
                CanExecuteDML = false,
                CanForceDisconnect = false,
                CanModifyStatistics = false,
                CanModifyCDC = false,
                CanDropObjects = false
            };
        }
    }
    
    /// <summary>
    /// Parse username to remove domain prefix
    /// </summary>
    /// <param name="fullUsername">Full username (may include domain)</param>
    /// <returns>Username without domain prefix</returns>
    /// <example>
    /// "DOMAIN\username" -> "username"
    /// "username@domain.com" -> "username"
    /// "username" -> "username"
    /// </example>
    public string ParseUsernameWithoutDomain(string fullUsername)
    {
        Logger.Debug("Parsing username: {FullUsername}", fullUsername);
        
        if (string.IsNullOrWhiteSpace(fullUsername))
        {
            Logger.Warn("Empty username provided");
            return string.Empty;
        }
        
        // Remove Windows domain prefix: "DOMAIN\username" -> "username"
        if (fullUsername.Contains("\\"))
        {
            var parsed = fullUsername.Split('\\')[1];
            Logger.Debug("Removed domain prefix: {FullUsername} -> {Parsed}", fullUsername, parsed);
            return parsed;
        }
        
        // Remove email domain: "username@domain.com" -> "username"
        if (fullUsername.Contains("@"))
        {
            var parsed = fullUsername.Split('@')[0];
            Logger.Debug("Removed email domain: {FullUsername} -> {Parsed}", fullUsername, parsed);
            return parsed;
        }
        
        // No domain found, return as-is
        Logger.Debug("No domain found, using as-is: {Username}", fullUsername);
        return fullUsername;
    }
    
    /// <summary>
    /// Validate if user can perform an operation
    /// </summary>
    public bool CanUserPerformOperation(UserPermissions permissions, string operationType)
    {
        Logger.Debug("Checking if user {Username} ({Level}) can perform: {Operation}",
            permissions.Username, permissions.AccessLevel, operationType);
        
        var canPerform = operationType.ToUpperInvariant() switch
        {
            "DDL" => permissions.CanExecuteDDL,
            "DML" => permissions.CanExecuteDML,
            "FORCE_DISCONNECT" => permissions.CanForceDisconnect,
            "RUNSTATS" => permissions.CanModifyStatistics,
            "CDC" => permissions.CanModifyCDC,
            "DROP" => permissions.CanDropObjects,
            "SELECT" => true, // Everyone can SELECT
            _ => false
        };
        
        Logger.Debug("Can perform {Operation}: {CanPerform}", operationType, canPerform);
        return canPerform;
    }
}

