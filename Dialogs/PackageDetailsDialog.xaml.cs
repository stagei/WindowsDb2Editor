using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IBM.Data.Db2;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Models;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.Dialogs;

public partial class PackageDetailsDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly DB2ConnectionManager _connectionManager;
    private readonly PackageInfo _package;
    private readonly List<PackageStatement> _statements = new();

    // Event to communicate with parent window
    public event EventHandler<string>? SqlTextRequested;
    
    // Public accessors for GUI testing - allows GuiTestingService to extract form data
    public System.Windows.Controls.DataGrid StatementsGridPublic => StatementsGrid;
    public System.Windows.Controls.TextBlock PackageNameTextPublic => PackageNameText;
    public System.Windows.Controls.TextBlock PackageInfoTextPublic => PackageInfoText;
    public System.Windows.Controls.TextBlock StatementCountTextPublic => StatementCountText;

    public PackageDetailsDialog(DB2ConnectionManager connectionManager, PackageInfo package)
    {
        InitializeComponent();
        _connectionManager = connectionManager;
        _package = package;

        Logger.Debug("PackageDetailsDialog opened for: {Schema}.{Package}", package.PackageSchema, package.PackageName);

        PackageNameText.Text = package.PackageName;
        PackageInfoText.Text = $"Schema: {package.PackageSchema} • Full Name: {package.PackageSchema}.{package.PackageName}";

        // Wire up selection changed event for buttons
        StatementsGrid.SelectionChanged += (s, e) =>
        {
            var hasSelection = StatementsGrid.SelectedItem != null;
            ViewStatementButton.IsEnabled = hasSelection;
            AddToEditorButton.IsEnabled = hasSelection;
        };

        // Apply grid preferences to all grids in this dialog
        this.Loaded += (s, e) =>
        {
            if (App.PreferencesService != null)
            {
                GridStyleHelper.ApplyGridStylesToWindow(this, App.PreferencesService.Preferences);
            }
        };

        LoadPackageDetails();
        _ = LoadStatementsAsync();
        _ = LoadDependenciesAsync();
    }
    
    /// <summary>
    /// Activates a specific tab by name for automated testing and direct navigation
    /// </summary>
    public void ActivateTab(string? tabName)
    {
        if (string.IsNullOrEmpty(tabName))
            return;
            
        Logger.Debug("Activating tab: {Tab}", tabName);
        
        var tab = tabName.ToLowerInvariant() switch
        {
            "properties" or "props" => PropertiesTab,
            "statements" or "sql-statements" or "sql" => StatementsTab,
            "dependencies" or "deps" => DependenciesTab,
            _ => null
        };
        
        if (tab != null)
        {
            DetailsTabControl.SelectedItem = tab;
            Logger.Info("Activated tab: {Tab}", tabName);
        }
        else
        {
            Logger.Warn("Unknown tab name: {Tab}", tabName);
        }
    }

    private void LoadPackageDetails()
    {
        PackageNameValue.Text = _package.PackageName;
        PackageSchemaValue.Text = _package.PackageSchema;
        BoundByValue.Text = _package.BoundBy;
        OwnerValue.Text = _package.Owner;
        IsolationValue.Text = _package.Isolation;
        CreateTimeValue.Text = _package.CreateTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "N/A";
        RemarksValue.Text = string.IsNullOrEmpty(_package.Remarks) ? "No remarks" : _package.Remarks;
    }

    private async Task LoadStatementsAsync()
    {
        Logger.Info("Loading SQL statements for package: {Schema}.{Package}", _package.PackageSchema, _package.PackageName);

        try
        {
            // Query SYSCAT.STATEMENTS to get all SQL statements in this package
                var sql = @"
                    SELECT 
                        TRIM(S.STMTNO) AS StmtNo,
                        TRIM(S.SECTNO) AS SectionNo,
                        TRIM(S.SEQNO) AS SeqNo,
                        TRIM(S.TEXT) AS StatementText
                    FROM SYSCAT.STATEMENTS S
                    JOIN SYSCAT.PACKAGES P ON S.PKGSCHEMA = P.PKGSCHEMA AND S.PKGNAME = P.PKGNAME
                    WHERE TRIM(S.PKGSCHEMA) = ? AND TRIM(S.PKGNAME) = ?
                    ORDER BY S.STMTNO, S.SECTNO, S.SEQNO";

            using var command = _connectionManager.CreateCommand(sql);
            command.Parameters.Add(new DB2Parameter("@pkgschema", _package.PackageSchema));
            command.Parameters.Add(new DB2Parameter("@pkgname", _package.PackageName));

            using var reader = await command.ExecuteReaderAsync();

            var statementGroups = new Dictionary<string, List<string>>();

            while (await reader.ReadAsync())
            {
                var stmtNo = reader.GetString(0);
                var sectNo = reader.GetString(1);
                var seqNo = reader.GetString(2);
                var text = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);

                var key = $"{stmtNo}:{sectNo}";

                if (!statementGroups.ContainsKey(key))
                {
                    statementGroups[key] = new List<string>();
                }

                statementGroups[key].Add(text);
            }

            // Convert to PackageStatement objects
            foreach (var kvp in statementGroups.OrderBy(k => k.Key))
            {
                var parts = kvp.Key.Split(':');
                var stmtNo = parts[0];
                var sectNo = parts[1];
                var fullText = string.Join("", kvp.Value);

                // Determine statement type from the SQL text
                var stmtType = DetermineStatementType(fullText);

                // Create preview (first 100 characters)
                var preview = fullText.Length > 100 ? fullText.Substring(0, 100) + "..." : fullText;

                _statements.Add(new PackageStatement
                {
                    StmtNo = stmtNo,
                    SectionNumber = sectNo,
                    StmtType = stmtType,
                    TextPreview = preview.Replace('\n', ' ').Replace('\r', ' ').Trim(),
                    FullText = fullText
                });
            }

            Dispatcher.Invoke(() =>
            {
                StatementsGrid.ItemsSource = _statements;
                StatementCountText.Text = _statements.Count.ToString();
            });

            Logger.Info("Loaded {Count} SQL statements", _statements.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load package statements");
            Dispatcher.Invoke(() =>
            {
                StatementCountText.Text = "Error";
                MessageBox.Show($"Error loading package statements:\n\n{ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            });
        }
    }

    private string DetermineStatementType(string sql)
    {
        var upperSql = sql.TrimStart().ToUpperInvariant();

        if (upperSql.StartsWith("SELECT")) return "SELECT";
        if (upperSql.StartsWith("INSERT")) return "INSERT";
        if (upperSql.StartsWith("UPDATE")) return "UPDATE";
        if (upperSql.StartsWith("DELETE")) return "DELETE";
        if (upperSql.StartsWith("CALL")) return "CALL";
        if (upperSql.StartsWith("MERGE")) return "MERGE";
        if (upperSql.StartsWith("CREATE")) return "CREATE";
        if (upperSql.StartsWith("ALTER")) return "ALTER";
        if (upperSql.StartsWith("DROP")) return "DROP";
        if (upperSql.StartsWith("GRANT")) return "GRANT";
        if (upperSql.StartsWith("REVOKE")) return "REVOKE";

        return "OTHER";
    }

    private void ViewFullStatement_Click(object sender, RoutedEventArgs e)
    {
        if (StatementsGrid.SelectedItem is PackageStatement statement)
        {
            Logger.Debug("Viewing full statement: Stmt {StmtNo}, Section {Section}", statement.StmtNo, statement.SectionNumber);

            var viewerDialog = new SqlStatementViewerDialog(statement.FullText, 
                $"Statement {statement.StmtNo} - Section {statement.SectionNumber}")
            {
                Owner = this
            };

            if (viewerDialog.ShowDialog() == true && viewerDialog.AddToEditor)
            {
                // If user clicked "Add to Editor" in the viewer dialog
                SqlTextRequested?.Invoke(this, statement.FullText);
                Logger.Info("Statement added to editor from viewer dialog");
            }
        }
    }

    private void AddToEditor_Click(object sender, RoutedEventArgs e)
    {
        if (StatementsGrid.SelectedItem is PackageStatement statement)
        {
            Logger.Info("Adding statement to editor: Stmt {StmtNo}, Section {Section}", statement.StmtNo, statement.SectionNumber);
            SqlTextRequested?.Invoke(this, statement.FullText);
            MessageBox.Show("Statement added to SQL editor", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private void StatementsGrid_DoubleClick(object sender, MouseButtonEventArgs e)
    {
        // Double-click on a statement row opens the full viewer
        ViewFullStatement_Click(sender, new RoutedEventArgs());
    }
    
    /// <summary>
    /// Load package dependencies using PackageDependencyAnalyzer
    /// </summary>
    private async Task LoadDependenciesAsync()
    {
        Logger.Info("Loading dependencies for package: {Schema}.{Package}", _package.PackageSchema, _package.PackageName);
        
        try
        {
            TablesUsedCount.Text = "⏳ Analyzing dependencies...";
            ViewsUsedCount.Text = "⏳ Analyzing dependencies...";
            ProceduresCalledCount.Text = "⏳ Analyzing dependencies...";
            FunctionsCalledCount.Text = "⏳ Analyzing dependencies...";
            
            var analyzer = new PackageDependencyAnalyzer();
            var dependencies = await analyzer.AnalyzeDependenciesAsync(
                _connectionManager,
                _package.PackageSchema,
                _package.PackageName);
            
            // Populate Tables Used
            var tablesData = dependencies.TablesUsed.Select(t => new
            {
                t.Schema,
                t.Name,
                t.UsageCount,
                StatementsText = string.Join(", ", t.StatementsUsedIn.Take(10)),
                VerifiedText = t.Verified ? "✅ Yes" : "❌ No"
            }).ToList();
            
            TablesUsedGrid.ItemsSource = tablesData;
            TablesUsedCount.Text = $"📋 {tablesData.Count} table(s) used";
            
            // Populate Views Used
            var viewsData = dependencies.ViewsUsed.Select(v => new
            {
                v.Schema,
                v.Name,
                v.UsageCount,
                StatementsText = string.Join(", ", v.StatementsUsedIn.Take(10)),
                VerifiedText = v.Verified ? "✅ Yes" : "❌ No"
            }).ToList();
            
            ViewsUsedGrid.ItemsSource = viewsData;
            ViewsUsedCount.Text = $"👁️ {viewsData.Count} view(s) used";
            
            // Populate Procedures Called
            var proceduresData = dependencies.ProceduresCalled.Select(p => new
            {
                p.Schema,
                p.Name,
                p.UsageCount,
                StatementsText = string.Join(", ", p.StatementsUsedIn.Take(10)),
                VerifiedText = p.Verified ? "✅ Yes" : "❌ No"
            }).ToList();
            
            ProceduresCalledGrid.ItemsSource = proceduresData;
            ProceduresCalledCount.Text = $"⚙️ {proceduresData.Count} procedure(s) called";
            
            // Populate Functions Called
            var functionsData = dependencies.FunctionsCalled.Select(f => new
            {
                f.Schema,
                f.Name,
                f.UsageCount,
                StatementsText = string.Join(", ", f.StatementsUsedIn.Take(10)),
                VerifiedText = f.Verified ? "✅ Yes" : "❌ No"
            }).ToList();
            
            FunctionsCalledGrid.ItemsSource = functionsData;
            FunctionsCalledCount.Text = $"🔧 {functionsData.Count} function(s) called";
            
            Logger.Info("Dependencies loaded successfully: {Tables} tables, {Views} views, {Procs} procedures, {Funcs} functions",
                       tablesData.Count, viewsData.Count, proceduresData.Count, functionsData.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load package dependencies");
            TablesUsedCount.Text = "❌ Error loading dependencies";
            ViewsUsedCount.Text = "❌ Error loading dependencies";
            ProceduresCalledCount.Text = "❌ Error loading dependencies";
            FunctionsCalledCount.Text = "❌ Error loading dependencies";
            MessageBox.Show($"Failed to load dependencies: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    /// <summary>
    /// Handle double-click on dependency item (navigate to object)
    /// </summary>
    private void DependencyItem_DoubleClick(object sender, MouseButtonEventArgs e)
    {
        // Future enhancement: Navigate to table/view/procedure details
        Logger.Debug("Dependency item double-clicked");
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
    
    private void DockAsTab_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Docking PackageDetailsDialog as tab: {Schema}.{Package}", _package.PackageSchema, _package.PackageName);
        
        try
        {
            if (System.Windows.Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.CreateTabWithPackageDetails(_connectionManager, _package);
                Close();
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to dock as tab");
            MessageBox.Show($"Failed to dock as tab: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private void ExportAiContext_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("AI Context Export requires AI provider configuration.\n\nConfigure Ollama, OpenAI, Claude, or another AI provider in Settings to enable this feature.", 
            "Export AI Context", MessageBoxButton.OK, MessageBoxImage.Information);
    }
    
    private void AnalyzePackage_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("AI Package Analysis requires AI provider configuration.\n\nConfigure an AI provider in Settings to enable:\n• Package purpose explanation\n• Business logic analysis\n• Optimization suggestions\n• Dependency impact analysis", 
            "Analyze Package with AI", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}

/// <summary>
/// Model for package SQL statements
/// </summary>
public class PackageStatement
{
    public string StmtNo { get; set; } = string.Empty;
    public string SectionNumber { get; set; } = string.Empty;
    public string StmtType { get; set; } = string.Empty;
    public string TextPreview { get; set; } = string.Empty;
    public string FullText { get; set; } = string.Empty;
}

