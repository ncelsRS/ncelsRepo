USE [ncelsProd]
GO

/****** Object:  Table [dbo].[OBK_StageExpDocumentResult]    Script Date: 02.11.2017 11:12:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_StageExpDocumentResult](
	[Id] [uniqueidentifier] NOT NULL,
	[ExpResult] [bit] NOT NULL,
	[AssessmetDeclarationId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_OBK_StageExpDocumentResult] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_StageExpDocumentResult]  WITH CHECK ADD  CONSTRAINT [FK_OBK_StageExpDocumentResult_OBK_AssessmentDeclaration] FOREIGN KEY([AssessmetDeclarationId])
REFERENCES [dbo].[OBK_AssessmentDeclaration] ([Id])
GO

ALTER TABLE [dbo].[OBK_StageExpDocumentResult] CHECK CONSTRAINT [FK_OBK_StageExpDocumentResult_OBK_AssessmentDeclaration]
GO


