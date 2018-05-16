CREATE TABLE [dbo].[Commissions] (
  [Id] int IDENTITY(1, 1) NOT NULL,
  [Number] int NOT NULL,
  [FullNumber] nvarchar(100) COLLATE Cyrillic_General_CI_AS NOT NULL,
  [KindId] int NOT NULL,
  [TypeId] int NOT NULL,
  [Date] datetime NOT NULL,
  [IsComplete] bit NOT NULL,
  [Comment] nvarchar(4000) COLLATE Cyrillic_General_CI_AS NULL,
  CONSTRAINT [PK_Commissions] PRIMARY KEY CLUSTERED ([Id]),
  CONSTRAINT [UQ_Commissions_Number] UNIQUE ([Number])
)
ON [PRIMARY]
GO

CREATE TABLE [dbo].[CommissionUnitTypes] (
  [Id] int NOT NULL,
  [Name] nvarchar(300) COLLATE Cyrillic_General_CI_AS NOT NULL,
  [MaxCount] int NULL,
  CONSTRAINT [PK_CommissionsUnitTypes] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

INSERT INTO dbo.CommissionUnitTypes(Id,Name,MaxCount)
VALUES(1,'Председатель',1)
GO

INSERT INTO dbo.CommissionUnitTypes(Id,Name,MaxCount)
VALUES(2,'Секретарь',1)
GO

INSERT INTO dbo.CommissionUnitTypes(Id,Name)
VALUES(3,'Член коммиссии')
GO

CREATE TABLE [dbo].[CommissionUnits] (
  [Id] int IDENTITY(1, 1) NOT NULL,
  [CommissionId] int NOT NULL,
  [EmployeeId] uniqueidentifier NOT NULL,
  [UnitTypeId] int NOT NULL,
  CONSTRAINT [PK_CommissionUnits] PRIMARY KEY CLUSTERED ([Id]),
  CONSTRAINT [CommissionUnits_CommissionId_fk] FOREIGN KEY ([CommissionId]) 
  REFERENCES [dbo].[Commissions] ([Id]) 
  ON UPDATE CASCADE
  ON DELETE CASCADE,
  CONSTRAINT [CommissionUnits_EmployeeId_fk] FOREIGN KEY ([EmployeeId]) 
  REFERENCES [dbo].[Employees] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION,
  CONSTRAINT [CommissionUnits_UnitTypeId_fk] FOREIGN KEY ([UnitTypeId]) 
  REFERENCES [dbo].[CommissionUnitTypes] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
)
ON [PRIMARY]
GO

CREATE TABLE [dbo].[CommissionTypes] (
  [Id] int NOT NULL,
  [Name] nvarchar(300) COLLATE Cyrillic_General_CI_AS NOT NULL,
  CONSTRAINT [PK_CommissionTypes] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

INSERT INTO dbo.CommissionTypes(Id,Name)
VALUES(1,'Экспертный совет')
GO

INSERT INTO dbo.CommissionTypes(Id,Name)
VALUES(2,'Фармацевтическая компания')
GO

INSERT INTO dbo.CommissionTypes(Id,Name)
VALUES(3,'Фармакологическая комиссия')
GO

ALTER TABLE [dbo].[Commissions]
ADD CONSTRAINT [Commissions_TypeId_fk] FOREIGN KEY ([TypeId]) 
  REFERENCES [dbo].[CommissionTypes] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

CREATE TABLE [dbo].[CommissionKinds] (
  [Id] int NOT NULL,
  [Name] nvarchar(200) COLLATE Cyrillic_General_CI_AS NOT NULL,
  [ShortName] nvarchar(10) COLLATE Cyrillic_General_CI_AS NOT NULL,
  CONSTRAINT [PK_CommissionKinds] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

INSERT INTO dbo.CommissionKinds(Id,Name,ShortName)
VALUES(1,'Лекарственные средства','ЛС')
GO

INSERT INTO dbo.CommissionKinds(Id,Name,ShortName)
VALUES(2,'Изделия медицинского назначения','ИМН')
GO

ALTER TABLE [dbo].[Commissions]
ADD CONSTRAINT [Commissions_KindId_fk] FOREIGN KEY ([KindId]) 
  REFERENCES [dbo].[CommissionKinds] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

CREATE VIEW dbo.CommissionsView
	AS
SELECT C.Id
	,C.Number
    ,C.FullNumber
    ,C.[Date]
    ,C.IsComplete    
    ,C.Comment
    ,CT.Id AS TypeId
    ,CT.[Name] AS TypeName    
    ,CK.Id AS KindId
    ,CK.[Name] AS KindName    
    ,CK.ShortName AS KindShortName
FROM dbo.Commissions AS C
	INNER JOIN dbo.CommissionTypes AS CT ON CT.Id = C.TypeId
	INNER JOIN dbo.CommissionKinds AS CK ON CK.Id = C.KindId
	
GO

ALTER TABLE [dbo].[CommissionUnits]
ADD CONSTRAINT [CommissionUnits_uq] 
UNIQUE NONCLUSTERED ([CommissionId], [EmployeeId])
WITH (
  PAD_INDEX = OFF,
  IGNORE_DUP_KEY = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
GO

CREATE TABLE [dbo].[CommissionQuestions] (
  [Id] int IDENTITY(1, 1) NOT NULL,
  [CommissionId] int NOT NULL,
  [DrugDeclarationId] uniqueidentifier NOT NULL,
  CONSTRAINT [PK_CommissionQuestions] PRIMARY KEY CLUSTERED ([Id]),
  CONSTRAINT [CommissionQuestions_CommissionId_fk] FOREIGN KEY ([CommissionId]) 
  REFERENCES [dbo].[Commissions] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
)
ON [PRIMARY]
GO