USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_Ref_LaboratoryRegulation]    Script Date: 24.12.2017 21:29:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_Ref_LaboratoryRegulation](
	[Id] [uniqueidentifier] NOT NULL,
	[LaboratoryMarkId] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](255) NOT NULL,
	[NameKz] [nvarchar](255) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_OBK_Ref_LaboratoryRegulation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_Ref_LaboratoryRegulation]  WITH CHECK ADD  CONSTRAINT [FK_OBK_Ref_LaboratoryRegulation_OBK_Ref_LaboratoryMark] FOREIGN KEY([LaboratoryMarkId])
REFERENCES [dbo].[OBK_Ref_LaboratoryMark] ([Id])
GO

ALTER TABLE [dbo].[OBK_Ref_LaboratoryRegulation] CHECK CONSTRAINT [FK_OBK_Ref_LaboratoryRegulation_OBK_Ref_LaboratoryMark]
GO


