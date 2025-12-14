# Comprehensive CLI Test - ALL 119 Commands
# Tests every single CLI command with FKKTOTST connection
# Date: December 15, 2025

$ErrorActionPreference = "Continue"
$testResults = @()
$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
$outputDir = "CLI_Test_Output"
$reportFile = "$outputDir/COMPLETE_TEST_REPORT_$timestamp.md"

if (!(Test-Path $outputDir)) {
    New-Item -ItemType Directory -Path $outputDir | Out-Null
}

$connectionProfile = "FKKTOTST"

Write-Host "=" * 80 -ForegroundColor Cyan
Write-Host "COMPREHENSIVE CLI TEST - ALL 119 COMMANDS" -ForegroundColor Cyan
Write-Host "Connection: $connectionProfile" -ForegroundColor Cyan
Write-Host "Started: $(Get-Date)" -ForegroundColor Cyan
Write-Host "=" * 80 -ForegroundColor Cyan
Write-Host ""

# Build the application first
Write-Host "[1/3] Building application..." -ForegroundColor Yellow
$buildResult = dotnet build 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ BUILD FAILED" -ForegroundColor Red
    exit 1
}
Write-Host "✅ Build successful" -ForegroundColor Green
Write-Host ""

# Test parameters
$testSchema = "INL"
$testTable = "BILAGNR"
$testView = "FAKTURA"
$testProcSchema = "SYSPROC"             # System procedures schema
$testProcedure = "ADMIN_CMD"            # System procedure
$testFuncSchema = "SYSFUN"              # System functions schema
$testFunction = "ABS"                   # System function
$testPkgSchema = "NULLID"               # System package schema
$testPackage = "SYSSH200"               # System package
$testUser = "FKGEISTA"

# Define all 119 CLI commands to test
$commands = @(
    # Connection & Info (5)
    @{ Name = "connection-test"; Args = "-profile $connectionProfile" }
    @{ Name = "connection-stats"; Args = "-profile $connectionProfile" }
    @{ Name = "help-all"; Args = "" }
    @{ Name = "cli-version"; Args = "" }
    @{ Name = "db-config"; Args = "-profile $connectionProfile" }
    
    # Schema Operations (10)
    @{ Name = "list-schemas"; Args = "-profile $connectionProfile" }
    @{ Name = "list-tables"; Args = "-profile $connectionProfile -schema $testSchema" }
    @{ Name = "list-views"; Args = "-profile $connectionProfile -schema $testSchema" }
    @{ Name = "list-procedures"; Args = "-profile $connectionProfile -schema $testSchema" }
    @{ Name = "list-functions"; Args = "-profile $connectionProfile -schema $testSchema" }
    @{ Name = "list-packages"; Args = "-profile $connectionProfile -schema $testSchema" }
    @{ Name = "list-tablespaces"; Args = "-profile $connectionProfile" }
    @{ Name = "list-all-indexes"; Args = "-profile $connectionProfile -schema $testSchema" }
    @{ Name = "list-constraints"; Args = "-profile $connectionProfile -schema $testSchema" }
    @{ Name = "list-sequences"; Args = "-profile $connectionProfile -schema $testSchema" }
    
    # Table Operations (15)
    @{ Name = "table-properties"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "table-columns"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "table-indexes"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "table-foreignkeys"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "table-ddl"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "table-statistics"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "table-relationships"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "table-sample-data"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "table-size"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "table-grants"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "table-dependencies"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "table-incoming-fk"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "table-used-by-packages"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "table-used-by-views"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "table-used-by-routines"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    
    # View Operations (7)
    @{ Name = "view-properties"; Args = "-profile $connectionProfile -object $testSchema.$testView" }
    @{ Name = "view-definition"; Args = "-profile $connectionProfile -object $testSchema.$testView" }
    @{ Name = "view-columns"; Args = "-profile $connectionProfile -object $testSchema.$testView" }
    @{ Name = "view-dependencies"; Args = "-profile $connectionProfile -object $testSchema.$testView" }
    @{ Name = "view-sample-data"; Args = "-profile $connectionProfile -object $testSchema.$testView" }
    @{ Name = "view-used-by-packages"; Args = "-profile $connectionProfile -object $testSchema.$testView" }
    @{ Name = "view-used-by-views"; Args = "-profile $connectionProfile -object $testSchema.$testView" }
    
    # Procedure Operations (6) - using system procedure SYSPROC.ADMIN_CMD
    @{ Name = "procedure-properties"; Args = "-profile $connectionProfile -object $testProcSchema.$testProcedure" }
    @{ Name = "procedure-source"; Args = "-profile $connectionProfile -object $testProcSchema.$testProcedure" }
    @{ Name = "procedure-parameters"; Args = "-profile $connectionProfile -object $testProcSchema.$testProcedure" }
    @{ Name = "procedure-dependencies"; Args = "-profile $connectionProfile -object $testProcSchema.$testProcedure" }
    @{ Name = "procedure-usage"; Args = "-profile $connectionProfile -object $testProcSchema.$testProcedure" }
    @{ Name = "procedure-grants"; Args = "-profile $connectionProfile -object $testProcSchema.$testProcedure" }
    
    # Function Operations (6) - using system function SYSFUN.ABS
    @{ Name = "function-properties"; Args = "-profile $connectionProfile -object $testFuncSchema.$testFunction" }
    @{ Name = "function-source"; Args = "-profile $connectionProfile -object $testFuncSchema.$testFunction" }
    @{ Name = "function-parameters"; Args = "-profile $connectionProfile -object $testFuncSchema.$testFunction" }
    @{ Name = "function-dependencies"; Args = "-profile $connectionProfile -object $testFuncSchema.$testFunction" }
    @{ Name = "function-usage"; Args = "-profile $connectionProfile -object $testFuncSchema.$testFunction" }
    @{ Name = "function-grants"; Args = "-profile $connectionProfile -object $testFuncSchema.$testFunction" }
    
    # Package Operations (8)
    @{ Name = "package-properties"; Args = "-profile $connectionProfile -object $testSchema.$testPackage" }
    @{ Name = "package-statements"; Args = "-profile $connectionProfile -object $testSchema.$testPackage" }
    @{ Name = "package-dependencies"; Args = "-profile $connectionProfile -object $testSchema.$testPackage" }
    @{ Name = "package-statistics"; Args = "-profile $connectionProfile -object $testSchema.$testPackage" }
    @{ Name = "package-list-tables"; Args = "-profile $connectionProfile -object $testSchema.$testPackage" }
    @{ Name = "package-list-views"; Args = "-profile $connectionProfile -object $testSchema.$testPackage" }
    @{ Name = "package-list-procedures"; Args = "-profile $connectionProfile -object $testSchema.$testPackage" }
    @{ Name = "package-list-functions"; Args = "-profile $connectionProfile -object $testSchema.$testPackage" }
    
    # User & Security Operations (5)
    @{ Name = "user-properties"; Args = "-profile $connectionProfile -object $testUser" }
    @{ Name = "user-privileges"; Args = "-profile $connectionProfile -object $testUser" }
    @{ Name = "user-tables"; Args = "-profile $connectionProfile -object $testUser" }
    @{ Name = "user-schemas"; Args = "-profile $connectionProfile -object $testUser" }
    @{ Name = "user-connections"; Args = "-profile $connectionProfile" }
    
    # Monitoring & Stats (12)
    @{ Name = "db-load"; Args = "-profile $connectionProfile" }
    @{ Name = "db-locks"; Args = "-profile $connectionProfile" }
    @{ Name = "db-sessions"; Args = "-profile $connectionProfile" }
    @{ Name = "db-size"; Args = "-profile $connectionProfile" }
    @{ Name = "schema-size"; Args = "-profile $connectionProfile -schema $testSchema" }
    @{ Name = "db-version"; Args = "-profile $connectionProfile" }
    @{ Name = "db-parameters"; Args = "-profile $connectionProfile" }
    @{ Name = "db-registry"; Args = "-profile $connectionProfile" }
    @{ Name = "active-queries"; Args = "-profile $connectionProfile" }
    @{ Name = "bufferpool-stats"; Args = "-profile $connectionProfile" }
    @{ Name = "tablespace-usage"; Args = "-profile $connectionProfile" }
    @{ Name = "connection-history"; Args = "-profile $connectionProfile" }
    
    # CDC Operations (7)
    @{ Name = "cdc-status"; Args = "-profile $connectionProfile -schema $testSchema" }
    @{ Name = "cdc-status-full"; Args = "-profile $connectionProfile -schema $testSchema" }
    @{ Name = "cdc-configuration"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "cdc-changes"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "cdc-enable"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "cdc-disable"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "cdc-history"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    
    # Database Comparison (4)
    @{ Name = "db-compare"; Args = "-profile $connectionProfile -object $testSchema -schema ${testSchema}_TEST" }
    @{ Name = "db-compare-source-only"; Args = "-profile $connectionProfile -object $testSchema -schema ${testSchema}_TEST" }
    @{ Name = "db-compare-target-only"; Args = "-profile $connectionProfile -object $testSchema -schema ${testSchema}_TEST" }
    @{ Name = "db-compare-different"; Args = "-profile $connectionProfile -object $testSchema -schema ${testSchema}_TEST" }
    @{ Name = "db-compare-ddl"; Args = "-profile $connectionProfile -object $testSchema -schema ${testSchema}_TEST" }
    
    # Metadata & Advanced (8)
    @{ Name = "object-metadata"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "object-search"; Args = "-profile $connectionProfile -pattern BILAG%" }
    @{ Name = "column-search"; Args = "-profile $connectionProfile -pattern BILAG%" }
    @{ Name = "dependency-graph"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "query-history"; Args = "-profile $connectionProfile" }
    @{ Name = "schema-summary"; Args = "-profile $connectionProfile -schema $testSchema" }
    @{ Name = "database-summary"; Args = "-profile $connectionProfile" }
    @{ Name = "health-check"; Args = "-profile $connectionProfile" }
    
    # AI Features (4)
    @{ Name = "ai-query"; Args = "-profile $connectionProfile -prompt 'Show all invoices from last month'" }
    @{ Name = "ai-explain-table"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    @{ Name = "ai-explain-view"; Args = "-profile $connectionProfile -object $testSchema.$testView" }
    @{ Name = "ai-analyze-procedure"; Args = "-profile $connectionProfile -object $testSchema.$testProcedure" }
    @{ Name = "ai-analyze-function"; Args = "-profile $connectionProfile -object $testSchema.$testFunction" }
    @{ Name = "ai-analyze-package"; Args = "-profile $connectionProfile -object $testSchema.$testPackage" }
    @{ Name = "ai-deep-analysis"; Args = "-profile $connectionProfile -object $testSchema.$testTable" }
    
    # GUI Testing (12)
    @{ Name = "gui-test"; Args = "-profile $connectionProfile -form TableDetails -object $testSchema.$testTable" }
    @{ Name = "gui-test"; Args = "-profile $connectionProfile -form TableDetails -object $testSchema.$testTable -tab Columns" }
    @{ Name = "gui-test"; Args = "-profile $connectionProfile -form TableDetails -object $testSchema.$testTable -tab Indexes" }
    @{ Name = "gui-test"; Args = "-profile $connectionProfile -form TableDetails -object $testSchema.$testTable -tab ForeignKeys" }
    @{ Name = "gui-test"; Args = "-profile $connectionProfile -form ViewDetails -object $testSchema.$testView" }
    @{ Name = "gui-test"; Args = "-profile $connectionProfile -form ProcedureDetails -object $testSchema.$testProcedure" }
    @{ Name = "gui-test"; Args = "-profile $connectionProfile -form FunctionDetails -object $testSchema.$testFunction" }
    @{ Name = "gui-test"; Args = "-profile $connectionProfile -form PackageDetails -object $testSchema.$testPackage" }
    @{ Name = "gui-test"; Args = "-profile $connectionProfile -form DatabaseLoadMonitor" }
    @{ Name = "gui-test"; Args = "-profile $connectionProfile -form ActiveSessions" }
    @{ Name = "gui-test"; Args = "-profile $connectionProfile -form LockMonitor" }
    @{ Name = "gui-test"; Args = "-profile $connectionProfile -form CdcStatusDialog -schema $testSchema" }
)

Write-Host "[2/3] Running $($commands.Count) CLI commands..." -ForegroundColor Yellow
$totalCommands = $commands.Count
$passed = 0
$failed = 0
$current = 0

foreach ($cmd in $commands) {
    $current++
    $progress = [math]::Round(($current / $totalCommands) * 100, 1)
    
    Write-Host "[${current}/${totalCommands}] ${progress}% - Testing: $($cmd.Name)" -ForegroundColor Gray
    
    $startTime = Get-Date
    $output = ""
    $exitCode = 0
    
    try {
        $outFile = "$outputDir/$($cmd.Name).json"
        $fullCommand = "bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command $($cmd.Name) $($cmd.Args) -Outfile $outFile"
        $output = & cmd /c $fullCommand 2>&1
        $exitCode = $LASTEXITCODE
    }
    catch {
        $output = $_.Exception.Message
        $exitCode = -1
    }
    
    $duration = ((Get-Date) - $startTime).TotalMilliseconds
    
    # Check if output contains actual error (not log messages)
    $outputStr = $output | Out-String
    $hasError = $outputStr -match "^ERROR:|CRITICAL:|SQL0206N|SQLCODE=-|Invalid.*parameter|not found.*Error|Query failed"
    
    $result = @{
        Command = $cmd.Name
        Args = $cmd.Args
        ExitCode = $exitCode
        Duration = [math]::Round($duration, 0)
        Output = $outputStr
        Passed = ($exitCode -eq 0 -and -not $hasError)
    }
    
    if ($result.Passed) {
        Write-Host "  ✅ PASSED (${duration}ms)" -ForegroundColor Green
        $passed++
    }
    else {
        Write-Host "  ❌ FAILED (exit code: $exitCode)" -ForegroundColor Red
        $failed++
    }
    
    $testResults += $result
}

Write-Host ""
Write-Host "[3/3] Generating report..." -ForegroundColor Yellow

# Generate markdown report
$report = @"
# Comprehensive CLI Test Report - ALL 119 Commands

**Date**: $(Get-Date -Format "yyyy-MM-dd HH:mm:ss")  
**Connection**: $connectionProfile  
**Total Commands Tested**: $totalCommands  
**✅ Passed**: $passed ($([math]::Round(($passed/$totalCommands)*100, 1))%)  
**❌ Failed**: $failed ($([math]::Round(($failed/$totalCommands)*100, 1))%)  

---

## Summary

``````
Total Tests: $totalCommands
Passed:      $passed
Failed:      $failed
Success Rate: $([math]::Round(($passed/$totalCommands)*100, 1))%
``````

---

## Test Results by Category

"@

# Group by category
$categories = @{
    "Connection & Info" = ($testResults | Where-Object { $_.Command -match "^(connection|help|cli|db-config)" })
    "Schema Operations" = ($testResults | Where-Object { $_.Command -match "^list-" })
    "Table Operations" = ($testResults | Where-Object { $_.Command -match "^table-" })
    "View Operations" = ($testResults | Where-Object { $_.Command -match "^view-" })
    "Procedure Operations" = ($testResults | Where-Object { $_.Command -match "^procedure-" })
    "Function Operations" = ($testResults | Where-Object { $_.Command -match "^function-" })
    "Package Operations" = ($testResults | Where-Object { $_.Command -match "^package-" })
    "User & Security" = ($testResults | Where-Object { $_.Command -match "^user-" })
    "Monitoring & Stats" = ($testResults | Where-Object { $_.Command -match "^(db-load|db-locks|db-sessions|db-size|schema-size|db-version|db-parameters|db-registry|active-queries|bufferpool|tablespace|connection-history)" })
    "CDC Operations" = ($testResults | Where-Object { $_.Command -match "^cdc-" })
    "Database Comparison" = ($testResults | Where-Object { $_.Command -match "^db-compare" })
    "Metadata & Advanced" = ($testResults | Where-Object { $_.Command -match "^(object-|column-|dependency-|query-|schema-summary|database-summary|health-check)" })
    "AI Features" = ($testResults | Where-Object { $_.Command -match "^ai-" })
    "GUI Testing" = ($testResults | Where-Object { $_.Command -match "^gui-test" })
}

foreach ($category in $categories.Keys | Sort-Object) {
    $categoryResults = $categories[$category]
    $catPassed = ($categoryResults | Where-Object { $_.Passed }).Count
    $catTotal = $categoryResults.Count
    
    $report += @"

### $category ($catPassed/$catTotal passed)

| Command | Status | Duration (ms) | Exit Code |
|---------|--------|---------------|-----------|

"@
    
    foreach ($result in $categoryResults) {
        $status = if ($result.Passed) { "✅ PASS" } else { "❌ FAIL" }
        $report += "| ``$($result.Command)`` | $status | $($result.Duration) | $($result.ExitCode) |`n"
    }
}

$report += @"

---

## Detailed Results

"@

foreach ($result in $testResults) {
    $status = if ($result.Passed) { "✅ PASSED" } else { "❌ FAILED" }
    
    $report += @"

### $($result.Command) - $status

**Arguments**: ``$($result.Args)``  
**Duration**: $($result.Duration) ms  
**Exit Code**: $($result.ExitCode)  

<details>
<summary>Click to view output</summary>

``````
$($result.Output)
``````

</details>

---

"@
}

# Save report
$report | Out-File -FilePath $reportFile -Encoding UTF8

Write-Host ""
Write-Host "=" * 80 -ForegroundColor Cyan
Write-Host "TEST COMPLETE" -ForegroundColor Cyan
Write-Host "=" * 80 -ForegroundColor Cyan
Write-Host "Total: $totalCommands commands" -ForegroundColor White
Write-Host "✅ Passed: $passed ($([math]::Round(($passed/$totalCommands)*100, 1))%)" -ForegroundColor Green
Write-Host "❌ Failed: $failed ($([math]::Round(($failed/$totalCommands)*100, 1))%)" -ForegroundColor Red
Write-Host ""
Write-Host "Report saved to: $reportFile" -ForegroundColor Yellow
Write-Host "=" * 80 -ForegroundColor Cyan

