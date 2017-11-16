alter table [EXP_DrugPrimaryFinalDocument]
add StatusId uniqueidentifier
go
ALTER TABLE [dbo].[EXP_DrugPrimaryFinalDocument]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugPrimaryFinalDocument_StatusId_Dictionaries] FOREIGN KEY(StatusId)
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugPrimaryFinalDocument] CHECK CONSTRAINT [FK_EXP_DrugPrimaryFinalDocument_StatusId_Dictionaries]
GO


