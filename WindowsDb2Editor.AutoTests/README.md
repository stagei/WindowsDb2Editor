# WindowsDb2Editor Automated UI Testing

This project uses **FlaUI** to automate UI testing of the WindowsDb2Editor application.

## 🎯 What This Does

This automated test suite:
- ✅ Starts the WindowsDb2Editor application
- ✅ Finds and clicks the FKKTOTST profile in Recent Connections
- ✅ Verifies connection tab opens
- ✅ Tests keyboard shortcuts (Ctrl+N)
- ✅ Takes screenshots on failures
- ✅ Provides detailed logging

## 🚀 How to Run

### Quick Start
```powershell
# From project root
.\_run_automated_ui_tests.ps1
```

### With Custom Profile
```powershell
.\_run_automated_ui_tests.ps1 -ProfileName "BASISVFT" -TestSchema "TEST"
```

### Build Only
```powershell
.\_run_automated_ui_tests.ps1 -BuildOnly
```

### Verbose Output
```powershell
.\_run_automated_ui_tests.ps1 -Verbose
```

## 📦 Dependencies

- **FlaUI.Core** (4.0.0) - Core UI automation framework
- **FlaUI.UIA3** (4.0.0) - UI Automation v3 wrapper
- **NLog** (6.0.6) - Logging framework

## 🔍 What Gets Tested

1. **Application Startup** - Verifies app launches correctly
2. **Main Window** - Checks window title and state
3. **Recent Connections Panel** - Finds and lists all saved connections
4. **Profile Connection** - Double-clicks FKKTOTST to connect
5. **Connection Tab** - Verifies connection tab appears
6. **New Connection Dialog** - Tests Ctrl+N keyboard shortcut

## 📝 Test Output

The tests provide detailed console output:
```
═══════════════════════════════════════════════════════════════
  WindowsDb2Editor - Automated UI Testing with FlaUI
═══════════════════════════════════════════════════════════════

📋 Test Configuration:
   Profile: FKKTOTST
   Test Schema: INL

🚀 Test 1: Starting application...
   ✅ Application started (PID: 12345)
   ✅ Main window found: DbExplorer - DB2 Database Manager

🔍 Test 2: Verifying main window...
   ✅ Window title: DbExplorer - DB2 Database Manager
   ✅ Main window verified

🔍 Test 3: Verifying Recent Connections panel...
   ✅ Recent Connections panel found
   ✅ Recent Connections list found
   📋 Found 3 connection(s):
      - FKKTOTST
      - BASISVFT
      - ILOGTST

🔌 Test 4: Connecting to profile 'FKKTOTST'...
   🔍 Searching for 'FKKTOTST' in list...
   ✅ Found profile: FKKTOTST
   🖱️  Double-clicking profile...
   ✅ Profile clicked, waiting for connection...

🔍 Test 5: Verifying connection tab for 'FKKTOTST'...
   ✅ Found 1 tab(s)
      - FKKTOTST @ server
   ✅ Connection tab found: FKKTOTST @ server

🔍 Test 6: Testing New Connection dialog...
   ⌨️  Pressing Ctrl+N...
   ✅ Dialog found: New DB2 Connection
   ⌨️  Pressing Escape to close...
   ✅ Dialog test complete

🧹 Cleaning up...
   Closing application...
   ✅ Cleanup complete

🎉 All tests passed!

═══════════════════════════════════════════════════════════════
✅ All automated UI tests passed!
═══════════════════════════════════════════════════════════════
```

## 📊 Logs

Test logs are saved to:
```
WindowsDb2Editor.AutoTests\bin\Debug\net10.0-windows\logs\autotests_YYYYMMDD.log
```

## 🐛 Troubleshooting

### Application Not Found
Make sure the main application is built first:
```powershell
dotnet build WindowsDb2Editor.csproj
```

### Profile Not Found
Ensure the profile exists in saved connections:
- Check `%APPDATA%\WindowsDb2Editor\connections.json`
- Or use `Manage Connections` in the app to create it

### Tests Fail to Find Elements
- Check that AutomationIds are set in XAML
- Ensure UI Automation is enabled (should be by default in WPF)
- Run with `-Verbose` for detailed output

## 🔧 Extending Tests

To add new tests, edit `Program.cs` and add methods like:

```csharp
private void Test_MyNewTest()
{
    Console.WriteLine("🔍 Test: My new test...");
    
    // Your test code here
    
    Console.WriteLine("   ✅ Test passed");
}
```

Then call it from `RunAllTests()`.

## 📚 FlaUI Resources

- **Documentation**: https://github.com/FlaUI/FlaUI
- **Examples**: https://github.com/FlaUI/FlaUI/tree/master/src/FlaUI.Core.UITests
- **License**: MIT (Free and Open Source)

