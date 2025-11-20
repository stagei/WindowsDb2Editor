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

namespace WindowsDb2Editor.Dialogs;

public partial class PackageDetailsDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly DB2ConnectionManager _connectionManager;
    private readonly PackageInfo _package;
    private readonly List<PackageStatement> _statements = new();

    // Event to communicate with parent window
    public event EventHandler<string>? SqlTextRequested;

    public PackageDetailsDialog(DB2ConnectionManager connectionManager, PackageInfo package)
    {
        InitializeComponent();
        _connectionManager = connectionManager;
        _package = package;

        Logger.Debug("PackageDetailsDialog opened for: {Schema}.{Package}", package.PackageSchema, package.PackageName);

        PackageNameText.Text = package.PackageName;
        PackageInfoText.Text = $"Schema: {package.PackageSchema} • Full Name: {package.PackageSchema}.{package.PackageName}";

        LoadPackageDetails();
        _ = LoadStatementsAsync();
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
                    TRIM(STMTNO) AS StmtNo,
                    TRIM(SECTNO) AS SectionNo,
                    TRIM(SEQNO) AS SeqNo,
                    TRIM(TEXT) AS StatementText
                FROM SYSCAT.STATEMENTS
                WHERE TRIM(PKGSCHEMA) = ? AND TRIM(PKGNAME) = ?
                ORDER BY STMTNO, SECTNO, SEQNO";

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

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
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

