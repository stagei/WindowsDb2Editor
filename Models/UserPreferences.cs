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
        /// Default theme (Dark or Light)
        /// </summary>
        [JsonPropertyName("defaultTheme")]
        public string DefaultTheme { get; set; } = "Dark";

        /// <summary>
        /// SQL editor font family
        /// </summary>
        [JsonPropertyName("editorFontFamily")]
        public string EditorFontFamily { get; set; } = "Consolas";

        /// <summary>
        /// SQL editor font size
        /// </summary>
        [JsonPropertyName("editorFontSize")]
        public double EditorFontSize { get; set; } = 14;

        /// <summary>
        /// Auto-format SQL on execute
        /// </summary>
        [JsonPropertyName("autoFormatOnExecute")]
        public bool AutoFormatOnExecute { get; set; } = false;

        /// <summary>
        /// Query timeout in seconds (default: 30)
        /// </summary>
        [JsonPropertyName("queryTimeoutSeconds")]
        public int QueryTimeoutSeconds { get; set; } = 30;

        /// <summary>
        /// Handle DB2Decimal errors gracefully
        /// </summary>
        [JsonPropertyName("handleDecimalErrorsGracefully")]
        public bool HandleDecimalErrorsGracefully { get; set; } = true;
    }
}

