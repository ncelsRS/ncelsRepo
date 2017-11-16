CREATE TABLE [dbo].[PermissionRole] (
  [Id] int IDENTITY(1, 1) NOT NULL,
  [Name] nvarchar(500) COLLATE Cyrillic_General_CI_AS NOT NULL,
  CONSTRAINT [PK_PermissionRole] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[PermissionKeys]
ALTER COLUMN [Key] nvarchar(1000) COLLATE Cyrillic_General_CI_AS
GO

CREATE TABLE [dbo].[EmployeePermissionRoles] (
  [Id] int IDENTITY(1, 1) NOT NULL,
  [EmployeeId] uniqueidentifier NOT NULL,
  [PermissionRoleId] int NOT NULL,
  CONSTRAINT [PK_EmployeePermissionRoles] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE TABLE [dbo].[PermissionRoleKeys] (
  [Id] int IDENTITY(1, 1) NOT NULL,
  [PermissionRoleId] int NULL,
  [PermissionKey] nvarchar(1000) COLLATE Cyrillic_General_CI_AS NULL,
  [PermissionValue] nvarchar(1000) COLLATE Cyrillic_General_CI_AS NULL,
  CONSTRAINT [PK_PermissionRoleKeys] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

INSERT INTO dbo.PermissionRole(Name)
SELECT E.Id
FROM dbo.Employees AS E


INSERT INTO dbo.EmployeePermissionRoles (EmployeeId, PermissionRoleId)
SELECT E.Id, R.Id
FROM dbo.Employees AS E
	INNER JOIN dbo.PermissionRole AS R ON R.[Name] = E.Id
    
INSERT INTO dbo.PermissionRoleKeys (PermissionRoleId, PermissionKey, PermissionValue)
SELECT R.Id, EP.PermissionKey, EP.PermissionValue
FROM dbo.EmployeePermissions AS EP
	INNER JOIN dbo.PermissionRole AS R ON R.[Name] = EP.EmployeeId
    
UPDATE dbo.PermissionRole
SET Name = 'Персональная роль - ' + ISNULL(E.LastName,'') + ' '+ ISNULL(E.FirstName,'') + ' '+ ISNULL(E.MiddleName,'')
FROM dbo.PermissionRole AS R
	INNER JOIN dbo.Employees AS E ON E.Id = R.[Name]
    
    
--check success operation
SELECT OldAccess.*
FROM 
  (    
      SELECT EP.EmployeeId, EP.PermissionKey, EP.PermissionValue
      FROM dbo.EmployeePermissions AS EP
  ) AS OldAccess
  LEFT JOIN
    (
        SELECT ER.EmployeeId, PK.PermissionKey, PK.PermissionValue
        FROM dbo.EmployeePermissionRoles AS ER
            INNER JOIN dbo.PermissionRoleKeys AS PK ON PK.PermissionRoleId = ER.PermissionRoleId
    ) AS NewAccess ON OldAccess.EmployeeId = NewAccess.EmployeeId
    	AND OldAccess.PermissionKey = NewAccess.PermissionKey
    	AND OldAccess.PermissionValue = NewAccess.PermissionValue
WHERE NewAccess.PermissionKey IS NULL
--check success operation end 

DROP TRIGGER [dbo].[delete_permissionkeys]
GO

DROP TRIGGER [dbo].[insert_permissionkeys]
GO

DROP TRIGGER [dbo].[insert_employees]
GO

DROP TRIGGER [dbo].[delete_employees]
GO
--add admin access to role
UPDATE dbo.PermissionRoleKeys
SET PermissionValue = 'true'
WHERE PermissionRoleId IN
(
	SELECT R.PermissionRoleId
	FROM dbo.EmployeePermissionRoles AS R
	WHERE R.EmployeeId = '2b2a86f1-4e48-4a84-81a9-8221103d1b62' -- admin
)
	AND PermissionKey = 'IsMenuPermissionRoleListVisibility'