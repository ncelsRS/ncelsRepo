USE [ncels]
GO

/****** Object:  Table [dbo].[EXP_AgreementProcSettingsTasks]    Script Date: 23.03.2017 10:10:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EXP_AgreementProcSettingsTasks](
	[Id] [uniqueidentifier] NOT NULL,
	[ActivityId] [uniqueidentifier] NOT NULL,
	[TaskTypeId] [uniqueidentifier] NOT NULL,
	[ParentTaskId] [uniqueidentifier] NULL,
	[ExecutorId] [uniqueidentifier] NULL,
	[OrderNum] [int] NOT NULL,
 CONSTRAINT [PK_EXP_AgreementProcSettingsTasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_AgreementProcSettingsTasks] ADD  CONSTRAINT [DF_EXP_AgreementProcSettingsTasks_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[EXP_AgreementProcSettingsTasks]  WITH CHECK ADD  CONSTRAINT [FK_EXP_AgreementProcSettingsTasks_Dictionaries] FOREIGN KEY([TaskTypeId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_AgreementProcSettingsTasks] CHECK CONSTRAINT [FK_EXP_AgreementProcSettingsTasks_Dictionaries]
GO

ALTER TABLE [dbo].[EXP_AgreementProcSettingsTasks]  WITH CHECK ADD  CONSTRAINT [FK_EXP_AgreementProcSettingsTasks_ExecutorId_Employees] FOREIGN KEY([ExecutorId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[EXP_AgreementProcSettingsTasks] CHECK CONSTRAINT [FK_EXP_AgreementProcSettingsTasks_ExecutorId_Employees]
GO

ALTER TABLE [dbo].[EXP_AgreementProcSettingsTasks]  WITH CHECK ADD  CONSTRAINT [FK_EXP_AgreementProcSettingsTasks_EXP_AgreementProcSettingsActivities] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[EXP_AgreementProcSettingsActivities] ([Id])
GO

ALTER TABLE [dbo].[EXP_AgreementProcSettingsTasks] CHECK CONSTRAINT [FK_EXP_AgreementProcSettingsTasks_EXP_AgreementProcSettingsActivities]
GO

ALTER TABLE [dbo].[EXP_AgreementProcSettingsTasks]  WITH CHECK ADD  CONSTRAINT [FK_EXP_AgreementProcSettingsTasks_ParentTaskId_EXP_AgreementProcSettingsTasks] FOREIGN KEY([ParentTaskId])
REFERENCES [dbo].[EXP_AgreementProcSettingsTasks] ([Id])
GO

ALTER TABLE [dbo].[EXP_AgreementProcSettingsTasks] CHECK CONSTRAINT [FK_EXP_AgreementProcSettingsTasks_ParentTaskId_EXP_AgreementProcSettingsTasks]
GO


