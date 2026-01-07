using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
    
    private readonly IConnectionManager _connectionManager;
    private readonly MermaidDiagramGeneratorService _generatorService;
    private readonly SchemaDiffAnalyzerService _diffAnalyzer;
    private readonly DiffBasedDdlGeneratorService _ddlGenerator;
    private readonly SqlMermaidIntegrationService _sqlMermaidService;
    private string _targetSchema;
    private string? _lastGeneratedMermaid;
    private List<string>? _lastSelectedTables;
    
    // Public properties for testing automation
    public bool IsDesignerLoaded { get; private set; }
    public string? LastGeneratedMermaid => _lastGeneratedMermaid;
    public List<string>? LastSelectedTables => _lastSelectedTables;
    public string TargetSchema => _targetSchema;
    
    public MermaidDesignerWindow(IConnectionManager connectionManager, string targetSchema)
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
            Logger.Info("Initializing Mermaid Designer WebView2 for schema: {Schema}", _targetSchema);
            Logger.Debug("MermaidDesignerWindow_Loaded event fired");
            Logger.Debug("BaseDirectory: {BaseDir}", AppDomain.CurrentDomain.BaseDirectory);
            
            Logger.Debug("Step 1: Ensuring WebView2 runtime");
            await MermaidWebView.EnsureCoreWebView2Async();
            Logger.Info("WebView2 runtime initialized successfully");
            
            Logger.Debug("Step 2: Registering web message handler");
            MermaidWebView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
            Logger.Debug("Web message handler registered");
            
            Logger.Debug("Step 3: Locating MermaidDesigner.html");
            var htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "MermaidDesigner.html");
            Logger.Debug("Expected HTML path: {HtmlPath}", htmlPath);
            
            if (!File.Exists(htmlPath))
            {
                Logger.Error("MermaidDesigner.html not found at: {Path}", htmlPath);
                Logger.Debug("Checking if Resources directory exists...");
                
                var resourcesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
                if (Directory.Exists(resourcesDir))
                {
                    Logger.Debug("Resources directory exists. Contents:");
                    var files = Directory.GetFiles(resourcesDir);
                    foreach (var file in files)
                    {
                        Logger.Debug("  - {File}", Path.GetFileName(file));
                    }
                }
                else
                {
                    Logger.Error("Resources directory does not exist: {Dir}", resourcesDir);
                }
                
                MessageBox.Show(
                    $"MermaidDesigner.html not found at:\n{htmlPath}\n\nPlease ensure it exists in Resources folder.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Close();
                return;
            }
            
            Logger.Info("MermaidDesigner.html found, loading into WebView2");
            Logger.Debug("File size: {Size} bytes", new FileInfo(htmlPath).Length);
            
            Logger.Debug("Step 4: Setting WebView2 source");
            MermaidWebView.Source = new Uri(htmlPath);
            Logger.Debug("WebView2 source set to: {Uri}", htmlPath);
            
            IsDesignerLoaded = true;
            Logger.Info("Mermaid Designer initialized successfully for schema: {Schema}", _targetSchema);
        }
        catch (Exception ex)
        {
            IsDesignerLoaded = false;
            Logger.Error(ex, "Failed to initialize Mermaid Designer");
            Logger.Debug("Exception type: {Type}", ex.GetType().Name);
            Logger.Debug("Exception message: {Message}", ex.Message);
            Logger.Debug("Stack trace: {StackTrace}", ex.StackTrace);
            
            if (ex.InnerException != null)
            {
                Logger.Error(ex.InnerException, "Inner exception details");
            }
            
            MessageBox.Show(
                $"Failed to initialize Mermaid Designer:\n\n{ex.Message}\n\nSee logs for details.",
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
            Logger.Debug("WebMessage received from JavaScript");
            var json = e.WebMessageAsJson;
            Logger.Debug("WebMessage JSON: {Json}", json?.Substring(0, Math.Min(200, json?.Length ?? 0)));
            
            if (string.IsNullOrEmpty(json))
            {
                Logger.Warn("WebMessage JSON is null or empty");
                return;
            }
            
            var message = JsonSerializer.Deserialize<WebMessage>(json);
            
            if (message == null)
            {
                Logger.Warn("WebMessage deserialized to null");
                return;
            }
            
            Logger.Info("Processing web message action: {Action}", message.Action);
            Logger.Debug("Message details - Action: {Action}, TableName: {Table}, Diagram length: {DiagramLen}, Original length: {OrigLen}, Edited length: {EditLen}",
                message.Action, message.TableName, message.Diagram?.Length ?? 0, message.Original?.Length ?? 0, message.Edited?.Length ?? 0);
            
            switch (message.Action)
            {
                case "generateFromDB":
                    Logger.Debug("Handling generateFromDB action");
                    await HandleGenerateFromDB();
                    break;
                    
                case "analyzeDiff":
                    Logger.Debug("Handling analyzeDiff action");
                    await HandleAnalyzeDiff(message.Original, message.Edited);
                    break;
                    
                case "generateDDL":
                    Logger.Debug("Handling generateDDL action");
                    await HandleGenerateDDL(message.Original, message.Edited);
                    break;
                    
                case "exportDiagram":
                    Logger.Debug("Handling exportDiagram action");
                    HandleExportDiagram(message.Diagram);
                    break;
                    
                case "openTableProperties":
                    Logger.Debug("Handling openTableProperties action for table: {Table}", message.TableName);
                    HandleOpenTableProperties(message.TableName);
                    break;
                    
                case "generateSqlFromMermaid":
                    Logger.Debug("Handling generateSqlFromMermaid action");
                    await HandleGenerateSqlFromMermaid(message.Diagram, message.Dialect);
                    break;
                    
                case "translateSqlDialect":
                    Logger.Debug("Handling translateSqlDialect action");
                    await HandleTranslateSqlDialect(message.SourceSql, message.SourceDialect, message.TargetDialect);
                    break;
                    
                case "generateMigrationAdvanced":
                    Logger.Debug("Handling generateMigrationAdvanced action");
                    await HandleGenerateMigrationAdvanced(message.Original, message.Edited, message.Dialect);
                    break;
                    
                default:
                    Logger.Warn("Unknown web message action: {Action}", message.Action);
                    break;
            }
            
            Logger.Debug("Web message action completed: {Action}", message.Action);
        }
        catch (JsonException jsonEx)
        {
            Logger.Error(jsonEx, "Failed to deserialize web message JSON");
            Logger.Debug("Raw JSON: {Json}", e.WebMessageAsJson);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error handling web message");
            Logger.Debug("Exception type: {Type}", ex.GetType().Name);
            Logger.Debug("Stack trace: {StackTrace}", ex.StackTrace);
            
            MessageBox.Show(
                $"Error processing web message: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
    
    private async Task HandleGenerateFromDB()
    {
        try
        {
            Logger.Info("Generating Mermaid diagram from database - Target schema: {Schema}", _targetSchema);
            Logger.Debug("Step 1: Opening SchemaTableSelectionDialog");
            
            var dialog = new SchemaTableSelectionDialog(_connectionManager, _targetSchema);
            Logger.Debug("SchemaTableSelectionDialog created");
            
            var dialogResult = dialog.ShowDialog();
            Logger.Debug("SchemaTableSelectionDialog closed - Result: {Result}", dialogResult);
            
            if (dialogResult != true)
            {
                Logger.Info("User cancelled table selection");
                return;
            }
            
            var selectedTables = dialog.SelectedTables;
            Logger.Info("User selected {Count} tables", selectedTables.Count);
            
            if (selectedTables.Count == 0)
            {
                Logger.Warn("No tables selected by user");
                MessageBox.Show("No tables selected.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            Logger.Debug("Step 2: Generating Mermaid diagram for {Count} tables", selectedTables.Count);
            foreach (var table in selectedTables)
            {
                Logger.Debug("  - Selected table: {Table}", table);
            }
            
            var mermaid = await _generatorService.GenerateMermaidDiagramAsync(_connectionManager, selectedTables);
            Logger.Info("Mermaid diagram generated: {Length} characters", mermaid.Length);
            Logger.Debug("First 200 chars of Mermaid: {Preview}", mermaid.Substring(0, Math.Min(200, mermaid.Length)));
            
            _lastGeneratedMermaid = mermaid;
            _lastSelectedTables = selectedTables;
            
            Logger.Debug("Step 3: Injecting diagram into WebView2 editor");
            var escapedMermaid = EscapeForJavaScript(mermaid);
            Logger.Debug("Escaped Mermaid length: {Length}", escapedMermaid.Length);
            
            var script = $"setEditorContent(`{escapedMermaid}`);";
            Logger.Debug("Executing JavaScript: setEditorContent (length: {Length})", script.Length);
            
            await MermaidWebView.ExecuteScriptAsync(script);
            Logger.Debug("JavaScript executed successfully");
            
            Logger.Info("Mermaid diagram generated and displayed successfully ({Length} chars for {Count} tables)", 
                mermaid.Length, selectedTables.Count);
        }
        catch (InvalidOperationException invOpEx)
        {
            Logger.Error(invOpEx, "Invalid operation during diagram generation");
            Logger.Debug("InvalidOperationException details: {Message}", invOpEx.Message);
            MessageBox.Show(
                $"Invalid operation: {invOpEx.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate Mermaid diagram");
            Logger.Debug("Exception type: {Type}", ex.GetType().Name);
            Logger.Debug("Exception message: {Message}", ex.Message);
            Logger.Debug("Stack trace: {StackTrace}", ex.StackTrace);
            
            if (ex.InnerException != null)
            {
                Logger.Error(ex.InnerException, "Inner exception during diagram generation");
            }
            
            MessageBox.Show(
                $"Failed to generate diagram:\n\n{ex.Message}\n\nSee logs for details.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
    
    /// <summary>
    /// Programmatically generate Mermaid diagram for testing automation.
    /// </summary>
    public async Task<string> GenerateDiagramAutomatedAsync(List<string> tableNames)
    {
        Logger.Info("Automated diagram generation for {Count} tables", tableNames.Count);
        Logger.Debug("Tables requested:");
        foreach (var table in tableNames)
        {
            Logger.Debug("  - {Table}", table);
        }
        
        try
        {
            Logger.Debug("Step 1: Calling MermaidDiagramGeneratorService.GenerateMermaidDiagramAsync");
            var mermaid = await _generatorService.GenerateMermaidDiagramAsync(_connectionManager, tableNames);
            Logger.Info("Diagram generated: {Length} characters", mermaid.Length);
            Logger.Debug("Mermaid preview (first 300 chars): {Preview}", mermaid.Substring(0, Math.Min(300, mermaid.Length)));
            
            _lastGeneratedMermaid = mermaid;
            _lastSelectedTables = tableNames;
            
            Logger.Debug("Step 2: Setting WebView2 editor content");
            await MermaidWebView.ExecuteScriptAsync($"setEditorContent(`{EscapeForJavaScript(mermaid)}`);");
            Logger.Debug("Editor content set successfully");
            
            Logger.Info("Automated diagram generated successfully: {Length} chars", mermaid.Length);
            return mermaid;
        }
        catch (IBM.Data.Db2.DB2Exception db2Ex)
        {
            Logger.Error(db2Ex, "DB2 error during automated diagram generation - SqlState: {SqlState}, ErrorCode: {ErrorCode}", 
                db2Ex.SqlState, db2Ex.ErrorCode);
            Logger.Debug("DB2 Exception details: {Details}", db2Ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Automated diagram generation failed");
            Logger.Debug("Exception type: {Type}", ex.GetType().Name);
            Logger.Debug("Exception message: {Message}", ex.Message);
            Logger.Debug("Stack trace: {StackTrace}", ex.StackTrace);
            throw;
        }
    }
    
    /// <summary>
    /// Extract data for testing automation.
    /// </summary>
    public object ExtractDataForTesting()
    {
        Logger.Info("Extracting Mermaid Designer data for testing automation");
        Logger.Debug("Target Schema: {Schema}", _targetSchema);
        Logger.Debug("IsDesignerLoaded: {Loaded}", IsDesignerLoaded);
        Logger.Debug("HasDiagram: {HasDiagram}", !string.IsNullOrEmpty(_lastGeneratedMermaid));
        Logger.Debug("MermaidLength: {Length}", _lastGeneratedMermaid?.Length ?? 0);
        Logger.Debug("SelectedTablesCount: {Count}", _lastSelectedTables?.Count ?? 0);
        
        var extractedData = new
        {
            TargetSchema = _targetSchema,
            IsLoaded = IsDesignerLoaded,
            LastGeneratedMermaid = _lastGeneratedMermaid,
            MermaidLength = _lastGeneratedMermaid?.Length ?? 0,
            SelectedTablesCount = _lastSelectedTables?.Count ?? 0,
            SelectedTables = _lastSelectedTables ?? new List<string>(),
            HasDiagram = !string.IsNullOrEmpty(_lastGeneratedMermaid)
        };
        
        Logger.Info("Data extraction complete - Schema: {Schema}, Diagram: {HasDiagram}, Tables: {Count}", 
            _targetSchema, extractedData.HasDiagram, extractedData.SelectedTablesCount);
        
        return extractedData;
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
    /// ENHANCED: Generates ALTER statements and opens review dialog for safe execution.
    /// Uses SqlMermaidErdTools for migration DDL generation from Mermaid diff.
    /// </summary>
    private async Task HandleGenerateDDL(string? original, string? edited)
    {
        try
        {
            if (string.IsNullOrEmpty(original) || string.IsNullOrEmpty(edited))
            {
                MessageBox.Show(
                    "No changes to analyze.\n\n" +
                    "1. Click 'Show Diff' to capture baseline\n" +
                    "2. Modify the Mermaid diagram\n" +
                    "3. Click 'Show Diff' again to see changes\n" +
                    "4. Click 'Generate DDL' to create ALTER statements",
                    "Info", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
                return;
            }
            
            Logger.Info("Generating ALTER statements from Mermaid diff using SqlMermaidErdTools");
            
            // Try SqlMermaidErdTools first for advanced migration
            string ddl;
            try
            {
                ddl = await _sqlMermaidService.GenerateMigrationFromMermaidDiffAsync(
                    original,
                    edited,
                    SqlMermaidErdTools.Models.SqlDialect.AnsiSql);
                
                Logger.Info("SqlMermaidErdTools generated migration: {Length} chars", ddl.Length);
            }
            catch (Exception sqlMermaidEx)
            {
                Logger.Warn(sqlMermaidEx, "SqlMermaidErdTools failed, using fallback DiffBasedDdlGenerator");
                
                // Fallback to legacy DiffBasedDdlGenerator
                ddl = await _ddlGenerator.GenerateMigrationScriptsAsync(
                    original,
                    edited,
                    _targetSchema,
                    SqlMermaidErdTools.Models.SqlDialect.AnsiSql);
            }
            
            // Check if migration has content
            if (string.IsNullOrWhiteSpace(ddl) || ddl.Contains("-- No changes detected"))
            {
                MessageBox.Show(
                    "No schema changes detected.\n\n" +
                    "The original and modified diagrams appear to be identical.",
                    "No Changes", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
                return;
            }
            
            // Parse DDL into individual ALTER statements
            var alterStatements = ParseDdlIntoStatements(ddl);
            
            if (alterStatements.Count == 0)
            {
                MessageBox.Show(
                    "Generated DDL contains no executable statements.",
                    "No Statements",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            
            Logger.Info("Parsed {Count} ALTER statements from DDL", alterStatements.Count);
            
            // Open ALTER statement review dialog
            var reviewDialog = new AlterStatementReviewDialog(alterStatements, _connectionManager);
            reviewDialog.Owner = this;
            
            var dialogResult = reviewDialog.ShowDialog();
            
            if (dialogResult == true && reviewDialog.StatementsExecuted)
            {
                Logger.Info("User executed {Count} ALTER statements successfully", reviewDialog.ExecutedCount);
                
                // Ask if user wants to reload diagram from DB to see changes
                var reloadResult = MessageBox.Show(
                    $"Successfully executed {reviewDialog.ExecutedCount} statement(s).\n\n" +
                    $"Reload diagram from database to see the changes?",
                    "Reload Diagram?",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                
                if (reloadResult == MessageBoxResult.Yes && _lastSelectedTables != null)
                {
                    // Reload diagram from same tables
                    var mermaid = await _generatorService.GenerateMermaidDiagramAsync(
                        _connectionManager,
                        _lastSelectedTables);
                    
                    _lastGeneratedMermaid = mermaid;
                    var escapedMermaid = EscapeForJavaScript(mermaid);
                    await MermaidWebView.ExecuteScriptAsync($"setEditorContent(`{escapedMermaid}`);");
                    
                    Logger.Info("Diagram reloaded from database successfully");
                }
            }
            else
            {
                Logger.Info("User closed ALTER review dialog without executing statements");
            }
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
    
    /// <summary>
    /// Parse SQL DDL into individual executable statements.
    /// Splits by semicolon and filters out comments.
    /// </summary>
    private List<string> ParseDdlIntoStatements(string ddl)
    {
        var statements = new List<string>();
        
        // Split by semicolon but preserve statements
        var lines = ddl.Split('\n');
        var currentStatement = new StringBuilder();
        
        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            
            // Skip comments and empty lines
            if (trimmedLine.StartsWith("--") || string.IsNullOrWhiteSpace(trimmedLine))
            {
                continue;
            }
            
            currentStatement.AppendLine(line);
            
            // If line ends with semicolon, statement is complete
            if (trimmedLine.EndsWith(";"))
            {
                var statement = currentStatement.ToString().Trim();
                
                if (!string.IsNullOrWhiteSpace(statement))
                {
                    statements.Add(statement);
                }
                
                currentStatement.Clear();
            }
        }
        
        // Add any remaining statement
        if (currentStatement.Length > 0)
        {
            var statement = currentStatement.ToString().Trim();
            if (!string.IsNullOrWhiteSpace(statement))
            {
                statements.Add(statement);
            }
        }
        
        return statements;
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

