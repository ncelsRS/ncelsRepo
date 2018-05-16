update EXP_DrugDeclaration set DrugFormId  = null
GO

ALTER TABLE [dbo].[EXP_DrugDeclaration]
DROP CONSTRAINT [FK_EXP_DrugDeclaration_sr_drug_forms]
GO  

ALTER TABLE [dbo].[EXP_DrugDeclaration]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugDeclaration_sr_dosage_forms] FOREIGN KEY([DrugFormId])
REFERENCES [dbo].[sr_dosage_forms] ([id])
GO

ALTER TABLE [dbo].[EXP_DrugDeclaration] CHECK CONSTRAINT [FK_EXP_DrugDeclaration_sr_dosage_forms]
GO
ALTER TABLE [dbo].[EXP_DrugProtectionDoc]
ADD [NameOwner] nvarchar(500) NULL
GO
ALTER TABLE [dbo].[EXP_DrugPatent]
ADD [NameOwner] nvarchar(500) NULL
GO
ALTER TABLE [dbo].[EXP_DrugPatent]
ADD [NameDocument] nvarchar(500) NULL
GO