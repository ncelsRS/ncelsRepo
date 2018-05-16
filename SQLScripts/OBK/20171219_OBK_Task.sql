USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_Tasks]    Script Date: 19.12.2017 10:08:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_Tasks](
	[Id] [uniqueidentifier] NOT NULL,
	[TaskNumber] [nvarchar](50) NULL,
	[RegisterDate] [nvarchar](50) NULL,
	[ExecutorId] [uniqueidentifier] NULL,
	[UnitId] [uniqueidentifier] NOT NULL,
	[ActReceptionId] [uniqueidentifier] NOT NULL,
	[AssessmentDeclarationId] [uniqueidentifier] NOT NULL,
	[TaskEndDate] [datetime] NULL,
	[IsSigned] [bit] NOT NULL,
	[SendToCoz] [bit] NOT NULL,
	[AcceptToCoz] [bit] NOT NULL,
	[SendToIC] [datetime] NULL,
	[CozExecutorId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_OBK_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_Tasks] ADD  CONSTRAINT [DF__OBK_Tasks__IsSig__012A0591]  DEFAULT ((0)) FOR [IsSigned]
GO

ALTER TABLE [dbo].[OBK_Tasks] ADD  CONSTRAINT [DF__OBK_Tasks__SendT__171946B0]  DEFAULT ((0)) FOR [SendToCoz]
GO

ALTER TABLE [dbo].[OBK_Tasks] ADD  CONSTRAINT [DF__OBK_Tasks__Accep__180D6AE9]  DEFAULT ((0)) FOR [AcceptToCoz]
GO

ALTER TABLE [dbo].[OBK_Tasks]  WITH CHECK ADD  CONSTRAINT [FK_OBK_Tasks_Employees] FOREIGN KEY([ExecutorId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[OBK_Tasks] CHECK CONSTRAINT [FK_OBK_Tasks_Employees]
GO

ALTER TABLE [dbo].[OBK_Tasks]  WITH CHECK ADD  CONSTRAINT [FK_OBK_Tasks_OBK_ActReception] FOREIGN KEY([ActReceptionId])
REFERENCES [dbo].[OBK_ActReception] ([Id])
GO

ALTER TABLE [dbo].[OBK_Tasks] CHECK CONSTRAINT [FK_OBK_Tasks_OBK_ActReception]
GO

ALTER TABLE [dbo].[OBK_Tasks]  WITH CHECK ADD  CONSTRAINT [FK_OBK_Tasks_OBK_AssessmentDeclaration] FOREIGN KEY([AssessmentDeclarationId])
REFERENCES [dbo].[OBK_AssessmentDeclaration] ([Id])
GO

ALTER TABLE [dbo].[OBK_Tasks] CHECK CONSTRAINT [FK_OBK_Tasks_OBK_AssessmentDeclaration]
GO

ALTER TABLE [dbo].[OBK_Tasks]  WITH CHECK ADD  CONSTRAINT [FK_OBK_Tasks_Units] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Units] ([Id])
GO

ALTER TABLE [dbo].[OBK_Tasks] CHECK CONSTRAINT [FK_OBK_Tasks_Units]
GO


