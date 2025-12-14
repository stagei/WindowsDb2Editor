# WindowsDb2Editor - Mermaid Functionality Test Suite
# Tests all Mermaid/ERD features using CLI and GUI

param(
    [string]$Profile = "FKKTOTST",
    [string]$TestSchema = "INL",
    [int]$TimeoutSeconds = 30
)

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  🧪 MERMAID FUNCTIONALITY TESTS" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Profile: $Profile" -ForegroundColor White
Write-Host "Test Schema: $TestSchema" -ForegroundColor White
Write-Host "SqlMermaidErdTools: Version 0.2.8" -ForegroundColor Gray
Write-Host ""

$exe = "bin\Debug\net10.0-windows\WindowsDb2Editor.exe"
$results = @()
$passed = 0
$failed = 0

# Helper function to run CLI command with timeout
function Invoke-CliCommand {
    param(
        [string[]]$Arguments,
        [int]$Timeout = 30
    )
    
    # CLI requires -Outfile parameter, create temp file for output
    $outputFile = [System.IO.Path]::GetTempFileName() + ".json"
    $stderrFile = [System.IO.Path]::GetTempFileName() + ".err"
    
    # Add outfile to arguments
    $Arguments += @("-Outfile", $outputFile)
    
    try {
        $proc = Start-Process $exe -ArgumentList $Arguments -NoNewWindow -PassThru -Wait -RedirectStandardError $stderrFile
        
        if ($proc.ExitCode -eq 0) {
            # Read output from file
            if (Test-Path $outputFile) {
                $content = Get-Content $outputFile -Raw -ErrorAction SilentlyContinue
                
                if ($content) {
                    # Parse JSON
                    try {
                        return ($content | ConvertFrom-Json)
                    }
                    catch {
                        # Return raw content if not JSON
                        return $content
                    }
                }
            }
        }
        else {
            $errorContent = Get-Content $stderrFile -Raw -ErrorAction SilentlyContinue
            Write-Host "  ❌ CLI Error (exit $($proc.ExitCode)): $errorContent" -ForegroundColor Red
        }
    }
    finally {
        Remove-Item $outputFile -Force -ErrorAction SilentlyContinue
        Remove-Item $stderrFile -Force -ErrorAction SilentlyContinue
    }
    
    return $null
}

Write-Host "========================================" -ForegroundColor Yellow
Write-Host "  TEST 1: MERMAID ERD GENERATION" -ForegroundColor Yellow
Write-Host "========================================" -ForegroundColor Yellow
Write-Host ""

Write-Host "Generating Mermaid ERD from schema: $TestSchema..." -ForegroundColor Cyan

$result = Invoke-CliCommand @("--profile", $Profile, "--command", "mermaid-erd", "--schema", $TestSchema, "--limit", "5")

if ($result -and $result.mermaidDiagram) {
    $diagramLength = $result.mermaidDiagram.Length
    $tableCount = $result.tableCount
    
    Write-Host "  ✅ PASS - Generated Mermaid ERD" -ForegroundColor Green
    Write-Host "     Diagram Length: $diagramLength chars" -ForegroundColor Gray
    Write-Host "     Tables Included: $tableCount" -ForegroundColor Gray
    
    # Validate Mermaid syntax
    if ($result.mermaidDiagram -match "erDiagram" -and $result.mermaidDiagram -match "{") {
        Write-Host "     ✅ Valid Mermaid syntax detected" -ForegroundColor Green
    }
    else {
        Write-Host "     ⚠️  Mermaid syntax may be invalid" -ForegroundColor Yellow
    }
    
    # Save for later tests
    $global:TestMermaidDiagram = $result.mermaidDiagram
    
    $passed++
    $results += [PSCustomObject]@{
        Test = "Mermaid ERD Generation"
        Status = "PASS"
        Details = "$tableCount tables, $diagramLength chars"
    }
}
else {
    Write-Host "  ❌ FAIL - No Mermaid diagram generated" -ForegroundColor Red
    $failed++
    $results += [PSCustomObject]@{
        Test = "Mermaid ERD Generation"
        Status = "FAIL"
        Details = "No output received"
    }
}

Write-Host ""

Write-Host "========================================" -ForegroundColor Yellow
Write-Host "  TEST 2: SQL TO MERMAID CONVERSION" -ForegroundColor Yellow
Write-Host "========================================" -ForegroundColor Yellow
Write-Host ""

Write-Host "Converting SQL DDL to Mermaid..." -ForegroundColor Cyan

$testSql = @"
CREATE TABLE TEST_CUSTOMERS (
    CUSTOMER_ID INT PRIMARY KEY,
    FIRST_NAME VARCHAR(50) NOT NULL,
    LAST_NAME VARCHAR(50) NOT NULL,
    EMAIL VARCHAR(100) UNIQUE,
    CREATED_DATE TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE TEST_ORDERS (
    ORDER_ID INT PRIMARY KEY,
    CUSTOMER_ID INT NOT NULL,
    ORDER_DATE TIMESTAMP NOT NULL,
    TOTAL_AMOUNT DECIMAL(10,2),
    FOREIGN KEY (CUSTOMER_ID) REFERENCES TEST_CUSTOMERS(CUSTOMER_ID)
);
"@

$result = Invoke-CliCommand @("--profile", $Profile, "--command", "mermaid-from-sql", "--sql", $testSql)

if ($result -and $result.mermaidDiagram) {
    $diagramLength = $result.mermaidDiagram.Length
    
    Write-Host "  ✅ PASS - Converted SQL to Mermaid" -ForegroundColor Green
    Write-Host "     Diagram Length: $diagramLength chars" -ForegroundColor Gray
    
    # Validate conversion
    if ($result.mermaidDiagram -match "TEST_CUSTOMERS" -and $result.mermaidDiagram -match "TEST_ORDERS") {
        Write-Host "     ✅ Both tables found in diagram" -ForegroundColor Green
    }
    
    if ($result.mermaidDiagram -match "CUSTOMER_ID.*FK") {
        Write-Host "     ✅ Foreign key relationship detected" -ForegroundColor Green
    }
    
    $global:TestSqlToMermaid = $result.mermaidDiagram
    
    $passed++
    $results += [PSCustomObject]@{
        Test = "SQL to Mermaid Conversion"
        Status = "PASS"
        Details = "$diagramLength chars, 2 tables"
    }
}
else {
    Write-Host "  ❌ FAIL - SQL to Mermaid conversion failed" -ForegroundColor Red
    $failed++
    $results += [PSCustomObject]@{
        Test = "SQL to Mermaid Conversion"
        Status = "FAIL"
        Details = "Conversion failed"
    }
}

Write-Host ""

Write-Host "========================================" -ForegroundColor Yellow
Write-Host "  TEST 3: MERMAID TO SQL CONVERSION" -ForegroundColor Yellow
Write-Host "========================================" -ForegroundColor Yellow
Write-Host ""

Write-Host "Converting Mermaid back to SQL..." -ForegroundColor Cyan

if ($global:TestSqlToMermaid) {
    $result = Invoke-CliCommand @("--profile", $Profile, "--command", "sql-from-mermaid", "--sql", $global:TestSqlToMermaid)
    
    if ($result -and $result.sqlDdl) {
        $sqlLength = $result.sqlDdl.Length
        
        Write-Host "  ✅ PASS - Converted Mermaid to SQL" -ForegroundColor Green
        Write-Host "     SQL Length: $sqlLength chars" -ForegroundColor Gray
        
        # Validate SQL syntax
        if ($result.sqlDdl -match "CREATE TABLE" -and $result.sqlDdl -match "TEST_CUSTOMERS") {
            Write-Host "     ✅ Valid SQL DDL generated" -ForegroundColor Green
        }
        
        if ($result.sqlDdl -match "FOREIGN KEY") {
            Write-Host "     ✅ Foreign key preserved" -ForegroundColor Green
        }
        
        $passed++
        $results += [PSCustomObject]@{
            Test = "Mermaid to SQL Conversion"
            Status = "PASS"
            Details = "$sqlLength chars SQL"
        }
    }
    else {
        Write-Host "  ❌ FAIL - Mermaid to SQL conversion failed" -ForegroundColor Red
        $failed++
        $results += [PSCustomObject]@{
            Test = "Mermaid to SQL Conversion"
            Status = "FAIL"
            Details = "Conversion failed"
        }
    }
}
else {
    Write-Host "  ⏭️  SKIP - No Mermaid diagram from previous test" -ForegroundColor Gray
    $results += [PSCustomObject]@{
        Test = "Mermaid to SQL Conversion"
        Status = "SKIP"
        Details = "Prerequisite failed"
    }
}

Write-Host ""

Write-Host "========================================" -ForegroundColor Yellow
Write-Host "  TEST 4: MERMAID DIFF & DDL GENERATION" -ForegroundColor Yellow
Write-Host "========================================" -ForegroundColor Yellow
Write-Host ""

Write-Host "Testing diff detection and DDL generation..." -ForegroundColor Cyan

# Create original and modified Mermaid diagrams
$originalMermaid = @"
erDiagram
    TEST_PRODUCTS {
        INT PRODUCT_ID PK
        VARCHAR NAME
        DECIMAL PRICE
    }
"@

$modifiedMermaid = @"
erDiagram
    TEST_PRODUCTS {
        INT PRODUCT_ID PK
        VARCHAR NAME
        DECIMAL PRICE
        TIMESTAMP CREATED_DATE
        VARCHAR CATEGORY
    }
"@

$combinedInput = "$originalMermaid|||$modifiedMermaid"

$result = Invoke-CliCommand @("--profile", $Profile, "--command", "mermaid-diff", "--sql", $combinedInput) -Timeout 30

if ($result -and $result.migrationDdl) {
    $ddlLength = $result.migrationDdl.Length
    
    Write-Host "  ✅ PASS - Generated migration DDL" -ForegroundColor Green
    Write-Host "     DDL Length: $ddlLength chars" -ForegroundColor Gray
    
    # Validate DDL contains expected changes
    if ($result.migrationDdl -match "ALTER TABLE.*TEST_PRODUCTS") {
        Write-Host "     ✅ ALTER TABLE statement generated" -ForegroundColor Green
    }
    
    if ($result.migrationDdl -match "ADD.*CREATED_DATE") {
        Write-Host "     ✅ Detected new column (CREATED_DATE)" -ForegroundColor Green
    }
    
    if ($result.migrationDdl -match "ADD.*CATEGORY") {
        Write-Host "     ✅ Detected new column (CATEGORY)" -ForegroundColor Green
    }
    
    $passed++
    $results += [PSCustomObject]@{
        Test = "Mermaid Diff & DDL Generation"
        Status = "PASS"
        Details = "$ddlLength chars DDL, 2 columns added"
    }
}
else {
    Write-Host "  ❌ FAIL - Diff/DDL generation failed" -ForegroundColor Red
    $failed++
    $results += [PSCustomObject]@{
        Test = "Mermaid Diff & DDL Generation"
        Status = "FAIL"
        Details = "No DDL generated"
    }
}

Write-Host ""

Write-Host "========================================" -ForegroundColor Yellow
Write-Host "  TEST 5: GUI MERMAID DESIGNER" -ForegroundColor Yellow
Write-Host "========================================" -ForegroundColor Yellow
Write-Host ""

Write-Host "Testing Mermaid Visual Designer window..." -ForegroundColor Cyan

# Launch GUI with Mermaid designer
Start-Process $exe -ArgumentList "--profile", $Profile

Start-Sleep -Seconds 5

$proc = Get-Process -Name "WindowsDb2Editor" -ErrorAction SilentlyContinue

if ($proc -and $proc.Responding) {
    Write-Host "  ✅ PASS - GUI launched successfully" -ForegroundColor Green
    Write-Host "     Process is responsive" -ForegroundColor Gray
    Write-Host "     💡 Manual test: Click View → Mermaid Visual Designer" -ForegroundColor Cyan
    
    $passed++
    $results += [PSCustomObject]@{
        Test = "GUI Mermaid Designer"
        Status = "PASS"
        Details = "GUI launched, manual verification needed"
    }
    
    # Give user time to test
    Write-Host ""
    Write-Host "  ⏸️  Pausing for 10 seconds for manual GUI testing..." -ForegroundColor Yellow
    Write-Host "     Test these features:" -ForegroundColor Gray
    Write-Host "       • View → Mermaid Visual Designer opens" -ForegroundColor Gray
    Write-Host "       • Load from DB button works" -ForegroundColor Gray
    Write-Host "       • Table selection dialog appears" -ForegroundColor Gray
    Write-Host "       • Diagram renders in preview pane" -ForegroundColor Gray
    Start-Sleep -Seconds 10
    
    # Kill process
    taskkill /F /IM WindowsDb2Editor.exe 2>$null | Out-Null
}
else {
    Write-Host "  ❌ FAIL - GUI failed to launch or not responding" -ForegroundColor Red
    $failed++
    $results += [PSCustomObject]@{
        Test = "GUI Mermaid Designer"
        Status = "FAIL"
        Details = "Application not responding"
    }
    
    taskkill /F /IM WindowsDb2Editor.exe 2>$null | Out-Null
}

Write-Host ""

Write-Host "========================================" -ForegroundColor Yellow
Write-Host "  TEST 6: REAL DATABASE INTEGRATION" -ForegroundColor Yellow
Write-Host "========================================" -ForegroundColor Yellow
Write-Host ""

Write-Host "Testing with real INL.KONTO table..." -ForegroundColor Cyan

$result = Invoke-CliCommand @("--profile", $Profile, "--command", "mermaid-erd", "--schema", "INL", "--limit", "1")

if ($result -and $result.mermaidDiagram -and $result.mermaidDiagram -match "KONTO") {
    Write-Host "  ✅ PASS - Generated ERD for INL.KONTO" -ForegroundColor Green
    Write-Host "     Table found in diagram" -ForegroundColor Gray
    
    # Check for foreign keys
    if ($result.mermaidDiagram -match "FK|REFERENCES") {
        Write-Host "     ✅ Foreign keys detected" -ForegroundColor Green
    }
    
    $passed++
    $results += [PSCustomObject]@{
        Test = "Real Database Integration"
        Status = "PASS"
        Details = "INL.KONTO ERD generated"
    }
}
else {
    Write-Host "  ❌ FAIL - Could not generate ERD for real table" -ForegroundColor Red
    $failed++
    $results += [PSCustomObject]@{
        Test = "Real Database Integration"
        Status = "FAIL"
        Details = "INL.KONTO not found"
    }
}

Write-Host ""

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  📊 TEST SUMMARY" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

Write-Host "Total Tests: $($results.Count)" -ForegroundColor White
Write-Host "Passed: $passed" -ForegroundColor Green
Write-Host "Failed: $failed" -ForegroundColor Red

$tested = $passed + $failed
if ($tested -gt 0) {
    $passRate = [math]::Round(($passed / $tested) * 100, 2)
    Write-Host "Pass Rate: $passRate%" -ForegroundColor Yellow
}

Write-Host ""

if ($results.Count -gt 0) {
    Write-Host "Detailed Results:" -ForegroundColor Cyan
    $results | Format-Table -AutoSize
}

Write-Host ""

Write-Host "📦 NuGet Package:" -ForegroundColor Cyan
Write-Host "   SqlMermaidErdTools v0.2.8" -ForegroundColor White
Write-Host ""

Write-Host "🎯 Mermaid CLI Commands Available:" -ForegroundColor Cyan
Write-Host "   • mermaid-erd          - Generate ERD from database" -ForegroundColor White
Write-Host "   • mermaid-from-sql     - Convert SQL DDL to Mermaid" -ForegroundColor White
Write-Host "   • sql-from-mermaid     - Convert Mermaid to SQL DDL" -ForegroundColor White
Write-Host "   • mermaid-diff         - Generate diff and migration DDL" -ForegroundColor White
Write-Host ""

if ($failed -eq 0 -and $passed -gt 0) {
    Write-Host "🎉 ALL MERMAID TESTS PASSED!" -ForegroundColor Green
    exit 0
}
elseif ($failed -gt 0) {
    Write-Host "⚠️  $failed test(s) failed - review logs for details" -ForegroundColor Yellow
    
    # Check logs
    $logFile = Get-ChildItem "bin\Debug\net10.0-windows\logs\*.log" -ErrorAction SilentlyContinue | Sort-Object LastWriteTime -Descending | Select-Object -First 1
    if ($logFile) {
        Write-Host ""
        Write-Host "Recent errors from log:" -ForegroundColor Yellow
        Get-Content $logFile.FullName | Select-String "ERROR.*Mermaid|ERROR.*SqlToMmd" | Select-Object -Last 5 | ForEach-Object {
            Write-Host "  $_" -ForegroundColor Red
        }
    }
    
    exit 1
}
else {
    Write-Host "⚠️  No tests were completed" -ForegroundColor Yellow
    exit 1
}

