using System.Text.Json.Serialization;

namespace WindowsDb2EditorTray.Models;

/// <summary>
/// Simplified connection profile model for tray icon
/// </summary>
public class ConnectionProfile
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("server")]
    public string Server { get; set; } = string.Empty;

    [JsonPropertyName("port")]
    public int Port { get; set; } = 50000;

    [JsonPropertyName("database")]
    public string Database { get; set; } = string.Empty;

    [JsonPropertyName("providerType")]
    public string ProviderType { get; set; } = "db2";
}
