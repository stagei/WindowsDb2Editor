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
    private string _targetSchema;
    
    public MermaidDesignerWindow(DB2ConnectionManager connectionManager, string targetSchema)
    {
        InitializeComponent();
        
        _connectionManager = connectionManager;
        _targetSchema = targetSchema;
        _generatorService = new MermaidDiagramGeneratorService();
        _diffAnalyzer = new SchemaDiffAnalyzerService();
        _ddlGenerator = new DiffBasedDdlGeneratorService();
        
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
            
            Logger.Info("Generating DDL from diff");
            
            var diff = _diffAnalyzer.AnalyzeDifferences(original, edited);
            
            if (!diff.HasChanges)
            {
                MessageBox.Show("No changes detected. DDL not generated.", 
                    "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            var ddl = _ddlGenerator.GenerateMigrationScripts(diff, _targetSchema);
            
            var tempFile = Path.Combine(Path.GetTempPath(), $"migration_{DateTime.Now:yyyyMMddHHmmss}.sql");
            await File.WriteAllTextAsync(tempFile, ddl);
            
            var result = MessageBox.Show(
                $"Migration script generated with {diff.TotalChanges} changes.\n\n" +
                $"Open in SQL editor for review?\n\n" +
                "WARNING: Review carefully before executing!",
                "DDL Generated",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start("notepad.exe", tempFile);
            }
            
            Logger.Info("DDL generation complete: {File}", tempFile);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate DDL");
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
    
    private class WebMessage
    {
        public string Action { get; set; } = string.Empty;
        public string? Original { get; set; }
        public string? Edited { get; set; }
        public string? Diagram { get; set; }
        public string? TableName { get; set; }
    }
}

