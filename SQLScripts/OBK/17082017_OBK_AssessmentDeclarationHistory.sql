USE [ncelsProd]
GO

/****** Object:  Table [dbo].[OBK_AssessmentDeclarationHistory]    Script Date: 16.08.2017 17:07:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_AssessmentDeclarationHistory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[StatusId] [int] NOT NULL,
	[Note] [nvarchar](max) NULL,
	[AssessmentDeclarationId] [uniqueidentifier] NOT NULL,
	[XmlSign] [nvarchar](max) NULL,
 CONSTRAINT [PK_OBK_AssessmentDeclarationHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclarationHistory]  WITH CHECK ADD  CONSTRAINT [FK_OBK_AssessmentDeclarationHistory_OBK_AssessmentDeclaration] FOREIGN KEY([AssessmentDeclarationId])
REFERENCES [dbo].[OBK_AssessmentDeclaration] ([Id])
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclarationHistory] CHECK CONSTRAINT [FK_OBK_AssessmentDeclarationHistory_OBK_AssessmentDeclaration]
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclarationHistory]  WITH CHECK ADD  CONSTRAINT [FK_OBK_AssessmentDeclarationHistory_OBK_Ref_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[OBK_Ref_Status] ([Id])
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclarationHistory] CHECK CONSTRAINT [FK_OBK_AssessmentDeclarationHistory_OBK_Ref_Status]
GO


