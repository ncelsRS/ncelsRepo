ALTER TABLE [dbo].[EXP_DrugDosage]
ADD [RegisterId] [int] NULL
GO
ALTER TABLE [dbo].[EXP_DrugDosage]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugDosage_register] FOREIGN KEY([RegisterId])
REFERENCES [dbo].[sr_register] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugDosage] CHECK CONSTRAINT [FK_EXP_DrugDosage_register]
GO

