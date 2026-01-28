using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using NLog;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Service for managing job status files for Missing FK Discovery batch jobs.
/// Provides file-based communication between detached batch job and UI.
/// </summary>
public class MissingFKJobStatusService
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Get status file path for a job.
    /// </summary>
    private static string GetStatusFilePath(string outputFolder, string jobId)
    {
        return Path.Combine(outputFolder, $"missing_fk_status_{jobId}.json");
    }

    /// <summary>
    /// Get error file path for a job.
    /// </summary>
    private static string GetErrorFilePath(string outputFolder, string jobId)
    {
        return Path.Combine(outputFolder, $"missing_fk_error_{jobId}.json");
    }

    /// <summary>
    /// Get lock file path.
    /// </summary>
    private static string GetLockFilePath(string outputFolder)
    {
        return Path.Combine(outputFolder, ".missing_fk_job_lock");
    }

    /// <summary>
    /// Get log file path for a job.
    /// </summary>
    public static string GetLogFilePath(string outputFolder, string jobId)
    {
        return Path.Combine(outputFolder, $"missing_fk_job_{jobId}.log");
    }

    /// <summary>
    /// Create initial status file when job starts.
    /// </summary>
    public void CreateStatusFile(string outputFolder, string jobId)
    {
        Logger.Debug("Creating status file for job: {JobId}", jobId);

        try
        {
            var statusPath = GetStatusFilePath(outputFolder, jobId);

            // Delete existing status file if present (from previous run)
            if (File.Exists(statusPath))
            {
                File.Delete(statusPath);
                Logger.Debug("Deleted existing status file: {Path}", statusPath);
            }

            var status = new MissingFKJobStatus
            {
                JobId = jobId,
                Status = "running",
                StartedAtUtc = DateTime.UtcNow,
                CompletedAtUtc = null,
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
            Logger.Info("Created status file: {Path}", statusPath);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to create status file for job: {JobId}", jobId);
            throw;
        }
    }

    /// <summary>
    /// Update status file with new status and progress.
    /// </summary>
    public void UpdateStatusFile(string outputFolder, string jobId, string status, MissingFKJobProgress? progress = null)
    {
        Logger.Debug("Updating status file for job: {JobId}, status: {Status}", jobId, status);

        try
        {
            var statusPath = GetStatusFilePath(outputFolder, jobId);
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
                StartedAtUtc = DateTime.UtcNow
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
            var statusPath = GetStatusFilePath(outputFolder, jobId);
            if (File.Exists(statusPath))
            {
                File.Delete(statusPath);
                Logger.Info("Deleted status file: {Path}", statusPath);
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
    /// </summary>
    public void CreateErrorFile(string outputFolder, string jobId, Exception error)
    {
        Logger.Debug("Creating error file for job: {JobId}", jobId);

        try
        {
            var errorPath = GetErrorFilePath(outputFolder, jobId);

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
            Logger.Info("Created error file: {Path}", errorPath);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to create error file for job: {JobId}", jobId);
            // Don't throw - error file creation failure is not critical
        }
    }

    /// <summary>
    /// Read current status file.
    /// </summary>
    public MissingFKJobStatus? ReadStatusFile(string outputFolder, string jobId)
    {
        try
        {
            var statusPath = GetStatusFilePath(outputFolder, jobId);
            if (!File.Exists(statusPath))
            {
                return null;
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
    /// Check if job is currently running (status file exists).
    /// </summary>
    public bool IsJobRunning(string outputFolder, string jobId)
    {
        var statusPath = GetStatusFilePath(outputFolder, jobId);
        return File.Exists(statusPath);
    }

    /// <summary>
    /// Acquire job lock (prevents multiple concurrent jobs).
    /// </summary>
    public bool AcquireJobLock(string outputFolder, string jobId)
    {
        Logger.Debug("Acquiring job lock for job: {JobId}", jobId);

        try
        {
            var lockPath = GetLockFilePath(outputFolder);

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
            Logger.Info("Acquired job lock: {Path}", lockPath);
            return true;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to acquire job lock for job: {JobId}", jobId);
            return false;
        }
    }

    /// <summary>
    /// Release job lock.
    /// </summary>
    public void ReleaseJobLock(string outputFolder)
    {
        Logger.Debug("Releasing job lock");

        try
        {
            var lockPath = GetLockFilePath(outputFolder);
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
    /// Check if job is locked (lock file exists).
    /// </summary>
    public bool IsJobLocked(string outputFolder)
    {
        var lockPath = GetLockFilePath(outputFolder);
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
    /// Get lock file information (if exists).
    /// </summary>
    public MissingFKJobLock? GetLockInfo(string outputFolder)
    {
        var lockPath = GetLockFilePath(outputFolder);
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
/// </summary>
public class MissingFKJobStatus
{
    public string JobId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty; // "running", "completed", "error"
    public DateTime StartedAtUtc { get; set; }
    public DateTime? CompletedAtUtc { get; set; }
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
