CREATE VIEW dbo.Exp_DrugDeclarationChangeView
	AS
SELECT change.DrugDeclarationId
	,change.ChangeString
FROM 
	(
		SELECT DISTINCT ST2.DrugDeclarationId, 
			substring(
				(
					Select ', '+ T.ChangeName + ' (пункт '+ CAST(ST1.ChangeTypeId AS nvarchar(max))+ ')'  AS [text()]
					From dbo.EXP_DrugChangeType ST1
						LEFT JOIN dbo.EXP_DIC_ChangeType AS T ON T.Id = ST1.ChangeTypeId
					Where ST1.DrugDeclarationId = ST2.DrugDeclarationId
					ORDER BY ST1.DrugDeclarationId, T.Id
					For XML PATH ('')
				), 2, 1000) AS ChangeString
		FROM dbo.EXP_DrugChangeType ST2
		) change
GO

ALTER VIEW dbo.Exp_DrugDosageStageView
	AS
SELECT DISTINCT ESD.Id
	,ESD.DosageId
    ,ESD.StageId AS DosageStageId
	,ESD.StartDate AS DosageStageStartDate
	,ESD.EndDate AS DosageStageEndDate
    ,DD.RegNumber AS DosageRegNumber
	,DD.BestBefore AS DosageBestBefore
	,DD.Dosage AS DosageDosageValue	
	,DD.SaleTypeId AS DosageSaleTypeId
	,DST.NameRu AS DosageSaleTypeName
	,DDMT.name AS DosageDosageName
	,DDMT.short_name AS DosageDosageShortName
	,DDBB.name AS DosageBestBeforeName
	,DDBB.short_name AS DosageBestBeforeShortName
    ,DDC.Id AS DeclarationId
    ,DDC.Number AS DeclarationNumber
    ,DDC.NameRu AS DeclarationNameRu
    ,DDC.CreatedDate AS DeclarationCreatedDate
	,DDC.TypeId AS DeclarationTypeId
	,DDCT.NameRu AS DeclarationTypeNameRu
    ,DDS.Id AS StageId
    ,DDS.NameRu AS StageNameRu
    ,DDSR.Id AS ResultId
    ,DDSR.NameRu AS ResultNameRu
	,ESDR.ResultDate AS ResultDate
	,ESDR.ResultCreatorId AS ResultCreatorId
	,ECRE.LastName AS ResultCreatorLastName
	,ECRE.FirstName AS ResultCreatorFirstName
	,ECRE.MiddleName AS ResultCreatorMiddleName
	,ISNULL(ECRE.ShortName, ECRE.LastName) AS ResultCreatorShortName
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
	,DNTD.TypeNDId AS NtdId
	,DNTDD.NameRu AS NtdNameRu
	,ISNULL(PrevC.Count,0) AS PrevCommissionCount
FROM dbo.EXP_ExpertiseStageDosage AS ESD
	INNER JOIN dbo.EXP_DrugDosage AS DD ON DD.Id = ESD.DosageId	
	LEFT JOIN dbo.sr_measures AS DDBB ON DDBB.id = DD.BestBeforeMeasureTypeDicId
	LEFT JOIN dbo.sr_measures AS DDMT ON DDMT.id = DD.DosageMeasureTypeId
	LEFT JOIN dbo.EXP_DIC_SaleType AS DST ON DST.Id = DD.SaleTypeId
    INNER JOIN dbo.EXP_DrugDeclaration AS DDC ON DDC.Id = DD.DrugDeclarationId
	LEFT JOIN dbo.EXP_DIC_Type AS DDCT ON DDCT.Id = DDC.TypeId
    INNER JOIN dbo.EXP_ExpertiseStage AS ES ON ES.Id = ESD.StageId
	INNER JOIN dbo.EXP_DIC_Stage AS DDS ON DDS.Id = ES.StageId
	LEFT JOIN dbo.Dictionaries AS DDST ON DDST.Type = 'EXP_DocumentStatus' AND DDST.Id = ESD.FinalDocStatusId
    INNER JOIN dbo.CommissionDrugDosage AS CDD ON CDD.DrugDosageId = ESD.DosageId AND CDD.StageId = ESD.StageId
    INNER JOIN dbo.Commissions AS C ON C.Id = CDD.CommissionId	
    LEFT JOIN 
		(
			SELECT PRCOMDD.CommissionId, PRCOMDD.StageId, PRCOMDD.DrugDosageId, COUNT(1) AS Count
			FROM dbo.Exp_DrugDosageStagePrevCommissionView AS PRCOMDD
			GROUP BY PRCOMDD.CommissionId, PRCOMDD.StageId, PRCOMDD.DrugDosageId
		) AS PrevC ON PrevC.CommissionId = C.Id AND PrevC.StageId = CDD.StageId AND PrevC.DrugDosageId = CDD.DrugDosageId
    LEFT JOIN dbo.CommissionConclusionTypes AS CCT ON CCT.Id = CDD.ConclusionTypeId
	LEFT JOIN dbo.EXP_ExpertiseStageDosageResult AS ESDR ON ESDR.Id = CDD.ExpertResultId
	LEFT JOIN dbo.Employees AS ECRE ON ECRE.Id = ESDR.ResultCreatorId
    LEFT JOIN dbo.EXP_DIC_StageResult AS DDSR ON DDSR.Id = ESDR.ResultId--ESD.ResultId

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
    LEFT JOIN dbo.EXP_DrugPrimaryNTD AS DNTD ON DNTD.DrugDeclarationId = DD.DrugDeclarationId
	LEFT JOIN dbo.EXP_DIC_TypeND AS DNTDD ON DNTDD.Id = DNTD.TypeNDId
GO

CREATE VIEW dbo.Exp_DrugDosageStagePrevCommissionConcatinateView
	AS
SELECT PrCom.DosageStageId
	,PrCom.PrevCommissions
FROM 
	(
		SELECT DISTINCT ST2.Id AS DosageStageId
			,substring(
				(
					SELECT ', Протокол №'+ T.DrevComFullNumber + ' '+ FORMAT( T.PrevComDate, 'dd.MM.yyyy') + ''  AS [text()]
					From dbo.Exp_DrugDosageStageView ST1
						LEFT JOIN dbo.Exp_DrugDosageStagePrevCommissionView AS T 
							ON T.CommissionId = ST1.CommissionId
								AND T.StageId = ST1.DosageStageId
								AND T.DrugDosageId = ST1.DosageId
					WHERE ST1.CommissionId = ST2.CommissionId
						AND ST1.DosageStageId = ST2.DosageStageId
						AND ST1.DosageId = ST2.DosageId
					ORDER BY ST1.CommissionId, T.StageId, T.DrugDosageId
					For XML PATH ('')
				), 2, 1000) AS PrevCommissions
		FROM dbo.Exp_DrugDosageStageView ST2
		WHERE ST2.PrevCommissionCount > 0
	) PrCom
GO