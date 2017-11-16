CREATE TABLE [dbo].[ActionLogs] (
  [Id] int IDENTITY(1, 1) NOT NULL,
  [Date] datetime NOT NULL,
  [Place] int NULL,
  [Text] nvarchar(1000) COLLATE Cyrillic_General_CI_AS NULL,
  [AdditionalText] nvarchar(max) COLLATE Cyrillic_General_CI_AS NULL,
  [EmployeeId] uniqueidentifier NULL,
  CONSTRAINT [PK_ActionLogs] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

EXEC sp_addextendedproperty 'MS_Description', N'1-ext 2-int', 'schema', 'dbo', 'table', 'ActionLogs', 'column', 'Place'
GO

CREATE NONCLUSTERED INDEX [ActionLogs_idx] ON [dbo].[ActionLogs]
  ([Date])
WITH (
  PAD_INDEX = OFF,
  DROP_EXISTING = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  SORT_IN_TEMPDB = OFF,
  ONLINE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
GO

ALTER TABLE [dbo].[ActionLogs]
ADD [Type] int NULL
GO

ALTER TABLE [dbo].[ActionLogs]
ALTER COLUMN [Type] int NOT NULL
GO

EXEC sp_addextendedproperty 'MS_Description', N'now "1" for all', 'schema', 'dbo', 'table', 'ActionLogs', 'column', 'Type'
GO

CREATE TABLE [dbo].[ActionLogPlaces] (
  [Id] int NOT NULL,
  [Name] nvarchar(200) COLLATE Cyrillic_General_CI_AS NULL,
  CONSTRAINT [PK_ActionLogPlaces] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

INSERT INTO dbo.ActionLogPlaces(Id,Name)
VALUES(2, 'Внутренний портал')
GO

INSERT INTO dbo.ActionLogPlaces(Id,Name)
VALUES(1, 'Внешний портал')
GO

EXEC sp_rename '[dbo].[ActionLogs].[Place]', 'PlaceId', 'COLUMN'
GO

CREATE VIEW dbo.ActionLogsView
	AS
SELECT L.Id
	,L.[Date]
    ,L.PlaceId
    ,P.[Name] AS PlaceName
    ,L.[Text]
    ,L.AdditionalText
    ,L.[Type]
    ,L.EmployeeId
    ,E.LastName AS EmployeeLastName
    ,E.FirstName AS EmployeeFirstName
    ,E.MiddleName AS EmployeeMiddleName    
FROM dbo.ActionLogs AS L
	LEFT JOIN dbo.Employees AS E ON E.Id = L.EmployeeId
    LEFT JOIN dbo.ActionLogPlaces AS P ON P.Id = L.PlaceId
