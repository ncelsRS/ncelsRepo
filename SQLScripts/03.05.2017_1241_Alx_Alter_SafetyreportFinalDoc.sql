/*
   3 мая 2017 г.12:40:11
   User: 
   Server: METIS\MSSQL2016
   Database: ncels
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.EXP_ExpertiseSafetyreportFinalDoc
	DROP CONSTRAINT FK_EXP_ExpertiseSafetyreportFinalDoc_EXP_ExpertiseStageDosage
GO
ALTER TABLE dbo.EXP_ExpertiseStageDosage SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.EXP_ExpertiseSafetyreportFinalDoc
	DROP CONSTRAINT DF_EXP_ExpertiseSafetyreportFinalDoc_Id
GO
CREATE TABLE dbo.Tmp_EXP_ExpertiseSafetyreportFinalDoc
	(
	Id uniqueidentifier NOT NULL,
	PrimaryConclusion ntext NULL,
	PrimaryConclusionKz ntext NULL,
	PharmaceuticalConclusion ntext NULL,
	PharmaceuticalConclusionKz ntext NULL,
	PharmacologicalConclusion ntext NULL,
	PharmacologicalConclusionKz ntext NULL,
	AnalyticalConclusion ntext NULL,
	AnalyticalConclusionKz ntext NULL,
	Conclusion ntext NULL,
	ConclusionKz ntext NULL,
	DosageStageId uniqueidentifier NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_EXP_ExpertiseSafetyreportFinalDoc SET (LOCK_ESCALATION = TABLE)
GO
DECLARE @v sql_variant 
SET @v = N'Заключение первичной'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_EXP_ExpertiseSafetyreportFinalDoc', N'COLUMN', N'PrimaryConclusion'
GO
DECLARE @v sql_variant 
SET @v = N'заключение фармацеское'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_EXP_ExpertiseSafetyreportFinalDoc', N'COLUMN', N'PharmaceuticalConclusion'
GO
DECLARE @v sql_variant 
SET @v = N'заключение фармакологическое'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_EXP_ExpertiseSafetyreportFinalDoc', N'COLUMN', N'PharmacologicalConclusion'
GO
DECLARE @v sql_variant 
SET @v = N'заключение иследовательской лаборатории'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_EXP_ExpertiseSafetyreportFinalDoc', N'COLUMN', N'AnalyticalConclusion'
GO
DECLARE @v sql_variant 
SET @v = N'заключение Зоб'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_EXP_ExpertiseSafetyreportFinalDoc', N'COLUMN', N'Conclusion'
GO
ALTER TABLE dbo.Tmp_EXP_ExpertiseSafetyreportFinalDoc ADD CONSTRAINT
	DF_EXP_ExpertiseSafetyreportFinalDoc_Id DEFAULT (newid()) FOR Id
GO
IF EXISTS(SELECT * FROM dbo.EXP_ExpertiseSafetyreportFinalDoc)
	 EXEC('INSERT INTO dbo.Tmp_EXP_ExpertiseSafetyreportFinalDoc (Id, PrimaryConclusion, PharmaceuticalConclusion, PharmacologicalConclusion, AnalyticalConclusion, Conclusion, DosageStageId)
		SELECT Id, PrimaryConclusion, PharmaceuticalConclusion, PharmacologicalConclusion, AnalyticalConclusion, Conclusion, DosageStageId FROM dbo.EXP_ExpertiseSafetyreportFinalDoc WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.EXP_ExpertiseSafetyreportFinalDoc
GO
EXECUTE sp_rename N'dbo.Tmp_EXP_ExpertiseSafetyreportFinalDoc', N'EXP_ExpertiseSafetyreportFinalDoc', 'OBJECT' 
GO
ALTER TABLE dbo.EXP_ExpertiseSafetyreportFinalDoc ADD CONSTRAINT
	PK_EXP_ExpertiseSafetyreportFinalDoc PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.EXP_ExpertiseSafetyreportFinalDoc ADD CONSTRAINT
	FK_EXP_ExpertiseSafetyreportFinalDoc_EXP_ExpertiseStageDosage FOREIGN KEY
	(
	DosageStageId
	) REFERENCES dbo.EXP_ExpertiseStageDosage
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
