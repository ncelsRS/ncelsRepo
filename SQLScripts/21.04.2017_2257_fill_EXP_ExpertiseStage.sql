insert into EXP_ExpertiseStage
(DeclarationId, StageId, StatusId, IsHistory)
select id, (select id from EXP_DIC_Stage where Code='0'),
(select id from EXP_DIC_StageStatus where Code='inQueue'), 0 from EXP_DrugDeclaration where StatusId<>1
go
insert into EXP_ExpertiseStageExecutors (ExpertiseStageId, ExecutorId)
select id, (select id from Employees where Login='grebennikova') from EXP_ExpertiseStage