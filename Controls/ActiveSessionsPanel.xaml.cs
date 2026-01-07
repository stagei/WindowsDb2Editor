using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Models;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.Controls;

public partial class ActiveSessionsPanel : UserControl
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly SessionMonitorService _sessionService;
    private DB2ConnectionManager? _connectionManager;
    
    public ActiveSessionsPanel()
    {
        InitializeComponent();
        _sessionService = new SessionMonitorService();
        Logger.Debug("ActiveSessionsPanel initialized");
        
        // Apply grid preferences
        ApplyGridPreferences();
    }
    
    private void ApplyGridPreferences()
    {
        if (App.PreferencesService != null)
        {
            GridStyleHelper.ApplyGridStyle(SessionsDataGrid, App.PreferencesService.Preferences);
        }
    }
    
    public async Task InitializeAsync(DB2ConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
        UserComboBox.Items.Add("*");
        UserComboBox.SelectedIndex = 0;
        ApplicationComboBox.Items.Add("*");
        ApplicationComboBox.SelectedIndex = 0;
        await RefreshDataAsync();
    }
    
    private async Task RefreshDataAsync()
    {
        if (_connectionManager == null) return;
        try
        {
            StatusText.Text = "Loading...";
            var filter = new SessionMonitorFilter
            {
                UserFilter = UserComboBox.SelectedItem?.ToString() ?? "*",
                ApplicationFilter = ApplicationComboBox.SelectedItem?.ToString() ?? "*"
            };
            var sessions = await _sessionService.GetActiveSessionsAsync(_connectionManager, filter);
            SessionsDataGrid.ItemsSource = sessions;
            StatusText.Text = $"{sessions.Count} sessions - {DateTime.Now:HH:mm:ss}";
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to refresh sessions");
            MessageBox.Show($"Error:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    private async void RefreshButton_Click(object sender, RoutedEventArgs e) => await RefreshDataAsync();
    
    private void GenerateScript_Click(object sender, RoutedEventArgs e)
    {
        var sessions = SessionsDataGrid.SelectedItems.Cast<SessionInfo>().ToList();
        if (sessions.Count == 0) return;
        var script = _sessionService.GenerateForceApplicationScript(sessions);
        var window = Services.ThemedWindowHelper.CreateScriptWindow("Force Application Script", script, 600, 400, Window.GetWindow(this));
        window.ShowDialog();
    }
    
    private async void ExportButton_Click(object sender, RoutedEventArgs e)
    {
        var sessions = SessionsDataGrid.ItemsSource as List<SessionInfo>;
        if (sessions == null || sessions.Count == 0) return;
        var saveDialog = new SaveFileDialog { Filter = "CSV Files (*.csv)|*.csv", FileName = $"sessions_{DateTime.Now:yyyyMMdd_HHmmss}.csv" };
        if (saveDialog.ShowDialog() == true)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Agent ID,Database,User,Application,Client,Connected,Duration,Status");
            foreach (var s in sessions)
                csv.AppendLine($"{s.AgentId},{s.DatabaseName},{s.AuthorizationId},{s.ApplicationName},{s.ClientName},{s.ConnectTime},{s.DurationText},{s.Status}");
            await File.WriteAllTextAsync(saveDialog.FileName, csv.ToString());
            MessageBox.Show("Exported", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

