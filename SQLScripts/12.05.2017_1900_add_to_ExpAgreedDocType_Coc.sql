insert into Dictionaries (Id, Type, Code, Name, NameKz, DisplayName, IsGuide)
values (NEWID(), 'ExpAgreedDocType', '8', N'Акт выполненых работ', N'Акт выполненых работ', N'Акт выполненых работ',0)
go

insert into Dictionaries (Id, Type, Code, Name, NameKz, DisplayName, IsGuide)
values( NEWID(), 'ExpActivityType', '10', N'Согласование акта выполненых работ', N'Согласование акта выполненых работ', N'Согласование акта выполненых работ', 0)
Go

Declare @SettingID uniqueidentifier = NEWID(), @activityId uniqueidentifier = NEWID()  ;



INSERT INTO [dbo].[EXP_AgreementProcSettings]
           ([Id]
           ,[AgreedDocTypeId])
     VALUES
           (@SettingID           
           ,(SELECT TOP 1 Id FROM Dictionaries WHERE [Type]='ExpAgreedDocType' AND [Code]='8'))


INSERT INTO [dbo].[EXP_AgreementProcSettingsActivities]
           ([Id]
           ,[SettingId]
           ,[ActivityTypeId])
     VALUES
           (@activityId
           ,@SettingID
           ,(SELECT TOP 1 Id FROM Dictionaries WHERE [Type]='ExpActivityType' AND [Code]='10'))


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
           ,(SELECT TOP 1 Id FROM Dictionaries WHERE [Type]='ExpTaskType' AND [Code]='1')
           ,NULL
           ,NULL
           ,1)
GO