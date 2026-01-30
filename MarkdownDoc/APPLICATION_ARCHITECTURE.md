# WindowsDb2Editor - Application Architecture

## Overview

WindowsDb2Editor is a **provider-agnostic database editor** for Windows, built with WPF/.NET 10. The architecture emphasizes:
- **Provider Independence**: Support for multiple databases (DB2, PostgreSQL, Oracle, SQL Server)
- **Factory Pattern**: All provider-specific code isolated to factories
- **Configuration-Driven**: SQL statements and metadata in JSON files
- **Modular Design**: Clear separation between UI, Services, and Data layers

## High-Level Architecture

```mermaid
graph TB
    subgraph "Presentation Layer"
        MW[MainWindow]
        TABS[ConnectionTabControl]
        DIALOGS[Dialogs]
        TREE[Object Browser]
    end
    
    subgraph "Service Layer"
        META[MetadataHandler]
        PREFS[PreferencesService]
        CONN_STOR[ConnectionStorageService]
        SQL_TRANS[SqlTranslationServices]
        EXPORT[ExportService]
        HISTORY[QueryHistoryService]
    end
    
    subgraph "Data Layer"
        FACTORY[ConnectionManagerFactory]
        ICONN[IConnectionManager]
        DB2[DB2ConnectionManager]
        PG[PostgreSQLConnectionManager]
        ORA[OracleConnectionManager]
    end
    
    subgraph "Configuration"
        JSON_SQL[*_sql_statements.json]
        JSON_CONN[connections.json]
        JSON_PREF[preferences.json]
        JSON_META[*_system_metadata.json]
    end
    
    subgraph "Database"
        DB[(Database Server)]
    end
    
    MW --> TABS
    MW --> DIALOGS
    TABS --> TREE
    
    TABS --> META
    DIALOGS --> META
    META --> JSON_SQL
    META --> JSON_META
    
    TABS --> FACTORY
    DIALOGS --> FACTORY
    FACTORY --> ICONN
    ICONN --> DB2
    ICONN --> PG
    ICONN --> ORA
    
    DB2 --> DB
    PG --> DB
    ORA --> DB
    
    CONN_STOR --> JSON_CONN
    PREFS --> JSON_PREF
```

## Core Design Patterns

### Factory Pattern (MANDATORY)

```mermaid
graph LR
    subgraph "UI/Services (Provider-Agnostic)"
        DIALOG[Any Dialog/Service]
        INTERFACE[IConnectionManager]
    end
    
    subgraph "Factory (ONLY Provider-Aware)"
        FACTORY[ConnectionManagerFactory]
    end
    
    subgraph "Provider Implementations"
        DB2[DB2ConnectionManager]
        PG[PostgreSQLConnectionManager]
        ORA[OracleConnectionManager]
        MSSQL[SqlServerConnectionManager]
    end
    
    DIALOG -->|"Uses interface"| INTERFACE
    DIALOG -->|"CreateConnectionManager()"| FACTORY
    FACTORY -->|"switch provider"| DB2
    FACTORY -->|"switch provider"| PG
    FACTORY -->|"switch provider"| ORA
    FACTORY -->|"switch provider"| MSSQL
    
    DB2 -.->|implements| INTERFACE
    PG -.->|implements| INTERFACE
    ORA -.->|implements| INTERFACE
    MSSQL -.->|implements| INTERFACE
```

### SQL Abstraction Pattern

```mermaid
graph TB
    subgraph "Application Code"
        SVC[Any Service]
        CODE["GetStatement('GetTableColumns')"]
    end
    
    subgraph "MetadataHandler"
        MH[MetadataHandler]
        CACHE[Statement Cache]
    end
    
    subgraph "JSON Configuration"
        DB2_SQL[db2_12.1_sql_statements.json]
        PG_SQL[postgresql_16.0_sql_statements.json]
        ORA_SQL[oracle_19c_sql_statements.json]
    end
    
    SVC --> CODE
    CODE --> MH
    MH --> CACHE
    MH -->|"provider=DB2"| DB2_SQL
    MH -->|"provider=PostgreSQL"| PG_SQL
    MH -->|"provider=Oracle"| ORA_SQL
    
    DB2_SQL -->|"SYSCAT.COLUMNS"| RESULT[SQL Statement]
    PG_SQL -->|"information_schema.columns"| RESULT
    ORA_SQL -->|"ALL_TAB_COLUMNS"| RESULT
```

## Folder Structure

```mermaid
graph TB
    ROOT[WindowsDb2Editor/]
    
    subgraph "UI Layer"
        CONTROLS[Controls/]
        DIALOGS[Dialogs/]
        MAIN[MainWindow.xaml]
    end
    
    subgraph "Business Logic"
        SERVICES[Services/]
        UTILS[Utils/]
    end
    
    subgraph "Data Access"
        DATA[Data/]
        PROVIDERS[Data/Providers/]
    end
    
    subgraph "Models"
        MODELS[Models/]
    end
    
    subgraph "Configuration"
        CONFIG[ConfigFiles/]
        RESOURCES[Resources/]
    end
    
    subgraph "Companion Apps"
        TRAY[WindowsDb2EditorTray/]
    end
    
    ROOT --> CONTROLS
    ROOT --> DIALOGS
    ROOT --> MAIN
    ROOT --> SERVICES
    ROOT --> UTILS
    ROOT --> DATA
    DATA --> PROVIDERS
    ROOT --> MODELS
    ROOT --> CONFIG
    ROOT --> RESOURCES
    ROOT --> TRAY
```

## Connection Flow

```mermaid
sequenceDiagram
    participant User
    participant Dialog as ConnectionDialog
    participant Storage as ConnectionStorageService
    participant Factory as ConnectionManagerFactory
    participant ConnMgr as IConnectionManager
    participant DB as Database
    
    User->>Dialog: Enter connection details
    Dialog->>Storage: SaveConnection(profile)
    Storage->>Storage: Write to connections.json
    
    User->>Dialog: Click "Connect"
    Dialog->>Factory: CreateConnectionManager(profile)
    Factory->>Factory: Switch on ProviderType
    Factory-->>ConnMgr: Return DB2/PG/Oracle ConnectionManager
    
    Dialog->>ConnMgr: ConnectAsync()
    ConnMgr->>DB: Open connection
    DB-->>ConnMgr: Connected
    ConnMgr-->>Dialog: Success
    
    Dialog->>User: Show connected tab
```

## Query Execution Flow

```mermaid
sequenceDiagram
    participant User
    participant Editor as SQL Editor
    participant Tab as ConnectionTabControl
    participant ConnMgr as IConnectionManager
    participant History as QueryHistoryService
    participant DB as Database
    
    User->>Editor: Type SQL query
    User->>Editor: Press F5 (Execute)
    
    Editor->>Tab: ExecuteQueryAsync(sql)
    Tab->>History: SaveQuery(sql)
    Tab->>ConnMgr: ExecuteQueryAsync(sql)
    
    ConnMgr->>DB: Execute SQL
    DB-->>ConnMgr: DataTable results
    
    ConnMgr-->>Tab: Results
    Tab->>Editor: Display in DataGrid
    Tab->>User: Show row count, timing
```

## Object Browser Architecture

```mermaid
graph TB
    subgraph "Object Browser Tree"
        ROOT[Database Root]
        SCHEMAS[Schemas Node]
        TABLES[Tables Node]
        VIEWS[Views Node]
        PROCS[Procedures Node]
        FUNCS[Functions Node]
    end
    
    subgraph "Lazy Loading"
        EXPAND[On Node Expand]
        LOAD[Load Children]
        META[MetadataHandler]
    end
    
    subgraph "Context Actions"
        CTX[Right-Click Menu]
        PROPS[View Properties]
        DDL[Generate DDL]
        SAMPLE[Sample Data]
        EXPORT[Export]
    end
    
    ROOT --> SCHEMAS
    SCHEMAS --> TABLES
    SCHEMAS --> VIEWS
    SCHEMAS --> PROCS
    SCHEMAS --> FUNCS
    
    EXPAND --> LOAD
    LOAD --> META
    META --> |SQL Query| DB[(Database)]
    
    TABLES --> CTX
    VIEWS --> CTX
    CTX --> PROPS
    CTX --> DDL
    CTX --> SAMPLE
    CTX --> EXPORT
```

## Tray Application Architecture

```mermaid
graph TB
    subgraph "Main Application"
        MAIN[WindowsDb2Editor.exe]
        STARTUP[App.xaml.cs OnStartup]
    end
    
    subgraph "Tray Application"
        TRAY[WindowsDb2EditorTray.exe]
        ICON[System Tray Icon]
        MENU[Context Menu]
        WATCH[NotificationFolderWatcher]
    end
    
    subgraph "Communication"
        FOLDER[Notifications Folder]
        PROFILES[connections.json]
    end
    
    subgraph "User Actions"
        CONNECT[Quick Connect to Profile]
        OPEN[Open Main App]
        EXIT[Exit Tray]
    end
    
    STARTUP -->|"Process.Start()"| TRAY
    TRAY --> ICON
    ICON --> MENU
    
    MENU --> CONNECT
    MENU --> OPEN
    MENU --> EXIT
    
    TRAY --> WATCH
    WATCH --> FOLDER
    TRAY -->|"Read profiles"| PROFILES
    
    CONNECT -->|"Launch with -profile"| MAIN
```

## CLI Architecture

```mermaid
graph TB
    subgraph "CLI Entry"
        ARGS[Command Line Arguments]
        PARSER[CliArgumentParser]
    end
    
    subgraph "Command Handler"
        CLI[CliCommandHandlerService]
        DISPATCH[Command Dispatcher]
    end
    
    subgraph "Commands (119+)"
        SCHEMA[Schema Commands]
        TABLE[Table Commands]
        VIEW[View Commands]
        PROC[Procedure Commands]
        EXPORT[Export Commands]
        AI[AI Commands]
        MERMAID[Mermaid Commands]
    end
    
    subgraph "Output"
        JSON[JSON Output]
        CSV[CSV Output]
        TSV[TSV Output]
    end
    
    ARGS --> PARSER
    PARSER --> CLI
    CLI --> DISPATCH
    
    DISPATCH --> SCHEMA
    DISPATCH --> TABLE
    DISPATCH --> VIEW
    DISPATCH --> PROC
    DISPATCH --> EXPORT
    DISPATCH --> AI
    DISPATCH --> MERMAID
    
    SCHEMA --> JSON
    TABLE --> JSON
    TABLE --> CSV
    EXPORT --> TSV
```

## Configuration Files

| File | Location | Purpose |
|------|----------|---------|
| `connections.json` | User Documents | Saved connection profiles |
| `preferences.json` | User Documents | User preferences (theme, fonts) |
| `query-history.json` | User Documents | Recent query history |
| `db2_12.1_sql_statements.json` | ConfigFiles/ | DB2 12.1 SQL queries |
| `db2_12.1_system_metadata.json` | ConfigFiles/ | DB2 system catalog reference |
| `supported_providers.json` | ConfigFiles/ | List of supported databases |
| `nlog.config` | App Root | Logging configuration |
| `appsettings.json` | App Root | Application settings |

## User Data Folder Structure

```
C:\Users\<user>\Documents\WindowsDb2Editor\
├── connections.json              (saved profiles)
├── preferences.json              (user settings)
├── query-history.json            (query history)
├── missing-fk-search-history.json
├── missing-fk-ignore-history.json
└── MissingFK\
    ├── running_job.json          (PID tracking)
    ├── IgnorePatterns\           (ignore pattern files)
    │   └── *.json
    └── Projects\                 (job outputs)
        └── 20260129_143052_SCHEMA_TABLE1_TABLE2\
            ├── missing_fk_input.json
            ├── missing_fk_results.json
            └── job_*.log
```

## Key Interfaces

### IConnectionManager

```csharp
public interface IConnectionManager
{
    // Connection
    Task<bool> ConnectAsync();
    Task DisconnectAsync();
    bool IsConnected { get; }
    
    // Query Execution
    Task<DataTable> ExecuteQueryAsync(string sql, params object[] parameters);
    Task<int> ExecuteNonQueryAsync(string sql, params object[] parameters);
    Task<object?> ExecuteScalarAsync(string sql, params object[] parameters);
    
    // Connection Info
    IConnectionInfo ConnectionInfo { get; }
}
```

### IConnectionInfo

```csharp
public interface IConnectionInfo
{
    string? Server { get; }
    int Port { get; }
    string? Database { get; }
    string? Username { get; }
    string? ProviderType { get; }
    string ProfileName { get; }
}
```

## Provider Support Matrix

| Feature | DB2 12.1 | PostgreSQL | Oracle | SQL Server |
|---------|----------|------------|--------|------------|
| Connection | ✅ | 🔲 Planned | 🔲 Planned | 🔲 Planned |
| Object Browser | ✅ | 🔲 | 🔲 | 🔲 |
| Query Execution | ✅ | 🔲 | 🔲 | 🔲 |
| DDL Generation | ✅ | 🔲 | 🔲 | 🔲 |
| IntelliSense | ✅ | 🔲 | 🔲 | 🔲 |
| Export (CSV/JSON) | ✅ | 🔲 | 🔲 | 🔲 |
| Mermaid ERD | ✅ | 🔲 | 🔲 | 🔲 |
| Missing FK Discovery | ✅ | 🔲 | 🔲 | 🔲 |

## Adding a New Database Provider

To add support for a new database (e.g., PostgreSQL):

1. **Create Connection Manager**: `Data/Providers/PostgreSQL/PostgreSQLConnectionManager.cs`
2. **Create SQL Statements**: `ConfigFiles/postgresql_16.0_sql_statements.json`
3. **Create System Metadata**: `ConfigFiles/postgresql_16.0_system_metadata.json`
4. **Create Syntax Highlighting**: `Resources/SyntaxHighlighting/PostgreSQLSQL.xshd`
5. **Update Factory**: Add case in `ConnectionManagerFactory.cs`
6. **Update Supported Providers**: Add to `supported_providers.json`

**No changes needed to**: UI code, dialogs, services, or other components.

## Technology Stack

| Component | Technology |
|-----------|------------|
| Framework | .NET 10 (net10.0-windows) |
| UI Framework | WPF |
| Theme | ModernWpfUI |
| SQL Editor | AvalonEdit |
| SQL Formatter | PoorMansTSqlFormatter |
| DB2 Driver | IBM.Data.Db2 (9.0.0.400) |
| Logging | NLog |
| Serialization | System.Text.Json |

## Summary

The WindowsDb2Editor architecture is designed for:

1. **Extensibility**: Add new database providers via configuration
2. **Maintainability**: Single source of truth for SQL and UI text
3. **Testability**: Interface-based design enables mocking
4. **User Experience**: Professional UI with dark/light themes
5. **Automation**: Full CLI support for scripting
