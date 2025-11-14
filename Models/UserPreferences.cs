using System.Text.Json.Serialization;

namespace WindowsDb2Editor.Models
{
    /// <summary>
    /// User preferences stored in preferences.json
    /// </summary>
    public class UserPreferences
    {
        /// <summary>
        /// Maximum number of rows to fetch per query (default: 1000)
        /// </summary>
        [JsonPropertyName("maxRowsPerQuery")]
        public int MaxRowsPerQuery { get; set; } = 1000;

        /// <summary>
        /// Default theme (System, Dark, or Light)
        /// </summary>
        [JsonPropertyName("defaultTheme")]
        public string DefaultTheme { get; set; } = "Dark";

        /// <summary>
        /// SQL editor font family
        /// </summary>
        [JsonPropertyName("fontFamily")]
        public string FontFamily { get; set; } = "Consolas";

        /// <summary>
        /// SQL editor font size
        /// </summary>
        [JsonPropertyName("fontSize")]
        public int FontSize { get; set; } = 14;

        /// <summary>
        /// Tab size in spaces
        /// </summary>
        [JsonPropertyName("tabSize")]
        public int TabSize { get; set; } = 4;

        /// <summary>
        /// Auto-format SQL on execute
        /// </summary>
        [JsonPropertyName("autoFormatOnExecute")]
        public bool AutoFormatOnExecute { get; set; } = false;

        /// <summary>
        /// Command timeout in seconds (default: 30, 0 = no timeout)
        /// </summary>
        [JsonPropertyName("commandTimeout")]
        public int CommandTimeout { get; set; } = 30;

        /// <summary>
        /// Handle DB2Decimal errors gracefully
        /// </summary>
        [JsonPropertyName("handleDecimalErrorsGracefully")]
        public bool HandleDecimalErrorsGracefully { get; set; } = true;

        /// <summary>
        /// Auto-refresh database objects on connection
        /// </summary>
        [JsonPropertyName("autoRefreshObjectsOnConnect")]
        public bool AutoRefreshObjectsOnConnect { get; set; } = true;

        /// <summary>
        /// Log level (Debug, Info, Warn, Error)
        /// </summary>
        [JsonPropertyName("logLevel")]
        public string LogLevel { get; set; } = "Info";
    }
}

