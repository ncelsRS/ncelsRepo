CREATE TABLE [dbo].[EXP_AgreementProcSettingsActivities](
	[Id] [uniqueidentifier] NOT NULL,
	[SettingId] [uniqueidentifier] NOT NULL,
	[ActivityTypeId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EXP_AgreementProcSettingsActivities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_AgreementProcSettingsActivities] ADD  CONSTRAINT [DF_EXP_AgreementProcSettingsActivities_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[EXP_AgreementProcSettingsActivities]  WITH CHECK ADD  CONSTRAINT [FK_EXP_AgreementProcSettingsActivities_Dictionaries] FOREIGN KEY([ActivityTypeId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_AgreementProcSettingsActivities] CHECK CONSTRAINT [FK_EXP_AgreementProcSettingsActivities_Dictionaries]
GO

ALTER TABLE [dbo].[EXP_AgreementProcSettingsActivities]  WITH CHECK ADD  CONSTRAINT [FK_EXP_AgreementProcSettingsActivities_EXP_AgreementProcSettings] FOREIGN KEY([SettingId])
REFERENCES [dbo].[EXP_AgreementProcSettings] ([Id])
GO

ALTER TABLE [dbo].[EXP_AgreementProcSettingsActivities] CHECK CONSTRAINT [FK_EXP_AgreementProcSettingsActivities_EXP_AgreementProcSettings]
GO


