using System.Text.Json.Serialization;

namespace WindowsDb2Editor.Models
{
    /// <summary>
    /// Represents a saved connection with encrypted password
    /// </summary>
    public class SavedConnection
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("provider")]
        public string Provider { get; set; } = "DB2";

        [JsonPropertyName("version")]
        public string Version { get; set; } = "12.1";

        [JsonPropertyName("server")]
        public string Server { get; set; } = string.Empty;

        [JsonPropertyName("port")]
        public int Port { get; set; } = 50000;

        [JsonPropertyName("database")]
        public string Database { get; set; } = string.Empty;

        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Encrypted password stored as Base64 string
        /// </summary>
        [JsonPropertyName("encryptedPassword")]
        public string EncryptedPassword { get; set; } = string.Empty;

        [JsonPropertyName("lastUsed")]
        public DateTime LastUsed { get; set; } = DateTime.Now;

        /// <summary>
        /// Convert to DB2Connection with decrypted password
        /// </summary>
        public DB2Connection ToDb2Connection(string decryptedPassword)
        {
            return new DB2Connection
            {
                Name = this.Name,
                Server = this.Server,
                Port = this.Port,
                Database = this.Database,
                Username = this.Username,
                Password = decryptedPassword
            };
        }
    }

    /// <summary>
    /// Container for saved connections
    /// </summary>
    public class SavedConnectionsContainer
    {
        [JsonPropertyName("connections")]
        public List<SavedConnection> Connections { get; set; } = new List<SavedConnection>();
    }
}

