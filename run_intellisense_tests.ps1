#Requires -Version 7.0
<#
.SYNOPSIS
    Runs the IntelliSense test suite for WindowsDb2Editor.

.DESCRIPTION
    Executes 110+ automated tests for the SQL IntelliSense system:
    - Scope parsing
    - Alias extraction
    - Exposed column detection
    - Nested subselect handling
    - Correlated subqueries
    - Real-world query patterns
    - DB2-specific syntax
    - Edge cases

.EXAMPLE
    .\run_intellisense_tests.ps1
    
.EXAMPLE
    .\run_intellisense_tests.ps1 -Verbose
#>

param(
    [switch]$Verbose
)

$ErrorActionPreference = 'Stop'
$ProjectRoot = $PSScriptRoot

Write-Host "`n🧪 WindowsDb2Editor IntelliSense Test Suite" -ForegroundColor Cyan
Write-Host "=" * 50

# Build the test project
Write-Host "`n📦 Building test project..." -ForegroundColor Yellow
$buildResult = & dotnet build "$ProjectRoot\Tests\IntelliSenseTests.csproj" -c Debug 2>&1

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Build failed!" -ForegroundColor Red
    if ($Verbose) {
        Write-Host $buildResult -ForegroundColor Gray
    }
    exit 1
}

Write-Host "✅ Build successful" -ForegroundColor Green

# Run the tests
Write-Host "`n🏃 Running tests..." -ForegroundColor Yellow
& dotnet run --project "$ProjectRoot\Tests\IntelliSenseTests.csproj" --no-build

$exitCode = $LASTEXITCODE

if ($exitCode -eq 0) {
    Write-Host "`n✅ All tests passed!" -ForegroundColor Green
} else {
    Write-Host "`n❌ Some tests failed!" -ForegroundColor Red
}

exit $exitCode
