delete from [dbo].[EXP_DrugSubstance]
GO
ALTER TABLE [EXP_DrugSubstance]
DROP CONSTRAINT [FK_EXP_DrugSubstance_EXP_DrugDeclaration]
GO
ALTER TABLE  [EXP_DrugSubstance] drop column [DrugDeclarationId]
GO
ALTER TABLE [dbo].[EXP_DrugSubstance]
ADD [DrugDosageId] [bigint] NOT NULL
GO
ALTER TABLE [dbo].[EXP_DrugSubstance]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugSubstance_DrugDosage] FOREIGN KEY([DrugDosageId])
REFERENCES [dbo].[EXP_DrugDosage] ([Id])
GO
update sr_substance_types
set name = 'Активные вещества'
where id=1
GO
delete from EXP_DIC_Type where id=4