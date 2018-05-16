ALTER VIEW dbo.Exp_DrugDosageStageView
	AS
SELECT ESD.Id
	,ESD.DosageId
    ,DD.RegNumber AS DosageRegNumber
    ,DDC.Id AS DeclarationId
    ,DDC.Number AS DeclarationNumber
    ,DDC.NameRu AS DeclarationNameRu
    ,DDC.CreatedDate AS DeclarationCreatedDate
    ,DDS.Id AS StageId
    ,DDS.NameRu AS StageNameRu
    ,DDSR.Id AS ResultId
    ,DDSR.NameRu AS ResultNameRu
	,DDST.Id AS FinalDocStatusId
	,DDST.Code AS FinalDocStatusCode
	,DDST.DisplayName AS FinalDocStatusDisplayName
FROM dbo.EXP_ExpertiseStageDosage AS ESD
	INNER JOIN dbo.EXP_DrugDosage AS DD ON DD.Id = ESD.DosageId
    INNER JOIN dbo.EXP_DrugDeclaration AS DDC ON DDC.Id = DD.DrugDeclarationId
    INNER JOIN dbo.EXP_ExpertiseStage AS ES ON ES.Id = ESD.StageId
	INNER JOIN dbo.EXP_DIC_Stage AS DDS ON DDS.Id = ES.StageId
    INNER JOIN dbo.EXP_DIC_StageResult AS DDSR ON DDSR.Id = ESD.ResultId
	LEFT JOIN dbo.Dictionaries AS DDST ON DDST.Type = 'EXP_DocumentStatus' AND DDST.Id = ESD.FinalDocStatusId
GO

ALTER TABLE [dbo].[CommissionConclusionTypes]
ADD [CommissionType] int NULL
GO

ALTER TABLE [dbo].[CommissionConclusionTypes]
ADD [Code] int NULL
GO

UPDATE dbo.CommissionConclusionTypes
SET Name = 'не рекомендовать к регистрации', CommissionType = 1, Code = 1
WHERE Id = 1

UPDATE dbo.CommissionConclusionTypes
SET Name = 'рекомендовать к регистрации', CommissionType = 1, Code = 2
WHERE Id = 2

UPDATE dbo.CommissionConclusionTypes
SET Name = 'продолжить экспертные работы', CommissionType = 1, Code = 3
WHERE Id = 3

UPDATE dbo.CommissionConclusionTypes
SET Name = 'снят с регистрации заявителем', CommissionType = 1, Code = 4
WHERE Id = 4


INSERT INTO dbo.CommissionConclusionTypes(Id, Name, Code, CommissionType)
VALUES (22, 'рекомендовать',2, 2)

INSERT INTO dbo.CommissionConclusionTypes(Id, Name, Code, CommissionType)
VALUES (21, 'не рекомендовать',1, 2)

INSERT INTO dbo.CommissionConclusionTypes(Id, Name, Code, CommissionType)
VALUES (23, 'рассмотреть документы повторно',3, 2)

INSERT INTO dbo.CommissionConclusionTypes(Id, Name, Code, CommissionType)
VALUES (24, 'снят с регистрации заявителем',4, 2)

INSERT INTO dbo.CommissionConclusionTypes(Id, Name, Code, CommissionType)
VALUES (25, 'решение Экспертного Совета',5, 2)


INSERT INTO dbo.CommissionConclusionTypes(Id, Name, Code, CommissionType)
VALUES (32, 'рекомендовать',2, 3)

INSERT INTO dbo.CommissionConclusionTypes(Id, Name, Code, CommissionType)
VALUES (31, 'не рекомендовать',1, 3)

INSERT INTO dbo.CommissionConclusionTypes(Id, Name, Code, CommissionType)
VALUES (33, 'рассмотреть документы повторно',3, 3)

INSERT INTO dbo.CommissionConclusionTypes(Id, Name, Code, CommissionType)
VALUES (34, 'снят с регистрации заявителем',4, 3)

INSERT INTO dbo.CommissionConclusionTypes(Id, Name, Code, CommissionType)
VALUES (35, 'решение Экспертного Совета',5, 3)



GO
--вот это поворот :(
DELETE FROM dbo.CommissionDrugDosage
GO

ALTER TABLE [dbo].[CommissionDrugDosage]
ADD [StageId] uniqueidentifier NOT NULL
GO

ALTER VIEW dbo.Exp_DrugDosageStageView
	AS
SELECT ESD.Id
	,ESD.DosageId
    ,ESD.StageId AS DosageStageId
    ,DD.RegNumber AS DosageRegNumber
    ,DDC.Id AS DeclarationId
    ,DDC.Number AS DeclarationNumber
    ,DDC.NameRu AS DeclarationNameRu
    ,DDC.CreatedDate AS DeclarationCreatedDate
    ,DDS.Id AS StageId
    ,DDS.NameRu AS StageNameRu
    ,DDSR.Id AS ResultId
    ,DDSR.NameRu AS ResultNameRu
	,DDST.Id AS FinalDocStatusId
	,DDST.Code AS FinalDocStatusCode
	,DDST.DisplayName AS FinalDocStatusDisplayName
FROM dbo.EXP_ExpertiseStageDosage AS ESD
	INNER JOIN dbo.EXP_DrugDosage AS DD ON DD.Id = ESD.DosageId
    INNER JOIN dbo.EXP_DrugDeclaration AS DDC ON DDC.Id = DD.DrugDeclarationId
    INNER JOIN dbo.EXP_ExpertiseStage AS ES ON ES.Id = ESD.StageId
	INNER JOIN dbo.EXP_DIC_Stage AS DDS ON DDS.Id = ES.StageId
    LEFT JOIN dbo.EXP_DIC_StageResult AS DDSR ON DDSR.Id = ESD.ResultId
	LEFT JOIN dbo.Dictionaries AS DDST ON DDST.Type = 'EXP_DocumentStatus' AND DDST.Id = ESD.FinalDocStatusId
GO

ALTER TABLE [dbo].[CommissionDrugDosage]
ADD CONSTRAINT [CommissionDrugDosage_DrugDosageId_fk] FOREIGN KEY ([DrugDosageId]) 
  REFERENCES [dbo].[EXP_DrugDosage] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

ALTER TABLE [dbo].[CommissionDrugDosage]
ADD CONSTRAINT [CommissionDrugDosage_StageId_fk] FOREIGN KEY ([StageId]) 
  REFERENCES [dbo].[EXP_ExpertiseStage] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO