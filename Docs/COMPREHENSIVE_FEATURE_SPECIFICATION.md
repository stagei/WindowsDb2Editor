# Comprehensive Feature Specification

**Date**: December 14, 2025  
**Version**: 1.0  
**Status**: 📋 **SPECIFICATION** - Ready for Implementation  
**Framework**: .NET 10 WPF

---

## Table of Contents

1. [Visual Design Improvements](#visual-design-improvements)
2. [Object Browser Enhancements](#object-browser-enhancements)
3. [Package Relationship Bidirectional Support](#package-relationship-bidirectional-support)
4. [Database Comparison Feature](#database-comparison-feature)
5. [Advanced IntelliSense System](#advanced-intellisense-system)
6. [AI Integration](#ai-integration)

---

## 1. Visual Design Improvements

### 1.1 Object Browser Font Size Reduction

**Current State**: FontSize="13" in TreeView

**Specification**:
- **Change to**: FontSize="11" (compact, professional)
- **Rationale**: More objects visible, matches modern IDEs
- **Implementation**:
  ```xml
  <!-- ConnectionTabControl.xaml line 91 -->
  <Setter Property="FontSize" Value="11"/>
  ```

**Testing**:
- ✅ Verify readability in dark mode
- ✅ Verify readability in light mode
- ✅ Ensure icons (📋, 👁️, etc.) still align properly

### 1.2 Dark Mode & Light Mode Consistency

**Specification**:
- **Audit all dialogs** for hard-coded colors
- **Replace with**: DynamicResource bindings
- **Ensure**: All backgrounds, foregrounds, borders use theme-aware resources

**Critical Elements to Verify**:
```xml
<!-- Always use DynamicResource, never hard-coded colors -->
Background="{DynamicResource SystemControlBackgroundAltHighBrush}"
Foreground="{DynamicResource SystemControlForegroundBaseHighBrush}"
BorderBrush="{DynamicResource SystemControlForegroundBaseMediumLowBrush}"
```

**Files to Audit**:
- All Dialogs/*.xaml
- All Controls/*.xaml
- MainWindow.xaml

### 1.3 Loading Indicators

**Specification**:
- Add visual feedback during background metadata collection
- Show progress in status bar
- Display object count when complete

**Implementation**:
```csharp
// Update status bar during metadata collection
ObjectBrowserStatusText.Text = "⏳ Loading database objects...";

// After completion
ObjectBrowserStatusText.Text = $"✅ {totalObjects} objects loaded";
```

---

## 2. Object Browser Enhancements

### 2.1 Async Population Confirmation

**Current Behavior** (Verified):
- ✅ Connection opens synchronously with progress dialog
- ✅ Metadata collection starts **asynchronously in background**
- ✅ User can start working immediately
- ✅ Object browser populates as metadata becomes available

**Finding**: **ALREADY OPTIMAL** - No changes needed

### 2.2 Performance Improvements

**Specification**:
- Cache metadata in memory for fast access
- Lazy-load schema contents (expand on demand)
- Virtualize TreeView for large schemas (1000+ objects)

**Implementation**:
```xml
<TreeView VirtualizingPanel.IsVirtualizing="True"
          VirtualizingPanel.VirtualizationMode="Recycling">
```

---

## 3. Package Relationship Bidirectional Support

### 3.1 Current State

**What Exists**:
- ✅ TableDetailsDialog shows "Used By Packages" tab (packages that reference the table)
- ✅ Implemented via `GetReferencingPackagesAsync()`

**What's Missing**:
- ❌ PackageDetailsDialog does NOT show what tables/views/functions it uses
- ❌ No dependency graph from package perspective

### 3.2 Specification: Add "Dependencies" Tab to PackageDetailsDialog

**New Tab Structure**:
```
📦 Package Details: INL.MY_PACKAGE
├── ⚙️ Properties
├── 📝 SQL Statements
└── 🔗 Dependencies (NEW)
    ├── 📋 Tables Used (5)
    ├── 👁️ Views Used (2)
    ├── ⚙️ Procedures Called (3)
    └── 🔧 Functions Called (1)
```

**Implementation Approach**:

#### Option A: Parse SQL Text (Recommended)
```csharp
public class PackageDependencyAnalyzer
{
    public async Task<PackageDependencies> AnalyzeDependenciesAsync(
        DB2ConnectionManager connection,
        string schema,
        string packageName)
    {
        // 1. Fetch all SQL statements from SYSCAT.STATEMENTS
        var statements = await FetchPackageStatementsAsync(...);
        
        // 2. Parse SQL to extract object references
        var dependencies = new PackageDependencies();
        
        foreach (var stmt in statements)
        {
            // Extract table names from FROM, JOIN clauses
            var tables = ExtractTableReferences(stmt.Text);
            dependencies.TablesUsed.AddRange(tables);
            
            // Extract procedure calls from CALL statements
            var procedures = ExtractProcedureCalls(stmt.Text);
            dependencies.ProceduresCalled.AddRange(procedures);
            
            // Extract function calls
            var functions = ExtractFunctionCalls(stmt.Text);
            dependencies.FunctionsCalled.AddRange(functions);
        }
        
        // 3. Verify objects exist in SYSCAT
        dependencies.TablesUsed = await VerifyTableExists(dependencies.TablesUsed);
        
        return dependencies;
    }
    
    private List<string> ExtractTableReferences(string sql)
    {
        // Regex patterns:
        // FROM\s+([A-Z0-9_]+\.?[A-Z0-9_]+)
        // JOIN\s+([A-Z0-9_]+\.?[A-Z0-9_]+)
        // UPDATE\s+([A-Z0-9_]+\.?[A-Z0-9_]+)
        // INSERT\s+INTO\s+([A-Z0-9_]+\.?[A-Z0-9_]+)
    }
}
```

#### Option B: Use DB2 Dependency Views (If Available)
```sql
-- Check if DB2 has built-in package dependency views
SELECT * FROM SYSCAT.PACKAGEDEP WHERE PKGSCHEMA = ? AND PKGNAME = ?
```

**Recommended**: Start with **Option A** (SQL parsing) as it's more reliable and doesn't depend on DB2 version

### 3.3 UI Design for Dependencies Tab

**Layout**:
```xml
<TabItem Header="🔗 Dependencies">
    <TabControl>
        <TabItem Header="📋 Tables Used">
            <DataGrid ItemsSource="{Binding TablesUsed}">
                <DataGrid.Columns>
                    <DataGridTextColumn Header="Schema" Binding="{Binding Schema}"/>
                    <DataGridTextColumn Header="Table" Binding="{Binding Name}"/>
                    <DataGridTextColumn Header="Usage Count" Binding="{Binding UsageCount}"/>
                    <DataGridTextColumn Header="Statement Numbers" Binding="{Binding StatementsUsedIn}"/>
                </DataGrid.Columns>
            </DataGrid>
        </TabItem>
        
        <TabItem Header="👁️ Views Used">
            <!-- Similar to Tables -->
        </TabItem>
        
        <TabItem Header="⚙️ Procedures Called">
            <!-- Similar to Tables -->
        </TabItem>
        
        <TabItem Header="🔧 Functions Called">
            <!-- Similar to Tables -->
        </TabItem>
    </TabControl>
</TabItem>
```

**Features**:
- Double-click to open referenced object details
- Show usage count (how many times referenced)
- List statement numbers where object is used
- Context menu: "View Statement" → jumps to SQL Statements tab

---

## 4. Database Comparison Feature

### 4.1 Design Decisions (Based on Best Practices)

#### UI Layout: **Option A - Side-by-Side** (Recommended)
**Why**: Easiest to visually scan, familiar to developers (git diff style)

```
┌─ DEV (FKKTOTST) ──┬─ TEST (FKKTEST) ──┬─ PROD (FKKPROD) ──┐
│ Column1 INT       │ Column1 INT       │ Column1 INT       │
│ Column2 VARCHAR   │ Column2 VARCHAR   │ Column2 VARCHAR   │
│ + Column3 DATE    │                   │                   │ ← NEW in DEV
│ Column4 DECIMAL   │ Column4 DECIMAL   │ Column4 DECIMAL   │
└───────────────────┴───────────────────┴───────────────────┘
```

**Features**:
- Synchronized scrolling across all columns
- Color-coded differences
- Click line to select across all databases

#### Line Copy Behavior: **Option A - Immediate ALTER** (Recommended)
**Why**: Faster workflow, instant feedback, undo support

**Workflow**:
1. User selects line: "Column3 DATE" in DEV
2. Clicks "Copy to TEST" button
3. System generates: `ALTER TABLE INL.BILAGNR ADD COLUMN Column3 DATE;`
4. Shows AlterStatementReviewDialog with preview
5. User confirms, executes
6. Comparison view auto-refreshes

#### Source Code Application: **Hybrid Approach** (Recommended)

**For Views**:
```sql
CREATE OR REPLACE VIEW INL.MY_VIEW AS
  SELECT ... (from source database)
```

**For Procedures/Functions**:
```sql
-- DB2 supports CREATE OR REPLACE for routines in 11.5+
CREATE OR REPLACE PROCEDURE INL.MY_PROC (...)
BEGIN
  ... (from source database)
END;
```

**For Tables**: 
```sql
-- Never DROP+CREATE for tables (data loss!)
-- Always use ALTER statements
ALTER TABLE INL.BILAGNR ADD COLUMN NEW_COL VARCHAR(50);
ALTER TABLE INL.BILAGNR DROP COLUMN OLD_COL;
ALTER TABLE INL.BILAGNR ALTER COLUMN EXISTING_COL SET DATA TYPE VARCHAR(100);
```

#### Connection Management: **Option A - Reuse Existing** (Recommended)
**Why**: Better resource utilization, faster comparison

**Specification**:
- Use already-open connections from active tabs
- If target database not connected, prompt user to connect
- Cache connection references during comparison
- Don't close connections when comparison window closes

#### Comparison Scope: **Comprehensive** (Recommended)

**For Tables**:
- ✅ Column definitions (name, type, length, nullable, default)
- ✅ Primary keys
- ✅ Foreign keys
- ✅ Indexes (including index type, columns, uniqueness)
- ✅ Table comments/remarks
- ✅ Triggers on the table
- ✅ Row counts (optional - can be slow)
- ❌ Permissions/grants (skip for performance)

**For Views/Procedures/Functions**:
- ✅ Full source code (CREATE statement)
- ✅ Comments/remarks
- ✅ Dependencies (what they reference) - optional
- ❌ Permissions/grants (skip)

#### Diff Algorithm: **Structural (Smart Diff)** (Recommended)
**Why**: More accurate, detects renames and type changes

**Features**:
- Parse DDL into structured objects
- Compare by column name, not line number
- Detect column reordering (not a real difference)
- Detect type changes (INT → BIGINT)
- Detect renames (via comments or manual mapping)

#### Integration Point: **Option C - Both** (Recommended)

**View Menu**:
```
View
  ├─ Migration Assistant...
  ├─ Mermaid Visual Designer...
  ├─ Database Comparison...     ← NEW
  └─ Settings...
```

**Context Menu** (right-click on object):
```
Right-click on INL.BILAGNR:
  ├─ View Details
  ├─ Compare Across Databases...  ← NEW
  └─ Generate DDL
```

**Keyboard Shortcut**: `Ctrl+Shift+C` (Compare)

### 4.2 Complete Specification

#### Component 1: DatabaseComparisonDialog

**Purpose**: Initial dialog to configure comparison

**UI Elements**:
1. **Object Selection**
   - Type: ComboBox (Table, View, Procedure, Function, Trigger)
   - Name: TextBox with autocomplete + Browse button
   
2. **Environment Selection** (CheckedListBox)
   - List of available connections
   - Checkbox to include/exclude
   - Shows connection status (Connected/Not Connected)
   - "Add Connection" button
   
3. **Comparison Options**
   - Structure Only (fast)
   - Structure + Statistics (slower, includes row counts)
   - Source Code (for views/procedures/functions)
   
4. **Actions**
   - Compare button (primary action)
   - Save Template (save comparison config for reuse)

#### Component 2: DatabaseComparisonResultsDialog

**Purpose**: Show comparison results with VSCode-style diff view

**UI Layout**:
```
┌─────────────────────────────────────────────────────────────────┐
│ Database Comparison Results                                     │
├─────────────────────────────────────────────────────────────────┤
│ Object: INL.BILAGNR (Table)                                     │
│ Databases: DEV ↔ TEST ↔ PROD                                   │
│ Differences: 5                                                  │
├─────────────────────────────────────────────────────────────────┤
│ [Tabs: All | Columns | Indexes | Foreign Keys | Triggers]      │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│ ┌─ DEV ────────────┬─ TEST ───────────┬─ PROD ───────────┐    │
│ │ BILAGNR DECIMAL  │ BILAGNR DECIMAL  │ BILAGNR DECIMAL  │    │
│ │ BILAGDATO DATE   │ BILAGDATO DATE   │ BILAGDATO DATE   │    │
│ │ NEW_COL VARCHAR  │ ← MISSING        │ ← MISSING        │🟢  │
│ │ STATUS CHAR(1)   │ STATUS CHAR(2)   │ STATUS CHAR(1)   │🟡  │
│ │                  │ EXTRA_COL INT    │ ← MISSING        │🔴  │
│ └──────────────────┴──────────────────┴──────────────────┘    │
│                                                                 │
│ Difference Summary:                                             │
│ ⚠️ Column NEW_COL exists in DEV only                           │
│ ⚠️ Column STATUS has different length (1 in DEV/PROD, 2 in TEST)│
│ ⚠️ Column EXTRA_COL exists in TEST only                        │
├─────────────────────────────────────────────────────────────────┤
│ Actions:                                                        │
│ [Copy Selected Line from DEV → TEST]  [Copy DEV → PROD]       │
│ [Generate ALTER Script for TEST...]   [Generate ALTER for PROD]│
│ [Apply All from DEV to TEST]          [Export Comparison...]   │
│                                                    [Close]      │
└─────────────────────────────────────────────────────────────────┘
```

**Features**:
- **Color Coding**:
  - 🟢 Green background: Line exists only in this database
  - 🔴 Red background: Line missing from this database
  - 🟡 Yellow background: Line differs across databases
  - ⚪ Normal: Line identical in all databases

- **Interactive Selection**:
  - Click a line in any column to select across all columns
  - Highlight selected line in all databases
  - Show quick actions for selected line

- **Synchronized Scrolling**:
  - Scroll one column, all columns scroll together
  - Maintain line alignment across all databases

- **Smart Line Matching**:
  - Match by column name, not line number
  - Handle reordering gracefully
  - Show "moved" indicators for reordered items

#### Component 3: DatabaseComparisonService

**Purpose**: Core comparison logic

**Methods**:
```csharp
public class DatabaseComparisonService
{
    // Compare table structures across multiple databases
    public async Task<TableComparisonResult> CompareTablesAsync(
        Dictionary<string, DB2ConnectionManager> connections,
        string schema,
        string tableName);
    
    // Compare view source code
    public async Task<SourceComparisonResult> CompareViewsAsync(...);
    
    // Compare procedure source code
    public async Task<SourceComparisonResult> CompareProceduresAsync(...);
    
    // Compare function source code
    public async Task<SourceComparisonResult> CompareFunctionsAsync(...);
    
    // Generate ALTER statements to sync target to source
    public string GenerateSyncAlterStatements(
        TableDefinition source,
        TableDefinition target);
}
```

**Comparison Algorithm**:
1. Fetch object definition from each database
2. Parse into structured format (columns, indexes, etc.)
3. Compare field-by-field (not line-by-line)
4. Identify differences: Added, Removed, Modified
5. Generate difference report
6. Generate ALTER statements for synchronization

#### Component 4: MultiDatabaseConnectionManager

**Purpose**: Manage multiple connections simultaneously

**Features**:
- Dictionary of connections by alias (DEV, TEST, PROD, etc.)
- Connection pooling and reuse
- Health check before comparison
- Graceful handling of connection failures
- Transaction support for ALTER execution

```csharp
public class MultiDatabaseConnectionManager : IDisposable
{
    private readonly Dictionary<string, DB2ConnectionManager> _connections = new();
    
    public async Task<bool> AddConnectionAsync(
        string alias, 
        ConnectionInfo connectionInfo);
    
    public DB2ConnectionManager? GetConnection(string alias);
    
    public async Task<bool> TestAllConnectionsAsync();
    
    public void Dispose();
}
```

---

## 5. Advanced IntelliSense System

### 5.1 Current JSON Metadata Files

**Available Files** (in ConfigFiles/):
1. `db2_12.1_keywords.json` - SQL keywords
2. `db2_12.1_sql_statements.json` - SQL statement templates
3. `db2_12.1_system_metadata.json` - System catalog info
4. `db2_12.1_en-US_texts.json` - Help text
5. `supported_providers.json` - Database provider info

### 5.2 IntelliSense Feature Specification

#### Feature 1: Context-Aware Keyword Completion

**Implementation**: Enhance AvalonEdit with `ICompletionData`

**Specification**:
```csharp
public class Db2IntelliSenseService
{
    private Dictionary<string, List<string>> _keywords;
    private Dictionary<string, SqlStatementTemplate> _statementTemplates;
    private Dictionary<string, List<string>> _systemTables;
    
    public void LoadMetadataForProvider(string provider, string version)
    {
        // Load JSON files for the specific provider
        _keywords = LoadKeywords($"ConfigFiles/{provider}_{version}_keywords.json");
        _statementTemplates = LoadStatements($"ConfigFiles/{provider}_{version}_sql_statements.json");
        _systemTables = LoadSystemMetadata($"ConfigFiles/{provider}_{version}_system_metadata.json");
    }
    
    public List<ICompletionData> GetCompletions(string currentText, int caretPosition)
    {
        var context = DetermineContext(currentText, caretPosition);
        
        return context switch
        {
            SqlContext.Keyword => GetKeywordCompletions(),
            SqlContext.TableName => GetTableNameCompletions(),
            SqlContext.ColumnName => GetColumnNameCompletions(),
            SqlContext.Function => GetFunctionCompletions(),
            SqlContext.SystemCatalog => GetSystemCatalogCompletions(),
            _ => GetGeneralCompletions()
        };
    }
}
```

#### Feature 2: Smart Completion Based on Context

**Contexts**:

**1. After SELECT** → Suggest columns from previously mentioned tables
```sql
SELECT |  ← Suggest: *, column names from recent tables
```

**2. After FROM** → Suggest schema.table names
```sql
FROM |  ← Suggest: schema names, table names
FROM INL.|  ← Suggest: tables in INL schema
```

**3. After WHERE** → Suggest column names
```sql
WHERE |  ← Suggest: columns from tables in FROM clause
```

**4. After JOIN** → Suggest table names and join conditions
```sql
JOIN |  ← Suggest: tables, common FK relationships
```

**5. Function/Procedure Names** → Suggest with parameter info
```sql
CALL |  ← Suggest: procedures with parameter hints
```

#### Feature 3: Live Schema Metadata Integration

**Specification**:
- When connection is established, cache schema metadata
- Load tables, views, procedures, functions from SYSCAT
- Provide real-time completion for:
  - Table names (with row count hints)
  - Column names (with data types)
  - View names
  - Procedure/function names with signatures

**Implementation**:
```csharp
public class LiveSchemaIntelliSenseProvider
{
    private DB2ConnectionManager _connection;
    private Dictionary<string, List<ColumnInfo>> _tableColumns;
    
    public async Task LoadSchemaMetadataAsync()
    {
        // Query SYSCAT.COLUMNS, SYSCAT.TABLES, etc.
        // Build in-memory index for fast lookup
    }
    
    public List<ICompletionData> GetTableCompletions(string prefix)
    {
        return _tableColumns.Keys
            .Where(t => t.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            .Select(t => new TableCompletionData(t, GetRowCount(t)))
            .ToList();
    }
    
    public List<ICompletionData> GetColumnCompletions(string tableName)
    {
        if (_tableColumns.TryGetValue(tableName, out var columns))
        {
            return columns.Select(c => new ColumnCompletionData(c)).ToList();
        }
        return new List<ICompletionData>();
    }
}
```

#### Feature 4: Snippet Support

**Specification**: Add SQL snippets from JSON templates

**Example Snippets**:
```json
{
  "snippets": [
    {
      "trigger": "seltop",
      "name": "SELECT TOP N",
      "template": "SELECT * FROM ${1:table} FETCH FIRST ${2:100} ROWS ONLY",
      "description": "Select top N rows from table"
    },
    {
      "trigger": "cte",
      "name": "Common Table Expression",
      "template": "WITH ${1:cte_name} AS (\n\tSELECT ${2:*}\n\tFROM ${3:table}\n)\nSELECT * FROM ${1:cte_name}",
      "description": "Create a CTE query"
    }
  ]
}
```

**Usage**:
- User types "seltop" + TAB → Expands to template
- Placeholder navigation with TAB
- Pre-filled with smart defaults

#### Feature 5: Function Signature Hints

**Specification**: Show function/procedure signatures as user types

**Example**:
```sql
CALL MYPROC(|)
         ↓
    Shows: MYPROC(IN param1 INT, IN param2 VARCHAR, OUT result VARCHAR)
```

**Implementation**:
```csharp
public class SignatureHintProvider
{
    public async Task<FunctionSignature> GetSignatureAsync(
        string functionName)
    {
        // Query SYSCAT.ROUTINEPARMS for parameters
        // Build signature string
        // Show tooltip near cursor
    }
}
```

### 5.3 AvalonEdit Integration

**Implementation Steps**:

1. **Create Completion Data Classes**
```csharp
public class Db2KeywordCompletionData : ICompletionData
{
    public string Text { get; set; }
    public string Description { get; set; }
    public ImageSource? Image { get; set; }
    public double Priority { get; set; }
    
    public void Complete(TextArea textArea, ISegment completionSegment, EventArgs e)
    {
        textArea.Document.Replace(completionSegment, Text);
    }
}
```

2. **Hook into AvalonEdit Events**
```csharp
// In ConnectionTabControl.xaml.cs
SqlEditor.TextArea.TextEntering += OnTextEntering;
SqlEditor.TextArea.TextEntered += OnTextEntered;

private void OnTextEntered(object sender, TextCompositionEventArgs e)
{
    if (e.Text == "." || e.Text == " " || _completionWindow == null)
    {
        ShowCompletionWindow();
    }
}

private void ShowCompletionWindow()
{
    _completionWindow = new CompletionWindow(SqlEditor.TextArea);
    var completions = _intelliSenseService.GetCompletions(
        SqlEditor.Text, 
        SqlEditor.CaretOffset);
    
    foreach (var completion in completions)
    {
        _completionWindow.CompletionList.CompletionData.Add(completion);
    }
    
    _completionWindow.Show();
}
```

3. **Provider-Specific Metadata Loading**
```csharp
// When connection is established
var provider = _connectionManager.ConnectionInfo.Provider; // "DB2"
var version = await _connectionManager.GetDatabaseVersionAsync(); // "12.1"

_intelliSenseService.LoadMetadataForProvider(provider, version);
await _intelliSenseService.LoadLiveSchemaMetadataAsync(_connectionManager);
```

---

## 6. AI Integration

### 6.1 AI Integration Architecture

**Goal**: Embed AI directly into WindowsDb2Editor for intelligent assistance

**Approach**: Hybrid (Local + Cloud)

**Options**:

#### Option A: Local AI (Recommended for Offline Deployment)
**Model**: Llama 3.2 3B (GGUF format) via LLM.NET or LLamaSharp
**Pros**: Offline, fast, no API costs
**Cons**: Limited model size, requires GPU for best performance

#### Option B: Cloud AI (Azure OpenAI, OpenAI)
**Model**: GPT-4o or GPT-4o-mini
**Pros**: Very powerful, latest features
**Cons**: Requires internet, API costs, latency

#### Option C: Hybrid (Recommended)
**Default**: Local AI for basic tasks
**Fallback**: Cloud AI for complex tasks (user configurable)
**Best of Both**: Speed + offline + power when needed

### 6.2 AI Use Cases

#### Use Case 1: Natural Language to SQL

**Feature**: "Ask AI" button in query editor

**User Types**:
```
"Show me all customers who made purchases in the last 30 days"
```

**AI Generates**:
```sql
SELECT DISTINCT c.*
FROM INL.CUSTOMERS c
JOIN INL.PURCHASES p ON c.CUSTOMER_ID = p.CUSTOMER_ID
WHERE p.PURCHASE_DATE >= CURRENT_DATE - 30 DAYS
ORDER BY c.CUSTOMER_NAME;
```

**Implementation**:
```csharp
public class AiSqlGenerationService
{
    private readonly ILargeLanguageModel _model;
    private readonly DB2ConnectionManager _connection;
    
    public async Task<string> GenerateSqlFromNaturalLanguageAsync(
        string naturalLanguageQuery)
    {
        // Build prompt with schema context
        var schemaContext = await GetSchemaContextAsync();
        
        var prompt = $@"
You are a DB2 SQL expert. Generate a SQL query for this request:

Request: {naturalLanguageQuery}

Available schema:
{schemaContext}

Rules:
- Use DB2 12.1 syntax
- Use proper schema prefixes (INL.TABLE_NAME)
- Optimize for performance
- Include comments

SQL Query:
";
        
        var sql = await _model.GenerateAsync(prompt);
        return CleanAndValidateSql(sql);
    }
    
    private async Task<string> GetSchemaContextAsync()
    {
        // Get list of tables with column names
        // Format as context for AI
        // Limit to 50 most common tables
    }
}
```

#### Use Case 2: SQL Optimization Suggestions

**Feature**: "Optimize This Query" button

**User Has Query**:
```sql
SELECT * FROM INL.BILAGNR WHERE BILAGDATO = '2025-01-01';
```

**AI Suggests**:
```
⚡ Optimization Suggestions:

1. Avoid SELECT * - specify needed columns
   Recommended: SELECT BILAGNR, BILAGDATO, STATUS FROM ...
   
2. Use indexed columns in WHERE clause
   Current: BILAGDATO (not indexed)
   Consider: Add index on BILAGDATO or use indexed column
   
3. Consider date range instead of exact match
   More flexible: WHERE BILAGDATO >= '2025-01-01' AND BILAGDATO < '2025-01-02'

Optimized Query:
SELECT BILAGNR, BILAGDATO, STATUS
FROM INL.BILAGNR
WHERE BILAGDATO >= '2025-01-01' AND BILAGDATO < '2025-01-02'
WITH UR; -- Uncommitted read for better performance if acceptable
```

#### Use Case 3: Explain Query

**Feature**: "Explain This Query" button

**AI Provides**:
- Natural language explanation of what query does
- Identifies potential issues
- Suggests improvements
- Explains JOIN logic and filtering

#### Use Case 4: Generate Test Data

**Feature**: "Generate Test Data" for table

**User Selects**: INL.CUSTOMERS table

**AI Generates**:
```sql
-- Realistic test data for INL.CUSTOMERS
INSERT INTO INL.CUSTOMERS (CUSTOMER_ID, NAME, EMAIL, COUNTRY) VALUES
(1, 'Ola Nordmann', 'ola.nordmann@example.no', 'Norway'),
(2, 'Kari Hansen', 'kari.hansen@example.no', 'Norway'),
(3, 'Per Olsen', 'per.olsen@example.no', 'Norway');
```

#### Use Case 5: Error Explanation

**Feature**: When query fails, AI explains error

**User Gets Error**:
```
ERROR [42703] SQL0206N "BILAGDAT" is not valid in the context where it is used.
```

**AI Explains**:
```
❌ Error Explanation:

The error indicates that column "BILAGDAT" doesn't exist in the table.

Likely Cause:
- Typo in column name
- Column name is actually "BILAGDATO" (with 'O' at end)

Suggestion:
Change: WHERE BILAGDAT = ...
To:     WHERE BILAGDATO = ...

Similar Columns in INL.BILAGNR:
- BILAGDATO (DATE) ← Did you mean this?
- BILAGNR (DECIMAL)
- STATUS (CHAR)
```

#### Use Case 6: Code Review / Best Practices

**Feature**: "Review My Code" button

**AI Checks**:
- SQL injection vulnerabilities
- Performance anti-patterns
- DB2 best practices
- Coding standards
- Security issues

**Example Output**:
```
📋 Code Review Results:

✅ Good Practices Found:
- Uses parameterized queries
- Proper transaction handling
- Appropriate indexes used

⚠️ Suggestions:
1. Consider adding WITH UR for read-only queries (better performance)
2. Use FETCH FIRST instead of TOP (DB2 standard)
3. Add error handling for constraint violations

❌ Issues Found:
1. DELETE without WHERE clause (line 15) - DANGEROUS!
   Recommendation: Add WHERE clause or use TRUNCATE
```

#### Use Case 7: Generate Documentation

**Feature**: Auto-generate documentation for database objects

**User Selects**: Table or procedure

**AI Generates**:
```markdown
# Table: INL.BILAGNR

## Purpose
Stores invoice numbers and associated billing dates for the invoice 
processing system.

## Columns
- **BILAGNR** (DECIMAL): Unique invoice number, primary key
- **BILAGDATO** (DATE): Invoice date
- **STATUS** (CHAR(1)): Invoice status (P=Pending, A=Approved, R=Rejected)

## Relationships
- Referenced by: INL.FASTE_TRANS (FK: BILAGNR)
- Used in packages: INL.INVOICE_PKG

## Usage Patterns
Common queries:
- Find invoices by date range
- Check invoice status
- Generate monthly reports

## Performance Notes
- Index on BILAGDATO for date range queries
- Partitioned by year for better performance
```

### 6.3 AI Integration Implementation

#### Component 1: AiIntegrationService

```csharp
public class AiIntegrationService
{
    private readonly IAiProvider _provider; // Local or Cloud
    
    // Configuration
    public void Configure(AiProviderType type, AiSettings settings);
    
    // Core AI features
    public async Task<string> GenerateSqlFromNaturalLanguageAsync(string query);
    public async Task<OptimizationSuggestions> OptimizeSqlAsync(string sql);
    public async Task<string> ExplainQueryAsync(string sql);
    public async Task<string> GenerateTestDataAsync(string tableName, int rowCount);
    public async Task<ErrorExplanation> ExplainErrorAsync(string errorMessage, string sql);
    public async Task<CodeReview> ReviewCodeAsync(string sql);
    public async Task<string> GenerateDocumentationAsync(string objectName, string objectType);
}
```

#### Component 2: AI Provider Abstraction

```csharp
public interface IAiProvider
{
    Task<string> GenerateAsync(string prompt, AiGenerationOptions options);
    Task<bool> IsAvailableAsync();
    Task<ModelInfo> GetModelInfoAsync();
}

public class LocalAiProvider : IAiProvider
{
    // Uses LLamaSharp with local GGUF model
    // Model: Llama-3.2-3B-Instruct.Q4_K_M.gguf (~2GB)
}

public class AzureOpenAiProvider : IAiProvider
{
    // Uses Azure OpenAI REST API
    // Model: GPT-4o-mini or GPT-4o
}

public class OpenAiProvider : IAiProvider
{
    // Uses OpenAI REST API
    // Model: gpt-4o, gpt-4o-mini, gpt-3.5-turbo
}
```

#### Component 3: AI Assistant Panel

**UI Location**: New tab in ConnectionTabControl (alongside Objects and History)

```xml
<TabItem Header="🤖 AI Assistant">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>
        
        <!-- Provider Status -->
        <Border Grid.Row="0" Background="DarkBlue" Padding="10">
            <StackPanel Orientation="Horizontal">
                <TextBlock Text="🤖 AI Provider: " Foreground="White"/>
                <TextBlock Name="AiProviderText" Text="Local (Llama 3.2 3B)" 
                           FontWeight="Bold" Foreground="LightGreen"/>
                <TextBlock Text=" • " Foreground="White" Margin="10,0"/>
                <TextBlock Name="AiStatusText" Text="Ready" Foreground="LightGreen"/>
            </StackPanel>
        </Border>
        
        <!-- Conversation History -->
        <ScrollViewer Grid.Row="1" VerticalScrollBarVisibility="Auto">
            <ItemsControl Name="ConversationHistory">
                <ItemsControl.ItemTemplate>
                    <DataTemplate>
                        <Border Padding="10" Margin="5"
                                Background="{Binding IsUser, Converter={StaticResource UserColorConverter}}">
                            <StackPanel>
                                <TextBlock Text="{Binding Sender}" FontWeight="Bold"/>
                                <TextBlock Text="{Binding Message}" TextWrapping="Wrap" Margin="0,5,0,0"/>
                                <TextBlock Text="{Binding Timestamp}" FontSize="10" 
                                           Foreground="Gray" Margin="0,5,0,0"/>
                            </StackPanel>
                        </Border>
                    </DataTemplate>
                </ItemsControl.ItemTemplate>
            </ItemsControl>
        </ScrollViewer>
        
        <!-- Quick Actions -->
        <WrapPanel Grid.Row="2" Margin="5">
            <Button Content="🎯 Generate SQL" Click="AiGenerateSql_Click"/>
            <Button Content="⚡ Optimize Query" Click="AiOptimize_Click"/>
            <Button Content="💡 Explain Query" Click="AiExplain_Click"/>
            <Button Content="🔍 Review Code" Click="AiReview_Click"/>
            <Button Content="📚 Generate Docs" Click="AiDocs_Click"/>
            <Button Content="🧪 Create Test Data" Click="AiTestData_Click"/>
        </WrapPanel>
        
        <!-- Input Box -->
        <Grid Grid.Row="3" Margin="5">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="*"/>
                <ColumnDefinition Width="Auto"/>
            </Grid.ColumnDefinitions>
            
            <TextBox Grid.Column="0" Name="AiInputBox"
                     PlaceholderText="Ask AI anything about your database or SQL..."
                     AcceptsReturn="True"
                     MaxHeight="100"
                     KeyDown="AiInput_KeyDown"/>
            
            <Button Grid.Column="1" Content="Send" Width="80" Margin="5,0,0,0"
                    Click="AiSend_Click"/>
        </Grid>
    </Grid>
</TabItem>
```

### 6.4 AI Context Building

**Specification**: Build rich context for AI to understand the database

**Context Elements**:
```csharp
public class AiContextBuilder
{
    public async Task<string> BuildContextAsync(
        DB2ConnectionManager connection,
        string currentQuery)
    {
        var context = new StringBuilder();
        
        // 1. Database information
        context.AppendLine($"Database: {connection.DatabaseName}");
        context.AppendLine($"Provider: DB2 12.1");
        
        // 2. Active schema
        var activeSchema = GetActiveSchema(currentQuery);
        context.AppendLine($"Active Schema: {activeSchema}");
        
        // 3. Relevant tables (mentioned in current query)
        var tables = ExtractTableNames(currentQuery);
        foreach (var table in tables)
        {
            var columns = await GetTableColumnsAsync(connection, table);
            context.AppendLine($"\nTable: {table}");
            foreach (var col in columns)
            {
                context.AppendLine($"  - {col.Name} {col.DataType}");
            }
        }
        
        // 4. Recent queries (for context about user's goals)
        var recentQueries = GetRecentQueries(5);
        context.AppendLine("\nRecent Queries:");
        foreach (var q in recentQueries)
        {
            context.AppendLine($"  - {q.Substring(0, Math.Min(100, q.Length))}");
        }
        
        return context.ToString();
    }
}
```

### 6.5 AI-Powered Features Specification

#### Feature 1: Query Autocomplete (AI-Powered)

**Trigger**: User pauses typing for 2 seconds

**AI Action**: Suggest next part of query

**Example**:
```sql
User types: SELECT * FROM INL.BILAGNR WHERE |
AI suggests: BILAGDATO >= CURRENT_DATE - 30 DAYS
```

**Implementation**:
- Use local model for speed
- Cache suggestions for common patterns
- Learn from user's query history

#### Feature 2: Error Auto-Fix

**Trigger**: Query fails with SQL error

**AI Action**: Suggest fix automatically

**Example**:
```
Error: Column "BILAGDAT" not found
AI Fix: Did you mean "BILAGDATO"? [Apply Fix]
```

**Implementation**:
```csharp
public class AiErrorFixService
{
    public async Task<string> SuggestFixAsync(
        string failedSql,
        string errorMessage,
        DB2ConnectionManager connection)
    {
        // Build prompt with error context
        var tableColumns = await GetRelevantColumnsAsync(failedSql);
        
        var prompt = $@"
SQL Error: {errorMessage}
Failed Query: {failedSql}
Available Columns: {string.Join(", ", tableColumns)}

Suggest a fix for this SQL error.
";
        
        var suggestion = await _ai.GenerateAsync(prompt);
        return suggestion;
    }
}
```

#### Feature 3: Schema Design Assistant

**Feature**: AI suggests schema improvements

**User Selects**: Table INL.BILAGNR

**AI Analyzes**:
```
📊 Schema Analysis for INL.BILAGNR

✅ Good Design:
- Has primary key (BILAGNR)
- Appropriate data types
- Foreign keys properly defined

💡 Suggestions:
1. Add index on BILAGDATO
   Reason: Frequently used in WHERE clauses
   Impact: 40% query performance improvement
   
2. Consider adding STATUS index
   Reason: Used in 60% of queries
   Type: CREATE INDEX IX_BILAGNR_STATUS ON INL.BILAGNR(STATUS)
   
3. Add CHECK constraint on STATUS
   Suggestion: CHECK (STATUS IN ('P', 'A', 'R'))
   Reason: Ensure data integrity

4. Consider partitioning by year
   Reason: Table has 10M+ rows, date-based queries
   Benefit: 60% performance improvement on range queries
```

#### Feature 4: Migration Script Generator

**Feature**: AI generates complete migration scripts

**User Input**:
```
"I need to add email and phone columns to the CUSTOMERS table, 
 make email unique, and migrate existing data format"
```

**AI Generates**:
```sql
-- Step 1: Add new columns
ALTER TABLE INL.CUSTOMERS ADD COLUMN EMAIL VARCHAR(255);
ALTER TABLE INL.CUSTOMERS ADD COLUMN PHONE VARCHAR(20);

-- Step 2: Migrate existing data (if needed)
UPDATE INL.CUSTOMERS 
SET EMAIL = LOWER(TRIM(OLD_EMAIL_FIELD))
WHERE OLD_EMAIL_FIELD IS NOT NULL;

-- Step 3: Add unique constraint
CREATE UNIQUE INDEX IX_CUSTOMERS_EMAIL ON INL.CUSTOMERS(EMAIL);

-- Step 4: Add validation
ALTER TABLE INL.CUSTOMERS 
  ADD CONSTRAINT CHK_EMAIL 
  CHECK (EMAIL LIKE '%@%.%');

-- Step 5: Verify migration
SELECT COUNT(*) AS TotalRows,
       COUNT(EMAIL) AS EmailsPopulated,
       COUNT(DISTINCT EMAIL) AS UniqueEmails
FROM INL.CUSTOMERS;
```

#### Feature 5: Smart Autocomplete with Context

**Feature**: AI predicts what user wants to type next

**Scenario**:
```sql
-- User has been querying invoices and payments
SELECT * FROM INL.BILAGNR WHERE BILAGDATO = '2025-01-01';
SELECT * FROM INL.PAYMENTS WHERE PAYMENT_DATE = '2025-01-01';

-- Now user types:
SELECT |

-- AI suggests (based on context):
-- "You're probably querying related financial data..."
Suggestion 1: SELECT * FROM INL.FASTE_TRANS WHERE TRANS_DATE = '2025-01-01'
Suggestion 2: SELECT B.*, P.* FROM INL.BILAGNR B JOIN INL.PAYMENTS P ON B.BILAGNR = P.INVOICE_NR
Suggestion 3: SELECT SUM(AMOUNT) FROM INL.PAYMENTS WHERE PAYMENT_DATE = '2025-01-01'
```

#### Feature 6: Database Chat Assistant

**Feature**: Conversational interface for database exploration

**User**: "What tables are related to customers?"

**AI**: 
```
I found 3 tables related to customers in the INL schema:

1. INL.CUSTOMERS (primary table)
   - 15,234 rows
   - Last updated: 2025-12-10

2. INL.CUSTOMER_ORDERS
   - Links to CUSTOMERS via CUSTOMER_ID
   - 45,678 rows

3. INL.CUSTOMER_PREFERENCES
   - Links to CUSTOMERS via CUSTOMER_ID
   - 12,567 rows

Would you like me to show the schema for any of these tables?
```

**User**: "Show me how to find all customers with no orders"

**AI**:
```sql
-- Customers with no orders
SELECT c.*
FROM INL.CUSTOMERS c
LEFT JOIN INL.CUSTOMER_ORDERS o ON c.CUSTOMER_ID = o.CUSTOMER_ID
WHERE o.ORDER_ID IS NULL;

This query uses a LEFT JOIN to include all customers, then filters 
for those with NULL order IDs (meaning no matching orders exist).
```

### 6.6 AI Settings and Configuration

**Dialog**: Settings → AI Configuration Tab

**Settings**:
```xml
<TabItem Header="🤖 AI Configuration">
    <StackPanel Margin="20">
        <TextBlock Text="AI Provider" FontSize="16" FontWeight="Bold" Margin="0,0,0,10"/>
        
        <RadioButton Content="Local AI (Llama 3.2 3B - Offline)" 
                     IsChecked="True" Margin="0,5"/>
        <TextBlock Text="Best for: Fast responses, offline use, no costs"
                   FontSize="11" Foreground="Gray" Margin="20,0,0,5"/>
        
        <RadioButton Content="Azure OpenAI (GPT-4o - Cloud)" 
                     Margin="0,5"/>
        <TextBlock Text="Best for: Complex queries, high accuracy, advanced features"
                   FontSize="11" Foreground="Gray" Margin="20,0,0,5"/>
        
        <RadioButton Content="OpenAI (gpt-4o-mini - Cloud)" 
                     Margin="0,5"/>
        <TextBlock Text="Best for: Balance of speed and accuracy, lower cost"
                   FontSize="11" Foreground="Gray" Margin="20,0,0,15"/>
        
        <Separator Margin="0,10"/>
        
        <TextBlock Text="API Settings (Cloud Providers)" FontSize="14" 
                   FontWeight="Bold" Margin="0,10,0,10"/>
        
        <Grid>
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="150"/>
                <ColumnDefinition Width="*"/>
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                <RowDefinition Height="Auto"/>
                <RowDefinition Height="Auto"/>
                <RowDefinition Height="Auto"/>
            </Grid.RowDefinitions>
            
            <TextBlock Grid.Row="0" Grid.Column="0" Text="API Key:" Margin="0,5"/>
            <PasswordBox Grid.Row="0" Grid.Column="1" Name="ApiKeyBox" Margin="5"/>
            
            <TextBlock Grid.Row="1" Grid.Column="0" Text="Endpoint:" Margin="0,5"/>
            <TextBox Grid.Row="1" Grid.Column="1" Name="EndpointBox" Margin="5"/>
            
            <TextBlock Grid.Row="2" Grid.Column="0" Text="Model:" Margin="0,5"/>
            <ComboBox Grid.Row="2" Grid.Column="1" Name="ModelBox" Margin="5">
                <ComboBoxItem Content="gpt-4o"/>
                <ComboBoxItem Content="gpt-4o-mini"/>
                <ComboBoxItem Content="gpt-3.5-turbo"/>
            </ComboBox>
        </Grid>
        
        <Separator Margin="0,10"/>
        
        <TextBlock Text="Features" FontSize="14" FontWeight="Bold" Margin="0,10,0,10"/>
        
        <CheckBox Content="Enable AI query suggestions" IsChecked="True" Margin="0,5"/>
        <CheckBox Content="Enable AI error explanations" IsChecked="True" Margin="0,5"/>
        <CheckBox Content="Enable AI autocomplete" Margin="0,5"/>
        <CheckBox Content="Enable AI code review" Margin="0,5"/>
        <CheckBox Content="Send query history for context (privacy concern)" Margin="0,5"/>
        
        <Separator Margin="0,10"/>
        
        <Button Content="Test Connection" Width="150" HorizontalAlignment="Left" 
                Margin="0,10" Click="TestAi_Click"/>
    </StackPanel>
</TabItem>
```

### 6.7 Privacy and Security

**Specification**:

**Local AI**:
- ✅ No data leaves the machine
- ✅ No API keys needed
- ✅ Fully offline capable
- ✅ GDPR/compliance friendly

**Cloud AI**:
- ⚠️ User must explicitly enable
- ⚠️ Show privacy warning on first use
- ⚠️ Allow user to control what data is sent
- ⚠️ Never send: Passwords, connection strings, sensitive data
- ✅ Only send: SQL query text, error messages (user-approved)

**Settings**:
```csharp
public class AiPrivacySettings
{
    public bool AllowSendingQueryHistory { get; set; } = false;
    public bool AllowSendingErrorMessages { get; set; } = true;
    public bool AllowSendingSchemaMetadata { get; set; } = true;
    public bool MaskSensitiveData { get; set; } = true;
    public List<string> SensitiveKeywords { get; set; } = new() 
    { 
        "PASSWORD", "SSN", "CREDIT_CARD", "EMAIL" 
    };
}
```

---

## 7. Implementation Priority and Timeline

### Phase 1: Visual Improvements (IMMEDIATE)
**Time**: 2 hours  
**Priority**: HIGH (quick wins)

- [x] Reduce object browser font to 11pt
- [x] Audit theme consistency
- [x] Add loading indicators
- [x] Test dark/light modes

### Phase 2: IntelliSense (HIGH PRIORITY)
**Time**: 8-12 hours  
**Priority**: HIGH (high-value feature)

- [x] Create Db2IntelliSenseService
- [x] Implement keyword completion
- [x] Implement context-aware suggestions
- [x] Integrate live schema metadata
- [x] Add snippet support
- [x] Test with AvalonEdit

### Phase 3: Package Dependencies (MEDIUM PRIORITY)
**Time**: 4-6 hours  
**Priority**: MEDIUM

- [x] Create PackageDependencyAnalyzer
- [x] Parse SQL to extract dependencies
- [x] Add Dependencies tab to PackageDetailsDialog
- [x] Test with real packages

### Phase 4: Database Comparison (HIGH PRIORITY)
**Time**: 16-20 hours  
**Priority**: HIGH (major feature)

- [x] Create MultiDatabaseConnectionManager
- [x] Create DatabaseComparisonService
- [x] Implement comparison algorithms
- [x] Create DatabaseComparisonDialog
- [x] Create DatabaseComparisonResultsDialog
- [x] Implement side-by-side diff view
- [x] Add interactive actions
- [x] Test with real databases

### Phase 5: AI Integration (FUTURE/OPTIONAL)
**Time**: 20-30 hours  
**Priority**: MEDIUM (high value but complex)

- [x] Research local AI models (LLamaSharp)
- [x] Create AiIntegrationService
- [x] Implement provider abstraction
- [x] Add AI Assistant panel
- [x] Implement natural language to SQL
- [x] Implement error explanation
- [x] Add settings and configuration

**Total Estimated Time**: 50-70 hours

---

## 8. Detailed Technical Specifications

### 8.1 IntelliSense Architecture

```
┌─────────────────────────────────────────────────────┐
│ AvalonEdit TextEditor (SqlEditor)                   │
│                                                     │
│  User types: SELECT * FROM I|                      │
│                             ↓                       │
├─────────────────────────────────────────────────────┤
│ IntelliSenseController                              │
│  - Detects trigger (., space, etc.)                │
│  - Gets caret position and context                 │
│  - Requests completions from providers             │
├─────────────────────────────────────────────────────┤
│ Completion Providers:                               │
│                                                     │
│ ┌─ KeywordProvider ─────────────────────┐         │
│ │ Source: db2_12.1_keywords.json        │         │
│ │ Returns: SELECT, INSERT, CREATE, etc. │         │
│ └───────────────────────────────────────┘         │
│                                                     │
│ ┌─ LiveSchemaProvider ──────────────────┐         │
│ │ Source: SYSCAT.TABLES, SYSCAT.COLUMNS │         │
│ │ Returns: Table/column names           │         │
│ └───────────────────────────────────────┘         │
│                                                     │
│ ┌─ StatementTemplateProvider ───────────┐         │
│ │ Source: db2_12.1_sql_statements.json  │         │
│ │ Returns: Full query templates         │         │
│ └───────────────────────────────────────┘         │
│                                                     │
│ ┌─ SnippetProvider ─────────────────────┐         │
│ │ Source: Custom snippets.json          │         │
│ │ Returns: User-defined snippets        │         │
│ └───────────────────────────────────────┘         │
│                                                     │
│ ┌─ AiCompletionProvider (Optional) ─────┐         │
│ │ Source: Local/Cloud AI model          │         │
│ │ Returns: Smart predictions            │         │
│ └───────────────────────────────────────┘         │
├─────────────────────────────────────────────────────┤
│ CompletionWindow                                    │
│  ┌──────────────────────────────────┐              │
│  │ 📋 INL (Schema)                  │              │
│  │ 📊 INDEXES (System Table)        │              │
│  │ 🔍 INSERT INTO (Keyword)         │              │
│  │ 📝 SELECT TOP Template           │              │
│  └──────────────────────────────────┘              │
└─────────────────────────────────────────────────────┘
```

### 8.2 Database Comparison Data Flow

```
User Initiates Comparison
         ↓
DatabaseComparisonDialog
  - Select object (INL.BILAGNR)
  - Select databases (DEV, TEST, PROD)
  - Choose comparison type
         ↓
MultiDatabaseConnectionManager
  - Verify all connections
  - Open missing connections
         ↓
DatabaseObjectFetcherService
  ┌─ Fetch from DEV ──┬─ Fetch from TEST ─┬─ Fetch from PROD ─┐
  │ TableDefinition    │ TableDefinition   │ TableDefinition   │
  │  - 15 columns      │  - 14 columns     │  - 15 columns     │
  │  - 3 indexes       │  - 2 indexes      │  - 3 indexes      │
  │  - 2 FKs           │  - 2 FKs          │  - 2 FKs          │
  └────────────────────┴───────────────────┴───────────────────┘
         ↓
DatabaseComparisonService
  - Compare structures field-by-field
  - Identify differences
  - Generate difference report
  - Prepare ALTER statements
         ↓
DatabaseComparisonResultsDialog
  - Display side-by-side comparison
  - Highlight differences
  - Enable user actions
         ↓
User Actions:
  - Copy line DEV → TEST
  - Generate ALTER for PROD
  - Apply all from DEV
         ↓
AlterStatementReviewDialog
  - Show generated ALTER statements
  - User reviews and confirms
  - Execute on target database
         ↓
Success!
  - Update comparison view
  - Show success message
```

### 8.3 AI Integration Data Flow

```
User Query: "Show customers with high spending"
         ↓
AiIntegrationService
         ↓
AiContextBuilder
  - Get active schema
  - Get table structures
  - Get recent queries
  - Build rich prompt
         ↓
AI Provider (Local or Cloud)
         ↓
Prompt:
  "You are a DB2 SQL expert.
   Database: FKKTOTST
   Schema: INL
   Tables: CUSTOMERS (id, name, email), ORDERS (customer_id, amount, date)
   Recent: User has been querying financial data
   
   User Request: Show customers with high spending
   
   Generate optimized DB2 SQL query:"
         ↓
AI Response:
  "SELECT c.CUSTOMER_ID, c.NAME, SUM(o.AMOUNT) as TOTAL_SPENT
   FROM INL.CUSTOMERS c
   JOIN INL.ORDERS o ON c.CUSTOMER_ID = o.CUSTOMER_ID
   GROUP BY c.CUSTOMER_ID, c.NAME
   HAVING SUM(o.AMOUNT) > 10000
   ORDER BY TOTAL_SPENT DESC
   FETCH FIRST 100 ROWS ONLY;"
         ↓
AiIntegrationService
  - Validate SQL syntax
  - Check table/column existence
  - Add safety limits if needed
         ↓
Display in Editor
  - Insert into SqlEditor
  - Highlight as AI-generated
  - Show "Review before executing" tooltip
         ↓
User Reviews and Executes
```

---

## 9. IntelliSense Implementation Details

### 9.1 Leveraging Existing JSON Files

**Files to Use**:

#### db2_12.1_keywords.json
```json
{
  "keywords": ["SELECT", "INSERT", "UPDATE", "DELETE", ...],
  "datatypes": ["INTEGER", "VARCHAR", "DECIMAL", "DATE", ...],
  "functions": ["COUNT", "SUM", "AVG", "SUBSTRING", ...],
  "systemTables": ["SYSCAT.TABLES", "SYSCAT.COLUMNS", ...]
}
```

**Usage**:
- Keyword completion
- Syntax highlighting
- Data type suggestions

#### db2_12.1_sql_statements.json
```json
{
  "statements": {
    "GetTableColumns": {
      "sql": "SELECT ... FROM SYSCAT.COLUMNS ...",
      "description": "Get all columns for a table",
      "parameters": ["schema", "tableName"]
    }
  }
}
```

**Usage**:
- Template completion
- Quick query generation
- Parameter hints

#### db2_12.1_system_metadata.json
```json
{
  "systemCatalogs": {
    "SYSCAT.TABLES": {
      "columns": ["TABSCHEMA", "TABNAME", "TYPE", ...],
      "description": "System catalog for tables"
    }
  }
}
```

**Usage**:
- SYSCAT query assistance
- System table completion
- Column name suggestions for system views

### 9.2 Provider-Specific IntelliSense

**Specification**:

```csharp
public class IntelliSenseManager
{
    private Dictionary<string, IIntelliSenseProvider> _providers = new();
    
    public void RegisterProvider(string providerName, IIntelliSenseProvider provider)
    {
        _providers[providerName] = provider;
    }
    
    public void SetActiveProvider(string providerName, string version)
    {
        // Load JSON files for this provider
        var keywordsFile = $"ConfigFiles/{providerName}_{version}_keywords.json";
        var statementsFile = $"ConfigFiles/{providerName}_{version}_sql_statements.json";
        var metadataFile = $"ConfigFiles/{providerName}_{version}_system_metadata.json";
        
        _activeProvider = _providers[providerName];
        _activeProvider.LoadMetadata(keywordsFile, statementsFile, metadataFile);
    }
    
    public List<ICompletionData> GetCompletions(
        string text, 
        int caretPosition,
        DB2ConnectionManager? connection)
    {
        var context = new CompletionContext
        {
            Text = text,
            CaretPosition = caretPosition,
            Provider = _activeProvider,
            Connection = connection
        };
        
        return _activeProvider.GetCompletions(context);
    }
}
```

**Multi-Provider Support**:
```
- DB2 12.1 → Load db2_12.1_*.json files
- PostgreSQL 14 → Load postgresql_14_*.json files (future)
- SQL Server 2022 → Load sqlserver_2022_*.json files (future)
- Oracle 19c → Load oracle_19c_*.json files (future)
```

**Provider Detection**:
```csharp
// Automatically detect and load correct IntelliSense
var connectionInfo = GetActiveConnection();
var provider = connectionInfo.Provider; // "DB2"
var version = await connectionInfo.GetVersionAsync(); // "12.1"

_intelliSenseManager.SetActiveProvider(provider, version);
```

### 9.3 Completion Data Classes

```csharp
public class Db2TableCompletionData : ICompletionData
{
    public string Text { get; set; } // "INL.BILAGNR"
    public string Description { get; set; } // "Invoice numbers table (15,234 rows)"
    public ImageSource? Image { get; set; } // Table icon
    public double Priority { get; set; } = 1.0;
    
    public object Content => new StackPanel
    {
        Children =
        {
            new TextBlock { Text = Text, FontWeight = FontWeights.Bold },
            new TextBlock { Text = Description, FontSize = 10, Foreground = Brushes.Gray }
        }
    };
    
    public void Complete(TextArea textArea, ISegment completionSegment, EventArgs e)
    {
        textArea.Document.Replace(completionSegment, Text);
    }
}

public class Db2ColumnCompletionData : ICompletionData
{
    public string ColumnName { get; set; }
    public string DataType { get; set; }
    public bool IsNullable { get; set; }
    public bool IsPrimaryKey { get; set; }
    
    public string Text => ColumnName;
    public string Description => $"{DataType}{(IsNullable ? "" : " NOT NULL")}{(IsPrimaryKey ? " PK" : "")}";
    // ... implement ICompletionData
}

public class Db2SnippetCompletionData : ICompletionData
{
    public string Trigger { get; set; }
    public string Template { get; set; }
    public string Description { get; set; }
    
    public void Complete(TextArea textArea, ISegment completionSegment, EventArgs e)
    {
        // Insert snippet with placeholders
        // Enable TAB navigation between placeholders
        textArea.Document.Replace(completionSegment, Template);
        HighlightPlaceholders(textArea);
    }
}
```

---

## 10. AI Use Cases - Comprehensive List

### 10.1 Query Assistance

| Use Case | Input | AI Output | Benefit |
|----------|-------|-----------|---------|
| **Natural Language to SQL** | "Find customers who ordered in last month" | Optimized SELECT query | Saves time, reduces errors |
| **SQL Completion** | `SELECT * FROM INL.BIL` | Suggests `INL.BILAGNR` | Faster typing |
| **Query Optimization** | Slow query | Optimized version + explanation | Better performance |
| **Error Explanation** | SQL error message | Plain English explanation + fix | Faster debugging |
| **Query Explanation** | Complex SQL | Plain English description | Better understanding |

### 10.2 Schema Design

| Use Case | Input | AI Output | Benefit |
|----------|-------|-----------|---------|
| **Schema Analysis** | Table name | Design suggestions, missing indexes | Better design |
| **Normalization Check** | Table structure | Normalization issues + fixes | Data integrity |
| **Index Recommendations** | Query patterns | Suggested indexes with impact | Performance |
| **Constraint Suggestions** | Table data | CHECK constraints, defaults | Data quality |

### 10.3 Development Assistance

| Use Case | Input | AI Output | Benefit |
|----------|-------|-----------|---------|
| **Generate Test Data** | Table schema | Realistic INSERT statements | Testing |
| **Create Mock Objects** | Production table | Test version with sample data | Development |
| **Generate Documentation** | Table/procedure | Markdown documentation | Knowledge sharing |
| **Code Review** | SQL code | Best practices analysis | Quality |
| **Security Scan** | Query | SQL injection vulnerabilities | Security |

### 10.4 Data Analysis

| Use Case | Input | AI Output | Benefit |
|----------|-------|-----------|---------|
| **Data Quality Check** | Table name | Anomaly detection queries | Data quality |
| **Pattern Detection** | Table data | Common patterns, outliers | Insights |
| **Report Generation** | Requirements | SQL for reports | Business intelligence |
| **Trend Analysis** | Time-series table | Trend queries, visualizations | Analytics |

### 10.5 Migration & Comparison

| Use Case | Input | AI Output | Benefit |
|----------|-------|-----------|---------|
| **Migration Script** | Source/target schemas | Complete migration DDL | Migration |
| **Rollback Script** | Migration script | Automatic rollback DDL | Safety |
| **Data Transformation** | Old/new formats | ETL scripts | Data migration |
| **Comparison Summary** | Diff results | Natural language summary | Understanding |

---

## 11. Implementation Roadmap

### Sprint 1: Visual & IntelliSense (Week 1)
**Goal**: Improve UX with visual polish and powerful IntelliSense

**Tasks**:
1. Visual improvements (font, themes, loading)
2. IntelliSense service foundation
3. Keyword completion
4. Live schema completion
5. Snippet support

**Deliverables**:
- Polished UI in dark/light modes
- Working IntelliSense with autocomplete
- Faster developer workflow

### Sprint 2: Package Dependencies & Database Comparison Foundation (Week 2)
**Goal**: Complete package analysis and start comparison feature

**Tasks**:
1. Package dependency analyzer
2. Add Dependencies tab to PackageDetailsDialog
3. Multi-database connection manager
4. Database object fetcher service
5. Comparison service foundation

**Deliverables**:
- Complete package analysis
- Foundation for database comparison

### Sprint 3: Database Comparison UI (Week 3)
**Goal**: Complete database comparison feature

**Tasks**:
1. DatabaseComparisonDialog
2. DatabaseComparisonResultsDialog
3. Side-by-side diff view
4. Interactive actions (copy line, generate ALTER)
5. Integration with View menu and context menu
6. Comprehensive testing

**Deliverables**:
- Full database comparison feature
- Working ALTER generation
- Professional diff visualization

### Sprint 4: AI Integration (Week 4-5)
**Goal**: Add AI-powered features

**Tasks**:
1. Research and select local AI model
2. Implement AI provider abstraction
3. Create AiIntegrationService
4. Add AI Assistant panel
5. Implement natural language to SQL
6. Implement error explanation
7. Add AI settings dialog
8. Privacy and security controls

**Deliverables**:
- Working AI assistant
- Natural language query generation
- Smart error explanations
- Configurable AI settings

---

## 12. Success Criteria

### Visual Improvements
- ✅ Object browser font size optimized (11-12pt)
- ✅ Perfect dark mode support
- ✅ Perfect light mode support
- ✅ Loading indicators provide feedback
- ✅ No visual glitches

### IntelliSense
- ✅ Autocomplete works for keywords
- ✅ Autocomplete works for table/column names
- ✅ Context-aware suggestions
- ✅ Snippet expansion
- ✅ Function signature hints
- ✅ Fast response time (<100ms)
- ✅ Works offline

### Package Dependencies
- ✅ Shows all tables used by package
- ✅ Shows all views/procedures/functions used
- ✅ Bidirectional navigation works
- ✅ Accurate SQL parsing

### Database Comparison
- ✅ Compare across 2-4 databases
- ✅ Side-by-side visualization
- ✅ Synchronized scrolling
- ✅ Difference highlighting
- ✅ Generate ALTER statements
- ✅ Copy individual lines
- ✅ Apply full source
- ✅ CLI command support

### AI Integration
- ✅ Natural language to SQL works
- ✅ Error explanations are helpful
- ✅ Query optimization provides value
- ✅ Works offline (local AI)
- ✅ Privacy controls functional
- ✅ User can disable AI completely

---

## 13. Risk Assessment

### Low Risk
- ✅ Visual improvements (low complexity, high value)
- ✅ IntelliSense (established patterns, well-understood)

### Medium Risk
- ⚠️ Package dependency parsing (SQL parsing can be tricky)
- ⚠️ Database comparison (complex UI, multiple edge cases)

### High Risk
- ⚠️ AI integration (model size, performance, licensing)
- ⚠️ Local AI deployment (GPU requirements, model distribution)

### Mitigation Strategies
1. **Incremental implementation** - Build in phases, test each phase
2. **Fallback mechanisms** - IntelliSense works without AI
3. **Make AI optional** - Core features work without AI
4. **Comprehensive testing** - Test with real databases
5. **User feedback loop** - Iterate based on usage

---

## 14. Questions Answered

### My Questions (Original)

1. **Object browser font size**: ✅ Will reduce to 11pt
2. **Object browser population**: ✅ Already async in background (optimal)
3. **Package shows dependencies**: ❌ Currently no, will implement
4. **Table shows "Used By Package"**: ✅ Already exists

### User's New Requirements

1. **Database comparison (not schema)**: ✅ Specified - compare across DEV/TEST/PROD
2. **IntelliSense using JSON files**: ✅ Detailed specification provided
3. **AI integration**: ✅ Comprehensive use cases and architecture defined

---

## 15. Immediate Next Steps

**Ready to Implement** (no blockers):
1. ✅ Visual improvements
2. ✅ IntelliSense system
3. ✅ Package dependencies

**Proceed with Implementation?**
- All specifications complete
- All design decisions made
- Architecture documented
- Ready for continuous implementation

**Estimated Total Time**: 50-70 hours over 3-4 weeks

---

**Specification Status**: ✅ **COMPLETE - READY FOR IMPLEMENTATION**  
**Awaiting**: User approval to begin continuous implementation  
**Priority Order**: Visual → IntelliSense → Package Dependencies → Database Comparison → AI (optional)

