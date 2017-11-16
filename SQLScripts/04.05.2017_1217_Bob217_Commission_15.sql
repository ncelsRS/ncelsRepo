CREATE VIEW dbo.EXP_DrugDeclarationPaysInfoView
	AS
SELECT 
  DrugDeclarationId,
  STUFF((
    SELECT ', ' + ISNULL(CAST(X.PaymentDatetime1C AS NVARCHAR(MAX)),'-')
    FROM (
		SELECT PDDD.DrugDeclarationId
			,PDP.PaymentDatetime1C
		FROM dbo.[EXP_DirectionToPays_DrugDeclaration] AS PDDD
			INNER JOIN dbo.EXP_DirectionToPays AS PDP ON PDP.Id = PDDD.DirectionToPayId
		) AS X
    WHERE (DrugDeclarationId = Results.DrugDeclarationId) 
    FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)')
  ,1,2,'') AS PaysDates
FROM (
		SELECT PDDD.DrugDeclarationId
			,PDP.PaymentDatetime1C
		FROM dbo.[EXP_DirectionToPays_DrugDeclaration] AS PDDD
			INNER JOIN dbo.EXP_DirectionToPays AS PDP ON PDP.Id = PDDD.DirectionToPayId
		) Results
GROUP BY DrugDeclarationId
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
    ,CDD.CommissionId
    ,C.TypeId AS CommissionTypeId
    ,CDD.ConclusionTypeId AS CommissionConclusionTypeId
    ,CCT.Code AS CommissionConclusionTypeCode
	,P.NameRu AS ProducerNameRu
	,P.CountryDicId AS ProducerCountryId
	,PCD.Name AS ProducerCountryName
	,DF.id AS DosageFormId
	,DF.name AS DosageFormName
    ,PI.PaysDates AS DeclarationPaysDates
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
	LEFT JOIN dbo.EXP_DrugOrganizations AS P ON P.DrugDeclarationId = DD.DrugDeclarationId 
	LEFT JOIN dbo.Dictionaries AS PCD ON PCD.Id = P.CountryDicId
	LEFT JOIN dbo.sr_dosage_forms AS DF ON DF.id = DD.DosageMeasureTypeId
    LEFT JOIN dbo.EXP_DrugDeclarationPaysInfoView AS PI ON PI.DrugDeclarationId = DD.DrugDeclarationId
GO