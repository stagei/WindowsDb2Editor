using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using NLog;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Service for managing saved search patterns for Missing FK Discovery.
/// Stores search patterns (table name or column name) with pattern type (standard or regex).
/// </summary>
public class MissingFKSearchHistoryService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly string _historyFilePath;
    private List<MissingFKSearchHistoryItem> _history;
    private readonly int _maxHistoryItems;

    public MissingFKSearchHistoryService(int maxHistoryItems = 50)
    {
        Logger.Debug("MissingFKSearchHistoryService initializing");

        _maxHistoryItems = maxHistoryItems;
        _historyFilePath = DetermineFilePath("missing-fk-search-history.json");

        _history = new List<MissingFKSearchHistoryItem>();
        LoadHistory();

        Logger.Info($"MissingFKSearchHistoryService initialized with max {_maxHistoryItems} items");
    }
    
    /// <summary>
    /// Determines the correct file path, handling migration from old AppData location.
    /// </summary>
    private static string DetermineFilePath(string fileName)
    {
        var newPath = UserDataFolderHelper.GetFilePath(fileName);
        var oldAppDataFolder = UserDataFolderHelper.GetOldAppDataFolder();
        var oldPath = Path.Combine(oldAppDataFolder, fileName);
        
        if (File.Exists(newPath)) return newPath;
        
        if (File.Exists(oldPath))
        {
            try
            {
                var newFolder = Path.GetDirectoryName(newPath);
                if (!string.IsNullOrEmpty(newFolder) && !Directory.Exists(newFolder))
                    Directory.CreateDirectory(newFolder);
                    
                File.Copy(oldPath, newPath, overwrite: false);
                Logger.Info("Migrated {FileName} to new location", fileName);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "Could not migrate {FileName}", fileName);
                return oldPath;
            }
        }
        
        return newPath;
    }

    /// <summary>
    /// Save search pattern to history.
    /// </summary>
    public void SaveSearchPattern(string pattern, string searchType, string patternType, string? name = null)
    {
        Logger.Debug("Saving search pattern to history: {Pattern}, Type: {SearchType}, PatternType: {PatternType}", 
            pattern, searchType, patternType);

        try
        {
            var item = new MissingFKSearchHistoryItem
            {
                Name = name ?? $"Search_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}",
                Pattern = pattern,
                SearchType = searchType, // "TableName" or "ColumnName"
                PatternType = patternType, // "Standard" or "Regex"
                SavedAt = DateTime.Now
            };

            // Remove existing pattern with same values (avoid duplicates)
            _history.RemoveAll(h => 
                h.Pattern == pattern && 
                h.SearchType == searchType && 
                h.PatternType == patternType);

            _history.Insert(0, item);

            // Keep only max items
            if (_history.Count > _maxHistoryItems)
            {
                _history = _history.Take(_maxHistoryItems).ToList();
            }

            SaveHistory();
            Logger.Info("Search pattern saved to history: {Name}", item.Name);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to save search pattern to history");
            throw;
        }
    }

    /// <summary>
    /// Get all search history items.
    /// </summary>
    public List<MissingFKSearchHistoryItem> GetHistory()
    {
        return new List<MissingFKSearchHistoryItem>(_history);
    }

    /// <summary>
    /// Delete a search pattern from history.
    /// </summary>
    public void DeleteSearchPattern(string name)
    {
        Logger.Debug("Deleting search pattern from history: {Name}", name);

        try
        {
            _history.RemoveAll(h => h.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            SaveHistory();
            Logger.Info("Search pattern deleted from history: {Name}", name);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to delete search pattern from history");
            throw;
        }
    }

    private void LoadHistory()
    {
        try
        {
            if (File.Exists(_historyFilePath))
            {
                var json = File.ReadAllText(_historyFilePath);
                var items = JsonSerializer.Deserialize<List<MissingFKSearchHistoryItem>>(json);
                if (items != null)
                {
                    _history = items.OrderByDescending(h => h.SavedAt).ToList();
                    Logger.Debug("Loaded {Count} search patterns from history", _history.Count);
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load search history");
            _history = new List<MissingFKSearchHistoryItem>();
        }
    }

    private void SaveHistory()
    {
        try
        {
            var json = JsonSerializer.Serialize(_history, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(_historyFilePath, json);
            Logger.Debug("Saved {Count} search patterns to history", _history.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to save search history");
            throw;
        }
    }
}

/// <summary>
/// Represents a saved search pattern in history.
/// </summary>
public class MissingFKSearchHistoryItem
{
    public string Name { get; set; } = string.Empty;
    public string Pattern { get; set; } = string.Empty;
    public string SearchType { get; set; } = "TableName"; // "TableName" or "ColumnName"
    public string PatternType { get; set; } = "Standard"; // "Standard" or "Regex"
    public DateTime SavedAt { get; set; }
}
