using System.IO;
using System.Text.Json;
using NLog;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Service for managing SQL query history
/// </summary>
public class QueryHistoryService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly string _historyFilePath;
    private List<QueryHistoryItem> _history;
    private readonly int _maxHistoryItems;

    public QueryHistoryService(int maxHistoryItems = 100)
    {
        Logger.Debug("QueryHistoryService initializing");

        _maxHistoryItems = maxHistoryItems;
        _historyFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "WindowsDb2Editor",
            "query-history.json");

        _history = new List<QueryHistoryItem>();
        LoadHistory();

        Logger.Info($"QueryHistoryService initialized with max {_maxHistoryItems} items");
    }

    /// <summary>
    /// Add query to history
    /// </summary>
    public void AddQuery(string sql, string database, bool success, double executionTimeMs, int? rowCount = null)
    {
        Logger.Debug($"Adding query to history: {sql.Substring(0, Math.Min(50, sql.Length))}...");

        var item = new QueryHistoryItem
        {
            Sql = sql,
            Database = database,
            ExecutedAt = DateTime.Now,
            Success = success,
            ExecutionTimeMs = executionTimeMs,
            RowCount = rowCount
        };

        _history.Insert(0, item); // Add to beginning

        // Trim history to max items
        if (_history.Count > _maxHistoryItems)
        {
            _history = _history.Take(_maxHistoryItems).ToList();
            Logger.Debug($"Trimmed history to {_maxHistoryItems} items");
        }

        SaveHistory();
        Logger.Info($"Query added to history (total: {_history.Count})");
    }

    /// <summary>
    /// Get all query history
    /// </summary>
    public List<QueryHistoryItem> GetHistory()
    {
        return _history;
    }

    /// <summary>
    /// Search query history
    /// </summary>
    public List<QueryHistoryItem> SearchHistory(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return _history;
        }

        Logger.Debug($"Searching history for: {searchTerm}");

        var results = _history
            .Where(item => item.Sql.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();

        Logger.Debug($"Found {results.Count} matching queries");
        return results;
    }

    /// <summary>
    /// Clear all history
    /// </summary>
    public void ClearHistory()
    {
        Logger.Info("Clearing query history");
        _history.Clear();
        SaveHistory();
    }

    /// <summary>
    /// Load history from file
    /// </summary>
    private void LoadHistory()
    {
        try
        {
            if (!File.Exists(_historyFilePath))
            {
                Logger.Debug($"History file not found: {_historyFilePath}");
                return;
            }

            var json = File.ReadAllText(_historyFilePath);
            _history = JsonSerializer.Deserialize<List<QueryHistoryItem>>(json) ?? new List<QueryHistoryItem>();

            Logger.Info($"Loaded {_history.Count} queries from history");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load query history");
            _history = new List<QueryHistoryItem>();
        }
    }

    /// <summary>
    /// Save history to file
    /// </summary>
    private void SaveHistory()
    {
        try
        {
            var directory = Path.GetDirectoryName(_historyFilePath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                Logger.Debug($"Created history directory: {directory}");
            }

            var json = JsonSerializer.Serialize(_history, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_historyFilePath, json);
            Logger.Debug($"Saved {_history.Count} queries to history file");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to save query history");
        }
    }
}

/// <summary>
/// Model for query history item
/// </summary>
public class QueryHistoryItem
{
    public string Sql { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;
    public DateTime ExecutedAt { get; set; }
    public bool Success { get; set; }
    public double ExecutionTimeMs { get; set; }
    public int? RowCount { get; set; }
}

