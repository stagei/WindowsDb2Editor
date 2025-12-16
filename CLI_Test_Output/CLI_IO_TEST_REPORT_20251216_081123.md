# CLI Test Report with Input/Output Capture

**Generated**: 2025-12-16 08:14:42  
**Connection Profile**: FKKTOTST  
**Total Tests**: 101  
**âœ… Passed**: 101 (100%)  
**âŒ Failed**: 0 (0%)  

---

## Executive Summary

| Metric | Value |
|--------|-------|
| Total Commands Tested | 101 |
| Passed | 101 |
| Failed | 0 |
| Success Rate | 100% |
| Test Duration | ~0 seconds |

---

## Results by Category

### âœ…  (101/101 passed)

| # | Command | Description | Status | Duration | Exit Code |
|---|---------|-------------|--------|----------|-----------|| 1 | `connection-test` | Test database connection | âœ… PASS | 2485ms | 0 |
| 2 | `connection-stats` | Get connection statistics | âœ… PASS | 2543ms | 0 |
| 3 | `help-all` | Show all available commands | âœ… PASS | 1230ms | 0 |
| 4 | `cli-version` | Show CLI version | âœ… PASS | 923ms | 0 |
| 5 | `db-config` | Get database configuration | âœ… PASS | 2461ms | 0 |
| 6 | `connection-history` | Show connection history | âœ… PASS | 2090ms | 0 |
| 7 | `list-schemas` | List all schemas | âœ… PASS | 2228ms | 0 |
| 8 | `list-tables` | List tables in schema | âœ… PASS | 2202ms | 0 |
| 9 | `list-views` | List views in schema | âœ… PASS | 2114ms | 0 |
| 10 | `list-procedures` | List procedures in schema | âœ… PASS | 2488ms | 0 |
| 11 | `list-functions` | List functions in schema | âœ… PASS | 2035ms | 0 |
| 12 | `list-packages` | List packages in schema | âœ… PASS | 2381ms | 0 |
| 13 | `list-tablespaces` | List all tablespaces | âœ… PASS | 2079ms | 0 |
| 14 | `list-all-indexes` | List all indexes in schema | âœ… PASS | 2225ms | 0 |
| 15 | `list-constraints` | List constraints in schema | âœ… PASS | 2272ms | 0 |
| 16 | `list-sequences` | List sequences in schema | âœ… PASS | 1960ms | 0 |
| 17 | `table-properties` | Get table properties | âœ… PASS | 2332ms | 0 |
| 18 | `table-columns` | List table columns | âœ… PASS | 2126ms | 0 |
| 19 | `table-indexes` | List table indexes | âœ… PASS | 2164ms | 0 |
| 20 | `table-foreignkeys` | List table foreign keys | âœ… PASS | 2230ms | 0 |
| 21 | `table-ddl` | Generate table DDL | âœ… PASS | 2346ms | 0 |
| 22 | `table-statistics` | Get table statistics | âœ… PASS | 2232ms | 0 |
| 23 | `table-relationships` | Show table relationships | âœ… PASS | 2281ms | 0 |
| 24 | `table-sample-data` | Get sample data from table | âœ… PASS | 2424ms | 0 |
| 25 | `table-size` | Get table size | âœ… PASS | 2364ms | 0 |
| 26 | `table-grants` | List table grants | âœ… PASS | 2231ms | 0 |
| 27 | `table-dependencies` | Show table dependencies | âœ… PASS | 1894ms | 0 |
| 28 | `table-incoming-fk` | List incoming foreign keys | âœ… PASS | 2020ms | 0 |
| 29 | `table-used-by-packages` | List packages using this table | âœ… PASS | 1941ms | 0 |
| 30 | `table-used-by-views` | List views using this table | âœ… PASS | 2052ms | 0 |
| 31 | `table-used-by-routines` | List routines using this table | âœ… PASS | 2127ms | 0 |
| 32 | `view-properties` | Get view properties | âœ… PASS | 2231ms | 0 |
| 33 | `view-definition` | Get view definition SQL | âœ… PASS | 2225ms | 0 |
| 34 | `view-columns` | List view columns | âœ… PASS | 2575ms | 0 |
| 35 | `view-dependencies` | Show view dependencies | âœ… PASS | 2406ms | 0 |
| 36 | `view-sample-data` | Get sample data from view | âœ… PASS | 2285ms | 0 |
| 37 | `view-used-by-packages` | List packages using this view | âœ… PASS | 1931ms | 0 |
| 38 | `view-used-by-views` | List views using this view | âœ… PASS | 1880ms | 0 |
| 39 | `procedure-properties` | Get procedure properties | âœ… PASS | 2172ms | 0 |
| 40 | `procedure-source` | Get procedure source code | âœ… PASS | 2045ms | 0 |
| 41 | `procedure-parameters` | List procedure parameters | âœ… PASS | 1843ms | 0 |
| 42 | `procedure-dependencies` | Show procedure dependencies | âœ… PASS | 1965ms | 0 |
| 43 | `procedure-usage` | Show procedure usage | âœ… PASS | 1827ms | 0 |
| 44 | `procedure-grants` | List procedure grants | âœ… PASS | 1911ms | 0 |
| 45 | `function-properties` | Get function properties | âœ… PASS | 1966ms | 0 |
| 46 | `function-source` | Get function source code | âœ… PASS | 1955ms | 0 |
| 47 | `function-parameters` | List function parameters | âœ… PASS | 1846ms | 0 |
| 48 | `function-dependencies` | Show function dependencies | âœ… PASS | 1914ms | 0 |
| 49 | `function-usage` | Show function usage | âœ… PASS | 1778ms | 0 |
| 50 | `function-grants` | List function grants | âœ… PASS | 2253ms | 0 |
| 51 | `package-properties` | Get package properties | âœ… PASS | 1950ms | 0 |
| 52 | `package-statements` | List package statements | âœ… PASS | 1946ms | 0 |
| 53 | `package-dependencies` | Show package dependencies | âœ… PASS | 2016ms | 0 |
| 54 | `package-statistics` | Get package statistics | âœ… PASS | 1975ms | 0 |
| 55 | `package-list-tables` | List tables used by package | âœ… PASS | 1948ms | 0 |
| 56 | `package-list-views` | List views used by package | âœ… PASS | 2073ms | 0 |
| 57 | `package-list-procedures` | List procedures used by package | âœ… PASS | 2303ms | 0 |
| 58 | `package-list-functions` | List functions used by package | âœ… PASS | 1971ms | 0 |
| 59 | `user-properties` | Get user properties | âœ… PASS | 1679ms | 0 |
| 60 | `user-privileges` | List user privileges | âœ… PASS | 1929ms | 0 |
| 61 | `user-tables` | List user's tables | âœ… PASS | 2114ms | 0 |
| 62 | `user-schemas` | List user's schemas | âœ… PASS | 2045ms | 0 |
| 63 | `user-connections` | List user connections | âœ… PASS | 2018ms | 0 |
| 64 | `db-load` | Get database load | âœ… PASS | 1923ms | 0 |
| 65 | `db-locks` | Show database locks | âœ… PASS | 2134ms | 0 |
| 66 | `db-sessions` | List database sessions | âœ… PASS | 1574ms | 0 |
| 67 | `db-size` | Get database size | âœ… PASS | 1623ms | 0 |
| 68 | `schema-size` | Get schema size | âœ… PASS | 1630ms | 0 |
| 69 | `db-version` | Get database version | âœ… PASS | 1692ms | 0 |
| 70 | `db-parameters` | List database parameters | âœ… PASS | 1724ms | 0 |
| 71 | `db-registry` | Show database registry | âœ… PASS | 2115ms | 0 |
| 72 | `active-queries` | List active queries | âœ… PASS | 1908ms | 0 |
| 73 | `bufferpool-stats` | Get buffer pool statistics | âœ… PASS | 1769ms | 0 |
| 74 | `tablespace-usage` | Show tablespace usage | âœ… PASS | 1989ms | 0 |
| 75 | `cdc-status` | Get CDC status for schema | âœ… PASS | 1743ms | 0 |
| 76 | `cdc-status-full` | Get full CDC status | âœ… PASS | 1734ms | 0 |
| 77 | `cdc-configuration` | Get CDC configuration for table | âœ… PASS | 1660ms | 0 |
| 78 | `cdc-changes` | List CDC changes | âœ… PASS | 1731ms | 0 |
| 79 | `cdc-enable` | Enable CDC for table | âœ… PASS | 1712ms | 0 |
| 80 | `cdc-disable` | Disable CDC for table | âœ… PASS | 1736ms | 0 |
| 81 | `cdc-history` | Show CDC history | âœ… PASS | 1748ms | 0 |
| 82 | `db-compare` | Compare two schemas | âœ… PASS | 2161ms | 0 |
| 83 | `db-compare-source-only` | Show objects only in source | âœ… PASS | 1770ms | 0 |
| 84 | `db-compare-target-only` | Show objects only in target | âœ… PASS | 1758ms | 0 |
| 85 | `db-compare-different` | Show different objects | âœ… PASS | 2013ms | 0 |
| 86 | `db-compare-ddl` | Generate comparison DDL | âœ… PASS | 1651ms | 0 |
| 87 | `object-metadata` | Get object metadata | âœ… PASS | 1755ms | 0 |
| 88 | `object-search` | Search for objects | âœ… PASS | 1783ms | 0 |
| 89 | `column-search` | Search for columns | âœ… PASS | 1646ms | 0 |
| 90 | `dependency-graph` | Generate dependency graph | âœ… PASS | 1664ms | 0 |
| 91 | `query-history` | Show query history | âœ… PASS | 1829ms | 0 |
| 92 | `schema-summary` | Get schema summary | âœ… PASS | 1723ms | 0 |
| 93 | `database-summary` | Get database summary | âœ… PASS | 1801ms | 0 |
| 94 | `health-check` | Run health check | âœ… PASS | 1614ms | 0 |
| 95 | `ai-query` | AI-assisted query generation | âœ… PASS | 1595ms | 0 |
| 96 | `ai-explain-table` | AI explanation of table | âœ… PASS | 1622ms | 0 |
| 97 | `ai-explain-view` | AI explanation of view | âœ… PASS | 1725ms | 0 |
| 98 | `ai-analyze-procedure` | AI analysis of procedure | âœ… PASS | 1727ms | 0 |
| 99 | `ai-analyze-function` | AI analysis of function | âœ… PASS | 1703ms | 0 |
| 100 | `ai-analyze-package` | AI analysis of package | âœ… PASS | 1621ms | 0 |
| 101 | `ai-deep-analysis` | Deep AI analysis of table | âœ… PASS | 1550ms | 0 |

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
**Duration**: 2485 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:11:26  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command connection-test -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:11:24.5331|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:11:24.5519|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:11:24.5628|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:11:24.5628|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:11:24.5628|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:11:24.5628|DEBUG|WindowsDb2Editor.App|Startup arguments: -command connection-test -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_connection-test_20251216_081123.json
2025-12-16 08:11:24.5628|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:11:24.5628|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:11:24.5628|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:11:24.5628|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:11:24.6181|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:11:24.6181|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:11:24.6181|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:11:24.6181|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:11:24.6342|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:11:24.6416|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:11:24.6416|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading 
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "connection-test",
  "profileName": "FKKTOTST",
  "testResult": "SUCCESS",
  "message": "Connection opened successfully",
  "retrievedAt": "2025-12-16T08:11:26.0127346+01:00"
}
```

---

### âœ… Test #2: connection-stats

**Category**: Connection & Info  
**Description**: Get connection statistics  
**Status**: âœ… PASSED  
**Duration**: 2543 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:11:28  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command connection-stats -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:11:26.8382|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:11:26.8512|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:11:26.8512|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:11:26.8512|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:11:26.8602|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:11:26.8602|DEBUG|WindowsDb2Editor.App|Startup arguments: -command connection-stats -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_connection-stats_20251216_081123.json
2025-12-16 08:11:26.8602|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:11:26.8602|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:11:26.8602|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:11:26.8602|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:11:26.9085|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:11:26.9085|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:11:26.9108|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:11:26.9108|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:11:26.9266|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:11:26.9266|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:11:26.9266|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "totalProfiles": 2,
  "note": "Connection stats based on saved profiles",
  "retrievedAt": "2025-12-16T08:11:28.6006251+01:00"
}
```

---

### âœ… Test #3: help-all

**Category**: Connection & Info  
**Description**: Show all available commands  
**Status**: âœ… PASSED  
**Duration**: 1230 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:11:29  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command help-all -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:11:29.5228|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:11:29.5370|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:11:29.5370|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:11:29.5370|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:11:29.5370|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:11:29.5370|DEBUG|WindowsDb2Editor.App|Startup arguments: -command help-all -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_help-all_20251216_081123.json
2025-12-16 08:11:29.5370|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:11:29.5370|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:11:29.5628|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:11:29.5628|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:11:29.5973|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:11:29.5973|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:11:29.5973|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:11:29.5973|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:11:29.6114|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:11:29.6114|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:11:29.6114|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for D
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
**Duration**: 923 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:11:30  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cli-version -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:11:30.4901|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:11:30.5020|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:11:30.5020|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:11:30.5020|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:11:30.5020|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:11:30.5020|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cli-version -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_cli-version_20251216_081123.json
2025-12-16 08:11:30.5020|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:11:30.5020|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:11:30.5184|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:11:30.5184|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:11:30.5601|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:11:30.5601|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:11:30.5601|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:11:30.5601|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:11:30.5827|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:11:30.5827|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:11:30.5827|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata
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
  "retrievedAt": "2025-12-16T08:11:30.7541813+01:00"
}
```

---

### âœ… Test #5: db-config

**Category**: Connection & Info  
**Description**: Get database configuration  
**Status**: âœ… PASSED  
**Duration**: 2461 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:11:33  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-config -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:11:31.5369|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:11:31.5509|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:11:31.5509|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:11:31.5509|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:11:31.5509|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:11:31.5509|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-config -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-config_20251216_081123.json
2025-12-16 08:11:31.5509|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:11:31.5509|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:11:31.5509|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:11:31.5509|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:11:31.6011|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:11:31.6011|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:11:31.6011|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:11:31.6011|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:11:31.6202|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:11:31.6271|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:11:31.6271|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for
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
**Duration**: 2090 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:11:35  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command connection-history -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:11:33.8477|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:11:33.8662|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:11:33.8662|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:11:33.8662|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:11:33.8662|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:11:33.8662|DEBUG|WindowsDb2Editor.App|Startup arguments: -command connection-history -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_connection-history_20251216_081123.json
2025-12-16 08:11:33.8662|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:11:33.8662|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:11:33.8662|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:11:33.8662|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:11:33.9186|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:11:33.9203|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:11:33.9203|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:11:33.9226|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:11:33.9226|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:11:33.9226|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:11:33.9226|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Lo
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
**Duration**: 2228 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:11:37  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-schemas -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:11:36.0214|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:11:36.0332|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:11:36.0332|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:11:36.0332|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:11:36.0332|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:11:36.0332|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-schemas -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_list-schemas_20251216_081123.json
2025-12-16 08:11:36.0332|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:11:36.0332|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:11:36.0474|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:11:36.0474|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:11:36.0965|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:11:36.0965|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:11:36.0965|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:11:36.0965|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:11:36.1120|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:11:36.1120|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:11:36.1120|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metada
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
**Duration**: 2202 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:11:39  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-tables -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:11:38.2890|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:11:38.3058|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:11:38.3058|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:11:38.3139|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:11:38.3139|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:11:38.3139|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-tables -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-tables_20251216_081123.json
2025-12-16 08:11:38.3139|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:11:38.3139|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:11:38.3139|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:11:38.3139|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:11:38.3606|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:11:38.3606|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:11:38.3606|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:11:38.3648|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:11:38.3648|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:11:38.3648|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:11:38.3814|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Load
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "totalTables": 0,
  "limitApplied": 0,
  "tables": [],
  "retrievedAt": "2025-12-16T08:11:39.7729432+01:00"
}
```

---

### âœ… Test #9: list-views

**Category**: Schema Operations  
**Description**: List views in schema  
**Status**: âœ… PASSED  
**Duration**: 2114 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:11:41  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-views -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:11:40.4228|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:11:40.4436|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:11:40.4436|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:11:40.4436|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:11:40.4436|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:11:40.4540|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-views -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-views_20251216_081123.json
2025-12-16 08:11:40.4540|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:11:40.4540|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:11:40.4540|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:11:40.4540|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:11:40.4976|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:11:40.4976|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:11:40.5017|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:11:40.5017|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:11:40.5017|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:11:40.5204|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:11:40.5204|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "totalViews": 0,
  "limitApplied": 0,
  "views": [],
  "retrievedAt": "2025-12-16T08:11:41.9011561+01:00"
}
```

---

### âœ… Test #10: list-procedures

**Category**: Schema Operations  
**Description**: List procedures in schema  
**Status**: âœ… PASSED  
**Duration**: 2488 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:11:44  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-procedures -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:11:42.5544|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:11:42.5807|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:11:42.5807|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:11:42.5807|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:11:42.5807|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:11:42.5807|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-procedures -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-procedures_20251216_081123.json
2025-12-16 08:11:42.5807|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:11:42.5807|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:11:42.5807|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:11:42.5807|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:11:42.6352|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:11:42.6352|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:11:42.6352|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:11:42.6352|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:11:42.6513|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:11:42.6593|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:11:42.6593|DEBUG|WindowsDb2Editor.Services.MetadataHand
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "totalProcedures": 0,
  "limitApplied": 0,
  "procedures": [],
  "retrievedAt": "2025-12-16T08:11:44.4099036+01:00"
}
```

---

### âœ… Test #11: list-functions

**Category**: Schema Operations  
**Description**: List functions in schema  
**Status**: âœ… PASSED  
**Duration**: 2035 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:11:46  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-functions -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:11:45.0332|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:11:45.0507|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:11:45.0507|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:11:45.0507|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:11:45.0507|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:11:45.0507|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-functions -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-functions_20251216_081123.json
2025-12-16 08:11:45.0507|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:11:45.0633|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:11:45.0633|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:11:45.0633|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:11:45.1002|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:11:45.1002|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:11:45.1002|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:11:45.1002|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:11:45.1002|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:11:45.1176|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:11:45.1176|DEBUG|WindowsDb2Editor.Services.MetadataHandle
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "totalFunctions": 0,
  "limitApplied": 0,
  "functions": [],
  "retrievedAt": "2025-12-16T08:11:46.4416856+01:00"
}
```

---

### âœ… Test #12: list-packages

**Category**: Schema Operations  
**Description**: List packages in schema  
**Status**: âœ… PASSED  
**Duration**: 2381 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:11:48  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-packages -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:11:47.0644|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:11:47.0839|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:11:47.0839|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:11:47.0839|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:11:47.0945|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:11:47.0945|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-packages -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-packages_20251216_081123.json
2025-12-16 08:11:47.0945|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:11:47.0945|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:11:47.0945|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:11:47.0945|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:11:47.1464|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:11:47.1464|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:11:47.1464|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:11:47.1464|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:11:47.1464|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:11:47.1608|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:11:47.1608|DEBUG|WindowsDb2Editor.Services.MetadataHandler|
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "totalPackages": 0,
  "limitApplied": 0,
  "packages": [],
  "retrievedAt": "2025-12-16T08:11:48.8226162+01:00"
}
```

---

### âœ… Test #13: list-tablespaces

**Category**: Schema Operations  
**Description**: List all tablespaces  
**Status**: âœ… PASSED  
**Duration**: 2079 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:11:51  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-tablespaces -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:11:49.4445|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:11:49.4590|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:11:49.4590|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:11:49.4590|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:11:49.4590|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:11:49.4696|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-tablespaces -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_list-tablespaces_20251216_081123.json
2025-12-16 08:11:49.4696|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:11:49.4696|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:11:49.4696|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:11:49.4696|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:11:49.5222|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:11:49.5222|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:11:49.5222|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:11:49.5222|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:11:49.5382|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:11:49.5382|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:11:49.5382|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
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
**Duration**: 2225 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:11:53  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-all-indexes -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:11:51.5822|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:11:51.5965|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:11:51.5965|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:11:51.5965|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:11:51.5965|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:11:51.5965|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-all-indexes -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-all-indexes_20251216_081123.json
2025-12-16 08:11:51.5965|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:11:51.5965|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:11:51.6098|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:11:51.6098|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:11:51.6438|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:11:51.6438|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:11:51.6438|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:11:51.6438|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:11:51.6588|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:11:51.6588|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:11:51.6588|DEBUG|WindowsDb2Editor.Services.MetadataHa
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
  "retrievedAt": "2025-12-16T08:11:53.1410303+01:00"
}
```

---

### âœ… Test #15: list-constraints

**Category**: Schema Operations  
**Description**: List constraints in schema  
**Status**: âœ… PASSED  
**Duration**: 2272 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:11:55  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-constraints -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:11:53.8710|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:11:53.8800|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:11:53.8800|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:11:53.8800|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:11:53.8800|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:11:53.8914|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-constraints -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-constraints_20251216_081123.json
2025-12-16 08:11:53.8914|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:11:53.8914|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:11:53.8914|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:11:53.8914|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:11:53.9500|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:11:53.9500|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:11:53.9500|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:11:53.9543|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:11:53.9543|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:11:53.9764|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:11:53.9764|DEBUG|WindowsDb2Editor.Services.MetadataHa
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
**Duration**: 1960 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:11:57  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-sequences -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:11:56.0499|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:11:56.0652|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:11:56.0652|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:11:56.0652|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:11:56.0652|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:11:56.0652|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-sequences -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-sequences_20251216_081123.json
2025-12-16 08:11:56.0652|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:11:56.0652|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:11:56.0652|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:11:56.0793|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:11:56.1155|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:11:56.1155|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:11:56.1155|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:11:56.1155|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:11:56.1155|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:11:56.1273|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:11:56.1273|DEBUG|WindowsDb2Editor.Services.MetadataHandle
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "list-sequences",
  "sequenceCount": 0,
  "sequences": [],
  "retrievedAt": "2025-12-16T08:11:57.3834717+01:00"
}
```

---

### âœ… Test #17: table-properties

**Category**: Table Operations  
**Description**: Get table properties  
**Status**: âœ… PASSED  
**Duration**: 2332 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:11:59  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-properties -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:11:58.1615|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:11:58.1827|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:11:58.1916|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:11:58.1916|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:11:58.1916|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:11:58.1916|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-properties -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-properties_20251216_081123.json
2025-12-16 08:11:58.1916|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:11:58.1916|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:11:58.1916|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:11:58.1916|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:11:58.2452|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:11:58.2452|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:11:58.2452|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:11:58.2452|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:11:58.2510|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:11:58.2510|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:11:58.2677|DEBUG|WindowsDb2Editor.Services.Me
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
  "retrievedAt": "2025-12-16T08:11:59.72029+01:00"
}
```

---

### âœ… Test #18: table-columns

**Category**: Table Operations  
**Description**: List table columns  
**Status**: âœ… PASSED  
**Duration**: 2126 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:01  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-columns -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:00.3617|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:00.3773|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:00.3773|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:00.3773|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:00.3773|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:00.3773|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-columns -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-columns_20251216_081123.json
2025-12-16 08:12:00.3773|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:00.3773|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:00.3946|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:00.3946|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:00.4413|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:00.4413|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:00.4413|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:00.4413|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:00.4563|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:00.4563|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:00.4563|DEBUG|WindowsDb2Editor.Services.Metadata
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
  "retrievedAt": "2025-12-16T08:12:01.8465055+01:00"
}
```

---

### âœ… Test #19: table-indexes

**Category**: Table Operations  
**Description**: List table indexes  
**Status**: âœ… PASSED  
**Duration**: 2164 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:04  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-indexes -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:02.5535|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:02.5695|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:02.5695|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:02.5695|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:02.5695|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:02.5695|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-indexes -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-indexes_20251216_081123.json
2025-12-16 08:12:02.5695|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:02.5695|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:02.5830|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:02.5830|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:02.6224|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:02.6224|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:02.6224|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:02.6224|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:02.6260|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:02.6441|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:02.6441|DEBUG|WindowsDb2Editor.Services.Metadata
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
  "retrievedAt": "2025-12-16T08:12:04.0190351+01:00"
}
```

---

### âœ… Test #20: table-foreignkeys

**Category**: Table Operations  
**Description**: List table foreign keys  
**Status**: âœ… PASSED  
**Duration**: 2230 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:06  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-foreignkeys -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:04.7069|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:04.7221|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:04.7221|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:04.7221|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:04.7221|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:04.7221|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-foreignkeys -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-foreignkeys_20251216_081123.json
2025-12-16 08:12:04.7221|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:04.7221|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:04.7363|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:04.7363|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:04.7718|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:04.7718|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:04.7718|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:04.7718|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:04.7882|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:04.7882|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:04.7882|DEBUG|WindowsDb2Editor.Services.
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
  "retrievedAt": "2025-12-16T08:12:06.2575931+01:00"
}
```

---

### âœ… Test #21: table-ddl

**Category**: Table Operations  
**Description**: Generate table DDL  
**Status**: âœ… PASSED  
**Duration**: 2346 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:08  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-ddl -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:06.8961|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:06.9106|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:06.9106|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:06.9106|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:06.9106|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:06.9106|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-ddl -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-ddl_20251216_081123.json
2025-12-16 08:12:06.9106|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:06.9106|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:06.9106|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:06.9284|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:06.9665|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:06.9665|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:06.9665|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:06.9693|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:06.9693|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:06.9693|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:06.9693|DEBUG|WindowsDb2Editor.Services.MetadataHandler|
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
  "retrievedAt": "2025-12-16T08:12:08.5941511+01:00"
}
```

---

### âœ… Test #22: table-statistics

**Category**: Table Operations  
**Description**: Get table statistics  
**Status**: âœ… PASSED  
**Duration**: 2232 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:10  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-statistics -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:09.3149|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:09.3308|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:09.3308|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:09.3308|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:09.3308|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:09.3308|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-statistics -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-statistics_20251216_081123.json
2025-12-16 08:12:09.3308|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:09.3451|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:09.3451|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:09.3451|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:09.3892|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:09.3910|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:09.3910|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:09.3910|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:09.3910|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:09.4078|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:09.4078|DEBUG|WindowsDb2Editor.Services.Me
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
**Duration**: 2281 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:13  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-relationships -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:11.5514|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:11.5803|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:11.5803|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:11.5803|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:11.5803|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:11.5803|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-relationships -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-relationships_20251216_081123.json
2025-12-16 08:12:11.5803|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:11.5803|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:11.5947|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:11.5947|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:11.6416|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:11.6416|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:11.6416|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:11.6416|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:11.6416|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:11.6578|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:11.6578|DEBUG|WindowsDb2Editor.Servi
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
**Duration**: 2424 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:15  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-sample-data -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:13.8169|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:13.8373|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:13.8373|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:13.8373|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:13.8373|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:13.8373|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-sample-data -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-sample-data_20251216_081123.json
2025-12-16 08:12:13.8373|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:13.8373|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:13.8518|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:13.8518|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:13.8848|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:13.8848|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:13.8848|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:13.8848|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:13.8958|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:13.8958|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:13.8958|DEBUG|WindowsDb2Editor.Services.
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
**Duration**: 2364 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:18  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-size -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:16.2682|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:16.2876|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:16.2876|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:16.2977|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:16.2977|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:16.2977|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-size -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-size_20251216_081123.json
2025-12-16 08:12:16.2977|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:16.2977|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:16.2977|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:16.2977|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:16.3385|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:16.3385|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:16.3385|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:16.3385|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:16.3549|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:16.3549|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:16.3549|DEBUG|WindowsDb2Editor.Services.MetadataHandle
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
  "retrievedAt": "2025-12-16T08:12:17.941534+01:00"
}
```

---

### âœ… Test #26: table-grants

**Category**: Table Operations  
**Description**: List table grants  
**Status**: âœ… PASSED  
**Duration**: 2231 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:20  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-grants -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:18.6932|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:18.7108|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:18.7108|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:18.7108|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:18.7108|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:18.7108|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-grants -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-grants_20251216_081123.json
2025-12-16 08:12:18.7108|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:18.7108|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:18.7280|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:18.7280|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:18.7719|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:18.7719|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:18.7719|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:18.7719|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:18.7874|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:18.7874|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:18.7874|DEBUG|WindowsDb2Editor.Services.MetadataHa
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
  "retrievedAt": "2025-12-16T08:12:20.1891324+01:00"
}
```

---

### âœ… Test #27: table-dependencies

**Category**: Table Operations  
**Description**: Show table dependencies  
**Status**: âœ… PASSED  
**Duration**: 1894 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:22  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-dependencies -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:20.7395|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:20.7560|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:20.7560|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:20.7560|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:20.7560|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:20.7560|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-dependencies -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-dependencies_20251216_081123.json
2025-12-16 08:12:20.7560|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:20.7668|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:20.7668|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:20.7668|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:20.8135|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:20.8135|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:20.8135|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:20.8135|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:20.8135|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:20.8336|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:20.8336|DEBUG|WindowsDb2Editor.Service
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
**Duration**: 2020 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:24  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-incoming-fk -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:22.6441|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:22.6575|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:22.6575|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:22.6575|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:22.6575|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:22.6575|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-incoming-fk -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-incoming-fk_20251216_081123.json
2025-12-16 08:12:22.6575|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:22.6575|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:22.6575|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:22.6731|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:22.7024|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:22.7045|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:22.7045|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:22.7045|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:22.7045|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:22.7245|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:22.7245|DEBUG|WindowsDb2Editor.Services.
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "incomingFKCount": 0,
  "incomingFKs": [],
  "retrievedAt": "2025-12-16T08:12:24.0994681+01:00"
}
```

---

### âœ… Test #29: table-used-by-packages

**Category**: Table Operations  
**Description**: List packages using this table  
**Status**: âœ… PASSED  
**Duration**: 1941 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:26  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-used-by-packages -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:24.6771|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:24.6958|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:24.6958|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:24.6958|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:24.6958|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:24.6958|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-used-by-packages -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-used-by-packages_20251216_081123.json
2025-12-16 08:12:24.6958|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:24.7043|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:24.7043|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:24.7043|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:24.7394|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:24.7394|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:24.7394|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:24.7394|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:24.7516|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:24.7516|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:24.7516|DEBUG|WindowsDb2Editor
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "packageCount": 0,
  "packages": [],
  "retrievedAt": "2025-12-16T08:12:26.0558607+01:00"
}
```

---

### âœ… Test #30: table-used-by-views

**Category**: Table Operations  
**Description**: List views using this table  
**Status**: âœ… PASSED  
**Duration**: 2052 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:28  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-used-by-views -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:26.6726|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:26.6892|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:26.6892|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:26.6892|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:26.6892|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:26.6892|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-used-by-views -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-used-by-views_20251216_081123.json
2025-12-16 08:12:26.6892|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:26.6892|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:26.6892|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:26.7056|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:26.7446|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:26.7446|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:26.7446|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:26.7446|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:26.7513|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:26.7665|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:26.7665|DEBUG|WindowsDb2Editor.Servi
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "viewCount": 0,
  "views": [],
  "retrievedAt": "2025-12-16T08:12:28.1140536+01:00"
}
```

---

### âœ… Test #31: table-used-by-routines

**Category**: Table Operations  
**Description**: List routines using this table  
**Status**: âœ… PASSED  
**Duration**: 2127 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:30  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-used-by-routines -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:28.6963|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:28.7112|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:28.7200|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:28.7200|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:28.7200|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:28.7200|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-used-by-routines -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-used-by-routines_20251216_081123.json
2025-12-16 08:12:28.7200|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:28.7200|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:28.7200|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:28.7200|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:28.7779|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:28.7779|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:28.7779|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:28.7779|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:28.7826|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:28.7826|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:28.7826|DEBUG|WindowsDb2Editor
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "routineCount": 0,
  "routines": [],
  "retrievedAt": "2025-12-16T08:12:30.2534359+01:00"
}
```

---

### âœ… Test #32: view-properties

**Category**: View Operations  
**Description**: Get view properties  
**Status**: âœ… PASSED  
**Duration**: 2231 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:32  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-properties -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:30.8957|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:30.9132|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:30.9132|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:30.9132|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:30.9132|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:30.9132|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-properties -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-properties_20251216_081123.json
2025-12-16 08:12:30.9132|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:30.9249|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:30.9249|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:30.9249|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:30.9572|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:30.9586|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:30.9586|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:30.9586|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:30.9586|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:30.9723|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:30.9723|DEBUG|WindowsDb2Editor.Services
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
  "retrievedAt": "2025-12-16T08:12:32.4831945+01:00"
}
```

---

### âœ… Test #33: view-definition

**Category**: View Operations  
**Description**: Get view definition SQL  
**Status**: âœ… PASSED  
**Duration**: 2225 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:34  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-definition -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:33.1581|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:33.1735|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:33.1735|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:33.1735|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:33.1735|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:33.1735|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-definition -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-definition_20251216_081123.json
2025-12-16 08:12:33.1735|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:33.1735|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:33.1888|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:33.1888|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:33.2264|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:33.2269|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:33.2269|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:33.2269|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:33.2414|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:33.2414|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:33.2414|DEBUG|WindowsDb2Editor.Services
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
**Duration**: 2575 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:37  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-columns -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:35.4296|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:35.4413|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:35.4413|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:35.4413|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:35.4413|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:35.4413|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-columns -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-columns_20251216_081123.json
2025-12-16 08:12:35.4413|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:35.4560|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:35.4560|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:35.4560|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:35.5073|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:35.5073|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:35.5073|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:35.5073|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:35.5208|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:35.5208|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:35.5208|DEBUG|WindowsDb2Editor.Services.Metad
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
**Duration**: 2406 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:39  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-dependencies -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:38.0221|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:38.0490|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:38.0490|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:38.0490|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:38.0490|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:38.0490|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-dependencies -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-dependencies_20251216_081123.json
2025-12-16 08:12:38.0490|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:38.0490|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:38.0490|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:38.0634|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:38.1057|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:38.1057|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:38.1057|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:38.1107|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:38.1107|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:38.1107|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:38.1107|DEBUG|WindowsDb2Editor.Serv
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
**Duration**: 2285 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:42  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-sample-data -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:40.3473|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:40.3610|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:40.3610|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:40.3610|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:40.3610|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:40.3610|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-sample-data -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-sample-data_20251216_081123.json
2025-12-16 08:12:40.3610|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:40.3610|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:40.3610|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:40.3768|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:40.4108|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:40.4108|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:40.4108|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:40.4108|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:40.4108|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:40.4261|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:40.4261|DEBUG|WindowsDb2Editor.Servic
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
**Duration**: 1931 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:44  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-used-by-packages -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:42.6452|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:42.6578|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:42.6578|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:42.6578|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:42.6578|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:42.6578|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-used-by-packages -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-used-by-packages_20251216_081123.json
2025-12-16 08:12:42.6578|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:42.6578|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:42.6765|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:42.6765|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:42.7141|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:42.7141|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:42.7141|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:42.7141|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:42.7204|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:42.7204|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:42.7204|DEBUG|WindowsDb2Edi
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
**Duration**: 1880 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:45  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-used-by-views -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:44.5502|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:44.5637|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:44.5637|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:44.5637|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:44.5637|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:44.5637|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-used-by-views -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-used-by-views_20251216_081123.json
2025-12-16 08:12:44.5637|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:44.5637|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:44.5637|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:44.5791|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:44.6098|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:44.6113|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:44.6113|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:44.6113|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:44.6113|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:44.6275|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:44.6275|DEBUG|WindowsDb2Editor.Se
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
**Duration**: 2172 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:48  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-properties -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:46.3868|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:46.3947|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:46.3947|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:46.3947|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:46.3947|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:46.3947|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-properties -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-properties_20251216_081123.json
2025-12-16 08:12:46.3947|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:46.3947|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:46.4084|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:46.4084|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:46.4418|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:46.4418|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:46.4418|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:46.4418|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:46.4418|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:46.4546|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:46.4546|DEBUG|WindowsDb2Edit
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
  "retrievedAt": "2025-12-16T08:12:47.9808409+01:00"
}
```

---

### âœ… Test #40: procedure-source

**Category**: Procedure Operations  
**Description**: Get procedure source code  
**Status**: âœ… PASSED  
**Duration**: 2045 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:50  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-source -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:48.6603|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:48.6737|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:48.6737|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:48.6737|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:48.6737|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:48.6737|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-source -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-source_20251216_081123.json
2025-12-16 08:12:48.6737|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:48.6737|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:48.6737|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:48.6737|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:48.7199|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:48.7199|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:48.7199|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:48.7199|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:48.7360|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:48.7360|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:48.7360|DEBUG|WindowsDb2Editor.Servi
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
**Duration**: 1843 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:51  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-parameters -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:50.6306|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:50.6426|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:50.6426|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:50.6426|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:50.6426|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:50.6426|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-parameters -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-parameters_20251216_081123.json
2025-12-16 08:12:50.6426|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:50.6426|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:50.6426|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:50.6587|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:50.6836|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:50.6836|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:50.6836|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:50.6836|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:50.6897|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:50.6897|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:50.6897|DEBUG|WindowsDb2Edit
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
**Duration**: 1965 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:53  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-dependencies -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:52.4889|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:52.5045|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:52.5045|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:52.5045|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:52.5045|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:52.5045|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-dependencies -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-dependencies_20251216_081123.json
2025-12-16 08:12:52.5045|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:52.5045|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:52.5045|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:52.5171|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:52.5593|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:52.5593|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:52.5593|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:52.5593|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:52.5636|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:52.5636|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:52.5636|DEBUG|WindowsDb2
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
**Duration**: 1827 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:55  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-usage -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:54.4555|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:54.4555|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:54.4698|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:54.4698|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:54.4698|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:54.4698|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-usage -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-usage_20251216_081123.json
2025-12-16 08:12:54.4698|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:54.4698|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:54.4698|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:54.4698|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:54.5167|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:54.5172|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:54.5172|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:54.5172|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:54.5172|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:54.5362|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:54.5362|DEBUG|WindowsDb2Editor.Service
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
**Duration**: 1911 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:57  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-grants -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:56.2449|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:56.2580|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:56.2580|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:56.2580|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:56.2580|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:56.2670|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-grants -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-grants_20251216_081123.json
2025-12-16 08:12:56.2670|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:56.2670|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:56.2670|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:56.2670|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:56.3150|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:56.3150|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:56.3150|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:56.3150|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:56.3306|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:56.3306|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:56.3306|DEBUG|WindowsDb2Editor.Servi
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
**Duration**: 1966 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:12:59  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-properties -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:12:58.2139|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:12:58.2271|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:12:58.2271|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:12:58.2271|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:12:58.2271|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:12:58.2271|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-properties -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-properties_20251216_081123.json
2025-12-16 08:12:58.2271|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:12:58.2271|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:12:58.2384|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:12:58.2384|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:12:58.2747|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:12:58.2747|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:12:58.2747|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:12:58.2747|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:12:58.2901|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:12:58.2901|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:12:58.2901|DEBUG|WindowsDb2Editor.Servic
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
  "retrievedAt": "2025-12-16T08:12:59.5729864+01:00"
}
```

---

### âœ… Test #46: function-source

**Category**: Function Operations  
**Description**: Get function source code  
**Status**: âœ… PASSED  
**Duration**: 1955 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:01  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-source -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:00.2551|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:00.2681|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:00.2681|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:00.2681|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:00.2681|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:00.2681|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-source -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-source_20251216_081123.json
2025-12-16 08:13:00.2681|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:00.2681|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:00.2681|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:00.2851|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:00.3237|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:00.3237|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:00.3237|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:00.3237|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:00.3376|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:00.3376|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:00.3376|DEBUG|WindowsDb2Editor.Services.Metad
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
**Duration**: 1846 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:03  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-parameters -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:02.0968|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:02.1125|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:02.1125|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:02.1125|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:02.1125|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:02.1125|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-parameters -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-parameters_20251216_081123.json
2025-12-16 08:13:02.1125|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:02.1125|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:02.1125|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:02.1274|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:02.1550|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:02.1550|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:02.1550|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:02.1582|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:02.1582|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:02.1582|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:02.1582|DEBUG|WindowsDb2Editor.Servic
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
**Duration**: 1914 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:05  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-dependencies -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:04.0518|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:04.0518|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:04.0518|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:04.0518|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:04.0685|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:04.0685|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-dependencies -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-dependencies_20251216_081123.json
2025-12-16 08:13:04.0685|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:04.0685|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:04.0685|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:04.0817|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:04.1106|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:04.1106|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:04.1106|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:04.1106|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:04.1106|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:04.1300|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:04.1300|DEBUG|WindowsDb2Editor.Se
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
**Duration**: 1778 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:07  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-usage -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:05.8917|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:05.8917|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:05.8917|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:05.8917|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:05.8917|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:05.8917|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-usage -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-usage_20251216_081123.json
2025-12-16 08:13:05.8917|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:05.8917|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:05.9084|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:05.9084|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:05.9419|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:05.9419|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:05.9419|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:05.9419|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:05.9419|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:05.9544|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:05.9544|DEBUG|WindowsDb2Editor.Services.Metadat
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
**Duration**: 2253 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:09  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-grants -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:07.7144|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:07.7382|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:07.7382|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:07.7382|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:07.7382|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:07.7382|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-grants -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-grants_20251216_081123.json
2025-12-16 08:13:07.7382|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:07.7382|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:07.7523|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:07.7523|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:07.8055|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:07.8055|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:07.8055|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:07.8055|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:07.8250|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:07.8322|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:07.8322|DEBUG|WindowsDb2Editor.Services.Metad
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
**Duration**: 1950 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:11  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-properties -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:09.9747|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:09.9867|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:09.9867|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:09.9867|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:09.9867|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:09.9867|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-properties -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-properties_20251216_081123.json
2025-12-16 08:13:09.9867|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:09.9867|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:09.9867|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:10.0032|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:10.0353|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:10.0353|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:10.0353|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:10.0353|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:10.0484|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:10.0484|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:10.0484|DEBUG|WindowsDb2Editor.Servic
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
**Duration**: 1946 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:13  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-statements -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:11.8812|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:11.8950|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:11.9075|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:11.9075|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:11.9075|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:11.9075|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-statements -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-statements_20251216_081123.json
2025-12-16 08:13:11.9075|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:11.9075|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:11.9075|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:11.9075|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:11.9447|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:11.9447|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:11.9447|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:11.9447|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:11.9575|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:11.9575|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:11.9575|DEBUG|WindowsDb2Editor.Servic
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
**Duration**: 2016 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:15  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-dependencies -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:13.8906|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:13.9082|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:13.9082|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:13.9082|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:13.9082|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:13.9082|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-dependencies -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-dependencies_20251216_081123.json
2025-12-16 08:13:13.9082|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:13.9245|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:13.9245|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:13.9245|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:13.9673|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:13.9673|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:13.9673|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:13.9673|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:13.9815|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:13.9815|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:13.9874|DEBUG|WindowsDb2Editor.Se
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
**Duration**: 1975 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:17  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-statistics -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:15.9002|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:15.9156|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:15.9156|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:15.9156|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:15.9156|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:15.9232|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-statistics -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-statistics_20251216_081123.json
2025-12-16 08:13:15.9232|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:15.9232|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:15.9232|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:15.9232|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:15.9611|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:15.9611|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:15.9611|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:15.9611|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:15.9722|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:15.9722|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:15.9722|DEBUG|WindowsDb2Editor.Servic
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
**Duration**: 1948 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:19  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-list-tables -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:17.8799|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:17.8935|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:17.8935|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:17.8935|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:17.8935|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:17.8935|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-list-tables -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-list-tables_20251216_081123.json
2025-12-16 08:13:17.8935|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:17.8935|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:17.8935|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:17.9077|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:17.9360|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:17.9360|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:17.9360|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:17.9360|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:17.9465|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:17.9465|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:17.9465|DEBUG|WindowsDb2Editor.Serv
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
**Duration**: 2073 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:21  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-list-views -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:19.8684|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:19.8894|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:19.8894|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:19.8894|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:19.8977|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:19.8977|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-list-views -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-list-views_20251216_081123.json
2025-12-16 08:13:19.8977|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:19.8977|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:19.8977|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:19.9099|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:19.9403|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:19.9403|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:19.9403|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:19.9403|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:19.9403|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:19.9595|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:19.9595|DEBUG|WindowsDb2Editor.Servic
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
**Duration**: 2303 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:23  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-list-procedures -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:21.9759|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:22.0036|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:22.0036|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:22.0036|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:22.0036|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:22.0036|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-list-procedures -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-list-procedures_20251216_081123.json
2025-12-16 08:13:22.0036|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:22.0036|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:22.0036|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:22.0193|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:22.0464|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:22.0464|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:22.0496|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:22.0496|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:22.0683|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:22.0683|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:22.0683|DEBUG|WindowsDb2Edi
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
**Duration**: 1971 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:25  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-list-functions -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:24.2074|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:24.2253|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:24.2253|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:24.2253|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:24.2356|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:24.2356|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-list-functions -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-list-functions_20251216_081123.json
2025-12-16 08:13:24.2356|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:24.2356|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:24.2356|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:24.2356|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:24.2830|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:24.2830|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:24.2830|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:24.2830|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:24.2830|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:24.3037|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:24.3037|DEBUG|WindowsDb2Edito
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
**Duration**: 1679 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:27  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command user-properties -profile FKKTOTST -object FKGEISTA -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:26.1272|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:26.1431|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:26.1431|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:26.1431|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:26.1431|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:26.1431|DEBUG|WindowsDb2Editor.App|Startup arguments: -command user-properties -profile FKKTOTST -object FKGEISTA -format json -Outfile CLI_Test_Output/temp_user-properties_20251216_081123.json
2025-12-16 08:13:26.1431|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:26.1431|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:26.1431|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:26.1431|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:26.1786|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:26.1786|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:26.1786|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:26.1786|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:26.1889|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:26.1889|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:26.1889|DEBUG|WindowsDb2Editor.Services.Metadat
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
**Duration**: 1929 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:29  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command user-privileges -profile FKKTOTST -object FKGEISTA -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:28.0094|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:28.0330|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:28.0330|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:28.0330|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:28.0330|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:28.0330|DEBUG|WindowsDb2Editor.App|Startup arguments: -command user-privileges -profile FKKTOTST -object FKGEISTA -format json -Outfile CLI_Test_Output/temp_user-privileges_20251216_081123.json
2025-12-16 08:13:28.0330|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:28.0330|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:28.0496|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:28.0496|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:28.0877|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:28.0877|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:28.0877|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:28.0877|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:28.1016|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:28.1016|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:28.1016|DEBUG|WindowsDb2Editor.Services.Metadat
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
**Duration**: 2114 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:31  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command user-tables -profile FKKTOTST -object FKGEISTA -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:29.7851|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:29.7986|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:29.7986|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:29.7986|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:29.7986|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:29.7986|DEBUG|WindowsDb2Editor.App|Startup arguments: -command user-tables -profile FKKTOTST -object FKGEISTA -format json -Outfile CLI_Test_Output/temp_user-tables_20251216_081123.json
2025-12-16 08:13:29.7986|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:29.7986|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:29.7986|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:29.7986|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:29.8458|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:29.8458|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:29.8458|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:29.8458|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:29.8458|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:29.8625|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:29.8625|DEBUG|WindowsDb2Editor.Services.MetadataHandler
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
**Duration**: 2045 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:33  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command user-schemas -profile FKKTOTST -object FKGEISTA -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:31.9283|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:31.9445|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:31.9445|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:31.9445|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:31.9445|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:31.9445|DEBUG|WindowsDb2Editor.App|Startup arguments: -command user-schemas -profile FKKTOTST -object FKGEISTA -format json -Outfile CLI_Test_Output/temp_user-schemas_20251216_081123.json
2025-12-16 08:13:31.9549|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:31.9549|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:31.9549|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:31.9549|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:31.9991|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:31.9991|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:32.0023|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:32.0023|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:32.0023|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:32.0023|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:32.0174|DEBUG|WindowsDb2Editor.Services.MetadataHandl
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
**Duration**: 2018 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:35  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command user-connections -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:33.9936|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:34.0183|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:34.0183|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:34.0183|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:34.0183|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:34.0183|DEBUG|WindowsDb2Editor.App|Startup arguments: -command user-connections -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_user-connections_20251216_081123.json
2025-12-16 08:13:34.0183|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:34.0183|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:34.0183|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:34.0325|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:34.0656|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:34.0656|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:34.0656|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:34.0656|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:34.0656|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:34.0811|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:34.0811|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
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
**Duration**: 1923 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:37  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-load -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:36.0051|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:36.0185|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:36.0185|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:36.0185|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:36.0185|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:36.0185|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-load -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-load_20251216_081123.json
2025-12-16 08:13:36.0185|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:36.0185|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:36.0185|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:36.0336|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:36.0655|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:36.0669|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:36.0669|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:36.0669|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:36.0669|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:36.0669|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:36.0841|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2
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
      "rowsRead": 850734,
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
      "rowsRead": 320469,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSROUTINEPARMS",
      "rowsRead": 294338,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSTABLES",
      "rowsRead": 125613,
      "rowsInserted": 0,
      "rowsUpdated": 150,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSDEPENDENCIES",
      "rowsRead": 22820,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSDATATYPES",
      "rowsRead": 12960,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSPLAN",
      "rowsRead": 12368,
      "rowsInserted": 0,
      "rowsUpdated": 18,
      "rowsDeleted": 0
    },
    {
      "schema": "INL",
      "tableName": "KONTOTYPE",
      "rowsRead": 4800,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSDBAUTH",
      "rowsRead": 3740,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSVIEWS",
      "rowsRead": 3733,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSRELS",
      "rowsRead": 2179,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSSCHEMATA",
      "rowsRead": 1357,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSTABLESPACES",
      "rowsRead": 974,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSTABCONST",
      "rowsRead": 588,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "INL",
      "tableName": "BILAGNR",
      "rowsRead": 543,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSINDEXES",
      "rowsRead": 238,
    
... (truncated)
```

---

### âœ… Test #65: db-locks

**Category**: Monitoring & Stats  
**Description**: Show database locks  
**Status**: âœ… PASSED  
**Duration**: 2134 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:39  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-locks -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:37.8865|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:37.9009|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:37.9009|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:37.9009|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:37.9009|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:37.9009|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-locks -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-locks_20251216_081123.json
2025-12-16 08:13:37.9009|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:37.9009|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:37.9079|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:37.9079|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:37.9376|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:37.9388|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:37.9388|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:37.9388|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:37.9388|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:37.9546|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:37.9546|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for D
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
      "agentId": 10779
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
      "agentId": 10779
    }
  ],
  "retrievedAt": "2025-12-16T08:13:39.4722676+01:00"
}
```

---

### âœ… Test #66: db-sessions

**Category**: Monitoring & Stats  
**Description**: List database sessions  
**Status**: âœ… PASSED  
**Duration**: 1574 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:41  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-sessions -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:39.9748|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:39.9748|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:39.9748|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:39.9748|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:39.9748|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:39.9881|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-sessions -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-sessions_20251216_081123.json
2025-12-16 08:13:39.9881|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:39.9881|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:39.9881|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:39.9881|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:40.0216|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:40.0216|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:40.0216|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:40.0216|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:40.0338|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:40.0338|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:40.0338|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata
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
      "connectTime": "2025-12-16T08:13:41.087",
      "message": "Session monitoring requires admin views"
    }
  ],
  "retrievedAt": "2025-12-16T08:13:41.0500783+01:00"
}
```

---

### âœ… Test #67: db-size

**Category**: Monitoring & Stats  
**Description**: Get database size  
**Status**: âœ… PASSED  
**Duration**: 1623 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:42  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-size -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:41.5376|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:41.5494|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:41.5494|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:41.5494|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:41.5494|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:41.5494|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-size -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-size_20251216_081123.json
2025-12-16 08:13:41.5494|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:41.5494|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:41.5494|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:41.5494|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:41.5828|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:41.5828|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:41.5828|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:41.5828|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:41.5828|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:41.5972|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:41.5972|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "totalRows": 1599183,
  "totalPages": 62127,
  "totalSizeMB": 242.68359375000,
  "approximateSizeGB": 0.23699569702148438,
  "retrievedAt": "2025-12-16T08:13:42.6681899+01:00"
}
```

---

### âœ… Test #68: schema-size

**Category**: Monitoring & Stats  
**Description**: Get schema size  
**Status**: âœ… PASSED  
**Duration**: 1630 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:44  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command schema-size -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:43.1758|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:43.1758|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:43.1894|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:43.1894|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:43.1894|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:43.1894|DEBUG|WindowsDb2Editor.App|Startup arguments: -command schema-size -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_schema-size_20251216_081123.json
2025-12-16 08:13:43.1894|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:43.1894|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:43.1894|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:43.1894|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:43.2275|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:43.2275|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:43.2275|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:43.2275|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:43.2385|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:43.2385|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:43.2385|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Load
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
  "retrievedAt": "2025-12-16T08:13:44.2957387+01:00"
}
```

---

### âœ… Test #69: db-version

**Category**: Monitoring & Stats  
**Description**: Get database version  
**Status**: âœ… PASSED  
**Duration**: 1692 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:46  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-version -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:44.8218|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:44.8340|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:44.8340|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:44.8340|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:44.8340|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:44.8340|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-version -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-version_20251216_081123.json
2025-12-16 08:13:44.8340|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:44.8340|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:44.8340|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:44.8463|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:44.8742|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:44.8742|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:44.8742|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:44.8742|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:44.8833|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:44.8833|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:44.8833|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata f
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
**Duration**: 1724 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:47  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-parameters -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:46.4986|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:46.5070|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:46.5070|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:46.5070|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:46.5070|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:46.5070|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-parameters -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-parameters_20251216_081123.json
2025-12-16 08:13:46.5070|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:46.5070|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:46.5203|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:46.5203|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:46.5512|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:46.5512|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:46.5512|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:46.5512|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:46.5512|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:46.5672|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:46.5672|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading meta
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
**Duration**: 2115 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:49  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-registry -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:48.3461|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:48.3628|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:48.3628|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:48.3628|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:48.3628|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:48.3628|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-registry -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-registry_20251216_081123.json
2025-12-16 08:13:48.3628|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:48.3628|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:48.3628|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:48.3769|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:48.4052|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:48.4052|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:48.4052|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:48.4052|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:48.4076|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:48.4076|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:48.4076|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata
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
**Duration**: 1908 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:51  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command active-queries -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:50.3519|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:50.3653|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:50.3653|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:50.3653|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:50.3653|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:50.3653|DEBUG|WindowsDb2Editor.App|Startup arguments: -command active-queries -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_active-queries_20251216_081123.json
2025-12-16 08:13:50.3653|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:50.3653|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:50.3773|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:50.3773|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:50.4065|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:50.4084|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:50.4084|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:50.4084|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:50.4084|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:50.4084|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:50.4084|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading me
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "active-queries",
  "queries": [
    {
      "APPLICATION_HANDLE": 10795,
      "STMT_TEXT": "SELECT APPLICATION_HANDLE, STMT_TEXT, ELAPSED_TIME_SEC FROM SYSIBMADM.MON_CURRENT_SQL ORDER BY ELAPSED_TIME_SEC DESC FETCH FIRST 20 ROWS ONLY",
      "ELAPSED_TIME_SEC": -1
    }
  ]
}
```

---

### âœ… Test #73: bufferpool-stats

**Category**: Monitoring & Stats  
**Description**: Get buffer pool statistics  
**Status**: âœ… PASSED  
**Duration**: 1769 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:53  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command bufferpool-stats -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:52.2462|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:52.2579|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:52.2579|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:52.2579|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:52.2579|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:52.2579|DEBUG|WindowsDb2Editor.App|Startup arguments: -command bufferpool-stats -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_bufferpool-stats_20251216_081123.json
2025-12-16 08:13:52.2579|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:52.2579|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:52.2692|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:52.2692|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:52.2950|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:52.2950|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:52.2950|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:52.2950|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:52.2985|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:52.2985|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:52.2985|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
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
**Duration**: 1989 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:55  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command tablespace-usage -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:54.0826|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:54.0990|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:54.0990|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:54.0990|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:54.0990|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:54.0990|DEBUG|WindowsDb2Editor.App|Startup arguments: -command tablespace-usage -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_tablespace-usage_20251216_081123.json
2025-12-16 08:13:54.0990|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:54.0990|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:54.1112|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:54.1112|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:54.1480|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:54.1480|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:54.1480|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:54.1480|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:54.1625|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:54.1625|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:54.1625|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
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
**Duration**: 1743 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:57  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-status -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:56.0527|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:56.0527|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:56.0675|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:56.0675|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:56.0675|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:56.0675|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-status -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_cdc-status_20251216_081123.json
2025-12-16 08:13:56.0675|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:56.0675|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:56.0675|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:56.0675|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:56.1008|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:56.1008|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:56.1008|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:56.1008|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:56.1115|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:56.1115|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:56.1115|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableCount": 0,
  "tables": [],
  "retrievedAt": "2025-12-16T08:13:57.281145+01:00"
}
```

---

### âœ… Test #76: cdc-status-full

**Category**: CDC Operations  
**Description**: Get full CDC status  
**Status**: âœ… PASSED  
**Duration**: 1734 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:13:59  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-status-full -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:57.8183|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:57.8322|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:57.8322|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:57.8322|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:57.8322|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:57.8322|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-status-full -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_cdc-status-full_20251216_081123.json
2025-12-16 08:13:57.8322|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:57.8322|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:57.8322|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:57.8322|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:57.8765|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:57.8765|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:57.8765|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:57.8765|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:57.8814|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:57.8934|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:57.8934|DEBUG|WindowsDb2Editor.Services.MetadataHand
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "cdcEnabled": false,
  "note": "CDC status requires SYSCAT.DATAPARTITIONS or CDC-specific views",
  "retrievedAt": "2025-12-16T08:13:59.0133888+01:00"
}
```

---

### âœ… Test #77: cdc-configuration

**Category**: CDC Operations  
**Description**: Get CDC configuration for table  
**Status**: âœ… PASSED  
**Duration**: 1660 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:00  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-configuration -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:13:59.5693|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:13:59.5847|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:13:59.5847|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:13:59.5847|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:13:59.5847|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:13:59.5847|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-configuration -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_cdc-configuration_20251216_081123.json
2025-12-16 08:13:59.5847|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:13:59.5847|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:13:59.5963|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:13:59.5963|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:13:59.6272|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:13:59.6279|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:13:59.6279|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:13:59.6279|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:13:59.6279|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:13:59.6279|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:13:59.6279|DEBUG|WindowsDb2Editor.Services.
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "cdcConfigured": false,
  "note": "CDC configuration requires ASN tables (IBMSNAP_*)",
  "retrievedAt": "2025-12-16T08:14:00.6773637+01:00"
}
```

---

### âœ… Test #78: cdc-changes

**Category**: CDC Operations  
**Description**: List CDC changes  
**Status**: âœ… PASSED  
**Duration**: 1731 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:02  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-changes -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:01.2130|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:01.2259|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:01.2259|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:01.2259|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:01.2259|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:01.2259|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-changes -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_cdc-changes_20251216_081123.json
2025-12-16 08:14:01.2259|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:01.2259|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:01.2372|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:01.2372|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:01.2683|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:01.2683|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:01.2683|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:01.2683|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:01.2683|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:01.2838|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:01.2838|DEBUG|WindowsDb2Editor.Services.MetadataHand
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "changesFound": 0,
  "note": "CDC changes require reading CDC change tables (configured per table)",
  "retrievedAt": "2025-12-16T08:14:02.4058083+01:00"
}
```

---

### âœ… Test #79: cdc-enable

**Category**: CDC Operations  
**Description**: Enable CDC for table  
**Status**: âœ… PASSED  
**Duration**: 1712 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:04  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-enable -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:02.9268|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:02.9411|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:02.9411|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:02.9411|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:02.9411|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:02.9411|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-enable -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_cdc-enable_20251216_081123.json
2025-12-16 08:14:02.9411|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:02.9411|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:02.9411|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:02.9571|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:02.9896|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:02.9901|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:02.9901|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:02.9901|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:02.9901|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:02.9901|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:02.9901|DEBUG|WindowsDb2Editor.Services.MetadataHandle
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
**Duration**: 1736 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:05  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-disable -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:04.6452|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:04.6583|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:04.6583|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:04.6583|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:04.6583|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:04.6583|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-disable -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_cdc-disable_20251216_081123.json
2025-12-16 08:14:04.6583|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:04.6583|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:04.6583|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:04.6583|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:04.6997|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:04.6997|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:04.6997|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:04.6997|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:04.7117|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:04.7117|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:04.7117|DEBUG|WindowsDb2Editor.Services.MetadataHand
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
**Duration**: 1748 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:07  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-history -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:06.4330|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:06.4470|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:06.4470|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:06.4470|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:06.4470|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:06.4470|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-history -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_cdc-history_20251216_081123.json
2025-12-16 08:14:06.4552|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:06.4552|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:06.4552|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:06.4552|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:06.4871|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:06.4871|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:06.4871|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:06.4871|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:06.4871|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:06.4871|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:06.5021|DEBUG|WindowsDb2Editor.Services.MetadataHand
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
**Duration**: 2161 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:09  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-compare -profile FKKTOTST -object INL -schema INL_TEST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:08.3517|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:08.3673|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:08.3673|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:08.3673|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:08.3673|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:08.3673|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-compare -profile FKKTOTST -object INL -schema INL_TEST -format json -Outfile CLI_Test_Output/temp_db-compare_20251216_081123.json
2025-12-16 08:14:08.3673|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:08.3775|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:08.3775|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:08.3775|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:08.4152|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:08.4152|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:08.4152|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:08.4152|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:08.4268|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:08.4268|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:08.4268|DEBUG|WindowsDb2Editor.Services.Metad
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
  "timestamp": "2025-12-16T08:14:09.7840873+01:00"
}
```

---

### âœ… Test #83: db-compare-source-only

**Category**: Database Comparison  
**Description**: Show objects only in source  
**Status**: âœ… PASSED  
**Duration**: 1770 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:11  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-compare-source-only -profile FKKTOTST -object INL -schema INL_TEST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:10.3399|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:10.3534|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:10.3637|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:10.3637|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:10.3637|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:10.3637|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-compare-source-only -profile FKKTOTST -object INL -schema INL_TEST -format json -Outfile CLI_Test_Output/temp_db-compare-source-only_20251216_081123.json
2025-12-16 08:14:10.3637|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:10.3637|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:10.3637|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:10.3790|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:10.4177|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:10.4177|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:10.4177|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:10.4177|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:10.4298|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:10.4298|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:10.4298|DEBUG|Windows
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
**Duration**: 1758 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:13  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-compare-target-only -profile FKKTOTST -object INL -schema INL_TEST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:12.0939|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:12.1096|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:12.1096|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:12.1168|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:12.1168|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:12.1168|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-compare-target-only -profile FKKTOTST -object INL -schema INL_TEST -format json -Outfile CLI_Test_Output/temp_db-compare-target-only_20251216_081123.json
2025-12-16 08:14:12.1168|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:12.1168|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:12.1168|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:12.1282|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:12.1595|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:12.1595|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:12.1595|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:12.1595|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:12.1595|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:12.1769|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:12.1769|DEBUG|Windows
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
**Duration**: 2013 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:15  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-compare-different -profile FKKTOTST -object INL -schema INL_TEST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:13.8782|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:13.8782|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:13.8782|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:13.8782|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:13.8782|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:13.8926|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-compare-different -profile FKKTOTST -object INL -schema INL_TEST -format json -Outfile CLI_Test_Output/temp_db-compare-different_20251216_081123.json
2025-12-16 08:14:13.8926|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:13.8926|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:13.8926|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:13.8926|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:13.9287|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:13.9287|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:13.9287|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:13.9287|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:13.9287|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:13.9402|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:13.9402|DEBUG|WindowsDb2E
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
**Duration**: 1651 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:17  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-compare-ddl -profile FKKTOTST -object INL -schema INL_TEST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:15.8463|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:15.8463|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:15.8463|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:15.8463|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:15.8463|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:15.8463|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-compare-ddl -profile FKKTOTST -object INL -schema INL_TEST -format json -Outfile CLI_Test_Output/temp_db-compare-ddl_20251216_081123.json
2025-12-16 08:14:15.8463|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:15.8618|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:15.8618|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:15.8618|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:15.8933|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:15.8933|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:15.8933|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:15.8933|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:15.8933|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:15.8933|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:15.9084|DEBUG|WindowsDb2Editor.Servic
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "db-compare-ddl",
  "sourceSchema": "INL",
  "targetSchema": "INL_TEST",
  "ddl": "-- ALTER TABLE statements placeholder\n-- Use DatabaseComparisonService for full implementation",
  "timestamp": "2025-12-16T08:14:16.9785028+01:00"
}
```

---

### âœ… Test #87: object-metadata

**Category**: Metadata & Advanced  
**Description**: Get object metadata  
**Status**: âœ… PASSED  
**Duration**: 1755 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:18  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command object-metadata -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:17.5364|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:17.5364|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:17.5364|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:17.5530|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:17.5530|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:17.5530|DEBUG|WindowsDb2Editor.App|Startup arguments: -command object-metadata -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_object-metadata_20251216_081123.json
2025-12-16 08:14:17.5530|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:17.5530|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:17.5530|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:17.5530|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:17.5868|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:17.5868|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:17.5868|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:17.5868|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:17.5989|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:17.5989|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:17.5989|DEBUG|WindowsDb2Editor.Services.Meta
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
**Duration**: 1783 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:20  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command object-search -profile FKKTOTST -object BILAG% -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:19.2413|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:19.2552|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:19.2552|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:19.2552|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:19.2552|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:19.2552|DEBUG|WindowsDb2Editor.App|Startup arguments: -command object-search -profile FKKTOTST -object BILAG% -format json -Outfile CLI_Test_Output/temp_object-search_20251216_081123.json
2025-12-16 08:14:19.2552|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:19.2552|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:19.2701|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:19.2701|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:19.3044|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:19.3044|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:19.3044|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:19.3044|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:19.3160|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:19.3160|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:19.3160|DEBUG|WindowsDb2Editor.Services.MetadataHandl
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
**Duration**: 1646 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:22  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command column-search -profile FKKTOTST -object BILAG% -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:21.0359|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:21.0359|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:21.0486|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:21.0486|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:21.0486|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:21.0486|DEBUG|WindowsDb2Editor.App|Startup arguments: -command column-search -profile FKKTOTST -object BILAG% -format json -Outfile CLI_Test_Output/temp_column-search_20251216_081123.json
2025-12-16 08:14:21.0486|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:21.0486|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:21.0486|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:21.0486|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:21.0835|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:21.0847|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:21.0847|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:21.0847|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:21.0847|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:21.0972|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:21.0972|DEBUG|WindowsDb2Editor.Services.MetadataHandl
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
**Duration**: 1664 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:23  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command dependency-graph -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:22.6816|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:22.6943|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:22.6943|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:22.6943|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:22.6943|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:22.6943|DEBUG|WindowsDb2Editor.App|Startup arguments: -command dependency-graph -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_dependency-graph_20251216_081123.json
2025-12-16 08:14:22.6943|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:22.6943|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:22.6943|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:22.7071|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:22.7332|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:22.7332|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:22.7332|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:22.7366|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:22.7366|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:22.7366|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:22.7366|DEBUG|WindowsDb2Editor.Services.Me
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
  "retrievedAt": "2025-12-16T08:14:23.8456206+01:00"
}
```

---

### âœ… Test #91: query-history

**Category**: Metadata & Advanced  
**Description**: Show query history  
**Status**: âœ… PASSED  
**Duration**: 1829 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:25  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command query-history -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:24.3517|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:24.3645|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:24.3645|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:24.3645|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:24.3645|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:24.3645|DEBUG|WindowsDb2Editor.App|Startup arguments: -command query-history -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_query-history_20251216_081123.json
2025-12-16 08:14:24.3645|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:24.3645|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:24.3645|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:24.3645|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:24.4054|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:24.4054|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:24.4054|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:24.4054|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:24.4090|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:24.4090|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:24.4090|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading meta
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "note": "Query history is session-based and not available in CLI mode",
  "sessionMode": "CLI",
  "historyAvailable": false,
  "retrievedAt": "2025-12-16T08:14:25.6832019+01:00"
}
```

---

### âœ… Test #92: schema-summary

**Category**: Metadata & Advanced  
**Description**: Get schema summary  
**Status**: âœ… PASSED  
**Duration**: 1723 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:27  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command schema-summary -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:26.2051|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:26.2051|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:26.2051|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:26.2051|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:26.2051|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:26.2208|DEBUG|WindowsDb2Editor.App|Startup arguments: -command schema-summary -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_schema-summary_20251216_081123.json
2025-12-16 08:14:26.2208|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:26.2208|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:26.2208|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:26.2208|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:26.2550|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:26.2550|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:26.2550|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:26.2596|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:26.2596|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:26.2678|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:26.2678|DEBUG|WindowsDb2Editor.Services.MetadataHandle
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
**Duration**: 1801 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:29  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command database-summary -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:27.8829|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:27.8951|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:27.8951|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:27.8951|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:27.8951|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:27.8951|DEBUG|WindowsDb2Editor.App|Startup arguments: -command database-summary -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_database-summary_20251216_081123.json
2025-12-16 08:14:27.8951|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:27.8951|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:27.9091|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:27.9091|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:27.9335|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:27.9335|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:27.9335|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:27.9335|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:27.9434|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:27.9434|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:27.9434|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
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
**Duration**: 1614 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:30  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command health-check -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:29.7261|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:29.7381|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:29.7381|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:29.7381|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:29.7381|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:29.7381|DEBUG|WindowsDb2Editor.App|Startup arguments: -command health-check -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_health-check_20251216_081123.json
2025-12-16 08:14:29.7381|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:29.7381|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:29.7381|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:29.7525|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:29.7770|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:29.7770|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:29.7770|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:29.7770|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:29.7884|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:29.7884|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:29.7884|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metada
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
**Duration**: 1595 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:32  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-query -profile FKKTOTST -prompt Show all invoices from last month -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:31.3421|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:31.3542|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:31.3542|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:31.3542|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:31.3542|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:31.3542|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-query -profile FKKTOTST -prompt Show all invoices from last month -format json -Outfile CLI_Test_Output/temp_ai-query_20251216_081123.json
2025-12-16 08:14:31.3542|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:31.3542|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:31.3682|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:31.3682|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:31.3964|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:31.3964|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:31.3964|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:31.3964|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:31.3964|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:31.4124|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:31.4124|DEBUG|WindowsDb2Editor.Servi
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
  "timestamp": "2025-12-16T08:14:32.428007+01:00"
}
```

---

### âœ… Test #96: ai-explain-table

**Category**: AI Features  
**Description**: AI explanation of table  
**Status**: âœ… PASSED  
**Duration**: 1622 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:34  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-explain-table -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:32.9115|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:32.9255|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:32.9255|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:32.9255|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:32.9255|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:32.9255|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-explain-table -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_ai-explain-table_20251216_081123.json
2025-12-16 08:14:32.9255|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:32.9255|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:32.9255|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:32.9255|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:32.9652|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:32.9652|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:32.9652|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:32.9652|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:32.9739|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:32.9739|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:32.9739|DEBUG|WindowsDb2Editor.Services.Me
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
  "timestamp": "2025-12-16T08:14:34.0308133+01:00"
}
```

---

### âœ… Test #97: ai-explain-view

**Category**: AI Features  
**Description**: AI explanation of view  
**Status**: âœ… PASSED  
**Duration**: 1725 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:35  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-explain-view -profile FKKTOTST -object INL.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:34.6166|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:34.6279|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:34.6279|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:34.6279|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:34.6279|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:34.6279|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-explain-view -profile FKKTOTST -object INL.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_ai-explain-view_20251216_081123.json
2025-12-16 08:14:34.6279|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:34.6279|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:34.6279|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:34.6279|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:34.6616|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:34.6616|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:34.6616|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:34.6616|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:34.6713|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:34.6754|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:34.6754|DEBUG|WindowsDb2Editor.Services.Me
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "ai-explain-view",
  "view": "INL.SYSDUMMY1",
  "explanation": "AI analysis placeholder",
  "timestamp": "2025-12-16T08:14:35.7765262+01:00"
}
```

---

### âœ… Test #98: ai-analyze-procedure

**Category**: AI Features  
**Description**: AI analysis of procedure  
**Status**: âœ… PASSED  
**Duration**: 1727 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:37  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-analyze-procedure -profile FKKTOTST -object INL.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:36.2922|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:36.3063|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:36.3063|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:36.3063|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:36.3063|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:36.3063|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-analyze-procedure -profile FKKTOTST -object INL.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_ai-analyze-procedure_20251216_081123.json
2025-12-16 08:14:36.3063|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:36.3150|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:36.3150|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:36.3150|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:36.3482|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:36.3482|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:36.3482|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:36.3482|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:36.3482|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:36.3668|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:36.3668|DEBUG|WindowsDb2Editor.S
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "ai-analyze-procedure",
  "procedure": "INL.ADMIN_CMD",
  "analysis": "AI code analysis placeholder",
  "timestamp": "2025-12-16T08:14:37.5054094+01:00"
}
```

---

### âœ… Test #99: ai-analyze-function

**Category**: AI Features  
**Description**: AI analysis of function  
**Status**: âœ… PASSED  
**Duration**: 1703 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:39  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-analyze-function -profile FKKTOTST -object INL.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:38.0547|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:38.0673|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:38.0673|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:38.0673|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:38.0673|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:38.0673|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-analyze-function -profile FKKTOTST -object INL.ABS -format json -Outfile CLI_Test_Output/temp_ai-analyze-function_20251216_081123.json
2025-12-16 08:14:38.0673|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:38.0673|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:38.0811|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:38.0811|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:38.1167|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:38.1167|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:38.1167|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:38.1167|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:38.1339|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:38.1339|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:38.1339|DEBUG|WindowsDb2Editor.Services.
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "ai-analyze-function",
  "function": "INL.ABS",
  "analysis": "AI code analysis placeholder",
  "timestamp": "2025-12-16T08:14:39.2178456+01:00"
}
```

---

### âœ… Test #100: ai-analyze-package

**Category**: AI Features  
**Description**: AI analysis of package  
**Status**: âœ… PASSED  
**Duration**: 1621 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:40  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-analyze-package -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:39.7092|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:39.7214|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:39.7214|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:39.7214|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:39.7214|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:39.7214|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-analyze-package -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_ai-analyze-package_20251216_081123.json
2025-12-16 08:14:39.7214|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:39.7214|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:39.7214|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:39.7214|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:39.7545|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:39.7545|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:39.7545|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:39.7545|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:39.7545|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:39.7689|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:39.7689|DEBUG|WindowsDb2Editor.Servic
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "ai-analyze-package",
  "package": "INL.SYSSH200",
  "analysis": "AI package analysis placeholder",
  "timestamp": "2025-12-16T08:14:40.8464208+01:00"
}
```

---

### âœ… Test #101: ai-deep-analysis

**Category**: AI Features  
**Description**: Deep AI analysis of table  
**Status**: âœ… PASSED  
**Duration**: 1550 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 08:14:42  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-deep-analysis -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 08:14:41.3175|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 08:14:41.3175|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 08:14:41.3175|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 08:14:41.3175|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 08:14:41.3175|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 08:14:41.3175|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-deep-analysis -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_ai-deep-analysis_20251216_081123.json
2025-12-16 08:14:41.3175|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 08:14:41.3175|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 08:14:41.3362|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 08:14:41.3362|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 08:14:41.3642|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 08:14:41.3642|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 08:14:41.3642|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 08:14:41.3642|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 08:14:41.3642|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 08:14:41.3788|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 08:14:41.3788|DEBUG|WindowsDb2Editor.Services.Me
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
  "timestamp": "2025-12-16T08:14:42.3971363+01:00"
}
```

---

## Test Environment

- **Executable**: bin\Debug\net10.0-windows\WindowsDb2Editor.exe
- **Test Timestamp**: 20251216_081123
- **PowerShell Version**: 5.1.26100.7019
- **Test Script**: _cli_test_with_io_capture.ps1

---

*Report generated automatically by WindowsDb2Editor CLI Test Suite*
