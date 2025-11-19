using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using NLog;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Service for managing connection profiles
/// </summary>
public class ConnectionProfileService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly string _profilesFile;
    
    public ConnectionProfileService()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var appFolder = Path.Combine(appData, "WindowsDb2Editor");
        _profilesFile = Path.Combine(appFolder, "connection_profiles.json");
        
        Logger.Debug("Connection profiles file: {File}", _profilesFile);
        
        // Ensure directory exists
        var directory = Path.GetDirectoryName(_profilesFile);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Logger.Debug("Created profiles directory: {Directory}", directory);
        }
    }
    
    /// <summary>
    /// Get connection profile by name
    /// </summary>
    public DB2Connection? GetProfile(string profileName)
    {
        Logger.Debug("Loading profile: {ProfileName}", profileName);
        
        if (!File.Exists(_profilesFile))
        {
            Logger.Warn("Profiles file not found: {File}", _profilesFile);
            return null;
        }
        
        try
        {
            var json = File.ReadAllText(_profilesFile);
            var profiles = JsonSerializer.Deserialize<List<DB2Connection>>(json);
            
            var profile = profiles?.FirstOrDefault(p => 
                p.Name.Equals(profileName, StringComparison.OrdinalIgnoreCase));
                
            if (profile == null)
            {
                Logger.Warn("Profile not found: {ProfileName}", profileName);
            }
            else
            {
                Logger.Info("Profile loaded: {ProfileName}", profileName);
            }
            
            return profile;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load profile: {ProfileName}", profileName);
            return null;
        }
    }
    
    /// <summary>
    /// Save connection profile
    /// </summary>
    public void SaveProfile(DB2Connection profile)
    {
        Logger.Info("Saving profile: {ProfileName}", profile.Name);
        
        try
        {
            var profiles = LoadAllProfiles();
            
            // Remove existing profile with same name
            profiles.RemoveAll(p => p.Name.Equals(profile.Name, StringComparison.OrdinalIgnoreCase));
            
            // Add new/updated profile
            profiles.Add(profile);
            
            // Save to file
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true 
            };
            var json = JsonSerializer.Serialize(profiles, options);
            
            File.WriteAllText(_profilesFile, json);
            Logger.Info("Profile saved successfully: {ProfileName}", profile.Name);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to save profile: {ProfileName}", profile.Name);
            throw;
        }
    }
    
    /// <summary>
    /// Load all connection profiles
    /// </summary>
    public List<DB2Connection> LoadAllProfiles()
    {
        if (!File.Exists(_profilesFile))
        {
            Logger.Debug("No profiles file found, returning empty list");
            return new List<DB2Connection>();
        }
            
        try
        {
            var json = File.ReadAllText(_profilesFile);
            var profiles = JsonSerializer.Deserialize<List<DB2Connection>>(json) ?? new List<DB2Connection>();
            
            Logger.Debug("Loaded {Count} profiles", profiles.Count);
            return profiles;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load profiles");
            return new List<DB2Connection>();
        }
    }
    
    /// <summary>
    /// Delete connection profile
    /// </summary>
    public bool DeleteProfile(string profileName)
    {
        Logger.Info("Deleting profile: {ProfileName}", profileName);
        
        try
        {
            var profiles = LoadAllProfiles();
            var removed = profiles.RemoveAll(p => p.Name.Equals(profileName, StringComparison.OrdinalIgnoreCase));
            
            if (removed > 0)
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(profiles, options);
                File.WriteAllText(_profilesFile, json);
                
                Logger.Info("Profile deleted: {ProfileName}", profileName);
                return true;
            }
            
            Logger.Warn("Profile not found for deletion: {ProfileName}", profileName);
            return false;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to delete profile: {ProfileName}", profileName);
            return false;
        }
    }
}

