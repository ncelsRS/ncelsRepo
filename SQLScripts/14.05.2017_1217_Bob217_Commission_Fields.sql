ALTER VIEW dbo.Exp_DrugDosageStageView
	AS
SELECT DISTINCT
 ESD.Id
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
	,P.DocDate AS ProducerDocDate
	,P.DocExpiryDate AS ProducerDocExpiryDate
	,PCD.Name AS ProducerCountryName
	,DF.id AS DosageFormId
	,DF.name AS DosageFormName
    ,PI.PaysDates AS DeclarationPaysDates
	,DDC.AtxId AS DeclarationAtxId
	,AC.name AS DeclarationAtxName
	,AC.code AS DeclarationAtxCode
	,DDC.MnnId AS DeclarationMnnId
	,TIN.name_rus AS DeclarationMnnNameRu
	,DDC.SaleTypeId AS DeclarationSaleTypeId
	,DST.NameRu AS DeclarationSaleTypeName
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
    LEFT JOIN dbo.EXP_DrugOrganizations AS P ON P.DrugDeclarationId = DD.DrugDeclarationId AND P.OrgManufactureTypeDicId IN (
                SELECT Id 
                FROM dbo.Dictionaries AS PTD 
                WHERE PTD.Id = P.OrgManufactureTypeDicId AND PTD.[Type] = 'OrgManufactureType' 
                    AND PTD.Code = 1)    
	LEFT JOIN dbo.Dictionaries AS PCD ON PCD.Id = P.CountryDicId
	LEFT JOIN dbo.sr_dosage_forms AS DF ON DF.id = DD.DosageMeasureTypeId
    LEFT JOIN dbo.EXP_DrugDeclarationPaysInfoView AS PI ON PI.DrugDeclarationId = DD.DrugDeclarationId
	LEFT JOIN dbo.sr_atc_codes AS AC ON AC.id = DDC.AtxId
	LEFT JOIN dbo.sr_international_names AS TIN ON TIN.id = DDC.MnnId
	LEFT JOIN dbo.EXP_DIC_SaleType AS DST ON DST.Id = DDC.SaleTypeId
GO