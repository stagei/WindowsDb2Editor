using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using NLog;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Service for managing job status files for Missing FK Discovery batch jobs.
/// Provides file-based communication between detached batch job and UI.
/// 
/// File locations:
/// - Status/trigger files (lock, status, error): MissingFK/ folder (for easy client monitoring)
/// - Project files (input, results, logs): MissingFK/Projects/{project}/ folder
/// </summary>
public class MissingFKJobStatusService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    /// <summary>
    /// Gets the base folder for status/trigger files.
    /// These are stored in the MissingFK root for easy client monitoring.
    /// </summary>
    private static string GetStatusBaseFolder()
    {
        return UserDataFolderHelper.GetMissingFKBaseFolder();
    }

    /// <summary>
    /// Get status file path for a job (in MissingFK base folder).
    /// </summary>
    private static string GetStatusFilePath(string jobId)
    {
        return Path.Combine(GetStatusBaseFolder(), $"job_status_{jobId}.json");
    }
    
    /// <summary>
    /// Get status file path for a job (legacy - uses outputFolder for backwards compatibility).
    /// </summary>
    private static string GetStatusFilePath(string outputFolder, string jobId)
    {
        // First check the new location (MissingFK base folder)
        var newPath = GetStatusFilePath(jobId);
        if (File.Exists(newPath))
            return newPath;
        
        // Fall back to old location (project folder)
        return Path.Combine(outputFolder, $"missing_fk_status_{jobId}.json");
    }

    /// <summary>
    /// Get error file path for a job (in MissingFK base folder).
    /// </summary>
    private static string GetErrorFilePath(string jobId)
    {
        return Path.Combine(GetStatusBaseFolder(), $"job_error_{jobId}.json");
    }
    
    /// <summary>
    /// Get error file path for a job (legacy - uses outputFolder for backwards compatibility).
    /// </summary>
    private static string GetErrorFilePath(string outputFolder, string jobId)
    {
        // First check the new location (MissingFK base folder)
        var newPath = GetErrorFilePath(jobId);
        if (File.Exists(newPath))
            return newPath;
        
        // Fall back to old location (project folder)
        return Path.Combine(outputFolder, $"missing_fk_error_{jobId}.json");
    }

    /// <summary>
    /// Get lock file path (in MissingFK base folder).
    /// This is the global lock for all Missing FK jobs.
    /// </summary>
    public static string GetLockFilePath()
    {
        return Path.Combine(GetStatusBaseFolder(), ".job_lock");
    }
    
    /// <summary>
    /// Get lock file path (legacy - kept for backwards compatibility).
    /// </summary>
    private static string GetLockFilePath(string outputFolder)
    {
        // Always use the central lock file now
        return GetLockFilePath();
    }

    /// <summary>
    /// Get log file path for a job (in project folder).
    /// </summary>
    public static string GetLogFilePath(string outputFolder, string jobId)
    {
        return Path.Combine(outputFolder, $"job_{jobId}.log");
    }

    /// <summary>
    /// Create initial status file when job starts.
    /// Status file is stored in MissingFK base folder for easy client monitoring.
    /// </summary>
    public void CreateStatusFile(string outputFolder, string jobId)
    {
        Logger.Debug("Creating status file for job: {JobId}", jobId);

        try
        {
            // Use central status file location (MissingFK base folder)
            var statusPath = GetStatusFilePath(jobId);

            // Delete existing status file if present (from previous run)
            if (File.Exists(statusPath))
            {
                File.Delete(statusPath);
                Logger.Debug("Deleted existing status file: {Path}", statusPath);
            }
            
            // Also store project folder reference in status
            var status = new MissingFKJobStatus
            {
                JobId = jobId,
                Status = "running",
                StartedAtUtc = DateTime.UtcNow,
                CompletedAtUtc = null,
                ProjectFolder = outputFolder,
                Progress = new MissingFKJobProgress
                {
                    TablesScanned = 0,
                    TotalTables = 0,
                    CurrentTable = string.Empty,
                    Phase = "starting"
                },
                Error = null
            };

            WriteStatusFileAtomic(statusPath, status);
            Logger.Info("Created status file in MissingFK folder: {Path}", statusPath);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to create status file for job: {JobId}", jobId);
            throw;
        }
    }

    /// <summary>
    /// Update status file with new status and progress.
    /// Status file is stored in MissingFK base folder for easy client monitoring.
    /// </summary>
    public void UpdateStatusFile(string outputFolder, string jobId, string status, MissingFKJobProgress? progress = null)
    {
        Logger.Debug("Updating status file for job: {JobId}, status: {Status}", jobId, status);

        try
        {
            // Use central status file location
            var statusPath = GetStatusFilePath(jobId);
            MissingFKJobStatus? existingStatus = null;

            // Read existing status if file exists
            if (File.Exists(statusPath))
            {
                try
                {
                    var json = File.ReadAllText(statusPath);
                    existingStatus = JsonSerializer.Deserialize<MissingFKJobStatus>(json);
                }
                catch (Exception ex)
                {
                    Logger.Warn(ex, "Failed to read existing status file, creating new one");
                }
            }

            var newStatus = existingStatus ?? new MissingFKJobStatus
            {
                JobId = jobId,
                StartedAtUtc = DateTime.UtcNow,
                ProjectFolder = outputFolder
            };

            newStatus.Status = status;
            if (progress != null)
            {
                newStatus.Progress = progress;
            }

            if (status == "completed" || status == "error")
            {
                newStatus.CompletedAtUtc = DateTime.UtcNow;
            }

            WriteStatusFileAtomic(statusPath, newStatus);
            Logger.Debug("Updated status file: {Path}, status: {Status}", statusPath, status);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to update status file for job: {JobId}", jobId);
            throw;
        }
    }

    /// <summary>
    /// Delete status file when job completes successfully.
    /// </summary>
    public void DeleteStatusFile(string outputFolder, string jobId)
    {
        Logger.Debug("Deleting status file for job: {JobId}", jobId);

        try
        {
            // Delete from central location
            var statusPath = GetStatusFilePath(jobId);
            if (File.Exists(statusPath))
            {
                File.Delete(statusPath);
                Logger.Info("Deleted status file: {Path}", statusPath);
            }
            
            // Also try to delete from old location (project folder) for cleanup
            var oldPath = Path.Combine(outputFolder, $"missing_fk_status_{jobId}.json");
            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
                Logger.Debug("Deleted legacy status file: {Path}", oldPath);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to delete status file for job: {JobId}", jobId);
            // Don't throw - deletion failure is not critical
        }
    }

    /// <summary>
    /// Create error file when job fails.
    /// Error file is stored in MissingFK base folder for easy client monitoring.
    /// </summary>
    public void CreateErrorFile(string outputFolder, string jobId, Exception error)
    {
        Logger.Debug("Creating error file for job: {JobId}", jobId);

        try
        {
            // Use central location
            var errorPath = GetErrorFilePath(jobId);

            var errorInfo = new MissingFKJobError
            {
                JobId = jobId,
                ErrorMessage = error.Message,
                ErrorType = error.GetType().Name,
                StackTrace = error.StackTrace ?? string.Empty,
                OccurredAtUtc = DateTime.UtcNow
            };

            var json = JsonSerializer.Serialize(errorInfo, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(errorPath, json);
            Logger.Info("Created error file in MissingFK folder: {Path}", errorPath);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to create error file for job: {JobId}", jobId);
            // Don't throw - error file creation failure is not critical
        }
    }

    /// <summary>
    /// Read current status file from MissingFK base folder.
    /// </summary>
    public MissingFKJobStatus? ReadStatusFile(string outputFolder, string jobId)
    {
        try
        {
            // Try central location first
            var statusPath = GetStatusFilePath(jobId);
            if (!File.Exists(statusPath))
            {
                // Fall back to old location (project folder)
                statusPath = Path.Combine(outputFolder, $"missing_fk_status_{jobId}.json");
                if (!File.Exists(statusPath))
                {
                    return null;
                }
            }

            var json = File.ReadAllText(statusPath);
            return JsonSerializer.Deserialize<MissingFKJobStatus>(json);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to read status file for job: {JobId}", jobId);
            return null;
        }
    }

    /// <summary>
    /// Check if job is currently running (status file exists in MissingFK base folder).
    /// </summary>
    public bool IsJobRunning(string jobId)
    {
        var statusPath = GetStatusFilePath(jobId);
        return File.Exists(statusPath);
    }
    
    /// <summary>
    /// Check if job is currently running (legacy overload for backwards compatibility).
    /// </summary>
    public bool IsJobRunning(string outputFolder, string jobId)
    {
        // Use the new central location
        return IsJobRunning(jobId);
    }

    /// <summary>
    /// Acquire job lock (prevents multiple concurrent jobs).
    /// Lock file is stored in MissingFK base folder.
    /// </summary>
    public bool AcquireJobLock(string jobId)
    {
        Logger.Debug("Acquiring job lock for job: {JobId}", jobId);

        try
        {
            var lockPath = GetLockFilePath();

            // Check if lock file exists
            if (File.Exists(lockPath))
            {
                // Try to read existing lock to see if it's stale
                try
                {
                    var json = File.ReadAllText(lockPath);
                    var existingLock = JsonSerializer.Deserialize<MissingFKJobLock>(json);
                    if (existingLock != null)
                    {
                        // Check if lock is older than 24 hours (stale lock)
                        var lockAge = DateTime.UtcNow - existingLock.StartedAtUtc;
                        if (lockAge.TotalHours > 24)
                        {
                            Logger.Warn("Removing stale lock file (age: {Age} hours)", lockAge.TotalHours);
                            File.Delete(lockPath);
                        }
                        else
                        {
                            Logger.Warn("Job lock already exists for job: {ExistingJobId}", existingLock.JobId);
                            return false;
                        }
                    }
                }
                catch
                {
                    // If we can't read the lock file, assume it's stale and delete it
                    Logger.Warn("Removing unreadable lock file");
                    File.Delete(lockPath);
                }
            }

            // Create new lock file
            var lockInfo = new MissingFKJobLock
            {
                JobId = jobId,
                StartedAtUtc = DateTime.UtcNow
            };

            var lockJson = JsonSerializer.Serialize(lockInfo, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(lockPath, lockJson);
            Logger.Info("Acquired job lock in MissingFK folder: {Path}", lockPath);
            return true;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to acquire job lock for job: {JobId}", jobId);
            return false;
        }
    }
    
    /// <summary>
    /// Acquire job lock (legacy overload for backwards compatibility).
    /// </summary>
    public bool AcquireJobLock(string outputFolder, string jobId)
    {
        // Use the new central lock
        return AcquireJobLock(jobId);
    }

    /// <summary>
    /// Release job lock from MissingFK base folder.
    /// </summary>
    public void ReleaseJobLock()
    {
        Logger.Debug("Releasing job lock");

        try
        {
            var lockPath = GetLockFilePath();
            if (File.Exists(lockPath))
            {
                File.Delete(lockPath);
                Logger.Info("Released job lock: {Path}", lockPath);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to release job lock");
            // Don't throw - lock release failure is not critical
        }
    }
    
    /// <summary>
    /// Release job lock (legacy overload for backwards compatibility).
    /// </summary>
    public void ReleaseJobLock(string outputFolder)
    {
        // Use the new central lock
        ReleaseJobLock();
    }

    /// <summary>
    /// Check if a job is locked (lock file exists in MissingFK base folder).
    /// </summary>
    public bool IsJobLocked()
    {
        var lockPath = GetLockFilePath();
        if (!File.Exists(lockPath))
        {
            return false;
        }

        // Check if lock is stale
        try
        {
            var json = File.ReadAllText(lockPath);
            var lockInfo = JsonSerializer.Deserialize<MissingFKJobLock>(json);
            if (lockInfo != null)
            {
                var lockAge = DateTime.UtcNow - lockInfo.StartedAtUtc;
                return lockAge.TotalHours <= 24; // Lock is valid if less than 24 hours old
            }
        }
        catch
        {
            // If we can't read the lock, assume it's invalid
        }

        return false;
    }
    
    /// <summary>
    /// Check if job is locked (legacy overload for backwards compatibility).
    /// </summary>
    public bool IsJobLocked(string outputFolder)
    {
        // Use the new central lock
        return IsJobLocked();
    }

    /// <summary>
    /// Read lock file info from MissingFK base folder.
    /// </summary>
    public MissingFKJobLock? ReadLockFile()
    {
        var lockPath = GetLockFilePath();
        if (!File.Exists(lockPath))
        {
            return null;
        }

        try
        {
            var json = File.ReadAllText(lockPath);
            return JsonSerializer.Deserialize<MissingFKJobLock>(json);
        }
        catch (Exception ex)
        {
            Logger.Warn(ex, "Failed to read lock file info");
            return null;
        }
    }

    /// <summary>
    /// Get lock file information (legacy overload for backwards compatibility).
    /// </summary>
    public MissingFKJobLock? GetLockInfo(string outputFolder)
    {
        // Use the new central lock
        return ReadLockFile();
    }

    /// <summary>
    /// Write status file atomically (write to temp file, then rename).
    /// </summary>
    private void WriteStatusFileAtomic(string statusPath, MissingFKJobStatus status)
    {
        var tempPath = statusPath + ".tmp";
        var json = JsonSerializer.Serialize(status, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(tempPath, json);
        File.Move(tempPath, statusPath, overwrite: true);
    }
}

/// <summary>
/// Job status model.
/// Stored in MissingFK folder for easy client monitoring.
/// </summary>
public class MissingFKJobStatus
{
    public string JobId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty; // "running", "completed", "error"
    public DateTime StartedAtUtc { get; set; }
    public DateTime? CompletedAtUtc { get; set; }
    public string? ProjectFolder { get; set; } // Path to project folder with results
    public MissingFKJobProgress Progress { get; set; } = new();
    public string? Error { get; set; }
}

/// <summary>
/// Job progress model.
/// </summary>
public class MissingFKJobProgress
{
    public int TablesScanned { get; set; }
    public int TotalTables { get; set; }
    public string CurrentTable { get; set; } = string.Empty;
    public string Phase { get; set; } = string.Empty; // "extracting", "analyzing", "generating"
}

/// <summary>
/// Job error model.
/// </summary>
public class MissingFKJobError
{
    public string JobId { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    public string ErrorType { get; set; } = string.Empty;
    public string StackTrace { get; set; } = string.Empty;
    public DateTime OccurredAtUtc { get; set; }
}

/// <summary>
/// Job lock model.
/// </summary>
public class MissingFKJobLock
{
    public string JobId { get; set; } = string.Empty;
    public DateTime StartedAtUtc { get; set; }
}
