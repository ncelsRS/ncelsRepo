declare @settingId uniqueidentifier;
declare @activityId uniqueidentifier=newid();

select @settingId=s.Id from EXP_AgreementProcSettings s
inner join Dictionaries st on st.Id=s.AgreedDocTypeId
where st.Code='1'


insert into EXP_AgreementProcSettingsActivities (Id, SettingId, ActivityTypeId)
select @activityId, @settingId, Id from Dictionaries where Type='ExpActivityType' and Code='2'

insert into EXP_AgreementProcSettingsTasks (ActivityId, OrderNum, TaskTypeId)
select @activityId, 1, Id from Dictionaries where Type='ExpTaskType' and Code='2'

insert into EXP_AgreementProcSettingsTasks (ActivityId, OrderNum, TaskTypeId)
select @activityId, 2, Id from Dictionaries where Type='ExpTaskType' and Code='2'