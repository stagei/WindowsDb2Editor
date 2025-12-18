using System;
using System.Windows;
using NLog;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Dialogs;

public partial class TablespaceDetailsDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly TablespaceInfo _tablespace;

    public TablespaceDetailsDialog(TablespaceInfo tablespace)
    {
        InitializeComponent();
        _tablespace = tablespace ?? throw new ArgumentNullException(nameof(tablespace));

        Logger.Debug("TablespaceDetailsDialog opened for: {Tablespace}", _tablespace.TablespaceName);

        Title = $"Tablespace Details - {_tablespace.TablespaceName}";

        TablespaceNameText.Text = _tablespace.TablespaceName;
        TablespaceInfoText.Text = $"Type: {_tablespace.TablespaceType} • Page size: {_tablespace.PageSize?.ToString() ?? "N/A"} • Owner: {_tablespace.Owner}";

        TablespaceNameValue.Text = _tablespace.TablespaceName;
        TablespaceTypeValue.Text = _tablespace.TablespaceType;
        DataTypeValue.Text = _tablespace.DataType;
        PageSizeValue.Text = _tablespace.PageSize?.ToString() ?? "N/A";
        OwnerValue.Text = _tablespace.Owner;
        CreateTimeValue.Text = _tablespace.CreateTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "N/A";
        RemarksValue.Text = string.IsNullOrWhiteSpace(_tablespace.Remarks) ? "No remarks" : _tablespace.Remarks;
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}

