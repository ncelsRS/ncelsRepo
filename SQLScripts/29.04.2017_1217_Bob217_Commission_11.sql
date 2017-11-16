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
    ,CDD.CommissionId
    ,C.TypeId AS CommissionTypeId
    ,CDD.ConclusionTypeId AS CommissionConclusionTypeId
    ,CCT.Code AS CommissionConclusionTypeCode
FROM dbo.EXP_ExpertiseStageDosage AS ESD
	INNER JOIN dbo.EXP_DrugDosage AS DD ON DD.Id = ESD.DosageId
    INNER JOIN dbo.EXP_DrugDeclaration AS DDC ON DDC.Id = DD.DrugDeclarationId
    INNER JOIN dbo.EXP_ExpertiseStage AS ES ON ES.Id = ESD.StageId
	INNER JOIN dbo.EXP_DIC_Stage AS DDS ON DDS.Id = ES.StageId
    LEFT JOIN dbo.EXP_DIC_StageResult AS DDSR ON DDSR.Id = ESD.ResultId
	LEFT JOIN dbo.Dictionaries AS DDST ON DDST.Type = 'EXP_DocumentStatus' AND DDST.Id = ESD.FinalDocStatusId
    LEFT JOIN dbo.CommissionDrugDosage AS CDD ON CDD.DrugDosageId = ESD.DosageId AND CDD.StageId = ESD.StageId
    LEFT JOIN dbo.Commissions AS C ON C.Id = CDD.CommissionId
    LEFT JOIN dbo.CommissionConclusionTypes AS CCT ON CCT.Id = CDD.ConclusionTypeId