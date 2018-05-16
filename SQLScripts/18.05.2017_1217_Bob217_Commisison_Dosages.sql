CREATE TABLE [dbo].[CommissionDrugDosageNeedCommission] (
  [Id] int IDENTITY(1, 1) NOT NULL,
  [DrugDosageId] bigint NOT NULL,
  [StageId] uniqueidentifier NOT NULL,
  [IsNeedEs] bit NOT NULL,
  [IsNeedFmk] bit NOT NULL,
  [IsNeedFmc] bit NOT NULL,
  CONSTRAINT [PK_CommissionDrugDosageNeedCommission] PRIMARY KEY CLUSTERED ([Id]),
  CONSTRAINT [CommissionDrugDosageNeedCommission_DrugDosageId_fk] FOREIGN KEY ([DrugDosageId]) 
  REFERENCES [dbo].[EXP_DrugDosage] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION,
  CONSTRAINT [CommissionDrugDosageNeedCommission_StageId_fk] FOREIGN KEY ([StageId]) 
  REFERENCES [dbo].[EXP_ExpertiseStage] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
)
ON [PRIMARY]
GO

CREATE VIEW dbo.Exp_DrugDosageStageForAddView
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
	,DNTD.TypeNDId AS NtdId
	,DNTDD.NameRu AS NtdNameRu
FROM dbo.EXP_ExpertiseStageDosage AS ESD
	INNER JOIN dbo.EXP_DrugDosage AS DD ON DD.Id = ESD.DosageId
    INNER JOIN dbo.EXP_DrugDeclaration AS DDC ON DDC.Id = DD.DrugDeclarationId
    INNER JOIN dbo.EXP_ExpertiseStage AS ES ON ES.Id = ESD.StageId
	INNER JOIN dbo.EXP_DIC_Stage AS DDS ON DDS.Id = ES.StageId
    LEFT JOIN dbo.EXP_DIC_StageResult AS DDSR ON DDSR.Id = ESD.ResultId
	LEFT JOIN dbo.Dictionaries AS DDST ON DDST.Type = 'EXP_DocumentStatus' AND DDST.Id = ESD.FinalDocStatusId
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
    LEFT JOIN dbo.EXP_DrugPrimaryNTD AS DNTD ON DNTD.DrugDeclarationId = DD.DrugDeclarationId
	LEFT JOIN dbo.EXP_DIC_TypeND AS DNTDD ON DNTDD.Id = DNTD.TypeNDId
GO

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
	,DNTD.TypeNDId AS NtdId
	,DNTDD.NameRu AS NtdNameRu
FROM dbo.EXP_ExpertiseStageDosage AS ESD
	INNER JOIN dbo.EXP_DrugDosage AS DD ON DD.Id = ESD.DosageId
    INNER JOIN dbo.EXP_DrugDeclaration AS DDC ON DDC.Id = DD.DrugDeclarationId
    INNER JOIN dbo.EXP_ExpertiseStage AS ES ON ES.Id = ESD.StageId
	INNER JOIN dbo.EXP_DIC_Stage AS DDS ON DDS.Id = ES.StageId
    LEFT JOIN dbo.EXP_DIC_StageResult AS DDSR ON DDSR.Id = ESD.ResultId
	LEFT JOIN dbo.Dictionaries AS DDST ON DDST.Type = 'EXP_DocumentStatus' AND DDST.Id = ESD.FinalDocStatusId
    INNER JOIN dbo.CommissionDrugDosage AS CDD ON CDD.DrugDosageId = ESD.DosageId AND CDD.StageId = ESD.StageId
    INNER JOIN dbo.Commissions AS C ON C.Id = CDD.CommissionId
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
    LEFT JOIN dbo.EXP_DrugPrimaryNTD AS DNTD ON DNTD.DrugDeclarationId = DD.DrugDeclarationId
	LEFT JOIN dbo.EXP_DIC_TypeND AS DNTDD ON DNTDD.Id = DNTD.TypeNDId
GO

--ALTER TABLE [dbo].[CommissionDrugDosage]
--ADD IsRepeat bit DEFAULT 0 NOT NULL
--GO
CREATE VIEW dbo.Exp_DrugDosageStagePrevCommissionView
	AS
SELECT DD.DrugDosageId
	,DD.StageId
	,DD.CommissionId
	,PrevC.Id AS DrevComId
	,PrevC.FullNumber AS DrevComFullNumber
	,PrevC.Date AS PrevComDate
FROM dbo.CommissionDrugDosage AS DD
	INNER JOIN dbo.Commissions AS C ON C.Id = DD.CommissionId
	INNER JOIN dbo.Commissions AS PrevC ON PrevC.TypeId = C.TypeId 
    	AND PrevC.Date < C.Date
	INNER JOIN dbo.CommissionDrugDosage AS PrevDD ON PrevDD.CommissionId = PrevC.Id 
		AND PrevDD.DrugDosageId = DD.DrugDosageId 
		AND PrevDD.StageId = DD.StageId
GO
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
	,DNTD.TypeNDId AS NtdId
	,DNTDD.NameRu AS NtdNameRu
	,ISNULL(PrevC.Count,0) AS PrevCommissionCount
FROM dbo.EXP_ExpertiseStageDosage AS ESD
	INNER JOIN dbo.EXP_DrugDosage AS DD ON DD.Id = ESD.DosageId
    INNER JOIN dbo.EXP_DrugDeclaration AS DDC ON DDC.Id = DD.DrugDeclarationId
    INNER JOIN dbo.EXP_ExpertiseStage AS ES ON ES.Id = ESD.StageId
	INNER JOIN dbo.EXP_DIC_Stage AS DDS ON DDS.Id = ES.StageId
    LEFT JOIN dbo.EXP_DIC_StageResult AS DDSR ON DDSR.Id = ESD.ResultId
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
    LEFT JOIN dbo.EXP_DrugPrimaryNTD AS DNTD ON DNTD.DrugDeclarationId = DD.DrugDeclarationId
	LEFT JOIN dbo.EXP_DIC_TypeND AS DNTDD ON DNTDD.Id = DNTD.TypeNDId
GO

ALTER TABLE [dbo].[CommissionDrugDosage]
ADD [ExpretResultId] int NULL
GO

ALTER TABLE [dbo].[CommissionDrugDosage]
ADD CONSTRAINT [CommissionDrugDosage_ExpertResultId_fk] FOREIGN KEY ([ExpretResultId]) 
  REFERENCES [dbo].[EXP_ExpertiseStageDosageResult] ([Id]) 
  ON UPDATE NO ACTION
  ON DELETE NO ACTION
GO

EXEC sp_rename '[dbo].[CommissionDrugDosage].[ExpretResultId]', 'ExpertResultId', 'COLUMN'
GO

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
	,ESDR.ResultDate AS ResultDate
	,ESDR.ResultCreatorId AS ResultCreatorId
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
	,DNTD.TypeNDId AS NtdId
	,DNTDD.NameRu AS NtdNameRu
	,ISNULL(PrevC.Count,0) AS PrevCommissionCount
FROM dbo.EXP_ExpertiseStageDosage AS ESD
	INNER JOIN dbo.EXP_DrugDosage AS DD ON DD.Id = ESD.DosageId
    INNER JOIN dbo.EXP_DrugDeclaration AS DDC ON DDC.Id = DD.DrugDeclarationId
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
	LEFT JOIN dbo.EXP_DIC_SaleType AS DST ON DST.Id = DDC.SaleTypeId
    LEFT JOIN dbo.EXP_DrugPrimaryNTD AS DNTD ON DNTD.DrugDeclarationId = DD.DrugDeclarationId
	LEFT JOIN dbo.EXP_DIC_TypeND AS DNTDD ON DNTDD.Id = DNTD.TypeNDId
GO

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
	,DDC.SaleTypeId AS DeclarationSaleTypeId
	,DST.NameRu AS DeclarationSaleTypeName
	,DNTD.TypeNDId AS NtdId
	,DNTDD.NameRu AS NtdNameRu
	,ISNULL(PrevC.Count,0) AS PrevCommissionCount
FROM dbo.EXP_ExpertiseStageDosage AS ESD
	INNER JOIN dbo.EXP_DrugDosage AS DD ON DD.Id = ESD.DosageId
    INNER JOIN dbo.EXP_DrugDeclaration AS DDC ON DDC.Id = DD.DrugDeclarationId
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
	LEFT JOIN dbo.EXP_DIC_SaleType AS DST ON DST.Id = DDC.SaleTypeId
    LEFT JOIN dbo.EXP_DrugPrimaryNTD AS DNTD ON DNTD.DrugDeclarationId = DD.DrugDeclarationId
	LEFT JOIN dbo.EXP_DIC_TypeND AS DNTDD ON DNTDD.Id = DNTD.TypeNDId
GO