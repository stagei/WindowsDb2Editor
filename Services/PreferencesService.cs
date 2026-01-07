using System;
using System.IO;
using System.Text.Json;
using NLog;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Services
{
    /// <summary>
    /// Service for managing user preferences stored in preferences.json
    /// </summary>
    public class PreferencesService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private const string PreferencesFileName = "preferences.json";
        private readonly string _preferencesFilePath;

        public UserPreferences Preferences { get; private set; } = new UserPreferences();

        public PreferencesService()
        {
            Logger.Debug("PreferencesService initialized");
            
            // Store preferences.json in user's AppData folder
            _preferencesFilePath = AppDataHelper.GetDataFilePath(PreferencesFileName);
            
            Logger.Debug("Preferences file path: {Path}", _preferencesFilePath);
            
            LoadPreferences();
        }

        /// <summary>
        /// Load preferences from JSON file, or create default if not exists
        /// </summary>
        public void LoadPreferences()
        {
            Logger.Debug("Loading preferences from file");
            
            try
            {
                if (File.Exists(_preferencesFilePath))
                {
                    Logger.Debug("Preferences file found, reading contents");
                    
                    var json = File.ReadAllText(_preferencesFilePath);
                    Preferences = JsonSerializer.Deserialize<UserPreferences>(json, new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        PropertyNameCaseInsensitive = true
                    }) ?? new UserPreferences();
                    
                    Logger.Info("Preferences loaded successfully - MaxRows: {MaxRows}", Preferences.MaxRowsPerQuery);
                }
                else
                {
                    Logger.Info("Preferences file not found, creating default preferences");
                    Preferences = new UserPreferences();
                    SavePreferences();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error loading preferences, using defaults");
                Preferences = new UserPreferences();
            }
        }

        /// <summary>
        /// Save current preferences to JSON file
        /// </summary>
        public void SavePreferences()
        {
            Logger.Debug("Saving preferences to file");
            
            try
            {
                var json = JsonSerializer.Serialize(Preferences, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                
                File.WriteAllText(_preferencesFilePath, json);
                
                Logger.Info("Preferences saved successfully");
                Logger.Debug("Saved preferences: {Json}", json);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error saving preferences");
            }
        }

        /// <summary>
        /// Update a specific preference and save
        /// </summary>
        public void UpdateMaxRowsPerQuery(int maxRows)
        {
            Logger.Debug("Updating MaxRowsPerQuery from {Old} to {New}", Preferences.MaxRowsPerQuery, maxRows);
            
            Preferences.MaxRowsPerQuery = maxRows;
            SavePreferences();
        }
        
        /// <summary>
        /// Reload preferences from file
        /// </summary>
        public void Reload()
        {
            LoadPreferences();
        }
        
        /// <summary>
        /// Increase all font sizes by 1 and save (UI, Grid, Editor)
        /// </summary>
        public void IncreaseFontSize()
        {
            if (Preferences.UIFontSize < 24)
            {
                Preferences.UIFontSize++;
                Preferences.GridFontSize = Math.Min(36, Preferences.GridFontSize + 1);
                Preferences.FontSize = Math.Min(36, Preferences.FontSize + 1);
                SavePreferences();
                Logger.Info("All font sizes increased - UI: {UI}, Grid: {Grid}, Editor: {Editor}", 
                    Preferences.UIFontSize, Preferences.GridFontSize, Preferences.FontSize);
            }
        }
        
        /// <summary>
        /// Decrease all font sizes by 1 and save (UI, Grid, Editor)
        /// </summary>
        public void DecreaseFontSize()
        {
            if (Preferences.UIFontSize > 8)
            {
                Preferences.UIFontSize--;
                Preferences.GridFontSize = Math.Max(8, Preferences.GridFontSize - 1);
                Preferences.FontSize = Math.Max(8, Preferences.FontSize - 1);
                SavePreferences();
                Logger.Info("All font sizes decreased - UI: {UI}, Grid: {Grid}, Editor: {Editor}", 
                    Preferences.UIFontSize, Preferences.GridFontSize, Preferences.FontSize);
            }
        }
        
        /// <summary>
        /// Increase TreeView item spacing by 1 and save
        /// </summary>
        public void IncreaseTreeViewSpacing()
        {
            if (Preferences.TreeViewItemSpacing < 20)
            {
                Preferences.TreeViewItemSpacing++;
                SavePreferences();
                Logger.Info("TreeView spacing increased to {Spacing}", Preferences.TreeViewItemSpacing);
            }
        }
        
        /// <summary>
        /// Decrease TreeView item spacing by 1 and save
        /// </summary>
        public void DecreaseTreeViewSpacing()
        {
            if (Preferences.TreeViewItemSpacing > 0)
            {
                Preferences.TreeViewItemSpacing--;
                SavePreferences();
                Logger.Info("TreeView spacing decreased to {Spacing}", Preferences.TreeViewItemSpacing);
            }
        }
    }
}

