alter table EXP_DrugCorespondence
add SubjectId int;
go

update EXP_DrugCorespondence set SubjectId=1;
go

alter table EXP_DrugCorespondence
alter column SubjectId int not null;
go

ALTER TABLE [dbo].[EXP_DrugCorespondence]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugCorespondence_SubjectId_EXP_DIC_CorespondenceSubject] FOREIGN KEY(SubjectId)
REFERENCES [dbo].EXP_DIC_CorespondenceSubject ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugCorespondence] CHECK CONSTRAINT [FK_EXP_DrugCorespondence_SubjectId_EXP_DIC_CorespondenceSubject]
GO