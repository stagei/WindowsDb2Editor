using System.Windows;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Models;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.Dialogs;

public partial class ConnectionDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private readonly ConnectionStorageService _storageService;
    public DB2Connection? Connection { get; private set; }

    public ConnectionDialog()
    {
        InitializeComponent();
        _storageService = new ConnectionStorageService();
        Logger.Debug("ConnectionDialog opened");
    }
    
    /// <summary>
    /// Constructor for editing existing connection
    /// </summary>
    public ConnectionDialog(DB2Connection connection) : this()
    {
        LoadConnection(connection);
    }
    
    /// <summary>
    /// Load an existing connection into the dialog fields
    /// </summary>
    public void LoadConnection(DB2Connection connection)
    {
        Logger.Debug("Loading connection into dialog: {Name}", connection.Name);
        
        NameTextBox.Text = connection.Name;
        ServerTextBox.Text = connection.Server;
        PortTextBox.Text = connection.Port.ToString();
        DatabaseTextBox.Text = connection.Database;
        UsernameTextBox.Text = connection.Username;
        PasswordBox.Password = connection.Password;
        ReadOnlyCheckBox.IsChecked = connection.IsReadOnly;
        AutoCommitCheckBox.IsChecked = connection.AutoCommit;
    }

    private async void TestConnection_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Testing connection");

        var connection = GetConnectionFromInputs();
        if (connection == null || !connection.IsValid())
        {
            Logger.Warn("Invalid connection information");
            MessageBox.Show("Please fill in all required fields.", "Validation Error", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            using var manager = new DB2ConnectionManager(connection);
            var success = await manager.TestConnectionAsync();

            if (success)
            {
                Logger.Info("Connection test successful");
                MessageBox.Show("Connection successful!", "Test Connection",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Logger.Warn("Connection test failed");
                MessageBox.Show("Connection failed. Please check your credentials.", "Test Connection",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Connection test error");
            MessageBox.Show($"Connection error: {ex.Message}", "Test Connection",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Connect_Click(object sender, RoutedEventArgs e)
    {
        Logger.Info("Connect button clicked");

        var connection = GetConnectionFromInputs();
        if (connection == null || !connection.IsValid())
        {
            Logger.Warn("Invalid connection information");
            MessageBox.Show("Please fill in all required fields.", "Validation Error",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Save connection to storage with encrypted password
        try
        {
            _storageService.SaveConnection(connection);
            Logger.Info("Connection saved to storage: {Name}", connection.Name);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to save connection");
            // Continue anyway - don't block connection if save fails
        }

        Connection = connection;
        Logger.Info($"Connection created: {connection.GetDisplayName()}");
        DialogResult = true;
        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Logger.Debug("Connection dialog cancelled");
        DialogResult = false;
        Close();
    }

    private DB2Connection? GetConnectionFromInputs()
    {
        if (!int.TryParse(PortTextBox.Text, out var port))
        {
            port = 50000;
        }

        return new DB2Connection
        {
            Name = NameTextBox.Text.Trim(),
            Server = ServerTextBox.Text.Trim(),
            Port = port,
            Database = DatabaseTextBox.Text.Trim(),
            Username = UsernameTextBox.Text.Trim(),
            Password = PasswordBox.Password,
            SavePassword = !string.IsNullOrEmpty(PasswordBox.Password),
            IsReadOnly = ReadOnlyCheckBox.IsChecked ?? false,  // Feature #2
            AutoCommit = AutoCommitCheckBox.IsChecked ?? true  // Feature #2
        };
    }
    
    /// <summary>
    /// Validates that port input is numeric only
    /// </summary>
    private void NumericOnly_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        // Only allow digits
        e.Handled = !IsNumeric(e.Text);
    }
    
    /// <summary>
    /// Checks if text contains only numeric characters
    /// </summary>
    private bool IsNumeric(string text)
    {
        return int.TryParse(text, out _);
    }
}

