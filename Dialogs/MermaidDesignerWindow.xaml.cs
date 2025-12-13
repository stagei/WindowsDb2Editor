using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Text.Json;
using Microsoft.Win32;
using Microsoft.Web.WebView2.Core;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.Dialogs;

public partial class MermaidDesignerWindow : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    private readonly DB2ConnectionManager _connectionManager;
    private readonly MermaidDiagramGeneratorService _generatorService;
    private readonly SchemaDiffAnalyzerService _diffAnalyzer;
    private readonly DiffBasedDdlGeneratorService _ddlGenerator;
    private readonly SqlMermaidIntegrationService _sqlMermaidService;
    private string _targetSchema;
    
    public MermaidDesignerWindow(DB2ConnectionManager connectionManager, string targetSchema)
    {
        InitializeComponent();
        
        _connectionManager = connectionManager;
        _targetSchema = targetSchema;
        _generatorService = new MermaidDiagramGeneratorService();
        _diffAnalyzer = new SchemaDiffAnalyzerService();
        _ddlGenerator = new DiffBasedDdlGeneratorService();
        _sqlMermaidService = new SqlMermaidIntegrationService();
        
        Loaded += MermaidDesignerWindow_Loaded;
    }
    
    private async void MermaidDesignerWindow_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            Logger.Info("Initializing Mermaid Designer WebView2");
            
            await MermaidWebView.EnsureCoreWebView2Async();
            
            MermaidWebView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
            
            var htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "MermaidDesigner.html");
            
            if (!File.Exists(htmlPath))
            {
                Logger.Error("MermaidDesigner.html not found at: {Path}", htmlPath);
                MessageBox.Show(
                    "MermaidDesigner.html not found. Please ensure it exists in Resources folder.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Close();
                return;
            }
            
            MermaidWebView.Source = new Uri(htmlPath);
            
            Logger.Info("Mermaid Designer initialized successfully");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to initialize Mermaid Designer");
            MessageBox.Show(
                $"Failed to initialize Mermaid Designer: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            Close();
        }
    }
    
    private async void CoreWebView2_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
    {
        try
        {
            var json = e.WebMessageAsJson;
            var message = JsonSerializer.Deserialize<WebMessage>(json);
            
            if (message == null) return;
            
            Logger.Debug("Received web message: {Action}", message.Action);
            
            switch (message.Action)
            {
                case "generateFromDB":
                    await HandleGenerateFromDB();
                    break;
                    
                case "analyzeDiff":
                    await HandleAnalyzeDiff(message.Original, message.Edited);
                    break;
                    
                case "generateDDL":
                    await HandleGenerateDDL(message.Original, message.Edited);
                    break;
                    
                case "exportDiagram":
                    HandleExportDiagram(message.Diagram);
                    break;
                    
                case "openTableProperties":
                    HandleOpenTableProperties(message.TableName);
                    break;
                    
                case "generateSqlFromMermaid":
                    await HandleGenerateSqlFromMermaid(message.Diagram, message.Dialect);
                    break;
                    
                case "translateSqlDialect":
                    await HandleTranslateSqlDialect(message.SourceSql, message.SourceDialect, message.TargetDialect);
                    break;
                    
                case "generateMigrationAdvanced":
                    await HandleGenerateMigrationAdvanced(message.Original, message.Edited, message.Dialect);
                    break;
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error handling web message");
        }
    }
    
    private async Task HandleGenerateFromDB()
    {
        try
        {
            Logger.Info("Generating Mermaid diagram from database");
            
            var dialog = new SchemaTableSelectionDialog(_connectionManager, _targetSchema);
            if (dialog.ShowDialog() != true)
                return;
            
            var selectedTables = dialog.SelectedTables;
            if (selectedTables.Count == 0)
            {
                MessageBox.Show("No tables selected.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            var mermaid = await _generatorService.GenerateMermaidDiagramAsync(_connectionManager, selectedTables);
            
            await MermaidWebView.ExecuteScriptAsync($"setEditorContent(`{EscapeForJavaScript(mermaid)}`);");
            
            Logger.Info("Mermaid diagram generated successfully");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate Mermaid diagram");
            MessageBox.Show(
                $"Failed to generate diagram: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
    
    private async Task HandleAnalyzeDiff(string? original, string? edited)
    {
        try
        {
            if (string.IsNullOrEmpty(original) || string.IsNullOrEmpty(edited))
            {
                Logger.Warn("Original or edited diagram is empty");
                return;
            }
            
            Logger.Info("Analyzing schema differences");
            
            var diff = _diffAnalyzer.AnalyzeDifferences(original, edited);
            
            var diffJson = JsonSerializer.Serialize(diff, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            await MermaidWebView.ExecuteScriptAsync($"displayDiff({diffJson});");
            
            Logger.Info("Diff analysis complete: {Changes} changes", diff.TotalChanges);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to analyze diff");
        }
    }
    
    /// <summary>
    /// REFACTORED: Now uses SqlMermaidErdTools for migration DDL generation.
    /// Generates migration scripts directly from Mermaid diagrams.
    /// </summary>
    private async Task HandleGenerateDDL(string? original, string? edited)
    {
        try
        {
            if (string.IsNullOrEmpty(original) || string.IsNullOrEmpty(edited))
            {
                MessageBox.Show("Please capture original version first using 'Show Diff' button.", 
                    "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            Logger.Info("Generating DDL from diff using SqlMermaidErdTools");
            
            // Use SqlMermaidErdTools for migration generation (ANSI SQL by default)
            var ddl = await _ddlGenerator.GenerateMigrationScriptsAsync(
                original,
                edited,
                _targetSchema,
                SqlMermaidErdTools.Models.SqlDialect.AnsiSql);
            
            // Check if migration has content
            if (string.IsNullOrWhiteSpace(ddl) || ddl.Contains("-- No changes detected"))
            {
                MessageBox.Show("No changes detected. DDL not generated.", 
                    "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            var tempFile = Path.Combine(Path.GetTempPath(), $"migration_{DateTime.Now:yyyyMMddHHmmss}.sql");
            await File.WriteAllTextAsync(tempFile, ddl);
            
            var result = MessageBox.Show(
                $"Migration script generated using SqlMermaidErdTools.\n\n" +
                $"Open in SQL editor for review?\n\n" +
                "WARNING: Review carefully before executing!",
                "DDL Generated",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start("notepad.exe", tempFile);
            }
            
            Logger.Info("DDL generation complete via SqlMermaidErdTools: {File}", tempFile);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate DDL via SqlMermaidErdTools");
            MessageBox.Show(
                $"Failed to generate DDL: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
    
    private void HandleExportDiagram(string? diagram)
    {
        try
        {
            if (string.IsNullOrEmpty(diagram))
            {
                Logger.Warn("Diagram is empty");
                return;
            }
            
            var saveDialog = new SaveFileDialog
            {
                Title = "Export Mermaid Diagram",
                Filter = "Mermaid Files (*.mmd)|*.mmd|Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                DefaultExt = ".mmd",
                FileName = $"diagram_{DateTime.Now:yyyyMMdd_HHmmss}.mmd"
            };
            
            if (saveDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveDialog.FileName, diagram);
                Logger.Info("Diagram exported to: {File}", saveDialog.FileName);
                MessageBox.Show(
                    $"Diagram exported successfully to:\n{saveDialog.FileName}",
                    "Export Complete",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to export diagram");
            MessageBox.Show(
                $"Failed to export diagram: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
    
    private void HandleOpenTableProperties(string? tableName)
    {
        try
        {
            if (string.IsNullOrEmpty(tableName))
            {
                Logger.Warn("Table name is empty");
                return;
            }
            
            Logger.Info("Opening table properties for: {Table}", tableName);
            
            var fullTableName = tableName.Contains(".") ? tableName : $"{_targetSchema}.{tableName}";
            var dialog = new TableDetailsDialog(_connectionManager, fullTableName);
            dialog.Owner = this;
            dialog.ShowDialog();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to open table properties for: {Table}", tableName);
            MessageBox.Show(
                $"Failed to open table properties: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
    
    private string EscapeForJavaScript(string text)
    {
        return text
            .Replace("\\", "\\\\")
            .Replace("`", "\\`")
            .Replace("$", "\\$")
            .Replace("\r", "\\r")
            .Replace("\n", "\\n");
    }
    
    /// <summary>
    /// NEW: Generates SQL DDL from Mermaid ERD using SqlMermaidErdTools.
    /// Supports multiple SQL dialects (ANSI, SQL Server, PostgreSQL, MySQL).
    /// </summary>
    private async Task HandleGenerateSqlFromMermaid(string? mermaidDiagram, string? dialectName)
    {
        try
        {
            if (string.IsNullOrEmpty(mermaidDiagram))
            {
                MessageBox.Show("Mermaid diagram is empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            Logger.Info("Generating SQL DDL from Mermaid - Dialect: {Dialect}", dialectName ?? "AnsiSql");
            
            // Parse dialect (default to ANSI SQL)
            var dialect = ParseSqlDialect(dialectName);
            
            // Convert Mermaid to SQL
            var sql = await _sqlMermaidService.ConvertMermaidToSqlAsync(mermaidDiagram, dialect);
            
            // Send result back to JavaScript
            await MermaidWebView.ExecuteScriptAsync($"showGeneratedSql(`{EscapeForJavaScript(sql)}`, '{dialect}');");
            
            Logger.Info("SQL DDL generated successfully - {Length} characters", sql.Length);
            MessageBox.Show(
                $"SQL DDL generated successfully!\n\nDialect: {dialect}\nLength: {sql.Length} characters",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate SQL from Mermaid");
            MessageBox.Show(
                $"Failed to generate SQL DDL:\n\n{ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
    
    /// <summary>
    /// NEW: Translates SQL from one dialect to another using SqlMermaidErdTools.
    /// Example: DB2 SQL → PostgreSQL, SQL Server → MySQL, etc.
    /// </summary>
    private async Task HandleTranslateSqlDialect(string? sourceSql, string? sourceDialectName, string? targetDialectName)
    {
        try
        {
            if (string.IsNullOrEmpty(sourceSql))
            {
                MessageBox.Show("Source SQL is empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            var sourceDialect = ParseSqlDialect(sourceDialectName);
            var targetDialect = ParseSqlDialect(targetDialectName);
            
            Logger.Info("Translating SQL: {Source} → {Target}", sourceDialect, targetDialect);
            
            // Translate SQL
            var translatedSql = await _sqlMermaidService.TranslateSqlDialectAsync(sourceSql, sourceDialect, targetDialect);
            
            // Send result back to JavaScript
            await MermaidWebView.ExecuteScriptAsync($"showTranslatedSql(`{EscapeForJavaScript(translatedSql)}`, '{sourceDialect}', '{targetDialect}');");
            
            Logger.Info("SQL translation complete - {Length} characters", translatedSql.Length);
            MessageBox.Show(
                $"SQL translation successful!\n\nFrom: {sourceDialect}\nTo: {targetDialect}\nLength: {translatedSql.Length} characters",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to translate SQL");
            MessageBox.Show(
                $"Failed to translate SQL:\n\n{ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
    
    /// <summary>
    /// NEW: Generates migration DDL using advanced SqlMermaidErdTools diff algorithm.
    /// Produces ALTER statements for schema changes.
    /// </summary>
    private async Task HandleGenerateMigrationAdvanced(string? beforeMermaid, string? afterMermaid, string? dialectName)
    {
        try
        {
            if (string.IsNullOrEmpty(beforeMermaid) || string.IsNullOrEmpty(afterMermaid))
            {
                MessageBox.Show("Both before and after Mermaid diagrams are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            var dialect = ParseSqlDialect(dialectName);
            Logger.Info("Generating advanced migration DDL - Dialect: {Dialect}", dialect);
            
            // Generate migration scripts using SqlMermaidErdTools
            var migrationDdl = await _sqlMermaidService.GenerateMigrationFromMermaidDiffAsync(
                beforeMermaid,
                afterMermaid,
                dialect);
            
            // Send result back to JavaScript
            await MermaidWebView.ExecuteScriptAsync($"showMigrationDdl(`{EscapeForJavaScript(migrationDdl)}`, '{dialect}');");
            
            Logger.Info("Migration DDL generated - {Length} characters", migrationDdl.Length);
            MessageBox.Show(
                $"Migration DDL generated successfully!\n\nDialect: {dialect}\nStatements: {CountSqlStatements(migrationDdl)}",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate migration DDL");
            MessageBox.Show(
                $"Failed to generate migration DDL:\n\n{ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
    
    /// <summary>
    /// Parses SQL dialect name to SqlDialect enum.
    /// </summary>
    private SqlMermaidErdTools.Models.SqlDialect ParseSqlDialect(string? dialectName)
    {
        if (string.IsNullOrEmpty(dialectName))
            return SqlMermaidErdTools.Models.SqlDialect.AnsiSql;
        
        return dialectName.ToUpperInvariant() switch
        {
            "ANSI" or "ANSISQL" or "SQL" => SqlMermaidErdTools.Models.SqlDialect.AnsiSql,
            "SQLSERVER" or "TSQL" or "MSSQL" => SqlMermaidErdTools.Models.SqlDialect.SqlServer,
            "POSTGRESQL" or "POSTGRES" or "PG" => SqlMermaidErdTools.Models.SqlDialect.PostgreSql,
            "MYSQL" or "MARIADB" => SqlMermaidErdTools.Models.SqlDialect.MySql,
            _ => SqlMermaidErdTools.Models.SqlDialect.AnsiSql
        };
    }
    
    /// <summary>
    /// Counts SQL statements (semicolon-separated).
    /// </summary>
    private int CountSqlStatements(string sql)
    {
        return sql.Split(';', StringSplitOptions.RemoveEmptyEntries).Length;
    }
    
    private class WebMessage
    {
        public string Action { get; set; } = string.Empty;
        public string? Original { get; set; }
        public string? Edited { get; set; }
        public string? Diagram { get; set; }
        public string? TableName { get; set; }
        
        // NEW: Properties for SqlMermaidErdTools features
        public string? Dialect { get; set; }
        public string? SourceSql { get; set; }
        public string? SourceDialect { get; set; }
        public string? TargetDialect { get; set; }
    }
}

