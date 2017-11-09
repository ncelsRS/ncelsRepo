EXEC sp_rename '[dbo].[CommissionQuestions]', 'CommissionDrugDeclarations', 'OBJECT'
GO
EXEC sp_rename '[dbo].[CommissionQuestions_CommissionId_fk]', 'CommissionDrugDeclarations_CommissionId_fk', 'OBJECT'
GO

EXEC sp_rename '[dbo].[PK_CommissionQuestions]', 'PK_CommissionDrugDeclarations', 'OBJECT'
GO

ALTER TABLE [dbo].[CommissionDrugDeclarations]
DROP CONSTRAINT [PK_CommissionDrugDeclarations]
GO

ALTER TABLE [dbo].[CommissionDrugDeclarations]
ADD CONSTRAINT [PK_CommissionDrugDeclarations] 
PRIMARY KEY CLUSTERED ([Id])
WITH (
  PAD_INDEX = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

CREATE TABLE [dbo].[CommissionQuestions] (
  [Id] int IDENTITY(1, 1) NOT NULL,
  [CommissionId] int NOT NULL,
  [Number] int NOT NULL,
  [TypeId] int NOT NULL,
  [Comment] nvarchar(max) COLLATE Cyrillic_General_CI_AS NULL,
  CONSTRAINT [CommissionQuestions_uq] UNIQUE ([CommissionId], [TypeId]),
  CONSTRAINT [PK_CommissionQuestions] PRIMARY KEY CLUSTERED ([Id]),
  CONSTRAINT [CommissionQuestions_CommissionId_fk] FOREIGN KEY ([CommissionId]) 
  REFERENCES [dbo].[Commissions] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
)
ON [PRIMARY]
GO

CREATE TABLE [dbo].[CommissionQuestionTypes] (
  [Id] int NOT NULL,
  [Name] nvarchar(200) COLLATE Cyrillic_General_CI_AS NULL,
  CONSTRAINT [PK_CommissionQuestionTypes] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO

ALTER TABLE [dbo].[CommissionQuestions]
ADD CONSTRAINT [CommissionQuestions_TypeId_fk] FOREIGN KEY ([TypeId]) 
  REFERENCES [dbo].[CommissionQuestionTypes] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

INSERT INTO dbo.CommissionQuestionTypes(Id,Name)
VALUES(1,'Рассмотрение заявок')
GO

INSERT INTO dbo.CommissionQuestionTypes(Id,Name)
VALUES(2,'Рассмотрение материалов клинических испытаний ЛС')
GO

INSERT INTO dbo.CommissionQuestionTypes(Id,Name)
VALUES(3,'Прочее')
GO

CREATE TABLE [dbo].[CommissionDrugDeclarationConclusionTypes] (
  [Id] int NOT NULL,
  [Name] nvarchar(200) COLLATE Cyrillic_General_CI_AS NOT NULL,
  CONSTRAINT [PK_CommissionDrugDeclarationConclusionTypes] PRIMARY KEY CLUSTERED ([Id])
)
ON [PRIMARY]
GO


INSERT INTO dbo.CommissionDrugDeclarationConclusionTypes(Id,Name)
VALUES(1, 'Рекомендовано')
GO

INSERT INTO dbo.CommissionDrugDeclarationConclusionTypes(Id,Name)
VALUES(2, 'Отказно')
GO

INSERT INTO dbo.CommissionDrugDeclarationConclusionTypes(Id,Name)
VALUES(3, 'Направлено на дополнительное рассмотрение')
GO

INSERT INTO dbo.CommissionDrugDeclarationConclusionTypes(Id,Name)
VALUES(4, 'Снято с регистрации заявителем')
GO

ALTER TABLE [dbo].[CommissionDrugDeclarations]
ADD [ConclusionTypeId] int NULL
GO

ALTER TABLE [dbo].[CommissionDrugDeclarations]
ADD [ConclusionComment] nvarchar(max) NULL
GO

ALTER TABLE [dbo].[CommissionDrugDeclarations]
ADD CONSTRAINT [CommissionDrugDeclarations_ConclusionTypeId_fk] FOREIGN KEY ([ConclusionTypeId]) 
  REFERENCES [dbo].[CommissionDrugDeclarationConclusionTypes] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

