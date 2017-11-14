ALTER TABLE [dbo].[EXP_DrugDosage]
ADD [SaleTypeId] [int] NULL
GO

ALTER TABLE [dbo].[EXP_DrugDosage]
ADD [BestBefore] [nvarchar](500) NULL
GO
ALTER TABLE [dbo].[EXP_DrugDosage]
ADD [BestBeforeMeasureTypeDicId] [bigint] NULL
GO
ALTER TABLE [dbo].[EXP_DrugDosage]
ADD [AppPeriodOpen] [nvarchar](500) NULL
GO
ALTER TABLE [dbo].[EXP_DrugDosage]
ADD [AppPeriodOpenMeasureDicId] [bigint] NULL
GO
ALTER TABLE [dbo].[EXP_DrugDosage]
ADD [AppPeriodMix] [nvarchar](500) NULL
GO
ALTER TABLE [dbo].[EXP_DrugDosage]
ADD [AppPeriodMixMeasureDicId] [bigint] NULL
GO


ALTER TABLE [dbo].[EXP_DrugDosage]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugDosage_EXP_SaleType] FOREIGN KEY([SaleTypeId])
REFERENCES [dbo].[EXP_DIC_SaleType] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugDosage] CHECK CONSTRAINT [FK_EXP_DrugDosage_EXP_SaleType]
GO

ALTER TABLE [dbo].[EXP_DrugDosage]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugDosage_BestBeforeMeasure] FOREIGN KEY([BestBeforeMeasureTypeDicId])
REFERENCES [dbo].[sr_measures] ([id])
GO

ALTER TABLE [dbo].[EXP_DrugDosage] CHECK CONSTRAINT [FK_EXP_DrugDosage_BestBeforeMeasure]
GO

ALTER TABLE [dbo].[EXP_DrugDosage]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugDosage_AppPeriodOpenMeasure] FOREIGN KEY([AppPeriodOpenMeasureDicId])
REFERENCES [dbo].[sr_measures] ([id])
GO

ALTER TABLE [dbo].[EXP_DrugDosage] CHECK CONSTRAINT [FK_EXP_DrugDosage_AppPeriodOpenMeasure]
GO

ALTER TABLE [dbo].[EXP_DrugDosage]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugDosage_AppPeriodMixMeasure] FOREIGN KEY([AppPeriodMixMeasureDicId])
REFERENCES [dbo].[sr_measures] ([id])
GO

ALTER TABLE [dbo].[EXP_DrugDosage] CHECK CONSTRAINT [FK_EXP_DrugDosage_AppPeriodMixMeasure]
GO


