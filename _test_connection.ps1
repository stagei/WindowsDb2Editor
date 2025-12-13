Write-Host "Testing DB2 Connection..." -ForegroundColor Cyan

# Test using CLI
$result = & "bin\Debug\net10.0-windows\WindowsDb2Editor.exe" --profile BASISTST --command list-schemas --outfile test_schemas.json 2>&1

if ($LASTEXITCODE -eq 0) {
    Write-Host "✅ Connection successful!" -ForegroundColor Green
    Write-Host ""
    Write-Host "📋 Available Schemas:" -ForegroundColor Cyan
    if (Test-Path "test_schemas.json") {
        $schemas = Get-Content "test_schemas.json" | ConvertFrom-Json
        $schemas | Format-Table -AutoSize
    }
} else {
    Write-Host "❌ Connection failed!" -ForegroundColor Red
    Write-Host $result
}
