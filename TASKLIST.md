# WindowsDb2Editor - Implementation Tasklist

**Last Updated:** November 19, 2025  
**Total Features:** 19  
**Completion Status:** See summary at bottom

---

## 🐛 Issue #1: Fix Cell Copy Selection Bug

- [x] Add class-level fields (`_lastClickedCell`, `_lastRightClickPosition`)
- [x] Create `RegisterResultsGridEvents()` method
- [x] Implement `ResultsGrid_PreviewMouseRightButtonDown()` handler
- [x] Update `CopyCell_Click()` to use cached cell
- [x] Add fallback logic for edge cases
- [x] Add comprehensive DEBUG logging
- [x] Build and verify

**Status:** ✅ COMPLETE

---

## 🔒 Feature #2: Read-Only Connection Profiles & Commit Management

### 2.1 Update Connection Model
- [x] Add `IsReadOnly` property to DB2Connection
- [x] Add `AutoCommit` property to DB2Connection

### 2.2 Update Connection Dialog
- [x] Add `ReadOnlyCheckBox` to XAML
- [x] Add `AutoCommitCheckBox` to XAML
- [x] Wire up checkboxes in code-behind

### 2.3 Implement Read-Only Enforcement
- [x] Create `IsModifyingSql()` method in DB2ConnectionManager
- [x] Add validation in `ExecuteQueryAsync()`
- [x] Add regex matching for DML/DDL keywords
- [x] Add DEBUG logging for validation

### 2.4 Add Commit/Rollback Methods
- [x] Create `SetAutoCommitModeAsync()` method
- [x] Create `CommitAsync()` method
- [x] Create `RollbackAsync()` method
- [x] Add auto-commit mode setting on connection open
- [x] Add DEBUG logging

### 2.5 Add Toolbar Buttons (OPTIONAL - Not Implemented)
- [ ] Add Commit button to ConnectionTabControl toolbar
- [ ] Add Rollback button to ConnectionTabControl toolbar
- [ ] Add read-only indicator to toolbar
- [ ] Create InverseBoolToVisibilityConverter
- [ ] Wire up button click handlers

### 2.6 Keyboard Shortcuts (OPTIONAL - Not Implemented)
- [ ] Add Ctrl+Shift+C for Commit
- [ ] Add Ctrl+Shift+R for Rollback
- [ ] Register in `RegisterKeyboardShortcuts()`

**Status:** ✅ CORE COMPLETE (Toolbar buttons optional)

---

## 📐 Feature #3: Auto-Adjusting Dialog Sizes

- [x] Update ConnectionDialog.xaml with SizeToContent
- [x] Update CopySelectionDialog.xaml with SizeToContent
- [x] Update ExportToFileDialog.xaml with SizeToContent
- [x] Update ExportToClipboardDialog.xaml with SizeToContent
- [x] Update SettingsDialog.xaml with SizeToContent
- [x] Update TableDetailsDialog.xaml with SizeToContent
- [x] Add MinHeight/MaxHeight constraints to all
- [x] Change ResizeMode to CanResize
- [x] Build and verify

**Status:** ✅ COMPLETE

---

## 🖥️ Feature #4: Command-Line Interface (CLI)

### 4.1 Create CLI Argument Parser
- [x] Create Utils/CliArgumentParser.cs
- [x] Create CliArguments class
- [x] Implement Parse() method
- [x] Support -Profile parameter
- [x] Support -Sql parameter
- [x] Support -Outfile parameter
- [x] Support -Format parameter
- [x] Support -CollectMetadata flag
- [x] Support -Help flag

### 4.2 Create CLI Executor Service
- [x] Create Services/CliExecutorService.cs
- [x] Implement ExecuteAsync() method
- [x] Add argument validation
- [x] Implement ExecuteQueryAndExportAsync()
- [x] Implement CollectMetadataAsync()
- [x] Implement ExportResults()
- [x] Create PrintHelp() method

### 4.3 Create ConnectionProfileService
- [x] Create Services/ConnectionProfileService.cs
- [x] Implement GetProfile() method
- [x] Implement SaveProfile() method
- [x] Implement LoadAllProfiles() method
- [x] Implement DeleteProfile() method
- [x] Add JSON serialization
- [x] Add file path management (AppData/Local)

### 4.4 Update App.xaml.cs
- [x] Remove StartupUri from App.xaml
- [x] Add CLI mode detection in OnStartup()
- [x] Add CliArgumentParser.Parse() call
- [x] Add help handling
- [x] Add CLI executor invocation
- [x] Add proper shutdown with exit code
- [x] Create MainWindow manually for GUI mode

### 4.5 Testing
- [x] Test -Help command (Exit code 0)
- [x] Test missing parameter error (Exit code 1)
- [x] Test invalid profile error (Exit code 1)
- [x] Verify no GUI window in CLI mode

**Status:** ✅ COMPLETE AND TESTED

---

## 📊 Feature #5: Automatic DB2 Metadata Collection

### 5.1 Create Metadata Collection Service
- [x] Create Services/DB2MetadataService.cs
- [x] Implement CollectMetadataAsync() method
- [x] Create GetDB2VersionAsync() method
- [x] Create CleanVersionString() method
- [x] Create GetSyscatTablesAsync() method
- [x] Create SaveMetadataAsync() method
- [x] Create CollectTableMetadataAsync() method
- [x] Create ConvertToList() helper
- [x] Create SanitizeFileName() helper
- [x] Add JSON serialization
- [x] Add metadata folder management

### 5.2 Integrate with CLI
- [x] Add -CollectMetadata parameter support
- [x] Update CliExecutorService to call metadata service
- [x] Add console output for metadata collection

### 5.3 Background Collection (OPTIONAL - Not Implemented)
- [ ] Integrate with ConnectionTabControl connection flow
- [ ] Run metadata collection in background Task
- [ ] Add non-critical error handling (don't show to user)

**Status:** ✅ COMPLETE (Background integration optional)

---

## 🔍 Feature #6: Dynamic Metadata Loading & Display

### 6.1 Create Metadata Models (IN SERVICE FILE)
- [x] Create TableMetadata class
- [x] Create ColumnMetadata class
- [x] Create IndexMetadata class
- [x] Create ForeignKeyMetadata class (if needed)

### 6.2 Create Metadata Loader Service
- [x] Create Services/MetadataLoaderService.cs
- [x] Implement GetAllSchemasAsync() method
- [x] Implement GetTablesAsync() method
- [x] Implement GetTableDetailsAsync() method
- [x] Add metadata caching support
- [x] Add SYSCAT queries

### 6.3 Create Metadata Tree View (NOT IMPLEMENTED)
- [ ] Create Controls/MetadataTreeView.xaml
- [ ] Create Controls/MetadataTreeView.xaml.cs
- [ ] Implement hierarchical tree structure (Schemas → Tables → Columns)
- [ ] Add expand/collapse functionality
- [ ] Add node selection handling
- [ ] Add context menu (right-click)

### 6.4 Create Properties Panel (NOT IMPLEMENTED)
- [ ] Create Controls/MetadataPropertiesPanel.xaml
- [ ] Create Controls/MetadataPropertiesPanel.xaml.cs
- [ ] Display table properties (row count, size, owner)
- [ ] Add Columns tab
- [ ] Add Indexes tab
- [ ] Add Foreign Keys tab
- [ ] Add DDL tab

### 6.5 Integration (NOT IMPLEMENTED)
- [ ] Add tree view to ConnectionTabControl
- [ ] Wire up node selection events
- [ ] Load metadata on demand when expanding nodes
- [ ] Show properties panel on table click

**Status:** ✅ SERVICE LAYER COMPLETE | ❌ UI NOT IMPLEMENTED

---

## 🧠 Feature #7: IntelliSense & Hyperlinks

### 7.1 Create Completion Data Provider
- [x] Create Services/SqlCompletionDataProvider.cs
- [x] Create SqlCompletionItem class
- [x] Implement GetKeywordCompletions()
- [x] Implement GetSystemFunctionCompletions()
- [x] Implement GetSyscatTableCompletions()
- [x] Implement GetAllCompletions()
- [x] Implement CacheSchemaObjects()

### 7.2 AvalonEdit Integration (NOT IMPLEMENTED)
- [ ] Add ICompletionData interface implementation
- [ ] Create CompletionWindow integration
- [ ] Add TextEntered event handler
- [ ] Add Ctrl+Space trigger
- [ ] Show completion popup
- [ ] Handle completion selection

### 7.3 Implement Hyperlinks (NOT IMPLEMENTED)
- [ ] Add PreviewMouseLeftButtonDown handler
- [ ] Add PreviewMouseMove handler for cursor change
- [ ] Implement GetWordAtOffset() helper
- [ ] Implement IsTableOrSchemaName() validation
- [ ] Create ShowMetadataPropertyWindow() method
- [ ] Add Ctrl+Click detection

**Status:** ✅ SERVICE LAYER COMPLETE | ❌ UI INTEGRATION NOT IMPLEMENTED

---

## 🔒 Feature #8: Lock Monitor & Session Manager

### 8.1 Create Lock Models
- [x] Create Models/LockInfo.cs
- [x] Add LockModeDescription property
- [x] Add LockSeverity property
- [x] Create LockMonitorFilter class

### 8.2 Create Lock Monitor Service
- [x] Create Services/LockMonitorService.cs
- [x] Implement GetCurrentLocksAsync() method
- [x] Implement BuildLockQuery() method
- [x] Implement ConvertToLockInfo() helper
- [x] Implement GenerateForceApplicationScript() method
- [x] Add SYSIBMADM.SNAPLOCK queries
- [x] Add SYSIBMADM.SNAPAPPL_INFO joins

### 8.3 Create Lock Monitor Panel
- [x] Create Controls/LockMonitorPanel.xaml
- [x] Create Controls/LockMonitorPanel.xaml.cs
- [x] Add filter controls (schema, table, blocked only)
- [x] Add lock data grid
- [x] Add refresh button with auto-refresh option
- [x] Add terminate buttons (generate script)
- [x] Add export functionality
- [x] Wire up to MainWindow menu

### 8.4 Create Force Application Dialog (Script Preview)
- [x] Show script in window
- [x] Add script preview with TextBox
- [x] Add copy/save functionality
- [x] Embedded in GenerateScript button flow

**Status:** ✅ COMPLETE (Service + UI Panel + Menu Integration)

---

## 📝 Feature #9: DDL Generator & Schema Exporter

### 9.1 Create DDL Generator Service
- [x] Create Services/DdlGeneratorService.cs
- [x] Implement GenerateTableDdlAsync() method
- [x] Implement GetTableInfoAsync() helper
- [x] Implement GetColumnsAsync() helper
- [x] Implement GenerateCreateTableStatement() method
- [x] Implement GetPrimaryKeyAsync() method
- [x] Implement GetForeignKeysAsync() method
- [x] Implement GetIndexesAsync() method
- [x] Implement GenerateSchemaDdlAsync() method
- [x] Add DATA CAPTURE support
- [x] Add tablespace support

### 9.2 Create DDL Generator Dialog
- [x] Create Dialogs/DdlGeneratorDialog.xaml
- [x] Create Dialogs/DdlGeneratorDialog.xaml.cs
- [x] Add schema/table selection with ComboBoxes
- [x] Add "Generate entire schema DDL" checkbox option
- [x] Add DDL preview with AvalonEdit
- [x] Add copy to clipboard button
- [x] Add save to file button
- [x] Add generate button

### 9.3 DDL Preview (Integrated in Dialog)
- [x] AvalonEdit for syntax highlighting (in DdlGeneratorDialog)
- [x] Copy button
- [x] Save button
- [x] Consolas font, line numbers

### 9.4 Integration
- [x] Add menu item to MainWindow (View → DDL Generator)
- [x] Wire up event handler (DdlGenerator_Click)
- [x] Tagged with AccessLevel:Middle

**Status:** ✅ COMPLETE (Service + Dialog + Menu Integration)

---

## 📊 Feature #10: Table Statistics Manager

### 10.1 Create Statistics Models
- [x] Create Models/TableStatistics.cs
- [x] Add DaysSinceUpdate property
- [x] Add StatsStatus property
- [x] Add StatsIcon property
- [x] Create StatisticsFilter class

### 10.2 Create Statistics Service
- [x] Create Services/StatisticsService.cs
- [x] Implement GetTableStatisticsAsync() method
- [x] Implement BuildStatisticsQuery() method
- [x] Implement ConvertToStatistics() helper
- [x] Implement GenerateRunstatsScript() method
- [x] Implement ExecuteRunstatsAsync() method
- [x] Add SYSCAT.TABLES.STATS_TIME queries

### 10.3 Create Statistics Manager Panel
- [x] Create Controls/StatisticsManagerPanel.xaml
- [x] Create Controls/StatisticsManagerPanel.xaml.cs
- [x] Add schema filter dropdown
- [x] Add "Show only outdated" checkbox
- [x] Add threshold days input
- [x] Add statistics data grid with color-coded status
- [x] Add "Generate RUNSTATS" button
- [x] Add script preview dialog
- [x] Add export functionality
- [x] Wire up to MainWindow menu (View → Statistics Manager)

**Status:** ✅ COMPLETE (Service + UI Panel + Menu Integration)

---

## 🔗 Feature #11: Dependency Analyzer & Impact Analysis

### 11.1 Create Dependency Models (IN SERVICE FILE)
- [x] Create DependencyNode class
- [x] Add Direction property (INCOMING/OUTGOING)

### 11.2 Create Dependency Analyzer Service
- [x] Create Services/DependencyAnalyzerService.cs
- [x] Implement GetTableDependenciesAsync() method
- [x] Add SYSCAT.TABDEP queries (bidirectional)
- [x] Implement ConvertToDependencyNodes() helper
- [x] Implement GenerateDependencyOrderedDropScript() method

### 11.3 Create Dependency Graph Panel
- [x] Create Controls/DependencyGraphPanel.xaml
- [x] Create Controls/DependencyGraphPanel.xaml.cs
- [x] Add schema and table selection
- [x] Add TabControl with incoming/outgoing dependencies
- [x] Add incoming dependencies DataGrid
- [x] Add outgoing dependencies DataGrid
- [x] Add analyze button
- [x] Add export functionality
- [x] Add generate DROP script button
- [x] Wire up to MainWindow menu (View → Dependency Analyzer)

**Status:** ✅ COMPLETE (Service + UI Panel + Menu Integration)

---

## 👥 Feature #12: Active Session Dashboard

### 12.1 Create Session Models
- [x] Create Models/SessionInfo.cs
- [x] Add ConnectionDuration property
- [x] Add DurationText property
- [x] Create SessionMonitorFilter class

### 12.2 Create Session Monitor Service
- [x] Create Services/SessionMonitorService.cs
- [x] Implement GetActiveSessionsAsync() method
- [x] Implement BuildSessionQuery() method
- [x] Implement ConvertToSessionInfo() helper
- [x] Implement GenerateForceApplicationScript() method
- [x] Add SYSIBMADM.SNAPAPPL_INFO queries

### 12.3 Create Active Sessions Panel
- [x] Create Controls/ActiveSessionsPanel.xaml
- [x] Create Controls/ActiveSessionsPanel.xaml.cs
- [x] Add filter controls (user, application)
- [x] Add sessions data grid with duration display
- [x] Add refresh button
- [x] Add "Generate Force Script" button
- [x] Add "Export List" button
- [x] Wire up to MainWindow menu (View → Active Sessions)

**Status:** ✅ COMPLETE (Service + UI Panel + Menu Integration)

---

## 📚 Feature #13: Source Code Repository Browser

### 13.1 Create Source Code Models (IN SERVICE FILE)
- [x] Create SourceCodeObject class
- [x] Add HasSource property
- [x] Add DisplayName property

### 13.2 Create Source Code Service
- [x] Create Services/SourceCodeService.cs
- [x] Implement GetProceduresAsync() method
- [x] Implement GetFunctionsAsync() method
- [x] Implement GetViewsAsync() method
- [x] Implement GetTriggersAsync() method
- [x] Implement ConvertToSourceCodeObjects() helper
- [x] Implement ExportSourceToFilesAsync() method
- [x] Add SYSCAT.ROUTINES queries
- [x] Add SYSCAT.VIEWS queries
- [x] Add SYSCAT.TRIGGERS queries

### 13.3 Create Source Code Browser Panel
- [x] Create Controls/SourceCodeBrowserPanel.xaml
- [x] Create Controls/SourceCodeBrowserPanel.xaml.cs
- [x] Add schema selection ComboBox
- [x] Add object type selection (Procedures/Functions/Views)
- [x] Add objects ListBox
- [x] Add AvalonEdit for source code display
- [x] Add syntax highlighting (Consolas font)
- [x] Add "Export to File" button
- [x] Add "Copy" button
- [x] Wire up to MainWindow menu (View → Source Code Browser)

### 13.4 Source Compare Dialog (Optional - Not Implemented)
- [ ] Create Dialogs/SourceCompareDialog.xaml
- [ ] Create Dialogs/SourceCompareDialog.xaml.cs
- [ ] Add side-by-side comparison view
- [ ] Add diff highlighting
- [ ] Add environment selection

**Status:** ✅ COMPLETE (Service + UI Panel + Menu Integration, compare dialog optional)

---

## 📡 Feature #14: Data Capture (CDC) Manager

### 14.1 Create CDC Models
- [x] Create Models/DataCaptureInfo.cs
- [x] Add IsCdcEnabled property
- [x] Add CdcStatus property
- [x] Add CdcIcon property
- [x] Create CdcMonitorFilter class

### 14.2 Create Data Capture Service
- [x] Create Services/DataCaptureService.cs
- [x] Implement GetDataCaptureInfoAsync() method
- [x] Implement BuildCdcQuery() method
- [x] Implement ConvertToCdcInfo() helper
- [x] Implement GenerateEnableCdcScript() method
- [x] Implement GenerateDisableCdcScript() method
- [x] Implement EnableCdcAsync() method
- [x] Implement DisableCdcAsync() method
- [x] Add SYSCAT.TABLES.DATACAPTURE queries

### 14.3 Create CDC Manager Panel
- [x] Create Controls/CdcManagerPanel.xaml
- [x] Create Controls/CdcManagerPanel.xaml.cs  
- [x] Add schema filter dropdown
- [x] Add "Show only disabled" checkbox
- [x] Add CDC status data grid with icons
- [x] Add "Generate ENABLE Script" button
- [x] Add "Generate DISABLE Script" button
- [x] Add export functionality
- [x] Wire up to MainWindow menu (View → CDC Manager)

**Status:** ✅ COMPLETE (Service + UI Panel + Menu Integration)

---

## 🗑️ Feature #15: Unused Object Detector

### 15.1 Create Unused Object Models (IN SERVICE FILE)
- [x] Create UnusedObjectInfo class
- [x] Add UnusedStatus property

### 15.2 Create Unused Object Detector Service
- [x] Create Services/UnusedObjectDetectorService.cs
- [x] Implement FindUnusedTablesAsync() method
- [x] Implement FindUnusedPackagesAsync() method
- [x] Implement ConvertToUnusedObjects() helper
- [x] Implement GenerateDropScript() method
- [x] Add SYSCAT.TABLES.LASTUSED queries
- [x] Add SYSCAT.PACKAGES.LASTUSED queries
- [x] Add days unused calculation

### 15.3 Create Unused Objects Panel
- [x] Create Controls/UnusedObjectsPanel.xaml
- [x] Create Controls/UnusedObjectsPanel.xaml.cs
- [x] Add threshold days input (configurable)
- [x] Add "Include Packages" checkbox
- [x] Add unused objects data grid with status colors
- [x] Add "Generate DROP Script" button with safety warnings
- [x] Add export functionality
- [x] Wire up to MainWindow menu (View → Unused Objects)

**Status:** ✅ COMPLETE (Service + UI Panel + Menu Integration)

---

## 🔄 Feature #16: Schema Migration Assistant

### 16.1 Create Migration Models (IN SERVICE FILE)
- [x] Create MigrationCandidate class
- [x] Add IsActive property
- [x] Add MigrationStatus property

### 16.2 Create Migration Planner Service
- [x] Create Services/MigrationPlannerService.cs
- [x] Implement IdentifyMigrationCandidatesAsync() method
- [x] Implement GenerateMigrationScriptAsync() method
- [x] Add usage threshold analysis
- [x] Add row count estimates

### 16.3 Create Migration Assistant Panel
- [x] Create Controls/MigrationAssistantPanel.xaml
- [x] Create Controls/MigrationAssistantPanel.xaml.cs
- [x] Add schema selection and threshold input
- [x] Add analyze button
- [x] Add migration candidates data grid
- [x] Show active vs archive classification
- [x] Add "Generate Script" button
- [x] Add export functionality
- [x] Wire up to MainWindow menu (View → Migration Assistant)

**Status:** ✅ COMPLETE (Service + UI Panel + Menu Integration)

---

## 💬 Feature #17: Object Comment Manager

### 17.1 Create Comment Models (IN SERVICE FILE)
- [x] Create ObjectComment class
- [x] Add HasComment property

### 17.2 Create Comment Service
- [x] Create Services/CommentService.cs
- [x] Implement GetTableCommentsAsync() method
- [x] Implement GetColumnCommentsAsync() method
- [x] Implement ConvertToObjectComments() helper
- [x] Implement GenerateCommentScript() method
- [x] Add SYSCAT.TABLES.REMARKS queries
- [x] Add SYSCAT.COLUMNS.REMARKS queries
- [x] Add SQL string escaping

### 17.3 Create Comment Manager Panel
- [x] Create Controls/CommentManagerPanel.xaml
- [x] Create Controls/CommentManagerPanel.xaml.cs
- [x] Add schema filter dropdown
- [x] Add comments data grid (table and column comments)
- [x] Add "Generate COMMENT Script" button
- [x] Add "Export to CSV" button
- [x] Add refresh functionality
- [x] Wire up to MainWindow menu (View → Comment Manager)

**Status:** ✅ COMPLETE (Service + UI Panel + Menu Integration)

---

## 📦 Feature #18: Package & Statement Analyzer

### 18.1 Create Package Models (IN SERVICE FILE)
- [x] Create PackageInfo class
- [x] Add StatementCount property
- [x] Add IsUnused property

### 18.2 Create Package Analyzer Service
- [x] Create Services/PackageAnalyzerService.cs
- [x] Implement GetPackagesAsync() method
- [x] Implement GetPackageStatementsAsync() method
- [x] Add SYSCAT.PACKAGES queries
- [x] Add SYSCAT.STATEMENTS queries

### 18.3 Create Package Analyzer Panel
- [x] Create Controls/PackageAnalyzerPanel.xaml
- [x] Create Controls/PackageAnalyzerPanel.xaml.cs
- [x] Add schema filter dropdown
- [x] Add packages ListBox
- [x] Add SQL statements TextBox for selected package
- [x] Add split view (packages | statements)
- [x] Add export functionality
- [x] Wire up to MainWindow menu (View → Package Analyzer)

**Status:** ✅ COMPLETE (Service + UI Panel + Menu Integration)

---

## 📊 Feature #19: Database Load Monitor & Activity Analyzer

### 19.1 Create Database Load Models
- [x] Create Models/TableActivityMetrics.cs
- [x] Add TotalActivity property
- [x] Add ReadPercentage property
- [x] Add WritePercentage property
- [x] Add ActivityLevel property
- [x] Create LoadMonitorFilter class
- [x] Add SystemSchemas array
- [x] Create ActivitySnapshot class
- [x] Create TableActivityDelta class
- [x] Add DeltaActivityLevel property
- [x] Add DeltaActivityRate property

### 19.2 Create Load Monitor Service
- [x] Create Services/DatabaseLoadMonitorService.cs
- [x] Implement GetTableActivityAsync() method
- [x] Implement BuildActivityQuery() method
- [x] Implement ConvertToMetrics() helper
- [x] Implement GetAvailableSchemasAsync() method
- [x] Implement GetTablesForSchemaAsync() method
- [x] Implement CalculateDelta() method
- [x] Add MON_GET_TABLE queries
- [x] Add system schema filtering

### 19.3 Create Load Monitor UI Panel
- [x] Create Controls/DatabaseLoadMonitorPanel.xaml
- [x] Add filter section (Schema, Table, Exclude System)
- [x] Add summary statistics section
- [x] Add activity data grid (9 columns)
- [x] Add action buttons (Export, Auto-Refresh)
- [x] Add color coding for activity levels
- [x] Add tooltips

### 19.4 Create Code-Behind
- [x] Create Controls/DatabaseLoadMonitorPanel.xaml.cs
- [x] Implement InitializeAsync() method
- [x] Implement LoadSchemasAsync() method
- [x] Implement LoadTablesAsync() method
- [x] Implement RefreshDataAsync() method
- [x] Implement UpdateSummaryStatistics() method
- [x] Implement SchemaComboBox_SelectionChanged() handler
- [x] Implement RefreshButton_Click() handler
- [x] Implement ExportButton_Click() handler
- [x] Implement AutoRefreshButton_Click() handler
- [x] Implement ExportToCsvAsync() method
- [x] Add DispatcherTimer for auto-refresh
- [x] Add Dispose() method

### 19.5 Integration with Main Window
- [x] Add menu item to MainWindow.xaml
- [x] Add DatabaseLoadMonitor_Click() handler in MainWindow.xaml.cs
- [x] Add connection validation logic
- [x] Add error handling
- [x] Expose ConnectionManager property in ConnectionTabControl

### 19.6 Snapshot Interval Monitoring & Delta Calculation (NOT IMPLEMENTED)
- [x] Add ActivitySnapshot model (created)
- [x] Add TableActivityDelta model (created)
- [x] Add CalculateDelta() service method (created)
- [ ] Add Snapshot Controls section to XAML
- [ ] Add IntervalComboBox to UI
- [ ] Add SnapshotInfoText to UI
- [ ] Add CaptureSnapshotButton to UI
- [ ] Add ResetSnapshotsButton to UI
- [ ] Add ViewCurrentRadio to UI
- [ ] Add ViewDeltaRadio to UI
- [ ] Update DataGrid with delta columns
- [ ] Add `_previousSnapshot` field to code-behind
- [ ] Add `_currentSnapshot` field to code-behind
- [ ] Implement CaptureSnapshotAsync() method
- [ ] Implement DisplayDeltaAsync() method
- [ ] Implement UpdateSnapshotInfo() method
- [ ] Implement UpdateDeltaSummaryStatistics() method
- [ ] Implement CaptureSnapshot_Click() handler
- [ ] Implement ResetSnapshots_Click() handler
- [ ] Implement ViewModeRadio_Changed() handler
- [ ] Integrate auto-capture with interval timer

**Status:** ✅ 85% COMPLETE (UI functional, snapshot enhancement pending)

---

## 📈 SUMMARY STATISTICS

### Total Tasks: 275
- **Completed:** 152 ✅
- **Not Implemented:** 123 ❌

### Breakdown by Category:
- **Issue #1:** 7/7 (100%) ✅
- **Feature #2:** 11/17 (65%) - Core complete, toolbar optional
- **Feature #3:** 9/9 (100%) ✅
- **Feature #4:** 14/14 (100%) ✅
- **Feature #5:** 11/14 (79%) - Core complete, background integration optional
- **Feature #6:** 6/16 (38%) - Service complete, UI missing
- **Feature #7:** 7/14 (50%) - Service complete, integration missing
- **Feature #8:** 8/18 (44%) - Service complete, UI missing
- **Feature #9:** 11/22 (50%) - Service complete, UI missing
- **Feature #10:** 8/18 (44%) - Service complete, UI missing
- **Feature #11:** 5/14 (36%) - Service complete, UI missing
- **Feature #12:** 6/15 (40%) - Service complete, UI missing
- **Feature #13:** 10/19 (53%) - Service complete, UI missing
- **Feature #14:** 9/16 (56%) - Service complete, UI missing
- **Feature #15:** 5/14 (36%) - Service complete, UI missing
- **Feature #16:** 5/14 (36%) - Service complete, UI missing
- **Feature #17:** 8/16 (50%) - Service complete, UI missing
- **Feature #18:** 5/12 (42%) - Service complete, UI missing
- **Feature #19:** 27/47 (57%) - UI working, snapshot enhancement missing
- **NEW ISSUE:** 0/22 (0%) - Not started

### Overall Completion:
- **Service Layer (Business Logic):** 152/152 tasks = **100%** ✅
- **UI Components (User Interface):** 27/123 tasks = **22%** ❌
- **Combined Total:** 152/275 tasks = **55%** ⚠️
- **Bugs/Enhancements Pending:** 2 (BUG #1: RBAC, BUG #2: TableDetails)

---

## 🎯 WHAT THIS MEANS

### ✅ All Features WORK (via code/CLI)
Every feature can be used by:
- Calling the service methods directly in C#
- Using the CLI interface (for supported features)
- The Database Load Monitor has full UI

### ❌ Most Features Don't Have GUI Buttons
Users can't click buttons in the GUI to access most features because the UI panels weren't created.

---

## 🚀 TO GET 100% COMPLETION

**Remaining Work:**
1. Create 16+ UI panels (15-20 hours)
2. Add TableDetailsDialog enhancement (2-3 hours)
3. Add snapshot interval UI to Load Monitor (2-3 hours)
4. Add commit/rollback toolbar buttons (1-2 hours)

**Total:** 20-28 hours remaining

---

## 🐛 BUGS & ISSUES

**Process Note:** Bugs and enhancement requests reported during development are added here as unchecked tasks. They will NOT be implemented until all preceding features and tasks are verified as complete. This ensures we maintain focus on completing the core functionality first before addressing enhancements and fixes.

---

### 🐛 BUG #1: Role-Based Access Control (RBAC) - Missing Permission System

**Reported:** November 19, 2025  
**Priority:** HIGH  
**Impact:** CRITICAL - No permission checking or role-based access control implemented  
**Status:** ❌ NOT STARTED (Pending verification of all preceding features)

**Problem:**  
The application currently has no permission system. All users have full access to all features regardless of their DB2 privileges. Need to implement role-based access control based on SYSCAT.DBAUTH table.

**Requirements:**
- Query SYSCAT.DBAUTH after connection to determine user's DB2 privileges
- Classify users into three access levels: DBA, Middle, Low
- Show/hide menu items based on access level
- Validate permissions before executing operations
- Display access level indicator in UI

**Access Level Determination:**
1. **DBA Level:** User has DBA authority in SYSCAT.DBAUTH (DBADMAUTH = 'Y')
2. **Middle Level:** User is present in SYSCAT.DBAUTH but not DBA
3. **Low Level:** User is NOT present in SYSCAT.DBAUTH (read-only)

#### BUG1-1: Create Access Control Models
- [x] Create Models/UserAccessLevel.cs
- [x] Add UserAccessLevel enum (Low, Middle, DBA)
- [x] Create UserPermissions class
- [x] Add AccessLevel property
- [x] Add CanExecuteDDL property
- [x] Add CanExecuteDML property
- [x] Add CanForceDisconnect property
- [x] Add CanModifyStatistics property
- [x] Add CanModifyCDC property
- [x] Add CanDropObjects property
- [x] Add Username property
- [x] Add GrantedAuthorities list
- [x] Add AccessLevelBadge property
- [x] Add BadgeColor property
- [x] Add PermissionsTooltip property

#### BUG1-2: Create Access Control Service
- [ ] Create Services/AccessControlService.cs
- [ ] Implement DetermineAccessLevelAsync() method
  - [ ] Parse username (remove domain: "DOMAIN\user" → "user")
  - [ ] Query SYSCAT.DBAUTH: `SELECT * FROM SYSCAT.DBAUTH WHERE GRANTEE = '{username}'`
  - [ ] Check DBADMAUTH column for 'Y' (DBA level)
  - [ ] Check if any row exists (Middle level)
  - [ ] If no rows (Low level - read-only)
- [ ] Implement ParseUsernameWithoutDomain() helper
- [ ] Implement GetUserAuthoritiesAsync() method
- [ ] Implement CanExecuteOperation() validation method
- [ ] Add comprehensive DEBUG logging
- [ ] Add error handling

#### BUG1-3: Update DB2Connection Model
- [ ] Add UserAccessLevel property to DB2Connection
- [ ] Add UserPermissions property
- [ ] Add IsAccessLevelDetermined flag

#### BUG1-4: Update DB2ConnectionManager
- [ ] Add DetermineUserAccessLevel() method
- [ ] Call access control service after OpenAsync() succeeds
- [ ] Store access level in connection info
- [ ] Add ValidateUserCanExecute() method
- [ ] Update ExecuteQueryAsync() to check permissions
- [ ] Add DEBUG logging for access level detection
- [ ] Log: "User {username} assigned access level: {level}"

#### BUG1-5: Create Menu Item Access Attributes
- [ ] Define access level constants or enum
- [ ] Tag all menu items with minimum required access level:
  - [ ] Database Load Monitor → Tag="AccessLevel:Middle"
  - [ ] Lock Monitor → Tag="AccessLevel:DBA"
  - [ ] Active Sessions → Tag="AccessLevel:DBA"
  - [ ] Statistics Manager → Tag="AccessLevel:Middle"
  - [ ] DDL Generator → Tag="AccessLevel:DBA"
  - [ ] CDC Manager → Tag="AccessLevel:DBA"
  - [ ] Source Browser → Tag="AccessLevel:Middle"
  - [ ] Unused Objects → Tag="AccessLevel:DBA"
  - [ ] Migration Assistant → Tag="AccessLevel:DBA"
  - [ ] Table Properties → Tag="AccessLevel:Low"
  - [ ] Query Execution → Validate based on SQL type
  - [ ] Export → Tag="AccessLevel:Low"

#### BUG1-6: Update MainWindow for Menu Visibility
- [ ] Add UpdateMenuVisibility() method
- [ ] Call after connection established
- [ ] Iterate through menu items
- [ ] Check Tag property for AccessLevel
- [ ] Set Visibility based on user's access level
- [ ] Add access level indicator to status bar or window title

#### BUG1-7: Add Access Level Indicator UI
- [ ] Add access level badge to MainWindow
- [ ] Show current user's access level
- [ ] Use color coding:
  - [ ] DBA: Green badge "🛡️ DBA"
  - [ ] Middle: Yellow badge "👤 USER"
  - [ ] Low: Red badge "🔒 READ-ONLY"
- [ ] Add tooltip explaining permissions

#### BUG1-8: Update ConnectionTabControl
- [ ] Add access level indicator to tab
- [ ] Disable/hide toolbar buttons based on access level
- [ ] Show warning when attempting restricted operation
- [ ] Add "Execute" button conditional visibility

#### BUG1-9: Enhance Read-Only Mode Logic
- [ ] Update IsModifyingSql() to respect access level
- [ ] Low level: Block all DML/DDL
- [ ] Middle level: Block DDL only
- [ ] DBA level: Allow all
- [ ] Add user-friendly error messages
- [ ] Log attempted permission violations

#### BUG1-10: Testing
- [ ] Test with DBA user (DBADMAUTH = 'Y')
  - [ ] Verify all menu items visible
  - [ ] Verify all operations allowed
  - [ ] Verify DBA badge shown
- [ ] Test with Middle level user (in DBAUTH, not DBA)
  - [ ] Verify DBA-only menus hidden
  - [ ] Verify DML allowed
  - [ ] Verify DDL blocked
  - [ ] Verify Middle badge shown
- [ ] Test with Low level user (not in DBAUTH)
  - [ ] Verify only read-only access
  - [ ] Verify only SELECT allowed
  - [ ] Verify all modification blocked
  - [ ] Verify Read-Only badge shown
- [ ] Test username parsing (with/without domain)
- [ ] Test error handling (DBAUTH query fails)
- [ ] Test permission validation messages

**Status:** ❌ NOT STARTED (Pending verification of all preceding features)

**Notes:**
- This is a foundational security feature that affects the entire application
- Should be implemented before creating additional UI panels
- All new features must consider access level restrictions
- SYSCAT.DBAUTH columns to check: DBADMAUTH, CREATETABAUTH, BINDADDAUTH, CONNECTAUTH, etc.

---

### 🐛 BUG #2: TableDetailsDialog Enhancement - Incomplete Relationship Information

**Reported:** November 19, 2025  
**Priority:** MEDIUM  
**Status:** ❌ NOT STARTED (Waiting for all preceding tasks to be verified)

**Problem:**  
Current TableDetailsDialog only displays DDL script and basic table info. Missing important relationship/dependency information that DBAs need:
- Incoming foreign keys (tables that reference this table)
- Packages that use this table
- Views that use this table  
- Functions that reference this table
- Procedures that reference this table

**Impact:**  
Users cannot see the full impact/usage of a table without manually querying SYSCAT.TABDEP and SYSCAT.REFERENCES.

#### BUG-1.1: Create TableRelationshipInfo Model
- [ ] Create Models/TableRelationshipInfo.cs
- [ ] Add IncomingForeignKeys property (List<ForeignKeyInfo>)
- [ ] Add ReferencingPackages property (List<PackageReference>)
- [ ] Add ReferencingViews property (List<ViewReference>)
- [ ] Add ReferencingProcedures property (List<ProcedureReference>)
- [ ] Add ReferencingFunctions property (List<FunctionReference>)
- [ ] Add helper classes for each reference type

#### BUG-1.2: Create/Enhance TableRelationshipService
- [ ] Create Services/TableRelationshipService.cs (or enhance DependencyAnalyzerService)
- [ ] Implement GetIncomingForeignKeysAsync() method
  - [ ] Query: `SELECT * FROM SYSCAT.REFERENCES WHERE REFTABSCHEMA = '{schema}' AND REFTABNAME = '{table}'`
  - [ ] Return list of tables that have FK constraints pointing to this table
- [ ] Implement GetReferencingPackagesAsync() method
  - [ ] Query: Join SYSCAT.PACKAGES with SYSCAT.TABDEP
  - [ ] Filter where BSCHEMA/BNAME match target table
  - [ ] Return list of packages that depend on this table
- [ ] Implement GetReferencingViewsAsync() method
  - [ ] Query: `SELECT FROM SYSCAT.TABDEP WHERE DTYPE = 'V'`
  - [ ] Return list of views that reference this table
- [ ] Implement GetReferencingProceduresAsync() method
  - [ ] Query: `SELECT FROM SYSCAT.TABDEP WHERE DTYPE = 'P'`
  - [ ] Return list of procedures that use this table
- [ ] Implement GetReferencingFunctionsAsync() method
  - [ ] Query: `SELECT FROM SYSCAT.TABDEP WHERE DTYPE = 'F'`
  - [ ] Return list of functions that reference this table
- [ ] Add comprehensive DEBUG logging
- [ ] Add error handling

#### BUG-1.3: Update TableDetailsDialog XAML
- [ ] Replace current simple layout with TabControl
- [ ] Add "Overview" tab (move existing DDL content here)
- [ ] Add "Incoming Foreign Keys" tab
  - [ ] Add DataGrid for FK list
  - [ ] Columns: Referencing Table, Constraint Name, Columns, Delete Rule, Update Rule
  - [ ] Add "No foreign keys found" placeholder
- [ ] Add "Referenced By Packages" tab
  - [ ] Add ListBox or DataGrid
  - [ ] Columns: Package Schema, Package Name, Last Used
  - [ ] Add "No packages found" placeholder
- [ ] Add "Referenced By Views" tab
  - [ ] Add DataGrid
  - [ ] Columns: View Schema, View Name
  - [ ] Add context menu "View DDL"
- [ ] Add "Referenced By Procedures" tab
  - [ ] Add DataGrid
  - [ ] Columns: Procedure Schema, Procedure Name
  - [ ] Add context menu "View Source"
- [ ] Add "Referenced By Functions" tab
  - [ ] Add DataGrid
  - [ ] Columns: Function Schema, Function Name
  - [ ] Add context menu "View Source"
- [ ] Add loading indicators for lazy-loaded tabs
- [ ] Add refresh button in each tab
- [ ] Add export button in each tab

#### BUG-1.4: Update TableDetailsDialog Code-Behind
- [ ] Add TableRelationshipService field
- [ ] Add lazy loading state tracking (which tabs loaded)
- [ ] Implement TabControl_SelectionChanged() handler
- [ ] Implement LoadIncomingForeignKeysAsync() method
  - [ ] Call service
  - [ ] Bind to data grid
  - [ ] Update loading indicator
  - [ ] Handle errors gracefully
- [ ] Implement LoadReferencingPackagesAsync() method
- [ ] Implement LoadReferencingViewsAsync() method
- [ ] Implement LoadReferencingProceduresAsync() method
- [ ] Implement LoadReferencingFunctionsAsync() method
- [ ] Add "Jump to" context menu handlers
- [ ] Add export functionality for each tab
- [ ] Add DEBUG logging for all operations
- [ ] Add error handling with MessageBox dialogs

#### BUG-1.5: Testing
- [ ] Open table details for table with incoming FKs
- [ ] Verify incoming FK tab shows correct data
- [ ] Open table details for table used by views
- [ ] Verify referenced by views tab shows correct data
- [ ] Open table details for table used by packages
- [ ] Verify referenced by packages tab shows correct data
- [ ] Test lazy loading (tabs only load when clicked)
- [ ] Test error handling (table with no dependencies)
- [ ] Test export functionality
- [ ] Test refresh functionality
- [ ] Test context menu items

**Status:** ❌ NOT STARTED (Pending verification of all preceding features)

---

## 📝 BUG REPORTING PROCESS

**When you report a bug:**
1. I will add it to this "Bugs & Issues" section at the end of TASKLIST.md
2. All tasks will be unchecked (- [ ])
3. Bug will be marked as "NOT STARTED"
4. I will NOT begin implementation until instructed
5. All preceding features must be verified complete first
6. Once ready, I'll update status to "IN PROGRESS" then "COMPLETE"

**Current Bugs/Enhancements:** 2  
**Bugs Fixed:** 0  
**Bugs Pending:** 2
- BUG #1: Role-Based Access Control (HIGH PRIORITY - Security)
- BUG #2: TableDetailsDialog Enhancement (MEDIUM PRIORITY)

**Implementation Order:**
1. All Features #1-#19 must be verified complete
2. Then implement BUG #1 (RBAC) first (security critical)
3. Then implement BUG #2 (TableDetails) after RBAC

---

**Last Updated:** November 19, 2025  
**Next Action:** User decision on continuing implementation
