declare @settingId uniqueidentifier=newid();
declare @activityId uniqueidentifier=newid();
insert into EXP_AgreementProcSettings (Id, AgreedDocTypeId)
select @settingId, Id from Dictionaries where Type='ExpAgreedDocType' and Code='1'

insert into EXP_AgreementProcSettingsActivities (Id, SettingId, ActivityTypeId)
select @activityId, @settingId, Id from Dictionaries where Type='ExpActivityType' and Code='1'

insert into EXP_AgreementProcSettingsTasks (ActivityId, OrderNum, TaskTypeId)
select @activityId, 1, Id from Dictionaries where Type='ExpTaskType' and Code='1'