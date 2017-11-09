DROP TABLE [dbo].[EXP_Materials];

CREATE TABLE [dbo].[EXP_Materials](
	[Id] [uniqueidentifier] NOT NULL,
	[RegistrationDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[DeleteDate] [datetime] NULL,
	[Name] [nvarchar](max) NOT NULL,
	[TypeId] [uniqueidentifier] NOT NULL,
	[DrugFormId] [int] NULL,
	[Dosage] [decimal](18, 2) NULL,
	[DosageUnitId] [uniqueidentifier] NULL,
	[DosageQuantity] [int] NULL,
	[Concentration] [nvarchar](512) NULL,
	[Volume] [decimal](18, 2) NULL,
	[VolumeUnitId] [uniqueidentifier] NULL,
	[IsContainNPP] [bit] NOT NULL,
	[ProducerId] [uniqueidentifier] NOT NULL,
	[CountryId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitId] [uniqueidentifier] NOT NULL,
	[Batch] [nvarchar](max) NOT NULL,
	[DateOfManufacture] [datetime] NULL,
	[ExpirationDate] [datetime] NULL,
	[RetestDate] [datetime] NULL,
	[IsCertificatePassport] [bit] NOT NULL,
	[StorageId] [uniqueidentifier] NULL,
	[StorageTemperatureFrom] [decimal](18, 2) NULL,
	[StorageTemperatureTo] [decimal](18, 2) NULL,
	[ActiveSubstancePercent] [decimal](18, 2) NULL,
	[WaterContentPercent] [decimal](18, 2) NULL,
	[DrugDeclarationId] [uniqueidentifier] NOT NULL,
	[IsAdditional] [bit] NOT NULL,
	[StorageConditionId] [uniqueidentifier] NULL,
	[StatusId] [uniqueidentifier] NULL,
	[ExternalStateId] [uniqueidentifier] NULL,
	[ConcordanceStatementId] [uniqueidentifier] NULL,
	[OpeningDate] [datetime] NULL,
	[ExpirationAfterOpeningDate] [datetime] NULL,
 CONSTRAINT [PK_EXP_Materials] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_Materials] ADD  CONSTRAINT [DF_EXP_Materials_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[EXP_Materials] ADD  CONSTRAINT [DF_EXP_Materials_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[EXP_Materials] ADD  CONSTRAINT [DF_EXP_Materials_IsCertificatePassport]  DEFAULT ((1)) FOR [IsCertificatePassport]
GO

ALTER TABLE [dbo].[EXP_Materials] ADD  CONSTRAINT [DF_EXP_Materials_IsAdditional]  DEFAULT ((0)) FOR [IsAdditional]
GO

ALTER TABLE [dbo].[EXP_Materials]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Materials_ConcordanceStatement] FOREIGN KEY([ConcordanceStatementId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_Materials] CHECK CONSTRAINT [FK_EXP_Materials_ConcordanceStatement]
GO

ALTER TABLE [dbo].[EXP_Materials]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Materials_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_Materials] CHECK CONSTRAINT [FK_EXP_Materials_Countries]
GO

ALTER TABLE [dbo].[EXP_Materials]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Materials_DIC_Storages] FOREIGN KEY([StorageId])
REFERENCES [dbo].[DIC_Storages] ([Id])
GO

ALTER TABLE [dbo].[EXP_Materials] CHECK CONSTRAINT [FK_EXP_Materials_DIC_Storages]
GO

ALTER TABLE [dbo].[EXP_Materials]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Materials_DosageUnit] FOREIGN KEY([DosageUnitId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_Materials] CHECK CONSTRAINT [FK_EXP_Materials_DosageUnit]
GO

ALTER TABLE [dbo].[EXP_Materials]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Materials_EXP_DrugDeclaration] FOREIGN KEY([DrugDeclarationId])
REFERENCES [dbo].[EXP_DrugDeclaration] ([Id])
GO

ALTER TABLE [dbo].[EXP_Materials] CHECK CONSTRAINT [FK_EXP_Materials_EXP_DrugDeclaration]
GO

ALTER TABLE [dbo].[EXP_Materials]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Materials_ExternalState] FOREIGN KEY([ExternalStateId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_Materials] CHECK CONSTRAINT [FK_EXP_Materials_ExternalState]
GO

ALTER TABLE [dbo].[EXP_Materials]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Materials_Organizations] FOREIGN KEY([ProducerId])
REFERENCES [dbo].[Organizations] ([Id])
GO

ALTER TABLE [dbo].[EXP_Materials] CHECK CONSTRAINT [FK_EXP_Materials_Organizations]
GO

ALTER TABLE [dbo].[EXP_Materials]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Materials_sr_dosage_forms] FOREIGN KEY([DrugFormId])
REFERENCES [dbo].[sr_dosage_forms] ([id])
GO

ALTER TABLE [dbo].[EXP_Materials] CHECK CONSTRAINT [FK_EXP_Materials_sr_dosage_forms]
GO

ALTER TABLE [dbo].[EXP_Materials]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Materials_Statuses] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_Materials] CHECK CONSTRAINT [FK_EXP_Materials_Statuses]
GO

ALTER TABLE [dbo].[EXP_Materials]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Materials_StorageConditions] FOREIGN KEY([StorageConditionId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_Materials] CHECK CONSTRAINT [FK_EXP_Materials_StorageConditions]
GO

ALTER TABLE [dbo].[EXP_Materials]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Materials_Types] FOREIGN KEY([TypeId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_Materials] CHECK CONSTRAINT [FK_EXP_Materials_Types]
GO

ALTER TABLE [dbo].[EXP_Materials]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Materials_Units] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_Materials] CHECK CONSTRAINT [FK_EXP_Materials_Units]
GO

ALTER TABLE [dbo].[EXP_Materials]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Materials_VolumeUnit] FOREIGN KEY([VolumeUnitId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_Materials] CHECK CONSTRAINT [FK_EXP_Materials_VolumeUnit]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'дата создания' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'CreatedDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Наименование' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Тип' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'TypeId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Лекарственная форма' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'DrugFormId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дозировка' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'Dosage'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Единица измерения дозировки' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'DosageUnitId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Количество доз' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'DosageQuantity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Концентрация' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'Concentration'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Объем' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'Volume'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Объем Единица измерения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'VolumeUnitId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Содержит НПП' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'IsContainNPP'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Страна' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'CountryId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Количество' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'Quantity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Единица измерения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'UnitId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Серия/партия' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'Batch'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата изготовления' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'DateOfManufacture'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Сведения о наличии. По умолчанию включен' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'IsCertificatePassport'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Комната, холодильник, морозильник' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'StorageId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Температура хранения, с' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'StorageTemperatureFrom'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Температура хранения, по' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'StorageTemperatureTo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'% содержания активных веществ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'ActiveSubstancePercent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'% содержания воды' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'WaterContentPercent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Условия хранения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'StorageConditionId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Статус' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'StatusId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Внешнее состояние' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'ExternalStateId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Заключение о соответствии/несоответствии' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'ConcordanceStatementId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата вскрытия' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'OpeningDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата окончания срока хранения после вскрытия' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_Materials', @level2type=N'COLUMN',@level2name=N'ExpirationAfterOpeningDate'
GO


