USE [ncelsProd]
GO

/****** Object:  Table [dbo].[OBK_AssessmentDeclarationCom]    Script Date: 23.08.2017 11:26:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_AssessmentDeclarationCom](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AssessmentDeclarationId] [uniqueidentifier] NOT NULL,
	[IsError] [bit] NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[ControlId] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_OBK_AssessmentDeclarationCom] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclarationCom]  WITH CHECK ADD  CONSTRAINT [FK_OBK_AssessmentDeclarationCom_OBK_AssessmentDeclaration] FOREIGN KEY([AssessmentDeclarationId])
REFERENCES [dbo].[OBK_AssessmentDeclaration] ([Id])
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclarationCom] CHECK CONSTRAINT [FK_OBK_AssessmentDeclarationCom_OBK_AssessmentDeclaration]
GO


