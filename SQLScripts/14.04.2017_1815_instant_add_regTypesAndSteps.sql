CREATE TABLE dbo.EXP_RegistrationTypes (
  Id uniqueidentifier ROWGUIDCOL NOT NULL,
  Name nvarchar(max) COLLATE Cyrillic_General_CI_AS NULL,
  ParentId uniqueidentifier NULL,
  PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON),
  CONSTRAINT EXP_RegistrationTypes_fk FOREIGN KEY (ParentId) 
  REFERENCES dbo.EXP_RegistrationTypes (Id) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
)
ON [PRIMARY]
GO

EXEC sp_addextendedproperty 'MS_Description', N'Типы регистраций этапов экспертиз', N'schema', N'dbo', N'table', N'EXP_RegistrationTypes'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Идентификатор типа регистрации', N'schema', N'dbo', N'table', N'EXP_RegistrationTypes', N'column', N'Id'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Наименование типа регистрации', N'schema', N'dbo', N'table', N'EXP_RegistrationTypes', N'column', N'Name'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Высший тип регистрации', N'schema', N'dbo', N'table', N'EXP_RegistrationTypes', N'column', N'ParentId'
GO

CREATE TABLE dbo.EXP_RegistrationExpSteps (
  Id uniqueidentifier ROWGUIDCOL NOT NULL,
  Name nvarchar(max) COLLATE Cyrillic_General_CI_AS NOT NULL,
  Duration int NOT NULL,
  RegistrationId uniqueidentifier NOT NULL,
  Priority int NOT NULL,
  PRIMARY KEY CLUSTERED (Id)
    WITH (
      PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF,
      ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON),
  CONSTRAINT EXP_RegistrationExpSteps_fk2 FOREIGN KEY (RegistrationId) 
  REFERENCES dbo.EXP_RegistrationTypes (Id) 
  ON UPDATE NO ACTION
  ON DELETE CASCADE
)
ON [PRIMARY]
GO

EXEC sp_addextendedproperty 'MS_Description', N'Этапы экспертизы', N'schema', N'dbo', N'table', N'EXP_RegistrationExpSteps'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Идентификатор этапа регистрации', N'schema', N'dbo', N'table', N'EXP_RegistrationExpSteps', N'column', N'Id'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Наименование этапа', N'schema', N'dbo', N'table', N'EXP_RegistrationExpSteps', N'column', N'Name'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Длительность в днях', N'schema', N'dbo', N'table', N'EXP_RegistrationExpSteps', N'column', N'Duration'
GO

EXEC sp_addextendedproperty 'MS_Description', N'Приоритет этапа от 1 до бесонечности', N'schema', N'dbo', N'table', N'EXP_RegistrationExpSteps', N'column', N'Priority'
GO
