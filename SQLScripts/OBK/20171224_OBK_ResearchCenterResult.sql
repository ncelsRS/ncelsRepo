USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_ResearchCenterResult]    Script Date: 24.12.2017 21:30:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_ResearchCenterResult](
	[Id] [uniqueidentifier] NOT NULL,
	[TaskMaterialId] [uniqueidentifier] NOT NULL,
	[LaboratoryMarkId] [uniqueidentifier] NOT NULL,
	[Claim] [nvarchar](255) NOT NULL,
	[Humidity] [nvarchar](255) NOT NULL,
	[FactResult] [nvarchar](255) NOT NULL,
	[LaboratoryRegulationId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_OBK_ResearchCenterResult] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_ResearchCenterResult]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ResearchCenterResult_OBK_Ref_LaboratoryMark] FOREIGN KEY([LaboratoryMarkId])
REFERENCES [dbo].[OBK_Ref_LaboratoryMark] ([Id])
GO

ALTER TABLE [dbo].[OBK_ResearchCenterResult] CHECK CONSTRAINT [FK_OBK_ResearchCenterResult_OBK_Ref_LaboratoryMark]
GO

ALTER TABLE [dbo].[OBK_ResearchCenterResult]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ResearchCenterResult_OBK_Ref_LaboratoryRegulation] FOREIGN KEY([LaboratoryRegulationId])
REFERENCES [dbo].[OBK_Ref_LaboratoryRegulation] ([Id])
GO

ALTER TABLE [dbo].[OBK_ResearchCenterResult] CHECK CONSTRAINT [FK_OBK_ResearchCenterResult_OBK_Ref_LaboratoryRegulation]
GO

ALTER TABLE [dbo].[OBK_ResearchCenterResult]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ResearchCenterResult_OBK_TaskMaterial] FOREIGN KEY([TaskMaterialId])
REFERENCES [dbo].[OBK_TaskMaterial] ([Id])
GO

ALTER TABLE [dbo].[OBK_ResearchCenterResult] CHECK CONSTRAINT [FK_OBK_ResearchCenterResult_OBK_TaskMaterial]
GO


