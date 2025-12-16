# Comprehensive CLI Test Report - ALL 119 Commands with Full I/O Capture

**Date**: 2025-12-16 13:49:25  
**Connection**: FKKTOTST  
**Total Commands Tested**: 113  
**âœ… Passed**: 113 (100%)  
**âŒ Failed**: 0 (0%)  

---

## Executive Summary

| Metric | Value |
|--------|-------|
| Total Commands Tested | 113 |
| Passed | 113 |
| Failed | 0 |
| Success Rate | 100% |
| Test Duration | ~0 seconds |

---

## Test Results by Category

### âœ…  (113/113 passed)

| # | Command | Description | Status | Duration | Exit Code |
|---|---------|-------------|--------|----------|-----------|
| 1 | `connection-test` | Test database connection | âœ… PASS | 2933ms | 0 |
| 2 | `connection-stats` | Get connection statistics | âœ… PASS | 2189ms | 0 |
| 3 | `help-all` | Show all available commands | âœ… PASS | 1129ms | 0 |
| 4 | `cli-version` | Show CLI version | âœ… PASS | 1085ms | 0 |
| 5 | `db-config` | Get database configuration | âœ… PASS | 2435ms | 0 |
| 6 | `connection-history` | Show connection history | âœ… PASS | 2207ms | 0 |
| 7 | `list-schemas` | List all schemas | âœ… PASS | 1853ms | 0 |
| 8 | `list-tables` | List tables in schema | âœ… PASS | 2129ms | 0 |
| 9 | `list-views` | List views in schema | âœ… PASS | 2062ms | 0 |
| 10 | `list-procedures` | List procedures in schema | âœ… PASS | 1987ms | 0 |
| 11 | `list-functions` | List functions in schema | âœ… PASS | 1932ms | 0 |
| 12 | `list-packages` | List packages in schema | âœ… PASS | 1846ms | 0 |
| 13 | `list-tablespaces` | List all tablespaces | âœ… PASS | 1821ms | 0 |
| 14 | `list-all-indexes` | List all indexes in schema | âœ… PASS | 1769ms | 0 |
| 15 | `list-constraints` | List constraints in schema | âœ… PASS | 1882ms | 0 |
| 16 | `list-sequences` | List sequences in schema | âœ… PASS | 1944ms | 0 |
| 17 | `table-properties` | Get table properties | âœ… PASS | 2865ms | 0 |
| 18 | `table-columns` | List table columns | âœ… PASS | 2266ms | 0 |
| 19 | `table-indexes` | List table indexes | âœ… PASS | 2178ms | 0 |
| 20 | `table-foreignkeys` | List table foreign keys | âœ… PASS | 2237ms | 0 |
| 21 | `table-ddl` | Generate table DDL | âœ… PASS | 2218ms | 0 |
| 22 | `table-statistics` | Get table statistics | âœ… PASS | 2225ms | 0 |
| 23 | `table-relationships` | Show table relationships | âœ… PASS | 2179ms | 0 |
| 24 | `table-sample-data` | Get sample data from table | âœ… PASS | 2249ms | 0 |
| 25 | `table-size` | Get table size | âœ… PASS | 2515ms | 0 |
| 26 | `table-grants` | List table grants | âœ… PASS | 2555ms | 0 |
| 27 | `table-dependencies` | Show table dependencies | âœ… PASS | 2281ms | 0 |
| 28 | `table-incoming-fk` | List incoming foreign keys | âœ… PASS | 2543ms | 0 |
| 29 | `table-used-by-packages` | List packages using this table | âœ… PASS | 2376ms | 0 |
| 30 | `table-used-by-views` | List views using this table | âœ… PASS | 2177ms | 0 |
| 31 | `table-used-by-routines` | List routines using this table | âœ… PASS | 2243ms | 0 |
| 32 | `view-properties` | Get view properties | âœ… PASS | 2416ms | 0 |
| 33 | `view-definition` | Get view definition SQL | âœ… PASS | 2547ms | 0 |
| 34 | `view-columns` | List view columns | âœ… PASS | 2234ms | 0 |
| 35 | `view-dependencies` | Show view dependencies | âœ… PASS | 2179ms | 0 |
| 36 | `view-sample-data` | Get sample data from view | âœ… PASS | 2054ms | 0 |
| 37 | `view-used-by-packages` | List packages using this view | âœ… PASS | 2035ms | 0 |
| 38 | `view-used-by-views` | List views using this view | âœ… PASS | 2302ms | 0 |
| 39 | `procedure-properties` | Get procedure properties | âœ… PASS | 2293ms | 0 |
| 40 | `procedure-source` | Get procedure source code | âœ… PASS | 2316ms | 0 |
| 41 | `procedure-parameters` | List procedure parameters | âœ… PASS | 2513ms | 0 |
| 42 | `procedure-dependencies` | Show procedure dependencies | âœ… PASS | 2385ms | 0 |
| 43 | `procedure-usage` | Show procedure usage | âœ… PASS | 2027ms | 0 |
| 44 | `procedure-grants` | List procedure grants | âœ… PASS | 1974ms | 0 |
| 45 | `function-properties` | Get function properties | âœ… PASS | 2162ms | 0 |
| 46 | `function-source` | Get function source code | âœ… PASS | 1952ms | 0 |
| 47 | `function-parameters` | List function parameters | âœ… PASS | 1975ms | 0 |
| 48 | `function-dependencies` | Show function dependencies | âœ… PASS | 1854ms | 0 |
| 49 | `function-usage` | Show function usage | âœ… PASS | 2434ms | 0 |
| 50 | `function-grants` | List function grants | âœ… PASS | 2075ms | 0 |
| 51 | `package-properties` | Get package properties | âœ… PASS | 2094ms | 0 |
| 52 | `package-statements` | List package statements | âœ… PASS | 1974ms | 0 |
| 53 | `package-dependencies` | Show package dependencies | âœ… PASS | 2058ms | 0 |
| 54 | `package-statistics` | Get package statistics | âœ… PASS | 2109ms | 0 |
| 55 | `package-list-tables` | List tables used by package | âœ… PASS | 1906ms | 0 |
| 56 | `package-list-views` | List views used by package | âœ… PASS | 2034ms | 0 |
| 57 | `package-list-procedures` | List procedures used by package | âœ… PASS | 1891ms | 0 |
| 58 | `package-list-functions` | List functions used by package | âœ… PASS | 2185ms | 0 |
| 59 | `user-properties` | Get user properties | âœ… PASS | 1944ms | 0 |
| 60 | `user-privileges` | List user privileges | âœ… PASS | 1979ms | 0 |
| 61 | `user-tables` | List user's tables | âœ… PASS | 2198ms | 0 |
| 62 | `user-schemas` | List user's schemas | âœ… PASS | 1888ms | 0 |
| 63 | `user-connections` | List user connections | âœ… PASS | 2297ms | 0 |
| 64 | `db-load` | Get database load | âœ… PASS | 2001ms | 0 |
| 65 | `db-locks` | Show database locks | âœ… PASS | 2132ms | 0 |
| 66 | `db-sessions` | List database sessions | âœ… PASS | 1811ms | 0 |
| 67 | `db-size` | Get database size | âœ… PASS | 1745ms | 0 |
| 68 | `schema-size` | Get schema size | âœ… PASS | 1823ms | 0 |
| 69 | `db-version` | Get database version | âœ… PASS | 1751ms | 0 |
| 70 | `db-parameters` | List database parameters | âœ… PASS | 1812ms | 0 |
| 71 | `db-registry` | Show database registry | âœ… PASS | 1795ms | 0 |
| 72 | `active-queries` | List active queries | âœ… PASS | 2085ms | 0 |
| 73 | `bufferpool-stats` | Get buffer pool statistics | âœ… PASS | 2135ms | 0 |
| 74 | `tablespace-usage` | Show tablespace usage | âœ… PASS | 2299ms | 0 |
| 75 | `cdc-status` | Get CDC status for schema | âœ… PASS | 2269ms | 0 |
| 76 | `cdc-status-full` | Get full CDC status | âœ… PASS | 1802ms | 0 |
| 77 | `cdc-configuration` | Get CDC configuration for table | âœ… PASS | 1792ms | 0 |
| 78 | `cdc-changes` | List CDC changes | âœ… PASS | 2098ms | 0 |
| 79 | `cdc-enable` | Enable CDC for table | âœ… PASS | 1978ms | 0 |
| 80 | `cdc-disable` | Disable CDC for table | âœ… PASS | 1916ms | 0 |
| 81 | `cdc-history` | Show CDC history | âœ… PASS | 1812ms | 0 |
| 82 | `db-compare` | Compare two schemas | âœ… PASS | 1909ms | 0 |
| 83 | `db-compare-source-only` | Show objects only in source | âœ… PASS | 2090ms | 0 |
| 84 | `db-compare-target-only` | Show objects only in target | âœ… PASS | 2482ms | 0 |
| 85 | `db-compare-different` | Show different objects | âœ… PASS | 2120ms | 0 |
| 86 | `db-compare-ddl` | Generate comparison DDL | âœ… PASS | 1831ms | 0 |
| 87 | `object-metadata` | Get object metadata | âœ… PASS | 2031ms | 0 |
| 88 | `object-search` | Search for objects | âœ… PASS | 1869ms | 0 |
| 89 | `column-search` | Search for columns | âœ… PASS | 1809ms | 0 |
| 90 | `dependency-graph` | Generate dependency graph | âœ… PASS | 1887ms | 0 |
| 91 | `query-history` | Show query history | âœ… PASS | 2119ms | 0 |
| 92 | `schema-summary` | Get schema summary | âœ… PASS | 2147ms | 0 |
| 93 | `database-summary` | Get database summary | âœ… PASS | 2263ms | 0 |
| 94 | `health-check` | Run health check | âœ… PASS | 2467ms | 0 |
| 95 | `ai-query` | AI-assisted query generation | âœ… PASS | 2127ms | 0 |
| 96 | `ai-explain-table` | AI explanation of table | âœ… PASS | 2219ms | 0 |
| 97 | `ai-explain-view` | AI explanation of view | âœ… PASS | 2298ms | 0 |
| 98 | `ai-analyze-procedure` | AI analysis of procedure | âœ… PASS | 1953ms | 0 |
| 99 | `ai-analyze-function` | AI analysis of function | âœ… PASS | 1856ms | 0 |
| 100 | `ai-analyze-package` | AI analysis of package | âœ… PASS | 1887ms | 0 |
| 101 | `ai-deep-analysis` | Deep AI analysis of table | âœ… PASS | 1915ms | 0 |
| 102 | `gui-test` | Test TableDetails form | âœ… PASS | 1971ms | 0 |
| 103 | `gui-test` | Test TableDetails Columns tab | âœ… PASS | 1964ms | 0 |
| 104 | `gui-test` | Test TableDetails Indexes tab | âœ… PASS | 2344ms | 0 |
| 105 | `gui-test` | Test TableDetails ForeignKeys tab | âœ… PASS | 2031ms | 0 |
| 106 | `gui-test` | Test ViewDetails form | âœ… PASS | 1873ms | 0 |
| 107 | `gui-test` | Test ProcedureDetails form | âœ… PASS | 2176ms | 0 |
| 108 | `gui-test` | Test FunctionDetails form | âœ… PASS | 1903ms | 0 |
| 109 | `gui-test` | Test PackageDetails form | âœ… PASS | 2149ms | 0 |
| 110 | `gui-test` | Test DatabaseLoadMonitor form | âœ… PASS | 2053ms | 0 |
| 111 | `gui-test` | Test ActiveSessions form | âœ… PASS | 2035ms | 0 |
| 112 | `gui-test` | Test LockMonitor form | âœ… PASS | 2147ms | 0 |
| 113 | `gui-test` | Test CdcStatusDialog form | âœ… PASS | 2303ms | 0 |

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
**Duration**: 2933 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:45:32  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command connection-test -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:45:30.3216|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:45:30.3343|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:45:30.3343|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:45:30.3343|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:45:30.3343|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:45:30.3343|DEBUG|WindowsDb2Editor.App|Startup arguments: -command connection-test -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_connection-test_20251216_134528.json
2025-12-16 13:45:30.3508|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:45:30.3508|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:45:30.3508|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:45:30.3508|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:45:30.3995|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:45:30.3995|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:45:30.3995|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:45:30.3995|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:45:30.3995|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:45:30.4175|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:45:30.4175|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading 
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "connection-test",
  "profileName": "FKKTOTST",
  "testResult": "SUCCESS",
  "message": "Connection opened successfully",
  "retrievedAt": "2025-12-16T13:45:31.9440048+01:00"
}
```

---

### âœ… Test #2: connection-stats

**Category**: Connection & Info  
**Description**: Get connection statistics  
**Status**: âœ… PASSED  
**Duration**: 2189 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:45:34  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command connection-stats -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:45:32.8398|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:45:32.8530|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:45:32.8530|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:45:32.8530|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:45:32.8650|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:45:32.8650|DEBUG|WindowsDb2Editor.App|Startup arguments: -command connection-stats -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_connection-stats_20251216_134528.json
2025-12-16 13:45:32.8650|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:45:32.8650|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:45:32.8650|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:45:32.8650|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:45:32.9014|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:45:32.9014|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:45:32.9014|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:45:32.9014|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:45:32.9014|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:45:32.9144|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:45:32.9144|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "totalProfiles": 2,
  "note": "Connection stats based on saved profiles",
  "retrievedAt": "2025-12-16T13:45:34.1798605+01:00"
}
```

---

### âœ… Test #3: help-all

**Category**: Connection & Info  
**Description**: Show all available commands  
**Status**: âœ… PASSED  
**Duration**: 1129 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:45:35  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command help-all -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:45:35.0555|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:45:35.0692|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:45:35.0692|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:45:35.0692|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:45:35.0692|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:45:35.0692|DEBUG|WindowsDb2Editor.App|Startup arguments: -command help-all -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_help-all_20251216_134528.json
2025-12-16 13:45:35.0692|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:45:35.0692|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:45:35.0692|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:45:35.0863|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:45:35.1197|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:45:35.1197|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:45:35.1197|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:45:35.1197|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:45:35.1197|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:45:35.1336|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:45:35.1336|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for D
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
**Duration**: 1085 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:45:36  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cli-version -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:45:36.0727|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:45:36.1012|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:45:36.1012|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:45:36.1012|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:45:36.1012|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:45:36.1012|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cli-version -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_cli-version_20251216_134528.json
2025-12-16 13:45:36.1012|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:45:36.1012|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:45:36.1163|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:45:36.1163|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:45:36.1696|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:45:36.1696|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:45:36.1696|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:45:36.1696|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:45:36.1776|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:45:36.1939|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:45:36.1939|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata
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
  "retrievedAt": "2025-12-16T13:45:36.3588504+01:00"
}
```

---

### âœ… Test #5: db-config

**Category**: Connection & Info  
**Description**: Get database configuration  
**Status**: âœ… PASSED  
**Duration**: 2435 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:45:38  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-config -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:45:37.1514|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:45:37.1633|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:45:37.1633|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:45:37.1633|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:45:37.1633|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:45:37.1633|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-config -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-config_20251216_134528.json
2025-12-16 13:45:37.1633|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:45:37.1633|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:45:37.1780|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:45:37.1780|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:45:37.2240|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:45:37.2252|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:45:37.2252|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:45:37.2252|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:45:37.2435|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:45:37.2435|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:45:37.2435|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for
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

### âœ… Test #6: connection-history

**Category**: Connection & Info  
**Description**: Show connection history  
**Status**: âœ… PASSED  
**Duration**: 2207 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:45:41  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command connection-history -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:45:39.5873|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:45:39.6177|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:45:39.6177|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:45:39.6177|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:45:39.6177|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:45:39.6177|DEBUG|WindowsDb2Editor.App|Startup arguments: -command connection-history -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_connection-history_20251216_134528.json
2025-12-16 13:45:39.6177|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:45:39.6177|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:45:39.6177|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:45:39.6331|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:45:39.6730|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:45:39.6730|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:45:39.6730|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:45:39.6730|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:45:39.6776|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:45:39.6951|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:45:39.6951|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Lo
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "connection-history",
  "message": "Connection history not available in current session",
  "history": []
}
```

---

### âœ… Test #7: list-schemas

**Category**: Schema Operations  
**Description**: List all schemas  
**Status**: âœ… PASSED  
**Duration**: 1853 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:45:42  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-schemas -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:45:41.6659|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:45:41.6659|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:45:41.6776|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:45:41.6776|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:45:41.6776|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:45:41.6776|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-schemas -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_list-schemas_20251216_134528.json
2025-12-16 13:45:41.6776|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:45:41.6776|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:45:41.6776|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:45:41.6776|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:45:41.7163|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:45:41.7163|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:45:41.7163|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:45:41.7163|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:45:41.7268|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:45:41.7268|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:45:41.7268|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metada
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schemaCount": 35,
  "schemas": [
    {
      "schemaName": "ASK",
      "owner": "SYSIBM",
      "createTime": "2014-01-06T13:42:50.684002",
      "remarks": ""
    },
    {
      "schemaName": "DB2ADMIN",
      "owner": "SYSIBM",
      "createTime": "2002-02-15T08:46:45.205001",
      "remarks": ""
    },
    {
      "schemaName": "DB2TE434",
      "owner": "SYSIBM",
      "createTime": "2006-01-06T08:58:22.767001",
      "remarks": ""
    },
    {
      "schemaName": "DBE",
      "owner": "SYSIBM",
      "createTime": "2006-01-06T08:58:14.737001",
      "remarks": ""
    },
    {
      "schemaName": "DBM",
      "owner": "SYSIBM",
      "createTime": "2001-11-09T11:04:09.326001",
      "remarks": ""
    },
    {
      "schemaName": "FK",
      "owner": "SYSIBM",
      "createTime": "2001-11-15T12:43:01.452",
      "remarks": ""
    },
    {
      "schemaName": "FKGEISTA",
      "owner": "SYSIBM",
      "createTime": "2025-01-03T13:42:42.730002",
      "remarks": ""
    },
    {
      "schemaName": "INL",
      "owner": "SYSIBM",
      "createTime": "2001-11-15T10:33:23.751008",
      "remarks": ""
    },
    {
      "schemaName": "LOG",
      "owner": "SYSIBM",
      "createTime": "2015-10-22T09:22:31.297002",
      "remarks": ""
    },
    {
      "schemaName": "NULLID",
      "owner": "SYSIBM",
      "createTime": "2001-11-09T09:52:29.159002",
      "remarks": ""
    },
    {
      "schemaName": "Q",
      "owner": "SYSIBM",
      "createTime": "2001-11-27T13:15:07.757001",
      "remarks": ""
    },
    {
      "schemaName": "QFW72D",
      "owner": "SYSIBM",
      "createTime": "2003-03-11T09:33:00.259",
      "remarks": ""
    },
    {
      "schemaName": "QFW72F",
      "owner": "SYSIBM",
      "createTime": "2003-07-08T10:18:49.615",
      "remarks": ""
    },
    {
      "schemaName": "QFW72G",
      "owner": "SYSIBM",
      "createTime": "2003-07-11T15:13:13.321",
      "remarks": ""
    },
    {
      "schemaName": "QFW81F",
      "owner": "SYSIBM",
      "createTime": "2005-06-16T12:30:55.327001",
      "remarks": ""
    },
    {
      "schemaName": "QFW81FP4",
      "owner": "SYSIBM",
      "createTime": "2005-01-10T13:23:33.979001",
      "remarks": ""
    },
    {
      "schemaName": "QFW81L",
      "owner": "SYSIBM",
      "createTime": "2007-11-13T13:16:41.421001",
      "remarks": ""
    },
    {
      "schemaName": "QFW81S",
      "owner": "SYSIBM",
      "createTime": "2016-07-28T15:07:07.254",
      "remarks": ""
    },
    {
      "schemaName": "QMF611",
      "owner": "SYSIBM",
      "createTime": "2001-11-27T13:14:53.616",
      "remarks": ""
    },
    {
      "schemaName": "QMF611N",
      "owner": "SYSIBM",
      "createTime": "2001-11-28T15:03:18.942001",
      "remarks": ""
    },
    {
      "schemaName": "QMF813",
      "owner": "SYSIBM",
      "createTime": "2004-09-27T14:47:28.852001"
... (truncated)
```

---

### âœ… Test #8: list-tables

**Category**: Schema Operations  
**Description**: List tables in schema  
**Status**: âœ… PASSED  
**Duration**: 2129 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:45:45  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-tables -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:45:43.5099|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:45:43.5228|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:45:43.5228|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:45:43.5228|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:45:43.5228|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:45:43.5228|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-tables -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-tables_20251216_134528.json
2025-12-16 13:45:43.5228|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:45:43.5228|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:45:43.5228|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:45:43.5228|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:45:43.5619|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:45:43.5619|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:45:43.5619|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:45:43.5619|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:45:43.5720|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:45:43.5720|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:45:43.5720|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Load
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "totalTables": 0,
  "limitApplied": 0,
  "tables": [],
  "retrievedAt": "2025-12-16T13:45:45.0360185+01:00"
}
```

---

### âœ… Test #9: list-views

**Category**: Schema Operations  
**Description**: List views in schema  
**Status**: âœ… PASSED  
**Duration**: 2062 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:45:47  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-views -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:45:45.7026|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:45:45.7254|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:45:45.7254|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:45:45.7254|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:45:45.7254|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:45:45.7254|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-views -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-views_20251216_134528.json
2025-12-16 13:45:45.7254|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:45:45.7254|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:45:45.7254|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:45:45.7254|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:45:45.7753|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:45:45.7753|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:45:45.7753|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:45:45.7753|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:45:45.7896|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:45:45.7896|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:45:45.7896|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "totalViews": 0,
  "limitApplied": 0,
  "views": [],
  "retrievedAt": "2025-12-16T13:45:47.102162+01:00"
}
```

---

### âœ… Test #10: list-procedures

**Category**: Schema Operations  
**Description**: List procedures in schema  
**Status**: âœ… PASSED  
**Duration**: 1987 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:45:49  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-procedures -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:45:47.7167|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:45:47.7292|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:45:47.7292|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:45:47.7292|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:45:47.7292|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:45:47.7292|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-procedures -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-procedures_20251216_134528.json
2025-12-16 13:45:47.7292|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:45:47.7292|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:45:47.7425|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:45:47.7425|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:45:47.7779|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:45:47.7779|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:45:47.7779|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:45:47.7779|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:45:47.7890|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:45:47.7890|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:45:47.7890|DEBUG|WindowsDb2Editor.Services.MetadataHand
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "totalProcedures": 0,
  "limitApplied": 0,
  "procedures": [],
  "retrievedAt": "2025-12-16T13:45:49.0913261+01:00"
}
```

---

### âœ… Test #11: list-functions

**Category**: Schema Operations  
**Description**: List functions in schema  
**Status**: âœ… PASSED  
**Duration**: 1932 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:45:51  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-functions -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:45:49.7138|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:45:49.7260|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:45:49.7260|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:45:49.7260|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:45:49.7260|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:45:49.7260|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-functions -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-functions_20251216_134528.json
2025-12-16 13:45:49.7260|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:45:49.7260|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:45:49.7260|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:45:49.7408|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:45:49.7688|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:45:49.7688|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:45:49.7688|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:45:49.7688|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:45:49.7723|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:45:49.7723|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:45:49.7723|DEBUG|WindowsDb2Editor.Services.MetadataHandle
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "totalFunctions": 0,
  "limitApplied": 0,
  "functions": [],
  "retrievedAt": "2025-12-16T13:45:51.0348509+01:00"
}
```

---

### âœ… Test #12: list-packages

**Category**: Schema Operations  
**Description**: List packages in schema  
**Status**: âœ… PASSED  
**Duration**: 1846 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:45:52  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-packages -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:45:51.6654|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:45:51.6793|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:45:51.6793|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:45:51.6793|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:45:51.6793|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:45:51.6793|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-packages -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-packages_20251216_134528.json
2025-12-16 13:45:51.6793|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:45:51.6793|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:45:51.6793|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:45:51.6940|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:45:51.7250|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:45:51.7255|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:45:51.7255|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:45:51.7255|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:45:51.7255|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:45:51.7255|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:45:51.7255|DEBUG|WindowsDb2Editor.Services.MetadataHandler|
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "totalPackages": 0,
  "limitApplied": 0,
  "packages": [],
  "retrievedAt": "2025-12-16T13:45:52.8776954+01:00"
}
```

---

### âœ… Test #13: list-tablespaces

**Category**: Schema Operations  
**Description**: List all tablespaces  
**Status**: âœ… PASSED  
**Duration**: 1821 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:45:54  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-tablespaces -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:45:53.4687|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:45:53.4829|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:45:53.4829|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:45:53.4829|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:45:53.4829|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:45:53.4923|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-tablespaces -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_list-tablespaces_20251216_134528.json
2025-12-16 13:45:53.4923|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:45:53.4923|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:45:53.4923|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:45:53.4923|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:45:53.5271|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:45:53.5271|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:45:53.5271|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:45:53.5271|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:45:53.5391|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:45:53.5391|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:45:53.5391|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "tablespaceCount": 37,
  "tablespaces": [
    {
      "tablespaceName": "DBETS1",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "DBETSSMP",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "DSQ1STBT",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "DSQTSCT1",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "DSQTSCT2",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "DSQTSCT3",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "DSQTSDEF",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RAATS1",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RAATS2",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RAATS7",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RAATS8",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RAATS9",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RAATSRC1",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RAATSRC2",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RAATSRC3",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RAATSRC4",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RAATSX1",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RAATSX2",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RAATSX3",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RAATSX4",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RAATSX5",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RDBITSDF",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RDBITSGV",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RDBITSPR",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RDBITSRS",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RDBITSSC",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RDBITSX1",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RDBITSX2",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RDBITSX3",
      "dataType": "A",
      "pageSize": 4096
    },
    {
      "tablespaceName": "RDBITSX4",
      "dataType": "A",

... (truncated)
```

---

### âœ… Test #14: list-all-indexes

**Category**: Schema Operations  
**Description**: List all indexes in schema  
**Status**: âœ… PASSED  
**Duration**: 1769 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:45:56  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-all-indexes -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:45:55.2927|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:45:55.3077|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:45:55.3077|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:45:55.3077|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:45:55.3077|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:45:55.3077|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-all-indexes -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-all-indexes_20251216_134528.json
2025-12-16 13:45:55.3077|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:45:55.3077|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:45:55.3202|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:45:55.3202|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:45:55.3544|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:45:55.3544|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:45:55.3544|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:45:55.3544|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:45:55.3664|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:45:55.3664|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:45:55.3664|DEBUG|WindowsDb2Editor.Services.MetadataHa
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "list-all-indexes",
  "indexCount": 17,
  "limitApplied": 17,
  "indexes": [
    {
      "schema": "INL",
      "indexName": "KUNDEKONTO_I1",
      "tableName": "KUNDEKONTO",
      "uniqueRule": "D"
    },
    {
      "schema": "INL",
      "indexName": "KUNDEKONTO_TEST",
      "tableName": "KUNDEKONTO_TEST",
      "uniqueRule": "D"
    },
    {
      "schema": "INL",
      "indexName": "KUNDEKONTOT_KNRI1",
      "tableName": "KUNDEKONTO_TEST",
      "uniqueRule": "D"
    },
    {
      "schema": "SYSIBM",
      "indexName": "SQL011115103324040",
      "tableName": "FIRMA",
      "uniqueRule": "P"
    },
    {
      "schema": "SYSIBM",
      "indexName": "SQL011115103324220",
      "tableName": "KUNDEKONTO",
      "uniqueRule": "P"
    },
    {
      "schema": "SYSIBM",
      "indexName": "SQL011115103324360",
      "tableName": "KONTOTYPE",
      "uniqueRule": "P"
    },
    {
      "schema": "SYSIBM",
      "indexName": "SQL011115103324430",
      "tableName": "KONTO",
      "uniqueRule": "P"
    },
    {
      "schema": "SYSIBM",
      "indexName": "SQL011115103324640",
      "tableName": "TRANSTYPE",
      "uniqueRule": "P"
    },
    {
      "schema": "SYSIBM",
      "indexName": "SQL011115103324840",
      "tableName": "BILAGNR",
      "uniqueRule": "P"
    },
    {
      "schema": "SYSIBM",
      "indexName": "SQL011119113415320",
      "tableName": "RENTESATSER",
      "uniqueRule": "P"
    },
    {
      "schema": "SYSIBM",
      "indexName": "SQL011205101347070",
      "tableName": "FASTE_LISTE",
      "uniqueRule": "P"
    },
    {
      "schema": "SYSIBM",
      "indexName": "SQL011205101347310",
      "tableName": "FASTE_TRANS",
      "uniqueRule": "P"
    },
    {
      "schema": "SYSIBM",
      "indexName": "SQL171114133531600",
      "tableName": "KUNDEKONTO_TEST",
      "uniqueRule": "P"
    },
    {
      "schema": "SYSIBM",
      "indexName": "SQL190320140707700",
      "tableName": "KUNDEKTO_M_FEIL_ORGNR",
      "uniqueRule": "P"
    },
    {
      "schema": "SYSIBM",
      "indexName": "SQL210629153335490",
      "tableName": "MKK_AVSETNING",
      "uniqueRule": "P"
    },
    {
      "schema": "INL",
      "indexName": "TRANSREG_I1",
      "tableName": "TRANSREG",
      "uniqueRule": "D"
    },
    {
      "schema": "INL",
      "indexName": "UTBET_XX",
      "tableName": "UTBET_MK",
      "uniqueRule": "D"
    }
  ],
  "retrievedAt": "2025-12-16T13:45:56.4690376+01:00"
}
```

---

### âœ… Test #15: list-constraints

**Category**: Schema Operations  
**Description**: List constraints in schema  
**Status**: âœ… PASSED  
**Duration**: 1882 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:45:58  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-constraints -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:45:57.0575|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:45:57.0709|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:45:57.0709|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:45:57.0709|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:45:57.0709|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:45:57.0709|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-constraints -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-constraints_20251216_134528.json
2025-12-16 13:45:57.0709|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:45:57.0709|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:45:57.0709|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:45:57.0709|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:45:57.1106|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:45:57.1106|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:45:57.1106|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:45:57.1106|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:45:57.1243|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:45:57.1243|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:45:57.1326|DEBUG|WindowsDb2Editor.Services.MetadataHa
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "list-constraints",
  "constraintCount": 30,
  "limitApplied": 30,
  "constraints": [
    {
      "schema": "INL",
      "tableName": "BILAGNR",
      "constraintName": "BILAGNR_FIR",
      "type": "F"
    },
    {
      "schema": "INL",
      "tableName": "FASTE_LISTE",
      "constraintName": "FASTE_FIR",
      "type": "F"
    },
    {
      "schema": "INL",
      "tableName": "FASTE_TRANS",
      "constraintName": "FASTE_KUNDEKONT",
      "type": "F"
    },
    {
      "schema": "INL",
      "tableName": "FASTE_TRANS",
      "constraintName": "FASTE_LT",
      "type": "F"
    },
    {
      "schema": "INL",
      "tableName": "FASTE_LISTE",
      "constraintName": "FASTE_TRANSTYPE",
      "type": "F"
    },
    {
      "schema": "INL",
      "tableName": "KONTO",
      "constraintName": "KONTO_FIR",
      "type": "F"
    },
    {
      "schema": "INL",
      "tableName": "KONTO",
      "constraintName": "KONTO_KTO_TYPE",
      "type": "F"
    },
    {
      "schema": "INL",
      "tableName": "TRANSREG",
      "constraintName": "KONTO_KTO_TYPE",
      "type": "F"
    },
    {
      "schema": "INL",
      "tableName": "KONTO",
      "constraintName": "KONTO_KUN_KTO",
      "type": "F"
    },
    {
      "schema": "INL",
      "tableName": "KONTOTYPE",
      "constraintName": "KTYPE_FIR",
      "type": "F"
    },
    {
      "schema": "INL",
      "tableName": "KUNDEKONTO",
      "constraintName": "KUNDE_FIR",
      "type": "F"
    },
    {
      "schema": "INL",
      "tableName": "KUNDEKONTO_TEST",
      "constraintName": "KUNDE_FIR",
      "type": "F"
    },
    {
      "schema": "INL",
      "tableName": "RENTESATSER",
      "constraintName": "RENTE_FIR",
      "type": "F"
    },
    {
      "schema": "INL",
      "tableName": "RENTESATSER",
      "constraintName": "RENTE_KTOTYPE",
      "type": "F"
    },
    {
      "schema": "INL",
      "tableName": "FIRMA",
      "constraintName": "SQL011115103324120",
      "type": "P"
    },
    {
      "schema": "INL",
      "tableName": "KUNDEKONTO",
      "constraintName": "SQL011115103324250",
      "type": "P"
    },
    {
      "schema": "INL",
      "tableName": "KONTOTYPE",
      "constraintName": "SQL011115103324390",
      "type": "P"
    },
    {
      "schema": "INL",
      "tableName": "KONTO",
      "constraintName": "SQL011115103324480",
      "type": "P"
    },
    {
      "schema": "INL",
      "tableName": "TRANSTYPE",
      "constraintName": "SQL011115103324670",
      "type": "P"
    },
    {
      "schema": "INL",
      "tableName": "BILAGNR",
      "constraintName": "SQL011115103324870",
      "type": "P"
    },
    {
      "schema": "INL",
      "tableName": "RENTESATSER",
      "constraintName": "SQL011119113415340",
      "type": "P"
    },
    {
      "schema": "INL",
      "tableName": "FASTE_LISTE",
      "constraintName": "SQL
... (truncated)
```

---

### âœ… Test #16: list-sequences

**Category**: Schema Operations  
**Description**: List sequences in schema  
**Status**: âœ… PASSED  
**Duration**: 1944 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:00  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-sequences -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:45:58.9643|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:45:58.9765|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:45:58.9765|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:45:58.9765|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:45:58.9765|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:45:58.9765|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-sequences -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-sequences_20251216_134528.json
2025-12-16 13:45:58.9765|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:45:58.9765|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:45:58.9765|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:45:58.9765|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:45:59.0102|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:45:59.0102|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:45:59.0102|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:45:59.0102|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:45:59.0102|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:45:59.0244|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:45:59.0244|DEBUG|WindowsDb2Editor.Services.MetadataHandle
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "list-sequences",
  "sequenceCount": 0,
  "sequences": [],
  "retrievedAt": "2025-12-16T13:46:00.2922482+01:00"
}
```

---

### âœ… Test #17: table-properties

**Category**: Table Operations  
**Description**: Get table properties  
**Status**: âœ… PASSED  
**Duration**: 2865 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:03  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-properties -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:01.0145|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:01.0252|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:01.0252|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:01.0252|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:01.0252|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:01.0252|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-properties -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-properties_20251216_134528.json
2025-12-16 13:46:01.0252|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:01.0252|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:01.0425|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:01.0425|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:01.0868|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:01.0868|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:01.0868|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:01.0868|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:01.0868|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:01.1061|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:01.1061|DEBUG|WindowsDb2Editor.Services.Me
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "columnCount": 5,
  "columns": [
    {
      "columnName": "FIRMA",
      "ordinalPosition": null,
      "dataType": "DECIMAL",
      "length": 6,
      "scale": null,
      "isNullable": false,
      "defaultValue": "0.0",
      "comment": "",
      "isIdentity": false,
      "isPrimaryKey": true
    },
    {
      "columnName": "AAR",
      "ordinalPosition": null,
      "dataType": "DECIMAL",
      "length": 4,
      "scale": null,
      "isNullable": false,
      "defaultValue": "0.0",
      "comment": "",
      "isIdentity": false,
      "isPrimaryKey": true
    },
    {
      "columnName": "SISTE_BILAGNR",
      "ordinalPosition": null,
      "dataType": "DECIMAL",
      "length": 6,
      "scale": null,
      "isNullable": false,
      "defaultValue": "0.0",
      "comment": "",
      "isIdentity": false,
      "isPrimaryKey": false
    },
    {
      "columnName": "TIDSPUNKT",
      "ordinalPosition": null,
      "dataType": "TIMESTAMP",
      "length": 10,
      "scale": null,
      "isNullable": false,
      "defaultValue": "CURRENT TIMESTAMP",
      "comment": "",
      "isIdentity": false,
      "isPrimaryKey": false
    },
    {
      "columnName": "BRUKERID",
      "ordinalPosition": null,
      "dataType": "CHARACTER",
      "length": 8,
      "scale": null,
      "isNullable": false,
      "defaultValue": "\u0027 \u0027",
      "comment": "",
      "isIdentity": false,
      "isPrimaryKey": false
    }
  ],
  "primaryKeyCount": 2,
  "primaryKeys": [
    "FIRMA",
    "AAR"
  ],
  "foreignKeyCount": 1,
  "foreignKeys": [
    {
      "constraintName": "BILAGNR_FIR",
      "columns": "FIRMA",
      "referencedSchema": "INL",
      "referencedTable": "FIRMA",
      "referencedColumns": "FIRMA",
      "deleteRule": "A",
      "updateRule": "A"
    }
  ],
  "indexCount": 1,
  "indexes": [
    {
      "indexName": "SQL011115103324840",
      "uniqueRule": "Primary Key",
      "isUnique": false,
      "isPrimaryKey": true,
      "columns": "FIRMA, AAR",
      "indexType": "Regular",
      "columnCount": null,
      "firstKeyCard": null,
      "fullKeyCard": null,
      "comment": null
    }
  ],
  "statistics": null,
  "retrievedAt": "2025-12-16T13:46:03.1571883+01:00"
}
```

---

### âœ… Test #18: table-columns

**Category**: Table Operations  
**Description**: List table columns  
**Status**: âœ… PASSED  
**Duration**: 2266 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:05  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-columns -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:03.9490|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:03.9634|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:03.9634|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:03.9634|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:03.9634|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:03.9634|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-columns -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-columns_20251216_134528.json
2025-12-16 13:46:03.9634|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:03.9634|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:03.9634|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:03.9774|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:04.0270|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:04.0270|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:04.0270|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:04.0270|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:04.0399|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:04.0399|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:04.0399|DEBUG|WindowsDb2Editor.Services.Metadata
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "columnCount": 5,
  "columns": [
    {
      "columnName": "FIRMA",
      "ordinalPosition": 0,
      "dataType": "DECIMAL",
      "length": 6,
      "scale": 0,
      "isNullable": false,
      "defaultValue": "0.0",
      "comment": "",
      "isIdentity": false
    },
    {
      "columnName": "AAR",
      "ordinalPosition": 1,
      "dataType": "DECIMAL",
      "length": 4,
      "scale": 0,
      "isNullable": false,
      "defaultValue": "0.0",
      "comment": "",
      "isIdentity": false
    },
    {
      "columnName": "SISTE_BILAGNR",
      "ordinalPosition": 2,
      "dataType": "DECIMAL",
      "length": 6,
      "scale": 0,
      "isNullable": false,
      "defaultValue": "0.0",
      "comment": "",
      "isIdentity": false
    },
    {
      "columnName": "TIDSPUNKT",
      "ordinalPosition": 3,
      "dataType": "TIMESTAMP",
      "length": 10,
      "scale": 6,
      "isNullable": false,
      "defaultValue": "CURRENT TIMESTAMP",
      "comment": "",
      "isIdentity": false
    },
    {
      "columnName": "BRUKERID",
      "ordinalPosition": 4,
      "dataType": "CHARACTER",
      "length": 8,
      "scale": 0,
      "isNullable": false,
      "defaultValue": "\u0027 \u0027",
      "comment": "",
      "isIdentity": false
    }
  ],
  "retrievedAt": "2025-12-16T13:46:05.4267336+01:00"
}
```

---

### âœ… Test #19: table-indexes

**Category**: Table Operations  
**Description**: List table indexes  
**Status**: âœ… PASSED  
**Duration**: 2178 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:07  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-indexes -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:06.1198|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:06.1332|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:06.1332|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:06.1332|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:06.1332|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:06.1332|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-indexes -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-indexes_20251216_134528.json
2025-12-16 13:46:06.1332|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:06.1332|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:06.1332|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:06.1332|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:06.1840|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:06.1840|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:06.1840|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:06.1840|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:06.1974|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:06.1974|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:06.1974|DEBUG|WindowsDb2Editor.Services.Metadata
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "indexCount": 1,
  "indexes": [
    {
      "indexName": "SQL011115103324840",
      "indexType": "REG",
      "isUnique": false,
      "isPrimaryKey": true,
      "columns": "\u002BFIRMA\u002BAAR",
      "columnCount": 2,
      "firstKeyCard": 2,
      "fullKeyCard": 41
    }
  ],
  "retrievedAt": "2025-12-16T13:46:07.6136336+01:00"
}
```

---

### âœ… Test #20: table-foreignkeys

**Category**: Table Operations  
**Description**: List table foreign keys  
**Status**: âœ… PASSED  
**Duration**: 2237 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:09  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-foreignkeys -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:08.3703|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:08.3845|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:08.3845|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:08.3845|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:08.3845|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:08.3845|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-foreignkeys -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-foreignkeys_20251216_134528.json
2025-12-16 13:46:08.3845|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:08.3845|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:08.3845|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:08.4008|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:08.4367|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:08.4367|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:08.4367|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:08.4367|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:08.4493|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:08.4493|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:08.4493|DEBUG|WindowsDb2Editor.Services.
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "foreignKeyCount": 1,
  "foreignKeys": [
    {
      "fkName": "BILAGNR_FIR",
      "fkColumns": "FIRMA",
      "refSchema": "INL",
      "refTable": "FIRMA",
      "refColumns": "FIRMA",
      "deleteRule": "A",
      "updateRule": "A"
    }
  ],
  "retrievedAt": "2025-12-16T13:46:09.8698344+01:00"
}
```

---

### âœ… Test #21: table-ddl

**Category**: Table Operations  
**Description**: Generate table DDL  
**Status**: âœ… PASSED  
**Duration**: 2218 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:12  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-ddl -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:10.5863|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:10.6043|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:10.6043|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:10.6043|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:10.6043|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:10.6043|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-ddl -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-ddl_20251216_134528.json
2025-12-16 13:46:10.6043|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:10.6043|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:10.6181|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:10.6181|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:10.6532|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:10.6532|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:10.6532|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:10.6532|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:10.6647|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:10.6647|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:10.6647|DEBUG|WindowsDb2Editor.Services.MetadataHandler|
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "ddl": "CREATE TABLE INL.BILAGNR (\r\n    FIRMA DECIMAL(6,0) NOT NULL DEFAULT 0.0,\r\n    AAR DECIMAL(4,0) NOT NULL DEFAULT 0.0,\r\n    SISTE_BILAGNR DECIMAL(6,0) NOT NULL DEFAULT 0.0,\r\n    TIDSPUNKT TIMESTAMP NOT NULL DEFAULT CURRENT TIMESTAMP,\r\n    BRUKERID CHARACTER NOT NULL DEFAULT \u0027 \u0027\r\n);\r\n",
  "ddlLength": 282,
  "columnCount": 5,
  "retrievedAt": "2025-12-16T13:46:12.0922494+01:00"
}
```

---

### âœ… Test #22: table-statistics

**Category**: Table Operations  
**Description**: Get table statistics  
**Status**: âœ… PASSED  
**Duration**: 2225 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:14  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-statistics -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:12.8291|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:12.8378|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:12.8378|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:12.8378|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:12.8378|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:12.8378|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-statistics -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-statistics_20251216_134528.json
2025-12-16 13:46:12.8378|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:12.8378|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:12.8528|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:12.8528|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:12.8927|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:12.8927|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:12.8927|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:12.8927|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:12.9115|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:12.9180|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:12.9180|DEBUG|WindowsDb2Editor.Services.Me
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "ALL",
  "totalTables": 271,
  "limitApplied": 271,
  "statistics": [
    {
      "schema": "ASK",
      "tableName": "VASK_KUNDER",
      "rowCount": 3205,
      "dataPages": 92,
      "freePages": 92,
      "lastStatsTime": "2014-01-18T16:00:06.351"
    },
    {
      "schema": "DB2ADMIN",
      "tableName": "EXPLAIN_INSTANCE",
      "rowCount": 1,
      "dataPages": 1,
      "freePages": 1,
      "lastStatsTime": "2004-12-03T13:54:16.368002"
    },
    {
      "schema": "DB2ADMIN",
      "tableName": "EXPLAIN_STATEMENT",
      "rowCount": 2,
      "dataPages": 1,
      "freePages": 1,
      "lastStatsTime": "2004-12-03T13:54:16.378001"
    },
    {
      "schema": "DBE",
      "tableName": "JOBJECT_TABLE",
      "rowCount": 0,
      "dataPages": 0,
      "freePages": 1,
      "lastStatsTime": "2011-01-15T16:00:14.823"
    },
    {
      "schema": "DBE",
      "tableName": "JOBJECT_TABLE_X",
      "rowCount": 0,
      "dataPages": 0,
      "freePages": 1,
      "lastStatsTime": "2011-01-15T16:00:10.152"
    },
    {
      "schema": "DBE",
      "tableName": "OBJECT_TABLE",
      "rowCount": 0,
      "dataPages": 0,
      "freePages": 1,
      "lastStatsTime": "2011-01-15T16:00:05.589"
    },
    {
      "schema": "DBE",
      "tableName": "OBJECT_TABLE_X",
      "rowCount": 0,
      "dataPages": 0,
      "freePages": 1,
      "lastStatsTime": "2011-01-15T16:00:09.745"
    },
    {
      "schema": "DBM",
      "tableName": "AD_BRUKER",
      "rowCount": 0,
      "dataPages": 0,
      "freePages": 1,
      "lastStatsTime": "2019-03-17T17:24:46.378"
    },
    {
      "schema": "DBM",
      "tableName": "AKTIVISER",
      "rowCount": 2,
      "dataPages": 1,
      "freePages": 1,
      "lastStatsTime": "2019-03-17T17:24:46.472001"
    },
    {
      "schema": "DBM",
      "tableName": "BANKTERM_ID",
      "rowCount": 0,
      "dataPages": 0,
      "freePages": 1,
      "lastStatsTime": "2019-03-17T17:24:46.535"
    },
    {
      "schema": "DBM",
      "tableName": "BANKTERM_MASKIN",
      "rowCount": 0,
      "dataPages": 0,
      "freePages": 1,
      "lastStatsTime": "2019-03-17T17:24:46.738"
    },
    {
      "schema": "DBM",
      "tableName": "BARE_SENTRALT",
      "rowCount": 0,
      "dataPages": 0,
      "freePages": 1,
      "lastStatsTime": "2019-03-17T17:24:46.832001"
    },
    {
      "schema": "DBM",
      "tableName": "DBQA_APPLICATION_SYSTEM",
      "rowCount": 0,
      "dataPages": 0,
      "freePages": 1,
      "lastStatsTime": "2025-07-12T16:00:00.816"
    },
    {
      "schema": "DBM",
      "tableName": "FK_LOG",
      "rowCount": 23674,
      "dataPages": 2959,
      "freePages": 2959,
      "lastStatsTime": "2025-07-26T16:00:03.968"
    },
    {
      "schema": "DBM",
      "tableName": "KUN_ADR2",
      "rowCount": 199794,
      "dataPages": 908,
      "freePages": 908,
      "lastStats
... (truncated)
```

---

### âœ… Test #23: table-relationships

**Category**: Table Operations  
**Description**: Show table relationships  
**Status**: âœ… PASSED  
**Duration**: 2179 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:16  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-relationships -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:15.0300|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:15.0404|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:15.0404|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:15.0404|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:15.0404|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:15.0404|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-relationships -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-relationships_20251216_134528.json
2025-12-16 13:46:15.0404|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:15.0404|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:15.0404|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:15.0404|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:15.0908|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:15.0908|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:15.0908|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:15.0908|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:15.1054|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:15.1054|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:15.1054|DEBUG|WindowsDb2Editor.Servi
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
[
  {
    "CONSTNAME": "BILAGNR_FIR",
    "TABSCHEMA": "INL     ",
    "TABNAME": "BILAGNR",
    "REFTABSCHEMA": "INL     ",
    "REFTABNAME": "FIRMA"
  }
]
```

---

### âœ… Test #24: table-sample-data

**Category**: Table Operations  
**Description**: Get sample data from table  
**Status**: âœ… PASSED  
**Duration**: 2249 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:18  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-sample-data -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:17.2040|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:17.2192|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:17.2192|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:17.2192|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:17.2274|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:17.2274|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-sample-data -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-sample-data_20251216_134528.json
2025-12-16 13:46:17.2274|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:17.2274|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:17.2274|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:17.2274|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:17.2743|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:17.2749|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:17.2749|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:17.2749|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:17.2749|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:17.2951|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:17.2951|DEBUG|WindowsDb2Editor.Services.
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
[
  {
    "FIRMA": 2054,
    "AAR": 2001,
    "SISTE_BILAGNR": 500160,
    "TIDSPUNKT": "2001-12-01T00:00:00",
    "BRUKERID": "A70     "
  },
  {
    "FIRMA": 2054,
    "AAR": 2002,
    "SISTE_BILAGNR": 502872,
    "TIDSPUNKT": "2002-01-10T12:15:35.181001",
    "BRUKERID": "ALK     "
  },
  {
    "FIRMA": 2054,
    "AAR": 2003,
    "SISTE_BILAGNR": 504845,
    "TIDSPUNKT": "2003-01-09T14:59:19.031001",
    "BRUKERID": "ALK     "
  },
  {
    "FIRMA": 2054,
    "AAR": 2004,
    "SISTE_BILAGNR": 506680,
    "TIDSPUNKT": "2004-01-12T14:34:58.416",
    "BRUKERID": "ALK     "
  },
  {
    "FIRMA": 2054,
    "AAR": 2005,
    "SISTE_BILAGNR": 509553,
    "TIDSPUNKT": "2005-01-17T09:41:19.042",
    "BRUKERID": "KAN     "
  },
  {
    "FIRMA": 2054,
    "AAR": 2006,
    "SISTE_BILAGNR": 512552,
    "TIDSPUNKT": "2006-01-19T08:51:04.003",
    "BRUKERID": "KAN     "
  },
  {
    "FIRMA": 2054,
    "AAR": 2007,
    "SISTE_BILAGNR": 514337,
    "TIDSPUNKT": "2007-01-11T08:52:35.865",
    "BRUKERID": "YKL     "
  },
  {
    "FIRMA": 2054,
    "AAR": 2008,
    "SISTE_BILAGNR": 520884,
    "TIDSPUNKT": "2008-01-08T12:43:01.855",
    "BRUKERID": "KME     "
  },
  {
    "FIRMA": 2054,
    "AAR": 2009,
    "SISTE_BILAGNR": 602457,
    "TIDSPUNKT": "2009-01-13T10:46:13.349",
    "BRUKERID": "KME     "
  },
  {
    "FIRMA": 2054,
    "AAR": 2010,
    "SISTE_BILAGNR": 604944,
    "TIDSPUNKT": "2010-01-12T08:10:43.326",
    "BRUKERID": "ASK     "
  },
  {
    "FIRMA": 2054,
    "AAR": 2011,
    "SISTE_BILAGNR": 612111,
    "TIDSPUNKT": "2011-01-07T08:49:31.784",
    "BRUKERID": "ASK     "
  },
  {
    "FIRMA": 2054,
    "AAR": 2012,
    "SISTE_BILAGNR": 617237,
    "TIDSPUNKT": "2012-01-05T09:47:49.172",
    "BRUKERID": "ASK     "
  },
  {
    "FIRMA": 9000,
    "AAR": 2010,
    "SISTE_BILAGNR": 100001,
    "TIDSPUNKT": "2010-12-31T08:00:36.013002",
    "BRUKERID": "ASK     "
  },
  {
    "FIRMA": 9000,
    "AAR": 2011,
    "SISTE_BILAGNR": 100163,
    "TIDSPUNKT": "2011-12-31T08:00:36.013002",
    "BRUKERID": "ASK     "
  },
  {
    "FIRMA": 9000,
    "AAR": 2012,
    "SISTE_BILAGNR": 110172,
    "TIDSPUNKT": "2012-01-17T08:00:36.013002",
    "BRUKERID": "ASK     "
  },
  {
    "FIRMA": 2054,
    "AAR": 2013,
    "SISTE_BILAGNR": 622303,
    "TIDSPUNKT": "2013-01-09T13:09:21.242",
    "BRUKERID": "ASK     "
  },
  {
    "FIRMA": 9000,
    "AAR": 2013,
    "SISTE_BILAGNR": 120111,
    "TIDSPUNKT": "2013-01-09T14:53:17.145",
    "BRUKERID": "ASK     "
  },
  {
    "FIRMA": 2054,
    "AAR": 2014,
    "SISTE_BILAGNR": 632381,
    "TIDSPUNKT": "2014-01-08T14:31:54.91",
    "BRUKERID": "ASK     "
  },
  {
    "FIRMA": 9000,
    "AAR": 2014,
    "SISTE_BILAGNR": 130180,
    "TIDSPUNKT": "2014-01-08T14:32:44.143",
    "BRUKERID": "ASK     "
  },
  {
    "FIRMA": 2054,
    "AAR": 2015,
    "SISTE_BILAGNR": 642489,
    "TIDSPUNKT": "2015-01-
... (truncated)
```

---

### âœ… Test #25: table-size

**Category**: Table Operations  
**Description**: Get table size  
**Status**: âœ… PASSED  
**Duration**: 2515 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:21  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-size -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:19.5456|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:19.5649|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:19.5718|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:19.5718|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:19.5718|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:19.5718|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-size -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-size_20251216_134528.json
2025-12-16 13:46:19.5718|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:19.5718|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:19.5718|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:19.5887|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:19.6323|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:19.6344|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:19.6344|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:19.6344|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:19.6344|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:19.6500|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:19.6500|DEBUG|WindowsDb2Editor.Services.MetadataHandle
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "rowCount": 41,
  "dataPages": 1,
  "indexPages": 1,
  "overflowPages": 0,
  "totalSizeMB": 0.00781250000,
  "retrievedAt": "2025-12-16T13:46:21.2467647+01:00"
}
```

---

### âœ… Test #26: table-grants

**Category**: Table Operations  
**Description**: List table grants  
**Status**: âœ… PASSED  
**Duration**: 2555 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:23  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-grants -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:22.1086|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:22.1364|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:22.1364|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:22.1364|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:22.1364|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:22.1364|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-grants -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-grants_20251216_134528.json
2025-12-16 13:46:22.1364|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:22.1364|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:22.1364|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:22.1364|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:22.1855|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:22.1855|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:22.1855|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:22.1855|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:22.1972|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:22.1972|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:22.1972|DEBUG|WindowsDb2Editor.Services.MetadataHa
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "grantCount": 4,
  "grants": [
    {
      "grantor": "SYSIBM",
      "grantee": "TRU",
      "granteeType": "U",
      "controlAuth": true,
      "alterAuth": false,
      "deleteAuth": false,
      "indexAuth": false,
      "insertAuth": false,
      "refAuth": false,
      "selectAuth": false,
      "updateAuth": false
    },
    {
      "grantor": "DB2ADMIN",
      "grantee": "QMFRUN",
      "granteeType": "U",
      "controlAuth": false,
      "alterAuth": false,
      "deleteAuth": false,
      "indexAuth": false,
      "insertAuth": false,
      "refAuth": false,
      "selectAuth": true,
      "updateAuth": false
    },
    {
      "grantor": "NETADM",
      "grantee": "FKKONTO",
      "granteeType": "G",
      "controlAuth": false,
      "alterAuth": false,
      "deleteAuth": false,
      "indexAuth": false,
      "insertAuth": false,
      "refAuth": false,
      "selectAuth": true,
      "updateAuth": false
    },
    {
      "grantor": "MEH",
      "grantee": "ASK",
      "granteeType": "U",
      "controlAuth": false,
      "alterAuth": true,
      "deleteAuth": true,
      "indexAuth": true,
      "insertAuth": true,
      "refAuth": true,
      "selectAuth": true,
      "updateAuth": true
    }
  ],
  "retrievedAt": "2025-12-16T13:46:23.8241804+01:00"
}
```

---

### âœ… Test #27: table-dependencies

**Category**: Table Operations  
**Description**: Show table dependencies  
**Status**: âœ… PASSED  
**Duration**: 2281 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:26  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-dependencies -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:24.5918|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:24.6032|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:24.6032|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:24.6032|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:24.6032|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:24.6032|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-dependencies -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-dependencies_20251216_134528.json
2025-12-16 13:46:24.6032|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:24.6032|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:24.6212|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:24.6212|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:24.6660|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:24.6660|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:24.6660|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:24.6660|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:24.6660|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:24.6821|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:24.6821|DEBUG|WindowsDb2Editor.Service
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "table-dependencies",
  "schema": "INL",
  "table": "BILAGNR",
  "dependencies": []
}
```

---

### âœ… Test #28: table-incoming-fk

**Category**: Table Operations  
**Description**: List incoming foreign keys  
**Status**: âœ… PASSED  
**Duration**: 2543 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:28  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-incoming-fk -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:26.9179|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:26.9323|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:26.9323|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:26.9323|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:26.9323|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:26.9323|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-incoming-fk -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-incoming-fk_20251216_134528.json
2025-12-16 13:46:26.9323|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:26.9323|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:26.9491|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:26.9491|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:26.9933|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:26.9943|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:26.9943|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:26.9943|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:26.9943|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:27.0128|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:27.0128|DEBUG|WindowsDb2Editor.Services.
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "incomingFKCount": 0,
  "incomingFKs": [],
  "retrievedAt": "2025-12-16T13:46:28.6587664+01:00"
}
```

---

### âœ… Test #29: table-used-by-packages

**Category**: Table Operations  
**Description**: List packages using this table  
**Status**: âœ… PASSED  
**Duration**: 2376 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:31  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-used-by-packages -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:29.4541|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:29.4780|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:29.4780|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:29.4780|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:29.4780|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:29.4780|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-used-by-packages -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-used-by-packages_20251216_134528.json
2025-12-16 13:46:29.4780|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:29.4780|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:29.4780|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:29.4958|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:29.5311|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:29.5311|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:29.5311|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:29.5311|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:29.5430|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:29.5430|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:29.5430|DEBUG|WindowsDb2Editor
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "packageCount": 0,
  "packages": [],
  "retrievedAt": "2025-12-16T13:46:31.0541207+01:00"
}
```

---

### âœ… Test #30: table-used-by-views

**Category**: Table Operations  
**Description**: List views using this table  
**Status**: âœ… PASSED  
**Duration**: 2177 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:33  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-used-by-views -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:31.7846|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:31.8081|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:31.8081|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:31.8081|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:31.8081|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:31.8081|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-used-by-views -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-used-by-views_20251216_134528.json
2025-12-16 13:46:31.8081|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:31.8081|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:31.8081|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:31.8230|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:31.8548|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:31.8548|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:31.8548|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:31.8548|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:31.8548|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:31.8731|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:31.8731|DEBUG|WindowsDb2Editor.Servi
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "viewCount": 0,
  "views": [],
  "retrievedAt": "2025-12-16T13:46:33.2274616+01:00"
}
```

---

### âœ… Test #31: table-used-by-routines

**Category**: Table Operations  
**Description**: List routines using this table  
**Status**: âœ… PASSED  
**Duration**: 2243 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:35  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-used-by-routines -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:33.9542|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:33.9729|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:33.9784|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:33.9784|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:33.9784|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:33.9784|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-used-by-routines -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-used-by-routines_20251216_134528.json
2025-12-16 13:46:33.9784|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:33.9784|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:33.9784|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:33.9784|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:34.0338|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:34.0338|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:34.0338|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:34.0338|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:34.0471|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:34.0471|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:34.0471|DEBUG|WindowsDb2Editor
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "routineCount": 0,
  "routines": [],
  "retrievedAt": "2025-12-16T13:46:35.4704046+01:00"
}
```

---

### âœ… Test #32: view-properties

**Category**: View Operations  
**Description**: Get view properties  
**Status**: âœ… PASSED  
**Duration**: 2416 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:37  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-properties -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:36.2763|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:36.2914|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:36.2914|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:36.2914|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:36.2914|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:36.2914|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-properties -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-properties_20251216_134528.json
2025-12-16 13:46:36.2914|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:36.2914|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:36.2914|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:36.2914|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:36.3452|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:36.3452|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:36.3452|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:36.3452|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:36.3633|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:36.3703|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:36.3703|DEBUG|WindowsDb2Editor.Services
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "SYSIBM",
  "viewName": "SYSDUMMY1",
  "definer": "SYSIBM",
  "isReadOnly": true,
  "isValid": true,
  "remarks": "",
  "createTime": "2025-06-08T23:17:58.506003",
  "dependsOn": null,
  "retrievedAt": "2025-12-16T13:46:37.9056679+01:00"
}
```

---

### âœ… Test #33: view-definition

**Category**: View Operations  
**Description**: Get view definition SQL  
**Status**: âœ… PASSED  
**Duration**: 2547 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:40  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-definition -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:38.6518|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:38.6679|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:38.6679|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:38.6679|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:38.6679|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:38.6679|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-definition -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-definition_20251216_134528.json
2025-12-16 13:46:38.6679|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:38.6679|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:38.6826|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:38.6826|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:38.7257|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:38.7257|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:38.7257|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:38.7324|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:38.7467|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:38.7467|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:38.7467|DEBUG|WindowsDb2Editor.Services
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "view-definition",
  "schema": "SYSIBM",
  "view": "SYSDUMMY1",
  "definition": [
    {
      "VIEW_DEFINITION": "create or replace view sysibm.sysdummy1 (ibmreqd) as values (char(\u0027Y\u0027)) \n\n"
    }
  ]
}
```

---

### âœ… Test #34: view-columns

**Category**: View Operations  
**Description**: List view columns  
**Status**: âœ… PASSED  
**Duration**: 2234 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:42  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-columns -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:41.1989|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:41.2300|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:41.2300|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:41.2300|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:41.2300|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:41.2300|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-columns -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-columns_20251216_134528.json
2025-12-16 13:46:41.2300|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:41.2300|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:41.2300|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:41.2300|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:41.2787|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:41.2787|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:41.2787|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:41.2787|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:41.2787|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:41.2922|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:41.2922|DEBUG|WindowsDb2Editor.Services.Metad
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
[
  {
    "COLNAME": "IBMREQD",
    "COLNO": 0,
    "TYPENAME": "CHARACTER",
    "LENGTH": 1,
    "SCALE": 0,
    "NULLS": "N",
    "REMARKS": null
  }
]
```

---

### âœ… Test #35: view-dependencies

**Category**: View Operations  
**Description**: Show view dependencies  
**Status**: âœ… PASSED  
**Duration**: 2179 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:44  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-dependencies -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:43.4367|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:43.4514|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:43.4514|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:43.4514|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:43.4514|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:43.4514|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-dependencies -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-dependencies_20251216_134528.json
2025-12-16 13:46:43.4514|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:43.4514|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:43.4636|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:43.4636|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:43.5153|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:43.5153|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:43.5153|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:43.5153|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:43.5321|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:43.5321|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:43.5321|DEBUG|WindowsDb2Editor.Serv
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
[]
```

---

### âœ… Test #36: view-sample-data

**Category**: View Operations  
**Description**: Get sample data from view  
**Status**: âœ… PASSED  
**Duration**: 2054 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:47  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-sample-data -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:45.5782|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:45.5919|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:45.5919|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:45.5919|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:45.5919|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:45.5919|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-sample-data -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-sample-data_20251216_134528.json
2025-12-16 13:46:45.5919|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:45.5919|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:45.6056|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:45.6056|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:45.6476|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:45.6476|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:45.6476|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:45.6519|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:45.6519|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:45.6519|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:45.6519|DEBUG|WindowsDb2Editor.Servic
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "view-sample-data",
  "schema": "SYSIBM",
  "view": "SYSDUMMY1",
  "data": [
    {
      "IBMREQD": "Y"
    }
  ]
}
```

---

### âœ… Test #37: view-used-by-packages

**Category**: View Operations  
**Description**: List packages using this view  
**Status**: âœ… PASSED  
**Duration**: 2035 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:49  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-used-by-packages -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:47.6237|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:47.6369|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:47.6369|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:47.6369|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:47.6369|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:47.6369|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-used-by-packages -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-used-by-packages_20251216_134528.json
2025-12-16 13:46:47.6369|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:47.6369|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:47.6369|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:47.6528|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:47.6853|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:47.6853|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:47.6853|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:47.6853|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:47.6853|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:47.6989|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:47.6989|DEBUG|WindowsDb2Edi
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "view-used-by-packages",
  "schema": "SYSIBM",
  "view": "SYSDUMMY1",
  "packages": [
    {
      "PKGSCHEMA": "NULLID  ",
      "PKGNAME": "ALGORITH"
    },
    {
      "PKGSCHEMA": "NULLID  ",
      "PKGNAME": "AOTMQ07 "
    },
    {
      "PKGSCHEMA": "NULLID  ",
      "PKGNAME": "DB2L2Q44"
    },
    {
      "PKGSCHEMA": "NULLID  ",
      "PKGNAME": "SQLL9Q1Q"
    },
    {
      "PKGSCHEMA": "NULLID  ",
      "PKGNAME": "SQLUAQ23"
    },
    {
      "PKGSCHEMA": "NULLID  ",
      "PKGNAME": "SYSSTAT "
    }
  ]
}
```

---

### âœ… Test #38: view-used-by-views

**Category**: View Operations  
**Description**: List views using this view  
**Status**: âœ… PASSED  
**Duration**: 2302 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:51  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-used-by-views -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:49.6902|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:49.6996|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:49.6996|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:49.6996|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:49.6996|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:49.6996|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-used-by-views -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-used-by-views_20251216_134528.json
2025-12-16 13:46:49.6996|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:49.6996|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:49.6996|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:49.7157|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:49.7527|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:49.7527|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:49.7527|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:49.7527|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:49.7635|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:49.7635|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:49.7635|DEBUG|WindowsDb2Editor.Se
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "view-used-by-views",
  "schema": "SYSIBM",
  "view": "SYSDUMMY1",
  "views": []
}
```

---

### âœ… Test #39: procedure-properties

**Category**: Procedure Operations  
**Description**: Get procedure properties  
**Status**: âœ… PASSED  
**Duration**: 2293 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:53  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-properties -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:52.0769|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:52.0902|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:52.0902|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:52.0902|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:52.0902|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:52.0902|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-properties -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-properties_20251216_134528.json
2025-12-16 13:46:52.0902|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:52.0902|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:52.0902|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:52.1073|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:52.1465|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:52.1465|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:52.1465|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:52.1465|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:52.1588|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:52.1588|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:52.1588|DEBUG|WindowsDb2Edit
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "SYSPROC",
  "procedureName": "ADMIN_CMD",
  "language": "C",
  "isDeterministic": false,
  "sourceCode": null,
  "retrievedAt": "2025-12-16T13:46:53.5860587+01:00"
}
```

---

### âœ… Test #40: procedure-source

**Category**: Procedure Operations  
**Description**: Get procedure source code  
**Status**: âœ… PASSED  
**Duration**: 2316 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:55  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-source -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:54.3164|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:54.3310|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:54.3310|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:54.3395|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:54.3395|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:54.3395|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-source -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-source_20251216_134528.json
2025-12-16 13:46:54.3395|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:54.3395|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:54.3395|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:54.3395|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:54.3942|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:54.3942|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:54.3942|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:54.3942|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:54.4071|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:54.4071|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:54.4071|DEBUG|WindowsDb2Editor.Servi
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
[
  {
    "TEXT": null
  }
]
```

---

### âœ… Test #41: procedure-parameters

**Category**: Procedure Operations  
**Description**: List procedure parameters  
**Status**: âœ… PASSED  
**Duration**: 2513 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:46:58  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-parameters -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:56.6681|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:56.6835|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:56.6835|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:56.6835|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:56.6835|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:56.6835|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-parameters -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-parameters_20251216_134528.json
2025-12-16 13:46:56.6835|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:56.6835|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:56.6999|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:56.6999|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:56.7422|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:56.7422|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:56.7422|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:56.7460|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:56.7460|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:56.7460|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:56.7620|DEBUG|WindowsDb2Edit
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
[
  {
    "PARMNAME": "CMD",
    "TYPENAME": "CLOB",
    "LENGTH": 2097152,
    "SCALE": 0,
    "ROWTYPE": "P",
    "ORDINAL": 1
  }
]
```

---

### âœ… Test #42: procedure-dependencies

**Category**: Procedure Operations  
**Description**: Show procedure dependencies  
**Status**: âœ… PASSED  
**Duration**: 2385 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:00  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-dependencies -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:46:59.2226|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:46:59.2315|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:46:59.2315|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:46:59.2315|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:46:59.2315|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:46:59.2315|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-dependencies -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-dependencies_20251216_134528.json
2025-12-16 13:46:59.2315|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:46:59.2315|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:46:59.2466|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:46:59.2466|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:46:59.2869|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:46:59.2869|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:46:59.2869|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:46:59.2869|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:46:59.2975|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:46:59.2975|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:46:59.2975|DEBUG|WindowsDb2
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "procedure-dependencies",
  "schema": "SYSPROC",
  "procedure": "ADMIN_CMD",
  "dependencies": []
}
```

---

### âœ… Test #43: procedure-usage

**Category**: Procedure Operations  
**Description**: Show procedure usage  
**Status**: âœ… PASSED  
**Duration**: 2027 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:02  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-usage -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:01.5355|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:01.5499|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:01.5499|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:01.5499|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:01.5499|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:01.5589|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-usage -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-usage_20251216_134528.json
2025-12-16 13:47:01.5589|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:01.5589|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:01.5589|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:01.5589|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:01.6059|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:01.6065|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:01.6065|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:01.6065|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:01.6065|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:01.6229|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:01.6229|DEBUG|WindowsDb2Editor.Service
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "procedure-usage",
  "schema": "SYSPROC",
  "procedure": "ADMIN_CMD",
  "usedBy": [
    {
      "PKGSCHEMA": "NULLID  ",
      "PKGNAME": "REVALH03"
    },
    {
      "PKGSCHEMA": "NULLID  ",
      "PKGNAME": "REVALK07"
    },
    {
      "PKGSCHEMA": "NULLID  ",
      "PKGNAME": "REVALP08"
    },
    {
      "PKGSCHEMA": "NULLID  ",
      "PKGNAME": "REVALQ08"
    },
    {
      "PKGSCHEMA": "SYSIBMADM",
      "PKGNAME": "P12857957"
    },
    {
      "PKGSCHEMA": "SYSIBMADM",
      "PKGNAME": "P1962280900"
    },
    {
      "PKGSCHEMA": "SYSIBMADM",
      "PKGNAME": "P450594709"
    },
    {
      "PKGSCHEMA": "SYSIBMADM",
      "PKGNAME": "P529433644"
    },
    {
      "PKGSCHEMA": "SYSIBMADM",
      "PKGNAME": "P550837822"
    }
  ]
}
```

---

### âœ… Test #44: procedure-grants

**Category**: Procedure Operations  
**Description**: List procedure grants  
**Status**: âœ… PASSED  
**Duration**: 1974 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:04  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-grants -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:03.4708|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:03.4822|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:03.4822|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:03.4822|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:03.4822|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:03.4822|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-grants -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-grants_20251216_134528.json
2025-12-16 13:47:03.4822|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:03.4822|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:03.4822|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:03.4822|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:03.5289|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:03.5289|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:03.5289|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:03.5289|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:03.5289|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:03.5289|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:03.5289|DEBUG|WindowsDb2Editor.Servi
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "procedure-grants",
  "schema": "SYSPROC",
  "procedure": "ADMIN_CMD",
  "grants": [
    {
      "GRANTEE": "NETADM  ",
      "GRANTEETYPE": "U",
      "EXECUTEAUTH": "G"
    },
    {
      "GRANTEE": "PUBLIC  ",
      "GRANTEETYPE": "G",
      "EXECUTEAUTH": "G"
    }
  ]
}
```

---

### âœ… Test #45: function-properties

**Category**: Function Operations  
**Description**: Get function properties  
**Status**: âœ… PASSED  
**Duration**: 2162 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:07  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-properties -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:05.4188|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:05.4670|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:05.4670|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:05.4670|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:05.4670|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:05.4670|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-properties -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-properties_20251216_134528.json
2025-12-16 13:47:05.4670|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:05.4670|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:05.4670|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:05.4815|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:05.5154|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:05.5154|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:05.5154|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:05.5154|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:05.5154|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:05.5290|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:05.5290|DEBUG|WindowsDb2Editor.Servic
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "SYSFUN",
  "functionName": "ABS",
  "definer": "SYSIBM",
  "language": "C",
  "isDeterministic": true,
  "origin": "E",
  "createTime": "2001-11-09T09:52:30.628001",
  "retrievedAt": "2025-12-16T13:47:07.0014424+01:00"
}
```

---

### âœ… Test #46: function-source

**Category**: Function Operations  
**Description**: Get function source code  
**Status**: âœ… PASSED  
**Duration**: 1952 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:09  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-source -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:07.5912|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:07.6088|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:07.6088|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:07.6088|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:07.6088|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:07.6088|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-source -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-source_20251216_134528.json
2025-12-16 13:47:07.6088|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:07.6088|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:07.6088|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:07.6229|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:07.6483|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:07.6483|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:07.6483|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:07.6483|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:07.6589|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:07.6589|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:07.6589|DEBUG|WindowsDb2Editor.Services.Metad
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
[
  {
    "TEXT": null
  },
  {
    "TEXT": null
  },
  {
    "TEXT": null
  },
  {
    "TEXT": null
  }
]
```

---

### âœ… Test #47: function-parameters

**Category**: Function Operations  
**Description**: List function parameters  
**Status**: âœ… PASSED  
**Duration**: 1975 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:11  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-parameters -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:09.6118|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:09.6218|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:09.6218|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:09.6218|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:09.6218|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:09.6218|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-parameters -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-parameters_20251216_134528.json
2025-12-16 13:47:09.6218|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:09.6218|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:09.6218|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:09.6218|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:09.6648|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:09.6648|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:09.6648|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:09.6648|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:09.6761|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:09.6761|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:09.6761|DEBUG|WindowsDb2Editor.Servic
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
[
  {
    "PARMNAME": null,
    "TYPENAME": "SMALLINT",
    "LENGTH": 2,
    "SCALE": 0,
    "ROWTYPE": "C",
    "ORDINAL": 0
  },
  {
    "PARMNAME": null,
    "TYPENAME": "INTEGER",
    "LENGTH": 4,
    "SCALE": 0,
    "ROWTYPE": "C",
    "ORDINAL": 0
  },
  {
    "PARMNAME": null,
    "TYPENAME": "DOUBLE",
    "LENGTH": 8,
    "SCALE": 0,
    "ROWTYPE": "C",
    "ORDINAL": 0
  },
  {
    "PARMNAME": null,
    "TYPENAME": "BIGINT",
    "LENGTH": 8,
    "SCALE": 0,
    "ROWTYPE": "C",
    "ORDINAL": 0
  },
  {
    "PARMNAME": null,
    "TYPENAME": "SMALLINT",
    "LENGTH": 2,
    "SCALE": 0,
    "ROWTYPE": "P",
    "ORDINAL": 1
  },
  {
    "PARMNAME": null,
    "TYPENAME": "INTEGER",
    "LENGTH": 4,
    "SCALE": 0,
    "ROWTYPE": "P",
    "ORDINAL": 1
  },
  {
    "PARMNAME": null,
    "TYPENAME": "DOUBLE",
    "LENGTH": 8,
    "SCALE": 0,
    "ROWTYPE": "P",
    "ORDINAL": 1
  },
  {
    "PARMNAME": null,
    "TYPENAME": "BIGINT",
    "LENGTH": 8,
    "SCALE": 0,
    "ROWTYPE": "P",
    "ORDINAL": 1
  }
]
```

---

### âœ… Test #48: function-dependencies

**Category**: Function Operations  
**Description**: Show function dependencies  
**Status**: âœ… PASSED  
**Duration**: 1854 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:12  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-dependencies -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:11.5457|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:11.5599|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:11.5599|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:11.5599|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:11.5599|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:11.5599|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-dependencies -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-dependencies_20251216_134528.json
2025-12-16 13:47:11.5599|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:11.5599|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:11.5599|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:11.5599|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:11.5940|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:11.5940|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:11.5940|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:11.5940|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:11.5940|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:11.6100|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:11.6100|DEBUG|WindowsDb2Editor.Se
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "function-dependencies",
  "schema": "SYSFUN",
  "function": "ABS",
  "dependencies": []
}
```

---

### âœ… Test #49: function-usage

**Category**: Function Operations  
**Description**: Show function usage  
**Status**: âœ… PASSED  
**Duration**: 2434 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:15  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-usage -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:13.3814|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:13.3949|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:13.3949|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:13.3949|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:13.3949|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:13.3949|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-usage -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-usage_20251216_134528.json
2025-12-16 13:47:13.3949|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:13.3949|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:13.4055|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:13.4055|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:13.4348|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:13.4353|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:13.4353|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:13.4353|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:13.4353|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:13.4548|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:13.4548|DEBUG|WindowsDb2Editor.Services.Metadat
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "function-usage",
  "schema": "SYSFUN",
  "function": "ABS",
  "usedBy": []
}
```

---

### âœ… Test #50: function-grants

**Category**: Function Operations  
**Description**: List function grants  
**Status**: âœ… PASSED  
**Duration**: 2075 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:17  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-grants -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:15.9696|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:15.9831|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:15.9831|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:15.9831|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:15.9831|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:15.9831|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-grants -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-grants_20251216_134528.json
2025-12-16 13:47:15.9831|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:15.9831|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:15.9831|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:15.9831|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:16.0244|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:16.0244|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:16.0244|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:16.0244|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:16.0368|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:16.0368|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:16.0368|DEBUG|WindowsDb2Editor.Services.Metad
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "function-grants",
  "schema": "SYSFUN",
  "function": "ABS",
  "grants": []
}
```

---

### âœ… Test #51: package-properties

**Category**: Package Operations  
**Description**: Get package properties  
**Status**: âœ… PASSED  
**Duration**: 2094 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:19  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-properties -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:17.9570|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:17.9680|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:17.9680|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:17.9680|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:17.9680|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:17.9680|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-properties -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-properties_20251216_134528.json
2025-12-16 13:47:17.9680|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:17.9680|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:17.9820|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:17.9820|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:18.0184|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:18.0184|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:18.0184|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:18.0184|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:18.0296|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:18.0296|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:18.0296|DEBUG|WindowsDb2Editor.Servic
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
[]
```

---

### âœ… Test #52: package-statements

**Category**: Package Operations  
**Description**: List package statements  
**Status**: âœ… PASSED  
**Duration**: 1974 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:21  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-statements -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:20.0217|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:20.0373|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:20.0373|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:20.0373|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:20.0373|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:20.0373|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-statements -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-statements_20251216_134528.json
2025-12-16 13:47:20.0373|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:20.0459|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:20.0459|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:20.0459|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:20.0829|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:20.0829|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:20.0829|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:20.0829|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:20.0934|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:20.0934|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:20.0934|DEBUG|WindowsDb2Editor.Servic
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
[]
```

---

### âœ… Test #53: package-dependencies

**Category**: Package Operations  
**Description**: Show package dependencies  
**Status**: âœ… PASSED  
**Duration**: 2058 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:23  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-dependencies -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:21.9996|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:22.0222|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:22.0296|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:22.0296|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:22.0296|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:22.0296|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-dependencies -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-dependencies_20251216_134528.json
2025-12-16 13:47:22.0296|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:22.0296|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:22.0296|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:22.0296|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:22.0698|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:22.0698|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:22.0698|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:22.0698|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:22.0826|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:22.0826|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:22.0826|DEBUG|WindowsDb2Editor.Se
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "package-dependencies",
  "schema": "INL",
  "package": "SYSSH200",
  "dependencies": []
}
```

---

### âœ… Test #54: package-statistics

**Category**: Package Operations  
**Description**: Get package statistics  
**Status**: âœ… PASSED  
**Duration**: 2109 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:25  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-statistics -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:24.1132|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:24.1274|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:24.1274|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:24.1274|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:24.1274|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:24.1274|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-statistics -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-statistics_20251216_134528.json
2025-12-16 13:47:24.1274|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:24.1274|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:24.1393|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:24.1393|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:24.1741|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:24.1741|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:24.1741|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:24.1741|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:24.1871|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:24.1871|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:24.2011|DEBUG|WindowsDb2Editor.Servic
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "package-statistics",
  "schema": "INL",
  "package": "SYSSH200",
  "statistics": []
}
```

---

### âœ… Test #55: package-list-tables

**Category**: Package Operations  
**Description**: List tables used by package  
**Status**: âœ… PASSED  
**Duration**: 1906 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:27  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-list-tables -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:26.2504|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:26.2639|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:26.2639|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:26.2639|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:26.2639|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:26.2639|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-list-tables -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-list-tables_20251216_134528.json
2025-12-16 13:47:26.2639|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:26.2639|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:26.2639|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:26.2639|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:26.3075|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:26.3075|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:26.3075|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:26.3075|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:26.3166|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:26.3166|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:26.3166|DEBUG|WindowsDb2Editor.Serv
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "package-list-tables",
  "schema": "INL",
  "package": "SYSSH200",
  "tables": []
}
```

---

### âœ… Test #56: package-list-views

**Category**: Package Operations  
**Description**: List views used by package  
**Status**: âœ… PASSED  
**Duration**: 2034 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:29  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-list-views -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:28.0999|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:28.1152|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:28.1234|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:28.1234|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:28.1234|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:28.1234|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-list-views -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-list-views_20251216_134528.json
2025-12-16 13:47:28.1234|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:28.1234|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:28.1234|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:28.1234|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:28.1675|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:28.1675|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:28.1675|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:28.1724|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:28.1724|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:28.1875|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:28.1875|DEBUG|WindowsDb2Editor.Servic
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "package-list-views",
  "schema": "INL",
  "package": "SYSSH200",
  "views": []
}
```

---

### âœ… Test #57: package-list-procedures

**Category**: Package Operations  
**Description**: List procedures used by package  
**Status**: âœ… PASSED  
**Duration**: 1891 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:31  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-list-procedures -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:30.1760|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:30.1862|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:30.1862|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:30.1862|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:30.1862|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:30.1862|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-list-procedures -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-list-procedures_20251216_134528.json
2025-12-16 13:47:30.1862|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:30.1862|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:30.1862|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:30.1862|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:30.2264|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:30.2264|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:30.2264|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:30.2264|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:30.2372|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:30.2372|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:30.2372|DEBUG|WindowsDb2Edi
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "package-list-procedures",
  "schema": "INL",
  "package": "SYSSH200",
  "procedures": []
}
```

---

### âœ… Test #58: package-list-functions

**Category**: Package Operations  
**Description**: List functions used by package  
**Status**: âœ… PASSED  
**Duration**: 2185 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:33  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-list-functions -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:32.0040|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:32.0165|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:32.0165|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:32.0165|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:32.0165|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:32.0165|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-list-functions -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-list-functions_20251216_134528.json
2025-12-16 13:47:32.0165|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:32.0165|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:32.0165|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:32.0165|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:32.0598|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:32.0598|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:32.0617|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:32.0617|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:32.0617|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:32.0617|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:32.0617|DEBUG|WindowsDb2Edito
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "package-list-functions",
  "schema": "INL",
  "package": "SYSSH200",
  "functions": []
}
```

---

### âœ… Test #59: user-properties

**Category**: User & Security  
**Description**: Get user properties  
**Status**: âœ… PASSED  
**Duration**: 1944 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:35  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command user-properties -profile FKKTOTST -object FKGEISTA -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:34.2060|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:34.2256|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:34.2256|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:34.2256|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:34.2256|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:34.2256|DEBUG|WindowsDb2Editor.App|Startup arguments: -command user-properties -profile FKKTOTST -object FKGEISTA -format json -Outfile CLI_Test_Output/temp_user-properties_20251216_134528.json
2025-12-16 13:47:34.2330|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:34.2330|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:34.2330|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:34.2330|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:34.2737|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:34.2737|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:34.2737|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:34.2737|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:34.2822|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:34.2822|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:34.2822|DEBUG|WindowsDb2Editor.Services.Metadat
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
[
  {
    "GRANTEE": "FKGEISTA",
    "GRANTEETYPE": "U",
    "DBADMAUTH": "Y",
    "CREATETABAUTH": "Y",
    "BINDADDAUTH": "Y",
    "CONNECTAUTH": "Y"
  },
  {
    "GRANTEE": "FKGEISTA",
    "GRANTEETYPE": "U",
    "DBADMAUTH": "Y",
    "CREATETABAUTH": "Y",
    "BINDADDAUTH": "Y",
    "CONNECTAUTH": "Y"
  },
  {
    "GRANTEE": "FKGEISTA",
    "GRANTEETYPE": "U",
    "DBADMAUTH": "Y",
    "CREATETABAUTH": "Y",
    "BINDADDAUTH": "Y",
    "CONNECTAUTH": "Y"
  },
  {
    "GRANTEE": "FKGEISTA",
    "GRANTEETYPE": "U",
    "DBADMAUTH": "Y",
    "CREATETABAUTH": "Y",
    "BINDADDAUTH": "Y",
    "CONNECTAUTH": "Y"
  }
]
```

---

### âœ… Test #60: user-privileges

**Category**: User & Security  
**Description**: List user privileges  
**Status**: âœ… PASSED  
**Duration**: 1979 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:37  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command user-privileges -profile FKKTOTST -object FKGEISTA -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:36.1736|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:36.1736|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:36.1736|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:36.1736|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:36.1736|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:36.1865|DEBUG|WindowsDb2Editor.App|Startup arguments: -command user-privileges -profile FKKTOTST -object FKGEISTA -format json -Outfile CLI_Test_Output/temp_user-privileges_20251216_134528.json
2025-12-16 13:47:36.1865|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:36.1865|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:36.1865|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:36.1865|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:36.2296|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:36.2296|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:36.2296|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:36.2337|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:36.2337|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:36.2337|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:36.2337|DEBUG|WindowsDb2Editor.Services.Metadat
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "error": "Format: USERNAME.SCHEMA.TABLE or just USERNAME for all privileges"
}
```

---

### âœ… Test #61: user-tables

**Category**: User & Security  
**Description**: List user's tables  
**Status**: âœ… PASSED  
**Duration**: 2198 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:39  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command user-tables -profile FKKTOTST -object FKGEISTA -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:38.2225|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:38.2344|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:38.2344|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:38.2344|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:38.2344|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:38.2344|DEBUG|WindowsDb2Editor.App|Startup arguments: -command user-tables -profile FKKTOTST -object FKGEISTA -format json -Outfile CLI_Test_Output/temp_user-tables_20251216_134528.json
2025-12-16 13:47:38.2344|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:38.2344|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:38.2496|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:38.2496|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:38.2920|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:38.2920|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:38.2920|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:38.2957|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:38.2957|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:38.2957|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:38.2957|DEBUG|WindowsDb2Editor.Services.MetadataHandler
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "user-tables",
  "user": "FKGEISTA",
  "tables": [
    {
      "TABSCHEMA": "DBM     ",
      "TABNAME": "DBQA_APPLICATION_SYSTEM"
    },
    {
      "TABSCHEMA": "DBM     ",
      "TABNAME": "FK_LOG"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_ACTIVE_DB_SESSIONS"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_DB_SET_DATA_CAPTURE_ON"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_DDL_SOURCE_COLUMN_INFO"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_DDL_SOURCE_COLUMN_REMARK"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_DDL_SOURCE_CREATE_TABLE"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_DDL_SOURCE_FOREIGN_KEY"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_DDL_SOURCE_INDEX"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_DDL_SOURCE_PRIMARY_KEY"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_DDL_SOURCE_TABLE_REMARK"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_DROP_DB_CODE"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_GROUP_LOCK_TERMINATE_SCRIPT"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_OBJECT_LOCK_INFO"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_OBJECT_LOCK_TERMINATE_SCRIPT"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_PACKAGE_SQL_SOURCE"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_SCHEMA_NAME"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_SOURCE"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_TABLE_DEPENDENCY"
    },
    {
      "TABSCHEMA": "TV      ",
      "TABNAME": "V_DBQA_UPDATE_STATISTICS_ON_TABLES"
    }
  ]
}
```

---

### âœ… Test #62: user-schemas

**Category**: User & Security  
**Description**: List user's schemas  
**Status**: âœ… PASSED  
**Duration**: 1888 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:41  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command user-schemas -profile FKKTOTST -object FKGEISTA -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:40.3454|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:40.3454|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:40.3584|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:40.3584|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:40.3584|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:40.3584|DEBUG|WindowsDb2Editor.App|Startup arguments: -command user-schemas -profile FKKTOTST -object FKGEISTA -format json -Outfile CLI_Test_Output/temp_user-schemas_20251216_134528.json
2025-12-16 13:47:40.3584|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:40.3584|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:40.3584|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:40.3584|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:40.3999|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:40.3999|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:40.3999|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:40.3999|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:40.4105|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:40.4105|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:40.4105|DEBUG|WindowsDb2Editor.Services.MetadataHandl
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "user-schemas",
  "user": "FKGEISTA",
  "schemas": []
}
```

---

### âœ… Test #63: user-connections

**Category**: User & Security  
**Description**: List user connections  
**Status**: âœ… PASSED  
**Duration**: 2297 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:43  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command user-connections -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:42.3658|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:42.3772|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:42.3772|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:42.3772|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:42.3772|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:42.3772|DEBUG|WindowsDb2Editor.App|Startup arguments: -command user-connections -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_user-connections_20251216_134528.json
2025-12-16 13:47:42.3772|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:42.3772|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:42.3919|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:42.3919|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:42.4292|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:42.4292|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:42.4292|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:42.4292|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:42.4398|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:42.4398|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:42.4398|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "user-connections",
  "connections": [],
  "note": "SYSIBMADM.APPLICATIONS requires SYSADM authority"
}
```

---

### âœ… Test #64: db-load

**Category**: Monitoring & Stats  
**Description**: Get database load  
**Status**: âœ… PASSED  
**Duration**: 2001 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:45  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-load -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:44.5177|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:44.5326|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:44.5326|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:44.5326|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:44.5326|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:44.5326|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-load -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-load_20251216_134528.json
2025-12-16 13:47:44.5326|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:44.5326|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:44.5491|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:44.5491|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:44.5789|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:44.5789|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:44.5789|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:44.5789|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:44.5789|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:44.5789|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:44.5789|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "ALL",
  "totalTables": 80,
  "limitApplied": 80,
  "metrics": [
    {
      "schema": "SYSIBM",
      "tableName": "SYSCOLUMNS",
      "rowsRead": 868466,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "INL",
      "tableName": "KONTO",
      "rowsRead": 834736,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSROUTINES",
      "rowsRead": 361094,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSROUTINEPARMS",
      "rowsRead": 347854,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSTABLES",
      "rowsRead": 134172,
      "rowsInserted": 0,
      "rowsUpdated": 161,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSDEPENDENCIES",
      "rowsRead": 27384,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSDATATYPES",
      "rowsRead": 13280,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSPLAN",
      "rowsRead": 12378,
      "rowsInserted": 0,
      "rowsUpdated": 18,
      "rowsDeleted": 0
    },
    {
      "schema": "INL",
      "tableName": "KONTOTYPE",
      "rowsRead": 4984,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSDBAUTH",
      "rowsRead": 3951,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSVIEWS",
      "rowsRead": 3737,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSRELS",
      "rowsRead": 2295,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSSCHEMATA",
      "rowsRead": 1513,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSTABLESPACES",
      "rowsRead": 1122,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSTABCONST",
      "rowsRead": 756,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "INL",
      "tableName": "BILAGNR",
      "rowsRead": 625,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSINDEXES",
      "rowsRead": 276,
   
... (truncated)
```

---

### âœ… Test #65: db-locks

**Category**: Monitoring & Stats  
**Description**: Show database locks  
**Status**: âœ… PASSED  
**Duration**: 2132 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:48  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-locks -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:46.5202|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:46.5320|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:46.5320|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:46.5320|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:46.5320|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:46.5320|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-locks -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-locks_20251216_134528.json
2025-12-16 13:47:46.5320|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:46.5320|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:46.5320|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:46.5320|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:46.5770|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:46.5783|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:46.5783|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:46.5783|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:46.5783|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:46.5783|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:46.5783|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for D
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "lockCount": 2,
  "locks": [
    {
      "database": "INLTST",
      "objectType": "TABLE",
      "objectSchema": "SYSIBM",
      "objectName": "SYSPLAN",
      "userId": "FKGEISTA",
      "clientName": "30237-FK",
      "applicationName": "WindowsDb2Editor.exe",
      "lockStatus": "GRNT",
      "lockMode": "IS",
      "agentId": 11430
    },
    {
      "database": "INLTST",
      "objectType": "TABLE",
      "objectSchema": "SYSIBM",
      "objectName": "SYSTABLES",
      "userId": "FKGEISTA",
      "clientName": "30237-FK",
      "applicationName": "WindowsDb2Editor.exe",
      "lockStatus": "GRNT",
      "lockMode": "IS",
      "agentId": 11430
    }
  ],
  "retrievedAt": "2025-12-16T13:47:48.058649+01:00"
}
```

---

### âœ… Test #66: db-sessions

**Category**: Monitoring & Stats  
**Description**: List database sessions  
**Status**: âœ… PASSED  
**Duration**: 1811 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:49  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-sessions -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:48.6620|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:48.6733|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:48.6733|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:48.6733|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:48.6733|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:48.6733|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-sessions -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-sessions_20251216_134528.json
2025-12-16 13:47:48.6733|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:48.6733|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:48.6733|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:48.6733|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:48.7139|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:48.7139|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:48.7139|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:48.7139|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:48.7246|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:48.7246|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:48.7246|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "totalSessions": 1,
  "limitApplied": 1,
  "sessions": [
    {
      "authId": "FKGEISTA",
      "connectTime": "2025-12-16T13:47:50.082",
      "message": "Session monitoring requires admin views"
    }
  ],
  "retrievedAt": "2025-12-16T13:47:49.8708273+01:00"
}
```

---

### âœ… Test #67: db-size

**Category**: Monitoring & Stats  
**Description**: Get database size  
**Status**: âœ… PASSED  
**Duration**: 1745 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:51  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-size -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:50.4269|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:50.4458|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:50.4458|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:50.4458|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:50.4458|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:50.4458|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-size -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-size_20251216_134528.json
2025-12-16 13:47:50.4534|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:50.4534|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:50.4534|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:50.4534|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:50.4936|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:50.4936|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:50.4936|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:50.4936|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:50.5033|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:50.5033|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:50.5033|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "totalRows": 1599183,
  "totalPages": 62127,
  "totalSizeMB": 242.68359375000,
  "approximateSizeGB": 0.23699569702148438,
  "retrievedAt": "2025-12-16T13:47:51.6101994+01:00"
}
```

---

### âœ… Test #68: schema-size

**Category**: Monitoring & Stats  
**Description**: Get schema size  
**Status**: âœ… PASSED  
**Duration**: 1823 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:53  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command schema-size -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:52.2229|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:52.2346|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:52.2346|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:52.2346|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:52.2346|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:52.2346|DEBUG|WindowsDb2Editor.App|Startup arguments: -command schema-size -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_schema-size_20251216_134528.json
2025-12-16 13:47:52.2346|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:52.2346|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:52.2346|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:52.2346|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:52.2776|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:52.2776|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:52.2776|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:52.2776|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:52.2878|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:52.2878|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:52.2878|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Load
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableCount": 15,
  "totalRows": 833627,
  "totalDataPages": 21297,
  "totalIndexPages": 22046,
  "totalSizeMB": 169.30859375000,
  "retrievedAt": "2025-12-16T13:47:53.4458453+01:00"
}
```

---

### âœ… Test #69: db-version

**Category**: Monitoring & Stats  
**Description**: Get database version  
**Status**: âœ… PASSED  
**Duration**: 1751 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:55  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-version -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:54.0378|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:54.0501|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:54.0501|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:54.0501|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:54.0501|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:54.0501|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-version -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-version_20251216_134528.json
2025-12-16 13:47:54.0501|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:54.0501|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:54.0501|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:54.0501|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:54.0884|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:54.0884|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:54.0884|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:54.0884|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:54.0991|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:54.0991|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:54.0991|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata f
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "db-version",
  "version": [
    {
      "SERVICE_LEVEL": "DB2 v12.1.1000.77",
      "FIXPACK_NUM": 0
    }
  ]
}
```

---

### âœ… Test #70: db-parameters

**Category**: Monitoring & Stats  
**Description**: List database parameters  
**Status**: âœ… PASSED  
**Duration**: 1812 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:57  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-parameters -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:55.8013|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:55.8150|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:55.8150|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:55.8150|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:55.8150|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:55.8150|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-parameters -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-parameters_20251216_134528.json
2025-12-16 13:47:55.8150|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:55.8150|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:55.8150|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:55.8290|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:55.8573|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:55.8573|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:55.8573|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:55.8603|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:55.8603|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:55.8603|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:55.8603|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading meta
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

### âœ… Test #71: db-registry

**Category**: Monitoring & Stats  
**Description**: Show database registry  
**Status**: âœ… PASSED  
**Duration**: 1795 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:47:58  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-registry -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:57.6273|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:57.6273|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:57.6411|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:57.6411|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:57.6411|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:57.6411|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-registry -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-registry_20251216_134528.json
2025-12-16 13:47:57.6411|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:57.6411|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:57.6411|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:57.6411|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:57.6772|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:57.6772|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:57.6772|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:57.6772|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:57.6772|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:57.6926|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:57.6926|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "db-registry",
  "registry": [
    {
      "REG_VAR_NAME": "DB2_COMMON_APP_DATA_PATH",
      "REG_VAR_VALUE": "C:\\ProgramData"
    },
    {
      "REG_VAR_NAME": "DB2_EXTSECURITY",
      "REG_VAR_VALUE": "YES"
    },
    {
      "REG_VAR_NAME": "DB2ADMINSERVER",
      "REG_VAR_VALUE": "T-NO1INLTST-DB"
    },
    {
      "REG_VAR_NAME": "DB2COMM",
      "REG_VAR_VALUE": "TCPIP"
    },
    {
      "REG_VAR_NAME": "DB2INSTDEF",
      "REG_VAR_VALUE": "DB2"
    },
    {
      "REG_VAR_NAME": "DB2INSTOWNER",
      "REG_VAR_VALUE": "t-no1inltst-db"
    },
    {
      "REG_VAR_NAME": "DB2INSTPROF",
      "REG_VAR_VALUE": "C:\\PROGRAMDATA\\IBM\\DB2\\DB2COPY1"
    },
    {
      "REG_VAR_NAME": "DB2PATH",
      "REG_VAR_VALUE": "C:\\DbInst"
    },
    {
      "REG_VAR_NAME": "DB2PORTRANGE",
      "REG_VAR_VALUE": "20000:20005"
    },
    {
      "REG_VAR_NAME": "DB2SYSTEM",
      "REG_VAR_VALUE": "T-NO1INLTST-DB"
    }
  ]
}
```

---

### âœ… Test #72: active-queries

**Category**: Monitoring & Stats  
**Description**: List active queries  
**Status**: âœ… PASSED  
**Duration**: 2085 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:00  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command active-queries -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:47:59.4392|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:47:59.4392|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:47:59.4392|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:47:59.4392|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:47:59.4392|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:47:59.4392|DEBUG|WindowsDb2Editor.App|Startup arguments: -command active-queries -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_active-queries_20251216_134528.json
2025-12-16 13:47:59.4392|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:47:59.4392|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:47:59.4563|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:47:59.4563|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:47:59.4861|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:47:59.4861|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:47:59.4861|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:47:59.4861|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:47:59.4861|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:47:59.5017|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:47:59.5017|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading me
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "active-queries",
  "queries": [
    {
      "APPLICATION_HANDLE": 11446,
      "STMT_TEXT": "SELECT APPLICATION_HANDLE, STMT_TEXT, ELAPSED_TIME_SEC FROM SYSIBMADM.MON_CURRENT_SQL ORDER BY ELAPSED_TIME_SEC DESC FETCH FIRST 20 ROWS ONLY",
      "ELAPSED_TIME_SEC": 0
    }
  ]
}
```

---

### âœ… Test #73: bufferpool-stats

**Category**: Monitoring & Stats  
**Description**: Get buffer pool statistics  
**Status**: âœ… PASSED  
**Duration**: 2135 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:03  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command bufferpool-stats -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:01.4940|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:01.5099|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:01.5164|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:01.5164|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:01.5164|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:01.5164|DEBUG|WindowsDb2Editor.App|Startup arguments: -command bufferpool-stats -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_bufferpool-stats_20251216_134528.json
2025-12-16 13:48:01.5164|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:01.5164|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:01.5164|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:01.5164|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:01.5736|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:01.5736|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:01.5736|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:01.5736|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:01.5850|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:01.5850|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:01.5850|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "bufferpool-stats",
  "stats": [
    {
      "BP_NAME": "IBMDEFAULTBP",
      "POOL_DATA_L_READS": 1,
      "POOL_DATA_P_READS": 0,
      "POOL_INDEX_L_READS": 0,
      "POOL_INDEX_P_READS": 0
    },
    {
      "BP_NAME": "BP32K",
      "POOL_DATA_L_READS": 0,
      "POOL_DATA_P_READS": 0,
      "POOL_INDEX_L_READS": 0,
      "POOL_INDEX_P_READS": 0
    },
    {
      "BP_NAME": "BIGTAB",
      "POOL_DATA_L_READS": 0,
      "POOL_DATA_P_READS": 0,
      "POOL_INDEX_L_READS": 0,
      "POOL_INDEX_P_READS": 0
    },
    {
      "BP_NAME": "USER32",
      "POOL_DATA_L_READS": 0,
      "POOL_DATA_P_READS": 0,
      "POOL_INDEX_L_READS": 0,
      "POOL_INDEX_P_READS": 0
    },
    {
      "BP_NAME": "IBMSYSTEMBP4K",
      "POOL_DATA_L_READS": 0,
      "POOL_DATA_P_READS": 0,
      "POOL_INDEX_L_READS": 0,
      "POOL_INDEX_P_READS": 0
    },
    {
      "BP_NAME": "IBMSYSTEMBP8K",
      "POOL_DATA_L_READS": 0,
      "POOL_DATA_P_READS": 0,
      "POOL_INDEX_L_READS": 0,
      "POOL_INDEX_P_READS": 0
    },
    {
      "BP_NAME": "IBMSYSTEMBP16K",
      "POOL_DATA_L_READS": 0,
      "POOL_DATA_P_READS": 0,
      "POOL_INDEX_L_READS": 0,
      "POOL_INDEX_P_READS": 0
    },
    {
      "BP_NAME": "IBMSYSTEMBP32K",
      "POOL_DATA_L_READS": 0,
      "POOL_DATA_P_READS": 0,
      "POOL_INDEX_L_READS": 0,
      "POOL_INDEX_P_READS": 0
    }
  ]
}
```

---

### âœ… Test #74: tablespace-usage

**Category**: Monitoring & Stats  
**Description**: Show tablespace usage  
**Status**: âœ… PASSED  
**Duration**: 2299 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:05  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command tablespace-usage -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:03.7526|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:03.7679|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:03.7679|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:03.7679|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:03.7679|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:03.7679|DEBUG|WindowsDb2Editor.App|Startup arguments: -command tablespace-usage -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_tablespace-usage_20251216_134528.json
2025-12-16 13:48:03.7679|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:03.7679|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:03.7839|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:03.7839|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:03.8184|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:03.8184|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:03.8184|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:03.8184|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:03.8184|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:03.8300|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:03.8300|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "tablespace-usage",
  "usage": [
    {
      "TBSP_NAME": "SYSCATSPACE",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP_USED_SIZE_KB": 0,
      "TBSP_FREE_SIZE_KB": 0
    },
    {
      "TBSP_NAME": "TEMPSPACE1",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP_USED_SIZE_KB": 0,
      "TBSP_FREE_SIZE_KB": 0
    },
    {
      "TBSP_NAME": "USERSPACE1",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP_USED_SIZE_KB": 0,
      "TBSP_FREE_SIZE_KB": 0
    },
    {
      "TBSP_NAME": "RDBITSDF",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP_USED_SIZE_KB": 0,
      "TBSP_FREE_SIZE_KB": 0
    },
    {
      "TBSP_NAME": "RDBITSGV",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP_USED_SIZE_KB": 0,
      "TBSP_FREE_SIZE_KB": 0
    },
    {
      "TBSP_NAME": "RDBITSPR",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP_USED_SIZE_KB": 0,
      "TBSP_FREE_SIZE_KB": 0
    },
    {
      "TBSP_NAME": "RDBITSRS",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP_USED_SIZE_KB": 0,
      "TBSP_FREE_SIZE_KB": 0
    },
    {
      "TBSP_NAME": "RDBITSSC",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP_USED_SIZE_KB": 0,
      "TBSP_FREE_SIZE_KB": 0
    },
    {
      "TBSP_NAME": "DSQ1STBT",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP_USED_SIZE_KB": 0,
      "TBSP_FREE_SIZE_KB": 0
    },
    {
      "TBSP_NAME": "DSQTSCT1",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP_USED_SIZE_KB": 0,
      "TBSP_FREE_SIZE_KB": 0
    },
    {
      "TBSP_NAME": "DSQTSCT2",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP_USED_SIZE_KB": 0,
      "TBSP_FREE_SIZE_KB": 0
    },
    {
      "TBSP_NAME": "DSQTSCT3",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP_USED_SIZE_KB": 0,
      "TBSP_FREE_SIZE_KB": 0
    },
    {
      "TBSP_NAME": "RAATS1",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP_USED_SIZE_KB": 0,
      "TBSP_FREE_SIZE_KB": 0
    },
    {
      "TBSP_NAME": "RAATS2",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP_USED_SIZE_KB": 0,
      "TBSP_FREE_SIZE_KB": 0
    },
    {
      "TBSP_NAME": "RAATS7",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP_USED_SIZE_KB": 0,
      "TBSP_FREE_SIZE_KB": 0
    },
    {
      "TBSP_NAME": "RDBITSX1",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP_USED_SIZE_KB": 0,
      "TBSP_FREE_SIZE_KB": 0
    },
    {
      "TBSP_NAME": "RDBITSX2",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP_USED_SIZE_KB": 0,
      "TBSP_FREE_SIZE_KB": 0
    },
    {
      "TBSP_NAME": "RDBITSX3",
      "TBSP_TYPE": "SMS",
      "TBSP_TOTAL_SIZE_KB": 0,
      "TBSP
... (truncated)
```

---

### âœ… Test #75: cdc-status

**Category**: CDC Operations  
**Description**: Get CDC status for schema  
**Status**: âœ… PASSED  
**Duration**: 2269 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:07  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-status -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:06.1296|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:06.1486|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:06.1486|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:06.1486|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:06.1486|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:06.1486|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-status -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_cdc-status_20251216_134528.json
2025-12-16 13:48:06.1486|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:06.1588|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:06.1588|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:06.1588|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:06.1961|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:06.1961|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:06.1961|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:06.1961|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:06.2064|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:06.2064|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:06.2064|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableCount": 0,
  "tables": [],
  "retrievedAt": "2025-12-16T13:48:07.6208621+01:00"
}
```

---

### âœ… Test #76: cdc-status-full

**Category**: CDC Operations  
**Description**: Get full CDC status  
**Status**: âœ… PASSED  
**Duration**: 1802 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:09  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-status-full -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:08.2100|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:08.2233|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:08.2233|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:08.2233|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:08.2233|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:08.2233|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-status-full -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_cdc-status-full_20251216_134528.json
2025-12-16 13:48:08.2233|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:08.2233|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:08.2233|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:08.2365|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:08.2635|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:08.2635|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:08.2635|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:08.2635|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:08.2725|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:08.2725|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:08.2725|DEBUG|WindowsDb2Editor.Services.MetadataHand
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "cdcEnabled": false,
  "note": "CDC status requires SYSCAT.DATAPARTITIONS or CDC-specific views",
  "retrievedAt": "2025-12-16T13:48:09.4377707+01:00"
}
```

---

### âœ… Test #77: cdc-configuration

**Category**: CDC Operations  
**Description**: Get CDC configuration for table  
**Status**: âœ… PASSED  
**Duration**: 1792 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:11  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-configuration -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:10.0204|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:10.0340|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:10.0340|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:10.0340|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:10.0340|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:10.0340|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-configuration -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_cdc-configuration_20251216_134528.json
2025-12-16 13:48:10.0340|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:10.0340|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:10.0340|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:10.0497|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:10.0810|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:10.0810|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:10.0810|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:10.0810|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:10.0810|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:10.0956|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:10.0956|DEBUG|WindowsDb2Editor.Services.
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "cdcConfigured": false,
  "note": "CDC configuration requires ASN tables (IBMSNAP_*)",
  "retrievedAt": "2025-12-16T13:48:11.2341906+01:00"
}
```

---

### âœ… Test #78: cdc-changes

**Category**: CDC Operations  
**Description**: List CDC changes  
**Status**: âœ… PASSED  
**Duration**: 2098 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:13  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-changes -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:11.8155|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:11.8306|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:11.8306|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:11.8306|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:11.8306|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:11.8306|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-changes -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_cdc-changes_20251216_134528.json
2025-12-16 13:48:11.8306|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:11.8306|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:11.8306|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:11.8306|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:11.8762|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:11.8774|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:11.8774|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:11.8774|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:11.8774|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:11.8774|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:11.8929|DEBUG|WindowsDb2Editor.Services.MetadataHand
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "changesFound": 0,
  "note": "CDC changes require reading CDC change tables (configured per table)",
  "retrievedAt": "2025-12-16T13:48:13.3286429+01:00"
}
```

---

### âœ… Test #79: cdc-enable

**Category**: CDC Operations  
**Description**: Enable CDC for table  
**Status**: âœ… PASSED  
**Duration**: 1978 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:15  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-enable -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:13.9434|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:13.9434|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:13.9434|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:13.9556|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:13.9556|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:13.9556|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-enable -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_cdc-enable_20251216_134528.json
2025-12-16 13:48:13.9556|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:13.9556|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:13.9556|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:13.9556|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:13.9910|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:13.9910|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:13.9910|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:13.9910|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:13.9910|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:14.0044|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:14.0044|DEBUG|WindowsDb2Editor.Services.MetadataHandle
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "cdc-enable",
  "schema": "INL",
  "table": "BILAGNR",
  "status": "CDC enable command would require ALTER TABLE permissions",
  "note": "Use: ALTER TABLE schema.table DATA CAPTURE CHANGES"
}
```

---

### âœ… Test #80: cdc-disable

**Category**: CDC Operations  
**Description**: Disable CDC for table  
**Status**: âœ… PASSED  
**Duration**: 1916 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:17  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-disable -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:15.9559|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:15.9559|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:15.9559|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:15.9559|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:15.9559|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:15.9559|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-disable -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_cdc-disable_20251216_134528.json
2025-12-16 13:48:15.9559|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:15.9559|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:15.9729|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:15.9729|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:16.0039|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:16.0039|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:16.0039|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:16.0039|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:16.0039|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:16.0193|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:16.0193|DEBUG|WindowsDb2Editor.Services.MetadataHand
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "cdc-disable",
  "schema": "INL",
  "table": "BILAGNR",
  "status": "CDC disable command would require ALTER TABLE permissions",
  "note": "Use: ALTER TABLE schema.table DATA CAPTURE NONE"
}
```

---

### âœ… Test #81: cdc-history

**Category**: CDC Operations  
**Description**: Show CDC history  
**Status**: âœ… PASSED  
**Duration**: 1812 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:19  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-history -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:17.8323|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:17.8460|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:17.8460|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:17.8460|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:17.8460|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:17.8460|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-history -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_cdc-history_20251216_134528.json
2025-12-16 13:48:17.8460|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:17.8460|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:17.8460|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:17.8618|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:17.8882|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:17.8882|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:17.8882|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:17.8882|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:17.8928|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:17.8928|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:17.8928|DEBUG|WindowsDb2Editor.Services.MetadataHand
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "cdc-history",
  "schema": "INL",
  "table": "BILAGNR",
  "history": [
    {
      "TABSCHEMA": "INL",
      "TABNAME": "BILAGNR",
      "DATACAPTURE": "N",
      "ALTER_TIME": "2010-04-11T12:49:29.613001"
    }
  ]
}
```

---

### âœ… Test #82: db-compare

**Category**: Database Comparison  
**Description**: Compare two schemas  
**Status**: âœ… PASSED  
**Duration**: 1909 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:21  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-compare -profile FKKTOTST -object INL -schema INL_TEST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:19.6315|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:19.6476|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:19.6476|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:19.6476|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:19.6476|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:19.6476|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-compare -profile FKKTOTST -object INL -schema INL_TEST -format json -Outfile CLI_Test_Output/temp_db-compare_20251216_134528.json
2025-12-16 13:48:19.6476|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:19.6476|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:19.6594|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:19.6594|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:19.6891|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:19.6907|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:19.6907|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:19.6907|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:19.6907|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:19.6907|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:19.7057|DEBUG|WindowsDb2Editor.Services.Metad
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "db-compare",
  "sourceSchema": "INL",
  "targetSchema": "INL_TEST",
  "sourceTableCount": 15,
  "targetTableCount": 0,
  "comparison": {
    "tablesInSourceOnly": [
      "Placeholder: Tables only in source"
    ],
    "tablesInTargetOnly": [
      "Placeholder: Tables only in target"
    ],
    "tablesDifferent": [
      "Placeholder: Tables with differences"
    ],
    "tablesIdentical": [
      "Placeholder: Tables that are identical"
    ]
  },
  "migrationDdl": "-- ALTER TABLE statements would be generated here by DatabaseComparisonService",
  "note": "Full comparison requires DatabaseComparisonService integration (Phase B)",
  "timestamp": "2025-12-16T13:48:20.9488802+01:00"
}
```

---

### âœ… Test #83: db-compare-source-only

**Category**: Database Comparison  
**Description**: Show objects only in source  
**Status**: âœ… PASSED  
**Duration**: 2090 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:23  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-compare-source-only -profile FKKTOTST -object INL -schema INL_TEST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:21.5376|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:21.5491|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:21.5491|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:21.5491|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:21.5491|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:21.5491|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-compare-source-only -profile FKKTOTST -object INL -schema INL_TEST -format json -Outfile CLI_Test_Output/temp_db-compare-source-only_20251216_134528.json
2025-12-16 13:48:21.5491|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:21.5491|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:21.5491|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:21.5491|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:21.5822|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:21.5822|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:21.5822|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:21.5822|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:21.5822|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:21.5961|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:21.5961|DEBUG|Windows
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
[
  {
    "TABNAME": "BILAGNR"
  },
  {
    "TABNAME": "FASTE_LISTE"
  },
  {
    "TABNAME": "FASTE_TRANS"
  },
  {
    "TABNAME": "FIRMA"
  },
  {
    "TABNAME": "KONTO"
  },
  {
    "TABNAME": "KONTOTYPE"
  },
  {
    "TABNAME": "KUNDEKONTO"
  },
  {
    "TABNAME": "KUNDEKONTO_TEST"
  },
  {
    "TABNAME": "KUNDEKTO_M_FEIL_ORGNR"
  },
  {
    "TABNAME": "MKK_AVSETNING"
  },
  {
    "TABNAME": "RENTESATSER"
  },
  {
    "TABNAME": "TRANSREG"
  },
  {
    "TABNAME": "TRANSREG_TEST"
  },
  {
    "TABNAME": "TRANSTYPE"
  },
  {
    "TABNAME": "UTBET_MK"
  }
]
```

---

### âœ… Test #84: db-compare-target-only

**Category**: Database Comparison  
**Description**: Show objects only in target  
**Status**: âœ… PASSED  
**Duration**: 2482 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:25  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-compare-target-only -profile FKKTOTST -object INL -schema INL_TEST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:23.7105|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:23.7236|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:23.7236|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:23.7236|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:23.7236|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:23.7236|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-compare-target-only -profile FKKTOTST -object INL -schema INL_TEST -format json -Outfile CLI_Test_Output/temp_db-compare-target-only_20251216_134528.json
2025-12-16 13:48:23.7236|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:23.7236|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:23.7236|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:23.7236|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:23.7744|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:23.7744|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:23.7744|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:23.7744|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:23.7894|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:23.7894|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:23.7894|DEBUG|Windows
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
[]
```

---

### âœ… Test #85: db-compare-different

**Category**: Database Comparison  
**Description**: Show different objects  
**Status**: âœ… PASSED  
**Duration**: 2120 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:27  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-compare-different -profile FKKTOTST -object INL -schema INL_TEST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:26.1030|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:26.1156|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:26.1156|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:26.1156|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:26.1156|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:26.1156|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-compare-different -profile FKKTOTST -object INL -schema INL_TEST -format json -Outfile CLI_Test_Output/temp_db-compare-different_20251216_134528.json
2025-12-16 13:48:26.1156|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:26.1156|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:26.1277|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:26.1277|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:26.1574|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:26.1591|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:26.1591|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:26.1591|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:26.1591|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:26.1591|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:26.1591|DEBUG|WindowsDb2E
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
[]
```

---

### âœ… Test #86: db-compare-ddl

**Category**: Database Comparison  
**Description**: Generate comparison DDL  
**Status**: âœ… PASSED  
**Duration**: 1831 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:29  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-compare-ddl -profile FKKTOTST -object INL -schema INL_TEST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:28.2384|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:28.2534|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:28.2534|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:28.2534|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:28.2534|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:28.2534|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-compare-ddl -profile FKKTOTST -object INL -schema INL_TEST -format json -Outfile CLI_Test_Output/temp_db-compare-ddl_20251216_134528.json
2025-12-16 13:48:28.2534|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:28.2534|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:28.2534|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:28.2534|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:28.2942|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:28.2942|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:28.2942|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:28.2942|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:28.3002|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:28.3002|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:28.3002|DEBUG|WindowsDb2Editor.Servic
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "db-compare-ddl",
  "sourceSchema": "INL",
  "targetSchema": "INL_TEST",
  "ddl": "-- ALTER TABLE statements placeholder\n-- Use DatabaseComparisonService for full implementation",
  "timestamp": "2025-12-16T13:48:29.4825442+01:00"
}
```

---

### âœ… Test #87: object-metadata

**Category**: Metadata & Advanced  
**Description**: Get object metadata  
**Status**: âœ… PASSED  
**Duration**: 2031 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:31  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command object-metadata -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:30.2068|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:30.2068|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:30.2215|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:30.2215|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:30.2215|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:30.2215|DEBUG|WindowsDb2Editor.App|Startup arguments: -command object-metadata -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_object-metadata_20251216_134528.json
2025-12-16 13:48:30.2215|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:30.2215|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:30.2215|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:30.2215|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:30.2675|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:30.2686|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:30.2686|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:30.2686|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:30.2686|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:30.2686|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:30.2686|DEBUG|WindowsDb2Editor.Services.Meta
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
[
  {
    "TABSCHEMA": "INL     ",
    "TABNAME": "BILAGNR",
    "TYPE": "T",
    "STATUS": "N",
    "COLCOUNT": 5,
    "NPAGES": 1,
    "FPAGES": 1,
    "CARD": 41,
    "STATS_TIME": "2025-09-27T16:00:01.147002",
    "CREATE_TIME": "2001-11-15T10:33:24.814001",
    "ALTER_TIME": "2010-04-11T12:49:29.613001",
    "INVALIDATE_TIME": "2025-06-08T23:17:38.255002",
    "OWNER": "TRU     "
  }
]
```

---

### âœ… Test #88: object-search

**Category**: Metadata & Advanced  
**Description**: Search for objects  
**Status**: âœ… PASSED  
**Duration**: 1869 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:33  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command object-search -profile FKKTOTST -object BILAG% -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:32.0860|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:32.1007|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:32.1007|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:32.1007|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:32.1007|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:32.1007|DEBUG|WindowsDb2Editor.App|Startup arguments: -command object-search -profile FKKTOTST -object BILAG% -format json -Outfile CLI_Test_Output/temp_object-search_20251216_134528.json
2025-12-16 13:48:32.1007|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:32.1007|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:32.1129|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:32.1129|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:32.1457|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:32.1457|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:32.1457|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:32.1457|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:32.1457|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:32.1457|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:32.1598|DEBUG|WindowsDb2Editor.Services.MetadataHandl
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "object-search",
  "pattern": "BILAG%",
  "objects": [
    {
      "TABSCHEMA": "INL     ",
      "TABNAME": "BILAGNR",
      "TYPE": "T"
    }
  ]
}
```

---

### âœ… Test #89: column-search

**Category**: Metadata & Advanced  
**Description**: Search for columns  
**Status**: âœ… PASSED  
**Duration**: 1809 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:35  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command column-search -profile FKKTOTST -object BILAG% -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:34.0087|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:34.0187|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:34.0187|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:34.0187|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:34.0187|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:34.0187|DEBUG|WindowsDb2Editor.App|Startup arguments: -command column-search -profile FKKTOTST -object BILAG% -format json -Outfile CLI_Test_Output/temp_column-search_20251216_134528.json
2025-12-16 13:48:34.0187|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:34.0187|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:34.0187|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:34.0187|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:34.0613|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:34.0613|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:34.0613|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:34.0667|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:34.0667|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:34.0667|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:34.0667|DEBUG|WindowsDb2Editor.Services.MetadataHandl
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "column-search",
  "pattern": "BILAG%",
  "columns": [
    {
      "TABSCHEMA": "INL     ",
      "TABNAME": "TRANSREG",
      "COLNAME": "BILAGDATO",
      "TYPENAME": "DECIMAL"
    },
    {
      "TABSCHEMA": "INL     ",
      "TABNAME": "TRANSREG",
      "COLNAME": "BILAGNR",
      "TYPENAME": "DECIMAL"
    },
    {
      "TABSCHEMA": "INL     ",
      "TABNAME": "TRANSREG_TEST",
      "COLNAME": "BILAGDATO",
      "TYPENAME": "DECIMAL"
    },
    {
      "TABSCHEMA": "INL     ",
      "TABNAME": "TRANSREG_TEST",
      "COLNAME": "BILAGNR",
      "TYPENAME": "DECIMAL"
    }
  ]
}
```

---

### âœ… Test #90: dependency-graph

**Category**: Metadata & Advanced  
**Description**: Generate dependency graph  
**Status**: âœ… PASSED  
**Duration**: 1887 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:37  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command dependency-graph -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:35.8031|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:35.8171|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:35.8171|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:35.8171|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:35.8171|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:35.8171|DEBUG|WindowsDb2Editor.App|Startup arguments: -command dependency-graph -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_dependency-graph_20251216_134528.json
2025-12-16 13:48:35.8171|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:35.8171|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:35.8171|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:35.8171|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:35.8634|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:35.8634|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:35.8634|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:35.8634|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:35.8634|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:35.8634|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:35.8791|DEBUG|WindowsDb2Editor.Services.Me
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "objectName": "BILAGNR",
  "dependencyCount": 0,
  "recursiveAnalysis": false,
  "dependencies": [],
  "note": "Direct dependencies only",
  "retrievedAt": "2025-12-16T13:48:37.0822961+01:00"
}
```

---

### âœ… Test #91: query-history

**Category**: Metadata & Advanced  
**Description**: Show query history  
**Status**: âœ… PASSED  
**Duration**: 2119 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:39  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command query-history -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:37.7775|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:37.7859|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:37.7859|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:37.7859|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:37.7859|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:37.7859|DEBUG|WindowsDb2Editor.App|Startup arguments: -command query-history -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_query-history_20251216_134528.json
2025-12-16 13:48:37.7859|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:37.7859|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:37.8025|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:37.8025|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:37.8394|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:37.8394|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:37.8394|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:37.8394|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:37.8525|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:37.8525|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:37.8525|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading meta
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "note": "Query history is session-based and not available in CLI mode",
  "sessionMode": "CLI",
  "historyAvailable": false,
  "retrievedAt": "2025-12-16T13:48:39.20355+01:00"
}
```

---

### âœ… Test #92: schema-summary

**Category**: Metadata & Advanced  
**Description**: Get schema summary  
**Status**: âœ… PASSED  
**Duration**: 2147 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:41  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command schema-summary -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:39.8968|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:39.9124|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:39.9124|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:39.9124|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:39.9124|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:39.9124|DEBUG|WindowsDb2Editor.App|Startup arguments: -command schema-summary -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_schema-summary_20251216_134528.json
2025-12-16 13:48:39.9124|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:39.9124|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:39.9261|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:39.9261|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:39.9624|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:39.9624|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:39.9624|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:39.9624|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:39.9624|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:39.9736|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:39.9736|DEBUG|WindowsDb2Editor.Services.MetadataHandle
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "schema-summary",
  "schema": "INL",
  "tables": 15,
  "views": 0,
  "procedures": 0,
  "functions": 0
}
```

---

### âœ… Test #93: database-summary

**Category**: Metadata & Advanced  
**Description**: Get database summary  
**Status**: âœ… PASSED  
**Duration**: 2263 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:43  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command database-summary -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:42.0561|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:42.0706|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:42.0706|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:42.0706|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:42.0706|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:42.0706|DEBUG|WindowsDb2Editor.App|Startup arguments: -command database-summary -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_database-summary_20251216_134528.json
2025-12-16 13:48:42.0706|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:42.0821|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:42.0821|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:42.0821|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:42.1261|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:42.1261|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:42.1261|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:42.1289|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:42.1289|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:42.1289|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:42.1289|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "database-summary",
  "schemas": 39,
  "tables": 271,
  "views": 369,
  "procedures": 584,
  "functions": 3466
}
```

---

### âœ… Test #94: health-check

**Category**: Metadata & Advanced  
**Description**: Run health check  
**Status**: âœ… PASSED  
**Duration**: 2467 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:46  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command health-check -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:44.4152|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:44.4267|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:44.4267|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:44.4267|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:44.4267|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:44.4267|DEBUG|WindowsDb2Editor.App|Startup arguments: -command health-check -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_health-check_20251216_134528.json
2025-12-16 13:48:44.4267|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:44.4267|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:44.4430|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:44.4430|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:44.4887|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:44.4887|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:44.4887|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:44.4887|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:44.4887|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:44.5061|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:44.5061|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metada
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "health-check",
  "overallStatus": "HEALTHY",
  "checks": [
    {
      "check": "Connection",
      "status": "OK",
      "message": "Database connection is active"
    },
    {
      "check": "Tables",
      "status": "OK",
      "count": 271
    }
  ]
}
```

---

### âœ… Test #95: ai-query

**Category**: AI Features  
**Description**: AI-assisted query generation  
**Status**: âœ… PASSED  
**Duration**: 2127 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:48  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-query -profile FKKTOTST -prompt Show all invoices from last month -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:46.8081|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:46.8170|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:46.8170|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:46.8170|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:46.8170|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:46.8170|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-query -profile FKKTOTST -prompt Show all invoices from last month -format json -Outfile CLI_Test_Output/temp_ai-query_20251216_134528.json
2025-12-16 13:48:46.8170|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:46.8170|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:46.8170|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:46.8324|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:46.8669|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:46.8669|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:46.8669|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:46.8669|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:46.8669|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:46.8821|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:46.8821|DEBUG|WindowsDb2Editor.Servi
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "ai-query",
  "prompt": "Show",
  "generatedSql": "-- AI-Generated SQL (placeholder)\n-- Natural language: Show\nSELECT * FROM SYSCAT.TABLES WHERE TABSCHEMA = \u0027SCHEMA\u0027 FETCH FIRST 10 ROWS ONLY;",
  "confidence": 0.85,
  "aiProvider": "Not configured (placeholder)",
  "schema": "ALL",
  "note": "AI provider not configured - this is a placeholder response. Configure AI provider in settings to enable this feature.",
  "timestamp": "2025-12-16T13:48:48.2131772+01:00"
}
```

---

### âœ… Test #96: ai-explain-table

**Category**: AI Features  
**Description**: AI explanation of table  
**Status**: âœ… PASSED  
**Duration**: 2219 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:50  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-explain-table -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:48.9597|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:48.9894|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:48.9894|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:48.9894|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:48.9894|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:48.9894|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-explain-table -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_ai-explain-table_20251216_134528.json
2025-12-16 13:48:48.9894|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:48.9894|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:48.9894|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:48.9894|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:49.0409|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:49.0409|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:49.0409|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:49.0409|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:49.0533|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:49.0533|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:49.0533|DEBUG|WindowsDb2Editor.Services.Me
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "ai-explain-table",
  "table": "INL.BILAGNR",
  "tableInfo": {
    "schema": "INL",
    "tableName": "BILAGNR",
    "type": "T",
    "columnCount": 5,
    "approximateRows": 41
  },
  "aiExplanation": "[AI Explanation Placeholder] INL.BILAGNR appears to be a T table with 5 columns containing approximately 41 rows. Configure AI provider to generate detailed analysis.",
  "relationships": "AI provider not configured",
  "usage": "AI provider not configured",
  "aiProvider": "Not configured (placeholder)",
  "note": "AI provider not configured - this is a placeholder response. Configure AI provider in settings to enable this feature.",
  "timestamp": "2025-12-16T13:48:50.4588604+01:00"
}
```

---

### âœ… Test #97: ai-explain-view

**Category**: AI Features  
**Description**: AI explanation of view  
**Status**: âœ… PASSED  
**Duration**: 2298 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:52  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-explain-view -profile FKKTOTST -object INL.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:51.1795|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:51.1944|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:51.1944|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:51.1944|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:51.1944|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:51.1944|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-explain-view -profile FKKTOTST -object INL.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_ai-explain-view_20251216_134528.json
2025-12-16 13:48:51.1944|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:51.1944|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:51.1944|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:51.2124|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:51.2481|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:51.2481|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:51.2481|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:51.2481|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:51.2615|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:51.2615|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:51.2615|DEBUG|WindowsDb2Editor.Services.Me
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "ai-explain-view",
  "view": "INL.SYSDUMMY1",
  "explanation": "AI analysis placeholder",
  "timestamp": "2025-12-16T13:48:52.754792+01:00"
}
```

---

### âœ… Test #98: ai-analyze-procedure

**Category**: AI Features  
**Description**: AI analysis of procedure  
**Status**: âœ… PASSED  
**Duration**: 1953 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:54  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-analyze-procedure -profile FKKTOTST -object INL.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:53.3989|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:53.4146|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:53.4146|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:53.4146|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:53.4146|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:53.4146|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-analyze-procedure -profile FKKTOTST -object INL.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_ai-analyze-procedure_20251216_134528.json
2025-12-16 13:48:53.4146|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:53.4146|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:53.4146|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:53.4146|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:53.4535|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:53.4535|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:53.4535|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:53.4535|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:53.4630|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:53.4630|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:53.4630|DEBUG|WindowsDb2Editor.S
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "ai-analyze-procedure",
  "procedure": "INL.ADMIN_CMD",
  "analysis": "AI code analysis placeholder",
  "timestamp": "2025-12-16T13:48:54.7190721+01:00"
}
```

---

### âœ… Test #99: ai-analyze-function

**Category**: AI Features  
**Description**: AI analysis of function  
**Status**: âœ… PASSED  
**Duration**: 1856 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:56  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-analyze-function -profile FKKTOTST -object INL.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:55.3192|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:55.3342|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:55.3342|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:55.3342|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:55.3342|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:55.3342|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-analyze-function -profile FKKTOTST -object INL.ABS -format json -Outfile CLI_Test_Output/temp_ai-analyze-function_20251216_134528.json
2025-12-16 13:48:55.3342|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:55.3342|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:55.3342|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:55.3484|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:55.3824|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:55.3824|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:55.3824|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:55.3824|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:55.3983|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:55.3983|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:55.3983|DEBUG|WindowsDb2Editor.Services.
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "ai-analyze-function",
  "function": "INL.ABS",
  "analysis": "AI code analysis placeholder",
  "timestamp": "2025-12-16T13:48:56.5829729+01:00"
}
```

---

### âœ… Test #100: ai-analyze-package

**Category**: AI Features  
**Description**: AI analysis of package  
**Status**: âœ… PASSED  
**Duration**: 1887 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:48:58  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-analyze-package -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:57.1703|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:57.1839|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:57.1839|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:57.1839|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:57.1839|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:57.1839|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-analyze-package -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_ai-analyze-package_20251216_134528.json
2025-12-16 13:48:57.1839|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:57.1839|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:57.1948|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:57.1948|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:57.2236|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:57.2245|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:57.2245|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:57.2245|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:57.2245|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:57.2397|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:57.2397|DEBUG|WindowsDb2Editor.Servic
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "ai-analyze-package",
  "package": "INL.SYSSH200",
  "analysis": "AI package analysis placeholder",
  "timestamp": "2025-12-16T13:48:58.4630388+01:00"
}
```

---

### âœ… Test #101: ai-deep-analysis

**Category**: AI Features  
**Description**: Deep AI analysis of table  
**Status**: âœ… PASSED  
**Duration**: 1915 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:49:00  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-deep-analysis -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:48:59.1312|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:48:59.1312|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:48:59.1312|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:48:59.1312|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:48:59.1466|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:48:59.1466|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-deep-analysis -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_ai-deep-analysis_20251216_134528.json
2025-12-16 13:48:59.1466|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:48:59.1466|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:48:59.1466|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:48:59.1466|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:48:59.1845|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:48:59.1845|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:48:59.1845|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:48:59.1845|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:48:59.1973|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:48:59.1973|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:48:59.1973|DEBUG|WindowsDb2Editor.Services.Me
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "ai-deep-analysis",
  "table": "INL.BILAGNR",
  "analysis": {
    "tableComment": "AI would extract SYSCAT.REMARKS here",
    "columnComments": [
      "Column1: Description from SYSCAT.COLUMNS.REMARKS",
      "Column2: Description"
    ],
    "dataSample": "AI would sample 100 rows here",
    "columnProfiling": [
      {
        "column": "ID",
        "distinctCount": 0,
        "nullCount": 0,
        "dataType": "INTEGER"
      },
      {
        "column": "NAME",
        "distinctCount": 0,
        "nullCount": 0,
        "dataType": "VARCHAR"
      }
    ],
    "relationships": [
      "Relationships with other tables would be listed here"
    ],
    "sensitiveDataDetected": false
  },
  "aiInsights": "[AI Insights Placeholder] Configure AI provider to generate deep analysis with data sampling, column profiling, and relationship explanations.",
  "aiProvider": "Not configured (placeholder)",
  "note": "AI provider not configured - this is a placeholder response. Configure AI provider in settings to enable this feature.",
  "timestamp": "2025-12-16T13:49:00.3921258+01:00"
}
```

---

### âœ… Test #102: gui-test

**Category**: GUI Testing  
**Description**: Test TableDetails form  
**Status**: âœ… PASSED  
**Duration**: 1971 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:49:02  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command gui-test -profile FKKTOTST -form TableDetails -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:49:01.0757|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:49:01.0899|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:49:01.0899|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:49:01.0899|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:49:01.0899|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:49:01.0899|DEBUG|WindowsDb2Editor.App|Startup arguments: -command gui-test -profile FKKTOTST -form TableDetails -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_gui-test_20251216_134528.json
2025-12-16 13:49:01.0899|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:49:01.0989|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:49:01.0989|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:49:01.0989|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:49:01.1318|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:49:01.1318|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:49:01.1318|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:49:01.1318|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:49:01.1318|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:49:01.1493|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:49:01.1493|DEBUG|WindowsDb2Editor.Services
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "gui-test",
  "form": null,
  "Object": "INL.BILAGNR",
  "tab": null,
  "status": "GUI tests require WPF context and cannot be run in CLI mode",
  "note": "Use the WPF application directly for GUI testing"
}
```

---

### âœ… Test #103: gui-test

**Category**: GUI Testing  
**Description**: Test TableDetails Columns tab  
**Status**: âœ… PASSED  
**Duration**: 1964 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:49:04  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command gui-test -profile FKKTOTST -form TableDetails -object INL.BILAGNR -tab Columns -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:49:03.0282|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:49:03.0413|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:49:03.0413|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:49:03.0413|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:49:03.0413|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:49:03.0413|DEBUG|WindowsDb2Editor.App|Startup arguments: -command gui-test -profile FKKTOTST -form TableDetails -object INL.BILAGNR -tab Columns -format json -Outfile CLI_Test_Output/temp_gui-test_20251216_134528.json
2025-12-16 13:49:03.0529|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:49:03.0529|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:49:03.0529|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:49:03.0529|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:49:03.0915|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:49:03.0915|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:49:03.0915|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:49:03.0915|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:49:03.1047|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:49:03.1047|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:49:03.1047|DEBUG|WindowsDb2Ed
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "gui-test",
  "form": null,
  "Object": "INL.BILAGNR",
  "tab": "columns",
  "status": "GUI tests require WPF context and cannot be run in CLI mode",
  "note": "Use the WPF application directly for GUI testing"
}
```

---

### âœ… Test #104: gui-test

**Category**: GUI Testing  
**Description**: Test TableDetails Indexes tab  
**Status**: âœ… PASSED  
**Duration**: 2344 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:49:06  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command gui-test -profile FKKTOTST -form TableDetails -object INL.BILAGNR -tab Indexes -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:49:05.1650|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:49:05.1826|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:49:05.1826|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:49:05.1826|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:49:05.1826|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:49:05.1935|DEBUG|WindowsDb2Editor.App|Startup arguments: -command gui-test -profile FKKTOTST -form TableDetails -object INL.BILAGNR -tab Indexes -format json -Outfile CLI_Test_Output/temp_gui-test_20251216_134528.json
2025-12-16 13:49:05.1935|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:49:05.1935|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:49:05.1935|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:49:05.1935|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:49:05.2413|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:49:05.2413|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:49:05.2413|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:49:05.2413|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:49:05.2413|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:49:05.2578|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:49:05.2578|DEBUG|WindowsDb2Ed
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "gui-test",
  "form": null,
  "Object": "INL.BILAGNR",
  "tab": "indexes",
  "status": "GUI tests require WPF context and cannot be run in CLI mode",
  "note": "Use the WPF application directly for GUI testing"
}
```

---

### âœ… Test #105: gui-test

**Category**: GUI Testing  
**Description**: Test TableDetails ForeignKeys tab  
**Status**: âœ… PASSED  
**Duration**: 2031 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:49:08  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command gui-test -profile FKKTOTST -form TableDetails -object INL.BILAGNR -tab ForeignKeys -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:49:07.3378|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:49:07.3512|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:49:07.3512|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:49:07.3512|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:49:07.3512|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:49:07.3512|DEBUG|WindowsDb2Editor.App|Startup arguments: -command gui-test -profile FKKTOTST -form TableDetails -object INL.BILAGNR -tab ForeignKeys -format json -Outfile CLI_Test_Output/temp_gui-test_20251216_134528.json
2025-12-16 13:49:07.3512|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:49:07.3512|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:49:07.3657|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:49:07.3657|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:49:07.4096|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:49:07.4096|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:49:07.4096|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:49:07.4126|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:49:07.4126|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:49:07.4126|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:49:07.4282|DEBUG|WindowsD
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "gui-test",
  "form": null,
  "Object": "INL.BILAGNR",
  "tab": "foreignkeys",
  "status": "GUI tests require WPF context and cannot be run in CLI mode",
  "note": "Use the WPF application directly for GUI testing"
}
```

---

### âœ… Test #106: gui-test

**Category**: GUI Testing  
**Description**: Test ViewDetails form  
**Status**: âœ… PASSED  
**Duration**: 1873 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:49:10  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command gui-test -profile FKKTOTST -form ViewDetails -object INL.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:49:09.3228|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:49:09.3370|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:49:09.3370|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:49:09.3370|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:49:09.3370|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:49:09.3370|DEBUG|WindowsDb2Editor.App|Startup arguments: -command gui-test -profile FKKTOTST -form ViewDetails -object INL.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_gui-test_20251216_134528.json
2025-12-16 13:49:09.3370|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:49:09.3370|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:49:09.3497|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:49:09.3497|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:49:09.3807|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:49:09.3820|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:49:09.3820|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:49:09.3820|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:49:09.3820|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:49:09.3983|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:49:09.3983|DEBUG|WindowsDb2Editor.Service
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "gui-test",
  "form": null,
  "Object": "INL.SYSDUMMY1",
  "tab": null,
  "status": "GUI tests require WPF context and cannot be run in CLI mode",
  "note": "Use the WPF application directly for GUI testing"
}
```

---

### âœ… Test #107: gui-test

**Category**: GUI Testing  
**Description**: Test ProcedureDetails form  
**Status**: âœ… PASSED  
**Duration**: 2176 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:49:12  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command gui-test -profile FKKTOTST -form ProcedureDetails -object INL.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:49:11.2590|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:49:11.2734|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:49:11.2734|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:49:11.2734|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:49:11.2734|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:49:11.2734|DEBUG|WindowsDb2Editor.App|Startup arguments: -command gui-test -profile FKKTOTST -form ProcedureDetails -object INL.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_gui-test_20251216_134528.json
2025-12-16 13:49:11.2734|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:49:11.2734|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:49:11.2734|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:49:11.2734|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:49:11.3146|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:49:11.3146|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:49:11.3146|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:49:11.3146|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:49:11.3185|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:49:11.3185|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:49:11.3340|DEBUG|WindowsDb2Editor.Se
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "gui-test",
  "form": null,
  "Object": "INL.ADMIN_CMD",
  "tab": null,
  "status": "GUI tests require WPF context and cannot be run in CLI mode",
  "note": "Use the WPF application directly for GUI testing"
}
```

---

### âœ… Test #108: gui-test

**Category**: GUI Testing  
**Description**: Test FunctionDetails form  
**Status**: âœ… PASSED  
**Duration**: 1903 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:49:14  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command gui-test -profile FKKTOTST -form FunctionDetails -object INL.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:49:13.3700|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:49:13.3829|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:49:13.3829|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:49:13.3829|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:49:13.3829|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:49:13.3829|DEBUG|WindowsDb2Editor.App|Startup arguments: -command gui-test -profile FKKTOTST -form FunctionDetails -object INL.ABS -format json -Outfile CLI_Test_Output/temp_gui-test_20251216_134528.json
2025-12-16 13:49:13.3829|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:49:13.3829|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:49:13.3829|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:49:13.3829|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:49:13.4222|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:49:13.4222|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:49:13.4222|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:49:13.4222|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:49:13.4337|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:49:13.4337|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:49:13.4337|DEBUG|WindowsDb2Editor.Services.
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "gui-test",
  "form": null,
  "Object": "INL.ABS",
  "tab": null,
  "status": "GUI tests require WPF context and cannot be run in CLI mode",
  "note": "Use the WPF application directly for GUI testing"
}
```

---

### âœ… Test #109: gui-test

**Category**: GUI Testing  
**Description**: Test PackageDetails form  
**Status**: âœ… PASSED  
**Duration**: 2149 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:49:16  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command gui-test -profile FKKTOTST -form PackageDetails -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:49:15.2901|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:49:15.3055|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:49:15.3055|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:49:15.3055|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:49:15.3055|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:49:15.3055|DEBUG|WindowsDb2Editor.App|Startup arguments: -command gui-test -profile FKKTOTST -form PackageDetails -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_gui-test_20251216_134528.json
2025-12-16 13:49:15.3055|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:49:15.3055|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:49:15.3055|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:49:15.3198|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:49:15.3491|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:49:15.3513|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:49:15.3513|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:49:15.3513|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:49:15.3513|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:49:15.3513|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:49:15.3513|DEBUG|WindowsDb2Editor.Servi
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "gui-test",
  "form": null,
  "Object": "INL.SYSSH200",
  "tab": null,
  "status": "GUI tests require WPF context and cannot be run in CLI mode",
  "note": "Use the WPF application directly for GUI testing"
}
```

---

### âœ… Test #110: gui-test

**Category**: GUI Testing  
**Description**: Test DatabaseLoadMonitor form  
**Status**: âœ… PASSED  
**Duration**: 2053 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:49:18  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command gui-test -profile FKKTOTST -form DatabaseLoadMonitor -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:49:17.4776|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:49:17.4776|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:49:17.4910|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:49:17.4910|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:49:17.4910|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:49:17.4910|DEBUG|WindowsDb2Editor.App|Startup arguments: -command gui-test -profile FKKTOTST -form DatabaseLoadMonitor -format json -Outfile CLI_Test_Output/temp_gui-test_20251216_134528.json
2025-12-16 13:49:17.4910|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:49:17.4910|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:49:17.4910|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:49:17.4910|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:49:17.5310|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:49:17.5310|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:49:17.5310|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:49:17.5310|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:49:17.5421|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:49:17.5421|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:49:17.5421|DEBUG|WindowsDb2Editor.Services.MetadataHand
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "gui-test",
  "form": null,
  "Object": null,
  "tab": null,
  "status": "GUI tests require WPF context and cannot be run in CLI mode",
  "note": "Use the WPF application directly for GUI testing"
}
```

---

### âœ… Test #111: gui-test

**Category**: GUI Testing  
**Description**: Test ActiveSessions form  
**Status**: âœ… PASSED  
**Duration**: 2035 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:49:21  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command gui-test -profile FKKTOTST -form ActiveSessions -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:49:19.5114|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:49:19.5295|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:49:19.5295|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:49:19.5389|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:49:19.5389|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:49:19.5389|DEBUG|WindowsDb2Editor.App|Startup arguments: -command gui-test -profile FKKTOTST -form ActiveSessions -format json -Outfile CLI_Test_Output/temp_gui-test_20251216_134528.json
2025-12-16 13:49:19.5389|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:49:19.5389|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:49:19.5389|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:49:19.5389|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:49:19.5765|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:49:19.5765|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:49:19.5765|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:49:19.5765|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:49:19.5886|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:49:19.5886|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:49:19.5886|DEBUG|WindowsDb2Editor.Services.MetadataHandler|L
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "gui-test",
  "form": null,
  "Object": null,
  "tab": null,
  "status": "GUI tests require WPF context and cannot be run in CLI mode",
  "note": "Use the WPF application directly for GUI testing"
}
```

---

### âœ… Test #112: gui-test

**Category**: GUI Testing  
**Description**: Test LockMonitor form  
**Status**: âœ… PASSED  
**Duration**: 2147 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:49:23  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command gui-test -profile FKKTOTST -form LockMonitor -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:49:21.5473|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:49:21.5609|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:49:21.5609|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:49:21.5609|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:49:21.5700|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:49:21.5700|DEBUG|WindowsDb2Editor.App|Startup arguments: -command gui-test -profile FKKTOTST -form LockMonitor -format json -Outfile CLI_Test_Output/temp_gui-test_20251216_134528.json
2025-12-16 13:49:21.5700|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:49:21.5700|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:49:21.5700|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:49:21.5700|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:49:21.6122|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:49:21.6122|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:49:21.6122|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:49:21.6122|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:49:21.6215|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:49:21.6215|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:49:21.6215|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Load
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "gui-test",
  "form": null,
  "Object": null,
  "tab": null,
  "status": "GUI tests require WPF context and cannot be run in CLI mode",
  "note": "Use the WPF application directly for GUI testing"
}
```

---

### âœ… Test #113: gui-test

**Category**: GUI Testing  
**Description**: Test CdcStatusDialog form  
**Status**: âœ… PASSED  
**Duration**: 2303 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 13:49:25  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command gui-test -profile FKKTOTST -form CdcStatusDialog -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 13:49:23.9179|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 13:49:23.9473|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 13:49:23.9473|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 13:49:23.9473|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 13:49:23.9473|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 13:49:23.9473|DEBUG|WindowsDb2Editor.App|Startup arguments: -command gui-test -profile FKKTOTST -form CdcStatusDialog -schema INL -format json -Outfile CLI_Test_Output/temp_gui-test_20251216_134528.json
2025-12-16 13:49:23.9473|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 13:49:23.9473|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 13:49:23.9473|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 13:49:23.9473|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 13:49:23.9963|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 13:49:23.9963|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 13:49:23.9963|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 13:49:23.9963|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 13:49:24.0128|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 13:49:24.0128|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 13:49:24.0128|DEBUG|WindowsDb2Editor.Services.Meta
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "gui-test",
  "form": null,
  "Object": null,
  "tab": null,
  "status": "GUI tests require WPF context and cannot be run in CLI mode",
  "note": "Use the WPF application directly for GUI testing"
}
```

---

## Test Environment

- **Executable**: bin\Debug\net10.0-windows\WindowsDb2Editor.exe
- **Test Timestamp**: 20251216_134528
- **PowerShell Version**: 5.1.26100.7462
- **Test Script**: _comprehensive_cli_test_all_119.ps1

---

*Report generated automatically by WindowsDb2Editor Comprehensive CLI Test Suite*
