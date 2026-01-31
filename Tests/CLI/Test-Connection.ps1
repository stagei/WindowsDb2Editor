param(
    [string]$Profile,
    [ValidateSet('', 'DB2', 'PostgreSQL')]
    [string]$Provider
)

$testsRoot = Split-Path $PSScriptRoot -Parent
$projectRoot = Split-Path $testsRoot -Parent

# Use suite log path when set (Run-TestSuite); otherwise create a dedicated log for this script
$logDir = Join-Path $projectRoot "CLI_Test_Output"
. (Join-Path $testsRoot "TestLogger.ps1") -LogDir $logDir -LogPrefix "connection_test"
Start-TestLog -Title "Connection test" -LogDir $logDir -LogPrefix "connection_test" | Out-Null

. (Join-Path $testsRoot "TestConfig.ps1") -Profile $Profile -Provider $Provider
$Profile = Get-WdeTestProfile
$providerLabel = if (Get-WdeTestProvider) { " [$(Get-WdeTestProvider)]" } else { "" }

Write-TestLog "Testing connection (profile: $($Profile))$($providerLabel)..." -Level INFO

$OutputDir = Join-Path $projectRoot "CLI_Test_Output"
if (-not (Test-Path $OutputDir)) {
    New-Item -ItemType Directory -Path $OutputDir -Force | Out-Null
}

# Test using CLI - works with any provider (DB2, PostgreSQL, etc.)
$exePath = Join-Path $projectRoot "bin\Debug\net10.0-windows\WindowsDb2Editor.exe"
$outFile = Join-Path $OutputDir "test_schemas.json"
$result = & $exePath --profile $Profile --command list-schemas --outfile $outFile 2>&1

if ($LASTEXITCODE -eq 0) {
    Write-TestLog "Connection successful (profile: $Profile)." -Level PASS
    if (Test-Path $outFile) {
        $schemas = Get-Content $outFile -Raw | ConvertFrom-Json
        $count = if ($schemas -is [Array]) { $schemas.Count } else { @($schemas).Count }
        Write-TestLog "Schemas returned: $count" -Level INFO
        $schemas | Format-Table -AutoSize | Out-String | ForEach-Object { Write-TestLog $_ -Level DEBUG -NoConsole }
    }
} else {
    Write-TestLog "Connection failed (profile: $Profile). ExitCode: $LASTEXITCODE" -Level FAIL
    Write-TestLog ($result | Out-String) -Level ERROR
}
