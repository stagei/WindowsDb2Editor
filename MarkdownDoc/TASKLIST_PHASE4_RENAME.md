# Phase 4: Rename DB2 → Db (Provider-Agnostic Naming) - Task List

**Goal:** Rename all DB2-specific names to provider-agnostic equivalents  
**Estimated Duration:** 6 weeks  
**Priority:** 🟢 MEDIUM

---

## Status Summary
- **Total Tasks:** 50
- **Completed:** 0
- **In Progress:** 0
- **Pending:** 50

---

## Tasks

### Week 6: Rename Core Classes

#### 4.1 Rename DB2ConnectionManager → DbConnectionManager
- [ ] Already done in Phase 2 ✅

#### 4.2 Rename DB2Connection → DbConnectionProfile
- [ ] Already done in Phase 3 ✅

#### 4.3 Rename DB2MetadataService → DbMetadataService
- [ ] Rename `Services/DB2MetadataService.cs` to `Services/DbMetadataService.cs`
- [ ] Rename class `DB2MetadataService` to `DbMetadataService`
- [ ] Update all using statements
- [ ] Update DI registration in App.xaml.cs
- [ ] Update all service references
- [ ] Test build succeeds
- [ ] Test application runs

#### 4.4 Update Service References
- [ ] Grep for "DB2MetadataService" in all .cs files
- [ ] Replace with "DbMetadataService"
- [ ] Grep for "DB2ConnectionManager" in all .cs files (if any remain)
- [ ] Replace with "DbConnectionManager"
- [ ] Grep for "DB2Connection" model references (not IBM's class)
- [ ] Replace with "DbConnectionProfile"
- [ ] Test build succeeds

### Week 7: Rename Namespace

#### 4.5 Rename Project Files
- [ ] Rename `WindowsDb2Editor.csproj` to `DbExplorer.csproj`
- [ ] Rename `WindowsDb2Editor.sln` to `DbExplorer.sln`
- [ ] Update solution file references
- [ ] Test solution opens in Visual Studio
- [ ] Test build succeeds

#### 4.6 Update Project Settings
- [ ] Update `<AssemblyName>` to `DbExplorer`
- [ ] Update `<RootNamespace>` to `DbExplorer`
- [ ] Update `<Product>` to `DbExplorer`
- [ ] Update `<ApplicationIcon>` if needed
- [ ] Update `<StartupObject>` if needed
- [ ] Test build succeeds
- [ ] Verify output is `DbExplorer.exe`

#### 4.7 Rename Namespace in All Files (Part 1)
- [ ] Grep for "namespace WindowsDb2Editor" in all .cs files
- [ ] Count total occurrences (expect ~50 files)
- [ ] Replace in App.xaml.cs
- [ ] Replace in MainWindow.xaml.cs
- [ ] Replace in all files in Data/
- [ ] Replace in all files in Services/
- [ ] Replace in all files in Models/
- [ ] Test build after each group

#### 4.7 Rename Namespace in All Files (Part 2)
- [ ] Replace in all files in Controls/
- [ ] Replace in all files in Dialogs/
- [ ] Replace in all files in Utils/
- [ ] Replace in all files in Panels/
- [ ] Test build succeeds after all changes

### Week 8: Update All References

#### 4.8 Update Using Statements
- [ ] Grep for "using WindowsDb2Editor" in all .cs files
- [ ] Replace with "using DbExplorer"
- [ ] Update all occurrences
- [ ] Test build succeeds

#### 4.9 Update XAML Namespaces
- [ ] Grep for "clr-namespace:WindowsDb2Editor" in all .xaml files
- [ ] Replace with "clr-namespace:DbExplorer"
- [ ] Update App.xaml
- [ ] Update MainWindow.xaml
- [ ] Update all dialog XAML files
- [ ] Update all UserControl XAML files
- [ ] Test build succeeds
- [ ] Test application starts

#### 4.10 Update Resource References
- [ ] Check ResourceDictionary references
- [ ] Update any namespace references in resources
- [ ] Test themes still work
- [ ] Test dark mode toggle

### Week 9: Update User-Facing Text

#### 4.11 Update Window Titles
- [ ] Update MainWindow.xaml Title to "DbExplorer"
- [ ] Update ConnectionDialog Title
- [ ] Update SettingsDialog Title
- [ ] Update all other dialog titles
- [ ] Test windows display correct titles

#### 4.12 Update ConfigFiles Text
- [ ] Update `db2_12.1_en-US_texts.json`
- [ ] Change "MainFormTitle" to "DbExplorer"
- [ ] Update any "DB2 Database Editor" references
- [ ] Test application displays "DbExplorer"

#### 4.13 Update About Dialog
- [ ] Update About dialog product name
- [ ] Update version display
- [ ] Update copyright if needed
- [ ] Test About dialog displays correctly

### Week 10: Update Build Configuration

#### 4.14 Update Build Scripts
- [ ] Update any build scripts with new exe name
- [ ] Update deployment scripts
- [ ] Update documentation with new exe name

#### 4.15 Update Kill/Build/Run Commands
- [ ] Update PowerShell one-liner to use `DbExplorer.exe`
- [ ] Test: `taskkill /F /IM DbExplorer.exe 2>$null`
- [ ] Test: Build and run
- [ ] Update `.cursorrules` with new exe name

#### 4.16 Update NLog Configuration
- [ ] Update nlog.config if it references assembly name
- [ ] Test logging still works
- [ ] Verify log files created correctly

#### 4.17 Update AppSettings
- [ ] Review appsettings.json for any WindowsDb2Editor references
- [ ] Update if needed
- [ ] Test configuration loads correctly

### Week 11: Comprehensive Testing

#### 4.18 Verify No "WindowsDb2Editor" Remains
- [ ] Grep for "WindowsDb2Editor" in all .cs files → expect 0
- [ ] Grep for "WindowsDb2Editor" in all .xaml files → expect 0
- [ ] Grep for "WindowsDb2Editor" in all .json files → expect 0
- [ ] Grep for "WindowsDb2Editor" in all .md files → expect 0
- [ ] Grep for "DB2Connection" (model, not IBM) → expect 0
- [ ] Grep for "DB2ConnectionManager" → expect 0
- [ ] Grep for "DB2MetadataService" → expect 0

#### 4.19 Verify IBM DB2 Classes Untouched
- [ ] Verify `IBM.Data.Db2.DB2Connection` still used (IBM's class)
- [ ] Verify `DB2Parameter` still used (IBM's class)
- [ ] Verify `DB2DataAdapter` still used (IBM's class)
- [ ] Verify `DB2Exception` still used (IBM's class)
- [ ] Test DB2 connectivity still works

#### 4.20 Full Application Testing
- [ ] Test application starts with "DbExplorer" title
- [ ] Test connection dialog shows providers
- [ ] Test connecting to ILOGTST
- [ ] Test object browser loads
- [ ] Test all property dialogs
- [ ] Test query execution
- [ ] Test all menu items
- [ ] Test all keyboard shortcuts
- [ ] Test dark mode toggle
- [ ] Test CLI execution with ILOGTST profile
- [ ] Test build succeeds
- [ ] Test exe name is `DbExplorer.exe`

#### 4.21 Update Documentation
- [ ] Update README.md with new name
- [ ] Update all MarkdownDoc files with new references
- [ ] Update `.cursorrules` completely
- [ ] Update any inline comments with old name

---

**Status:** ⏳ NOT STARTED  
**Next Milestone:** Application fully renamed to DbExplorer  
**Blockers:** Phase 3 must be complete

---

**Last Updated:** 2025-11-20

