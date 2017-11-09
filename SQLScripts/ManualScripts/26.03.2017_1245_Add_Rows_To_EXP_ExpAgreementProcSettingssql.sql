delete from [dbo].[EXP_AgreementProcSettingsTasks]
where Id = '3998a76c-dc83-4a58-b1ae-30ca87bb99d5'
GO


delete from [dbo].[EXP_AgreementProcSettingsTasks]
where Id = '4af2668b-cc6c-4f49-beb8-e07cb0951bbb'
GO

delete from [dbo].[EXP_AgreementProcSettingsActivities]
where Id = 'd0517673-af63-4b8d-97af-d7d67910482f'
GO

delete from [dbo].[EXP_AgreementProcSettings]
where Id = '63049c4d-dc4b-4890-9c40-66df56f28957'
GO

INSERT [dbo].[EXP_AgreementProcSettings] ([Id], [AgreedDocTypeId]) VALUES (N'63049c4d-dc4b-4890-9c40-66df56f28957', N'8BEDB56C-6E4F-470D-9B6F-BFB5D0E2FAE7')
GO

INSERT [dbo].[EXP_AgreementProcSettingsActivities] ([Id], [SettingId], [ActivityTypeId]) VALUES (N'd0517673-af63-4b8d-97af-d7d67910482f', N'63049c4d-dc4b-4890-9c40-66df56f28957', N'D6662966-BB85-4C25-9CAC-A28EA77D8B97')
GO

INSERT [dbo].[EXP_AgreementProcSettingsTasks] ([Id], [ActivityId], [TaskTypeId], [ParentTaskId], [ExecutorId], [OrderNum]) VALUES (N'4af2668b-cc6c-4f49-beb8-e07cb0951bbb', N'd0517673-af63-4b8d-97af-d7d67910482f', N'74DFEFE4-1198-4C46-AF3B-51BCC90EB216', NULL, N'e93f990d-9058-4c84-95d7-30c46663da50', 1)
GO

INSERT [dbo].[EXP_AgreementProcSettingsTasks] ([Id], [ActivityId], [TaskTypeId], [ParentTaskId], [ExecutorId], [OrderNum]) VALUES (N'3998a76c-dc83-4a58-b1ae-30ca87bb99d5', N'd0517673-af63-4b8d-97af-d7d67910482f', N'74DFEFE4-1198-4C46-AF3B-51BCC90EB216', N'4af2668b-cc6c-4f49-beb8-e07cb0951bbb', N'ea5a9b76-ab12-4050-8781-d89d795c46b4', 2)
GO
