CREATE TABLE [dbo].[VisitTypes] (
  [Id] int IDENTITY(1, 1) NOT NULL,
  [Name] nvarchar(4000) COLLATE Cyrillic_General_CI_AS NOT NULL,
  [Group] nvarchar(4000) COLLATE Cyrillic_General_CI_AS NULL,
  [Time] int NOT NULL,
  CONSTRAINT [PK_VisitTypes] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

INSERT INTO dbo.VisitTypes(Name, [Group], Time)
VALUES('Платные консультации по договору', 'Общее', 15)

INSERT INTO dbo.VisitTypes(Name, [Group], Time)
VALUES('Консультация по регистрации Договора', 'Экспертиза', 15)
INSERT INTO dbo.VisitTypes(Name, [Group], Time)
VALUES('Регистрация договора', 'Экспертиза', 15)
INSERT INTO dbo.VisitTypes(Name, [Group], Time)
VALUES('Регистрация Заявления', 'Экспертиза', 15)

INSERT INTO dbo.VisitTypes(Name, [Group], Time)
VALUES('Консультация по регистрации Договора', 'ОБК', 15)
INSERT INTO dbo.VisitTypes(Name, [Group], Time)
VALUES('Регистрация договора', 'ОБК', 15)
INSERT INTO dbo.VisitTypes(Name, [Group], Time)
VALUES('Регистрация Заявления', 'ОБК', 15)

CREATE TABLE [dbo].[VisitEmployeeWorkingTimes] (
  [Id] int IDENTITY(1, 1) NOT NULL,
  [EmployeeId] uniqueidentifier NOT NULL,
  [Date] date NOT NULL,
  [TimeBegin] int NOT NULL,
  [TimeEnd] int NOT NULL,
  CONSTRAINT [PK_VisitEmployeeWorkingTimes] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

CREATE TABLE [dbo].[VisitEmployeeTypes] (
  [Id] int IDENTITY(1, 1) NOT NULL,
  [EmployeeId] uniqueidentifier NOT NULL,
  [VisitTypeId] int NOT NULL,
  [IsEnable] bit NOT NULL,
  CONSTRAINT [PK_VisitEmployeeTypes] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[VisitEmployeeTypes]
ADD CONSTRAINT [VisitEmployeeTypes_VisitTypeId_fk] FOREIGN KEY ([VisitTypeId]) 
  REFERENCES [dbo].[VisitTypes] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE [dbo].[VisitEmployeeWorkingTimes]
ADD CONSTRAINT [VisitEmployeeWorkingTimes_EmployeeId_fk] FOREIGN KEY ([EmployeeId]) 
  REFERENCES [dbo].[Employees] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO


CREATE TABLE [dbo].[VisitStatuses] (
  [Id] int NOT NULL,
  [Name] nvarchar(200) COLLATE Cyrillic_General_CI_AS NOT NULL,
  CONSTRAINT [PK_VisitStatuses] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

INSERT INTO dbo.VisitStatuses
VALUES(1, 'Ожидает подтверждения')

INSERT INTO dbo.VisitStatuses
VALUES(2, 'Запланировано')

INSERT INTO dbo.VisitStatuses
VALUES(3, 'Никто не пришёл')

INSERT INTO dbo.VisitStatuses
VALUES(4, 'Выполнено')
GO

CREATE TABLE [dbo].[Visits] (
  [Id] int IDENTITY(1, 1) NOT NULL,
  [VisitTypeId] int NOT NULL,
  [VisitorId] uniqueidentifier NOT NULL,
  [EmployeeId] uniqueidentifier NOT NULL,
  [Date] date NOT NULL,
  [TimeBegin] int NOT NULL,
  [Duration] int NOT NULL,
  [Comment] nvarchar(4000) COLLATE Cyrillic_General_CI_AS NULL,
  [VisitStatusId] int NOT NULL,
  CONSTRAINT [PK_Visits] PRIMARY KEY CLUSTERED ([Id]),
  CONSTRAINT [Visits_EmployeeId_fk] FOREIGN KEY ([EmployeeId]) 
  REFERENCES [dbo].[Employees] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION,
  CONSTRAINT [Visits_StatusId_fk] FOREIGN KEY ([VisitStatusId]) 
  REFERENCES [dbo].[VisitStatuses] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION,
  CONSTRAINT [Visits_TypeId_fk] FOREIGN KEY ([VisitTypeId]) 
  REFERENCES [dbo].[VisitTypes] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION,
  CONSTRAINT [Visits_VisitorId_fk] FOREIGN KEY ([VisitorId]) 
  REFERENCES [dbo].[Employees] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
)
ON [PRIMARY]
GO

EXEC sp_addextendedproperty 'MS_Description', N'Тип приёма', 'schema', 'dbo', 'table', 'Visits', 'column', 'VisitTypeId'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Тот кто пришёл на приём', 'schema', 'dbo', 'table', 'Visits', 'column', 'VisitorId'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Тот кто принимает гостя', 'schema', 'dbo', 'table', 'Visits', 'column', 'EmployeeId'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Дата приёма', 'schema', 'dbo', 'table', 'Visits', 'column', 'Date'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Время начала (в минутах от 00:00)', 'schema', 'dbo', 'table', 'Visits', 'column', 'TimeBegin'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Время приёма в минутах', 'schema', 'dbo', 'table', 'Visits', 'column', 'Duration'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Краткое описание вопроса', 'schema', 'dbo', 'table', 'Visits', 'column', 'Comment'
GO

EXEC sp_addextendedproperty 'MS_Description', N'статус приёма в коде будет Enum VisitStatuses', 'schema', 'dbo', 'table', 'Visits', 'column', 'VisitStatusId'
GO
CREATE VIEW dbo.VisitsView
	AS
SELECT V.Id AS VisitId
	,V.[Date] AS VisitDate
    ,V.Duration AS VisitDuration
    ,V.TimeBegin AS VisitTimeBegin
    ,V.Comment AS VisitComment
    ,V.VisitStatusId AS VisitStatusId
    ,VS.[Name] AS VisitStatusName
    ,V.VisitTypeId AS VisitTypeId
    ,VT.[Name] AS VisitTypeName
    ,VT.[Group] AS VisitTypeGroup     
    ,V.VisitorId aS VisitorId 
    ,VV.LastName AS VisitorLastName
    ,VV.FirstName AS VisitorFirstName
    ,VV.MiddleName AS VisitorMiddleName  
    ,V.EmployeeId AS EmployeeId
    ,VE.LastName AS EmployeeLastName
    ,VE.FirstName AS EmployeeFirstName
    ,VE.MiddleName AS EmployeeMiddleName    
FROM dbo.Visits AS V
	INNER JOIN dbo.Employees AS VV ON VV.Id = V.VisitorId
    INNER JOIN dbo.Employees AS VE ON VE.Id = V.EmployeeId
    INNER JOIN dbo.VisitTypes AS VT ON VT.Id = V.VisitTypeId
    INNER JOIN dbo.VisitStatuses AS VS ON VS.Id = V.VisitStatusId