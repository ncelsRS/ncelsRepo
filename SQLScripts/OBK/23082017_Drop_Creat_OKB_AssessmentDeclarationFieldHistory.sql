USE [ncelsProd]
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclarationFieldHistory] DROP CONSTRAINT [FK_OBK_AssessmentDeclarationFieldHistory_OBK_AssessmentDeclaration]
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclarationFieldHistory] DROP CONSTRAINT [FK_OBK_AssessmentDeclarationFieldHistory_Employees]
GO

/****** Object:  Table [dbo].[OBK_AssessmentDeclarationFieldHistory]    Script Date: 23.08.2017 10:30:24 ******/
DROP TABLE [dbo].[OBK_AssessmentDeclarationFieldHistory]
GO

/****** Object:  Table [dbo].[OBK_AssessmentDeclarationFieldHistory]    Script Date: 23.08.2017 10:30:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_AssessmentDeclarationFieldHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ControlId] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NULL,
	[ValueField] [nvarchar](max) NULL,
	[DisplayField] [nvarchar](max) NULL,
	[CreateDate] [datetime] NOT NULL,
	[AssessmentDeclarationId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_OBK_AssessmentDeclarationFieldHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclarationFieldHistory]  WITH CHECK ADD  CONSTRAINT [FK_OBK_AssessmentDeclarationFieldHistory_Employees] FOREIGN KEY([UserId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclarationFieldHistory] CHECK CONSTRAINT [FK_OBK_AssessmentDeclarationFieldHistory_Employees]
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclarationFieldHistory]  WITH CHECK ADD  CONSTRAINT [FK_OBK_AssessmentDeclarationFieldHistory_OBK_AssessmentDeclaration] FOREIGN KEY([AssessmentDeclarationId])
REFERENCES [dbo].[OBK_AssessmentDeclaration] ([Id])
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclarationFieldHistory] CHECK CONSTRAINT [FK_OBK_AssessmentDeclarationFieldHistory_OBK_AssessmentDeclaration]
GO


