CREATE VIEW dbo.CommissionDrugDosageCount2View
	AS
SELECT DDS.CommissionId AS CommissionId
	,DDS.CommissionConclusionTypeId AS ConclusionTypeId
	,IIF(DDS.PrevCommissionCount = 0, 0, 1) AS IsRepeat
    ,DDS.DeclarationTypeId AS TypeId
    ,COUNT(1) AS Count
FROM dbo.Exp_DrugDosageStageView AS DDS
GROUP BY DDS.CommissionId
	,DDS.CommissionConclusionTypeId
	,IIF(DDS.PrevCommissionCount = 0, 0, 1)
	,DDS.DeclarationTypeId
GO

CREATE VIEW dbo.CommissionDrugDosageCount3View
	AS
SELECT DISTINCT
    a.CommissionId,
	a.IsRepeat,
	a.ConclusionTypeId,
	TypeList = STUFF(
    	(
        	SELECT ', ' 
                  + CASE TypeId
                      WHEN 1 THEN 'Р'
                      WHEN 2 THEN 'ПР'
                      WHEN 3 THEN 'ВИ'
                      ELSE '?' 
                  END 
                  +' - '+ CONVERT(nvarchar(max), sum(Count))
            FROM dbo.CommissionDrugDosageCount2View t2
            WHERE  ConclusionTypeId = a.ConclusionTypeId
                AND CommissionId = a.CommissionId
                AND IsRepeat = a.IsRepeat
            GROUP BY ConclusionTypeId, TypeId
            FOR XML PATH('')
         )
             , 1, 1, '')
FROM dbo.CommissionDrugDosageCount2View AS a
  WHERE a.ConclusionTypeId IS NOT NULL
GO

ALTER TABLE [dbo].[CommissionConclusionTypes]
ADD [Name2] nvarchar(300) NULL
GO

UPDATE dbo.CommissionConclusionTypes
SET Name2 = 'Рекомендовано'
WHERE Code = 2

UPDATE dbo.CommissionConclusionTypes
SET Name2 = 'Не рекомендовано'
WHERE Code = 1

UPDATE dbo.CommissionConclusionTypes
SET Name2 = 'Направлено на Экспертный совет'
WHERE Code = 5

UPDATE dbo.CommissionConclusionTypes
SET Name2 = 'Направлено на повторное рассмотрение'
WHERE Code = 3

UPDATE dbo.CommissionConclusionTypes
SET Name2 = 'Снято с регистрации заявителем'
WHERE Code = 4
GO
CREATE VIEW dbo.CommissionDrugDosageCount3WithoutRepeatView
	AS
SELECT DISTINCT
    a.CommissionId,
	a.ConclusionTypeId,
	TypeList = STUFF(
    	(
        	SELECT ', ' 
                  + CASE TypeId
                      WHEN 1 THEN 'Р'
                      WHEN 2 THEN 'ПР'
                      WHEN 3 THEN 'ВИ'
                      ELSE '?' 
                  END 
                  +' - '+ CONVERT(nvarchar(max), sum(Count))
            FROM dbo.CommissionDrugDosageCount2View t2
            WHERE ConclusionTypeId = a.ConclusionTypeId
                AND CommissionId = a.CommissionId
            GROUP BY ConclusionTypeId, TypeId
            FOR XML PATH('')
         )
             , 1, 1, '')
FROM dbo.CommissionDrugDosageCount2View AS a
  WHERE a.ConclusionTypeId IS NOT NULL
GO
 
 CREATE VIEW dbo.CommissionDrugDosageCount4WithoutRepeatView
	AS
SELECT C.CommissionId
	,C.TypeId
	,C.Count
	,T.Name AS TypeName
	,T.Name2 AS TypeName2
	,X.TypeList
FROM dbo.Commissions AS COM
    INNER JOIN 
    (
        SELECT CD.ConclusionTypeId AS TypeId	
            , CD.CommissionId AS CommissionId
            ,COUNT(1) AS Count
        FROM dbo.CommissionDrugDosage AS CD
        WHERE CD.ConclusionTypeId IS NOT NULL            
        GROUP BY CommissionId, CD.ConclusionTypeId--, D.StatusId
    ) AS C ON C.CommissionId = COM.Id
    INNER JOIN dbo.CommissionConclusionTypes AS T ON T.Id = C.TypeId
    INNER JOIN 
        (
            SELECT a.CommissionId
            	,a.ConclusionTypeId
                ,a.TypeList
            FROM dbo.CommissionDrugDosageCount3WithoutRepeatView AS a
        ) AS X ON X.ConclusionTypeId = C.TypeId
            AND X.CommissionId = COM.Id
GO

CREATE VIEW dbo.CommissionDrugDosageCount4View
	AS
SELECT C.CommissionId
	,C.TypeId
    ,C.IsRepeat
	,C.Count
	,T.Name AS TypeName
	,T.Name2 AS TypeName2
	,X.TypeList
FROM dbo.Commissions AS COM
    INNER JOIN 
    (
        SELECT CD.CommissionConclusionTypeId AS TypeId	
            , CD.CommissionId AS CommissionId
            , IIF(CD.PrevCommissionCount = 0, 0, 1) AS IsRepeat
            ,COUNT(1) AS Count
        FROM dbo.Exp_DrugDosageStageView AS CD
        WHERE CD.CommissionConclusionTypeId IS NOT NULL            
        GROUP BY CD.CommissionId, CD.CommissionConclusionTypeId, IIF(CD.PrevCommissionCount = 0, 0, 1)
    ) AS C ON C.CommissionId = COM.Id
    INNER JOIN dbo.CommissionConclusionTypes AS T ON T.Id = C.TypeId
    INNER JOIN 
        (
            SELECT a.CommissionId
            	,a.ConclusionTypeId
                ,a.IsRepeat
                ,a.TypeList
            FROM dbo.CommissionDrugDosageCount3View AS a
        ) AS X ON X.ConclusionTypeId = C.TypeId
            AND X.CommissionId = COM.Id
            AND X.IsRepeat = C.IsRepeat
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
    ,CDD.ConclusionComment AS CommissionConclusionComment
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