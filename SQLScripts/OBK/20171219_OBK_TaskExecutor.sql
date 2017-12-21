USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_TaskExecutor]    Script Date: 19.12.2017 10:11:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_TaskExecutor](
	[Id] [uniqueidentifier] NOT NULL,
	[TaskId] [uniqueidentifier] NOT NULL,
	[ExecutorId] [uniqueidentifier] NOT NULL,
	[StageId] [int] NOT NULL,
	[ExecutorType] [int] NULL,
 CONSTRAINT [PK_OBK_TaskExecutor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_TaskExecutor]  WITH CHECK ADD  CONSTRAINT [FK_OBK_TaskExecutor_Employees] FOREIGN KEY([ExecutorId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[OBK_TaskExecutor] CHECK CONSTRAINT [FK_OBK_TaskExecutor_Employees]
GO

ALTER TABLE [dbo].[OBK_TaskExecutor]  WITH CHECK ADD  CONSTRAINT [FK_OBK_TaskExecutor_OBK_Tasks] FOREIGN KEY([TaskId])
REFERENCES [dbo].[OBK_Tasks] ([Id])
GO

ALTER TABLE [dbo].[OBK_TaskExecutor] CHECK CONSTRAINT [FK_OBK_TaskExecutor_OBK_Tasks]
GO


