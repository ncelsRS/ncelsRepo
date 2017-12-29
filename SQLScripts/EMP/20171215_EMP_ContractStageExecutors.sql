USE [ncels]
GO

/****** Object:  Table [dbo].[EMP_ContractStageExecutors]    Script Date: 27.12.2017 18:03:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_ContractStageExecutors](
	[ContractStageId] [uniqueidentifier] NOT NULL,
	[ExecutorId] [uniqueidentifier] NOT NULL,
	[ExecutorType] [int] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EMP_ContractStageExecutors_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EMP_ContractStageExecutors]  WITH CHECK ADD  CONSTRAINT [FK_EMP_ContractStageExecutors_ContractStageId_EMP_ContractStage_Id] FOREIGN KEY([ContractStageId])
REFERENCES [dbo].[EMP_ContractStage] ([Id])
GO

ALTER TABLE [dbo].[EMP_ContractStageExecutors] CHECK CONSTRAINT [FK_EMP_ContractStageExecutors_ContractStageId_EMP_ContractStage_Id]
GO

ALTER TABLE [dbo].[EMP_ContractStageExecutors]  WITH CHECK ADD  CONSTRAINT [FK_EMP_ContractStageExecutors_ExecutorId_Employee_Id] FOREIGN KEY([ExecutorId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[EMP_ContractStageExecutors] CHECK CONSTRAINT [FK_EMP_ContractStageExecutors_ExecutorId_Employee_Id]
GO


