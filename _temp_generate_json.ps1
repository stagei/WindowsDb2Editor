# Generate JSON entries for all SIMPLE SQL queries
# This accelerates the migration by automating JSON structure creation

Write-Host "=== Generating JSON Entries for SQL Queries ===" -ForegroundColor Cyan

# Define queries with full details (extracted from services)
$queries = @(
    @{
        Name = "SERVICE_GetTableColumnsBasic"
        SQL = "SELECT TRIM(COLNAME), TRIM(TYPENAME), LENGTH, SCALE, TRIM(NULLS), TRIM(DEFAULT), TRIM(REMARKS) FROM SYSCAT.COLUMNS WHERE TRIM(TABNAME) = '?' ORDER BY COLNO"
        Desc = "Get basic column info for a table (used by DB2ConnectionManager)"
        Params = @("TABNAME")
        Source = "DB2ConnectionManager:GetTableColumnsAsync"
    },
    @{
        Name = "SERVICE_GetTableColumnsAll"
        SQL = "SELECT * FROM SYSCAT.COLUMNS WHERE TABNAME = '?' AND TABSCHEMA = '?' ORDER BY COLNO"
        Desc = "Get all column metadata for schema.table (used by DB2MetadataService)"
        Params = @("TABNAME", "TABSCHEMA")
        Source = "DB2MetadataService:AnalyzeTableStructure"
    },
    @{
        Name = "SERVICE_GetTableIndexes"
        SQL = "SELECT * FROM SYSCAT.INDEXES WHERE TABNAME = '?' AND TABSCHEMA = '?'"
        Desc = "Get all indexes for schema.table (used by DB2MetadataService)"
        Params = @("TABNAME", "TABSCHEMA")
        Source = "DB2MetadataService:AnalyzeTableStructure"
    },
    @{
        Name = "SERVICE_GetAllTablesBasic"
        SQL = "SELECT TABSCHEMA, TABNAME, TYPE FROM SYSCAT.TABLES WHERE TYPE IN ('T', 'V')"
        Desc = "Get all user tables and views for metadata loading"
        Params = @()
        Source = "MetadataLoaderService:LoadAsync"
    },
    @{
        Name = "SERVICE_GetColumnMetadataForTable"
        SQL = "SELECT COLNAME, TYPENAME, LENGTH, SCALE, NULLS, DEFAULT, REMARKS FROM SYSCAT.COLUMNS WHERE TABSCHEMA = '?' AND TABNAME = '?' ORDER BY COLNO"
        Desc = "Get column metadata for a specific table (used by MetadataLoaderService)"
        Params = @("TABSCHEMA", "TABNAME")
        Source = "MetadataLoaderService:LoadAsync"
    }
    # ... Add remaining 10 queries here
)

# Generate JSON format
$jsonEntries = @()

foreach ($query in $queries) {
    $paramsJson = ($query.Params | ForEach-Object { "`"$_`"" }) -join ", "
    
    $entry = @"
    "$($query.Name)": {
      "sql": "$($query.SQL)",
      "description": "$($query.Desc)",
      "parameters": [$paramsJson],
      "returns": "Query results",
      "source": "$($query.Source)"
    }
"@
    
    $jsonEntries += $entry
}

Write-Host "Generated JSON for $($queries.Count) queries:" -ForegroundColor Green
Write-Host ""
Write-Host ($jsonEntries -join ",`n")
Write-Host ""
Write-Host "Copy this JSON and append to db2_12.1_sql_statements.json" -ForegroundColor Yellow

