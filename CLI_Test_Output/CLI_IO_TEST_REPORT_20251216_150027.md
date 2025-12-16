# CLI Test Report with Input/Output Capture

**Generated**: 2025-12-16 15:02:56  
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
| Test Duration | ~148.3 seconds |

---

## Results by Category

### âœ…  (101/101 passed)

| # | Command | Description | Status | Duration | Exit Code |
|---|---------|-------------|--------|----------|-----------|| 1 | `connection-test` | Test database connection | âœ… PASS | 2238ms | 0 |
| 2 | `connection-stats` | Get connection statistics | âœ… PASS | 1812ms | 0 |
| 3 | `help-all` | Show all available commands | âœ… PASS | 923ms | 0 |
| 4 | `cli-version` | Show CLI version | âœ… PASS | 519ms | 0 |
| 5 | `db-config` | Get database configuration | âœ… PASS | 1647ms | 0 |
| 6 | `connection-history` | Show connection history | âœ… PASS | 1378ms | 0 |
| 7 | `list-schemas` | List all schemas | âœ… PASS | 1413ms | 0 |
| 8 | `list-tables` | List tables in schema | âœ… PASS | 1527ms | 0 |
| 9 | `list-views` | List views in schema | âœ… PASS | 1403ms | 0 |
| 10 | `list-procedures` | List procedures in schema | âœ… PASS | 1524ms | 0 |
| 11 | `list-functions` | List functions in schema | âœ… PASS | 1448ms | 0 |
| 12 | `list-packages` | List packages in schema | âœ… PASS | 1510ms | 0 |
| 13 | `list-tablespaces` | List all tablespaces | âœ… PASS | 1453ms | 0 |
| 14 | `list-all-indexes` | List all indexes in schema | âœ… PASS | 1435ms | 0 |
| 15 | `list-constraints` | List constraints in schema | âœ… PASS | 1451ms | 0 |
| 16 | `list-sequences` | List sequences in schema | âœ… PASS | 1382ms | 0 |
| 17 | `table-properties` | Get table properties | âœ… PASS | 1629ms | 0 |
| 18 | `table-columns` | List table columns | âœ… PASS | 1479ms | 0 |
| 19 | `table-indexes` | List table indexes | âœ… PASS | 1417ms | 0 |
| 20 | `table-foreignkeys` | List table foreign keys | âœ… PASS | 1349ms | 0 |
| 21 | `table-ddl` | Generate table DDL | âœ… PASS | 1496ms | 0 |
| 22 | `table-statistics` | Get table statistics | âœ… PASS | 1461ms | 0 |
| 23 | `table-relationships` | Show table relationships | âœ… PASS | 1449ms | 0 |
| 24 | `table-sample-data` | Get sample data from table | âœ… PASS | 1434ms | 0 |
| 25 | `table-size` | Get table size | âœ… PASS | 1392ms | 0 |
| 26 | `table-grants` | List table grants | âœ… PASS | 1486ms | 0 |
| 27 | `table-dependencies` | Show table dependencies | âœ… PASS | 1400ms | 0 |
| 28 | `table-incoming-fk` | List incoming foreign keys | âœ… PASS | 1482ms | 0 |
| 29 | `table-used-by-packages` | List packages using this table | âœ… PASS | 1385ms | 0 |
| 30 | `table-used-by-views` | List views using this table | âœ… PASS | 1450ms | 0 |
| 31 | `table-used-by-routines` | List routines using this table | âœ… PASS | 1513ms | 0 |
| 32 | `view-properties` | Get view properties | âœ… PASS | 1490ms | 0 |
| 33 | `view-definition` | Get view definition SQL | âœ… PASS | 1441ms | 0 |
| 34 | `view-columns` | List view columns | âœ… PASS | 1466ms | 0 |
| 35 | `view-dependencies` | Show view dependencies | âœ… PASS | 1435ms | 0 |
| 36 | `view-sample-data` | Get sample data from view | âœ… PASS | 1432ms | 0 |
| 37 | `view-used-by-packages` | List packages using this view | âœ… PASS | 1538ms | 0 |
| 38 | `view-used-by-views` | List views using this view | âœ… PASS | 1461ms | 0 |
| 39 | `procedure-properties` | Get procedure properties | âœ… PASS | 1508ms | 0 |
| 40 | `procedure-source` | Get procedure source code | âœ… PASS | 1443ms | 0 |
| 41 | `procedure-parameters` | List procedure parameters | âœ… PASS | 1684ms | 0 |
| 42 | `procedure-dependencies` | Show procedure dependencies | âœ… PASS | 1505ms | 0 |
| 43 | `procedure-usage` | Show procedure usage | âœ… PASS | 1465ms | 0 |
| 44 | `procedure-grants` | List procedure grants | âœ… PASS | 1461ms | 0 |
| 45 | `function-properties` | Get function properties | âœ… PASS | 1462ms | 0 |
| 46 | `function-source` | Get function source code | âœ… PASS | 1439ms | 0 |
| 47 | `function-parameters` | List function parameters | âœ… PASS | 1551ms | 0 |
| 48 | `function-dependencies` | Show function dependencies | âœ… PASS | 1444ms | 0 |
| 49 | `function-usage` | Show function usage | âœ… PASS | 1444ms | 0 |
| 50 | `function-grants` | List function grants | âœ… PASS | 1338ms | 0 |
| 51 | `package-properties` | Get package properties | âœ… PASS | 1507ms | 0 |
| 52 | `package-statements` | List package statements | âœ… PASS | 1475ms | 0 |
| 53 | `package-dependencies` | Show package dependencies | âœ… PASS | 1491ms | 0 |
| 54 | `package-statistics` | Get package statistics | âœ… PASS | 1474ms | 0 |
| 55 | `package-list-tables` | List tables used by package | âœ… PASS | 1460ms | 0 |
| 56 | `package-list-views` | List views used by package | âœ… PASS | 1433ms | 0 |
| 57 | `package-list-procedures` | List procedures used by package | âœ… PASS | 1427ms | 0 |
| 58 | `package-list-functions` | List functions used by package | âœ… PASS | 1458ms | 0 |
| 59 | `user-properties` | Get user properties | âœ… PASS | 1352ms | 0 |
| 60 | `user-privileges` | List user privileges | âœ… PASS | 1337ms | 0 |
| 61 | `user-tables` | List user's tables | âœ… PASS | 1436ms | 0 |
| 62 | `user-schemas` | List user's schemas | âœ… PASS | 1457ms | 0 |
| 63 | `user-connections` | List user connections | âœ… PASS | 1426ms | 0 |
| 64 | `db-load` | Get database load | âœ… PASS | 1740ms | 0 |
| 65 | `db-locks` | Show database locks | âœ… PASS | 2351ms | 0 |
| 66 | `db-sessions` | List database sessions | âœ… PASS | 1444ms | 0 |
| 67 | `db-size` | Get database size | âœ… PASS | 1537ms | 0 |
| 68 | `schema-size` | Get schema size | âœ… PASS | 1460ms | 0 |
| 69 | `db-version` | Get database version | âœ… PASS | 1572ms | 0 |
| 70 | `db-parameters` | List database parameters | âœ… PASS | 1442ms | 0 |
| 71 | `db-registry` | Show database registry | âœ… PASS | 1588ms | 0 |
| 72 | `active-queries` | List active queries | âœ… PASS | 1450ms | 0 |
| 73 | `bufferpool-stats` | Get buffer pool statistics | âœ… PASS | 1662ms | 0 |
| 74 | `tablespace-usage` | Show tablespace usage | âœ… PASS | 1640ms | 0 |
| 75 | `cdc-status` | Get CDC status for schema | âœ… PASS | 1441ms | 0 |
| 76 | `cdc-status-full` | Get full CDC status | âœ… PASS | 1565ms | 0 |
| 77 | `cdc-configuration` | Get CDC configuration for table | âœ… PASS | 1367ms | 0 |
| 78 | `cdc-changes` | List CDC changes | âœ… PASS | 1492ms | 0 |
| 79 | `cdc-enable` | Enable CDC for table | âœ… PASS | 1367ms | 0 |
| 80 | `cdc-disable` | Disable CDC for table | âœ… PASS | 1448ms | 0 |
| 81 | `cdc-history` | Show CDC history | âœ… PASS | 1402ms | 0 |
| 82 | `db-compare` | Compare two schemas | âœ… PASS | 1458ms | 0 |
| 83 | `db-compare-source-only` | Show objects only in source | âœ… PASS | 1412ms | 0 |
| 84 | `db-compare-target-only` | Show objects only in target | âœ… PASS | 1397ms | 0 |
| 85 | `db-compare-different` | Show different objects | âœ… PASS | 1498ms | 0 |
| 86 | `db-compare-ddl` | Generate comparison DDL | âœ… PASS | 1411ms | 0 |
| 87 | `object-metadata` | Get object metadata | âœ… PASS | 1418ms | 0 |
| 88 | `object-search` | Search for objects | âœ… PASS | 1436ms | 0 |
| 89 | `column-search` | Search for columns | âœ… PASS | 1375ms | 0 |
| 90 | `dependency-graph` | Generate dependency graph | âœ… PASS | 1434ms | 0 |
| 91 | `query-history` | Show query history | âœ… PASS | 1311ms | 0 |
| 92 | `schema-summary` | Get schema summary | âœ… PASS | 1582ms | 0 |
| 93 | `database-summary` | Get database summary | âœ… PASS | 1476ms | 0 |
| 94 | `health-check` | Run health check | âœ… PASS | 1377ms | 0 |
| 95 | `ai-query` | AI-assisted query generation | âœ… PASS | 1400ms | 0 |
| 96 | `ai-explain-table` | AI explanation of table | âœ… PASS | 1362ms | 0 |
| 97 | `ai-explain-view` | AI explanation of view | âœ… PASS | 1517ms | 0 |
| 98 | `ai-analyze-procedure` | AI analysis of procedure | âœ… PASS | 1474ms | 0 |
| 99 | `ai-analyze-function` | AI analysis of function | âœ… PASS | 1527ms | 0 |
| 100 | `ai-analyze-package` | AI analysis of package | âœ… PASS | 1448ms | 0 |
| 101 | `ai-deep-analysis` | Deep AI analysis of table | âœ… PASS | 1391ms | 0 |

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
**Duration**: 2238 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:30  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command connection-test -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:28.8367|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:28.8470|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:28.8470|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:28.8470|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:28.8470|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:28.8470|DEBUG|WindowsDb2Editor.App|Startup arguments: -command connection-test -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_connection-test_20251216_150027.json
2025-12-16 15:00:28.8470|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:28.8470|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:28.8470|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:28.8470|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:28.8781|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:28.8781|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:28.8781|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:28.8781|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:28.8781|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:28.8886|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:28.8886|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading 
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "connection-test",
  "profileName": "FKKTOTST",
  "testResult": "SUCCESS",
  "message": "Connection opened successfully",
  "retrievedAt": "2025-12-16T15:00:30.141994+01:00"
}
```

---

### âœ… Test #2: connection-stats

**Category**: Connection & Info  
**Description**: Get connection statistics  
**Status**: âœ… PASSED  
**Duration**: 1812 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:32  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command connection-stats -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:30.8942|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:30.9077|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:30.9077|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:30.9077|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:30.9077|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:30.9077|DEBUG|WindowsDb2Editor.App|Startup arguments: -command connection-stats -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_connection-stats_20251216_150027.json
2025-12-16 15:00:30.9077|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:30.9077|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:30.9077|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:30.9077|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:30.9337|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:30.9346|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:30.9346|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:30.9346|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:30.9346|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:30.9346|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:30.9346|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "totalProfiles": 2,
  "note": "Connection stats based on saved profiles",
  "retrievedAt": "2025-12-16T15:00:31.9905517+01:00"
}
```

---

### âœ… Test #3: help-all

**Category**: Connection & Info  
**Description**: Show all available commands  
**Status**: âœ… PASSED  
**Duration**: 923 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:32  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command help-all -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:32.7091|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:32.7212|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:32.7212|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:32.7212|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:32.7212|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:32.7212|DEBUG|WindowsDb2Editor.App|Startup arguments: -command help-all -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_help-all_20251216_150027.json
2025-12-16 15:00:32.7212|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:32.7212|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:32.7212|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:32.7212|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:32.7674|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:32.7674|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:32.7674|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:32.7714|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:32.7797|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:32.7797|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:32.7797|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for D
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
**Duration**: 519 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:33  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cli-version -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:33.3192|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:33.3192|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:33.3312|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:33.3312|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:33.3312|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:33.3312|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cli-version -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_cli-version_20251216_150027.json
2025-12-16 15:00:33.3312|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:33.3312|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:33.3312|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:33.3312|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:33.3559|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:33.3559|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:33.3559|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:33.3559|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:33.3621|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:33.3621|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:33.3621|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata
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
  "retrievedAt": "2025-12-16T15:00:33.428991+01:00"
}
```

---

### âœ… Test #5: db-config

**Category**: Connection & Info  
**Description**: Get database configuration  
**Status**: âœ… PASSED  
**Duration**: 1647 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:35  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-config -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:33.8432|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:33.8432|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:33.8432|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:33.8432|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:33.8570|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:33.8570|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-config -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-config_20251216_150027.json
2025-12-16 15:00:33.8570|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:33.8570|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:33.8570|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:33.8570|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:33.8802|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:33.8802|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:33.8802|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:33.8802|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:33.8802|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:33.8895|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:33.8895|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for
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
**Duration**: 1378 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:36  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command connection-history -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:35.4878|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:35.4878|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:35.4878|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:35.4878|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:35.4878|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:35.4878|DEBUG|WindowsDb2Editor.App|Startup arguments: -command connection-history -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_connection-history_20251216_150027.json
2025-12-16 15:00:35.4878|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:35.5011|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:35.5011|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:35.5011|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:35.5329|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:35.5329|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:35.5329|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:35.5329|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:35.5329|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:35.5329|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:35.5329|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Lo
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
**Duration**: 1413 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:37  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-schemas -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:36.8607|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:36.8607|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:36.8607|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:36.8740|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:36.8740|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:36.8740|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-schemas -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_list-schemas_20251216_150027.json
2025-12-16 15:00:36.8740|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:36.8740|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:36.8740|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:36.8740|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:36.8994|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:36.8994|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:36.8994|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:36.8994|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:36.8994|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:36.8994|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:36.8994|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metada
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
**Duration**: 1527 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:39  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-tables -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:38.2879|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:38.2879|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:38.2879|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:38.2879|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:38.2879|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:38.2879|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-tables -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-tables_20251216_150027.json
2025-12-16 15:00:38.2879|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:38.2879|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:38.3037|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:38.3037|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:38.3252|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:38.3252|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:38.3252|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:38.3252|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:38.3316|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:38.3316|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:38.3316|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Load
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "totalTables": 0,
  "limitApplied": 0,
  "tables": [],
  "retrievedAt": "2025-12-16T15:00:39.3915663+01:00"
}
```

---

### âœ… Test #9: list-views

**Category**: Schema Operations  
**Description**: List views in schema  
**Status**: âœ… PASSED  
**Duration**: 1403 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:40  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-views -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:39.8328|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:39.8424|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:39.8424|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:39.8424|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:39.8424|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:39.8424|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-views -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-views_20251216_150027.json
2025-12-16 15:00:39.8424|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:39.8424|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:39.8424|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:39.8424|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:39.8682|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:39.8682|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:39.8682|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:39.8682|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:39.8769|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:39.8769|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:39.8769|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "totalViews": 0,
  "limitApplied": 0,
  "views": [],
  "retrievedAt": "2025-12-16T15:00:40.8091726+01:00"
}
```

---

### âœ… Test #10: list-procedures

**Category**: Schema Operations  
**Description**: List procedures in schema  
**Status**: âœ… PASSED  
**Duration**: 1524 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:42  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-procedures -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:41.2662|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:41.2662|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:41.2662|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:41.2662|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:41.2662|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:41.2662|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-procedures -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-procedures_20251216_150027.json
2025-12-16 15:00:41.2662|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:41.2662|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:41.2806|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:41.2806|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:41.3000|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:41.3000|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:41.3000|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:41.3000|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:41.3000|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:41.3000|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:41.3000|DEBUG|WindowsDb2Editor.Services.MetadataHand
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "totalProcedures": 0,
  "limitApplied": 0,
  "procedures": [],
  "retrievedAt": "2025-12-16T15:00:42.3336447+01:00"
}
```

---

### âœ… Test #11: list-functions

**Category**: Schema Operations  
**Description**: List functions in schema  
**Status**: âœ… PASSED  
**Duration**: 1448 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:43  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-functions -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:42.7700|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:42.7700|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:42.7700|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:42.7700|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:42.7700|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:42.7700|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-functions -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-functions_20251216_150027.json
2025-12-16 15:00:42.7700|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:42.7700|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:42.7700|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:42.7700|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:42.8031|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:42.8031|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:42.8044|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:42.8044|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:42.8044|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:42.8044|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:42.8044|DEBUG|WindowsDb2Editor.Services.MetadataHandle
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "totalFunctions": 0,
  "limitApplied": 0,
  "functions": [],
  "retrievedAt": "2025-12-16T15:00:43.7722797+01:00"
}
```

---

### âœ… Test #12: list-packages

**Category**: Schema Operations  
**Description**: List packages in schema  
**Status**: âœ… PASSED  
**Duration**: 1510 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:45  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-packages -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:44.2306|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:44.2396|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:44.2396|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:44.2396|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:44.2396|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:44.2396|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-packages -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-packages_20251216_150027.json
2025-12-16 15:00:44.2396|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:44.2396|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:44.2396|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:44.2396|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:44.2692|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:44.2703|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:44.2703|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:44.2703|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:44.2703|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:44.2703|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:44.2703|DEBUG|WindowsDb2Editor.Services.MetadataHandler|
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "totalPackages": 0,
  "limitApplied": 0,
  "packages": [],
  "retrievedAt": "2025-12-16T15:00:45.2958313+01:00"
}
```

---

### âœ… Test #13: list-tablespaces

**Category**: Schema Operations  
**Description**: List all tablespaces  
**Status**: âœ… PASSED  
**Duration**: 1453 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:46  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-tablespaces -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:45.7494|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:45.7494|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:45.7494|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:45.7494|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:45.7494|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:45.7494|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-tablespaces -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_list-tablespaces_20251216_150027.json
2025-12-16 15:00:45.7494|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:45.7494|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:45.7494|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:45.7658|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:45.7840|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:45.7840|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:45.7840|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:45.7840|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:45.7840|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:45.7840|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:45.7840|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
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
**Duration**: 1435 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:48  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-all-indexes -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:47.1775|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:47.1918|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:47.1918|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:47.1918|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:47.1918|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:47.1918|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-all-indexes -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-all-indexes_20251216_150027.json
2025-12-16 15:00:47.1918|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:47.1918|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:47.1918|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:47.1918|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:47.2219|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:47.2219|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:47.2219|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:47.2219|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:47.2219|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:47.2219|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:47.2219|DEBUG|WindowsDb2Editor.Services.MetadataHa
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
  "retrievedAt": "2025-12-16T15:00:48.1958428+01:00"
}
```

---

### âœ… Test #15: list-constraints

**Category**: Schema Operations  
**Description**: List constraints in schema  
**Status**: âœ… PASSED  
**Duration**: 1451 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:49  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-constraints -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:48.6220|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:48.6307|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:48.6307|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:48.6307|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:48.6307|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:48.6307|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-constraints -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-constraints_20251216_150027.json
2025-12-16 15:00:48.6307|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:48.6307|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:48.6307|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:48.6307|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:48.6586|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:48.6586|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:48.6586|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:48.6586|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:48.6586|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:48.6586|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:48.6586|DEBUG|WindowsDb2Editor.Services.MetadataHa
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
**Duration**: 1382 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:51  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command list-sequences -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:50.0506|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:50.0506|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:50.0506|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:50.0506|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:50.0506|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:50.0641|DEBUG|WindowsDb2Editor.App|Startup arguments: -command list-sequences -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_list-sequences_20251216_150027.json
2025-12-16 15:00:50.0641|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:50.0641|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:50.0641|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:50.0641|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:50.0868|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:50.0868|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:50.0868|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:50.0868|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:50.0868|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:50.0955|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:50.0955|DEBUG|WindowsDb2Editor.Services.MetadataHandle
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "list-sequences",
  "sequenceCount": 0,
  "sequences": [],
  "retrievedAt": "2025-12-16T15:00:51.0368268+01:00"
}
```

---

### âœ… Test #17: table-properties

**Category**: Table Operations  
**Description**: Get table properties  
**Status**: âœ… PASSED  
**Duration**: 1629 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:52  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-properties -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:51.4400|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:51.4513|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:51.4513|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:51.4513|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:51.4513|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:51.4513|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-properties -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-properties_20251216_150027.json
2025-12-16 15:00:51.4513|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:51.4513|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:51.4513|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:51.4513|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:51.4774|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:51.4774|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:51.4774|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:51.4774|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:51.4774|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:51.4880|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:51.4880|DEBUG|WindowsDb2Editor.Services.Me
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
  "retrievedAt": "2025-12-16T15:00:52.6658962+01:00"
}
```

---

### âœ… Test #18: table-columns

**Category**: Table Operations  
**Description**: List table columns  
**Status**: âœ… PASSED  
**Duration**: 1479 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:54  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-columns -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:53.1043|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:53.1133|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:53.1133|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:53.1133|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:53.1133|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:53.1133|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-columns -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-columns_20251216_150027.json
2025-12-16 15:00:53.1133|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:53.1133|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:53.1133|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:53.1133|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:53.1413|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:53.1413|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:53.1413|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:53.1413|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:53.1413|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:53.1413|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:53.1413|DEBUG|WindowsDb2Editor.Services.Metadata
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
  "retrievedAt": "2025-12-16T15:00:54.1470942+01:00"
}
```

---

### âœ… Test #19: table-indexes

**Category**: Table Operations  
**Description**: List table indexes  
**Status**: âœ… PASSED  
**Duration**: 1417 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:55  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-indexes -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:54.5998|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:54.5998|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:54.5998|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:54.5998|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:54.5998|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:54.5998|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-indexes -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-indexes_20251216_150027.json
2025-12-16 15:00:54.5998|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:54.5998|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:54.6145|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:54.6145|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:54.6145|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:54.6351|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:54.6351|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:54.6351|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:54.6351|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:54.6445|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:54.6445|DEBUG|WindowsDb2Editor.Services.Metadata
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
  "retrievedAt": "2025-12-16T15:00:55.5678497+01:00"
}
```

---

### âœ… Test #20: table-foreignkeys

**Category**: Table Operations  
**Description**: List table foreign keys  
**Status**: âœ… PASSED  
**Duration**: 1349 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:56  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-foreignkeys -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:55.9820|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:55.9907|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:55.9907|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:55.9907|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:55.9907|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:55.9986|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-foreignkeys -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-foreignkeys_20251216_150027.json
2025-12-16 15:00:55.9986|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:55.9986|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:55.9986|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:55.9986|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:56.0253|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:56.0253|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:56.0253|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:56.0253|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:56.0253|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:56.0253|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:56.0253|DEBUG|WindowsDb2Editor.Services.
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
  "retrievedAt": "2025-12-16T15:00:56.9188102+01:00"
}
```

---

### âœ… Test #21: table-ddl

**Category**: Table Operations  
**Description**: Generate table DDL  
**Status**: âœ… PASSED  
**Duration**: 1496 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:58  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-ddl -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:57.3758|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:57.3758|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:57.3758|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:57.3758|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:57.3758|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:57.3758|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-ddl -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-ddl_20251216_150027.json
2025-12-16 15:00:57.3758|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:57.3758|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:57.3918|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:57.3918|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:57.4117|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:57.4117|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:57.4117|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:57.4117|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:57.4117|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:57.4240|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:57.4240|DEBUG|WindowsDb2Editor.Services.MetadataHandler|
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
  "retrievedAt": "2025-12-16T15:00:58.4183848+01:00"
}
```

---

### âœ… Test #22: table-statistics

**Category**: Table Operations  
**Description**: Get table statistics  
**Status**: âœ… PASSED  
**Duration**: 1461 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:00:59  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-statistics -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:00:58.8487|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:00:58.8725|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:00:58.8770|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:00:58.8770|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:00:58.8770|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:00:58.8770|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-statistics -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-statistics_20251216_150027.json
2025-12-16 15:00:58.8770|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:00:58.8770|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:00:58.8770|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:00:58.8770|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:00:58.9024|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:00:58.9024|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:00:58.9024|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:00:58.9024|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:00:58.9049|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:00:58.9049|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:00:58.9049|DEBUG|WindowsDb2Editor.Services.Me
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
**Duration**: 1449 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:01  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-relationships -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:00.3179|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:00.3271|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:00.3271|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:00.3271|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:00.3271|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:00.3271|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-relationships -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-relationships_20251216_150027.json
2025-12-16 15:01:00.3271|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:00.3271|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:00.3398|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:00.3398|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:00.3605|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:00.3605|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:00.3605|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:00.3605|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:00.3605|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:00.3605|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:00.3605|DEBUG|WindowsDb2Editor.Servi
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
**Duration**: 1434 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:02  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-sample-data -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:01.7715|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:01.7715|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:01.7715|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:01.7715|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:01.7715|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:01.7715|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-sample-data -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-sample-data_20251216_150027.json
2025-12-16 15:01:01.7715|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:01.7866|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:01.7866|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:01.7866|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:01.8082|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:01.8082|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:01.8082|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:01.8082|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:01.8082|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:01.8082|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:01.8082|DEBUG|WindowsDb2Editor.Services.
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
**Duration**: 1392 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:04  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-size -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:03.1900|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:03.1900|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:03.1900|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:03.1900|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:03.1900|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:03.1900|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-size -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-size_20251216_150027.json
2025-12-16 15:01:03.1900|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:03.1900|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:03.2073|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:03.2073|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:03.2267|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:03.2267|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:03.2267|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:03.2267|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:03.2267|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:03.2267|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:03.2267|DEBUG|WindowsDb2Editor.Services.MetadataHandle
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
  "retrievedAt": "2025-12-16T15:01:04.1566106+01:00"
}
```

---

### âœ… Test #26: table-grants

**Category**: Table Operations  
**Description**: List table grants  
**Status**: âœ… PASSED  
**Duration**: 1486 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:05  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-grants -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:04.5800|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:04.5938|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:04.5938|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:04.5938|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:04.5938|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:04.5938|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-grants -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-grants_20251216_150027.json
2025-12-16 15:01:04.5938|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:04.5938|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:04.5938|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:04.5938|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:04.6198|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:04.6198|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:04.6198|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:04.6198|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:04.6274|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:04.6274|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:04.6274|DEBUG|WindowsDb2Editor.Services.MetadataHa
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
  "retrievedAt": "2025-12-16T15:01:05.6432644+01:00"
}
```

---

### âœ… Test #27: table-dependencies

**Category**: Table Operations  
**Description**: Show table dependencies  
**Status**: âœ… PASSED  
**Duration**: 1400 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:07  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-dependencies -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:06.0662|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:06.0662|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:06.0662|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:06.0662|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:06.0662|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:06.0662|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-dependencies -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-dependencies_20251216_150027.json
2025-12-16 15:01:06.0662|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:06.0662|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:06.0811|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:06.0811|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:06.0811|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:06.0811|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:06.1033|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:06.1033|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:06.1033|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:06.1033|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:06.1033|DEBUG|WindowsDb2Editor.Service
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
**Duration**: 1482 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:08  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-incoming-fk -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:07.4932|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:07.4932|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:07.4932|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:07.4932|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:07.4932|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:07.4932|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-incoming-fk -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-incoming-fk_20251216_150027.json
2025-12-16 15:01:07.4932|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:07.4932|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:07.4932|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:07.5085|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:07.5265|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:07.5265|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:07.5265|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:07.5265|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:07.5265|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:07.5265|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:07.5265|DEBUG|WindowsDb2Editor.Services.
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "incomingFKCount": 0,
  "incomingFKs": [],
  "retrievedAt": "2025-12-16T15:01:08.5407788+01:00"
}
```

---

### âœ… Test #29: table-used-by-packages

**Category**: Table Operations  
**Description**: List packages using this table  
**Status**: âœ… PASSED  
**Duration**: 1385 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:09  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-used-by-packages -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:08.9493|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:08.9582|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:08.9582|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:08.9582|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:08.9582|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:08.9582|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-used-by-packages -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-used-by-packages_20251216_150027.json
2025-12-16 15:01:08.9582|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:08.9582|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:08.9582|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:08.9582|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:08.9860|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:08.9860|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:08.9860|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:08.9860|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:08.9935|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:08.9935|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:08.9935|DEBUG|WindowsDb2Editor
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "packageCount": 0,
  "packages": [],
  "retrievedAt": "2025-12-16T15:01:09.9350847+01:00"
}
```

---

### âœ… Test #30: table-used-by-views

**Category**: Table Operations  
**Description**: List views using this table  
**Status**: âœ… PASSED  
**Duration**: 1450 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:11  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-used-by-views -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:10.3759|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:10.3759|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:10.3759|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:10.3759|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:10.3759|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:10.3759|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-used-by-views -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-used-by-views_20251216_150027.json
2025-12-16 15:01:10.3759|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:10.3759|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:10.3759|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:10.3759|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:10.4091|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:10.4091|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:10.4091|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:10.4091|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:10.4091|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:10.4091|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:10.4091|DEBUG|WindowsDb2Editor.Servi
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "viewCount": 0,
  "views": [],
  "retrievedAt": "2025-12-16T15:01:11.379808+01:00"
}
```

---

### âœ… Test #31: table-used-by-routines

**Category**: Table Operations  
**Description**: List routines using this table  
**Status**: âœ… PASSED  
**Duration**: 1513 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:12  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command table-used-by-routines -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:11.8277|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:11.8277|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:11.8277|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:11.8277|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:11.8277|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:11.8277|DEBUG|WindowsDb2Editor.App|Startup arguments: -command table-used-by-routines -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_table-used-by-routines_20251216_150027.json
2025-12-16 15:01:11.8277|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:11.8277|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:11.8277|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:11.8277|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:11.8654|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:11.8654|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:11.8654|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:11.8654|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:11.8731|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:11.8731|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:11.8731|DEBUG|WindowsDb2Editor
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableName": "BILAGNR",
  "routineCount": 0,
  "routines": [],
  "retrievedAt": "2025-12-16T15:01:12.8959529+01:00"
}
```

---

### âœ… Test #32: view-properties

**Category**: View Operations  
**Description**: Get view properties  
**Status**: âœ… PASSED  
**Duration**: 1490 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:14  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-properties -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:13.3174|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:13.3174|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:13.3174|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:13.3174|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:13.3174|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:13.3174|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-properties -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-properties_20251216_150027.json
2025-12-16 15:01:13.3174|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:13.3300|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:13.3300|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:13.3300|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:13.3540|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:13.3540|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:13.3540|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:13.3540|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:13.3540|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:13.3653|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:13.3653|DEBUG|WindowsDb2Editor.Services
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
  "retrievedAt": "2025-12-16T15:01:14.3951947+01:00"
}
```

---

### âœ… Test #33: view-definition

**Category**: View Operations  
**Description**: Get view definition SQL  
**Status**: âœ… PASSED  
**Duration**: 1441 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:15  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-definition -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:14.8550|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:14.8550|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:14.8550|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:14.8705|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:14.8705|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:14.8705|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-definition -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-definition_20251216_150027.json
2025-12-16 15:01:14.8705|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:14.8705|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:14.8705|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:14.8705|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:14.8985|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:14.8985|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:14.8985|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:14.8985|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:14.9068|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:14.9068|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:14.9068|DEBUG|WindowsDb2Editor.Services
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
**Duration**: 1466 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:17  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-columns -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:16.2393|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:16.2631|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:16.2631|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:16.2631|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:16.2631|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:16.2631|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-columns -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-columns_20251216_150027.json
2025-12-16 15:01:16.2631|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:16.2631|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:16.2631|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:16.2631|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:16.2903|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:16.2903|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:16.2903|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:16.2924|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:16.2924|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:16.2924|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:16.2924|DEBUG|WindowsDb2Editor.Services.Metad
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
**Duration**: 1435 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:18  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-dependencies -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:17.7442|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:17.7535|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:17.7535|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:17.7535|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:17.7535|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:17.7535|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-dependencies -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-dependencies_20251216_150027.json
2025-12-16 15:01:17.7535|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:17.7535|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:17.7535|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:17.7535|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:17.7816|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:17.7821|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:17.7821|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:17.7821|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:17.7821|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:17.7821|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:17.7821|DEBUG|WindowsDb2Editor.Serv
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
**Duration**: 1432 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:20  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-sample-data -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:19.1870|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:19.1958|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:19.1958|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:19.1958|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:19.1958|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:19.1958|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-sample-data -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-sample-data_20251216_150027.json
2025-12-16 15:01:19.1958|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:19.1958|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:19.1958|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:19.1958|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:19.2238|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:19.2238|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:19.2238|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:19.2238|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:19.2265|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:19.2265|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:19.2265|DEBUG|WindowsDb2Editor.Servic
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
**Duration**: 1538 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:21  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-used-by-packages -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:20.6698|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:20.6698|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:20.6839|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:20.6839|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:20.6839|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:20.6839|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-used-by-packages -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-used-by-packages_20251216_150027.json
2025-12-16 15:01:20.6839|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:20.6839|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:20.6900|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:20.6900|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:20.7169|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:20.7169|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:20.7169|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:20.7169|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:20.7258|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:20.7258|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:20.7258|DEBUG|WindowsDb2Edi
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
**Duration**: 1461 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:23  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command view-used-by-views -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:22.1660|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:22.1792|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:22.1792|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:22.1792|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:22.1792|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:22.1792|DEBUG|WindowsDb2Editor.App|Startup arguments: -command view-used-by-views -profile FKKTOTST -object SYSIBM.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_view-used-by-views_20251216_150027.json
2025-12-16 15:01:22.1792|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:22.1792|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:22.1792|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:22.1792|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:22.2068|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:22.2068|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:22.2068|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:22.2068|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:22.2068|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:22.2068|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:22.2068|DEBUG|WindowsDb2Editor.Se
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
**Duration**: 1508 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:24  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-properties -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:23.6210|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:23.6210|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:23.6210|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:23.6210|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:23.6210|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:23.6210|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-properties -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-properties_20251216_150027.json
2025-12-16 15:01:23.6210|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:23.6210|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:23.6361|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:23.6361|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:23.6574|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:23.6574|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:23.6574|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:23.6574|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:23.6691|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:23.6691|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:23.6691|DEBUG|WindowsDb2Edit
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
  "retrievedAt": "2025-12-16T15:01:24.7104728+01:00"
}
```

---

### âœ… Test #40: procedure-source

**Category**: Procedure Operations  
**Description**: Get procedure source code  
**Status**: âœ… PASSED  
**Duration**: 1443 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:26  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-source -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:25.1379|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:25.1469|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:25.1469|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:25.1469|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:25.1469|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:25.1469|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-source -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-source_20251216_150027.json
2025-12-16 15:01:25.1469|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:25.1469|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:25.1559|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:25.1559|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:25.1559|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:25.1758|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:25.1758|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:25.1758|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:25.1758|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:25.1758|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:25.1758|DEBUG|WindowsDb2Editor.Servi
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
**Duration**: 1684 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:27  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-parameters -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:26.6101|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:26.6101|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:26.6101|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:26.6101|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:26.6101|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:26.6101|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-parameters -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-parameters_20251216_150027.json
2025-12-16 15:01:26.6101|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:26.6101|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:26.6101|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:26.6101|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:26.6481|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:26.6481|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:26.6497|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:26.6497|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:26.6497|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:26.6606|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:26.6606|DEBUG|WindowsDb2Edit
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
**Duration**: 1505 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:29  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-dependencies -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:28.2551|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:28.2551|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:28.2551|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:28.2551|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:28.2551|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:28.2551|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-dependencies -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-dependencies_20251216_150027.json
2025-12-16 15:01:28.2551|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:28.2551|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:28.2551|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:28.2705|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:28.2877|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:28.2877|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:28.2877|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:28.2877|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:28.2877|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:28.2877|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:28.2877|DEBUG|WindowsDb2
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
**Duration**: 1465 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:30  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-usage -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:29.8426|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:29.8426|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:29.8426|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:29.8426|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:29.8426|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:29.8426|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-usage -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-usage_20251216_150027.json
2025-12-16 15:01:29.8426|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:29.8426|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:29.8572|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:29.8572|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:29.8782|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:29.8782|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:29.8782|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:29.8782|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:29.8782|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:29.8782|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:29.8782|DEBUG|WindowsDb2Editor.Service
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
**Duration**: 1461 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:32  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command procedure-grants -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:31.2393|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:31.2484|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:31.2484|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:31.2484|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:31.2484|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:31.2484|DEBUG|WindowsDb2Editor.App|Startup arguments: -command procedure-grants -profile FKKTOTST -object SYSPROC.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_procedure-grants_20251216_150027.json
2025-12-16 15:01:31.2484|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:31.2484|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:31.2484|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:31.2484|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:31.2745|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:31.2745|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:31.2745|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:31.2745|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:31.2819|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:31.2819|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:31.2819|DEBUG|WindowsDb2Editor.Servi
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
**Duration**: 1462 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:33  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-properties -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:32.7423|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:32.7423|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:32.7423|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:32.7423|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:32.7423|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:32.7423|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-properties -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-properties_20251216_150027.json
2025-12-16 15:01:32.7423|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:32.7423|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:32.7559|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:32.7559|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:32.7814|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:32.7814|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:32.7814|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:32.7814|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:32.7905|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:32.7905|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:32.7905|DEBUG|WindowsDb2Editor.Servic
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
  "retrievedAt": "2025-12-16T15:01:33.7598982+01:00"
}
```

---

### âœ… Test #46: function-source

**Category**: Function Operations  
**Description**: Get function source code  
**Status**: âœ… PASSED  
**Duration**: 1439 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:35  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-source -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:34.1674|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:34.1751|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:34.1751|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:34.1751|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:34.1751|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:34.1751|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-source -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-source_20251216_150027.json
2025-12-16 15:01:34.1751|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:34.1751|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:34.1751|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:34.1751|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:34.2023|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:34.2023|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:34.2023|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:34.2023|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:34.2023|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:34.2151|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:34.2151|DEBUG|WindowsDb2Editor.Services.Metad
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
**Duration**: 1551 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:36  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-parameters -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:35.6249|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:35.6249|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:35.6249|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:35.6249|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:35.6249|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:35.6249|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-parameters -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-parameters_20251216_150027.json
2025-12-16 15:01:35.6249|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:35.6249|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:35.6401|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:35.6401|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:35.6668|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:35.6668|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:35.6668|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:35.6668|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:35.6757|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:35.6757|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:35.6757|DEBUG|WindowsDb2Editor.Servic
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
**Duration**: 1444 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:38  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-dependencies -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:37.1813|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:37.1813|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:37.1813|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:37.1813|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:37.1813|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:37.1813|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-dependencies -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-dependencies_20251216_150027.json
2025-12-16 15:01:37.1927|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:37.1927|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:37.1927|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:37.1927|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:37.2206|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:37.2206|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:37.2206|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:37.2206|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:37.2245|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:37.2245|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:37.2245|DEBUG|WindowsDb2Editor.Se
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
**Duration**: 1444 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:39  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-usage -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:38.6164|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:38.6256|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:38.6256|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:38.6256|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:38.6256|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:38.6256|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-usage -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-usage_20251216_150027.json
2025-12-16 15:01:38.6256|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:38.6256|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:38.6256|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:38.6365|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:38.6590|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:38.6590|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:38.6590|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:38.6590|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:38.6716|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:38.6716|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:38.6716|DEBUG|WindowsDb2Editor.Services.Metadat
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
**Duration**: 1338 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:41  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command function-grants -profile FKKTOTST -object SYSFUN.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:40.0496|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:40.0496|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:40.0630|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:40.0630|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:40.0630|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:40.0630|DEBUG|WindowsDb2Editor.App|Startup arguments: -command function-grants -profile FKKTOTST -object SYSFUN.ABS -format json -Outfile CLI_Test_Output/temp_function-grants_20251216_150027.json
2025-12-16 15:01:40.0630|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:40.0630|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:40.0630|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:40.0630|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:40.0873|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:40.0873|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:40.0873|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:40.0873|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:40.0945|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:40.0945|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:40.0945|DEBUG|WindowsDb2Editor.Services.Metad
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
**Duration**: 1507 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:42  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-properties -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:41.4097|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:41.4097|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:41.4097|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:41.4097|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:41.4097|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:41.4097|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-properties -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-properties_20251216_150027.json
2025-12-16 15:01:41.4097|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:41.4097|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:41.4277|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:41.4277|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:41.4500|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:41.4500|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:41.4500|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:41.4500|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:41.4500|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:41.4607|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:41.4607|DEBUG|WindowsDb2Editor.Servic
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
**Duration**: 1475 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:44  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-statements -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:42.9919|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:42.9919|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:42.9919|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:42.9919|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:42.9919|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:42.9919|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-statements -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-statements_20251216_150027.json
2025-12-16 15:01:42.9919|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:42.9919|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:42.9919|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:43.0064|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:43.0317|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:43.0317|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:43.0317|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:43.0317|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:43.0317|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:43.0431|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:43.0431|DEBUG|WindowsDb2Editor.Servic
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
**Duration**: 1491 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:45  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-dependencies -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:44.4337|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:44.4442|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:44.4442|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:44.4442|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:44.4442|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:44.4442|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-dependencies -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-dependencies_20251216_150027.json
2025-12-16 15:01:44.4442|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:44.4442|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:44.4442|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:44.4566|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:44.4786|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:44.4786|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:44.4786|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:44.4786|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:44.4786|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:44.4884|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:44.4884|DEBUG|WindowsDb2Editor.Se
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
**Duration**: 1474 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:47  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-statistics -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:45.9514|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:45.9612|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:45.9612|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:45.9612|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:45.9612|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:45.9612|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-statistics -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-statistics_20251216_150027.json
2025-12-16 15:01:45.9612|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:45.9612|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:45.9612|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:45.9612|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:45.9998|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:45.9998|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:45.9998|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:45.9998|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:46.0093|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:46.0093|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:46.0093|DEBUG|WindowsDb2Editor.Servic
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
**Duration**: 1460 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:48  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-list-tables -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:47.3742|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:47.3742|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:47.3742|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:47.3742|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:47.3742|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:47.3742|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-list-tables -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-list-tables_20251216_150027.json
2025-12-16 15:01:47.3742|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:47.3900|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:47.3900|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:47.3900|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:47.4134|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:47.4134|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:47.4134|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:47.4134|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:47.4134|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:47.4225|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:47.4225|DEBUG|WindowsDb2Editor.Serv
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
**Duration**: 1433 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:49  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-list-views -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:48.8732|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:48.8829|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:48.8829|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:48.8829|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:48.8829|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:48.8829|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-list-views -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-list-views_20251216_150027.json
2025-12-16 15:01:48.8829|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:48.8829|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:48.8829|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:48.8829|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:48.9105|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:48.9105|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:48.9105|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:48.9105|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:48.9105|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:48.9230|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:48.9230|DEBUG|WindowsDb2Editor.Servic
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
**Duration**: 1427 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:51  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-list-procedures -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:50.2662|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:50.2662|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:50.2662|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:50.2662|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:50.2662|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:50.2662|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-list-procedures -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-list-procedures_20251216_150027.json
2025-12-16 15:01:50.2662|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:50.2787|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:50.2787|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:50.2787|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:50.3004|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:50.3004|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:50.3004|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:50.3004|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:50.3073|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:50.3073|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:50.3073|DEBUG|WindowsDb2Edi
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
**Duration**: 1458 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:52  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command package-list-functions -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:51.7121|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:51.7220|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:51.7220|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:51.7220|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:51.7220|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:51.7220|DEBUG|WindowsDb2Editor.App|Startup arguments: -command package-list-functions -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_package-list-functions_20251216_150027.json
2025-12-16 15:01:51.7220|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:51.7220|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:51.7220|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:51.7220|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:51.7537|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:51.7537|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:51.7537|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:51.7537|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:51.7537|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:51.7537|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:51.7537|DEBUG|WindowsDb2Edito
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
**Duration**: 1352 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:54  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command user-properties -profile FKKTOTST -object FKGEISTA -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:53.1474|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:53.1474|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:53.1474|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:53.1474|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:53.1474|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:53.1474|DEBUG|WindowsDb2Editor.App|Startup arguments: -command user-properties -profile FKKTOTST -object FKGEISTA -format json -Outfile CLI_Test_Output/temp_user-properties_20251216_150027.json
2025-12-16 15:01:53.1474|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:53.1474|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:53.1641|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:53.1641|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:53.1848|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:53.1848|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:53.1848|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:53.1848|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:53.1932|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:53.1932|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:53.1932|DEBUG|WindowsDb2Editor.Services.Metadat
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
**Duration**: 1337 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:55  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command user-privileges -profile FKKTOTST -object FKGEISTA -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:54.5181|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:54.5181|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:54.5181|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:54.5181|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:54.5181|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:54.5181|DEBUG|WindowsDb2Editor.App|Startup arguments: -command user-privileges -profile FKKTOTST -object FKGEISTA -format json -Outfile CLI_Test_Output/temp_user-privileges_20251216_150027.json
2025-12-16 15:01:54.5181|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:54.5181|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:54.5321|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:54.5321|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:54.5521|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:54.5521|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:54.5521|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:54.5521|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:54.5521|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:54.5521|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:54.5521|DEBUG|WindowsDb2Editor.Services.Metadat
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
**Duration**: 1436 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:56  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command user-tables -profile FKKTOTST -object FKGEISTA -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:55.9176|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:55.9272|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:55.9272|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:55.9272|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:55.9272|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:55.9272|DEBUG|WindowsDb2Editor.App|Startup arguments: -command user-tables -profile FKKTOTST -object FKGEISTA -format json -Outfile CLI_Test_Output/temp_user-tables_20251216_150027.json
2025-12-16 15:01:55.9272|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:55.9272|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:55.9272|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:55.9272|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:55.9553|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:55.9553|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:55.9566|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:55.9566|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:55.9566|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:55.9566|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:55.9566|DEBUG|WindowsDb2Editor.Services.MetadataHandler
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
**Duration**: 1457 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:58  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command user-schemas -profile FKKTOTST -object FKGEISTA -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:57.2938|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:57.3033|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:57.3033|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:57.3033|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:57.3033|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:57.3033|DEBUG|WindowsDb2Editor.App|Startup arguments: -command user-schemas -profile FKKTOTST -object FKGEISTA -format json -Outfile CLI_Test_Output/temp_user-schemas_20251216_150027.json
2025-12-16 15:01:57.3033|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:57.3033|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:57.3033|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:57.3033|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:57.3313|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:57.3313|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:57.3313|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:57.3313|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:57.3386|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:57.3386|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:57.3386|DEBUG|WindowsDb2Editor.Services.MetadataHandl
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
**Duration**: 1426 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:01:59  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command user-connections -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:01:58.7588|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:01:58.7733|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:01:58.7733|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:01:58.7733|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:01:58.7733|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:01:58.7733|DEBUG|WindowsDb2Editor.App|Startup arguments: -command user-connections -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_user-connections_20251216_150027.json
2025-12-16 15:01:58.7733|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:01:58.7733|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:01:58.7733|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:01:58.7733|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:01:58.8027|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:01:58.8027|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:01:58.8027|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:01:58.8027|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:01:58.8108|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:01:58.8108|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:01:58.8108|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
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
**Duration**: 1740 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:01  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-load -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:00.2253|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:00.2419|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:00.2419|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:00.2419|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:00.2419|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:00.2419|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-load -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-load_20251216_150027.json
2025-12-16 15:02:00.2419|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:00.2419|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:00.2567|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:00.2567|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:00.2915|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:00.2915|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:00.2915|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:00.2915|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:00.2915|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:00.3073|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:00.3073|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "ALL",
  "totalTables": 73,
  "limitApplied": 73,
  "metrics": [
    {
      "schema": "SYSIBM",
      "tableName": "SYSROUTINEPARMS",
      "rowsRead": 26758,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSROUTINES",
      "rowsRead": 11580,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSCOLUMNS",
      "rowsRead": 8862,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSPLAN",
      "rowsRead": 2924,
      "rowsInserted": 0,
      "rowsUpdated": 6,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSTABLES",
      "rowsRead": 2665,
      "rowsInserted": 0,
      "rowsUpdated": 43,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSDEPENDENCIES",
      "rowsRead": 2282,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSDATATYPES",
      "rowsRead": 160,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSPLANDEP",
      "rowsRead": 64,
      "rowsInserted": 14,
      "rowsUpdated": 0,
      "rowsDeleted": 64
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSSECTION",
      "rowsRead": 96,
      "rowsInserted": 6,
      "rowsUpdated": 0,
      "rowsDeleted": 32
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSSCHEMATA",
      "rowsRead": 117,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSROLEAUTH",
      "rowsRead": 108,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSDBAUTH",
      "rowsRead": 95,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSTABCONST",
      "rowsRead": 84,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSHISTOGRAMTEMPLATEUSE",
      "rowsRead": 73,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSINDEXES",
      "rowsRead": 19,
      "rowsInserted": 0,
      "rowsUpdated": 46,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSRELS",
      "rowsRead": 58,
      "rowsInserted": 0,
      "rowsUpdated": 0,
      "rowsDeleted": 0
    },
    {
      "schema": "SYSIBM",
      "tableName": "SYSTABLESPACES",
      "rowsRead":
... (truncated)
```

---

### âœ… Test #65: db-locks

**Category**: Monitoring & Stats  
**Description**: Show database locks  
**Status**: âœ… PASSED  
**Duration**: 2351 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:03  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-locks -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:01.9686|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:01.9774|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:01.9774|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:01.9774|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:01.9774|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:01.9774|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-locks -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-locks_20251216_150027.json
2025-12-16 15:02:01.9774|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:01.9774|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:01.9774|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:01.9774|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:02.0071|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:02.0071|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:02.0071|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:02.0071|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:02.0071|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:02.0071|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:02.0071|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for D
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
      "agentId": 196
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
      "agentId": 196
    }
  ],
  "retrievedAt": "2025-12-16T15:02:03.8604481+01:00"
}
```

---

### âœ… Test #66: db-sessions

**Category**: Monitoring & Stats  
**Description**: List database sessions  
**Status**: âœ… PASSED  
**Duration**: 1444 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:05  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-sessions -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:04.2815|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:04.2815|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:04.2815|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:04.2815|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:04.2815|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:04.2815|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-sessions -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-sessions_20251216_150027.json
2025-12-16 15:02:04.2815|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:04.2815|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:04.2815|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:04.2815|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:04.3146|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:04.3146|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:04.3146|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:04.3146|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:04.3146|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:04.3146|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:04.3146|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata
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
      "connectTime": "2025-12-16T15:02:05.406",
      "message": "Session monitoring requires admin views"
    }
  ],
  "retrievedAt": "2025-12-16T15:02:05.3087519+01:00"
}
```

---

### âœ… Test #67: db-size

**Category**: Monitoring & Stats  
**Description**: Get database size  
**Status**: âœ… PASSED  
**Duration**: 1537 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:06  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-size -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:05.7440|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:05.7555|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:05.7555|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:05.7555|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:05.7555|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:05.7555|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-size -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-size_20251216_150027.json
2025-12-16 15:02:05.7555|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:05.7555|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:05.7555|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:05.7555|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:05.7821|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:05.7821|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:05.7821|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:05.7821|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:05.7889|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:05.7889|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:05.7889|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "totalRows": 1599183,
  "totalPages": 62127,
  "totalSizeMB": 242.68359375000,
  "approximateSizeGB": 0.23699569702148438,
  "retrievedAt": "2025-12-16T15:02:06.8341841+01:00"
}
```

---

### âœ… Test #68: schema-size

**Category**: Monitoring & Stats  
**Description**: Get schema size  
**Status**: âœ… PASSED  
**Duration**: 1460 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:08  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command schema-size -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:07.2846|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:07.3086|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:07.3086|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:07.3086|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:07.3086|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:07.3086|DEBUG|WindowsDb2Editor.App|Startup arguments: -command schema-size -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_schema-size_20251216_150027.json
2025-12-16 15:02:07.3086|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:07.3086|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:07.3086|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:07.3086|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:07.3386|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:07.3386|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:07.3386|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:07.3386|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:07.3467|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:07.3467|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:07.3467|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Load
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
  "retrievedAt": "2025-12-16T15:02:08.3047358+01:00"
}
```

---

### âœ… Test #69: db-version

**Category**: Monitoring & Stats  
**Description**: Get database version  
**Status**: âœ… PASSED  
**Duration**: 1572 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:09  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-version -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:08.7430|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:08.7515|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:08.7515|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:08.7515|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:08.7515|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:08.7515|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-version -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-version_20251216_150027.json
2025-12-16 15:02:08.7515|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:08.7515|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:08.7515|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:08.7515|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:08.7809|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:08.7809|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:08.7809|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:08.7809|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:08.7883|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:08.7883|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:08.7883|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata f
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
**Duration**: 1442 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:11  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-parameters -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:10.2853|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:10.2853|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:10.2853|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:10.2853|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:10.2853|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:10.2853|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-parameters -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-parameters_20251216_150027.json
2025-12-16 15:02:10.2853|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:10.3003|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:10.3003|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:10.3003|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:10.3263|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:10.3263|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:10.3263|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:10.3263|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:10.3359|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:10.3359|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:10.3359|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading meta
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
**Duration**: 1588 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:12  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-registry -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:11.7557|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:11.7656|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:11.7656|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:11.7656|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:11.7656|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:11.7656|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-registry -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_db-registry_20251216_150027.json
2025-12-16 15:02:11.7656|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:11.7656|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:11.7656|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:11.7656|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:11.7916|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:11.7916|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:11.7916|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:11.7916|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:11.7976|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:11.7976|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:11.7976|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata
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
**Duration**: 1450 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:14  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command active-queries -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:13.3313|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:13.3313|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:13.3313|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:13.3313|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:13.3313|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:13.3450|DEBUG|WindowsDb2Editor.App|Startup arguments: -command active-queries -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_active-queries_20251216_150027.json
2025-12-16 15:02:13.3450|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:13.3450|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:13.3450|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:13.3450|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:13.3680|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:13.3680|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:13.3680|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:13.3680|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:13.3680|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:13.3769|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:13.3769|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading me
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "active-queries",
  "queries": [
    {
      "APPLICATION_HANDLE": 212,
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
**Duration**: 1662 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:16  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command bufferpool-stats -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:14.7919|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:14.7919|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:14.7919|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:14.7919|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:14.7919|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:14.7919|DEBUG|WindowsDb2Editor.App|Startup arguments: -command bufferpool-stats -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_bufferpool-stats_20251216_150027.json
2025-12-16 15:02:14.8049|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:14.8049|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:14.8049|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:14.8049|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:14.8306|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:14.8306|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:14.8306|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:14.8306|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:14.8306|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:14.8306|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:14.8306|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "bufferpool-stats",
  "stats": [
    {
      "BP_NAME": "IBMDEFAULTBP",
      "POOL_DATA_L_READS": 0,
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
**Duration**: 1640 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:17  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command tablespace-usage -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:16.4518|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:16.4609|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:16.4609|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:16.4609|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:16.4609|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:16.4609|DEBUG|WindowsDb2Editor.App|Startup arguments: -command tablespace-usage -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_tablespace-usage_20251216_150027.json
2025-12-16 15:02:16.4609|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:16.4609|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:16.4609|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:16.4609|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:16.4883|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:16.4883|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:16.4897|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:16.4897|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:16.4897|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:16.4897|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:16.4897|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
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
**Duration**: 1441 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:19  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-status -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:18.0746|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:18.0836|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:18.0836|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:18.0836|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:18.0836|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:18.0836|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-status -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_cdc-status_20251216_150027.json
2025-12-16 15:02:18.0836|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:18.0836|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:18.0836|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:18.0836|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:18.1109|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:18.1118|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:18.1118|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:18.1118|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:18.1118|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:18.1118|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:18.1118|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "schema": "INL",
  "tableCount": 0,
  "tables": [],
  "retrievedAt": "2025-12-16T15:02:19.1251929+01:00"
}
```

---

### âœ… Test #76: cdc-status-full

**Category**: CDC Operations  
**Description**: Get full CDC status  
**Status**: âœ… PASSED  
**Duration**: 1565 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:20  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-status-full -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:19.5348|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:19.5348|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:19.5348|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:19.5348|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:19.5348|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:19.5348|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-status-full -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_cdc-status-full_20251216_150027.json
2025-12-16 15:02:19.5348|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:19.5348|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:19.5526|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:19.5526|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:19.5762|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:19.5762|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:19.5762|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:19.5762|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:19.5843|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:19.5843|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:19.5843|DEBUG|WindowsDb2Editor.Services.MetadataHand
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "cdcEnabled": false,
  "note": "CDC status requires SYSCAT.DATAPARTITIONS or CDC-specific views",
  "retrievedAt": "2025-12-16T15:02:20.6835995+01:00"
}
```

---

### âœ… Test #77: cdc-configuration

**Category**: CDC Operations  
**Description**: Get CDC configuration for table  
**Status**: âœ… PASSED  
**Duration**: 1367 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:22  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-configuration -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:21.0900|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:21.0988|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:21.0988|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:21.0988|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:21.0988|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:21.0988|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-configuration -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_cdc-configuration_20251216_150027.json
2025-12-16 15:02:21.0988|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:21.0988|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:21.0988|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:21.0988|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:21.1264|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:21.1264|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:21.1264|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:21.1264|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:21.1264|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:21.1264|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:21.1264|DEBUG|WindowsDb2Editor.Services.
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "cdcConfigured": false,
  "note": "CDC configuration requires ASN tables (IBMSNAP_*)",
  "retrievedAt": "2025-12-16T15:02:22.0597385+01:00"
}
```

---

### âœ… Test #78: cdc-changes

**Category**: CDC Operations  
**Description**: List CDC changes  
**Status**: âœ… PASSED  
**Duration**: 1492 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:23  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-changes -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:22.4726|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:22.4726|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:22.4726|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:22.4726|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:22.4726|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:22.4726|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-changes -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_cdc-changes_20251216_150027.json
2025-12-16 15:02:22.4726|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:22.4726|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:22.4899|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:22.4899|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:22.5090|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:22.5090|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:22.5090|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:22.5090|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:22.5090|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:22.5090|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:22.5090|DEBUG|WindowsDb2Editor.Services.MetadataHand
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "changesFound": 0,
  "note": "CDC changes require reading CDC change tables (configured per table)",
  "retrievedAt": "2025-12-16T15:02:23.5604069+01:00"
}
```

---

### âœ… Test #79: cdc-enable

**Category**: CDC Operations  
**Description**: Enable CDC for table  
**Status**: âœ… PASSED  
**Duration**: 1367 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:24  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-enable -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:23.9778|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:23.9887|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:23.9887|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:23.9887|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:23.9887|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:23.9887|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-enable -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_cdc-enable_20251216_150027.json
2025-12-16 15:02:23.9887|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:23.9887|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:23.9887|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:23.9887|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:24.0141|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:24.0141|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:24.0141|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:24.0141|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:24.0206|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:24.0206|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:24.0206|DEBUG|WindowsDb2Editor.Services.MetadataHandle
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
**Duration**: 1448 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:26  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-disable -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:25.3416|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:25.3416|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:25.3416|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:25.3416|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:25.3416|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:25.3416|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-disable -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_cdc-disable_20251216_150027.json
2025-12-16 15:02:25.3416|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:25.3416|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:25.3416|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:25.3559|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:25.3738|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:25.3738|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:25.3738|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:25.3738|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:25.3738|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:25.3738|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:25.3738|DEBUG|WindowsDb2Editor.Services.MetadataHand
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
**Duration**: 1402 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:27  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command cdc-history -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:26.8063|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:26.8154|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:26.8154|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:26.8154|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:26.8154|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:26.8154|DEBUG|WindowsDb2Editor.App|Startup arguments: -command cdc-history -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_cdc-history_20251216_150027.json
2025-12-16 15:02:26.8154|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:26.8154|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:26.8154|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:26.8154|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:26.8448|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:26.8448|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:26.8448|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:26.8448|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:26.8448|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:26.8448|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:26.8448|DEBUG|WindowsDb2Editor.Services.MetadataHand
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
**Duration**: 1458 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:29  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-compare -profile FKKTOTST -object INL -schema INL_TEST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:28.1931|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:28.1931|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:28.1931|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:28.1931|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:28.1931|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:28.1931|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-compare -profile FKKTOTST -object INL -schema INL_TEST -format json -Outfile CLI_Test_Output/temp_db-compare_20251216_150027.json
2025-12-16 15:02:28.1931|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:28.1931|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:28.1931|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:28.1931|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:28.2293|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:28.2293|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:28.2293|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:28.2293|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:28.2359|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:28.2359|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:28.2359|DEBUG|WindowsDb2Editor.Services.Metad
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
  "timestamp": "2025-12-16T15:02:29.2531122+01:00"
}
```

---

### âœ… Test #83: db-compare-source-only

**Category**: Database Comparison  
**Description**: Show objects only in source  
**Status**: âœ… PASSED  
**Duration**: 1412 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:30  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-compare-source-only -profile FKKTOTST -object INL -schema INL_TEST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:29.6744|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:29.6744|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:29.6744|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:29.6744|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:29.6744|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:29.6744|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-compare-source-only -profile FKKTOTST -object INL -schema INL_TEST -format json -Outfile CLI_Test_Output/temp_db-compare-source-only_20251216_150027.json
2025-12-16 15:02:29.6744|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:29.6744|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:29.6949|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:29.6949|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:29.7178|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:29.7178|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:29.7178|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:29.7178|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:29.7178|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:29.7265|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:29.7265|DEBUG|Windows
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
**Duration**: 1397 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:32  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-compare-target-only -profile FKKTOTST -object INL -schema INL_TEST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:31.0886|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:31.0982|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:31.0982|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:31.0982|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:31.0982|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:31.0982|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-compare-target-only -profile FKKTOTST -object INL -schema INL_TEST -format json -Outfile CLI_Test_Output/temp_db-compare-target-only_20251216_150027.json
2025-12-16 15:02:31.0982|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:31.0982|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:31.0982|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:31.0982|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:31.1261|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:31.1261|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:31.1261|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:31.1261|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:31.1261|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:31.1420|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:31.1420|DEBUG|Windows
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
**Duration**: 1498 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:33  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-compare-different -profile FKKTOTST -object INL -schema INL_TEST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:32.4713|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:32.4713|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:32.4713|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:32.4713|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:32.4713|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:32.4713|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-compare-different -profile FKKTOTST -object INL -schema INL_TEST -format json -Outfile CLI_Test_Output/temp_db-compare-different_20251216_150027.json
2025-12-16 15:02:32.4713|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:32.4713|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:32.4869|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:32.4869|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:32.5070|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:32.5070|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:32.5070|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:32.5070|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:32.5181|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:32.5181|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:32.5181|DEBUG|WindowsDb2E
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
**Duration**: 1411 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:35  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command db-compare-ddl -profile FKKTOTST -object INL -schema INL_TEST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:33.9746|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:33.9746|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:33.9746|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:33.9890|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:33.9890|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:33.9890|DEBUG|WindowsDb2Editor.App|Startup arguments: -command db-compare-ddl -profile FKKTOTST -object INL -schema INL_TEST -format json -Outfile CLI_Test_Output/temp_db-compare-ddl_20251216_150027.json
2025-12-16 15:02:33.9890|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:33.9890|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:33.9890|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:33.9890|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:34.0150|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:34.0163|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:34.0163|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:34.0163|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:34.0163|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:34.0163|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:34.0163|DEBUG|WindowsDb2Editor.Servic
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "db-compare-ddl",
  "sourceSchema": "INL",
  "targetSchema": "INL_TEST",
  "ddl": "-- ALTER TABLE statements placeholder\n-- Use DatabaseComparisonService for full implementation",
  "timestamp": "2025-12-16T15:02:34.9636635+01:00"
}
```

---

### âœ… Test #87: object-metadata

**Category**: Metadata & Advanced  
**Description**: Get object metadata  
**Status**: âœ… PASSED  
**Duration**: 1418 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:36  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command object-metadata -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:35.4133|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:35.4223|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:35.4223|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:35.4223|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:35.4223|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:35.4223|DEBUG|WindowsDb2Editor.App|Startup arguments: -command object-metadata -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_object-metadata_20251216_150027.json
2025-12-16 15:02:35.4223|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:35.4223|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:35.4305|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:35.4305|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:35.4305|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:35.4305|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:35.4511|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:35.4511|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:35.4511|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:35.4511|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:35.4511|DEBUG|WindowsDb2Editor.Services.Meta
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
**Duration**: 1436 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:37  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command object-search -profile FKKTOTST -object BILAG% -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:36.8323|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:36.8323|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:36.8323|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:36.8323|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:36.8323|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:36.8323|DEBUG|WindowsDb2Editor.App|Startup arguments: -command object-search -profile FKKTOTST -object BILAG% -format json -Outfile CLI_Test_Output/temp_object-search_20251216_150027.json
2025-12-16 15:02:36.8323|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:36.8323|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:36.8323|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:36.8517|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:36.8697|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:36.8697|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:36.8697|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:36.8697|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:36.8697|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:36.8697|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:36.8697|DEBUG|WindowsDb2Editor.Services.MetadataHandl
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
**Duration**: 1375 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:39  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command column-search -profile FKKTOTST -object BILAG% -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:38.2558|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:38.2558|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:38.2558|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:38.2558|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:38.2558|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:38.2558|DEBUG|WindowsDb2Editor.App|Startup arguments: -command column-search -profile FKKTOTST -object BILAG% -format json -Outfile CLI_Test_Output/temp_column-search_20251216_150027.json
2025-12-16 15:02:38.2558|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:38.2558|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:38.2689|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:38.2689|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:38.2881|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:38.2881|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:38.2881|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:38.2881|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:38.2881|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:38.2881|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:38.2881|DEBUG|WindowsDb2Editor.Services.MetadataHandl
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
**Duration**: 1434 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:40  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command dependency-graph -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:39.6396|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:39.6396|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:39.6396|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:39.6396|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:39.6396|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:39.6396|DEBUG|WindowsDb2Editor.App|Startup arguments: -command dependency-graph -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_dependency-graph_20251216_150027.json
2025-12-16 15:02:39.6396|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:39.6546|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:39.6546|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:39.6546|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:39.6764|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:39.6764|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:39.6764|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:39.6764|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:39.6764|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:39.6764|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:39.6871|DEBUG|WindowsDb2Editor.Services.Me
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
  "retrievedAt": "2025-12-16T15:02:40.660695+01:00"
}
```

---

### âœ… Test #91: query-history

**Category**: Metadata & Advanced  
**Description**: Show query history  
**Status**: âœ… PASSED  
**Duration**: 1311 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:42  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command query-history -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:41.0685|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:41.0685|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:41.0685|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:41.0685|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:41.0685|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:41.0685|DEBUG|WindowsDb2Editor.App|Startup arguments: -command query-history -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_query-history_20251216_150027.json
2025-12-16 15:02:41.0685|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:41.0685|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:41.0832|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:41.0832|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:41.1027|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:41.1027|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:41.1027|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:41.1027|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:41.1027|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:41.1027|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:41.1027|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading meta
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "note": "Query history is session-based and not available in CLI mode",
  "sessionMode": "CLI",
  "historyAvailable": false,
  "retrievedAt": "2025-12-16T15:02:41.9658662+01:00"
}
```

---

### âœ… Test #92: schema-summary

**Category**: Metadata & Advanced  
**Description**: Get schema summary  
**Status**: âœ… PASSED  
**Duration**: 1582 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:43  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command schema-summary -profile FKKTOTST -schema INL -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:42.3768|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:42.3820|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:42.3820|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:42.3820|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:42.3820|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:42.3820|DEBUG|WindowsDb2Editor.App|Startup arguments: -command schema-summary -profile FKKTOTST -schema INL -format json -Outfile CLI_Test_Output/temp_schema-summary_20251216_150027.json
2025-12-16 15:02:42.3820|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:42.3820|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:42.3820|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:42.3820|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:42.4169|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:42.4169|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:42.4169|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:42.4205|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:42.4256|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:42.4256|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:42.4256|DEBUG|WindowsDb2Editor.Services.MetadataHandle
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
**Duration**: 1476 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:45  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command database-summary -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:43.9840|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:43.9931|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:43.9931|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:43.9931|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:43.9931|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:43.9931|DEBUG|WindowsDb2Editor.App|Startup arguments: -command database-summary -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_database-summary_20251216_150027.json
2025-12-16 15:02:43.9931|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:43.9931|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:44.0019|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:44.0019|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:44.0019|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:44.0019|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:44.0019|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:44.0019|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:44.0287|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:44.0287|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:44.0287|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loadin
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
**Duration**: 1377 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:46  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command health-check -profile FKKTOTST -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:45.4687|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:45.4775|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:45.4775|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:45.4775|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:45.4775|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:45.4775|DEBUG|WindowsDb2Editor.App|Startup arguments: -command health-check -profile FKKTOTST -format json -Outfile CLI_Test_Output/temp_health-check_20251216_150027.json
2025-12-16 15:02:45.4775|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:45.4775|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:45.4775|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:45.4775|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:45.5050|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:45.5057|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:45.5057|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:45.5057|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:45.5057|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:45.5057|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:45.5057|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metada
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
**Duration**: 1400 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:47  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-query -profile FKKTOTST -prompt Show all invoices from last month -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:46.8526|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:46.8526|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:46.8526|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:46.8526|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:46.8526|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:46.8526|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-query -profile FKKTOTST -prompt Show all invoices from last month -format json -Outfile CLI_Test_Output/temp_ai-query_20251216_150027.json
2025-12-16 15:02:46.8526|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:46.8680|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:46.8680|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:46.8680|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:46.8892|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:46.8892|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:46.8892|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:46.8911|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:46.8911|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:46.8911|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:46.8911|DEBUG|WindowsDb2Editor.Servi
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
  "timestamp": "2025-12-16T15:02:47.831565+01:00"
}
```

---

### âœ… Test #96: ai-explain-table

**Category**: AI Features  
**Description**: AI explanation of table  
**Status**: âœ… PASSED  
**Duration**: 1362 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:49  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-explain-table -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:48.2607|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:48.2702|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:48.2702|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:48.2702|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:48.2702|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:48.2702|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-explain-table -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_ai-explain-table_20251216_150027.json
2025-12-16 15:02:48.2702|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:48.2702|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:48.2702|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:48.2702|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:48.2989|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:48.2989|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:48.2989|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:48.3020|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:48.3020|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:48.3020|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:48.3020|DEBUG|WindowsDb2Editor.Services.Me
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
  "timestamp": "2025-12-16T15:02:49.190763+01:00"
}
```

---

### âœ… Test #97: ai-explain-view

**Category**: AI Features  
**Description**: AI explanation of view  
**Status**: âœ… PASSED  
**Duration**: 1517 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:50  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-explain-view -profile FKKTOTST -object INL.SYSDUMMY1 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:49.6425|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:49.6425|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:49.6425|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:49.6425|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:49.6425|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:49.6425|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-explain-view -profile FKKTOTST -object INL.SYSDUMMY1 -format json -Outfile CLI_Test_Output/temp_ai-explain-view_20251216_150027.json
2025-12-16 15:02:49.6425|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:49.6425|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:49.6425|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:49.6579|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:49.6759|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:49.6759|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:49.6759|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:49.6759|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:49.6759|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:49.6759|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:49.6759|DEBUG|WindowsDb2Editor.Services.Me
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "ai-explain-view",
  "view": "INL.SYSDUMMY1",
  "explanation": "AI analysis placeholder",
  "timestamp": "2025-12-16T15:02:50.7214457+01:00"
}
```

---

### âœ… Test #98: ai-analyze-procedure

**Category**: AI Features  
**Description**: AI analysis of procedure  
**Status**: âœ… PASSED  
**Duration**: 1474 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:52  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-analyze-procedure -profile FKKTOTST -object INL.ADMIN_CMD -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:51.1295|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:51.1385|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:51.1385|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:51.1385|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:51.1385|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:51.1385|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-analyze-procedure -profile FKKTOTST -object INL.ADMIN_CMD -format json -Outfile CLI_Test_Output/temp_ai-analyze-procedure_20251216_150027.json
2025-12-16 15:02:51.1385|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:51.1385|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:51.1385|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:51.1385|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:51.1664|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:51.1664|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:51.1664|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:51.1664|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:51.1734|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:51.1734|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:51.1734|DEBUG|WindowsDb2Editor.S
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "ai-analyze-procedure",
  "procedure": "INL.ADMIN_CMD",
  "analysis": "AI code analysis placeholder",
  "timestamp": "2025-12-16T15:02:52.2021306+01:00"
}
```

---

### âœ… Test #99: ai-analyze-function

**Category**: AI Features  
**Description**: AI analysis of function  
**Status**: âœ… PASSED  
**Duration**: 1527 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:53  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-analyze-function -profile FKKTOTST -object INL.ABS -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:52.6086|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:52.6176|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:52.6176|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:52.6176|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:52.6176|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:52.6176|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-analyze-function -profile FKKTOTST -object INL.ABS -format json -Outfile CLI_Test_Output/temp_ai-analyze-function_20251216_150027.json
2025-12-16 15:02:52.6176|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:52.6176|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:52.6176|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:52.6176|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:52.6465|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:52.6465|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:52.6465|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:52.6465|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:52.6465|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:52.6465|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:52.6465|DEBUG|WindowsDb2Editor.Services.
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "ai-analyze-function",
  "function": "INL.ABS",
  "analysis": "AI code analysis placeholder",
  "timestamp": "2025-12-16T15:02:53.7148556+01:00"
}
```

---

### âœ… Test #100: ai-analyze-package

**Category**: AI Features  
**Description**: AI analysis of package  
**Status**: âœ… PASSED  
**Duration**: 1448 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:55  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-analyze-package -profile FKKTOTST -object INL.SYSSH200 -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:54.1702|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:54.1792|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:54.1792|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:54.1792|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:54.1792|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:54.1792|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-analyze-package -profile FKKTOTST -object INL.SYSSH200 -format json -Outfile CLI_Test_Output/temp_ai-analyze-package_20251216_150027.json
2025-12-16 15:02:54.1792|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:54.1792|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:54.1792|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:54.1792|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:54.2091|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:54.2091|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:54.2091|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:54.2091|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:54.2091|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:54.2091|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:54.2198|DEBUG|WindowsDb2Editor.Servic
... (truncated, see full output in JSON report)
```
#### ðŸ“‹ JSON Result

```json
{
  "command": "ai-analyze-package",
  "package": "INL.SYSSH200",
  "analysis": "AI package analysis placeholder",
  "timestamp": "2025-12-16T15:02:55.1681357+01:00"
}
```

---

### âœ… Test #101: ai-deep-analysis

**Category**: AI Features  
**Description**: Deep AI analysis of table  
**Status**: âœ… PASSED  
**Duration**: 1391 ms  
**Exit Code**: 0  
**Timestamp**: 2025-12-16 15:02:56  

#### ðŸ“¥ CLI Input (Command Executed)

```bash
bin\Debug\net10.0-windows\WindowsDb2Editor.exe -command ai-deep-analysis -profile FKKTOTST -object INL.BILAGNR -format json
```

#### ðŸ“¤ Console Output

```
2025-12-16 15:02:55.5852|INFO|WindowsDb2Editor.Services.LoggingService|Application logging initialized
2025-12-16 15:02:55.5852|DEBUG|WindowsDb2Editor.Services.LoggingService|NLog configuration loaded from nlog.config
2025-12-16 15:02:55.5852|INFO|WindowsDb2Editor.App|===== WindowsDb2Editor Application Starting =====
2025-12-16 15:02:55.5852|INFO|WindowsDb2Editor.App|Framework: .NET 10
2025-12-16 15:02:55.5852|INFO|WindowsDb2Editor.App|Version: 1.0.0
2025-12-16 15:02:55.5852|DEBUG|WindowsDb2Editor.App|Startup arguments: -command ai-deep-analysis -profile FKKTOTST -object INL.BILAGNR -format json -Outfile CLI_Test_Output/temp_ai-deep-analysis_20251216_150027.json
2025-12-16 15:02:55.5852|DEBUG|WindowsDb2Editor.App|Initializing MetadataHandler
2025-12-16 15:02:55.5852|INFO|WindowsDb2Editor.Services.MetadataHandler|MetadataHandler initialized. ConfigFiles path: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles
2025-12-16 15:02:55.6019|INFO|WindowsDb2Editor.Services.MetadataHandler|Loading all metadata from ConfigFiles
2025-12-16 15:02:55.6019|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading supported providers from: C:\opt\src\WindowsDb2Editor\bin\Debug\net10.0-windows\ConfigFiles\supported_providers.json
2025-12-16 15:02:55.6211|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers from supported_providers.json
2025-12-16 15:02:55.6211|INFO|WindowsDb2Editor.Services.MetadataHandler|Loaded 1 providers
2025-12-16 15:02:55.6211|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for provider: DB2
2025-12-16 15:02:55.6211|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loading metadata for DB2 12.1
2025-12-16 15:02:55.6280|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 205 SQL statements from: db2_12.1_sql_statements.json
2025-12-16 15:02:55.6280|DEBUG|WindowsDb2Editor.Services.MetadataHandler|Loaded 119 texts from: db2_12.1_en-US_texts.json
2025-12-16 15:02:55.6280|DEBUG|WindowsDb2Editor.Services.Me
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
  "timestamp": "2025-12-16T15:02:56.5660684+01:00"
}
```

---

## Test Environment

- **Executable**: bin\Debug\net10.0-windows\WindowsDb2Editor.exe
- **Test Timestamp**: 20251216_150027
- **PowerShell Version**: 5.1.26100.7462
- **Test Script**: _cli_test_with_io_capture.ps1

---

*Report generated automatically by WindowsDb2Editor CLI Test Suite*
