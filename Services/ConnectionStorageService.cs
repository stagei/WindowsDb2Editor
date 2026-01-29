using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using NLog;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Services
{
    /// <summary>
    /// Service for storing and retrieving saved connections with encrypted passwords
    /// Uses Windows Data Protection API (DPAPI) for secure password encryption
    /// </summary>
    public class ConnectionStorageService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private const string ConnectionsFileName = "connections.json";
        private readonly string _connectionsFilePath;

        public ConnectionStorageService()
        {
            Logger.Debug("ConnectionStorageService initialized");
            
            // Store connections.json in user's data folder (Documents\WindowsDb2Editor)
            _connectionsFilePath = DetermineConnectionsFilePath();
            
            Logger.Debug("Connections file path: {Path}", _connectionsFilePath);
        }
        
        /// <summary>
        /// Determines the correct connections file path, handling migration from old AppData location.
        /// </summary>
        private string DetermineConnectionsFilePath()
        {
            // New location (uses UserDataFolderHelper)
            var newPath = UserDataFolderHelper.GetFilePath(ConnectionsFileName);
            
            // Old AppData location
            var oldAppDataFolder = UserDataFolderHelper.GetOldAppDataFolder();
            var oldPath = Path.Combine(oldAppDataFolder, ConnectionsFileName);
            
            // If connections exist in new location, use it
            if (File.Exists(newPath))
            {
                Logger.Debug("Found connections in new location: {Path}", newPath);
                return newPath;
            }
            
            // If connections exist in old location, migrate
            if (File.Exists(oldPath))
            {
                Logger.Info("Found connections in old AppData location, migrating: {Path}", oldPath);
                
                try
                {
                    // Ensure new folder exists
                    var newFolder = Path.GetDirectoryName(newPath);
                    if (!string.IsNullOrEmpty(newFolder) && !Directory.Exists(newFolder))
                    {
                        Directory.CreateDirectory(newFolder);
                    }
                    
                    // Copy to new location
                    File.Copy(oldPath, newPath, overwrite: false);
                    Logger.Info("Migrated connections from {Old} to {New}", oldPath, newPath);
                }
                catch (Exception ex)
                {
                    Logger.Warn(ex, "Could not migrate connections, will use old location");
                    return oldPath;
                }
                
                return newPath;
            }
            
            // Neither exists, use new location
            Logger.Debug("No existing connections found, using new location: {Path}", newPath);
            return newPath;
        }

        /// <summary>
        /// Encrypt password using Windows Data Protection API (DPAPI)
        /// Returns Base64 encoded encrypted data
        /// </summary>
        public string EncryptPassword(string plainTextPassword)
        {
            if (string.IsNullOrEmpty(plainTextPassword))
            {
                return string.Empty;
            }

            try
            {
                Logger.Debug("Encrypting password using DPAPI");
                
                // Convert string to bytes
                var plainBytes = Encoding.UTF8.GetBytes(plainTextPassword);
                
                // Encrypt using DPAPI with CurrentUser scope
                var encryptedBytes = ProtectedData.Protect(
                    plainBytes,
                    null, // No additional entropy
                    DataProtectionScope.CurrentUser);
                
                // Convert to Base64
                var base64Encrypted = Convert.ToBase64String(encryptedBytes);
                
                Logger.Debug("Password encrypted successfully (length: {Length})", base64Encrypted.Length);
                return base64Encrypted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to encrypt password");
                throw new Exception("Password encryption failed", ex);
            }
        }

        /// <summary>
        /// Decrypt password using Windows Data Protection API (DPAPI)
        /// Takes Base64 encoded encrypted data
        /// </summary>
        public string DecryptPassword(string base64EncryptedPassword)
        {
            if (string.IsNullOrEmpty(base64EncryptedPassword))
            {
                return string.Empty;
            }

            try
            {
                Logger.Debug("Decrypting password using DPAPI");
                
                // Convert from Base64
                var encryptedBytes = Convert.FromBase64String(base64EncryptedPassword);
                
                // Decrypt using DPAPI
                var decryptedBytes = ProtectedData.Unprotect(
                    encryptedBytes,
                    null, // No additional entropy
                    DataProtectionScope.CurrentUser);
                
                // Convert bytes to string
                var plainText = Encoding.UTF8.GetString(decryptedBytes);
                
                Logger.Debug("Password decrypted successfully");
                return plainText;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to decrypt password");
                throw new Exception("Password decryption failed", ex);
            }
        }

        /// <summary>
        /// Save a connection with encrypted password
        /// </summary>
        public void SaveConnection(DatabaseConnection connection)
        {
            Logger.Info("Saving connection: {Name}", connection.Name);
            Logger.Debug("Connection details - Server: {Server}, Database: {Database}, User: {User}, Provider: {Provider}", 
                connection.Server, connection.Database, connection.Username, connection.ProviderType);

            try
            {
                // Load existing connections
                var container = LoadConnectionsContainer();
                
                // Encrypt password
                var encryptedPassword = EncryptPassword(connection.Password);
                
                // Create saved connection
                var savedConnection = new SavedConnection
                {
                    Name = connection.Name,
                    Server = connection.Server,
                    Port = connection.Port,
                    Database = connection.Database,
                    Username = connection.Username,
                    EncryptedPassword = encryptedPassword,
                    LastUsed = DateTime.Now,
                    Provider = connection.ProviderType?.ToUpperInvariant() ?? "DB2",
                    Version = "12.1" // TODO: Store version from connection
                };
                
                // Remove existing connection with same name
                container.Connections.RemoveAll(c => c.Name.Equals(connection.Name, StringComparison.OrdinalIgnoreCase));
                
                // Add to list (most recent first)
                container.Connections.Insert(0, savedConnection);
                
                // Keep only last 10 connections
                if (container.Connections.Count > 10)
                {
                    container.Connections = container.Connections.Take(10).ToList();
                    Logger.Debug("Trimmed connections list to 10 most recent");
                }
                
                // Save to file
                SaveConnectionsContainer(container);
                
                Logger.Info("Connection saved successfully: {Name}", connection.Name);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to save connection: {Name}", connection.Name);
                throw;
            }
        }

        /// <summary>
        /// Load all saved connections
        /// </summary>
        public List<SavedConnection> LoadConnections()
        {
            Logger.Debug("Loading saved connections");
            
            try
            {
                var container = LoadConnectionsContainer();
                Logger.Info("Loaded {Count} saved connections", container.Connections.Count);
                return container.Connections;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to load connections");
                return new List<SavedConnection>();
            }
        }

        /// <summary>
        /// Get a connection by name with decrypted password
        /// </summary>
        public DatabaseConnection? GetConnection(string connectionName)
        {
            Logger.Debug("Retrieving connection: {Name}", connectionName);
            
            try
            {
                var savedConnection = LoadConnections()
                    .FirstOrDefault(c => c.Name.Equals(connectionName, StringComparison.OrdinalIgnoreCase));
                
                if (savedConnection == null)
                {
                    Logger.Warn("Connection not found: {Name}", connectionName);
                    return null;
                }
                
                // Decrypt password
                var decryptedPassword = DecryptPassword(savedConnection.EncryptedPassword);
                
                // Update last used
                UpdateLastUsed(connectionName);
                
                Logger.Info("Connection retrieved successfully: {Name}", connectionName);
                return savedConnection.ToDatabaseConnection(decryptedPassword);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to retrieve connection: {Name}", connectionName);
                return null;
            }
        }

        /// <summary>
        /// Update last used timestamp for a connection
        /// </summary>
        public void UpdateLastUsed(string connectionName)
        {
            Logger.Debug("Updating last used for connection: {Name}", connectionName);
            
            try
            {
                var container = LoadConnectionsContainer();
                var connection = container.Connections
                    .FirstOrDefault(c => c.Name.Equals(connectionName, StringComparison.OrdinalIgnoreCase));
                
                if (connection != null)
                {
                    connection.LastUsed = DateTime.Now;
                    
                    // Re-sort by last used (most recent first)
                    container.Connections = container.Connections
                        .OrderByDescending(c => c.LastUsed)
                        .ToList();
                    
                    SaveConnectionsContainer(container);
                    Logger.Debug("Last used timestamp updated for: {Name}", connectionName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to update last used for: {Name}", connectionName);
            }
        }

        /// <summary>
        /// Delete a saved connection
        /// </summary>
        public void DeleteConnection(string connectionName)
        {
            Logger.Info("Deleting connection: {Name}", connectionName);
            
            try
            {
                var container = LoadConnectionsContainer();
                var removed = container.Connections.RemoveAll(c => c.Name.Equals(connectionName, StringComparison.OrdinalIgnoreCase));
                
                if (removed > 0)
                {
                    SaveConnectionsContainer(container);
                    Logger.Info("Connection deleted: {Name}", connectionName);
                }
                else
                {
                    Logger.Warn("Connection not found for deletion: {Name}", connectionName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to delete connection: {Name}", connectionName);
                throw;
            }
        }

        /// <summary>
        /// Load connections container from JSON file
        /// </summary>
        private SavedConnectionsContainer LoadConnectionsContainer()
        {
            if (!File.Exists(_connectionsFilePath))
            {
                Logger.Debug("Connections file not found, creating new container");
                return new SavedConnectionsContainer();
            }

            try
            {
                var json = File.ReadAllText(_connectionsFilePath);
                var container = JsonSerializer.Deserialize<SavedConnectionsContainer>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new SavedConnectionsContainer();
                
                Logger.Debug("Connections container loaded from file");
                return container;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to load connections file, returning empty container");
                return new SavedConnectionsContainer();
            }
        }

        /// <summary>
        /// Save connections container to JSON file
        /// </summary>
        private void SaveConnectionsContainer(SavedConnectionsContainer container)
        {
            try
            {
                var json = JsonSerializer.Serialize(container, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                
                File.WriteAllText(_connectionsFilePath, json);
                Logger.Debug("Connections container saved to file");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to save connections file");
                throw;
            }
        }

        /// <summary>
        /// Get the file path where connections are stored
        /// </summary>
        public string GetConnectionsFilePath()
        {
            return _connectionsFilePath;
        }
    }
}

