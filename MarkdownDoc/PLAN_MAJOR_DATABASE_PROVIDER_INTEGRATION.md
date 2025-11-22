# Plan: Major Database Provider Integration

**Document Version:** 1.0  
**Created:** 2025-11-22  
**Status:** Planning  
**Branch:** cursor/plan-major-database-provider-integration-claude-4.5-sonnet-thinking-2686

---

## Executive Summary

Transform **WindowsDb2Editor** from a DB2-specific tool into a multi-database management platform supporting all major database providers while maintaining the current DB2 functionality and offline deployment capability.

### Target Database Providers

1. **IBM DB2** (Current - Fully Implemented)
2. **Microsoft SQL Server**
3. **Oracle Database**
4. **PostgreSQL**
5. **MySQL**
6. **MariaDB**
7. **SQLite**
8. **MongoDB** (NoSQL - Optional Phase 2)

---

## Phase 1: Architecture Refactoring

### 1.1 Create Database Provider Abstraction Layer

**Goal:** Decouple database-specific logic from core application logic.

#### New Interfaces

```csharp
// Services/Database/IDbProvider.cs
public interface IDbProvider
{
    string ProviderName { get; }
    string DisplayName { get; }
    DbProviderType ProviderType { get; }
    
    IDbConnection CreateConnection(string connectionString);
    IDbCommand CreateCommand(string sql, IDbConnection connection);
    IDbDataAdapter CreateDataAdapter(IDbCommand command);
    IDbParameter CreateParameter(string name, object value);
    
    string BuildConnectionString(ConnectionParameters parameters);
    string GetConnectionStringTemplate();
    
    // Metadata operations
    Task<List<string>> GetDatabasesAsync(IDbConnection connection);
    Task<List<DbTable>> GetTablesAsync(IDbConnection connection, string schema);
    Task<List<DbColumn>> GetColumnsAsync(IDbConnection connection, string schema, string table);
    Task<List<DbIndex>> GetIndexesAsync(IDbConnection connection, string schema, string table);
    
    // Provider-specific SQL generation
    string GetTableDefinitionSql(string schema, string table);
    string GetViewDefinitionSql(string schema, string view);
    string GetProcedureDefinitionSql(string schema, string procedure);
    
    // Dialect-specific features
    bool SupportsSchemaConcept { get; }
    bool SupportsStoredProcedures { get; }
    bool SupportsSequences { get; }
    string DefaultSchema { get; }
}

// Models/Database/DbProviderType.cs
public enum DbProviderType
{
    DB2,
    SqlServer,
    Oracle,
    PostgreSQL,
    MySQL,
    MariaDB,
    SQLite,
    MongoDB
}

// Models/Database/ConnectionParameters.cs
public class ConnectionParameters
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Database { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IntegratedSecurity { get; set; }
    public Dictionary<string, string> AdditionalParameters { get; set; }
}
```

#### Provider Implementations

**File Structure:**
```
Services/
└── Database/
    ├── IDbProvider.cs
    ├── Providers/
    │   ├── Db2Provider.cs          (Migrate existing DB2 logic)
    │   ├── SqlServerProvider.cs
    │   ├── OracleProvider.cs
    │   ├── PostgreSqlProvider.cs
    │   ├── MySqlProvider.cs
    │   ├── MariaDbProvider.cs
    │   └── SqliteProvider.cs
    └── DbProviderFactory.cs
```

### 1.2 Provider Factory Pattern

```csharp
// Services/Database/DbProviderFactory.cs
public class DbProviderFactory
{
    private readonly ILogger _logger;
    private readonly Dictionary<DbProviderType, IDbProvider> _providers;
    
    public DbProviderFactory(ILogger logger)
    {
        _logger = logger;
        _providers = new Dictionary<DbProviderType, IDbProvider>
        {
            { DbProviderType.DB2, new Db2Provider() },
            { DbProviderType.SqlServer, new SqlServerProvider() },
            { DbProviderType.Oracle, new OracleProvider() },
            { DbProviderType.PostgreSQL, new PostgreSqlProvider() },
            { DbProviderType.MySQL, new MySqlProvider() },
            { DbProviderType.MariaDB, new MariaDbProvider() },
            { DbProviderType.SQLite, new SqliteProvider() }
        };
    }
    
    public IDbProvider GetProvider(DbProviderType providerType)
    {
        if (_providers.TryGetValue(providerType, out var provider))
        {
            _logger.Debug("Retrieved provider: {ProviderType}", providerType);
            return provider;
        }
        
        throw new NotSupportedException($"Database provider {providerType} is not supported");
    }
    
    public IEnumerable<IDbProvider> GetAllProviders() => _providers.Values;
}
```

### 1.3 Refactor Existing DB2 Code

**Tasks:**
- Extract DB2-specific logic from `DB2ConnectionManager` into `Db2Provider`
- Update `ConnectionTabControl.xaml.cs` to use `IDbProvider`
- Migrate query execution logic to use abstraction layer
- Update all Services to work with `IDbProvider` instead of DB2-specific types

---

## Phase 2: NuGet Package Integration

### 2.1 Required NuGet Packages

Add to `.csproj`:

```xml
<!-- Microsoft SQL Server -->
<PackageReference Include="Microsoft.Data.SqlClient" Version="5.2.0" />

<!-- Oracle Database -->
<PackageReference Include="Oracle.ManagedDataAccess.Core" Version="23.5.0" />

<!-- PostgreSQL -->
<PackageReference Include="Npgsql" Version="8.0.3" />

<!-- MySQL -->
<PackageReference Include="MySql.Data" Version="8.4.0" />

<!-- MariaDB (compatible with MySQL) -->
<!-- Uses MySql.Data package -->

<!-- SQLite -->
<PackageReference Include="Microsoft.Data.Sqlite" Version="8.0.5" />
<PackageReference Include="SQLitePCLRaw.bundle_e_sqlite3" Version="2.1.6" />

<!-- MongoDB (Optional - Phase 2) -->
<PackageReference Include="MongoDB.Driver" Version="2.28.0" />
```

### 2.2 Offline Deployment Considerations

**Challenge:** Package all native drivers for offline deployment.

**Solution:**
1. Use `--self-contained` publish mode
2. Include all native libraries (e.g., Oracle OCI, PostgreSQL libpq)
3. Create provider-specific deployment packages
4. Add provider selection during installation

**Deployment Structure:**
```
WindowsDb2Editor/
├── WindowsDb2Editor.exe
├── Providers/
│   ├── DB2/           (IBM.Data.Db2 native libs)
│   ├── SqlServer/     (Microsoft.Data.SqlClient libs)
│   ├── Oracle/        (Oracle.ManagedDataAccess libs)
│   ├── PostgreSQL/    (Npgsql + libpq)
│   ├── MySQL/         (MySql.Data libs)
│   └── SQLite/        (SQLite native libs)
├── nlog.config
└── appsettings.json
```

---

## Phase 3: UI/UX Changes

### 3.1 Connection Dialog Redesign

**File:** `Dialogs/ConnectDialog.xaml`

**New Features:**
1. **Database Provider Dropdown** (ComboBox at top)
   - Display: "IBM DB2", "SQL Server", "Oracle", "PostgreSQL", "MySQL", "MariaDB", "SQLite"
   - Icon for each provider
   
2. **Dynamic Connection Fields**
   - Show/hide fields based on selected provider
   - SQL Server: Add "Integrated Security" checkbox
   - SQLite: Replace Host/Port with "File Path" browser
   - Oracle: Add "Service Name" vs "SID" option
   
3. **Connection String Preview**
   - Real-time display of connection string
   - Mask password in preview
   
4. **Test Connection Button**
   - Provider-specific connection testing
   - Show provider version info on success

**Mockup:**
```
┌─────────────────────────────────────────────┐
│  Connect to Database                    [X] │
├─────────────────────────────────────────────┤
│  Database Type: [▼ IBM DB2          ]      │
│                                             │
│  Host:          [localhost          ]      │
│  Port:          [50000              ]      │
│  Database:      [SAMPLE             ]      │
│  Username:      [db2admin           ]      │
│  Password:      [●●●●●●●●●●●●       ]      │
│                                             │
│  ☐ Save Password                           │
│  ☐ Integrated Security (Windows Auth)     │
│                                             │
│  Connection String (preview):              │
│  Server=localhost:50000;Database=...       │
│                                             │
│  [Test Connection]  [Connect]  [Cancel]    │
└─────────────────────────────────────────────┘
```

### 3.2 Rename Application

**Current:** WindowsDb2Editor  
**New:** WindowsDbEditor (or DatabaseEditor, MultiDbEditor)

**Updates Required:**
1. Solution name
2. Assembly name
3. Window titles
4. About dialog
5. Application manifest
6. Deployment scripts
7. Documentation

### 3.3 Provider-Specific Icons

**Tab Headers:**
```
[DB2 Icon] SAMPLE @ localhost:50000      [X]
[SQL Icon] Northwind @ sql-server        [X]
[PG Icon]  postgres @ 192.168.1.100      [X]
```

**Status Bar:**
```
Connected to: [Oracle Icon] Oracle Database 19c | User: SCOTT | Schema: HR
```

---

## Phase 4: Syntax Highlighting & Formatting

### 4.1 Multi-Dialect SQL Syntax Highlighting

**Challenge:** Each database has different SQL dialect.

**Solution:** Create provider-specific XSHD files.

**File Structure:**
```
Resources/
├── SyntaxHighlighting/
│   ├── DB2SQL.xshd           (Current)
│   ├── TSQL.xshd             (SQL Server)
│   ├── PLSQL.xshd            (Oracle)
│   ├── PostgreSQL.xshd
│   ├── MySQL.xshd
│   └── SQLite.xshd
```

**Dynamic Highlighting:**
```csharp
// In ConnectionTabControl.xaml.cs
private void SetSyntaxHighlighting(DbProviderType providerType)
{
    var xshdFile = providerType switch
    {
        DbProviderType.DB2 => "DB2SQL.xshd",
        DbProviderType.SqlServer => "TSQL.xshd",
        DbProviderType.Oracle => "PLSQL.xshd",
        DbProviderType.PostgreSQL => "PostgreSQL.xshd",
        DbProviderType.MySQL => "MySQL.xshd",
        DbProviderType.MariaDB => "MySQL.xshd",
        DbProviderType.SQLite => "SQLite.xshd",
        _ => "DB2SQL.xshd"
    };
    
    SqlEditor.SyntaxHighlighting = LoadXshd(xshdFile);
}
```

### 4.2 SQL Formatting Support

**Current:** PoorMansTSqlFormatter (T-SQL only)

**Options:**
1. Keep PoorMansTSqlFormatter for SQL Server
2. Add separate formatters for other dialects
3. **Recommended:** Use **SqlFormatter** NuGet package (multi-dialect)

```xml
<PackageReference Include="SqlFormatter" Version="1.0.0" />
```

**Implementation:**
```csharp
// Services/SqlFormatterService.cs
public string FormatSql(string sql, DbProviderType providerType)
{
    return providerType switch
    {
        DbProviderType.DB2 => SqlFormatter.Format(sql, SqlDialect.Db2),
        DbProviderType.SqlServer => SqlFormatter.Format(sql, SqlDialect.TSql),
        DbProviderType.Oracle => SqlFormatter.Format(sql, SqlDialect.PlSql),
        DbProviderType.PostgreSQL => SqlFormatter.Format(sql, SqlDialect.PostgreSql),
        DbProviderType.MySQL => SqlFormatter.Format(sql, SqlDialect.MySql),
        DbProviderType.MariaDB => SqlFormatter.Format(sql, SqlDialect.MySql),
        DbProviderType.SQLite => SqlFormatter.Format(sql, SqlDialect.Sqlite),
        _ => sql
    };
}
```

---

## Phase 5: Metadata Browsing

### 5.1 Provider-Specific Metadata Queries

Each provider has different system catalogs:

| Provider   | System Catalog           | Schema Query              |
|------------|--------------------------|---------------------------|
| DB2        | SYSCAT                   | `SELECT * FROM SYSCAT.TABLES` |
| SQL Server | INFORMATION_SCHEMA, sys  | `SELECT * FROM sys.tables` |
| Oracle     | ALL_TABLES, DBA_TABLES   | `SELECT * FROM ALL_TABLES` |
| PostgreSQL | information_schema, pg_catalog | `SELECT * FROM pg_tables` |
| MySQL      | INFORMATION_SCHEMA       | `SHOW TABLES` |
| MariaDB    | INFORMATION_SCHEMA       | `SHOW TABLES` |
| SQLite     | sqlite_master            | `SELECT * FROM sqlite_master` |

### 5.2 Object Browser Adaptation

**Current:** DB2-specific object browser with SYSCAT queries

**New:** Provider-agnostic object browser

**Implementation:**
```csharp
// Each provider implements:
public class SqlServerProvider : IDbProvider
{
    public async Task<List<DbTable>> GetTablesAsync(IDbConnection connection, string schema)
    {
        var sql = @"
            SELECT 
                TABLE_SCHEMA AS SchemaName,
                TABLE_NAME AS TableName,
                TABLE_TYPE AS TableType
            FROM INFORMATION_SCHEMA.TABLES
            WHERE TABLE_SCHEMA = @Schema
            ORDER BY TABLE_NAME";
        
        // Execute and return
    }
    
    public async Task<List<DbColumn>> GetColumnsAsync(IDbConnection connection, string schema, string table)
    {
        var sql = @"
            SELECT 
                COLUMN_NAME AS ColumnName,
                DATA_TYPE AS DataType,
                CHARACTER_MAXIMUM_LENGTH AS Length,
                IS_NULLABLE AS IsNullable
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_SCHEMA = @Schema AND TABLE_NAME = @Table
            ORDER BY ORDINAL_POSITION";
        
        // Execute and return
    }
}
```

---

## Phase 6: Feature Compatibility Matrix

### 6.1 Feature Support by Provider

| Feature                    | DB2 | SQL Server | Oracle | PostgreSQL | MySQL | MariaDB | SQLite |
|----------------------------|-----|------------|--------|------------|-------|---------|--------|
| **Core Features**          |     |            |        |            |       |         |        |
| Multiple Connections       | ✅  | ✅         | ✅     | ✅         | ✅    | ✅      | ✅     |
| Query Execution            | ✅  | ✅         | ✅     | ✅         | ✅    | ✅      | ✅     |
| SQL Formatting             | ✅  | ✅         | ✅     | ✅         | ✅    | ✅      | ✅     |
| Syntax Highlighting        | ✅  | ✅         | ✅     | ✅         | ✅    | ✅      | ✅     |
| **Metadata Features**      |     |            |        |            |       |         |        |
| Table Browser              | ✅  | ✅         | ✅     | ✅         | ✅    | ✅      | ✅     |
| View Browser               | ✅  | ✅         | ✅     | ✅         | ✅    | ✅      | ✅     |
| Stored Procedures          | ✅  | ✅         | ✅     | ✅         | ✅    | ✅      | ❌     |
| Sequences                  | ✅  | ✅         | ✅     | ✅         | ❌    | ❌      | ❌     |
| Triggers                   | ✅  | ✅         | ✅     | ✅         | ✅    | ✅      | ✅     |
| **Monitoring Features**    |     |            |        |            |       |         |        |
| Active Sessions            | ✅  | ✅         | ✅     | ✅         | ✅    | ✅      | ❌     |
| Lock Monitor               | ✅  | ✅         | ✅     | ✅         | ⚠️    | ⚠️      | ❌     |
| Database Load              | ✅  | ✅         | ✅     | ✅         | ⚠️    | ⚠️      | ❌     |
| Performance Stats          | ✅  | ✅         | ✅     | ✅         | ⚠️    | ⚠️      | ❌     |
| **Advanced Features**      |     |            |        |            |       |         |        |
| Dependency Graph           | ✅  | ✅         | ✅     | ✅         | ⚠️    | ⚠️      | ❌     |
| CDC Manager                | ✅  | ✅         | ⚠️     | ⚠️         | ❌    | ❌      | ❌     |
| Migration Assistant        | ✅  | ✅         | ✅     | ✅         | ✅    | ✅      | ⚠️     |
| Statistics Manager         | ✅  | ✅         | ✅     | ✅         | ✅    | ✅      | ❌     |

**Legend:**
- ✅ Fully Supported
- ⚠️ Partially Supported (limited functionality)
- ❌ Not Supported (database limitation)

### 6.2 Feature Enabling/Disabling

**Implementation:**
```csharp
// In IDbProvider
public bool SupportsFeature(DatabaseFeature feature)
{
    return feature switch
    {
        DatabaseFeature.StoredProcedures => SupportsStoredProcedures,
        DatabaseFeature.Sequences => SupportsSequences,
        DatabaseFeature.ActiveSessions => SupportsSessionMonitoring,
        DatabaseFeature.LockMonitoring => SupportsLockMonitoring,
        DatabaseFeature.CDC => SupportsCDC,
        _ => false
    };
}

// In MainWindow.xaml.cs
private void UpdateMenuBasedOnProvider(IDbProvider provider)
{
    // Disable menu items for unsupported features
    MenuItemCdc.IsEnabled = provider.SupportsFeature(DatabaseFeature.CDC);
    MenuItemLockMonitor.IsEnabled = provider.SupportsFeature(DatabaseFeature.LockMonitoring);
    MenuItemActiveSessions.IsEnabled = provider.SupportsFeature(DatabaseFeature.ActiveSessions);
}
```

---

## Phase 7: Configuration Management

### 7.1 Recent Connections Management

**Current:** DB2 connections only

**New:** Multi-provider recent connections

**Model:**
```csharp
// Models/ConnectionProfile.cs
public class ConnectionProfile
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DbProviderType ProviderType { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Database { get; set; }
    public string Username { get; set; }
    public bool SavePassword { get; set; }
    public string EncryptedPassword { get; set; }
    public bool IntegratedSecurity { get; set; }
    public DateTime LastConnected { get; set; }
    public Dictionary<string, string> AdditionalSettings { get; set; }
}
```

**Storage:** `ConfigFiles/RecentConnections.json`

```json
{
  "Connections": [
    {
      "Id": "guid-1",
      "Name": "Production DB2",
      "ProviderType": "DB2",
      "Host": "db2-prod.company.com",
      "Port": 50000,
      "Database": "PRODDB",
      "Username": "admin",
      "SavePassword": false,
      "LastConnected": "2025-11-22T10:30:00"
    },
    {
      "Id": "guid-2",
      "Name": "Dev SQL Server",
      "ProviderType": "SqlServer",
      "Host": "sql-dev",
      "Port": 1433,
      "Database": "DevDB",
      "IntegratedSecurity": true,
      "LastConnected": "2025-11-21T15:45:00"
    }
  ]
}
```

### 7.2 Provider-Specific Settings

**File:** `ConfigFiles/ProviderSettings.json`

```json
{
  "DB2": {
    "DefaultPort": 50000,
    "CommandTimeout": 30,
    "EnableConnectionPooling": true
  },
  "SqlServer": {
    "DefaultPort": 1433,
    "CommandTimeout": 30,
    "MultipleActiveResultSets": true,
    "TrustServerCertificate": false
  },
  "Oracle": {
    "DefaultPort": 1521,
    "CommandTimeout": 30,
    "UseServiceName": true
  },
  "PostgreSQL": {
    "DefaultPort": 5432,
    "CommandTimeout": 30,
    "ApplicationName": "WindowsDbEditor"
  },
  "MySQL": {
    "DefaultPort": 3306,
    "CommandTimeout": 30,
    "AllowUserVariables": true
  },
  "SQLite": {
    "DefaultFileExtension": ".db",
    "EnableForeignKeys": true,
    "JournalMode": "WAL"
  }
}
```

---

## Phase 8: Testing Strategy

### 8.1 Test Database Setup

**Docker Compose for Test Databases:**

```yaml
version: '3.8'

services:
  db2:
    image: ibmcom/db2:11.5.9.0
    environment:
      - LICENSE=accept
      - DB2INST1_PASSWORD=password
    ports:
      - "50000:50000"
  
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourStrong@Passw0rd
    ports:
      - "1433:1433"
  
  oracle:
    image: container-registry.oracle.com/database/express:21.3.0-xe
    environment:
      - ORACLE_PWD=password
    ports:
      - "1521:1521"
  
  postgres:
    image: postgres:16
    environment:
      - POSTGRES_PASSWORD=password
    ports:
      - "5432:5432"
  
  mysql:
    image: mysql:8.3
    environment:
      - MYSQL_ROOT_PASSWORD=password
    ports:
      - "3306:3306"
  
  mariadb:
    image: mariadb:11.3
    environment:
      - MYSQL_ROOT_PASSWORD=password
    ports:
      - "3307:3306"
```

### 8.2 Manual Test Checklist

**Per Provider:**
- [ ] Connect to database
- [ ] Browse tables, views, procedures
- [ ] Execute SELECT query
- [ ] Execute INSERT/UPDATE/DELETE
- [ ] Format SQL
- [ ] View table properties
- [ ] View column details
- [ ] Export query results
- [ ] Dark/Light theme switching
- [ ] Multiple simultaneous connections
- [ ] Connection timeout handling
- [ ] Invalid credentials handling
- [ ] Query cancellation

### 8.3 Automated Testing

**Test Project:** `WindowsDbEditor.Tests`

```csharp
[TestClass]
public class SqlServerProviderTests
{
    private SqlServerProvider _provider;
    
    [TestInitialize]
    public void Setup()
    {
        _provider = new SqlServerProvider();
    }
    
    [TestMethod]
    public void BuildConnectionString_WithValidParameters_ReturnsCorrectString()
    {
        var parameters = new ConnectionParameters
        {
            Host = "localhost",
            Port = 1433,
            Database = "TestDB",
            Username = "sa",
            Password = "pass"
        };
        
        var connectionString = _provider.BuildConnectionString(parameters);
        
        Assert.IsTrue(connectionString.Contains("Server=localhost,1433"));
        Assert.IsTrue(connectionString.Contains("Database=TestDB"));
    }
}
```

---

## Phase 9: Migration Path

### 9.1 Backward Compatibility

**Requirement:** Existing DB2 users should not experience breaking changes.

**Strategy:**
1. Keep all existing DB2 functionality intact
2. Default provider type is DB2 if not specified
3. Migrate existing connection profiles to new format
4. Support old connection string format

### 9.2 Migration Script

```csharp
// Services/ConnectionMigrationService.cs
public class ConnectionMigrationService
{
    public void MigrateOldConnectionsToNewFormat()
    {
        Logger.Info("Starting connection migration");
        
        // Read old RecentConnections.json (DB2-only format)
        var oldConnections = ReadOldConnections();
        
        // Convert to new format (multi-provider)
        var newConnections = oldConnections.Select(old => new ConnectionProfile
        {
            Id = Guid.NewGuid(),
            Name = $"{old.Database} @ {old.Host}",
            ProviderType = DbProviderType.DB2,  // Default to DB2
            Host = old.Host,
            Port = old.Port,
            Database = old.Database,
            Username = old.Username,
            SavePassword = old.SavePassword,
            EncryptedPassword = old.EncryptedPassword,
            LastConnected = old.LastConnected
        }).ToList();
        
        // Save new format
        SaveNewConnections(newConnections);
        
        // Backup old file
        BackupOldConnectionFile();
        
        Logger.Info("Connection migration completed: {Count} connections migrated", newConnections.Count);
    }
}
```

---

## Phase 10: Documentation Updates

### 10.1 User Guide

**New Sections:**
1. Supported Database Providers
2. Provider-Specific Connection Instructions
3. Feature Availability Matrix
4. Syntax Highlighting for Each Dialect
5. Provider-Specific Keyboard Shortcuts
6. Migration Guide (from DB2-only to multi-provider)

### 10.2 Developer Documentation

**New Files:**
- `MarkdownDoc/PROVIDER_INTEGRATION_GUIDE.md`
- `MarkdownDoc/ADDING_NEW_PROVIDER.md`
- `MarkdownDoc/PROVIDER_API_REFERENCE.md`

### 10.3 README Updates

**Update:** Root `README.md`

```markdown
# WindowsDbEditor - Multi-Database Management Tool

A professional database management tool for Windows 11 supporting multiple database providers.

## Supported Databases

- ✅ IBM DB2
- ✅ Microsoft SQL Server
- ✅ Oracle Database
- ✅ PostgreSQL
- ✅ MySQL
- ✅ MariaDB
- ✅ SQLite

## Key Features

- 🔌 Multi-provider support
- 🌙 Dark/Light themes
- ✨ Syntax highlighting (provider-specific)
- 🎨 SQL auto-formatting
- 📊 Advanced monitoring (provider-dependent)
- 🔒 Offline deployment capability
- ...
```

---

## Implementation Roadmap

### Sprint 1: Foundation (Week 1-2)
**Tasks:**
- [x] Create plan document
- [ ] Design `IDbProvider` interface
- [ ] Create `DbProviderFactory`
- [ ] Implement `Db2Provider` (migrate existing code)
- [ ] Update project structure
- [ ] Add NuGet packages for all providers

**Deliverable:** Working abstraction layer with DB2 provider

### Sprint 2: SQL Server Support (Week 3)
**Tasks:**
- [ ] Implement `SqlServerProvider`
- [ ] Create T-SQL syntax highlighting (TSQL.xshd)
- [ ] Add SQL Server to connection dialog
- [ ] Test metadata queries (tables, columns, indexes)
- [ ] Implement SQL Server-specific features

**Deliverable:** Full SQL Server support

### Sprint 3: Oracle Support (Week 4)
**Tasks:**
- [ ] Implement `OracleProvider`
- [ ] Create PL/SQL syntax highlighting (PLSQL.xshd)
- [ ] Handle Oracle-specific connection (Service Name vs SID)
- [ ] Test metadata queries
- [ ] Implement Oracle-specific features

**Deliverable:** Full Oracle support

### Sprint 4: PostgreSQL Support (Week 5)
**Tasks:**
- [ ] Implement `PostgreSqlProvider`
- [ ] Create PostgreSQL syntax highlighting
- [ ] Test metadata queries
- [ ] Implement PostgreSQL-specific features
- [ ] Handle schema filtering

**Deliverable:** Full PostgreSQL support

### Sprint 5: MySQL/MariaDB Support (Week 6)
**Tasks:**
- [ ] Implement `MySqlProvider`
- [ ] Implement `MariaDbProvider` (use MySQL base)
- [ ] Create MySQL syntax highlighting
- [ ] Test metadata queries (SHOW TABLES, etc.)
- [ ] Handle MySQL-specific syntax

**Deliverable:** Full MySQL and MariaDB support

### Sprint 6: SQLite Support (Week 7)
**Tasks:**
- [ ] Implement `SqliteProvider`
- [ ] Create SQLite syntax highlighting
- [ ] Add file browser for SQLite connection
- [ ] Handle sqlite_master queries
- [ ] Disable unsupported features for SQLite

**Deliverable:** Full SQLite support

### Sprint 7: UI/UX Polish (Week 8)
**Tasks:**
- [ ] Redesign connection dialog (multi-provider)
- [ ] Add provider icons to tabs
- [ ] Update menu items based on provider capabilities
- [ ] Add provider selector to Welcome screen
- [ ] Implement connection migration

**Deliverable:** Polished multi-provider UI

### Sprint 8: Testing & Documentation (Week 9-10)
**Tasks:**
- [ ] Set up Docker test environment
- [ ] Manual testing with all providers
- [ ] Create automated tests
- [ ] Write user guide
- [ ] Write developer documentation
- [ ] Update README
- [ ] Create provider comparison matrix

**Deliverable:** Tested and documented multi-provider application

### Sprint 9: Deployment & Release (Week 11)
**Tasks:**
- [ ] Create offline deployment package (all providers)
- [ ] Test on clean Windows 11 VM
- [ ] Update deployment scripts
- [ ] Create release notes
- [ ] Publish binaries
- [ ] Update NuGet dependencies

**Deliverable:** Production-ready release

---

## Risk Assessment

### High Risk

| Risk | Impact | Mitigation |
|------|--------|------------|
| **Oracle licensing/deployment** | Package size, licensing concerns | Use Oracle.ManagedDataAccess (free), document licensing |
| **Offline deployment size** | 500MB+ with all providers | Offer modular installation (select providers) |
| **Performance degradation** | Slower than DB2-only version | Optimize provider factory, lazy loading |

### Medium Risk

| Risk | Impact | Mitigation |
|------|--------|------------|
| **Provider-specific bugs** | Different behavior per database | Extensive testing, provider-specific error handling |
| **Syntax highlighting accuracy** | Complex SQL dialects | Use established XSHD files, community contributions |
| **Feature parity** | Not all features work on all DBs | Clear documentation, disable unsupported features |

### Low Risk

| Risk | Impact | Mitigation |
|------|--------|------------|
| **Migration issues** | Existing users lose connections | Thorough migration testing, backup old config |
| **NuGet dependency conflicts** | Version conflicts between providers | Use latest stable versions, test compatibility |

---

## Success Criteria

### Must Have (MVP)
- ✅ All 7 database providers functional
- ✅ Connect, query, browse metadata for each
- ✅ Provider-specific syntax highlighting
- ✅ Backward compatible with existing DB2 connections
- ✅ Offline deployment works
- ✅ No regression in existing DB2 features

### Should Have
- ✅ SQL formatting for all dialects
- ✅ Provider-specific icons and UI indicators
- ✅ Feature availability matrix in UI
- ✅ Connection migration from old format
- ✅ Comprehensive user documentation

### Nice to Have
- ⚠️ MongoDB support (NoSQL)
- ⚠️ Import/export connections between users
- ⚠️ Provider-specific performance monitoring
- ⚠️ Cross-database query comparison
- ⚠️ Database migration tools (e.g., DB2 → PostgreSQL)

---

## Appendix A: Provider-Specific Details

### A.1 Connection String Formats

**IBM DB2:**
```
Server=hostname:port;Database=dbname;UID=username;PWD=password;
```

**SQL Server:**
```
Server=hostname,port;Database=dbname;User Id=username;Password=password;TrustServerCertificate=true;
```

**Oracle:**
```
Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=hostname)(PORT=port))(CONNECT_DATA=(SERVICE_NAME=service)));User Id=username;Password=password;
```

**PostgreSQL:**
```
Host=hostname;Port=port;Database=dbname;Username=username;Password=password;
```

**MySQL:**
```
Server=hostname;Port=port;Database=dbname;Uid=username;Pwd=password;
```

**MariaDB:**
```
Server=hostname;Port=port;Database=dbname;Uid=username;Pwd=password;
```

**SQLite:**
```
Data Source=C:\path\to\database.db;Version=3;
```

### A.2 Default Ports

| Provider   | Default Port |
|------------|--------------|
| DB2        | 50000        |
| SQL Server | 1433         |
| Oracle     | 1521         |
| PostgreSQL | 5432         |
| MySQL      | 3306         |
| MariaDB    | 3306         |
| SQLite     | N/A (file)   |

### A.3 System Catalog Comparison

| Metadata       | DB2                  | SQL Server           | Oracle            | PostgreSQL       | MySQL                |
|----------------|----------------------|----------------------|-------------------|------------------|----------------------|
| Tables         | SYSCAT.TABLES        | sys.tables           | ALL_TABLES        | pg_tables        | INFORMATION_SCHEMA.TABLES |
| Columns        | SYSCAT.COLUMNS       | sys.columns          | ALL_TAB_COLUMNS   | pg_attribute     | INFORMATION_SCHEMA.COLUMNS |
| Indexes        | SYSCAT.INDEXES       | sys.indexes          | ALL_INDEXES       | pg_indexes       | INFORMATION_SCHEMA.STATISTICS |
| Procedures     | SYSCAT.ROUTINES      | sys.procedures       | ALL_PROCEDURES    | pg_proc          | INFORMATION_SCHEMA.ROUTINES |
| Views          | SYSCAT.VIEWS         | sys.views            | ALL_VIEWS         | pg_views         | INFORMATION_SCHEMA.VIEWS |

---

## Appendix B: File Changes Summary

### New Files to Create

**Interfaces:**
- `Services/Database/IDbProvider.cs`
- `Services/Database/IDbConnection.cs` (wrapper)
- `Models/Database/DbProviderType.cs`
- `Models/Database/ConnectionParameters.cs`
- `Models/Database/ConnectionProfile.cs`

**Provider Implementations:**
- `Services/Database/Providers/Db2Provider.cs`
- `Services/Database/Providers/SqlServerProvider.cs`
- `Services/Database/Providers/OracleProvider.cs`
- `Services/Database/Providers/PostgreSqlProvider.cs`
- `Services/Database/Providers/MySqlProvider.cs`
- `Services/Database/Providers/MariaDbProvider.cs`
- `Services/Database/Providers/SqliteProvider.cs`

**Factories:**
- `Services/Database/DbProviderFactory.cs`

**Services:**
- `Services/ConnectionMigrationService.cs`
- `Services/ProviderConfigurationService.cs`

**Resources:**
- `Resources/SyntaxHighlighting/TSQL.xshd`
- `Resources/SyntaxHighlighting/PLSQL.xshd`
- `Resources/SyntaxHighlighting/PostgreSQL.xshd`
- `Resources/SyntaxHighlighting/MySQL.xshd`
- `Resources/SyntaxHighlighting/SQLite.xshd`

**Configuration:**
- `ConfigFiles/ProviderSettings.json`

### Files to Modify

**Dialogs:**
- `Dialogs/ConnectDialog.xaml` (add provider selector)
- `Dialogs/ConnectDialog.xaml.cs` (dynamic fields logic)

**Controls:**
- `Controls/ConnectionTabControl.xaml.cs` (use IDbProvider)
- `Controls/WelcomePanel.xaml` (add provider selector)

**Services:**
- `Services/SqlFormatterService.cs` (multi-dialect support)
- All panel services (update to use IDbProvider)

**Main Window:**
- `MainWindow.xaml.cs` (update menu based on provider)
- `App.xaml.cs` (register providers in DI)

**Project File:**
- `WindowsDb2Editor.csproj` (add NuGet packages)

---

## Appendix C: TASKLIST.md Updates

**Add to TASKLIST.md after plan approval:**

```markdown
## 🎯 PHASE 11: MULTI-DATABASE PROVIDER SUPPORT

### Sprint 1: Foundation & Abstraction Layer
- [ ] Create IDbProvider interface
- [ ] Create DbProviderFactory
- [ ] Implement Db2Provider (migrate existing code)
- [ ] Create ConnectionParameters model
- [ ] Create DbProviderType enum
- [ ] Add NuGet packages (all providers)
- [ ] Update dependency injection configuration

### Sprint 2: SQL Server Support
- [ ] Implement SqlServerProvider
- [ ] Create TSQL.xshd syntax highlighting
- [ ] Add SQL Server connection dialog support
- [ ] Implement metadata queries (SQL Server)
- [ ] Test connection, query execution, browsing

### Sprint 3: Oracle Support
- [ ] Implement OracleProvider
- [ ] Create PLSQL.xshd syntax highlighting
- [ ] Add Oracle connection dialog (Service Name/SID)
- [ ] Implement metadata queries (Oracle)
- [ ] Test connection, query execution, browsing

### Sprint 4: PostgreSQL Support
- [ ] Implement PostgreSqlProvider
- [ ] Create PostgreSQL.xshd syntax highlighting
- [ ] Add PostgreSQL connection dialog support
- [ ] Implement metadata queries (PostgreSQL)
- [ ] Test connection, query execution, browsing

### Sprint 5: MySQL/MariaDB Support
- [ ] Implement MySqlProvider
- [ ] Implement MariaDbProvider
- [ ] Create MySQL.xshd syntax highlighting
- [ ] Add MySQL connection dialog support
- [ ] Implement metadata queries (MySQL)
- [ ] Test connection, query execution, browsing

### Sprint 6: SQLite Support
- [ ] Implement SqliteProvider
- [ ] Create SQLite.xshd syntax highlighting
- [ ] Add file browser for SQLite connections
- [ ] Implement metadata queries (SQLite)
- [ ] Disable unsupported features for SQLite
- [ ] Test connection, query execution, browsing

### Sprint 7: UI/UX Enhancements
- [ ] Redesign ConnectDialog (provider selector)
- [ ] Add provider icons to tab headers
- [ ] Update status bar with provider info
- [ ] Add provider logos/icons to Resources
- [ ] Update WelcomePanel with provider selector
- [ ] Implement dynamic menu items based on provider

### Sprint 8: Configuration & Migration
- [ ] Create ProviderSettings.json
- [ ] Implement ConnectionMigrationService
- [ ] Update ConnectionProfile model (multi-provider)
- [ ] Migrate existing DB2 connections
- [ ] Test connection profile save/load

### Sprint 9: SQL Formatting
- [ ] Integrate SqlFormatter NuGet package
- [ ] Implement multi-dialect formatting
- [ ] Update SqlFormatterService
- [ ] Test formatting for all dialects

### Sprint 10: Testing & Validation
- [ ] Create Docker Compose test environment
- [ ] Manual test all providers (connection)
- [ ] Manual test all providers (query execution)
- [ ] Manual test all providers (metadata browsing)
- [ ] Test feature availability per provider
- [ ] Test provider switching (multiple tabs)
- [ ] Test offline deployment with all providers

### Sprint 11: Documentation
- [ ] Create PROVIDER_INTEGRATION_GUIDE.md
- [ ] Create ADDING_NEW_PROVIDER.md
- [ ] Update README.md (multi-provider)
- [ ] Create user guide (provider-specific instructions)
- [ ] Create feature compatibility matrix
- [ ] Document connection string formats
- [ ] Create troubleshooting guide

### Sprint 12: Deployment & Release
- [ ] Update deployment scripts
- [ ] Test offline deployment package
- [ ] Create modular installer (optional providers)
- [ ] Update application manifest
- [ ] Rename assembly (WindowsDbEditor)
- [ ] Create release notes
- [ ] Publish binaries
```

---

## Next Steps

1. **Review this plan** with stakeholders
2. **Approve architecture** (IDbProvider interface design)
3. **Set up development branch** (`feature/multi-provider-support`)
4. **Begin Sprint 1** (Foundation & Abstraction Layer)
5. **Establish test environment** (Docker Compose)

---

**Document Status:** ✅ COMPLETE - Ready for Review  
**Estimated Total Effort:** 11 weeks (full-time development)  
**Estimated Lines of Code:** ~8,000 new lines (excluding XSHD files)  
**Estimated Binary Size:** ~450 MB (offline, all providers)

---

**Author:** Claude (Cursor AI Agent)  
**Last Updated:** 2025-11-22
