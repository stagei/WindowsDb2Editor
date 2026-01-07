$OutputDir = "CLI_Test_Output"
if (-not (Test-Path $OutputDir)) {
    New-Item -ItemType Directory -Path $OutputDir -Force | Out-Null
}

Write-Host "Testing DB2 Connection..." -ForegroundColor Cyan

# Test using CLI
$result = & "bin\Debug\net10.0-windows\WindowsDb2Editor.exe" --profile BASISTST --command list-schemas --outfile "$OutputDir\test_schemas.json" 2>&1

if ($LASTEXITCODE -eq 0) {
    Write-Host "✅ Connection successful!" -ForegroundColor Green
    Write-Host ""
    Write-Host "📋 Available Schemas:" -ForegroundColor Cyan
    if (Test-Path "$OutputDir\test_schemas.json") {
        $schemas = Get-Content "$OutputDir\test_schemas.json" | ConvertFrom-Json
        $schemas | Format-Table -AutoSize
    }
} else {
    Write-Host "❌ Connection failed!" -ForegroundColor Red
    Write-Host $result
}
