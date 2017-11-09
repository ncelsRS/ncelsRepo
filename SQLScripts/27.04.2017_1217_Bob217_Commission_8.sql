CREATE VIEW dbo.Exp_DrugDosageStageView
	AS
SELECT ESD.Id
	,ESD.DosageId
    ,DD.RegNumber AS DosageRegNumber
    ,DDC.Number AS DeclarationNumber
    ,DDC.NameRu AS DeclarationNameRu
    ,DDS.Id AS StageId
    ,DDS.NameRu AS StageNameRu
    ,DDS.Id AS ResultId
    ,DDSR.NameRu AS ResultNameRu
FROM dbo.EXP_ExpertiseStageDosage AS ESD
	INNER JOIN dbo.EXP_DrugDosage AS DD ON DD.Id = ESD.DosageId
    INNER JOIN dbo.EXP_DrugDeclaration AS DDC ON DDC.Id = DD.DrugDeclarationId
    INNER JOIN dbo.EXP_ExpertiseStage AS ES ON ES.Id = ESD.StageId
	INNER JOIN dbo.EXP_DIC_Stage AS DDS ON DDS.Id = ES.StageId
    INNER JOIN dbo.EXP_DIC_StageResult AS DDSR ON DDSR.Id = ESD.ResultId
GO
UPDATE dbo.CommissionTypes
SET Name = 'Фармацевтическая комиссия'
WHERE Id = 2