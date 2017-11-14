CREATE TABLE [dbo].[EXP_ExpertiseStageDosageResult] (
  [Id] int IDENTITY(1, 1) NOT NULL,
  [StageDosageId] uniqueidentifier NOT NULL,
  [ResultId] int NULL,
  [ResultDate] datetime NULL,
  [ResultCreatorId] uniqueidentifier NULL,
  [CommissionId] int NULL,
  CONSTRAINT [pk_EXP_ExpertiseStageDosageResult] PRIMARY KEY CLUSTERED ([Id]),
  CONSTRAINT [EXP_ExpertiseStageDosageResult_CommissionId_fk] FOREIGN KEY ([CommissionId]) 
  REFERENCES [dbo].[Commissions] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION,
  CONSTRAINT [EXP_ExpertiseStageDosageResult_ResultId_fk] FOREIGN KEY ([ResultId]) 
  REFERENCES [dbo].[EXP_DIC_StageResult] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION,
  CONSTRAINT [EXP_ExpertiseStageDosageResult_StageDosageId_fk] FOREIGN KEY ([StageDosageId]) 
  REFERENCES [dbo].[EXP_ExpertiseStageDosage] ([Id]) 
  ON UPDATE CASCADE
  ON DELETE CASCADE
)
ON [PRIMARY]
GO

EXEC sp_addextendedproperty 'MS_Description', N'этап заявки', 'schema', 'dbo', 'table', 'EXP_ExpertiseStageDosageResult', 'column', 'StageDosageId'
GO

EXEC sp_addextendedproperty 'MS_Description', N'значение', 'schema', 'dbo', 'table', 'EXP_ExpertiseStageDosageResult', 'column', 'ResultId'
GO

EXEC sp_addextendedproperty 'MS_Description', N'дата выставления', 'schema', 'dbo', 'table', 'EXP_ExpertiseStageDosageResult', 'column', 'ResultDate'
GO

EXEC sp_addextendedproperty 'MS_Description', N'создатель', 'schema', 'dbo', 'table', 'EXP_ExpertiseStageDosageResult', 'column', 'ResultCreatorId'
GO

EXEC sp_addextendedproperty 'MS_Description', N'заполняется если результат был поставлен коммиссией', 'schema', 'dbo', 'table', 'EXP_ExpertiseStageDosageResult', 'column', 'CommissionId'
GO