# Major Database Provider Integration Plan

## Purpose
- Deliver a phased, well-instrumented strategy that evolves WindowsDb2Editor into a multi-provider desktop studio without regressing the existing DB2-first experience.
- Preserve offline deployment, NLog-based diagnostics, rich RBAC enforcement, and WPF UX patterns while making all core features portable across database engines.

## Goals & Success Criteria
- **Unified architecture:** Abstract provider-specific logic behind shared interfaces so UI layers stay unchanged.
- **Pluggable drivers:** Bundle required managed drivers for offline install (no external clients) and enable opt-in publishing to keep package size manageable.
- **Functional parity:** Support query execution, metadata browsing, export, DDL inspection, and monitoring for every provider with feature flags that hide unsupported tools.
- **RBAC continuity:** Map DBA/Middle/Low tiers onto each provider’s authorization model and surface the lock icon indicator consistently.
- **Instrumentation:** Maintain INFO/DEBUG/ERROR telemetry via NLog with provider tags for every log statement.
- **Verification:** Provide repeatable manual + automated validation suites per provider (smoke tests, regression runs, CLI coverage).

## Target Providers & Priority
| Wave | Provider | Driver Package | Primary Use Case | Notes |
|------|----------|----------------|------------------|-------|
| 0 | IBM DB2 (existing) | Net.IBM.Data.Db2 9.0.0.400 | Baseline | Refactor to plug-in without regressions |
| 1 | Microsoft SQL Server & Azure SQL | Microsoft.Data.SqlClient 5.x | Enterprise OLTP, Azure workloads | Highest demand, similar feature depth |
| 1 | PostgreSQL | Npgsql 8.x | Open-source/Postgres variants | Rich metadata, JSON capabilities |
| 2 | MySQL / MariaDB | MySqlConnector 2.x | LAMP stacks | Metadata differs, limited monitoring views |
| 2 | Oracle Database | Oracle.ManagedDataAccess.Core 3.x | Enterprises needing Oracle tooling | Requires ODP.NET licensing review |
| 3 | Snowflake | Snowflake.Data 4.x | Cloud analytics | Mostly read-heavy, unique auth flow |
| 3 | SQLite (read-only) | System.Data.SQLite.Core | Local quick analysis | Lightweight fallback/testing option |

## Guiding Architecture Principles
- Single WPF UI with provider-specific capability switches (no duplicated dialogs).
- Dependency injection for all services; providers registered via `IServiceCollection`.
- Provider capabilities described via metadata (supportsDDL, supportsLocks, etc.).
- SQL dialect awareness confined to provider packages (no scattered conditionals).
- Extensive DEBUG logging before/after every cross-provider operation.

## Architecture Changes
### 1. Provider Abstraction Layer
- Introduce `IDatabaseProvider` (identity metadata) and `IDatabaseProviderFactory` (register + resolve).
- Standardize `IConnectionManager`, `IQueryExecutor`, `IMetadataExplorer`, `IRbacEvaluator`, `IMonitoringService` contracts.
- Each provider module implements contracts and exposes DI registrations via `ProviderDescriptor`.

### 2. Connection Lifecycle
- Extend `SavedConnection` and connection dialogs with `ProviderType`.
- Inject provider-specific validators (connection string builder per provider).
- Shared connection tab hosts provider badges + provider-colored accent (respecting theme resources).
- Pool driver binaries per provider under `Drivers/<Provider>` and copy during publish.

### 3. Metadata & RBAC
- Expand Access Level model to include provider-specific authority checks (e.g., SQL Server `HAS_PERMS_BY_NAME`, PostgreSQL `pg_roles`).
- Metadata queries encapsulated inside provider modules; UI binds to provider-neutral DTOs (`TableInfo`, `LockInfo`, etc.).
- Feature toggles: expose capabilities via `ProviderFeatureMatrix` (e.g., `SupportsLockTermination`, `SupportsObjectDDlPreview`).

### 4. Query Execution & Formatting
- Retain shared AvalonEdit editor; add SQL dialect switch to formatter (PoorMansTSqlFormatter for SQL Server/Postgres, fallback for others).
- CLI execution path routes through provider-specific executors but shares result export pipeline.
- Add statement delimiter awareness per provider (`;`, `/`, `GO`).

### 5. UI/UX Updates
- Connection dialog: provider dropdown, dynamic field hints, driver availability indicator.
- Tab headers: show `{Database} @ {Host} — {Provider}` tooltip.
- Menu items annotated with provider capability tags (hide/disable when unsupported).
- Status bar provider indicator + RBAC badge (e.g., `🟢 SQL Server • DBA`).
- Settings dialog includes default provider preference and driver cache management.

### 6. Configuration & Packaging
- `appsettings.json`: add `SupportedProviders` section with default driver versions, capability flags, and publish toggles.
- `nuget.config`: ensure private feeds (Oracle) resolved when needed.
- Deployment: update `deploy.ps1` (and future scripts) to copy provider driver folders and run smoke tests per provider.

## Implementation Roadmap
### Phase 0 – Foundation Hardening (1 sprint)
1. Inventory all DB2-specific assumptions across `Data/`, `Services/`, `Dialogs/`.
2. Define provider contracts + DTOs; refactor DB2 implementation to align.
3. Centralize connection logging, RBAC evaluation, and metadata caching behind new interfaces.
4. Add provider selection to connection dialog and saved profiles (DB2 default).
5. Regression build + manual verification on DB2.

### Phase 1 – SQL Server & PostgreSQL (2 sprints)
1. Add provider projects/folders (`Providers/SqlServer`, `Providers/Postgres`).
2. Implement connection + query executors with parameterization, timeouts, cancellation.
3. Port metadata queries (schemas, tables, dependencies) using system catalogs (`sys.*`, `pg_catalog.*`).
4. Map RBAC tiers (server role membership, `pg_has_role`).
5. Support lock/session monitors where DMV equivalents exist.
6. Extend CLI + export flows; add unit/integration smoke tests using local containers.
7. Update TASKLIST + docs; run dotnet build + targeted manual tests.

### Phase 2 – MySQL/MariaDB & Oracle (3 sprints)
1. Implement providers with focus on schema explorer, query execution, export.
2. Address lack of advanced monitoring by adding feature flags + UI messaging.
3. Integrate Oracle advanced auth (TNS, EZConnect) and adjust driver packaging (ODP.NET).
4. Provide fallback RBAC heuristics (definer/invoker rights, grants).
5. Update DDL generator to respect dialect differences (backtick quoting, uppercase identifiers).
6. Expand testing matrix (Docker for MySQL/MariaDB, container/VM for Oracle XE).

### Phase 3 – Cloud/Embedded Providers (2 sprints)
1. Snowflake provider with key-pair, SSO, and warehouse context prompts.
2. SQLite provider for local/offline validation; restrict to read-only by default.
3. Enhance CLI to accept provider-specific auth payloads (JSON or secure profile store).
4. Document limitations and recommended workloads per provider.

### Phase 4 – Polish & GA (1 sprint)
1. Harden telemetry (provider label in every log entry, aggregated performance metrics).
2. Update documentation, onboarding wizard, marketing screenshots.
3. Verify role-based menu gating across providers.
4. Execute final regression plan: `dotnet build`, UI smoke, CLI automation, `run_cli_tests`.
5. Produce final TASKLIST + verification artifacts (grep proof, build logs).

## Testing & Validation Strategy
- **Unit tests:** Mock provider contracts, ensure RBAC + formatting logic unaffected by provider.
- **Integration harness:** Containerized databases launched via scripts; capture snapshots to `Tests/Data`.
- **CLI matrix:** For each provider, run `ExecuteQuery`, `Export`, `CollectMetadata`.
- **UI smoke:** Automated WPF UI tests per provider profile covering connection, query, metadata views.
- **Performance baselines:** Log query timings per provider; set regression thresholds.
- **Offline validation:** Simulate air-gapped environment, verify bundled drivers and configuration.

## Risks & Mitigations
| Risk | Impact | Mitigation |
|------|--------|------------|
| Package size explosion | Slower downloads/offline copy | Enable driver opt-in during publish, modular installers |
| Oracle licensing constraints | Blocked builds | Evaluate redistribution terms, provide external download instructions if required |
| Feature fragmentation | Confusing UI | Capability matrix drives dynamic visibility + contextual messaging |
| RBAC mismatch | Security regressions | Document provider-specific role mappings, add automated checks |
| Testing complexity | Longer release cycles | Invest in reusable container scripts + CI automation |
| Performance regression | Poor UX | Add provider-specific caching and telemetry, profile before GA |

## Open Questions
1. Which providers require priority support for the first public build?
2. Are there compliance constraints preventing bundling certain drivers?
3. Should provider modules ship as optional plug-ins (MEF-style) or be compiled in?
4. Do we need custom formatters per dialect beyond PoorMansTSqlFormatter?
5. How will licensing and EULA acceptance be handled inside the installer?

## Next Actions
1. Approve roadmap + provider priority order.
2. Create engineering tickets aligned with the phases above (including estimates).
3. Begin Phase 0 refactor focusing on provider abstractions and DB2 parity.
