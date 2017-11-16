USE [ncelsProd]
GO

/****** Object:  Table [dbo].[OBK_AssessmentStageExecutors]    Script Date: 16.08.2017 17:09:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_AssessmentStageExecutors](
	[AssessmentStageId] [uniqueidentifier] NOT NULL,
	[ExecutorId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_OBK_AssessmentStageExecutors] PRIMARY KEY CLUSTERED 
(
	[AssessmentStageId] ASC,
	[ExecutorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_AssessmentStageExecutors]  WITH CHECK ADD  CONSTRAINT [FK_OBK_AssessmentStageExecutors_Employees] FOREIGN KEY([ExecutorId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[OBK_AssessmentStageExecutors] CHECK CONSTRAINT [FK_OBK_AssessmentStageExecutors_Employees]
GO

ALTER TABLE [dbo].[OBK_AssessmentStageExecutors]  WITH CHECK ADD  CONSTRAINT [FK_OBK_AssessmentStageExecutors_OBK_AssessmentStage] FOREIGN KEY([AssessmentStageId])
REFERENCES [dbo].[OBK_AssessmentStage] ([Id])
GO

ALTER TABLE [dbo].[OBK_AssessmentStageExecutors] CHECK CONSTRAINT [FK_OBK_AssessmentStageExecutors_OBK_AssessmentStage]
GO


