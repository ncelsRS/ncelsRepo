DECLARE @settingID uniqueidentifier = NEWID(), @activityId uniqueidentifier = NEWID(), @taskId uniqueidentifier = NEWID();
INSERT INTO [dbo].[EXP_AgreementProcSettings]
           ([Id]
           ,[AgreedDocTypeId])
     VALUES
           (@settingID
           ,(SELECT TOP 1 Id FROM Dictionaries WHERE [Type] = 'ExpAgreedDocType' AND Code ='2'))


	INSERT INTO [dbo].[EXP_AgreementProcSettingsActivities]
           ([Id]
           ,[SettingId]
           ,[ActivityTypeId])
     VALUES
           (@activityId
           ,@settingID
           ,(SELECT TOP 1 Id FROM Dictionaries WHERE [Type] = 'ExpActivityType' AND Code ='4'))

	INSERT INTO [dbo].[EXP_AgreementProcSettingsTasks]
           ([Id]
           ,[ActivityId]
           ,[TaskTypeId]
           ,[ParentTaskId]
           ,[ExecutorId]
           ,[OrderNum])
     VALUES
           (@taskId
           ,@activityId
           ,(SELECT TOP 1 Id FROM Dictionaries WHERE [Type] = 'ExpTaskType' AND Code ='1')
           ,NULL
           ,(SELECT TOP (1) [Id] FROM [dbo].[Employees] WHERE Login ='abdukayumov.i')
           ,1)

	INSERT INTO [dbo].[EXP_AgreementProcSettingsTasks]
           ([Id]
           ,[ActivityId]
           ,[TaskTypeId]
           ,[ParentTaskId]
           ,[ExecutorId]
           ,[OrderNum])
     VALUES
           (NEWID()
           ,@activityId
           ,(SELECT TOP 1 Id FROM Dictionaries WHERE [Type] = 'ExpTaskType' AND Code ='1')
           ,@taskId
           ,(SELECT TOP (1) Id  FROM [dbo].[Employees]  WHERE Login ='dzheksembieva.m')
           ,2)
GO





