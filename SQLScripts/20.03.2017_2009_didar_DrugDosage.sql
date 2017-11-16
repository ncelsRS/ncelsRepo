

CREATE TABLE [dbo].[EXP_DrugDosage](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RegNumber] [nvarchar](50) NULL,
	[DrugDeclarationId] [uniqueidentifier] NOT NULL,
	[Dosage] [decimal](18, 2) NOT NULL,
	[DosageMeasureTypeId] [bigint] NULL,
	[DosageNoteKz] [nvarchar](500) NULL,
	[DosageNoteRu] [nvarchar](500) NULL,
	[ConcentrationRu] [nvarchar](500) NULL,
	[ConcentrationKz] [nvarchar](500) NULL,
 CONSTRAINT [PK_EXP_DrugDosage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_DrugDosage]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugDosage_EXP_DrugDeclaration] FOREIGN KEY([DrugDeclarationId])
REFERENCES [dbo].[EXP_DrugDeclaration] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugDosage] CHECK CONSTRAINT [FK_EXP_DrugDosage_EXP_DrugDeclaration]
GO
ALTER TABLE [dbo].[EXP_DrugDosage]  WITH CHECK ADD  CONSTRAINT [FK_EXPDrugDosage_measures] FOREIGN KEY([DosageMeasureTypeId])
REFERENCES [dbo].[sr_measures] ([id])
GO

ALTER TABLE [dbo].[EXP_DrugDosage] CHECK CONSTRAINT [FK_EXPDrugDosage_measures]
GO

delete from [dbo].[EXP_DrugWrapping]
GO
ALTER TABLE [EXP_DrugWrapping]
DROP CONSTRAINT [FK_EXP_DrugWrapping_EXP_DrugDeclaration]
GO
ALTER TABLE  [EXP_DrugWrapping] drop column [DrugDeclarationId]
GO
ALTER TABLE [dbo].[EXP_DrugWrapping]
ADD [DrugDosageId] [bigint] NOT NULL
GO
ALTER TABLE [dbo].[EXP_DrugWrapping]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugWrapping_DrugDosage] FOREIGN KEY([DrugDosageId])
REFERENCES [dbo].[EXP_DrugDosage] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugWrapping] CHECK CONSTRAINT [FK_EXP_DrugWrapping_DrugDosage]
GO
delete from [dbo].[EXP_DrugPrice]
GO
ALTER TABLE [EXP_DrugPrice]
DROP CONSTRAINT [FK_EXP_DrugPrice_EXP_DrugDeclaration]
GO
ALTER TABLE [EXP_DrugPrice] drop column [DrugDeclarationId]
GO
ALTER TABLE [dbo].[EXP_DrugPrice]
ADD [DrugDosageId] [bigint] NOT NULL
GO
ALTER TABLE [dbo].[EXP_DrugPrice]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugPrice_DrugDosage] FOREIGN KEY([DrugDosageId])
REFERENCES [dbo].[EXP_DrugDosage] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugPrice] CHECK CONSTRAINT [FK_EXP_DrugPrice_DrugDosage]
GO
