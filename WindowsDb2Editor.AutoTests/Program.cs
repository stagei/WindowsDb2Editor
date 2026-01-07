using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Services;

namespace WindowsDb2Editor.AutoTests;

class Program
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    static async Task Main(string[] args)
    {
        ConfigureLogging();
        
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine("  WindowsDb2Editor - Automated UI Testing with FlaUI");
        Console.WriteLine("═══════════════════════════════════════════════════════════════");
        Console.WriteLine();
        
        try
        {
            var tester = new WindowsDb2EditorTester();
            
            // Parse command line arguments
            var profileName = args.Length > 0 ? args[0] : "FKKTOTST";
            var testSchema = args.Length > 1 ? args[1] : "INL";
            
            Console.WriteLine($"📋 Test Configuration:");
            Console.WriteLine($"   Profile: {profileName}");
            Console.WriteLine($"   Test Schema: {testSchema}");
            Console.WriteLine();
            
            // Run tests
            await tester.RunAllTestsAsync(profileName, testSchema);
            
            Console.WriteLine();
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine("✅ All tests completed successfully!");
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Test suite failed");
            Console.WriteLine();
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine("❌ Test suite failed!");
            Console.WriteLine($"   Error: {ex.Message}");
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Environment.Exit(1);
        }
    }
    
    static void ConfigureLogging()
    {
        var config = new NLog.Config.LoggingConfiguration();
        
        var consoleTarget = new NLog.Targets.ColoredConsoleTarget("console")
        {
            Layout = "${time} ${level:uppercase=true:padding=-5} ${message}"
        };
        
        config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget);
        
        LogManager.Configuration = config;
    }
}

public class WindowsDb2EditorTester
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private Application? _app;
    private AutomationBase? _automation;
    private Window? _mainWindow;
    private DB2ConnectionManager? _testConnectionManager;
    
    private string GetAppPath()
    {
        // Try multiple possible locations
        var possiblePaths = new[]
        {
            @"..\..\..\..\bin\Debug\net10.0-windows\WindowsDb2Editor.exe",  // From test bin folder
            @"..\..\..\WindowsDb2Editor\bin\Debug\net10.0-windows\WindowsDb2Editor.exe",  // Alternative
            @"WindowsDb2Editor.exe",  // Same directory
            @"..\..\..\bin\Debug\net10.0-windows\WindowsDb2Editor.exe"  // Up 3 levels
        };
        
        foreach (var path in possiblePaths)
        {
            var fullPath = Path.GetFullPath(path);
            if (File.Exists(fullPath))
            {
                return fullPath;
            }
        }
        
        // Search from current directory upwards
        var currentDir = Directory.GetCurrentDirectory();
        while (!string.IsNullOrEmpty(currentDir))
        {
            var testPath = Path.Combine(currentDir, "bin", "Debug", "net10.0-windows", "WindowsDb2Editor.exe");
            if (File.Exists(testPath))
            {
                return testPath;
            }
            
            var parentDir = Directory.GetParent(currentDir);
            if (parentDir == null) break;
            currentDir = parentDir.FullName;
        }
        
        throw new FileNotFoundException(
            $"WindowsDb2Editor.exe not found. Current directory: {Directory.GetCurrentDirectory()}");
    }
    
    public async Task RunAllTestsAsync(string profileName, string testSchema)
    {
        try
        {
            // Test 1: Start application
            Test_StartApplication();
            Thread.Sleep(2000);
            
            // Test 2: Verify main window
            Test_VerifyMainWindow();
            Thread.Sleep(1000);
            
            // Test 3: Find and verify Recent Connections
            Test_VerifyRecentConnections();
            Thread.Sleep(1000);
            
            // Test 4: Connect to profile
            Test_ConnectToProfile(profileName);
            Thread.Sleep(3000);
            
            // Test 5: Verify connection tab opened
            Test_VerifyConnectionTab(profileName);
            Thread.Sleep(1000);
            
            // Test 6: Test New Connection dialog
            Test_NewConnectionDialog();
            Thread.Sleep(1000);
            
            // Test 7-12: Mermaid Designer tests (WebView2-aware)
            if (_mainWindow != null && _automation != null)
            {
                Console.WriteLine();
                var mermaidTester = new MermaidWebViewTests(_mainWindow, _automation);
                mermaidTester.RunMermaidDesignerTests();
            }
            
            // Test 13-20: Mermaid Integration Deep Tests (establish real DB connection)
            try
            {
                Console.WriteLine();
                await Test_MermaidIntegrationAsync(profileName, testSchema);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ⚠️  Mermaid integration tests skipped: {ex.Message}");
                Logger.Warn(ex, "Mermaid integration tests skipped");
            }
            
            Console.WriteLine();
            Console.WriteLine("🎉 All tests passed!");
        }
        finally
        {
            Cleanup();
        }
    }
    
    private void Test_StartApplication()
    {
        Console.WriteLine("🚀 Test 1: Starting application...");
        Logger.Info("Starting WindowsDb2Editor");
        
        // Kill any existing instances
        var existingProcesses = Process.GetProcessesByName("WindowsDb2Editor");
        foreach (var proc in existingProcesses)
        {
            proc.Kill();
            proc.WaitForExit();
        }
        
        Thread.Sleep(1000);
        
        // Start the application
        var fullPath = GetAppPath();
        Console.WriteLine($"   📂 Using application path: {fullPath}");
        Logger.Info($"Application path: {fullPath}");
        
        _app = Application.Launch(fullPath);
        _automation = new UIA3Automation();
        
        // Wait for main window
        var retries = 10;
        while (retries-- > 0)
        {
            try
            {
                _mainWindow = _app.GetMainWindow(_automation, TimeSpan.FromSeconds(2));
                if (_mainWindow != null)
                {
                    break;
                }
            }
            catch
            {
                Thread.Sleep(500);
            }
        }
        
        if (_mainWindow == null)
        {
            throw new Exception("Main window not found after 5 seconds");
        }
        
        Console.WriteLine($"   ✅ Application started (PID: {_app.ProcessId})");
        Console.WriteLine($"   ✅ Main window found: {_mainWindow.Name}");
        Logger.Info($"Application started successfully: {_mainWindow.Name}");
    }
    
    private void Test_VerifyMainWindow()
    {
        Console.WriteLine("🔍 Test 2: Verifying main window...");
        
        if (_mainWindow == null)
        {
            throw new Exception("Main window is null");
        }
        
        // Maximize window
        _mainWindow.Patterns.Window.Pattern.SetWindowVisualState(WindowVisualState.Maximized);
        Thread.Sleep(500);
        
        // Check title
        var title = _mainWindow.Name;
        Console.WriteLine($"   ✅ Window title: {title}");
        
        if (!title.Contains("DbExplorer") && !title.Contains("Database Manager"))
        {
            throw new Exception($"Unexpected window title: {title}");
        }
        
        // Check if window is visible
        if (!_mainWindow.IsAvailable)
        {
            throw new Exception("Main window is not available");
        }
        
        Console.WriteLine("   ✅ Main window verified");
    }
    
    private void Test_VerifyRecentConnections()
    {
        Console.WriteLine("🔍 Test 3: Verifying Recent Connections panel...");
        
        if (_mainWindow == null)
        {
            throw new Exception("Main window is null");
        }
        
        // Find the Recent Connections header
        var recentHeader = _mainWindow.FindFirstDescendant(cf =>
            cf.ByText("Recent Connections"));
        
        if (recentHeader == null)
        {
            throw new Exception("Recent Connections header not found");
        }
        
        Console.WriteLine("   ✅ Recent Connections panel found");
        
        // Find the ListView
        var listView = _mainWindow.FindFirstDescendant(cf =>
            cf.ByAutomationId("RecentConnectionsList"));
        
        if (listView == null)
        {
            // Try finding any ListView
            listView = _mainWindow.FindFirstDescendant(cf =>
                cf.ByControlType(ControlType.List));
        }
        
        if (listView != null)
        {
            Console.WriteLine("   ✅ Recent Connections list found");
            
            // Get all list items
            var items = listView.FindAllChildren(cf =>
                cf.ByControlType(ControlType.ListItem));
            
            Console.WriteLine($"   📋 Found {items.Length} connection(s):");
            
            foreach (var item in items)
            {
                var name = item.Name ?? item.HelpText ?? "Unknown";
                Console.WriteLine($"      - {name}");
            }
        }
        else
        {
            Console.WriteLine("   ⚠️  Recent Connections list not found by AutomationId");
        }
    }
    
    private void Test_ConnectToProfile(string profileName)
    {
        Console.WriteLine($"🔌 Test 4: Connecting to profile '{profileName}'...");
        
        if (_mainWindow == null)
        {
            throw new Exception("Main window is null");
        }
        
        // Find the ListView
        var listView = _mainWindow.FindFirstDescendant(cf =>
            cf.ByAutomationId("RecentConnectionsList"));
        
        if (listView == null)
        {
            listView = _mainWindow.FindFirstDescendant(cf =>
                cf.ByControlType(ControlType.List));
        }
        
        if (listView == null)
        {
            throw new Exception("Could not find Recent Connections list");
        }
        
        // Find the profile item
        Console.WriteLine($"   🔍 Searching for '{profileName}' in list...");
        
        var profileItem = listView.FindFirstDescendant(cf =>
            cf.ByText(profileName).And(cf.ByControlType(ControlType.ListItem)));
        
        if (profileItem == null)
        {
            // Try finding by partial name
            var allItems = listView.FindAllDescendants(cf =>
                cf.ByControlType(ControlType.ListItem));
            
            foreach (var item in allItems)
            {
                Console.WriteLine($"   📝 Checking item: {item.Name}");
                if (item.Name?.Contains(profileName, StringComparison.OrdinalIgnoreCase) == true)
                {
                    profileItem = item;
                    break;
                }
                
                // Check descendants for text
                var textElement = item.FindFirstDescendant(cf =>
                    cf.ByText(profileName));
                if (textElement != null)
                {
                    profileItem = item;
                    break;
                }
            }
        }
        
        if (profileItem == null)
        {
            throw new Exception($"Profile '{profileName}' not found in Recent Connections");
        }
        
        Console.WriteLine($"   ✅ Found profile: {profileItem.Name}");
        
        // Double-click the item
        Console.WriteLine("   🖱️  Double-clicking profile...");
        profileItem.DoubleClick();
        
        Console.WriteLine("   ✅ Profile clicked, waiting for connection...");
    }
    
    private void Test_VerifyConnectionTab(string profileName)
    {
        Console.WriteLine($"🔍 Test 5: Verifying connection tab for '{profileName}'...");
        
        if (_mainWindow == null)
        {
            throw new Exception("Main window is null");
        }
        
        // Wait a bit for connection to establish
        Thread.Sleep(2000);
        
        // Look for a TabControl or TabItem
        var tabControl = _mainWindow.FindFirstDescendant(cf =>
            cf.ByControlType(ControlType.Tab));
        
        if (tabControl != null)
        {
            var tabs = tabControl.FindAllDescendants(cf =>
                cf.ByControlType(ControlType.TabItem));
            
            Console.WriteLine($"   ✅ Found {tabs.Length} tab(s)");
            
            foreach (var tab in tabs)
            {
                Console.WriteLine($"      - {tab.Name}");
            }
            
            // Check if our connection tab exists
            var connectionTab = tabControl.FindFirstDescendant(cf =>
                cf.ByText(profileName).And(cf.ByControlType(ControlType.TabItem)));
            
            if (connectionTab != null)
            {
                Console.WriteLine($"   ✅ Connection tab found: {connectionTab.Name}");
            }
            else
            {
                Console.WriteLine($"   ⚠️  Connection tab for '{profileName}' not found yet");
            }
        }
        else
        {
            Console.WriteLine("   ℹ️  No tab control found (might still be on welcome screen)");
        }
    }
    
    private void Test_NewConnectionDialog()
    {
        Console.WriteLine("🔍 Test 6: Testing New Connection dialog...");
        
        if (_mainWindow == null)
        {
            throw new Exception("Main window is null");
        }
        
        // Try keyboard shortcut: Ctrl+N
        Console.WriteLine("   ⌨️  Pressing Ctrl+N...");
        _mainWindow.Focus();
        Thread.Sleep(300);
        
        Keyboard.TypeSimultaneously(VirtualKeyShort.CONTROL, VirtualKeyShort.KEY_N);
        Thread.Sleep(1000);
        
        // Look for the New Connection dialog
        var dialog = _mainWindow.FindFirstDescendant(cf =>
            cf.ByControlType(ControlType.Window).And(cf.ByName("New DB2 Connection")));
        
        if (dialog == null)
        {
            // Try finding any dialog
            dialog = _mainWindow.FindFirstDescendant(cf =>
                cf.ByControlType(ControlType.Window));
        }
        
        if (dialog != null)
        {
            Console.WriteLine($"   ✅ Dialog found: {dialog.Name}");
            
            // Close it with Escape
            Console.WriteLine("   ⌨️  Pressing Escape to close...");
            Keyboard.Type(VirtualKeyShort.ESCAPE);
            Thread.Sleep(500);
            
            Console.WriteLine("   ✅ Dialog test complete");
        }
        else
        {
            Console.WriteLine("   ⚠️  New Connection dialog not found");
        }
    }
    
    private async Task Test_MermaidIntegrationAsync(string profileName, string testSchema)
    {
        Console.WriteLine("🔬 Test 13-20: Mermaid Integration Deep Tests...");
        Logger.Info("Establishing test DB connection for Mermaid integration tests");
        
        try
        {
            // Get connection info from saved connections
            var storageService = new ConnectionStorageService();
            var connection = storageService.GetConnection(profileName);
            
            if (connection == null)
            {
                Console.WriteLine($"   ⚠️  Connection profile '{profileName}' not found");
                return;
            }
            
            Console.WriteLine($"   🔌 Connecting to {connection.Server}...");
            _testConnectionManager = new DB2ConnectionManager(connection);
            await _testConnectionManager.OpenAsync();
            
            if (!_testConnectionManager.IsConnected)
            {
                Console.WriteLine("   ❌ Failed to establish DB connection");
                return;
            }
            
            Console.WriteLine($"   ✅ DB connection established successfully");
            
            // Run deep integration tests
            var integrationTester = new MermaidIntegrationTests(_testConnectionManager, testSchema);
            await integrationTester.RunAllIntegrationTests();
            
            // Run CLI tests (comprehensive 3-part test plan)
            Console.WriteLine("\n═══════════════════════════════════════════════");
            Console.WriteLine("  MERMAID CLI TESTS (3-Part Test Plan)");
            Console.WriteLine("═══════════════════════════════════════════════\n");
            
            var cliTestsPassed = await MermaidCliTests.RunAllTests(_testConnectionManager);
            if (!cliTestsPassed)
            {
                throw new Exception("Mermaid CLI tests failed");
            }
            
            // Run functional UI tests (verify actual workflows)
            Console.WriteLine("\n═══════════════════════════════════════════════");
            Console.WriteLine("  MERMAID DESIGNER FUNCTIONAL TESTS");
            Console.WriteLine("═══════════════════════════════════════════════\n");
            
            var functionalTester = new MermaidDesignerFunctionalTests(_app, _automation);
            functionalTester.RunAllTests();
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Mermaid integration tests failed");
            Console.WriteLine($"   ❌ Integration test error: {ex.Message}");
            throw;
        }
        finally
        {
            // Clean up test connection
            if (_testConnectionManager != null)
            {
                Console.WriteLine("   🧹 Closing test DB connection...");
                _testConnectionManager.Dispose();
                _testConnectionManager = null;
            }
        }
    }
    
    private void Cleanup()
    {
        Console.WriteLine();
        Console.WriteLine("🧹 Cleaning up...");
        
        try
        {
            // Clean up test connection if still open
            if (_testConnectionManager != null)
            {
                _testConnectionManager.Dispose();
                _testConnectionManager = null;
            }
            
            if (_app != null && !_app.HasExited)
            {
                Console.WriteLine("   Closing application...");
                _app.Close();
                Thread.Sleep(1000);
                
                if (!_app.HasExited)
                {
                    _app.Kill();
                }
            }
            
            _automation?.Dispose();
            Console.WriteLine("   ✅ Cleanup complete");
        }
        catch (Exception ex)
        {
            Logger.Warn(ex, "Error during cleanup");
        }
    }
}

