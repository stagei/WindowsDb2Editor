# CLI Extension for Automated Testing - Design Document

**Date**: December 13, 2025  
**Status**: ⚠️ ARCHITECTURE COMPLETE - IMPLEMENTATION IN PROGRESS  
**Purpose**: Enable Cursor AI to perform automated testing via CLI

---

## 🎯 OBJECTIVE

Extend DbExplorer's CLI to expose all GUI functionality as testable commands with structured JSON output, enabling:
- Automated regression testing
- Schema validation
- Performance monitoring
- Cross-version comparison
- CI/CD integration

---

## 📋 REQUIREMENTS

### User Request Summary:
> "Make all functions like table properties, trigger code, trigger usage, view info, usage etc. available by triggering the function using a connection profile, launching the form with a parameter, to retrieve all data in the form in a structured way, and export it as a structured JSON file, to automatically build automated tests."

### Key Requirements:
1. **Command-based CLI execution** (not just SQL queries)
2. **Structured JSON output** (not CSV/TSV)
3. **All GUI features accessible** (table props, triggers, views, etc.)
4. **Connection profile-based** (reuse existing profiles)
5. **Automated testing-friendly** (predictable output format)

---

## 🏗️ ARCHITECTURE

### Current CLI (Legacy):
```bash
DbExplorer.exe -Profile "MYDB" -Sql "SELECT ..." -Outfile result.json
```

### New CLI (Command-Based):
```bash
DbExplorer.exe -Profile "MYDB" -Command table-props -Object "SCHEMA.TABLE" -Outfile result.json
```

### Command Structure:
```
┌─────────────┐
│  CLI Args   │
└──────┬──────┘
       │
       ├── Legacy Mode: -Sql <query> → Execute SQL → Export (JSON/CSV/TSV/XML)
       │
       └── Command Mode: -Command <cmd> → Service Call → JSON Export
                          └── Commands:
                              ├── table-props
                              ├── trigger-info
                              ├── view-info
                              ├── procedure-info
                              ├── lock-monitor
                              ├── active-sessions
                              ├── database-load
                              └── ... (15 total)
```

---

## 📝 NEW CLI PARAMETERS

### Extended `CliArguments` Class:
```csharp
public class CliArguments
{
    // Existing
    public string? ProfileName { get; set; }
    public string? Sql { get; set; }
    public string? OutFile { get; set; }
    public string? Format { get; set; } = "json";
    
    // NEW - Command-based execution
    public string? Command { get; set; }  // table-props, trigger-info, etc.
    public string? Object { get; set; }   // SCHEMA.TABLE, SCHEMA.VIEW
    public string? Schema { get; set; }   // Schema filter
    public string? ObjectType { get; set; } // TABLE, VIEW, PROCEDURE
    public int? Limit { get; set; }       // Result limit
    public bool IncludeDependencies { get; set; }
    public bool IncludeSourceCode { get; set; }
}
```

---

## 🎬 AVAILABLE COMMANDS

### Object Information Commands

#### 1. `table-props` - Table Properties
**Purpose**: Get comprehensive table metadata  
**Parameters**: `-Object SCHEMA.TABLE`  
**Optional**: `-IncludeDependencies` (includes statistics)

**Output Structure**:
```json
{
  "schema": "MYSCHEMA",
  "tableName": "CUSTOMERS",
  "columns": [
    {
      "columnName": "CUSTOMER_ID",
      "dataType": "INTEGER",
      "length": 4,
      "scale": 0,
      "isNullable": false,
      "defaultValue": null,
      "comment": "Primary key",
      "isIdentity": true
    },
    {
      "columnName": "NAME",
      "dataType": "VARCHAR",
      "length": 100,
      "scale": 0,
      "isNullable": false,
      "defaultValue": null,
      "comment": "Customer name",
      "isIdentity": false
    }
  ],
  "primaryKeys": ["CUSTOMER_ID"],
  "foreignKeys": [
    {
      "constraintName": "FK_CUST_ADDR",
      "columns": "ADDRESS_ID",
      "referencedSchema": "MYSCHEMA",
      "referencedTable": "ADDRESSES",
      "referencedColumns": "ADDRESS_ID"
    }
  ],
  "indexes": [
    {
      "indexName": "IDX_CUST_NAME",
      "isUnique": false,
      "columns": "+NAME",
      "indexType": "CLUS"
    }
  ],
  "statistics": {
    "rowCount": 15432,
    "pages": 2048,
    "freePages": 128,
    "overflowPages": 0,
    "lastStatsTime": "2025-12-01 10:30:00"
  },
  "retrievedAt": "2025-12-13T15:30:00Z"
}
```

**Example Command**:
```bash
DbExplorer.exe -Profile "PRODDB" -Command table-props -Object "MYSCHEMA.CUSTOMERS" -IncludeDependencies -Outfile table.json
```

---

#### 2. `trigger-info` - Trigger Details
**Purpose**: Get trigger metadata and source code  
**Parameters**: `-Object SCHEMA.TRIGGER`  
**Optional**: `-IncludeSourceCode`

**Output Structure**:
```json
{
  "schema": "MYSCHEMA",
  "triggerName": "TRG_AUDIT_CUSTOMERS",
  "targetSchema": "MYSCHEMA",
  "targetTable": "CUSTOMERS",
  "triggerTime": "AFTER",
  "triggerEvent": "UPDATE",
  "granularity": "ROW",
  "comment": "Audit trail for customer updates",
  "sourceCode": "CREATE TRIGGER TRG_AUDIT_CUSTOMERS\nAFTER UPDATE ON CUSTOMERS\nFOR EACH ROW\nBEGIN\n  INSERT INTO AUDIT_LOG ...\nEND",
  "retrievedAt": "2025-12-13T15:30:00Z"
}
```

**Example Command**:
```bash
DbExplorer.exe -Profile "PRODDB" -Command trigger-info -Object "MYSCHEMA.TRG_AUDIT_CUSTOMERS" -IncludeSourceCode -Outfile trigger.json
```

---

#### 3. `trigger-usage` - Find All Triggers
**Purpose**: List all triggers in schema and their usage  
**Parameters**: `-Schema MYSCHEMA` (optional, default: all schemas)

**Output Structure**:
```json
{
  "schema": "MYSCHEMA",
  "triggerCount": 12,
  "triggers": [
    {
      "triggerSchema": "MYSCHEMA",
      "triggerName": "TRG_AUDIT_CUSTOMERS",
      "targetSchema": "MYSCHEMA",
      "targetTable": "CUSTOMERS",
      "triggerTime": "AFTER",
      "triggerEvent": "UPDATE",
      "isEnabled": true
    },
    {
      "triggerSchema": "MYSCHEMA",
      "triggerName": "TRG_VALIDATE_ORDER",
      "targetSchema": "MYSCHEMA",
      "targetTable": "ORDERS",
      "triggerTime": "BEFORE",
      "triggerEvent": "INSERT",
      "isEnabled": true
    }
  ],
  "retrievedAt": "2025-12-13T15:30:00Z"
}
```

**Example Command**:
```bash
DbExplorer.exe -Profile "PRODDB" -Command trigger-usage -Schema "MYSCHEMA" -Outfile triggers.json
```

---

#### 4. `view-info` - View Definition
**Purpose**: Get view metadata, definition, and dependencies  
**Parameters**: `-Object SCHEMA.VIEW`  
**Optional**: `-IncludeSourceCode`, `-IncludeDependencies`

**Output Structure**:
```json
{
  "schema": "MYSCHEMA",
  "viewName": "V_CUSTOMER_ORDERS",
  "viewCheck": "NONE",
  "isReadOnly": false,
  "isValid": true,
  "comment": "Customer orders with details",
  "sourceCode": "CREATE VIEW V_CUSTOMER_ORDERS AS\nSELECT c.CUSTOMER_ID, c.NAME, o.ORDER_ID, o.ORDER_DATE\nFROM CUSTOMERS c\nJOIN ORDERS o ON c.CUSTOMER_ID = o.CUSTOMER_ID",
  "dependencies": {
    "dependsOn": [
      "MYSCHEMA.CUSTOMERS",
      "MYSCHEMA.ORDERS"
    ],
    "usedBy": [
      "MYSCHEMA.SP_GET_CUSTOMER_REPORT"
    ]
  },
  "retrievedAt": "2025-12-13T15:30:00Z"
}
```

**Example Command**:
```bash
DbExplorer.exe -Profile "PRODDB" -Command view-info -Object "MYSCHEMA.V_CUSTOMER_ORDERS" -IncludeSourceCode -IncludeDependencies -Outfile view.json
```

---

### Monitoring Commands

#### 5. `lock-monitor` - Current Database Locks
**Purpose**: Get all current locks for monitoring  
**Parameters**: None

**Output Structure**:
```json
{
  "lockCount": 3,
  "locks": [
    {
      "lockName": "LOCK_12345",
      "lockType": "X",
      "lockStatus": "GRANTED",
      "holder": "APP1",
      "waiter": null,
      "objectType": "TABLE",
      "objectName": "MYSCHEMA.CUSTOMERS",
      "duration": "00:02:15"
    },
    {
      "lockName": "LOCK_12346",
      "lockType": "S",
      "lockStatus": "WAITING",
      "holder": "APP2",
      "waiter": "APP3",
      "objectType": "ROW",
      "objectName": "MYSCHEMA.ORDERS",
      "duration": "00:00:45"
    }
  ],
  "retrievedAt": "2025-12-13T15:30:00Z"
}
```

**Example Command**:
```bash
DbExplorer.exe -Profile "PRODDB" -Command lock-monitor -Outfile locks.json
```

---

#### 6. `active-sessions` - Database Sessions
**Purpose**: List all active database connections  
**Parameters**: `-Limit N` (optional)

**Output Structure**:
```json
{
  "totalSessions": 47,
  "limitApplied": 20,
  "sessions": [
    {
      "applicationName": "MyApp",
      "user": "DBUSER1",
      "authId": "DBUSER1",
      "status": "ACTIVE",
      "executionTime": "00:05:23",
      "clientIpAddress": "192.168.1.100"
    }
  ],
  "retrievedAt": "2025-12-13T15:30:00Z"
}
```

**Example Command**:
```bash
DbExplorer.exe -Profile "PRODDB" -Command active-sessions -Limit 20 -Outfile sessions.json
```

---

#### 7. `database-load` - Table Activity Metrics
**Purpose**: Monitor table-level activity  
**Parameters**: `-Schema MYSCHEMA` (optional), `-Limit N`

**Output Structure**:
```json
{
  "schema": "MYSCHEMA",
  "totalTables": 45,
  "limitApplied": 10,
  "metrics": [
    {
      "schema": "MYSCHEMA",
      "tableName": "CUSTOMERS",
      "rowsRead": 15432,
      "rowsInserted": 234,
      "rowsUpdated": 456,
      "rowsDeleted": 12,
      "lastActivityTime": "2025-12-13 15:29:45"
    }
  ],
  "retrievedAt": "2025-12-13T15:30:00Z"
}
```

**Example Command**:
```bash
DbExplorer.exe -Profile "PRODDB" -Command database-load -Schema "MYSCHEMA" -Limit 10 -Outfile load.json
```

---

### Listing Commands

#### 8. `list-tables` - All Tables in Schema
**Output Structure**:
```json
{
  "schema": "MYSCHEMA",
  "totalTables": 45,
  "limitApplied": 100,
  "tables": [
    {
      "schema": "MYSCHEMA",
      "tableName": "CUSTOMERS",
      "type": "T",
      "rowCount": 15432,
      "pages": 2048
    }
  ],
  "retrievedAt": "2025-12-13T15:30:00Z"
}
```

---

## 🧪 AUTOMATED TESTING WORKFLOW

### 1. Setup Phase
```bash
# Create connection profiles in GUI (one-time)
# Profiles stored in: %LOCALAPPDATA%\WindowsDb2Editor\connection_profiles.json
```

### 2. Test Execution Phase
```bash
# Test 1: Verify table structure
DbExplorer.exe -Profile "TESTDB" -Command table-props -Object "MYSCHEMA.CUSTOMERS" -Outfile test_table.json

# Test 2: Verify all triggers exist
DbExplorer.exe -Profile "TESTDB" -Command trigger-usage -Schema "MYSCHEMA" -Outfile test_triggers.json

# Test 3: Verify view definitions
DbExplorer.exe -Profile "TESTDB" -Command view-info -Object "MYSCHEMA.V_ORDERS" -IncludeSourceCode -Outfile test_view.json

# Test 4: Monitor database health
DbExplorer.exe -Profile "PRODDB" -Command lock-monitor -Outfile prod_locks.json
DbExplorer.exe -Profile "PRODDB" -Command database-load -Schema "MYSCHEMA" -Limit 20 -Outfile prod_load.json
```

### 3. Validation Phase (PowerShell/Python)
```powershell
# Parse JSON and assert expected structure
$tableProps = Get-Content test_table.json | ConvertFrom-Json

# Assert primary key exists
if ($tableProps.primaryKeys -contains "CUSTOMER_ID") {
    Write-Host "✅ Primary key correct"
} else {
    Write-Host "❌ Primary key missing"
    exit 1
}

# Assert column count
if ($tableProps.columns.Count -eq 10) {
    Write-Host "✅ Column count correct"
} else {
    Write-Host "❌ Expected 10 columns, got $($tableProps.columns.Count)"
    exit 1
}

# Assert trigger count
$triggers = Get-Content test_triggers.json | ConvertFrom-Json
if ($triggers.triggerCount -eq 5) {
    Write-Host "✅ Trigger count correct"
} else {
    Write-Host "❌ Expected 5 triggers, got $($triggers.triggerCount)"
    exit 1
}
```

---

## 🎯 USE CASES

### Use Case 1: Schema Validation
**Scenario**: Verify production schema matches expected structure

```bash
# Get production table structure
DbExplorer.exe -Profile "PROD" -Command table-props -Object "SCHEMA.TABLE" -IncludeDependencies -Outfile prod_table.json

# Get test table structure
DbExplorer.exe -Profile "TEST" -Command table-props -Object "SCHEMA.TABLE" -IncludeDependencies -Outfile test_table.json

# Compare JSON files (PowerShell/Python script)
# Assert: same columns, same data types, same constraints
```

---

### Use Case 2: Regression Testing
**Scenario**: Ensure no breaking changes after migration

```bash
# Before migration
DbExplorer.exe -Profile "DB_V1" -Command list-tables -Schema "APP" -Outfile before.json
DbExplorer.exe -Profile "DB_V1" -Command trigger-usage -Schema "APP" -Outfile triggers_before.json

# After migration
DbExplorer.exe -Profile "DB_V2" -Command list-tables -Schema "APP" -Outfile after.json
DbExplorer.exe -Profile "DB_V2" -Command trigger-usage -Schema "APP" -Outfile triggers_after.json

# Compare: table count, trigger count, view count
```

---

### Use Case 3: Performance Monitoring
**Scenario**: Continuous monitoring in production

```bash
# Cron job / Windows Task Scheduler (every 5 minutes)
DbExplorer.exe -Profile "PROD" -Command lock-monitor -Outfile "logs\locks_$(Get-Date -Format 'yyyyMMdd_HHmmss').json"
DbExplorer.exe -Profile "PROD" -Command active-sessions -Limit 100 -Outfile "logs\sessions_$(Get-Date -Format 'yyyyMMdd_HHmmss').json"
DbExplorer.exe -Profile "PROD" -Command database-load -Schema "APP" -Limit 50 -Outfile "logs\load_$(Get-Date -Format 'yyyyMMdd_HHmmss').json"

# Alert if:
# - Lock count > 10
# - Session count > 50
# - Any table with > 1M rows read/minute
```

---

### Use Case 4: Documentation Generation
**Scenario**: Auto-generate schema documentation

```bash
# Export all tables
DbExplorer.exe -Profile "PROD" -Command list-tables -Schema "APP" -Outfile schema_tables.json

# For each table, get detailed properties
foreach table in tables:
    DbExplorer.exe -Profile "PROD" -Command table-props -Object "$schema.$table" -IncludeDependencies -Outfile "docs/table_$table.json"

# Generate Markdown documentation from JSON
python generate_docs.py --input docs/*.json --output schema_documentation.md
```

---

## 📊 BENEFITS

### For Cursor AI:
✅ **Automated Testing**: Run regression tests via CLI  
✅ **Schema Validation**: Compare expected vs actual structure  
✅ **Continuous Monitoring**: Scheduled health checks  
✅ **Data-Driven Tests**: JSON output = easy assertions

### For Developers:
✅ **CI/CD Integration**: Add to build pipelines  
✅ **Version Comparison**: Compare schemas across versions  
✅ **Documentation**: Auto-generate from JSON  
✅ **Debugging**: Export state for troubleshooting

### For DBAs:
✅ **Monitoring**: Track locks, sessions, load  
✅ **Auditing**: Record schema changes over time  
✅ **Performance**: Identify hot tables  
✅ **Migration**: Validate post-migration state

---

## ⚠️ IMPLEMENTATION STATUS

### ✅ Completed:
- CLI argument parsing extended
- Architecture designed
- Command structure defined
- JSON output format specified
- Documentation created
- Help text updated

### ⚠️ In Progress:
- Service API alignment (constructor parameters)
- Method signature compatibility
- Filter object creation for monitoring services

### ❌ Pending:
- Full compilation and testing
- Example JSON validation
- PowerShell test harness
- CI/CD integration examples

---

## 🔧 TECHNICAL NOTES

### Service API Alignment Needed:

1. **ObjectBrowserService** - Requires `DB2ConnectionManager` in constructor
2. **SourceCodeService** - Method names may differ
3. **LockMonitorService** - Requires `LockMonitorFilter` parameter
4. **SessionMonitorService** - Requires `SessionMonitorFilter` parameter  
5. **DatabaseLoadMonitorService** - Requires `LoadMonitorFilter` parameter
6. **StatisticsService** - Requires `StatisticsFilter` parameter
7. **DependencyAnalyzerService** - Method signatures need verification

### Recommended Approach:
1. **Option A**: Align service APIs to match CLI needs
2. **Option B**: Create CLI-specific adapter methods
3. **Option C**: Use raw SQL queries for MVP, refactor later

---

## 📝 EXAMPLE TEST SCRIPT

### PowerShell Test Harness:
```powershell
# test_schema.ps1
param(
    [string]$Profile = "TESTDB",
    [string]$Schema = "MYSCHEMA"
)

$ErrorActionPreference = "Stop"

Write-Host "🧪 Running DB Schema Tests..." -ForegroundColor Cyan

# Test 1: Table count
Write-Host "`n📊 Test 1: Table Count" -ForegroundColor Yellow
.\DbExplorer.exe -Profile $Profile -Command list-tables -Schema $Schema -Outfile temp_tables.json
$tables = Get-Content temp_tables.json | ConvertFrom-Json

if ($tables.totalTables -eq 45) {
    Write-Host "✅ PASS: Expected 45 tables, got $($tables.totalTables)" -ForegroundColor Green
} else {
    Write-Host "❌ FAIL: Expected 45 tables, got $($tables.totalTables)" -ForegroundColor Red
    exit 1
}

# Test 2: CUSTOMERS table structure
Write-Host "`n📋 Test 2: CUSTOMERS Table" -ForegroundColor Yellow
.\DbExplorer.exe -Profile $Profile -Command table-props -Object "$Schema.CUSTOMERS" -Outfile temp_customers.json
$table = Get-Content temp_customers.json | ConvertFrom-Json

if ($table.columns.Count -eq 10) {
    Write-Host "✅ PASS: Expected 10 columns" -ForegroundColor Green
} else {
    Write-Host "❌ FAIL: Expected 10 columns, got $($table.columns.Count)" -ForegroundColor Red
    exit 1
}

if ($table.primaryKeys -contains "CUSTOMER_ID") {
    Write-Host "✅ PASS: Primary key correct" -ForegroundColor Green
} else {
    Write-Host "❌ FAIL: Primary key missing or incorrect" -ForegroundColor Red
    exit 1
}

# Test 3: Trigger count
Write-Host "`n🔔 Test 3: Trigger Count" -ForegroundColor Yellow
.\DbExplorer.exe -Profile $Profile -Command trigger-usage -Schema $Schema -Outfile temp_triggers.json
$triggers = Get-Content temp_triggers.json | ConvertFrom-Json

if ($triggers.triggerCount -eq 5) {
    Write-Host "✅ PASS: Expected 5 triggers, got $($triggers.triggerCount)" -ForegroundColor Green
} else {
    Write-Host "❌ FAIL: Expected 5 triggers, got $($triggers.triggerCount)" -ForegroundColor Red
    exit 1
}

# Cleanup
Remove-Item temp_*.json -ErrorAction SilentlyContinue

Write-Host "`n✅ All tests passed!" -ForegroundColor Green
exit 0
```

**Usage**:
```bash
# Run tests
.\test_schema.ps1 -Profile "TESTDB" -Schema "MYSCHEMA"

# CI/CD integration
if (.\test_schema.ps1) {
    Write-Host "Tests passed, deploying..."
    # Deploy
} else {
    Write-Host "Tests failed, aborting deployment"
    exit 1
}
```

---

## 🎉 SUMMARY

This CLI extension transforms DbExplorer into a **testable, automatable database tool** that Cursor AI can use for:
- ✅ Regression testing
- ✅ Schema validation
- ✅ Performance monitoring
- ✅ Documentation generation
- ✅ CI/CD integration

**Next Steps**:
1. Fix service API alignment issues
2. Complete implementation
3. Test with real database
4. Create comprehensive test suite
5. Document best practices

---

**Document Date**: December 13, 2025  
**Status**: ARCHITECTURE COMPLETE - AWAITING API FIXES  
**Estimated Completion**: 2-4 hours (after API alignment)

