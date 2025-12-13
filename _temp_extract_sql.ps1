# Temporary script to extract all SIMPLE SQL queries from services
# This will help accelerate the migration to JSON

$files = @(
    @{Path="Data\DB2ConnectionManager.cs"; Prefix="CONN"},
    @{Path="Services\DB2MetadataService.cs"; Prefix="META"},
    @{Path="Services\MetadataLoaderService.cs"; Prefix="LOADER"},
    @{Path="Services\PackageAnalyzerService.cs"; Prefix="PKG"},
    @{Path="Services\SourceCodeService.cs"; Prefix="SRC"},
    @{Path="Services\TableRelationshipService.cs"; Prefix="REL"}
)

Write-Host "=== Extracting SQL from SIMPLE query files ===" -ForegroundColor Cyan
Write-Host ""

$allQueries = @()
$queryId = 1

foreach ($file in $files) {
    $content = Get-Content $file.Path -Raw
    $matches = [regex]::Matches($content, 'var (\w+) = \$@"([^"]+)"', [System.Text.RegularExpressions.RegexOptions]::Singleline)
    
    foreach ($match in $matches) {
        $varName = $match.Groups[1].Value
        $sqlContent = $match.Groups[2].Value.Trim()
        
        # Only include if it contains SELECT, INSERT, UPDATE, DELETE, CREATE, ALTER, DROP
        if ($sqlContent -match '(?i)(SELECT|INSERT|UPDATE|DELETE|CREATE|ALTER|DROP)') {
            $queryName = "$($file.Prefix)_$varName"
            
            Write-Host "Found: $queryName" -ForegroundColor Green
            Write-Host "  File: $($file.Path)"
            Write-Host "  SQL: $($sqlContent.Substring(0, [Math]::Min(80, $sqlContent.Length)))..."
            Write-Host ""
            
            $allQueries += @{
                Name = $queryName
                SQL = $sqlContent
                File = $file.Path
                VarName = $varName
            }
            
            $queryId++
        }
    }
}

Write-Host "Total queries extracted: $($allQueries.Count)" -ForegroundColor Cyan
Write-Host "Ready to add to JSON!" -ForegroundColor Yellow

