# Database Expansion TODO List

**Project:** Multi-Database Support for WindowsDb2Editor  
**Version:** 1.0  
**Created:** November 25, 2025  
**Strategy:** Hybrid Approach (6-8 weeks)  
**Status:** Planning Phase

---

## Table of Contents

1. [Phase 1: Foundation & Architecture (Weeks 1-2)](#phase-1-foundation--architecture-weeks-1-2)
2. [Phase 2: PostgreSQL Proof-of-Concept (Weeks 1-2)](#phase-2-postgresql-proof-of-concept-weeks-1-2)
3. [Phase 3: Service Layer Refactoring (Weeks 3-4)](#phase-3-service-layer-refactoring-weeks-3-4)
4. [Phase 4: SQL Server Support (Weeks 5-6)](#phase-4-sql-server-support-weeks-5-6)
5. [Phase 5: MySQL Support (Week 7)](#phase-5-mysql-support-week-7)
6. [Phase 6: UI Adaptation (Week 7-8)](#phase-6-ui-adaptation-week-7-8)
7. [Phase 7: Testing & Polish (Week 8-10)](#phase-7-testing--polish-week-8-10)

---

## Status Legend

- ⏳ **NOT STARTED** - Task not yet begun
- 🔄 **IN PROGRESS** - Currently being worked on
- ✅ **COMPLETE** - Task finished and verified
- ⚠️ **BLOCKED** - Waiting on dependencies
- ❌ **CANCELLED** - Task no longer needed

---

## Phase 1: Foundation & Architecture (Weeks 1-2)

**Goal:** Create abstraction layer without breaking existing DB2 functionality

### 1.1 Define Core Interfaces

- [ ] ⏳ Create `IDbConnectionManager` interface
  - [ ] Define `Provider` property
  - [ ] Define `Version` property
  - [ ] Define `IsConnected` property
  - [ ] Define `OpenAsync()` method
  - [ ] Define `Close()` method
  - [ ] Define `ExecuteQueryAsync()` method
  - [ ] Define `ExecuteNonQueryAsync()` method
  - [ ] Define `ExecuteScalarAsync()` method
  - [ ] Define `CreateCommand()` method
  - [ ] Define `GetCurrentUserAsync()` method
  - [ ] Add XML documentation for all members

- [ ] ⏳ Create `IDatabaseMetadataProvider` interface
  - [ ] Define `GetSchemasAsync()` method
  - [ ] Define `GetTablesAsync(schema)` method
  - [ ] Define `GetViewsAsync(schema)` method
  - [ ] Define `GetStoredProceduresAsync(schema)` method
  - [ ] Define `GetTableColumnsAsync(tableName)` method
  - [ ] Define `GetIndexesAsync(tableName)` method
  - [ ] Define `GetForeignKeysAsync(tableName)` method
  - [ ] Define `GetPrimaryKeyAsync(tableName)` method
  - [ ] Add XML documentation

- [ ] ⏳ Create `IQueryBuilder` interface
  - [ ] Define `BuildPaginatedQuery(sql, maxRows, offset)` method
  - [ ] Define `BuildInsertStatement(table, columns, values)` method
  - [ ] Define `BuildUpdateStatement(table, updates, where)` method
  - [ ] Define `BuildDeleteStatement(table, where)` method
  - [ ] Define `GetParameterPlaceholder(index)` method (handles ?, $1, @p, etc.)
  - [ ] Add XML documentation

- [ ] ⏳ Create `IProviderCapabilities` interface
  - [ ] Define `SupportsPackages` property
  - [ ] Define `SupportsCDC` property (Change Data Capture)
  - [ ] Define `SupportsWindowFunctions` property
  - [ ] Define `SupportsMaterializedViews` property
  - [ ] Define `SupportsFullTextSearch` property
  - [ ] Define `SupportsTransactions` property
  - [ ] Define `SupportsStoredProcedures` property
  - [ ] Define `SupportsXmlDataType` property
  - [ ] Define `SupportsJsonDataType` property
  - [ ] Define `MaxIdentifierLength` property
  - [ ] Define `IsCaseSensitive` property
  - [ ] Add XML documentation

### 1.2 Create Abstract Base Classes

- [ ] ⏳ Create `DatabaseConnectionManagerBase : IDbConnectionManager, IDisposable`
  - [ ] Implement common `OpenAsync()` logic
  - [ ] Implement common `Close()` logic
  - [ ] Implement common `Dispose()` logic
  - [ ] Add abstract `CreateProviderConnection()` method
  - [ ] Add abstract `BuildConnectionString()` method
  - [ ] Add common error handling/logging
  - [ ] Add connection state management
  - [ ] Add connection pooling support

- [ ] ⏳ Create `DatabaseMetadataProviderBase : IDatabaseMetadataProvider`
  - [ ] Add protected `IDbConnectionManager` field
  - [ ] Implement common caching logic
  - [ ] Add abstract methods for provider-specific queries
  - [ ] Add common error handling

- [ ] ⏳ Create `QueryBuilderBase : IQueryBuilder`
  - [ ] Implement common SQL building logic
  - [ ] Add abstract methods for dialect-specific syntax
  - [ ] Add SQL injection protection helpers
  - [ ] Add parameter validation

### 1.3 Create Provider Factory

- [ ] ⏳ Create `DatabaseProviderFactory` static class
  - [ ] Implement `CreateConnectionManager(provider, profile)` method
  - [ ] Implement `CreateMetadataProvider(provider, connectionManager)` method
  - [ ] Implement `CreateQueryBuilder(provider, version)` method
  - [ ] Implement `CreateCapabilities(provider)` method
  - [ ] Add provider registration system
  - [ ] Add provider discovery (scan assemblies)
  - [ ] Add error handling for unsupported providers
  - [ ] Add caching for factory instances

### 1.4 Refactor Existing DB2 Implementation

- [ ] ⏳ Update `DB2ConnectionManager` to implement `IDbConnectionManager`
  - [ ] Remove direct inheritance, implement interface instead
  - [ ] Keep all existing functionality working
  - [ ] Add interface method implementations
  - [ ] Ensure backward compatibility

- [ ] ⏳ Create `Db2MetadataProvider : IDatabaseMetadataProvider`
  - [ ] Extract metadata logic from `DB2MetadataService`
  - [ ] Implement all interface methods
  - [ ] Use existing DB2 queries
  - [ ] Add comprehensive logging

- [ ] ⏳ Create `Db2QueryBuilder : IQueryBuilder`
  - [ ] Extract pagination logic from `DB2ConnectionManager`
  - [ ] Implement DB2-specific SQL building
  - [ ] Handle DB2 parameter style (?)
  - [ ] Add unit tests

- [ ] ⏳ Create `Db2Capabilities : IProviderCapabilities`
  - [ ] Set `SupportsPackages = true` (unique to DB2)
  - [ ] Set `SupportsCDC = true`
  - [ ] Set all capability flags
  - [ ] Document DB2-specific features

### 1.5 Update Models

- [ ] ⏳ Update `SavedConnection` model
  - [ ] Remove hardcoded `Provider = "DB2"` default
  - [ ] Rename `ToDb2Connection()` to `ToConnection()`
  - [ ] Make method provider-agnostic
  - [ ] Add validation for provider field

- [ ] ⏳ Update `DB2Connection` model
  - [ ] Rename to `DatabaseConnection` (more generic)
  - [ ] OR: Keep name but make provider-agnostic
  - [ ] Remove DB2-specific assumptions
  - [ ] Add provider validation

### 1.6 Testing Foundation

- [ ] ⏳ Create unit tests for interfaces
  - [ ] Test `IDbConnectionManager` implementations
  - [ ] Test `IDatabaseMetadataProvider` implementations
  - [ ] Test `IQueryBuilder` implementations
  - [ ] Test `IProviderCapabilities` implementations

- [ ] ⏳ Create integration tests for DB2
  - [ ] Verify DB2 still works after refactoring
  - [ ] Test connection pooling
  - [ ] Test query execution
  - [ ] Test metadata loading
  - [ ] Test error handling

---

## Phase 2: PostgreSQL Proof-of-Concept (Weeks 1-2)

**Goal:** Prove architecture works with a second database

### 2.1 Add PostgreSQL NuGet Package

- [ ] ⏳ Update `WindowsDb2Editor.csproj`
  - [ ] Add `<PackageReference Include="Npgsql" Version="8.0.5" />`
  - [ ] Restore NuGet packages
  - [ ] Verify no conflicts with existing packages
  - [ ] Update project documentation

### 2.2 Create PostgreSQL Configuration Files

- [ ] ⏳ Create `ConfigFiles/postgres_15_sql_statements.json`
  - [ ] Add `GetCurrentUser` statement: `SELECT current_user`
  - [ ] Add `GetTablesForSchema`: `SELECT table_name FROM information_schema.tables WHERE table_schema = $1`
  - [ ] Add `GetColumnsForTable`: Query `information_schema.columns`
  - [ ] Add `GetSchemasInDatabase`: `SELECT schema_name FROM information_schema.schemata`
  - [ ] Add `GetViewsForSchema`: Query `information_schema.views`
  - [ ] Add `GetIndexesForTable`: Query `pg_indexes`
  - [ ] Add `GetForeignKeys`: Query `information_schema.key_column_usage`
  - [ ] Add `GetPrimaryKey`: Query `information_schema.table_constraints`
  - [ ] Add pagination query pattern (LIMIT/OFFSET)
  - [ ] Add DDL generation queries
  - [ ] Document parameter style ($1, $2, $3...)

- [ ] ⏳ Create `ConfigFiles/postgres_15_en-US_texts.json`
  - [ ] Copy structure from `db2_12.1_en-US_texts.json`
  - [ ] Update provider-specific text
  - [ ] Add PostgreSQL-specific terminology

- [ ] ⏳ Create `ConfigFiles/postgres_15_keywords.json`
  - [ ] Add PostgreSQL reserved keywords
  - [ ] Add PostgreSQL data types
  - [ ] Add PostgreSQL functions
  - [ ] Reference: https://www.postgresql.org/docs/current/sql-keywords-appendix.html

- [ ] ⏳ Update `ConfigFiles/supported_providers.json`
  ```json
  {
    "provider_code": "PostgreSQL",
    "display_name": "PostgreSQL Database",
    "icon": "🐘",
    "vendor": "PostgreSQL Global Development Group",
    "description": "PostgreSQL: The World's Most Advanced Open Source Database",
    "supported_versions": ["15", "14", "13", "12"],
    "default_port": 5432,
    "connection_string_template": "Host={host};Port={port};Database={database};Username={username};Password={password};",
    "requires_schema": true,
    "system_catalog_schema": "information_schema"
  }
  ```

### 2.3 Implement PostgreSQL Provider Classes

- [ ] ⏳ Create `Data/PostgresConnectionManager.cs`
  - [ ] Implement `IDbConnectionManager`
  - [ ] Use `Npgsql.NpgsqlConnection`
  - [ ] Build PostgreSQL connection string
  - [ ] Handle `NpgsqlException` errors
  - [ ] Implement connection pooling
  - [ ] Add comprehensive logging
  - [ ] Test connection open/close

- [ ] ⏳ Create `Services/PostgresMetadataProvider.cs`
  - [ ] Implement `IDatabaseMetadataProvider`
  - [ ] Query `information_schema` tables
  - [ ] Query `pg_catalog` for advanced metadata
  - [ ] Implement schema discovery
  - [ ] Implement table/view discovery
  - [ ] Add result caching
  - [ ] Add error handling

- [ ] ⏳ Create `Services/PostgresQueryBuilder.cs`
  - [ ] Implement `IQueryBuilder`
  - [ ] Use `LIMIT x OFFSET y` pagination
  - [ ] Use `$1, $2, $3` parameter style
  - [ ] Build INSERT/UPDATE/DELETE statements
  - [ ] Handle PostgreSQL-specific syntax
  - [ ] Add SQL injection protection

- [ ] ⏳ Create `Models/PostgresCapabilities.cs`
  - [ ] Implement `IProviderCapabilities`
  - [ ] Set `SupportsPackages = false`
  - [ ] Set `SupportsCDC = true` (logical replication)
  - [ ] Set `SupportsMaterializedViews = true`
  - [ ] Set `SupportsJsonDataType = true`
  - [ ] Set `SupportsWindowFunctions = true`
  - [ ] Set `SupportsFullTextSearch = true`
  - [ ] Set `MaxIdentifierLength = 63`

### 2.4 Register PostgreSQL in Factory

- [ ] ⏳ Update `DatabaseProviderFactory`
  - [ ] Add PostgreSQL case in switch statement
  - [ ] Register `PostgresConnectionManager`
  - [ ] Register `PostgresMetadataProvider`
  - [ ] Register `PostgresQueryBuilder`
  - [ ] Register `PostgresCapabilities`
  - [ ] Add logging for PostgreSQL initialization

### 2.5 Test PostgreSQL Integration

- [ ] ⏳ Setup PostgreSQL test database
  - [ ] Install PostgreSQL 15 (local or Docker)
  - [ ] Create test database
  - [ ] Create test schema with tables/views
  - [ ] Populate sample data

- [ ] ⏳ Integration tests
  - [ ] Test connection to PostgreSQL
  - [ ] Test schema retrieval
  - [ ] Test table listing
  - [ ] Test query execution
  - [ ] Test pagination
  - [ ] Test parameterized queries
  - [ ] Test metadata loading
  - [ ] Test error handling

- [ ] ⏳ UI testing
  - [ ] Create PostgreSQL connection in ConnectionDialog
  - [ ] Browse schemas/tables
  - [ ] Execute SELECT query
  - [ ] View results in DataGrid
  - [ ] Test syntax highlighting (if implemented)

---

## Phase 3: Service Layer Refactoring (Weeks 3-4)

**Goal:** Update all services to use abstraction layer

### 3.1 Critical Services (High Priority)

- [ ] ⏳ Refactor `ObjectBrowserService.cs`
  - [ ] Replace `DB2ConnectionManager` with `IDbConnectionManager`
  - [ ] Use `IDatabaseMetadataProvider` for metadata
  - [ ] Remove DB2-specific code
  - [ ] Test with DB2 and PostgreSQL
  - [ ] Add error handling for provider differences

- [ ] ⏳ Refactor `DdlGeneratorService.cs`
  - [ ] Use provider-specific DDL templates
  - [ ] Query MetadataHandler for DDL patterns
  - [ ] Support DB2 and PostgreSQL DDL syntax
  - [ ] Add provider detection
  - [ ] Test DDL generation for both databases

- [ ] ⏳ Refactor `MetadataLoaderService.cs`
  - [ ] Use `IDatabaseMetadataProvider`
  - [ ] Remove SYSCAT hardcoded queries
  - [ ] Load provider-specific metadata
  - [ ] Cache per provider
  - [ ] Test with both DB2 and PostgreSQL

- [ ] ⏳ Refactor `ExportService.cs`
  - [ ] Use generic data export
  - [ ] Support provider-specific data types
  - [ ] Handle date/time format differences
  - [ ] Test CSV/Excel export with both DBs

- [ ] ⏳ Refactor `SqlFormatterService.cs`
  - [ ] Add provider parameter
  - [ ] Use provider-specific keywords
  - [ ] Format according to database conventions
  - [ ] Test with DB2 and PostgreSQL SQL

### 3.2 Monitoring Services (Medium Priority)

- [ ] ⏳ Refactor `SessionMonitorService.cs`
  - [ ] Abstract session monitoring
  - [ ] DB2: Query `SYSIBMADM.APPLICATIONS`
  - [ ] PostgreSQL: Query `pg_stat_activity`
  - [ ] Create provider-specific implementations
  - [ ] Test with both databases

- [ ] ⏳ Refactor `LockMonitorService.cs`
  - [ ] Abstract lock monitoring
  - [ ] DB2: Query `SYSIBMADM.LOCKS_HELD`
  - [ ] PostgreSQL: Query `pg_locks`
  - [ ] Handle different lock types
  - [ ] Test lock detection

- [ ] ⏳ Refactor `DatabaseLoadMonitorService.cs`
  - [ ] Abstract performance monitoring
  - [ ] DB2: Use MON_* table functions
  - [ ] PostgreSQL: Use `pg_stat_database`
  - [ ] Normalize metrics across providers
  - [ ] Add provider-specific charts

- [ ] ⏳ Refactor `StatisticsService.cs`
  - [ ] Abstract statistics management
  - [ ] DB2: RUNSTATS commands
  - [ ] PostgreSQL: ANALYZE commands
  - [ ] SQL Server: UPDATE STATISTICS
  - [ ] Provide provider-specific recommendations

### 3.3 Advanced Services (Lower Priority)

- [ ] ⏳ Refactor `PackageAnalyzerService.cs`
  - [ ] Mark as DB2-only feature
  - [ ] Check `ProviderCapabilities.SupportsPackages`
  - [ ] Hide UI for non-DB2 providers
  - [ ] Document DB2-specific nature

- [ ] ⏳ Refactor `DataCaptureService.cs` (CDC)
  - [ ] Abstract CDC management
  - [ ] DB2: ALTER TABLE DATA CAPTURE
  - [ ] PostgreSQL: Logical replication
  - [ ] SQL Server: Change Data Capture
  - [ ] Provider-specific UI/workflow

- [ ] ⏳ Refactor `MigrationPlannerService.cs`
  - [ ] Support cross-database migration
  - [ ] Generate migration plan (DB2 → PostgreSQL)
  - [ ] Map data types across providers
  - [ ] Handle incompatible features
  - [ ] Generate compatibility report

- [ ] ⏳ Refactor `DependencyAnalyzerService.cs`
  - [ ] Use `IDatabaseMetadataProvider`
  - [ ] Query provider-specific dependency views
  - [ ] Build dependency graph generically
  - [ ] Test with multiple providers

- [ ] ⏳ Refactor `TableRelationshipService.cs`
  - [ ] Query foreign keys generically
  - [ ] DB2: SYSCAT.REFERENCES
  - [ ] PostgreSQL: information_schema.referential_constraints
  - [ ] Generate relationship diagrams

- [ ] ⏳ Refactor `CommentService.cs`
  - [ ] Abstract COMMENT syntax
  - [ ] DB2: `COMMENT ON TABLE...`
  - [ ] PostgreSQL: `COMMENT ON TABLE...` (same!)
  - [ ] SQL Server: `sp_addextendedproperty`
  - [ ] Provider-specific implementation

- [ ] ⏳ Refactor `SourceCodeService.cs`
  - [ ] Query stored procedures generically
  - [ ] DB2: SYSCAT.ROUTINES
  - [ ] PostgreSQL: information_schema.routines, pg_proc
  - [ ] Extract procedure source code
  - [ ] Handle language differences (SQL PL vs PL/pgSQL)

- [ ] ⏳ Refactor `AccessControlService.cs`
  - [ ] Abstract user permissions
  - [ ] DB2: Query SYSCAT.DBAUTH
  - [ ] PostgreSQL: Query pg_roles, pg_database
  - [ ] Map to common permission levels
  - [ ] Test RBAC with multiple providers

### 3.4 Remaining Services

- [ ] ⏳ Refactor `UnusedObjectDetectorService.cs`
- [ ] ⏳ Refactor `SchemaDiffAnalyzerService.cs`
- [ ] ⏳ Refactor `ConnectionProfileService.cs` (should already be generic)
- [ ] ⏳ Refactor `ConnectionStorageService.cs` (should already be generic)
- [ ] ⏳ Refactor `ConnectionHistoryService.cs` (should already be generic)
- [ ] ⏳ Refactor `QueryHistoryService.cs` (should already be generic)
- [ ] ⏳ Refactor `ConnectionStatisticsService.cs`
- [ ] ⏳ Refactor `MermaidDiagramGeneratorService.cs`
- [ ] ⏳ Refactor `SqlSafetyValidatorService.cs`
- [ ] ⏳ Refactor `IntellisenseService.cs`
- [ ] ⏳ Refactor `SqlCompletionDataProvider.cs`

### 3.5 Service Testing

- [ ] ⏳ Create unit tests for each refactored service
  - [ ] Mock `IDbConnectionManager`
  - [ ] Mock `IDatabaseMetadataProvider`
  - [ ] Test with both DB2 and PostgreSQL configurations
  - [ ] Verify error handling
  - [ ] Test edge cases

- [ ] ⏳ Integration tests for service layer
  - [ ] Connect to real DB2 database
  - [ ] Connect to real PostgreSQL database
  - [ ] Execute service operations
  - [ ] Verify results
  - [ ] Performance testing

---

## Phase 4: SQL Server Support (Weeks 5-6)

**Goal:** Add third database provider

### 4.1 Add SQL Server NuGet Package

- [ ] ⏳ Update `WindowsDb2Editor.csproj`
  - [ ] Add `<PackageReference Include="Microsoft.Data.SqlClient" Version="5.2.2" />`
  - [ ] Restore packages
  - [ ] Test for conflicts

### 4.2 Create SQL Server Configuration Files

- [ ] ⏳ Create `ConfigFiles/sqlserver_2022_sql_statements.json`
  - [ ] Query `sys.tables`, `sys.columns`, `sys.indexes`
  - [ ] Use `OFFSET/FETCH NEXT` pagination (same as DB2!)
  - [ ] Parameter style: `@p1, @p2, @p3`
  - [ ] Add DDL generation queries
  - [ ] Add monitoring queries (DMVs)

- [ ] ⏳ Create `ConfigFiles/sqlserver_2022_en-US_texts.json`
- [ ] ⏳ Create `ConfigFiles/sqlserver_2022_keywords.json`

- [ ] ⏳ Update `ConfigFiles/supported_providers.json`
  ```json
  {
    "provider_code": "SQL Server",
    "display_name": "Microsoft SQL Server",
    "icon": "🗃️",
    "vendor": "Microsoft",
    "description": "Microsoft SQL Server - Enterprise Database",
    "supported_versions": ["2022", "2019", "2017"],
    "default_port": 1433,
    "connection_string_template": "Server={host},{port};Database={database};User Id={username};Password={password};TrustServerCertificate=True;",
    "requires_schema": true,
    "system_catalog_schema": "sys"
  }
  ```

### 4.3 Implement SQL Server Provider Classes

- [ ] ⏳ Create `Data/SqlServerConnectionManager.cs`
  - [ ] Use `Microsoft.Data.SqlClient.SqlConnection`
  - [ ] Handle `SqlException`
  - [ ] Implement `IDbConnectionManager`
  - [ ] Test connection

- [ ] ⏳ Create `Services/SqlServerMetadataProvider.cs`
  - [ ] Query `sys.tables`, `sys.columns`, `sys.indexes`
  - [ ] Query `sys.foreign_keys`, `sys.schemas`
  - [ ] Implement `IDatabaseMetadataProvider`

- [ ] ⏳ Create `Services/SqlServerQueryBuilder.cs`
  - [ ] Use `OFFSET/FETCH NEXT` (SQL Server 2012+)
  - [ ] Use `@p1, @p2` parameter style
  - [ ] Implement `IQueryBuilder`

- [ ] ⏳ Create `Models/SqlServerCapabilities.cs`
  - [ ] Set `SupportsPackages = false`
  - [ ] Set `SupportsCDC = true`
  - [ ] Set `SupportsWindowFunctions = true`
  - [ ] Set `SupportsJsonDataType = true` (SQL Server 2016+)
  - [ ] Set `SupportsXmlDataType = true`

### 4.4 Register and Test SQL Server

- [ ] ⏳ Register in `DatabaseProviderFactory`
- [ ] ⏳ Setup SQL Server test database (LocalDB or Docker)
- [ ] ⏳ Integration tests
- [ ] ⏳ UI testing

---

## Phase 5: MySQL Support (Week 7)

**Goal:** Add fourth database provider

### 5.1 Add MySQL NuGet Package

- [ ] ⏳ Update `WindowsDb2Editor.csproj`
  - [ ] Add `<PackageReference Include="MySql.Data" Version="9.1.0" />`
  - [ ] Restore packages

### 5.2 Create MySQL Configuration Files

- [ ] ⏳ Create `ConfigFiles/mysql_8.0_sql_statements.json`
  - [ ] Query `information_schema` tables
  - [ ] Use `LIMIT offset, count` pagination
  - [ ] Parameter style: `?` (same as DB2!)
  - [ ] Add DDL generation

- [ ] ⏳ Create `ConfigFiles/mysql_8.0_en-US_texts.json`
- [ ] ⏳ Create `ConfigFiles/mysql_8.0_keywords.json`

- [ ] ⏳ Update `ConfigFiles/supported_providers.json`
  ```json
  {
    "provider_code": "MySQL",
    "display_name": "MySQL Database",
    "icon": "🐬",
    "vendor": "Oracle Corporation",
    "description": "MySQL - The World's Most Popular Open Source Database",
    "supported_versions": ["8.0", "5.7"],
    "default_port": 3306,
    "connection_string_template": "Server={host};Port={port};Database={database};Uid={username};Pwd={password};",
    "requires_schema": false,
    "system_catalog_schema": "information_schema"
  }
  ```

### 5.3 Implement MySQL Provider Classes

- [ ] ⏳ Create `Data/MySqlConnectionManager.cs`
- [ ] ⏳ Create `Services/MySqlMetadataProvider.cs`
- [ ] ⏳ Create `Services/MySqlQueryBuilder.cs`
- [ ] ⏳ Create `Models/MySqlCapabilities.cs`
  - [ ] Set `SupportsPackages = false`
  - [ ] Set `SupportsCDC = false` (limited CDC support)
  - [ ] Set `SupportsWindowFunctions = true` (MySQL 8.0+)
  - [ ] Set `SupportsJsonDataType = true`
  - [ ] Set `SupportsTransactions = true` (InnoDB)

### 5.4 Register and Test MySQL

- [ ] ⏳ Register in `DatabaseProviderFactory`
- [ ] ⏳ Setup MySQL test database (Docker)
- [ ] ⏳ Integration tests
- [ ] ⏳ UI testing

---

## Phase 6: UI Adaptation (Week 7-8)

**Goal:** Update UI to handle multiple database providers

### 6.1 Update ConnectionDialog

- [ ] ⏳ Update `Dialogs/ConnectionDialog.xaml`
  - [ ] Add provider dropdown/ComboBox
  - [ ] Populate from `MetadataHandler.GetSupportedProviders()`
  - [ ] Show provider icon (🗄️ DB2, 🐘 PostgreSQL, etc.)
  - [ ] Update port based on selected provider
  - [ ] Show/hide schema field based on `RequiresSchema`

- [ ] ⏳ Update `Dialogs/ConnectionDialog.xaml.cs`
  - [ ] Load providers on dialog open
  - [ ] Update connection string template
  - [ ] Validate provider-specific fields
  - [ ] Test connection with selected provider
  - [ ] Save provider in connection profile

### 6.2 Update MainWindow

- [ ] ⏳ Update `MainWindow.xaml.cs`
  - [ ] Use `DatabaseProviderFactory` instead of hardcoded DB2
  - [ ] Create connection manager based on provider
  - [ ] Display provider in status bar/tab header
  - [ ] Add provider icon to tab

- [ ] ⏳ Update menu items with feature availability
  - [ ] Check `IProviderCapabilities` before showing menu items
  - [ ] Disable "Package Analyzer" for non-DB2 providers
  - [ ] Show provider-appropriate monitoring panels
  - [ ] Add tooltip explaining why feature is disabled

### 6.3 Update Controls/Panels

- [ ] ⏳ Update `Controls/ConnectionTabControl.xaml.cs`
  - [ ] Accept `IDbConnectionManager` instead of `DB2ConnectionManager`
  - [ ] Display provider name/icon
  - [ ] Use generic query execution
  - [ ] Test with all 4 providers

- [ ] ⏳ Update `Controls/ObjectBrowserPanel.xaml.cs`
  - [ ] Use `IDatabaseMetadataProvider`
  - [ ] Display provider-specific metadata
  - [ ] Handle different schema structures
  - [ ] Test tree view with all providers

- [ ] ⏳ Update `Controls/DatabaseLoadMonitorPanel.xaml.cs`
  - [ ] Show only if provider supports monitoring
  - [ ] Use provider-specific monitoring queries
  - [ ] Adapt charts to provider metrics

- [ ] ⏳ Update `Controls/PackageAnalyzerPanel.xaml.cs`
  - [ ] Show only for DB2
  - [ ] Display "Not available for {provider}" message otherwise
  - [ ] Gracefully handle unsupported features

- [ ] ⏳ Update `Controls/LockMonitorPanel.xaml.cs`
  - [ ] Use provider-specific lock queries
  - [ ] Adapt to different lock types
  - [ ] Test with DB2, PostgreSQL, SQL Server

- [ ] ⏳ Update `Controls/StatisticsManagerPanel.xaml.cs`
  - [ ] Show provider-specific statistics commands
  - [ ] DB2: RUNSTATS
  - [ ] PostgreSQL: ANALYZE
  - [ ] SQL Server: UPDATE STATISTICS
  - [ ] MySQL: ANALYZE TABLE

- [ ] ⏳ Update remaining controls (10+ files)

### 6.4 Syntax Highlighting

- [ ] ⏳ Create `Resources/PostgreSQL.xshd` (AvalonEdit syntax)
  - [ ] Define PostgreSQL keywords
  - [ ] Define data types
  - [ ] Define functions
  - [ ] Test in AvalonEdit

- [ ] ⏳ Create `Resources/SQLServer.xshd`
- [ ] ⏳ Create `Resources/MySQL.xshd`

- [ ] ⏳ Update syntax highlighting loader
  - [ ] Load XSHD based on provider
  - [ ] Switch syntax highlighting when provider changes
  - [ ] Test highlighting accuracy

### 6.5 Feature Availability Matrix UI

- [ ] ⏳ Create `Dialogs/ProviderFeaturesDialog.xaml`
  - [ ] Display feature comparison table
  - [ ] Show checkmarks for supported features
  - [ ] Show red X for unsupported features
  - [ ] Add "Learn More" links

- [ ] ⏳ Add "Database Features" menu item
  - [ ] Open `ProviderFeaturesDialog`
  - [ ] Show current provider features
  - [ ] Allow comparison with other providers

### 6.6 Provider-Specific Help

- [ ] ⏳ Update Help menu
  - [ ] Add "DB2 Documentation" (only if connected to DB2)
  - [ ] Add "PostgreSQL Documentation" (only if connected to PostgreSQL)
  - [ ] Add "SQL Server Documentation"
  - [ ] Add "MySQL Documentation"
  - [ ] Link to official provider documentation

---

## Phase 7: Testing & Polish (Week 8-10)

**Goal:** Comprehensive testing and bug fixes

### 7.1 Unit Testing

- [ ] ⏳ Write unit tests for all new interfaces
  - [ ] Test `IDbConnectionManager` implementations
  - [ ] Test `IDatabaseMetadataProvider` implementations
  - [ ] Test `IQueryBuilder` implementations
  - [ ] Test `IProviderCapabilities` implementations

- [ ] ⏳ Write unit tests for provider factories
  - [ ] Test `DatabaseProviderFactory`
  - [ ] Test provider registration
  - [ ] Test error handling

- [ ] ⏳ Write unit tests for query builders
  - [ ] Test pagination SQL generation
  - [ ] Test parameter binding
  - [ ] Test SQL injection protection

- [ ] ⏳ Achieve 80%+ code coverage on new code

### 7.2 Integration Testing

- [ ] ⏳ Setup test databases
  - [ ] DB2 test database (existing)
  - [ ] PostgreSQL 15 test database
  - [ ] SQL Server 2022 test database
  - [ ] MySQL 8.0 test database

- [ ] ⏳ Populate test data
  - [ ] Create identical schema in all 4 databases
  - [ ] Insert sample data
  - [ ] Create views, indexes, foreign keys
  - [ ] Create stored procedures (where supported)

- [ ] ⏳ End-to-end test scenarios
  - [ ] **Scenario 1:** Connect to each database
  - [ ] **Scenario 2:** Browse schemas/tables
  - [ ] **Scenario 3:** Execute SELECT query
  - [ ] **Scenario 4:** Execute INSERT/UPDATE/DELETE
  - [ ] **Scenario 5:** View table metadata
  - [ ] **Scenario 6:** Generate DDL
  - [ ] **Scenario 7:** Export data to CSV
  - [ ] **Scenario 8:** Monitor active sessions
  - [ ] **Scenario 9:** View locks
  - [ ] **Scenario 10:** Run statistics update

- [ ] ⏳ Performance testing
  - [ ] Benchmark connection time per provider
  - [ ] Benchmark query execution
  - [ ] Benchmark metadata loading
  - [ ] Identify bottlenecks
  - [ ] Optimize slow operations

### 7.3 Regression Testing

- [ ] ⏳ Verify DB2 functionality unchanged
  - [ ] All existing features still work
  - [ ] Performance not degraded
  - [ ] No new bugs introduced
  - [ ] User experience unchanged

- [ ] ⏳ Run full test suite on DB2
  - [ ] Connection tests
  - [ ] Query execution tests
  - [ ] Metadata tests
  - [ ] UI tests
  - [ ] Service tests

### 7.4 Error Handling & Edge Cases

- [ ] ⏳ Test connection failures
  - [ ] Invalid credentials
  - [ ] Server unreachable
  - [ ] Network timeout
  - [ ] Database doesn't exist
  - [ ] Port blocked by firewall

- [ ] ⏳ Test query errors
  - [ ] Syntax errors
  - [ ] Permission denied
  - [ ] Table not found
  - [ ] Column not found
  - [ ] Data type mismatch

- [ ] ⏳ Test provider-specific errors
  - [ ] DB2: SQL0204N (table not found)
  - [ ] PostgreSQL: syntax error at or near
  - [ ] SQL Server: Invalid object name
  - [ ] MySQL: Unknown table

- [ ] ⏳ Test edge cases
  - [ ] Empty result sets
  - [ ] NULL values
  - [ ] Very large result sets (100K+ rows)
  - [ ] Long-running queries
  - [ ] Special characters in data
  - [ ] Unicode data

### 7.5 UI/UX Testing

- [ ] ⏳ Manual UI testing
  - [ ] Test all dialogs with each provider
  - [ ] Test menu items enable/disable correctly
  - [ ] Test keyboard shortcuts
  - [ ] Test dark/light theme switching
  - [ ] Test window resizing
  - [ ] Test tab switching

- [ ] ⏳ Accessibility testing
  - [ ] Screen reader compatibility
  - [ ] Keyboard navigation
  - [ ] High contrast mode
  - [ ] Font scaling

- [ ] ⏳ Usability testing
  - [ ] Intuitive provider selection
  - [ ] Clear error messages
  - [ ] Helpful tooltips
  - [ ] Consistent UI across providers

### 7.6 Documentation

- [ ] ⏳ Update README.md
  - [ ] List supported databases
  - [ ] Add connection examples for each provider
  - [ ] Document provider-specific features
  - [ ] Add troubleshooting section

- [ ] ⏳ Create `MarkdownDoc/MULTI_DATABASE_GUIDE.md`
  - [ ] How to connect to each database
  - [ ] Feature comparison matrix
  - [ ] Provider-specific notes
  - [ ] Migration guide (DB2 → PostgreSQL)

- [ ] ⏳ Update `MarkdownDoc/NEXTSTEPS.md`
  - [ ] Mark multi-database support as complete
  - [ ] Add future database expansion plans

- [ ] ⏳ Create provider-specific documentation
  - [ ] `MarkdownDoc/DB2_FEATURES.md`
  - [ ] `MarkdownDoc/POSTGRESQL_FEATURES.md`
  - [ ] `MarkdownDoc/SQLSERVER_FEATURES.md`
  - [ ] `MarkdownDoc/MYSQL_FEATURES.md`

- [ ] ⏳ Update code documentation
  - [ ] XML documentation for all public APIs
  - [ ] Code comments for complex logic
  - [ ] README in ConfigFiles directory

### 7.7 Bug Fixes

- [ ] ⏳ Fix all P0 (critical) bugs
- [ ] ⏳ Fix all P1 (high) bugs
- [ ] ⏳ Fix all P2 (medium) bugs
- [ ] ⏳ Triage P3 (low) bugs for future releases

### 7.8 Performance Optimization

- [ ] ⏳ Profile application with each provider
- [ ] ⏳ Optimize metadata caching
- [ ] ⏳ Optimize query execution
- [ ] ⏳ Reduce memory usage
- [ ] ⏳ Optimize UI rendering

### 7.9 Final Release Preparation

- [ ] ⏳ Update version to 2.0.0 (major version bump)
- [ ] ⏳ Update `WindowsDb2Editor.csproj` metadata
- [ ] ⏳ Create release notes
- [ ] ⏳ Create changelog
- [ ] ⏳ Update screenshots in documentation
- [ ] ⏳ Create demo video (optional)

---

## Optional Enhancements (Future)

### Oracle Support

- [ ] ⏳ Add `Oracle.ManagedDataAccess.Core` package
- [ ] ⏳ Create Oracle configuration files
- [ ] ⏳ Implement Oracle provider classes
- [ ] ⏳ Handle Oracle licensing requirements
- [ ] ⏳ Test with Oracle 19c/21c

### SQLite Support

- [ ] ⏳ Add `Microsoft.Data.Sqlite` package
- [ ] ⏳ Create SQLite configuration files
- [ ] ⏳ Implement SQLite provider classes
- [ ] ⏳ Handle file-based connection (no server/port)
- [ ] ⏳ Useful for demos and testing

### Cross-Database Migration Wizard

- [ ] ⏳ Create `MigrationWizard` dialog
- [ ] ⏳ Select source database (e.g., DB2)
- [ ] ⏳ Select target database (e.g., PostgreSQL)
- [ ] ⏳ Map schemas and tables
- [ ] ⏳ Map data types
- [ ] ⏳ Generate migration scripts
- [ ] ⏳ Execute migration with progress tracking

### Database Comparison Tool

- [ ] ⏳ Compare two databases (same or different providers)
- [ ] ⏳ Show schema differences
- [ ] ⏳ Show data differences
- [ ] ⏳ Generate sync scripts

### Multi-Database Query Execution

- [ ] ⏳ Execute same query on multiple databases
- [ ] ⏳ Compare results
- [ ] ⏳ Useful for testing migrations

---

## Success Metrics

### Completion Criteria

- ✅ All Phase 1-7 tasks marked complete
- ✅ All 4 providers (DB2, PostgreSQL, SQL Server, MySQL) working
- ✅ Zero regression in DB2 functionality
- ✅ 80%+ test coverage on new code
- ✅ All P0/P1 bugs fixed
- ✅ Documentation complete

### Quality Metrics

- ✅ Connection success rate > 99%
- ✅ Query execution time < 100ms for simple queries
- ✅ Metadata loading time < 2 seconds
- ✅ Memory usage < 500MB with 10 connections
- ✅ Zero crashes in 8-hour test run

### User Acceptance

- ✅ 5+ users successfully connect to PostgreSQL
- ✅ 5+ users successfully connect to SQL Server
- ✅ 5+ users successfully connect to MySQL
- ✅ Positive feedback on provider selection UX
- ✅ No confusion about provider-specific features

---

## Risk Mitigation

### High-Risk Items

1. **Breaking DB2 Functionality**
   - Mitigation: Comprehensive regression testing after every change
   - Testing: Run full DB2 test suite daily
   - Rollback plan: Git branches for each phase

2. **Performance Degradation**
   - Mitigation: Benchmark before/after each phase
   - Testing: Performance tests for all providers
   - Optimization: Profile and optimize hot paths

3. **Incomplete Feature Parity**
   - Mitigation: Document feature matrix early
   - Acceptance: Some features are provider-specific (OK)
   - Communication: Clear messaging when feature unavailable

### Medium-Risk Items

4. **Configuration File Errors**
   - Mitigation: JSON schema validation
   - Testing: Automated tests for all config files
   - Validation: Schema validation on app startup

5. **Driver Compatibility**
   - Mitigation: Use latest stable driver versions
   - Testing: Test on Windows 11 clean VM
   - Fallback: Document known compatibility issues

---

## Progress Tracking

**Last Updated:** November 25, 2025

### Phase Status

| Phase | Status | Completion | Notes |
|-------|--------|------------|-------|
| Phase 1 | ⏳ NOT STARTED | 0% | Foundation & Architecture |
| Phase 2 | ⏳ NOT STARTED | 0% | PostgreSQL PoC |
| Phase 3 | ⏳ NOT STARTED | 0% | Service Refactoring |
| Phase 4 | ⏳ NOT STARTED | 0% | SQL Server |
| Phase 5 | ⏳ NOT STARTED | 0% | MySQL |
| Phase 6 | ⏳ NOT STARTED | 0% | UI Adaptation |
| Phase 7 | ⏳ NOT STARTED | 0% | Testing & Polish |

**Overall Progress: 0%**

---

## Team Assignment

| Phase | Primary Developer | Supporting | Reviewer |
|-------|------------------|------------|----------|
| Phase 1 | Senior Dev | - | Tech Lead |
| Phase 2 | Senior Dev | Junior Dev | Tech Lead |
| Phase 3 | 2x Mid-Level | Senior Dev | Tech Lead |
| Phase 4 | Mid-Level | Junior Dev | Senior Dev |
| Phase 5 | Junior Dev | Mid-Level | Senior Dev |
| Phase 6 | UI/UX Dev | Mid-Level | Senior Dev |
| Phase 7 | QA Team | All Devs | Tech Lead |

---

## Timeline

```
Week 1-2: Foundation + PostgreSQL PoC
Week 3-4: Service Layer Refactoring
Week 5-6: SQL Server Support
Week 7:   MySQL Support
Week 8:   UI Adaptation
Week 9-10: Testing & Polish

Target Completion: Week 10 (8-10 weeks from start)
```

---

**Document Status:** DRAFT - Ready for Review  
**Next Review Date:** [TBD]  
**Approved By:** [TBD]
