# Final Transformation - Database-Agnostic + Bilingual Support

**Goal**: Complete database-agnostic JSON abstraction + Norwegian/English bilingual support

**Status**: 0% - Task list created, ready for continuous implementation

**Date**: 2025-12-13

---

## Phase A: SQL Statement Migration to JSON (60 tasks)

### A1: Extract SQL from Dialogs (15 tasks)
- [ ] Extract SQL from ConnectionDialog.xaml.cs
- [ ] Extract SQL from TableDetailsDialog.xaml.cs (9 tabs worth)
- [ ] Extract SQL from ObjectDetailsDialog.xaml.cs
- [ ] Extract SQL from UserDetailsDialog.xaml.cs
- [ ] Extract SQL from PackageDetailsDialog.xaml.cs
- [ ] Extract SQL from SqlStatementViewerDialog.xaml.cs
- [ ] Extract SQL from ConnectionProgressDialog.xaml.cs
- [ ] Extract SQL from SchemaTableSelectionDialog.xaml.cs
- [ ] Extract SQL from ConnectionInfoDialog.xaml.cs
- [ ] Extract SQL from SettingsDialog.xaml.cs
- [ ] Extract SQL from ExportToClipboardDialog.xaml.cs
- [ ] Extract SQL from CopySelectionDialog.xaml.cs
- [ ] Extract SQL from ExportToFileDialog.xaml.cs
- [ ] Add all dialog SQL to db2_12.1_sql_statements.json with agnostic names
- [ ] Update all dialogs to use MetadataHandler.GetQuery()

### A2: Extract SQL from Panels (12 tasks)
- [ ] Extract SQL from WelcomePanel.xaml.cs
- [ ] Extract SQL from DatabaseLoadMonitorPanel.xaml.cs
- [ ] Extract SQL from MigrationAssistantPanel.xaml.cs
- [ ] Extract SQL from DependencyGraphPanel.xaml.cs
- [ ] Extract SQL from PackageAnalyzerPanel.xaml.cs
- [ ] Extract SQL from CommentManagerPanel.xaml.cs
- [ ] Extract SQL from SourceCodeBrowserPanel.xaml.cs
- [ ] Extract SQL from UnusedObjectsPanel.xaml.cs
- [ ] Extract SQL from CdcManagerPanel.xaml.cs
- [ ] Extract SQL from ActiveSessionsPanel.xaml.cs
- [ ] Extract SQL from StatisticsManagerPanel.xaml.cs
- [ ] Extract SQL from LockMonitorPanel.xaml.cs

### A3: Extract SQL from Services (20 tasks)
- [ ] Extract SQL from AccessControlService.cs
- [ ] Extract SQL from CommentService.cs
- [ ] Extract SQL from ConnectionStatisticsService.cs
- [ ] Extract SQL from DatabaseLoadMonitorService.cs
- [ ] Extract SQL from DataCaptureService.cs
- [ ] Extract SQL from DB2MetadataService.cs (largest service)
- [ ] Extract SQL from DdlGeneratorService.cs
- [ ] Extract SQL from DependencyAnalyzerService.cs
- [ ] Extract SQL from ExportService.cs
- [ ] Extract SQL from LockMonitorService.cs
- [ ] Extract SQL from MigrationPlannerService.cs
- [ ] Extract SQL from ObjectBrowserService.cs
- [ ] Extract SQL from PackageAnalyzerService.cs
- [ ] Extract SQL from SessionMonitorService.cs
- [ ] Extract SQL from SourceCodeService.cs
- [ ] Extract SQL from StatisticsService.cs
- [ ] Extract SQL from TableRelationshipService.cs
- [ ] Extract SQL from UnusedObjectDetectorService.cs
- [ ] Add all service SQL to db2_12.1_sql_statements.json
- [ ] Update all services to use MetadataHandler.GetQuery()

### A4: Migrate CLI SQL Statements (13 tasks)
- [ ] Add all 90 CLI command SQL queries to db2_12.1_sql_statements.json
- [ ] Name them with CLI prefix (e.g., "CLI_GetTableColumns")
- [ ] Update CliCommandHandlerService to use MetadataHandler.GetQuery()
- [ ] Verify all 90 CLI commands still work after migration
- [ ] Test full CLI suite (90 commands)
- [ ] Document CLI query naming convention
- [ ] Create CLI query reference guide
- [ ] Ensure CLI queries have proper descriptions
- [ ] Test CLI with FKKTOTST database
- [ ] Verify JSON output format unchanged
- [ ] Update CLI help text with query source info
- [ ] Document query parameter substitution pattern
- [ ] Validate all CLI queries compile and execute

---

## Phase B: Norwegian Localization (40 tasks)

### B1: Create Norwegian Text Files (5 tasks)
- [ ] Create db2_12.1_no-NO_texts.json
- [ ] Translate all 119 text keys from en-US to Norwegian
- [ ] Add Norwegian menu labels
- [ ] Add Norwegian button labels
- [ ] Add Norwegian status messages and tooltips

### B2: Update All Dialogs for Localization (13 tasks)
- [ ] Update ConnectionDialog to use GetText()
- [ ] Update TableDetailsDialog to use GetText()
- [ ] Update ObjectDetailsDialog to use GetText()
- [ ] Update UserDetailsDialog to use GetText()
- [ ] Update PackageDetailsDialog to use GetText()
- [ ] Update SqlStatementViewerDialog to use GetText()
- [ ] Update ConnectionProgressDialog to use GetText()
- [ ] Update SchemaTableSelectionDialog to use GetText()
- [ ] Update ConnectionInfoDialog to use GetText()
- [ ] Update SettingsDialog to use GetText() + add language selector
- [ ] Update ExportToClipboardDialog to use GetText()
- [ ] Update CopySelectionDialog to use GetText()
- [ ] Update ExportToFileDialog to use GetText()

### B3: Update All Panels for Localization (12 tasks)
- [ ] Update WelcomePanel to use GetText()
- [ ] Update DatabaseLoadMonitorPanel to use GetText()
- [ ] Update MigrationAssistantPanel to use GetText()
- [ ] Update DependencyGraphPanel to use GetText()
- [ ] Update PackageAnalyzerPanel to use GetText()
- [ ] Update CommentManagerPanel to use GetText()
- [ ] Update SourceCodeBrowserPanel to use GetText()
- [ ] Update UnusedObjectsPanel to use GetText()
- [ ] Update CdcManagerPanel to use GetText()
- [ ] Update ActiveSessionsPanel to use GetText()
- [ ] Update StatisticsManagerPanel to use GetText()
- [ ] Update LockMonitorPanel to use GetText()

### B4: Update MainWindow & Core UI (5 tasks)
- [ ] Update MainWindow.xaml menus to use GetText()
- [ ] Update MainWindow.xaml.cs status messages to use GetText()
- [ ] Update ConnectionTabControl to use GetText()
- [ ] Update all MessageBox calls to use GetText()
- [ ] Update all error messages to use GetText()

### B5: Language Switching & Preferences (5 tasks)
- [ ] Add language preference to PreferencesService
- [ ] Add language dropdown to SettingsDialog
- [ ] Implement runtime language switching
- [ ] Test Norwegian language display
- [ ] Test English language display (default)

---

## Phase C: Full DB2 Support Verification (25 tasks)

### C1: Verify All GUI Forms Work (13 tasks)
- [ ] Test ConnectionDialog (connect to FKKTOTST)
- [ ] Test TableDetailsDialog - all 9 tabs (ASK.VASK_KUNDER)
- [ ] Test ObjectDetailsDialog (package details)
- [ ] Test UserDetailsDialog (current user)
- [ ] Test DatabaseLoadMonitorPanel
- [ ] Test LockMonitorPanel
- [ ] Test ActiveSessionsPanel
- [ ] Test StatisticsManagerPanel
- [ ] Test SourceCodeBrowserPanel
- [ ] Test PackageAnalyzerPanel
- [ ] Test CommentManagerPanel
- [ ] Test UnusedObjectsPanel
- [ ] Test CdcManagerPanel

### C2: Verify All SQL Queries Work (12 tasks)
- [ ] Test all SYSCAT.TABLES queries
- [ ] Test all SYSCAT.COLUMNS queries
- [ ] Test all SYSCAT.INDEXES queries
- [ ] Test all SYSCAT.REFERENCES queries (FKs)
- [ ] Test all SYSCAT.VIEWS queries
- [ ] Test all SYSCAT.ROUTINES queries
- [ ] Test all SYSCAT.TRIGGERS queries
- [ ] Test all SYSCAT.PACKAGES queries
- [ ] Test all dependency queries (TABDEP, ROUTINEDEP, PACKAGEDEP)
- [ ] Test all statistics queries
- [ ] Test all monitoring queries (simplified for DB2 12.1)
- [ ] Document any DB2 version-specific quirks

---

## Phase D: Integration & Testing (15 tasks)

### D1: JSON Configuration Validation (5 tasks)
- [ ] Verify db2_12.1_sql_statements.json has ALL queries (200+)
- [ ] Verify db2_12.1_en-US_texts.json has ALL text keys (150+)
- [ ] Verify db2_12.1_no-NO_texts.json has ALL text keys (150+)
- [ ] Verify db2_12.1_keywords.json has ALL keywords (100+)
- [ ] Verify supported_providers.json is correct

### D2: Service Integration (5 tasks)
- [ ] Verify all services use MetadataHandler
- [ ] Verify no hardcoded SQL remains
- [ ] Verify no hardcoded UI text remains
- [ ] Test all services with FKKTOTST database
- [ ] Document service dependencies on MetadataHandler

### D3: CLI Integration (5 tasks)
- [ ] Verify all 90 CLI commands use JSON queries
- [ ] Test all 90 commands after JSON migration
- [ ] Verify JSON output format unchanged
- [ ] Update CLI documentation
- [ ] Create CLI regression test suite

---

## Phase E: Documentation & Finalization (10 tasks)

### E1: Documentation (5 tasks)
- [ ] Update README.md with JSON configuration guide
- [ ] Document all SQL statement keys in db2_12.1_sql_statements.json
- [ ] Document all UI text keys in texts.json files
- [ ] Create localization guide for adding new languages
- [ ] Create provider addition guide (for PostgreSQL, etc.)

### E2: Final Verification (5 tasks)
- [ ] Build project (0 errors, 0 warnings)
- [ ] Test GUI with Norwegian language
- [ ] Test GUI with English language
- [ ] Test all 90 CLI commands
- [ ] Create final completion report

---

## Summary

**Total Tasks**: 165 tasks across 5 phases

**Breakdown**:
- Phase A: SQL Migration (60 tasks)
- Phase B: Norwegian Localization (40 tasks)
- Phase C: DB2 Verification (25 tasks)
- Phase D: Integration & Testing (15 tasks)
- Phase E: Documentation (10 tasks)

**Estimated Effort**: 8-12 hours (continuous mode)

**Completion Protocol**:
1. ✅ Implement all tasks
2. ✅ Build and test
3. ✅ Add to repo
4. ✅ Commit
5. ✅ Push to remote (sync)
6. ✅ Send SMS when complete

---

**Ready for Continuous Implementation**: YES  
**Started**: 2025-12-13 (waiting for go signal)

