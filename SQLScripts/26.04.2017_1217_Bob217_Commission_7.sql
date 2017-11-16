DELETE FROM dbo.CommissionDrugDeclarations

EXEC sp_rename '[dbo].[CommissionDrugDeclarations].[DrugDeclarationId]', 'DrugDosageId', 'COLUMN'
GO

ALTER TABLE [dbo].[CommissionDrugDeclarations]
DROP COLUMN [DrugDosageId]
GO

ALTER TABLE [dbo].[CommissionDrugDeclarations]
ADD [DrugDosageId] int NULL
GO

ALTER TABLE [dbo].[CommissionDrugDeclarations]
ALTER COLUMN [DrugDosageId] int NOT NULL
GO

EXEC sp_rename '[dbo].[CommissionDrugDeclarationConclusionTypes]', 'CommissionConclusionTypes', 'OBJECT'
GO

EXEC sp_rename '[dbo].[CommissionDrugDeclarations]', 'CommissionDrugDosage', 'OBJECT'
GO

ALTER TABLE [dbo].[CommissionDrugDosage]
ALTER COLUMN [DrugDosageId] bigint NOT NULL
GO