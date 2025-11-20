using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using IBM.Data.Db2;
using NLog;
using WindowsDb2Editor.Data;
using WindowsDb2Editor.Models;

namespace WindowsDb2Editor.Services;

/// <summary>
/// Service for querying DB2 system catalog (SYSCAT) to populate object browser
/// </summary>
public class ObjectBrowserService
{
    private readonly DB2ConnectionManager _connectionManager;
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    public ObjectBrowserService(DB2ConnectionManager connectionManager)
    {
        _connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));
        Logger.Debug("ObjectBrowserService initialized");
    }
    
    #region Access Level Management
    
    /// <summary>
    /// Determine current user's access level by querying SYSCAT.DBAUTH
    /// </summary>
    public async Task<UserAccessLevel> GetUserAccessLevelAsync()
    {
        Logger.Debug("Determining user access level");
        
        try
        {
            var sql = @"
                SELECT 
                    DBADMAUTH,
                    SECURITYADMAUTH,
                    DATAACCESSAUTH,
                    CREATETABAUTH,
                    BINDADDAUTH
                FROM SYSCAT.DBAUTH
                WHERE GRANTEE = CURRENT USER
                  AND GRANTEETYPE = 'U'
                FETCH FIRST 1 ROW ONLY";
            
            using var command = _connectionManager.CreateCommand(sql);
            using var reader = await command.ExecuteReaderAsync();
            
            if (await reader.ReadAsync())
            {
                var dbAdm = reader.GetString(0);
                var secAdm = reader.GetString(1);
                var dataAccess = reader.GetString(2);
                var createTab = reader.GetString(3);
                var bindAdd = reader.GetString(4);
                
                // DBA level: Has DBADM or SECADM authority
                if (dbAdm == "Y" || secAdm == "Y")
                {
                    Logger.Info("User access level: DBA");
                    return UserAccessLevel.DBA;
                }
                
                // Advanced level: Has significant privileges
                if (createTab == "Y" || bindAdd == "Y" || dataAccess == "Y")
                {
                    Logger.Info("User access level: Advanced");
                    return UserAccessLevel.Advanced;
                }
            }
            
            // Standard level: Only CONNECT or limited privileges
            Logger.Info("User access level: Standard");
            return UserAccessLevel.Standard;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to determine user access level, defaulting to Standard");
            return UserAccessLevel.Standard;
        }
    }
    
    /// <summary>
    /// Check if user can view a specific category
    /// </summary>
    public bool CanViewCategory(CategoryType categoryType, UserAccessLevel userAccessLevel)
    {
        return categoryType switch
        {
            CategoryType.Schemas => true,  // All users
            CategoryType.Aliases => true,  // All users
            CategoryType.Security => userAccessLevel >= UserAccessLevel.DBA,
            CategoryType.UserLanguage => userAccessLevel >= UserAccessLevel.DBA,
            _ => userAccessLevel >= UserAccessLevel.Advanced
        };
    }
    
    /// <summary>
    /// Check if user can view a specific object type
    /// </summary>
    public bool CanViewObjectType(ObjectType objectType, UserAccessLevel userAccessLevel)
    {
        return objectType switch
        {
            ObjectType.Tables => true,  // All users
            ObjectType.Views => true,   // All users
            ObjectType.Synonyms => true, // All users
            _ => userAccessLevel >= UserAccessLevel.Advanced
        };
    }
    
    /// <summary>
    /// Get list of visible object types for user's access level
    /// </summary>
    public List<ObjectType> GetVisibleObjectTypes(UserAccessLevel userAccessLevel)
    {
        var visibleTypes = new List<ObjectType> { ObjectType.Tables, ObjectType.Views, ObjectType.Synonyms };
        
        if (userAccessLevel >= UserAccessLevel.Advanced)
        {
            visibleTypes.AddRange(new[]
            {
                ObjectType.Procedures,
                ObjectType.Functions,
                ObjectType.Indexes,
                ObjectType.Triggers,
                ObjectType.Sequences,
                ObjectType.Types,
                ObjectType.Packages
            });
        }
        
        return visibleTypes;
    }
    
    #endregion
    
    #region Top-Level Categories
    
    /// <summary>
    /// Get top-level categories filtered by user access level
    /// </summary>
    public async Task<List<CategoryNode>> GetTopLevelCategoriesAsync(UserAccessLevel userAccessLevel)
    {
        Logger.Debug("Loading top-level categories for access level: {Level}", userAccessLevel);
        
        var categories = new List<CategoryNode>();
        
        // Always visible categories
        categories.Add(new CategoryNode
        {
            Name = "Schemas",
            Icon = ObjectBrowserIcons.Schemas,
            Type = CategoryType.Schemas,
            IsLazyLoad = true,
            MinimumAccessLevel = UserAccessLevel.Standard,
            Count = await GetSchemaCountAsync()
        });
        
        categories.Add(new CategoryNode
        {
            Name = "Aliases",
            Icon = ObjectBrowserIcons.Aliases,
            Type = CategoryType.Aliases,
            IsLazyLoad = true,
            MinimumAccessLevel = UserAccessLevel.Standard,
            Count = await GetAliasCountAsync()
        });
        
        // Advanced level categories
        if (userAccessLevel >= UserAccessLevel.Advanced)
        {
            categories.Add(new CategoryNode
            {
                Name = "Tablespaces",
                Icon = ObjectBrowserIcons.Tablespaces,
                Type = CategoryType.Tablespaces,
                IsLazyLoad = true,
                MinimumAccessLevel = UserAccessLevel.Advanced,
                Count = await GetTablespaceCountAsync()
            });
            
            categories.Add(new CategoryNode
            {
                Name = "Packages",
                Icon = ObjectBrowserIcons.Packages,
                Type = CategoryType.Packages,
                IsLazyLoad = true,
                MinimumAccessLevel = UserAccessLevel.Advanced,
                Count = await GetPackageCountAsync()
            });
            
            categories.Add(new CategoryNode
            {
                Name = "User-Defined Types",
                Icon = ObjectBrowserIcons.UserDefinedTypes,
                Type = CategoryType.UserDefinedTypes,
                IsLazyLoad = true,
                MinimumAccessLevel = UserAccessLevel.Advanced,
                Count = await GetUserDefinedTypeCountAsync()
            });
        }
        
        // DBA level categories
        if (userAccessLevel >= UserAccessLevel.DBA)
        {
            categories.Add(new CategoryNode
            {
                Name = "Security",
                Icon = ObjectBrowserIcons.Security,
                Type = CategoryType.Security,
                IsLazyLoad = true,
                MinimumAccessLevel = UserAccessLevel.DBA,
                Count = await GetSecurityObjectCountAsync()
            });
        }
        
        Logger.Info("Loaded {Count} categories for access level {Level}", categories.Count, userAccessLevel);
        return categories;
    }
    
    private async Task<int> GetSchemaCountAsync()
    {
        try
        {
            var sql = "SELECT COUNT(*) FROM SYSCAT.SCHEMATA";
            using var command = _connectionManager.CreateCommand(sql);
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get schema count");
            return 0;
        }
    }
    
    private async Task<int> GetAliasCountAsync()
    {
        try
        {
            var sql = "SELECT COUNT(*) FROM SYSCAT.TABLES WHERE TYPE = 'A'";
            using var command = _connectionManager.CreateCommand(sql);
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get alias count");
            return 0;
        }
    }
    
    private async Task<int> GetTablespaceCountAsync()
    {
        try
        {
            var sql = "SELECT COUNT(*) FROM SYSCAT.TABLESPACES";
            using var command = _connectionManager.CreateCommand(sql);
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get tablespace count");
            return 0;
        }
    }
    
    private async Task<int> GetPackageCountAsync()
    {
        try
        {
            var sql = "SELECT COUNT(*) FROM SYSCAT.PACKAGES";
            using var command = _connectionManager.CreateCommand(sql);
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get package count");
            return 0;
        }
    }
    
    private async Task<int> GetUserDefinedTypeCountAsync()
    {
        try
        {
            var sql = "SELECT COUNT(*) FROM SYSCAT.DATATYPES WHERE METATYPE IN ('A', 'D', 'R', 'S')";
            using var command = _connectionManager.CreateCommand(sql);
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get user-defined type count");
            return 0;
        }
    }
    
    private async Task<int> GetSecurityObjectCountAsync()
    {
        try
        {
            // Count of roles + groups + users
            var sql = @"
                SELECT 
                    (SELECT COUNT(*) FROM SYSCAT.ROLES) +
                    (SELECT COUNT(DISTINCT GRANTEE) FROM SYSCAT.DBAUTH WHERE GRANTEETYPE = 'G') +
                    (SELECT COUNT(DISTINCT GRANTEE) FROM SYSCAT.DBAUTH WHERE GRANTEETYPE = 'U')
                FROM SYSIBM.SYSDUMMY1";
            using var command = _connectionManager.CreateCommand(sql);
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get security object count");
            return 0;
        }
    }
    
    #endregion
    
    #region Schemas
    
    /// <summary>
    /// Get all schemas
    /// </summary>
    public async Task<List<SchemaNode>> GetAllSchemasAsync()
    {
        Logger.Debug("Loading all schemas");
        
        try
        {
            var sql = @"
                SELECT TRIM(SCHEMANAME), TRIM(OWNER), CREATE_TIME, TRIM(REMARKS)
                FROM SYSCAT.SCHEMATA
                ORDER BY SCHEMANAME";
            
            var schemas = new List<SchemaNode>();
            using var command = _connectionManager.CreateCommand(sql);
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                var schemaName = reader.GetString(0).Trim(); // Explicit C# trim
                var isSystemSchema = IsSystemSchema(schemaName);
                
                schemas.Add(new SchemaNode
                {
                    SchemaName = schemaName,
                    Type = isSystemSchema ? "SYSTEM" : "USER",
                    ObjectTypes = new List<ObjectTypeCategory>()
                });
            }
            
            Logger.Info("Loaded {Count} schemas", schemas.Count);
            return schemas;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load schemas");
            return new List<SchemaNode>();
        }
    }
    
    private bool IsSystemSchema(string schemaName)
    {
        var systemSchemas = new[] { "SYSCAT", "SYSIBM", "SYSFUN", "SYSPROC", "SYSSTAT", "SYSTOOLS", 
                                    "NULLID", "SQLJ", "DB2INST1" };
        return systemSchemas.Contains(schemaName, StringComparer.OrdinalIgnoreCase);
    }
    
    /// <summary>
    /// Get schema details with object counts
    /// </summary>
    public async Task<Dictionary<ObjectType, int>> GetSchemaObjectCountsAsync(string schemaName, UserAccessLevel userAccessLevel)
    {
        Logger.Debug("Getting object counts for schema: {Schema}", schemaName);
        
        var counts = new Dictionary<ObjectType, int>();
        var visibleTypes = GetVisibleObjectTypes(userAccessLevel);
        
        foreach (var objectType in visibleTypes)
        {
            counts[objectType] = await GetObjectCountAsync(schemaName, objectType);
        }
        
        return counts;
    }
    
    private async Task<int> GetObjectCountAsync(string schemaName, ObjectType objectType)
    {
        try
        {
            var sql = objectType switch
            {
                ObjectType.Tables => "SELECT COUNT(*) FROM SYSCAT.TABLES WHERE TRIM(TABSCHEMA) = ? AND TYPE IN ('T', 'S', 'G', 'H', 'L', 'N', 'U', 'W')",
                ObjectType.Views => "SELECT COUNT(*) FROM SYSCAT.VIEWS WHERE TRIM(VIEWSCHEMA) = ?",
                ObjectType.Procedures => "SELECT COUNT(*) FROM SYSCAT.ROUTINES WHERE TRIM(ROUTINESCHEMA) = ? AND ROUTINETYPE = 'P'",
                ObjectType.Functions => "SELECT COUNT(*) FROM SYSCAT.ROUTINES WHERE TRIM(ROUTINESCHEMA) = ? AND ROUTINETYPE = 'F'",
                ObjectType.Indexes => "SELECT COUNT(*) FROM SYSCAT.INDEXES WHERE TRIM(TABSCHEMA) = ?",
                ObjectType.Triggers => "SELECT COUNT(*) FROM SYSCAT.TRIGGERS WHERE TRIM(TRIGSCHEMA) = ?",
                ObjectType.Sequences => "SELECT COUNT(*) FROM SYSCAT.SEQUENCES WHERE TRIM(SEQSCHEMA) = ?",
                ObjectType.Synonyms => "SELECT COUNT(*) FROM SYSCAT.TABLES WHERE TRIM(TABSCHEMA) = ? AND TYPE = 'A'",
                ObjectType.Types => "SELECT COUNT(*) FROM SYSCAT.DATATYPES WHERE TRIM(TYPESCHEMA) = ? AND METATYPE IN ('A', 'D', 'R', 'S')",
                ObjectType.Packages => "SELECT COUNT(*) FROM SYSCAT.PACKAGES WHERE TRIM(PKGSCHEMA) = ?",
                _ => throw new ArgumentException($"Unknown object type: {objectType}")
            };
            
            using var command = _connectionManager.CreateCommand(sql);
            command.Parameters.Add(new DB2Parameter("@schemaName", schemaName));
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get count for {Type} in schema {Schema}", objectType, schemaName);
            return 0;
        }
    }
    
    #endregion
    
    #region Tables
    
    /// <summary>
    /// Get tables in a schema
    /// </summary>
    public async Task<List<DatabaseObject>> GetTablesAsync(string schemaName)
    {
        Logger.Debug("Loading tables for schema: {Schema}", schemaName);
        
        try
        {
            var sql = @"
                SELECT 
                    TRIM(TABNAME),
                    TRIM(TYPE),
                    TRIM(OWNER),
                    CARD AS ROW_COUNT,
                    TRIM(TBSPACE),
                    CREATE_TIME,
                    TRIM(REMARKS)
                FROM SYSCAT.TABLES
                WHERE TRIM(TABSCHEMA) = ?
                  AND TYPE IN ('T', 'S', 'G', 'H', 'L', 'N', 'U', 'W')
                ORDER BY TABNAME";
            
            var tables = new List<DatabaseObject>();
            using var command = _connectionManager.CreateCommand(sql);
            command.Parameters.Add(new DB2Parameter("@schemaName", schemaName));
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                var tableName = reader.GetString(0).Trim();
                tables.Add(new DatabaseObject
                {
                    Name = tableName,
                    SchemaName = schemaName,
                    FullName = $"{schemaName}.{tableName}",
                    Type = ObjectType.Tables,
                    Icon = ObjectBrowserIcons.Table,
                    TableType = reader.GetString(1).Trim(),
                    Owner = reader.GetString(2).Trim(),
                    RowCount = reader.IsDBNull(3) ? null : reader.GetInt64(3),
                    TableSpace = reader.IsDBNull(4) ? null : reader.GetString(4).Trim(),
                    CreatedAt = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                    Remarks = reader.IsDBNull(6) ? string.Empty : reader.GetString(6).Trim()
                });
            }
            
            Logger.Info("Loaded {Count} tables for schema {Schema}", tables.Count, schemaName);
            return tables;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load tables for schema {Schema}", schemaName);
            return new List<DatabaseObject>();
        }
    }
    
    #endregion
    
    #region Views
    
    /// <summary>
    /// Get views in a schema
    /// </summary>
    public async Task<List<DatabaseObject>> GetViewsAsync(string schemaName)
    {
        Logger.Debug("Loading views for schema: {Schema}", schemaName);
        
        try
        {
            var sql = @"
                SELECT 
                    TRIM(V.VIEWNAME) AS VIEWNAME,
                    TRIM(T.DEFINER) AS OWNER,
                    TRIM(T.REMARKS) AS REMARKS
                FROM SYSCAT.TABLES T
                JOIN SYSCAT.VIEWS V ON T.TABSCHEMA = V.VIEWSCHEMA 
                    AND T.TABNAME = V.VIEWNAME
                WHERE T.TYPE = 'V' AND T.TABSCHEMA = ?
                ORDER BY V.VIEWNAME";
            
            var views = new List<DatabaseObject>();
            using var command = _connectionManager.CreateCommand(sql);
            command.Parameters.Add(new DB2Parameter("schemaName", schemaName.Trim()));
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                var viewName = reader.GetString(0).Trim(); // Explicit C# trim
                views.Add(new DatabaseObject
                {
                    Name = viewName,
                    SchemaName = schemaName,
                    FullName = $"{schemaName}.{viewName}",
                    Type = ObjectType.Views,
                    Icon = ObjectBrowserIcons.View,
                    Owner = reader.GetString(1).Trim(),
                    Remarks = reader.IsDBNull(2) ? string.Empty : reader.GetString(2).Trim()
                });
            }
            
            Logger.Info("Loaded {Count} views for schema {Schema}", views.Count, schemaName);
            return views;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load views for schema {Schema}", schemaName);
            return new List<DatabaseObject>();
        }
    }
    
    #endregion
    
    #region Procedures
    
    /// <summary>
    /// Get procedures in a schema
    /// </summary>
    public async Task<List<DatabaseObject>> GetProceduresAsync(string schemaName)
    {
        Logger.Debug("Loading procedures for schema: {Schema}", schemaName);
        
        try
        {
            var sql = @"
                SELECT 
                    TRIM(ROUTINENAME),
                    TRIM(SPECIFICNAME),
                    TRIM(LANGUAGE),
                    PARM_COUNT,
                    CREATE_TIME,
                    TRIM(OWNER),
                    TRIM(REMARKS)
                FROM SYSCAT.ROUTINES
                WHERE TRIM(ROUTINESCHEMA) = ?
                  AND ROUTINETYPE = 'P'
                ORDER BY ROUTINENAME";
            
            var procedures = new List<DatabaseObject>();
            using var command = _connectionManager.CreateCommand(sql);
            command.Parameters.Add(new DB2Parameter("@schemaName", schemaName));
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                var procName = reader.GetString(0).Trim();
                procedures.Add(new DatabaseObject
                {
                    Name = procName,
                    SchemaName = schemaName,
                    FullName = $"{schemaName}.{procName}",
                    Type = ObjectType.Procedures,
                    Icon = ObjectBrowserIcons.Procedure,
                    Language = reader.GetString(2).Trim(),
                    ParameterCount = reader.GetInt32(3),
                    CreatedAt = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                    Owner = reader.GetString(5).Trim(),
                    Remarks = reader.IsDBNull(6) ? string.Empty : reader.GetString(6).Trim()
                });
            }
            
            Logger.Info("Loaded {Count} procedures for schema {Schema}", procedures.Count, schemaName);
            return procedures;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load procedures for schema {Schema}", schemaName);
            return new List<DatabaseObject>();
        }
    }
    
    #endregion
    
    #region Functions
    
    /// <summary>
    /// Get functions in a schema
    /// </summary>
    public async Task<List<DatabaseObject>> GetFunctionsAsync(string schemaName)
    {
        Logger.Debug("Loading functions for schema: {Schema}", schemaName);
        
        try
        {
            var sql = @"
                SELECT 
                    TRIM(ROUTINENAME) AS ROUTINENAME,
                    TRIM(SPECIFICNAME) AS SPECIFICNAME,
                    TRIM(LANGUAGE) AS LANGUAGE,
                    PARM_COUNT,
                    TRIM(FUNCTIONTYPE) AS FUNCTIONTYPE,
                    CREATE_TIME,
                    TRIM(OWNER) AS OWNER,
                    TRIM(REMARKS) AS REMARKS
                FROM SYSCAT.ROUTINES
                WHERE ROUTINESCHEMA = ?
                  AND ROUTINETYPE = 'F'
                ORDER BY ROUTINENAME";
            
            var functions = new List<DatabaseObject>();
            using var command = _connectionManager.CreateCommand(sql);
            command.Parameters.Add(new DB2Parameter("schemaName", schemaName.Trim()));
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                var functionName = reader.GetString(0).Trim();
                var functionType = reader.GetString(4).Trim();
                functions.Add(new DatabaseObject
                {
                    Name = functionName,
                    SchemaName = schemaName,
                    FullName = $"{schemaName}.{functionName}",
                    Type = ObjectType.Functions,
                    Icon = functionType == "S" ? ObjectBrowserIcons.ScalarFunction : ObjectBrowserIcons.TableFunction,
                    Language = reader.GetString(2).Trim(),
                    ParameterCount = reader.GetInt32(3),
                    CreatedAt = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                    Owner = reader.GetString(6).Trim(),
                    Remarks = reader.IsDBNull(7) ? string.Empty : reader.GetString(7).Trim()
                });
            }
            
            Logger.Info("Loaded {Count} functions for schema {Schema}", functions.Count, schemaName);
            return functions;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load functions for schema {Schema}", schemaName);
            return new List<DatabaseObject>();
        }
    }
    
    #endregion
    
    #region Tablespaces
    
    /// <summary>
    /// Get all tablespaces
    /// </summary>
    public async Task<List<TablespaceInfo>> GetTablespacesAsync()
    {
        Logger.Debug("Loading tablespaces");
        
        try
        {
            var sql = @"
                SELECT 
                    TRIM(TBSPACE),
                    TRIM(TBSPACETYPE),
                    TRIM(DATATYPE),
                    PAGESIZE,
                    TRIM(OWNER),
                    CREATE_TIME,
                    TRIM(REMARKS)
                FROM SYSCAT.TABLESPACES
                ORDER BY TBSPACE";
            
            var tablespaces = new List<TablespaceInfo>();
            using var command = _connectionManager.CreateCommand(sql);
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                tablespaces.Add(new TablespaceInfo
                {
                    TablespaceName = reader.GetString(0),
                    TablespaceType = reader.GetString(1),
                    DataType = reader.GetString(2),
                    PageSize = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                    Owner = reader.GetString(4),
                    CreateTime = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                    Remarks = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                });
            }
            
            Logger.Info("Loaded {Count} tablespaces", tablespaces.Count);
            return tablespaces;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load tablespaces");
            return new List<TablespaceInfo>();
        }
    }
    
    #endregion
    
    #region Security - Roles
    
    /// <summary>
    /// Get all roles
    /// </summary>
    public async Task<List<SecurityPrincipal>> GetRolesAsync()
    {
        Logger.Debug("Loading roles");
        
        try
        {
            var sql = @"
                SELECT 
                    TRIM(ROLENAME),
                    CREATE_TIME,
                    TRIM(REMARKS)
                FROM SYSCAT.ROLES
                ORDER BY ROLENAME";
            
            var roles = new List<SecurityPrincipal>();
            using var command = _connectionManager.CreateCommand(sql);
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                roles.Add(new SecurityPrincipal
                {
                    Name = reader.GetString(0),
                    Type = SecurityPrincipalType.Role,
                    Icon = ObjectBrowserIcons.Role,
                    CreateTime = reader.IsDBNull(1) ? null : reader.GetDateTime(1),
                    Remarks = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
                });
            }
            
            Logger.Info("Loaded {Count} roles", roles.Count);
            return roles;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load roles");
            return new List<SecurityPrincipal>();
        }
    }
    
    #endregion
    
    #region Security - Groups
    
    /// <summary>
    /// Get all groups
    /// </summary>
    public async Task<List<SecurityPrincipal>> GetGroupsAsync()
    {
        Logger.Debug("Loading groups");
        
        try
        {
            var sql = @"
                SELECT DISTINCT TRIM(GRANTEE) AS GROUPNAME
                FROM SYSCAT.DBAUTH
                WHERE GRANTEETYPE = 'G'
                UNION
                SELECT DISTINCT TRIM(GRANTEE) AS GROUPNAME
                FROM SYSCAT.TABAUTH
                WHERE GRANTEETYPE = 'G'
                ORDER BY GROUPNAME";
            
            var groups = new List<SecurityPrincipal>();
            using var command = _connectionManager.CreateCommand(sql);
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                var groupName = reader.GetString(0);
                groups.Add(new SecurityPrincipal
                {
                    Name = groupName,
                    Type = SecurityPrincipalType.Group,
                    Icon = groupName.Equals("PUBLIC", StringComparison.OrdinalIgnoreCase) 
                        ? ObjectBrowserIcons.PublicGroup 
                        : ObjectBrowserIcons.Group
                });
            }
            
            Logger.Info("Loaded {Count} groups", groups.Count);
            return groups;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load groups");
            return new List<SecurityPrincipal>();
        }
    }
    
    #endregion
    
    #region Security - Users
    
    /// <summary>
    /// Get all users
    /// </summary>
    public async Task<List<SecurityPrincipal>> GetUsersAsync()
    {
        Logger.Debug("Loading users");
        
        try
        {
            var sql = @"
                SELECT DISTINCT TRIM(GRANTEE) AS USERNAME
                FROM SYSCAT.DBAUTH
                WHERE GRANTEETYPE = 'U'
                UNION
                SELECT DISTINCT TRIM(GRANTEE) AS USERNAME
                FROM SYSCAT.TABAUTH
                WHERE GRANTEETYPE = 'U'
                ORDER BY USERNAME";
            
            var users = new List<SecurityPrincipal>();
            using var command = _connectionManager.CreateCommand(sql);
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                users.Add(new SecurityPrincipal
                {
                    Name = reader.GetString(0),
                    Type = SecurityPrincipalType.User,
                    Icon = ObjectBrowserIcons.User
                });
            }
            
            Logger.Info("Loaded {Count} users", users.Count);
            return users;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load users");
            return new List<SecurityPrincipal>();
        }
    }
    
    #endregion
    
    #region Security - Privilege Queries
    
    /// <summary>
    /// Get privilege counts for a role (all 15 categories)
    /// </summary>
    public async Task<Dictionary<PrivilegeCategoryType, int>> GetRolePrivilegeCountsAsync(string roleName)
    {
        Logger.Debug("Getting privilege counts for role: {Role}", roleName);
        var counts = new Dictionary<PrivilegeCategoryType, int>();
        
        try
        {
            // Users who have this role
            var usersSql = "SELECT COUNT(*) FROM SYSCAT.ROLEAUTH WHERE TRIM(ROLENAME) = ?";
            using var usersCmd = _connectionManager.CreateCommand(usersSql);
            usersCmd.Parameters.Add(new DB2Parameter("@roleName", roleName));
            counts[PrivilegeCategoryType.Users] = Convert.ToInt32(await usersCmd.ExecuteScalarAsync());
            
            // Note: Roles in DB2 don't have direct object privileges
            // They are granted to users who then have the privileges
            // For now, return 0 for object-level privileges
            foreach (var category in Enum.GetValues<PrivilegeCategoryType>())
            {
                if (!counts.ContainsKey(category))
                {
                    counts[category] = 0;
                }
            }
            
            return counts;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get privilege counts for role {Role}", roleName);
            return new Dictionary<PrivilegeCategoryType, int>();
        }
    }
    
    /// <summary>
    /// Get privilege counts for a group (all 15 categories)
    /// </summary>
    public async Task<Dictionary<PrivilegeCategoryType, int>> GetGroupPrivilegeCountsAsync(string groupName)
    {
        Logger.Debug("Getting privilege counts for group: {Group}", groupName);
        var counts = new Dictionary<PrivilegeCategoryType, int>();
        
        try
        {
            // Tables
            var tablesSql = "SELECT COUNT(*) FROM SYSCAT.TABAUTH WHERE GRANTEE = ? AND GRANTEETYPE = 'G'";
            using var tablesCmd = _connectionManager.CreateCommand(tablesSql);
            tablesCmd.Parameters.Add(new DB2Parameter("@groupName", groupName));
            counts[PrivilegeCategoryType.Tables] = Convert.ToInt32(await tablesCmd.ExecuteScalarAsync());
            
            // Routines (Procedures + Functions)
            var routinesSql = "SELECT COUNT(*) FROM SYSCAT.ROUTINEAUTH WHERE GRANTEE = ? AND GRANTEETYPE = 'G'";
            using var routinesCmd = _connectionManager.CreateCommand(routinesSql);
            routinesCmd.Parameters.Add(new DB2Parameter("@groupName", groupName));
            var routineCount = Convert.ToInt32(await routinesCmd.ExecuteScalarAsync());
            counts[PrivilegeCategoryType.Procedures] = routineCount; // Approximate
            counts[PrivilegeCategoryType.Functions] = routineCount;
            
            // Schemas
            var schemasSql = "SELECT COUNT(*) FROM SYSCAT.SCHEMAAUTH WHERE GRANTEE = ? AND GRANTEETYPE = 'G'";
            using var schemasCmd = _connectionManager.CreateCommand(schemasSql);
            schemasCmd.Parameters.Add(new DB2Parameter("@groupName", groupName));
            counts[PrivilegeCategoryType.Schemas] = Convert.ToInt32(await schemasCmd.ExecuteScalarAsync());
            
            // Sequences
            var sequencesSql = "SELECT COUNT(*) FROM SYSCAT.SEQUENCEAUTH WHERE GRANTEE = ? AND GRANTEETYPE = 'G'";
            using var sequencesCmd = _connectionManager.CreateCommand(sequencesSql);
            sequencesCmd.Parameters.Add(new DB2Parameter("@groupName", groupName));
            counts[PrivilegeCategoryType.Sequences] = Convert.ToInt32(await sequencesCmd.ExecuteScalarAsync());
            
            // Fill remaining categories with 0 for now
            foreach (var category in Enum.GetValues<PrivilegeCategoryType>())
            {
                if (!counts.ContainsKey(category))
                {
                    counts[category] = 0;
                }
            }
            
            return counts;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get privilege counts for group {Group}", groupName);
            return new Dictionary<PrivilegeCategoryType, int>();
        }
    }
    
    /// <summary>
    /// Get privilege counts for a user (all 15 categories)
    /// </summary>
    public async Task<Dictionary<PrivilegeCategoryType, int>> GetUserPrivilegeCountsAsync(string userName)
    {
        Logger.Debug("Getting privilege counts for user: {User}", userName);
        var counts = new Dictionary<PrivilegeCategoryType, int>();
        
        try
        {
            // Tables
            var tablesSql = "SELECT COUNT(*) FROM SYSCAT.TABAUTH WHERE GRANTEE = ? AND GRANTEETYPE = 'U'";
            using var tablesCmd = _connectionManager.CreateCommand(tablesSql);
            tablesCmd.Parameters.Add(new DB2Parameter("@userName", userName));
            counts[PrivilegeCategoryType.Tables] = Convert.ToInt32(await tablesCmd.ExecuteScalarAsync());
            
            // Views (subset of tables)
            counts[PrivilegeCategoryType.Views] = counts[PrivilegeCategoryType.Tables] / 2; // Approximate
            
            // Columns
            var columnsSql = "SELECT COUNT(*) FROM SYSCAT.COLAUTH WHERE GRANTEE = ? AND GRANTEETYPE = 'U'";
            using var columnsCmd = _connectionManager.CreateCommand(columnsSql);
            columnsCmd.Parameters.Add(new DB2Parameter("@userName", userName));
            counts[PrivilegeCategoryType.Columns] = Convert.ToInt32(await columnsCmd.ExecuteScalarAsync());
            
            // Routines (Procedures + Functions)
            var routinesSql = "SELECT COUNT(*) FROM SYSCAT.ROUTINEAUTH WHERE GRANTEE = ? AND GRANTEETYPE = 'U'";
            using var routinesCmd = _connectionManager.CreateCommand(routinesSql);
            routinesCmd.Parameters.Add(new DB2Parameter("@userName", userName));
            var routineCount = Convert.ToInt32(await routinesCmd.ExecuteScalarAsync());
            counts[PrivilegeCategoryType.Procedures] = routineCount / 2; // Approximate
            counts[PrivilegeCategoryType.Functions] = routineCount / 2;
            
            // Schemas
            var schemasSql = "SELECT COUNT(*) FROM SYSCAT.SCHEMAAUTH WHERE GRANTEE = ? AND GRANTEETYPE = 'U'";
            using var schemasCmd = _connectionManager.CreateCommand(schemasSql);
            schemasCmd.Parameters.Add(new DB2Parameter("@userName", userName));
            counts[PrivilegeCategoryType.Schemas] = Convert.ToInt32(await schemasCmd.ExecuteScalarAsync());
            
            // Sequences
            var sequencesSql = "SELECT COUNT(*) FROM SYSCAT.SEQUENCEAUTH WHERE GRANTEE = ? AND GRANTEETYPE = 'U'";
            using var sequencesCmd = _connectionManager.CreateCommand(sequencesSql);
            sequencesCmd.Parameters.Add(new DB2Parameter("@userName", userName));
            counts[PrivilegeCategoryType.Sequences] = Convert.ToInt32(await sequencesCmd.ExecuteScalarAsync());
            
            // Tablespaces
            var tablespacesSql = "SELECT COUNT(*) FROM SYSCAT.TBSPACEAUTH WHERE GRANTEE = ? AND GRANTEETYPE = 'U'";
            using var tablespacesCmd = _connectionManager.CreateCommand(tablespacesSql);
            tablespacesCmd.Parameters.Add(new DB2Parameter("@userName", userName));
            counts[PrivilegeCategoryType.Tablespaces] = Convert.ToInt32(await tablespacesCmd.ExecuteScalarAsync());
            
            // Packages
            var packagesSql = "SELECT COUNT(*) FROM SYSCAT.PACKAGEAUTH WHERE GRANTEE = ? AND GRANTEETYPE = 'U'";
            using var packagesCmd = _connectionManager.CreateCommand(packagesSql);
            packagesCmd.Parameters.Add(new DB2Parameter("@userName", userName));
            counts[PrivilegeCategoryType.Packages] = Convert.ToInt32(await packagesCmd.ExecuteScalarAsync());
            
            // Indexes
            var indexesSql = "SELECT COUNT(*) FROM SYSCAT.INDEXAUTH WHERE GRANTEE = ? AND GRANTEETYPE = 'U'";
            using var indexesCmd = _connectionManager.CreateCommand(indexesSql);
            indexesCmd.Parameters.Add(new DB2Parameter("@userName", userName));
            counts[PrivilegeCategoryType.Indexes] = Convert.ToInt32(await indexesCmd.ExecuteScalarAsync());
            
            // Fill remaining categories with 0 for now
            foreach (var category in Enum.GetValues<PrivilegeCategoryType>())
            {
                if (!counts.ContainsKey(category))
                {
                    counts[category] = 0;
                }
            }
            
            return counts;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to get privilege counts for user {User}", userName);
            return new Dictionary<PrivilegeCategoryType, int>();
        }
    }
    
    #endregion
    
    #region Indexes
    
    /// <summary>
    /// Get indexes in a schema
    /// </summary>
    public async Task<List<DatabaseObject>> GetIndexesAsync(string schemaName)
    {
        Logger.Debug("Loading indexes for schema: {Schema}", schemaName);
        
        try
        {
            var sql = @"
                SELECT 
                    TRIM(INDNAME),
                    TRIM(TABSCHEMA),
                    TRIM(TABNAME),
                    TRIM(UNIQUERULE),
                    TRIM(INDEXTYPE),
                    CREATE_TIME,
                    TRIM(OWNER),
                    TRIM(REMARKS)
                FROM SYSCAT.INDEXES
                WHERE TRIM(TABSCHEMA) = ?
                ORDER BY INDNAME";
            
            var indexes = new List<DatabaseObject>();
            using var command = _connectionManager.CreateCommand(sql);
            command.Parameters.Add(new DB2Parameter("@schemaName", schemaName));
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                var uniqueRule = reader.GetString(3);
                var icon = uniqueRule == "P" ? ObjectBrowserIcons.PrimaryKey : ObjectBrowserIcons.Index;
                
                indexes.Add(new DatabaseObject
                {
                    Name = reader.GetString(0),
                    SchemaName = schemaName,
                    FullName = $"{schemaName}.{reader.GetString(0)}",
                    Type = ObjectType.Indexes,
                    Icon = icon,
                    TableSpace = $"{reader.GetString(1)}.{reader.GetString(2)}", // Store table reference
                    CreatedAt = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                    Owner = reader.GetString(6),
                    Remarks = reader.IsDBNull(7) ? string.Empty : reader.GetString(7)
                });
            }
            
            Logger.Info("Loaded {Count} indexes for schema {Schema}", indexes.Count, schemaName);
            return indexes;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load indexes for schema {Schema}", schemaName);
            return new List<DatabaseObject>();
        }
    }
    
    #endregion
    
    #region Triggers
    
    /// <summary>
    /// Get triggers in a schema
    /// </summary>
    public async Task<List<DatabaseObject>> GetTriggersAsync(string schemaName)
    {
        Logger.Debug("Loading triggers for schema: {Schema}", schemaName);
        
        try
        {
            var sql = @"
                SELECT 
                    TRIM(TRIGNAME),
                    TRIM(TABSCHEMA),
                    TRIM(TABNAME),
                    TRIM(TRIGEVENT),
                    TRIM(TRIGTIME),
                    CREATE_TIME,
                    TRIM(OWNER),
                    TRIM(REMARKS)
                FROM SYSCAT.TRIGGERS
                WHERE TRIM(TRIGSCHEMA) = ?
                ORDER BY TRIGNAME";
            
            var triggers = new List<DatabaseObject>();
            using var command = _connectionManager.CreateCommand(sql);
            command.Parameters.Add(new DB2Parameter("@schemaName", schemaName));
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                triggers.Add(new DatabaseObject
                {
                    Name = reader.GetString(0),
                    SchemaName = schemaName,
                    FullName = $"{schemaName}.{reader.GetString(0)}",
                    Type = ObjectType.Triggers,
                    Icon = ObjectBrowserIcons.Trigger,
                    TableSpace = $"{reader.GetString(1)}.{reader.GetString(2)}", // Store table reference
                    CreatedAt = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                    Owner = reader.GetString(6),
                    Remarks = reader.IsDBNull(7) ? string.Empty : reader.GetString(7)
                });
            }
            
            Logger.Info("Loaded {Count} triggers for schema {Schema}", triggers.Count, schemaName);
            return triggers;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load triggers for schema {Schema}", schemaName);
            return new List<DatabaseObject>();
        }
    }
    
    #endregion
    
    #region Sequences
    
    /// <summary>
    /// Get sequences in a schema
    /// </summary>
    public async Task<List<DatabaseObject>> GetSequencesAsync(string schemaName)
    {
        Logger.Debug("Loading sequences for schema: {Schema}", schemaName);
        
        try
        {
            var sql = @"
                SELECT 
                    TRIM(SEQNAME),
                    TRIM(SEQTYPE),
                    START,
                    INCREMENT,
                    TRIM(OWNER),
                    CREATE_TIME,
                    TRIM(REMARKS)
                FROM SYSCAT.SEQUENCES
                WHERE TRIM(SEQSCHEMA) = ?
                ORDER BY SEQNAME";
            
            var sequences = new List<DatabaseObject>();
            using var command = _connectionManager.CreateCommand(sql);
            command.Parameters.Add(new DB2Parameter("@schemaName", schemaName));
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                sequences.Add(new DatabaseObject
                {
                    Name = reader.GetString(0),
                    SchemaName = schemaName,
                    FullName = $"{schemaName}.{reader.GetString(0)}",
                    Type = ObjectType.Sequences,
                    Icon = ObjectBrowserIcons.Sequence,
                    Owner = reader.GetString(4),
                    CreatedAt = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                    Remarks = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                });
            }
            
            Logger.Info("Loaded {Count} sequences for schema {Schema}", sequences.Count, schemaName);
            return sequences;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load sequences for schema {Schema}", schemaName);
            return new List<DatabaseObject>();
        }
    }
    
    #endregion
    
    #region Synonyms
    
    /// <summary>
    /// Get synonyms (aliases) in a schema
    /// </summary>
    public async Task<List<DatabaseObject>> GetSynonymsAsync(string schemaName)
    {
        Logger.Debug("Loading synonyms for schema: {Schema}", schemaName);
        
        try
        {
            var sql = @"
                SELECT 
                    TRIM(TABNAME) AS ALIAS_NAME,
                    TRIM(BASE_TABSCHEMA),
                    TRIM(BASE_TABNAME),
                    TRIM(OWNER),
                    CREATE_TIME,
                    TRIM(REMARKS)
                FROM SYSCAT.TABLES
                WHERE TRIM(TABSCHEMA) = ?
                  AND TYPE = 'A'
                ORDER BY TABNAME";
            
            var synonyms = new List<DatabaseObject>();
            using var command = _connectionManager.CreateCommand(sql);
            command.Parameters.Add(new DB2Parameter("@schemaName", schemaName));
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                synonyms.Add(new DatabaseObject
                {
                    Name = reader.GetString(0),
                    SchemaName = schemaName,
                    FullName = $"{schemaName}.{reader.GetString(0)}",
                    Type = ObjectType.Synonyms,
                    Icon = ObjectBrowserIcons.Synonym,
                    TableSpace = $"{reader.GetString(1)}.{reader.GetString(2)}", // Store target reference
                    Owner = reader.GetString(3),
                    CreatedAt = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                    Remarks = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                });
            }
            
            Logger.Info("Loaded {Count} synonyms for schema {Schema}", synonyms.Count, schemaName);
            return synonyms;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load synonyms for schema {Schema}", schemaName);
            return new List<DatabaseObject>();
        }
    }
    
    #endregion
    
    #region Types
    
    /// <summary>
    /// Get user-defined types in a schema
    /// </summary>
    public async Task<List<DatabaseObject>> GetTypesAsync(string schemaName)
    {
        Logger.Debug("Loading types for schema: {Schema}", schemaName);
        
        try
        {
            var sql = @"
                SELECT 
                    TRIM(TYPENAME),
                    TRIM(METATYPE),
                    TRIM(OWNER),
                    CREATE_TIME,
                    TRIM(REMARKS)
                FROM SYSCAT.DATATYPES
                WHERE TRIM(TYPESCHEMA) = ?
                  AND METATYPE IN ('A', 'D', 'R', 'S')
                ORDER BY TYPENAME";
            
            var types = new List<DatabaseObject>();
            using var command = _connectionManager.CreateCommand(sql);
            command.Parameters.Add(new DB2Parameter("@schemaName", schemaName));
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                types.Add(new DatabaseObject
                {
                    Name = reader.GetString(0),
                    SchemaName = schemaName,
                    FullName = $"{schemaName}.{reader.GetString(0)}",
                    Type = ObjectType.Types,
                    Icon = ObjectBrowserIcons.Type,
                    Owner = reader.GetString(2),
                    CreatedAt = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                    Remarks = reader.IsDBNull(4) ? string.Empty : reader.GetString(4)
                });
            }
            
            Logger.Info("Loaded {Count} types for schema {Schema}", types.Count, schemaName);
            return types;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load types for schema {Schema}", schemaName);
            return new List<DatabaseObject>();
        }
    }
    
    #endregion
    
    #region Packages
    
    /// <summary>
    /// Get all packages (top-level category)
    /// </summary>
    public async Task<List<PackageInfo>> GetPackagesAsync()
    {
        Logger.Debug("Loading all packages");
        
        try
        {
            var sql = @"
                SELECT 
                    TRIM(PKGSCHEMA),
                    TRIM(PKGNAME),
                    TRIM(BOUNDBY),
                    TRIM(OWNER),
                    TRIM(ISOLATION),
                    CREATE_TIME,
                    TRIM(REMARKS)
                FROM SYSCAT.PACKAGES
                ORDER BY PKGSCHEMA, PKGNAME";
            
            var packages = new List<PackageInfo>();
            using var command = _connectionManager.CreateCommand(sql);
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                packages.Add(new PackageInfo
                {
                    PackageSchema = reader.GetString(0),
                    PackageName = reader.GetString(1),
                    BoundBy = reader.GetString(2),
                    Owner = reader.GetString(3),
                    Isolation = reader.GetString(4),
                    CreateTime = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
                    Remarks = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                });
            }
            
            Logger.Info("Loaded {Count} packages", packages.Count);
            return packages;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load packages");
            return new List<PackageInfo>();
        }
    }
    
    /// <summary>
    /// Get packages in a schema
    /// </summary>
    public async Task<List<DatabaseObject>> GetPackagesForSchemaAsync(string schemaName)
    {
        Logger.Debug("Loading packages for schema: {Schema}", schemaName);
        
        try
        {
            var sql = @"
                SELECT 
                    TRIM(PKGNAME),
                    TRIM(BOUNDBY),
                    TRIM(OWNER),
                    TRIM(ISOLATION),
                    CREATE_TIME,
                    TRIM(REMARKS)
                FROM SYSCAT.PACKAGES
                WHERE TRIM(PKGSCHEMA) = ?
                ORDER BY PKGNAME";
            
            var packages = new List<DatabaseObject>();
            using var command = _connectionManager.CreateCommand(sql);
            command.Parameters.Add(new DB2Parameter("@schemaName", schemaName));
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                packages.Add(new DatabaseObject
                {
                    Name = reader.GetString(0),
                    SchemaName = schemaName,
                    FullName = $"{schemaName}.{reader.GetString(0)}",
                    Type = ObjectType.Packages,
                    Icon = ObjectBrowserIcons.Package,
                    Owner = reader.GetString(2),
                    CreatedAt = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                    Remarks = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                });
            }
            
            Logger.Info("Loaded {Count} packages for schema {Schema}", packages.Count, schemaName);
            return packages;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Failed to load packages for schema {Schema}", schemaName);
            return new List<DatabaseObject>();
        }
    }
    
    #endregion
}

