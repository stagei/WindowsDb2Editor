using System.Data;
using System.Windows;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Dialogs;

public partial class TableDetailsDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly DB2ConnectionManager _connectionManager;
    private readonly string _fullTableName;
    private readonly string _schema;
    private readonly string _tableName;

    public string? SelectedRelatedTable { get; private set; }
    public bool ShouldQueryTable { get; private set; }

    public TableDetailsDialog(DB2ConnectionManager connectionManager, string fullTableName)
    {
        InitializeComponent();
        _connectionManager = connectionManager;
        _fullTableName = fullTableName;
        
        // Parse schema and table name
        var parts = fullTableName.Split('.');
        if (parts.Length == 2)
        {
            _schema = parts[0];
            _tableName = parts[1];
        }
        else
        {
            _schema = "";
            _tableName = fullTableName;
        }
        
        Logger.Debug("TableDetailsDialog opened for: {Table}", fullTableName);
        
        TableNameText.Text = _tableName;
        TableInfoText.Text = $"Schema: {_schema} • Full Name: {_fullTableName}";
        
        _ = LoadTableDetailsAsync();
    }

    private async Task LoadTableDetailsAsync()
    {
        Logger.Info("Loading details for table: {Table}", _fullTableName);
        
        try
        {
            // Load all details in parallel
            var columnsTask = LoadColumnsAsync();
            var foreignKeysTask = LoadForeignKeysAsync();
            var indexesTask = LoadIndexesAsync();
            var statsTask = LoadStatisticsAsync();
            var ddlTask = GenerateDDLAsync();
            
            await Task.WhenAll(columnsTask, foreignKeysTask, indexesTask, statsTask, ddlTask);
            
            Logger.Info("Table details loaded successfully");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load table details");
            MessageBox.Show($"Error loading table details:\n\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task LoadColumnsAsync()
    {
        Logger.Debug("Loading columns for: {Table}", _tableName);
        
        try
        {
            var sql = $@"
                SELECT 
                    COLNAME AS ColumnName,
                    TYPENAME AS DataType,
                    LENGTH,
                    CASE WHEN NULLS = 'Y' THEN 'Yes' ELSE 'No' END AS Nullable,
                    COALESCE(DEFAULT, '-') AS DefaultValue,
                    COALESCE(REMARKS, '') AS Remarks
                FROM SYSCAT.COLUMNS 
                WHERE TABSCHEMA = '{_schema}' AND TABNAME = '{_tableName}'
                ORDER BY COLNO";
            
            var dataTable = await _connectionManager.ExecuteQueryAsync(sql);
            
            Dispatcher.Invoke(() =>
            {
                ColumnsGrid.ItemsSource = dataTable.DefaultView;
                ColumnCountText.Text = dataTable.Rows.Count.ToString();
            });
            
            Logger.Info("Loaded {Count} columns", dataTable.Rows.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load columns");
            Dispatcher.Invoke(() => ColumnCountText.Text = "Error");
        }
    }

    private async Task LoadForeignKeysAsync()
    {
        Logger.Debug("Loading foreign keys for: {Table}", _tableName);
        
        try
        {
            var sql = $@"
                SELECT 
                    FK.CONSTNAME AS FKName,
                    FK.COLNAME AS FKColumn,
                    PK.TABSCHEMA || '.' || PK.TABNAME AS PKTable,
                    PK.COLNAME AS PKColumn,
                    R.DELETERULE AS DeleteRule,
                    R.UPDATERULE AS UpdateRule
                FROM SYSCAT.REFERENCES R
                JOIN SYSCAT.KEYCOLUSE FK ON R.CONSTNAME = FK.CONSTNAME AND R.TABSCHEMA = FK.TABSCHEMA AND R.TABNAME = FK.TABNAME
                JOIN SYSCAT.KEYCOLUSE PK ON R.REFKEYNAME = PK.CONSTNAME AND R.REFTABSCHEMA = PK.TABSCHEMA AND R.REFTABNAME = PK.TABNAME
                    AND FK.COLSEQ = PK.COLSEQ
                WHERE R.TABSCHEMA = '{_schema}' AND R.TABNAME = '{_tableName}'
                ORDER BY FK.CONSTNAME, FK.COLSEQ";
            
            var dataTable = await _connectionManager.ExecuteQueryAsync(sql);
            
            Dispatcher.Invoke(() =>
            {
                ForeignKeysGrid.ItemsSource = dataTable.DefaultView;
                FKCountText.Text = dataTable.Rows.Count.ToString();
            });
            
            Logger.Info("Loaded {Count} foreign keys", dataTable.Rows.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load foreign keys");
            Dispatcher.Invoke(() => FKCountText.Text = "Error");
        }
    }

    private async Task LoadIndexesAsync()
    {
        Logger.Debug("Loading indexes for: {Table}", _tableName);
        
        try
        {
            var sql = $@"
                SELECT 
                    I.INDNAME AS IndexName,
                    CASE WHEN I.INDEXTYPE = 'CLUS' THEN 'Clustered'
                         WHEN I.INDEXTYPE = 'REG' THEN 'Regular'
                         WHEN I.INDEXTYPE = 'DIM' THEN 'Dimension'
                         ELSE I.INDEXTYPE END AS IndexType,
                    CASE WHEN I.UNIQUERULE = 'U' THEN 'Yes' 
                         WHEN I.UNIQUERULE = 'P' THEN 'Primary Key'
                         ELSE 'No' END AS IsUnique,
                    LISTAGG(IC.COLNAME, ', ') WITHIN GROUP (ORDER BY IC.COLSEQ) AS Columns
                FROM SYSCAT.INDEXES I
                LEFT JOIN SYSCAT.INDEXCOLUSE IC ON I.INDSCHEMA = IC.INDSCHEMA AND I.INDNAME = IC.INDNAME
                WHERE I.TABSCHEMA = '{_schema}' AND I.TABNAME = '{_tableName}'
                GROUP BY I.INDNAME, I.INDEXTYPE, I.UNIQUERULE
                ORDER BY I.INDNAME";
            
            var dataTable = await _connectionManager.ExecuteQueryAsync(sql);
            
            Dispatcher.Invoke(() =>
            {
                IndexesGrid.ItemsSource = dataTable.DefaultView;
                IndexCountText.Text = dataTable.Rows.Count.ToString();
            });
            
            Logger.Info("Loaded {Count} indexes", dataTable.Rows.Count);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load indexes");
            Dispatcher.Invoke(() => IndexCountText.Text = "Error");
        }
    }

    private async Task LoadStatisticsAsync()
    {
        Logger.Debug("Loading statistics for: {Table}", _tableName);
        
        try
        {
            // Get row count
            var countSql = $"SELECT COUNT(*) FROM {_fullTableName}";
            var rowCount = await _connectionManager.ExecuteScalarAsync(countSql);
            
            // Get table info
            var infoSql = $@"
                SELECT TYPE, TBSPACE
                FROM SYSCAT.TABLES
                WHERE TABSCHEMA = '{_schema}' AND TABNAME = '{_tableName}'";
            
            var infoTable = await _connectionManager.ExecuteQueryAsync(infoSql);
            
            Dispatcher.Invoke(() =>
            {
                RowCountText.Text = rowCount != null ? Convert.ToInt64(rowCount).ToString("N0") : "Unknown";
                
                if (infoTable.Rows.Count > 0)
                {
                    var type = infoTable.Rows[0]["TYPE"]?.ToString() ?? "Unknown";
                    var tbspace = infoTable.Rows[0]["TBSPACE"]?.ToString() ?? "Unknown";
                    
                    TableTypeText.Text = type switch
                    {
                        "T" => "Table",
                        "V" => "View",
                        "S" => "System Table",
                        "A" => "Alias",
                        _ => type
                    };
                    
                    TablespaceText.Text = tbspace;
                }
            });
            
            Logger.Info("Statistics loaded - Row count: {Count}", rowCount);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load statistics");
            Dispatcher.Invoke(() => RowCountText.Text = "Error");
        }
    }

    private async Task GenerateDDLAsync()
    {
        Logger.Debug("Generating DDL for: {Table}", _tableName);
        
        try
        {
            // Get columns
            var columnsSql = $@"
                SELECT COLNAME, TYPENAME, LENGTH, SCALE, NULLS, DEFAULT
                FROM SYSCAT.COLUMNS 
                WHERE TABSCHEMA = '{_schema}' AND TABNAME = '{_tableName}'
                ORDER BY COLNO";
            
            var columnsTable = await _connectionManager.ExecuteQueryAsync(columnsSql);
            
            // Build DDL
            var ddl = new System.Text.StringBuilder();
            ddl.AppendLine($"-- DDL for table {_fullTableName}");
            ddl.AppendLine();
            ddl.AppendLine($"CREATE TABLE {_fullTableName} (");
            
            for (int i = 0; i < columnsTable.Rows.Count; i++)
            {
                var row = columnsTable.Rows[i];
                var colName = row["COLNAME"].ToString();
                var typeName = row["TYPENAME"].ToString();
                var length = row["LENGTH"].ToString();
                var scale = row["SCALE"].ToString();
                var nulls = row["NULLS"].ToString();
                var defaultVal = row["DEFAULT"]?.ToString();
                
                ddl.Append($"    {colName} {typeName}");
                
                if (typeName == "VARCHAR" || typeName == "CHAR" || typeName == "GRAPHIC" || typeName == "VARGRAPHIC")
                {
                    ddl.Append($"({length})");
                }
                else if (typeName == "DECIMAL" || typeName == "NUMERIC")
                {
                    ddl.Append($"({length},{scale})");
                }
                
                if (nulls == "N")
                {
                    ddl.Append(" NOT NULL");
                }
                
                if (!string.IsNullOrEmpty(defaultVal) && defaultVal != "null")
                {
                    ddl.Append($" DEFAULT {defaultVal}");
                }
                
                if (i < columnsTable.Rows.Count - 1)
                {
                    ddl.AppendLine(",");
                }
                else
                {
                    ddl.AppendLine();
                }
            }
            
            ddl.AppendLine(");");
            
            Dispatcher.Invoke(() =>
            {
                DDLTextBox.Text = ddl.ToString();
            });
            
            Logger.Debug("DDL generated successfully");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to generate DDL");
            Dispatcher.Invoke(() => DDLTextBox.Text = $"-- Error generating DDL:\n-- {ex.Message}");
        }
    }

    private void ForeignKey_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (ForeignKeysGrid.SelectedItem is DataRowView row)
        {
            var relatedTable = row["PKTable"]?.ToString();
            if (!string.IsNullOrEmpty(relatedTable))
            {
                Logger.Info("Opening related table: {Table}", relatedTable);
                SelectedRelatedTable = relatedTable;
                DialogResult = true;
                Close();
            }
        }
    }

    private void QueryTable_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Query top 1000 rows requested for: {Table}", _fullTableName);
        ShouldQueryTable = true;
        DialogResult = true;
        Close();
    }

    private void CopyDDL_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Clipboard.SetText(DDLTextBox.Text);
            MessageBox.Show("DDL copied to clipboard!", "Success", 
                MessageBoxButton.OK, MessageBoxImage.Information);
            Logger.Debug("DDL copied to clipboard");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to copy DDL to clipboard");
            MessageBox.Show($"Failed to copy to clipboard:\n\n{ex.Message}", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}

