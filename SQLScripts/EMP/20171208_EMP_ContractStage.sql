USE [ncels]
GO

/****** Object:  Table [dbo].[EMP_ContractStage]    Script Date: 11.12.2017 11:50:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_ContractStage](
	[Id] [uniqueidentifier] NOT NULL,
	[ContractId] [uniqueidentifier] NOT NULL,
	[StageId] [uniqueidentifier] NOT NULL,
	[StageStatusId] [uniqueidentifier] NOT NULL,
	[ParentStageId] [uniqueidentifier] NULL,
	[Result] [int] NULL,
	[DateCreate] [datetime] NOT NULL,
 CONSTRAINT [PK_EMP_ContractStage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EMP_ContractStage]  WITH CHECK ADD  CONSTRAINT [FK_EMP_ContractStage_ContractId_EMP_Contract_Id] FOREIGN KEY([ContractId])
REFERENCES [dbo].[EMP_Contract] ([Id])
GO

ALTER TABLE [dbo].[EMP_ContractStage] CHECK CONSTRAINT [FK_EMP_ContractStage_ContractId_EMP_Contract_Id]
GO

ALTER TABLE [dbo].[EMP_ContractStage]  WITH CHECK ADD  CONSTRAINT [FK_EMP_ContractStage_ParentStageId_EMP_ContractStage_id] FOREIGN KEY([ParentStageId])
REFERENCES [dbo].[EMP_ContractStage] ([Id])
GO

ALTER TABLE [dbo].[EMP_ContractStage] CHECK CONSTRAINT [FK_EMP_ContractStage_ParentStageId_EMP_ContractStage_id]
GO

ALTER TABLE [dbo].[EMP_ContractStage]  WITH CHECK ADD  CONSTRAINT [FK_EMP_ContractStage_StageId_EMP_Ref_Stage_Id] FOREIGN KEY([StageId])
REFERENCES [dbo].[EMP_Ref_Stage] ([Id])
GO

ALTER TABLE [dbo].[EMP_ContractStage] CHECK CONSTRAINT [FK_EMP_ContractStage_StageId_EMP_Ref_Stage_Id]
GO

ALTER TABLE [dbo].[EMP_ContractStage]  WITH CHECK ADD  CONSTRAINT [FK_EMP_ContractStage_StageStatusId_EMP_Ref_StageStatus_Id] FOREIGN KEY([StageStatusId])
REFERENCES [dbo].[EMP_Ref_StageStatus] ([Id])
GO

ALTER TABLE [dbo].[EMP_ContractStage] CHECK CONSTRAINT [FK_EMP_ContractStage_StageStatusId_EMP_Ref_StageStatus_Id]
GO


