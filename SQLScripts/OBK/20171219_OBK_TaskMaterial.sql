USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_TaskMaterial]    Script Date: 19.12.2017 10:11:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_TaskMaterial](
	[Id] [uniqueidentifier] NOT NULL,
	[TaskId] [uniqueidentifier] NOT NULL,
	[LaboratoryTypeId] [uniqueidentifier] NOT NULL,
	[ProductSeriesId] [int] NOT NULL,
	[Quantity] [int] NULL,
	[UnitLaboratoryId] [uniqueidentifier] NULL,
	[IdNumber] [nvarchar](50) NULL,
	[StorageConditionId] [uniqueidentifier] NULL,
	[ExternalConditionId] [uniqueidentifier] NULL,
	[DimensionIMN] [nvarchar](50) NULL,
 CONSTRAINT [PK_OBK_TaskMaterial] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_TaskMaterial]  WITH CHECK ADD  CONSTRAINT [FK_OBK_TaskMaterial_OBK_Procunts_Series] FOREIGN KEY([ProductSeriesId])
REFERENCES [dbo].[OBK_Procunts_Series] ([Id])
GO

ALTER TABLE [dbo].[OBK_TaskMaterial] CHECK CONSTRAINT [FK_OBK_TaskMaterial_OBK_Procunts_Series]
GO

ALTER TABLE [dbo].[OBK_TaskMaterial]  WITH CHECK ADD  CONSTRAINT [FK_OBK_TaskMaterial_OBK_Ref_LaboratoryType] FOREIGN KEY([LaboratoryTypeId])
REFERENCES [dbo].[OBK_Ref_LaboratoryType] ([Id])
GO

ALTER TABLE [dbo].[OBK_TaskMaterial] CHECK CONSTRAINT [FK_OBK_TaskMaterial_OBK_Ref_LaboratoryType]
GO

ALTER TABLE [dbo].[OBK_TaskMaterial]  WITH CHECK ADD  CONSTRAINT [FK_OBK_TaskMaterial_OBK_Ref_MaterialCondition] FOREIGN KEY([StorageConditionId])
REFERENCES [dbo].[OBK_Ref_MaterialCondition] ([Id])
GO

ALTER TABLE [dbo].[OBK_TaskMaterial] CHECK CONSTRAINT [FK_OBK_TaskMaterial_OBK_Ref_MaterialCondition]
GO

ALTER TABLE [dbo].[OBK_TaskMaterial]  WITH CHECK ADD  CONSTRAINT [FK_OBK_TaskMaterial_OBK_Ref_MaterialCondition1] FOREIGN KEY([ExternalConditionId])
REFERENCES [dbo].[OBK_Ref_MaterialCondition] ([Id])
GO

ALTER TABLE [dbo].[OBK_TaskMaterial] CHECK CONSTRAINT [FK_OBK_TaskMaterial_OBK_Ref_MaterialCondition1]
GO

ALTER TABLE [dbo].[OBK_TaskMaterial]  WITH CHECK ADD  CONSTRAINT [FK_OBK_TaskMaterial_OBK_Tasks] FOREIGN KEY([TaskId])
REFERENCES [dbo].[OBK_Tasks] ([Id])
GO

ALTER TABLE [dbo].[OBK_TaskMaterial] CHECK CONSTRAINT [FK_OBK_TaskMaterial_OBK_Tasks]
GO

ALTER TABLE [dbo].[OBK_TaskMaterial]  WITH CHECK ADD  CONSTRAINT [FK_OBK_TaskMaterial_Units] FOREIGN KEY([UnitLaboratoryId])
REFERENCES [dbo].[Units] ([Id])
GO

ALTER TABLE [dbo].[OBK_TaskMaterial] CHECK CONSTRAINT [FK_OBK_TaskMaterial_Units]
GO


