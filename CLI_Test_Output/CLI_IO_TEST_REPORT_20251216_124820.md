# CLI Test Report with Input/Output Capture

**Generated**: 2025-12-16 12:48:30  
**Connection Profile**: FKKTOTST  
**Total Tests**: 5  
**âœ… Passed**: 5 (100%)  
**âŒ Failed**: 0 (0%)  

---

## Executive Summary

| Metric | Value |
|--------|-------|
| Total Commands Tested | 5 |
| Passed | 5 |
| Failed | 0 |
| Success Rate | 100% |
| Test Duration | ~0 seconds |

---

## Results by Category

### âœ…  (5/5 passed)

| # | Command | Description | Status | Duration | Exit Code |
|---|---------|-------------|--------|----------|-----------|| 1 | `connection-test` | Test database connection | âœ… PASS | 2466ms | 0 |
| 2 | `connection-stats` | Get connection statistics | âœ… PASS | 3124ms | 0 |
| 3 | `help-all` | Show all available commands | âœ… PASS | 1214ms | 0 |
| 4 | `cli-version` | Show CLI version | âœ… PASS | 822ms | 0 |
| 5 | `db-config` | Get database configuration | âœ… PASS | 2420ms | 0 |

---

## Detailed Test Results with Input/Output

Each test case below shows:
- **CLI Input**: The exact command line executed
- **Console Output**: What was printed to the console (logs, errors)
- **JSON Output**: The structured data returned by the command

---

### âœ… Test #1: connection-test

**Category**: Connection & Info  
**Description**: Test database connection  
**Status**: âœ… PASSED  
**Duration**: 2466 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 12:48:22  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command connection-test -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 12:48:21.3165|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 12:48:21.3320|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 12:48:21.3320|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 12:48:21.3320|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 12:48:21.3320|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 12:48:21.3320|DEBUG|WindowsDb2Editor.App|Startup arguments: -command connection-test -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_connection-test_20251216_124820.json
2025-12-16 12:48:21.3320|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 12:48:21.3320|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 12:48:21.3490|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 12:48:21.3490|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 12:48:21.3856|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 12:48:21.3856|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 12:48:21.3856|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 12:48:21.3856|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 12:48:21.3993|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 12:48:21.3993|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 12:48:21.3993|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading 
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "connection-test",
  "profileName": "FKKTOTST",
  "testResult": "SUCCESS",
  "message": "Connection opened successfully",
  "retrievedAt": "2025-12-16T12:48:22.7806257+01:00"
}
```

---

### âœ… Test #2: connection-stats

**Category**: Connection & Info  
**Description**: Get connection statistics  
**Status**: âœ… PASSED  
**Duration**: 3124 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 12:48:26  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command connection-stats -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 12:48:23.8820|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 12:48:23.8820|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 12:48:23.8940|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 12:48:23.8940|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 12:48:23.8940|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 12:48:23.8940|DEBUG|WindowsDb2Editor.App|Startup arguments: -command connection-stats -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_connection-stats_20251216_124820.json
2025-12-16 12:48:23.8940|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 12:48:23.8940|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 12:48:23.8940|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 12:48:23.8940|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 12:48:23.9312|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 12:48:23.9312|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 12:48:23.9312|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 12:48:23.9312|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 12:48:23.9413|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 12:48:23.9413|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 12:48:23.9413|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "totalProfiles": 2,
  "note": "Connection stats based on saved profiles",
  "retrievedAt": "2025-12-16T12:48:26.0115543+01:00"
}
```

---

### âœ… Test #3: help-all

**Category**: Connection & Info  
**Description**: Show all available commands  
**Status**: âœ… PASSED  
**Duration**: 1214 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 12:48:27  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command help-all -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 12:48:26.9426|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 12:48:26.9621|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 12:48:26.9621|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 12:48:26.9621|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 12:48:26.9621|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 12:48:26.9621|DEBUG|WindowsDb2Editor.App|Startup arguments: -command help-all -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_help-all_20251216_124820.json
2025-12-16 12:48:26.9621|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 12:48:26.9621|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 12:48:26.9621|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 12:48:26.9769|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 12:48:27.0082|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 12:48:27.0082|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 12:48:27.0097|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 12:48:27.0097|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 12:48:27.0097|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 12:48:27.0097|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 12:48:27.0097|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for D
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "totalCommands": 91,
  "categoryCount": 22,
  "categories": [
    {
      "category": "Advanced Monitoring",
      "commandCount": 8,
      "commands": [
        {
          "command": "database-load-full",
          "category": "Advanced Monitoring",
          "description": "Complete load metrics"
        },
        {
          "command": "table-activity",
          "category": "Advanced Monitoring",
          "description": "Table activity metrics"
        },
        {
          "command": "top-active-tables",
          "category": "Advanced Monitoring",
          "description": "Top N active tables"
        },
        {
          "command": "lock-monitor-full",
          "category": "Advanced Monitoring",
          "description": "Complete lock info"
        },
        {
          "command": "lock-chains",
          "category": "Advanced Monitoring",
          "description": "Lock wait chains"
        },
        {
          "command": "active-sessions-full",
          "category": "Advanced Monitoring",
          "description": "Complete session info"
        },
        {
          "command": "session-details",
          "category": "Advanced Monitoring",
          "description": "Detailed session info"
        },
        {
          "command": "long-running-sessions",
          "category": "Advanced Monitoring",
          "description": "Long-running sessions"
        }
      ]
    },
    {
      "category": "CDC",
      "commandCount": 3,
      "commands": [
        {
          "command": "cdc-status-full",
          "category": "CDC",
          "description": "Complete CDC status"
        },
        {
          "command": "cdc-configuration",
          "category": "CDC",
          "description": "CDC configuration"
        },
        {
          "command": "cdc-changes",
          "category": "CDC",
          "description": "CDC changes"
        }
      ]
    },
    {
      "category": "Comments",
      "commandCount": 3,
      "commands": [
        {
          "command": "list-comments",
          "category": "Comments",
          "description": "List objects with/without comments"
        },
        {
          "command": "object-comment",
          "category": "Comments",
          "description": "Get object comment"
        },
        {
          "command": "missing-comments",
          "category": "Comments",
          "description": "Find uncommented objects"
        }
      ]
    },
    {
      "category": "Connection",
      "commandCount": 3,
      "commands": [
        {
          "command": "connection-profiles",
          "category": "Connection",
          "description": "List connection profiles"
        },
        {
          "command": "connection-stats",
          "category": "Connection",
          "description": "Connection statistics"
        },
        {
          "command": "connection-test",
          "categ
... (truncated)
```

---

### âœ… Test #4: cli-version

**Category**: Connection & Info  
**Description**: Show CLI version  
**Status**: âœ… PASSED  
**Duration**: 822 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 12:48:28  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cli-version -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 12:48:27.8482|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 12:48:27.8482|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 12:48:27.8482|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 12:48:27.8482|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 12:48:27.8482|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 12:48:27.8482|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cli-version -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_cli-version_20251216_124820.json
2025-12-16 12:48:27.8482|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 12:48:27.8482|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 12:48:27.8748|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 12:48:27.8748|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 12:48:27.9112|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 12:48:27.9112|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 12:48:27.9112|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 12:48:27.9112|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 12:48:27.9112|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 12:48:27.9263|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 12:48:27.9263|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "applicationName": "DbExplorer CLI",
  "cliVersion": "1.0.0",
  "framework": ".NET 10.0",
  "totalCommands": 94,
  "implementationDate": "2025-12-14",
  "features": [
    "94 CLI commands for automated testing",
    "Structured JSON output",
    "DB2 12.1 compatibility",
    "Read-only operations (no DML)",
    "Mermaid ERD integration (SqlMermaidErdTools)",
    "Multi-provider support (DB2 primary)",
    "AI Assistant integration (Natural Language to SQL, Deep Analysis)"
  ],
  "retrievedAt": "2025-12-16T12:48:28.0366387+01:00"
}
```

---

### âœ… Test #5: db-config

**Category**: Connection & Info  
**Description**: Get database configuration  
**Status**: âœ… PASSED  
**Duration**: 2420 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 12:48:30  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-config -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 12:48:28.5823|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 12:48:28.5990|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 12:48:28.5990|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 12:48:28.5990|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 12:48:28.5990|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 12:48:28.5990|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-config -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-config_20251216_124820.json
2025-12-16 12:48:28.5990|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 12:48:28.5990|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 12:48:28.5990|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 12:48:28.5990|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 12:48:28.6387|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 12:48:28.6387|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 12:48:28.6387|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 12:48:28.6387|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 12:48:28.6494|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 12:48:28.6494|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 12:48:28.6494|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "db-config",
  "configCount": 100,
  "config": [
    {
      "name": "act_sortmem_limit",
      "value": "100"
    },
    {
      "name": "alt_collate",
      "value": ""
    },
    {
      "name": "app_ctl_heap_sz",
      "value": "128"
    },
    {
      "name": "appgroup_mem_sz",
      "value": "30000"
    },
    {
      "name": "appl_memory",
      "value": "40000"
    },
    {
      "name": "applheapsz",
      "value": "128"
    },
    {
      "name": "archretrydelay",
      "value": "20"
    },
    {
      "name": "authn_cache_duration",
      "value": "3"
    },
    {
      "name": "authn_cache_users",
      "value": "0"
    },
    {
      "name": "auto_ai_maint",
      "value": "ON"
    },
    {
      "name": "auto_ai_optimizer",
      "value": "OFF"
    },
    {
      "name": "auto_cg_stats",
      "value": "OFF"
    },
    {
      "name": "auto_db_backup",
      "value": "OFF"
    },
    {
      "name": "auto_del_rec_obj",
      "value": "OFF"
    },
    {
      "name": "auto_maint",
      "value": "OFF"
    },
    {
      "name": "auto_model_discover",
      "value": "ON"
    },
    {
      "name": "auto_reorg",
      "value": "ON"
    },
    {
      "name": "auto_reval",
      "value": "DISABLED"
    },
    {
      "name": "auto_runstats",
      "value": "ON"
    },
    {
      "name": "auto_sampling",
      "value": "OFF"
    },
    {
      "name": "auto_stats_views",
      "value": "OFF"
    },
    {
      "name": "auto_stmt_stats",
      "value": "OFF"
    },
    {
      "name": "auto_tbl_maint",
      "value": "ON"
    },
    {
      "name": "autorestart",
      "value": "ON"
    },
    {
      "name": "avg_appls",
      "value": "1"
    },
    {
      "name": "backup_pending",
      "value": "NO"
    },
    {
      "name": "blk_log_dsk_ful",
      "value": "NO"
    },
    {
      "name": "blocknonlogged",
      "value": "NO"
    },
    {
      "name": "catalogcache_sz",
      "value": "-1"
    },
    {
      "name": "cf_catchup_trgt",
      "value": "15"
    },
    {
      "name": "cf_db_mem_sz",
      "value": "32768"
    },
    {
      "name": "cf_deda_ratio",
      "value": "4"
    },
    {
      "name": "cf_gbp_sz",
      "value": "4096"
    },
    {
      "name": "cf_lock_sz",
      "value": "16384"
    },
    {
      "name": "cf_sca_sz",
      "value": "4096"
    },
    {
      "name": "cf_self_tuning_mem",
      "value": "OFF"
    },
    {
      "name": "chngpgs_thresh",
      "value": "60"
    },
    {
      "name": "codepage",
      "value": "1252"
    },
    {
      "name": "codeset",
      "value": "1252"
    },
    {
      "name": "collate_info",
      "value": ""
    },
    {
      "name": "connect_proc",
      "value": ""
    },
    {
      "name": "country",
      "value": "47"
    },
    {
      "name": "ctrl_file_recov_path",
 
... (truncated)
```

---

## Test Environment

- **Executable**: bin\Debug\net10.0-windows\WindowsDb2Editor.exe
- **Test Timestamp**: 20251216_124820
- **PowerShell Version**: 5.1.26100.7462
- **Test Script**: _cli_test_with_io_capture.ps1

---

*Report generated automatically by WindowsDb2Editor CLI Test Suite*
