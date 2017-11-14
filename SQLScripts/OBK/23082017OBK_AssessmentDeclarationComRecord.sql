USE [ncelsProd]
GO

/****** Object:  Table [dbo].[OBK_AssessmentDeclarationComRecord]    Script Date: 23.08.2017 11:26:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_AssessmentDeclarationComRecord](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CommentId] [bigint] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ValueField] [nvarchar](500) NULL,
	[Note] [nvarchar](2000) NULL,
	[DisplayField] [nvarchar](500) NULL,
 CONSTRAINT [PK_OBK_AssessmentDeclarationComRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclarationComRecord]  WITH CHECK ADD  CONSTRAINT [FK_OBK_AssessmentDeclarationComRecord_Employees] FOREIGN KEY([UserId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclarationComRecord] CHECK CONSTRAINT [FK_OBK_AssessmentDeclarationComRecord_Employees]
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclarationComRecord]  WITH CHECK ADD  CONSTRAINT [FK_OBK_AssessmentDeclarationComRecord_OBK_AssessmentDeclarationCom] FOREIGN KEY([CommentId])
REFERENCES [dbo].[OBK_AssessmentDeclarationCom] ([Id])
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclarationComRecord] CHECK CONSTRAINT [FK_OBK_AssessmentDeclarationComRecord_OBK_AssessmentDeclarationCom]
GO


