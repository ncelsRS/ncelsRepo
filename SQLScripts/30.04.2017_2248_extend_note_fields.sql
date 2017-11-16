alter table EXP_DrugCorespondence
alter column note nvarchar(max);
go

alter table EXP_DrugCorespondenceRemark
alter column NameRemark nvarchar(max);
go

alter table EXP_DrugCorespondenceRemark
alter column AnswerRemark nvarchar(max);
go

alter table EXP_DrugCorespondenceRemark
alter column Note nvarchar(max);
go

alter table EXP_ExpertiseStageRemark
alter column NameRemark nvarchar(max);
go

alter table EXP_ExpertiseStageRemark
alter column AnswerRemark nvarchar(max);

alter table EXP_ExpertiseStageRemark
alter column Note nvarchar(max);
go

alter table EXP_ExpertiseStage
alter column OtdRemarks nvarchar(max);
go