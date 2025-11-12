using System.Windows;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Dialogs;

public partial class ConnectionDialog : Window
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    public DB2Connection? Connection { get; private set; }

    public ConnectionDialog()
    {
        InitializeComponent();
        Logger.Debug("ConnectionDialog opened");
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
            SavePassword = !string.IsNullOrEmpty(PasswordBox.Password)
        };
    }
}

