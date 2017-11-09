SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER view [dbo].[EXP_DrugDeclarationRegisterView] as
SELECT        
	d.Id, 
	d .Number, 
	d .TypeId, 
	t .NameRu AS TypeNameRu, 
	t .NameKz AS TypeNameKz, 
	d .NameRu, 
	d .NameKz, 
	d .NameEn, 
	d .FirstSendDate, 
	p.NameRu AS ProducerRu, 
	p.NameKz AS ProducerKz, 
    p.NameEn AS ProducerEn, 
	pk.NameRu AS PackerRu, 
	pk.NameKz AS PackerKz, 
	pk.NameEn AS PackerEn, 
	rc.NameRu ReleaseControlRu, 
	rc.nameKz ReleaseControlKz, 
	rc.NameEn ReleaseControlEn, 
    c.Id AS CountryId, 
	c.Name AS CountryRu, 
	c.NameKz AS CountryKz, 
	d .StatusId, 
	s.NameRu AS StatusRu, 
	s.NameKz AS StatusKz, 
	dt.DrugTypeRu, 
	dt.DrugTypeKz, 
	d .MnnId, 
	mnn.name_rus AS MnnRu, 
    mnn.name_kz AS MnnKz, 
	mnn.name_eng AS MnnEn, 
	df.name AS DrugFormRu, 
	df.name_kz AS DrugFormKz, 
	ds.DosageRu, 
	ds.DosageKz, 
	exs.Id AS StageId, 
	stage.NameRu AS StageRu, 
    stage.NameKz AS StageKz, 
	stages.Code StatusCode, 
	emp.Id AS ExpertId, 
	emp.ShortName AS ExpertInitials, 
	CONVERT(bit, exs.IsSuspended) Suspended,	
	CONVERT(bit, NULL) OnBoard, 
	CONVERT(bit, NULL) ProductionEvaluation, 
	exs.StartDate StageStartDate, 
    case when exs.IsSuspended=0 
		then exs.EndDate 
		else DATEADD(dd, DATEDIFF(dd,exs.SuspendedStartDate, CURRENT_TIMESTAMP), exs.EndDate) 
	end as StageControlDate, 
	CONVERT(datetime, NULL) ConclusionDate, 
	CONVERT(bit, IIF(stages.Code = '4', 1, 0)) StageCompleted,
	exs.FactEndDate StageEndDate, 
	CONVERT(bit, NULL) StageOverdue, 
    CONVERT(int, NULL) OverdueDays, 
	CONVERT(uniqueidentifier, NULL) LetterId, 
	CONVERT(nvarchar(250), NULL) LetterNumber, 
	CONVERT(int, NULL) StageDays, 
	CONVERT(bit, NULL) IsNewProducer, 
    dsub.ActiveSubstanceRu, 
	dsub.ActiveSubstanceKz, 
	dsub.SecondarySubstanceRu, 
	dsub.SecondarySubstanceKz,
    (SELECT
		COUNT(Id) AS Expr1
    FROM
		dbo.EXP_DrugDosage AS dd
    WHERE        
		(DrugDeclarationId = d .Id) 
		AND (Id IN 
			(SELECT
				DrugDosageId
             FROM
				dbo.EXP_DrugSubstance AS ds
             WHERE        
				(IsControl = 'true' 
				OR IsPoison = 'true')
	))) AS CountDosageIsControl, 
	app.NameRu AS ApplicantRu, 
	app.NameKz AS ApplicantKz, 
	app.NameEn AS ApplicantEn, 
	appc.Name AS ApplicantCountryRu, 
    appc.NameKz AS ApplicantCountryKz, 
	h.NameRu AS HolderRu, 
	h.NameKz AS HolderKz, 
	h.NameEn AS HolderEn, 
	hc.Name AS HolderCountryRu, 
	hc.NameKz AS HolderCountryKz, 
	exs.StageId as DicStageId,
	stage.Code StageCode, 
	stager.NameRu StageResultRu, 
	stager.NameKz StageResultKz,
	exs.SuspendedStartDate,
	convert(bit,iif(dp.Id is not null and dps.Code='6',1,0)) as Paid,
	dp.PaymentDatetime1C as PaymentDate,
    convert(bit,iif(dp.Id is not null and dps.Code='7',1,0)) as PaymentOverdue
FROM            
	EXP_DrugDeclaration d 
	INNER JOIN EXP_DIC_Type t ON t .Id = d .TypeId 
	INNER JOIN EXP_DIC_Status s ON s.Id = d .StatusId 
	LEFT JOIN (
		SELECT 
			p.*, 
			pt.Code ProducerTypeCode
        FROM            
			EXP_DrugOrganizations p 
			INNER JOIN Dictionaries pt ON pt.Id = p.OrgManufactureTypeDicId
		) p ON p.DrugDeclarationId = d .Id 
		AND p.ProducerTypeCode = '1' 
	LEFT JOIN Dictionaries c ON c.Id = p.CountryDicId 
	LEFT JOIN (
		SELECT        
			p.*, 
			pt.Code ProducerTypeCode
        FROM            
			EXP_DrugOrganizations p 
			INNER JOIN Dictionaries pt ON pt.Id = p.OrgManufactureTypeDicId
		) app ON app.DrugDeclarationId = d .Id 
		AND app.ProducerTypeCode = '5' 
	LEFT JOIN Dictionaries appc ON appc.Id = app.CountryDicId 
	LEFT JOIN (
		SELECT        
			p.*, 
			pt.Code ProducerTypeCode
        FROM            
			EXP_DrugOrganizations p 
			INNER JOIN Dictionaries pt ON pt.Id = p.OrgManufactureTypeDicId
		) h ON h.DrugDeclarationId = d .Id 
		AND h.ProducerTypeCode = '2' 
	LEFT JOIN Dictionaries hc ON hc.Id = h.CountryDicId 
	LEFT JOIN (
		SELECT        
			pk.*, 
			pt.Code ProducerTypeCode
        FROM            
			EXP_DrugOrganizations pk 
			INNER JOIN Dictionaries pt ON pt.Id = pk.OrgManufactureTypeDicId
		) pk ON pk.DrugDeclarationId = d .Id 
		AND pk.ProducerTypeCode = '4' 
	LEFT JOIN (
		SELECT        
			rc.*, 
			pt.Code ProducerTypeCode
        FROM            
			EXP_DrugOrganizations rc 
			INNER JOIN Dictionaries pt ON pt.Id = rc.OrgManufactureTypeDicId
		) rc ON rc.DrugDeclarationId = d .Id 
		AND rc.ProducerTypeCode = '7' 
	LEFT JOIN EXP_DrugTypeView dt ON dt.DrugDeclarationId = d .Id 
	LEFT JOIN sr_international_names mnn ON mnn.id = d .MnnId 
	LEFT JOIN sr_dosage_forms df ON df.id = d .DrugFormId 
	LEFT JOIN EXP_DrugDosageView ds ON ds.DrugDeclarationId = d .Id /*left join Dictionaries dps on dps.Id=dp.StatusId and dps.Code='6') dp on dp.DrugDeclarationId=d.Id*/ 
	LEFT JOIN EXP_DrugSubstanceView dsub ON dsub.DrugDeclarationId = d .Id 
	INNER JOIN EXP_ExpertiseStage exs ON exs.DeclarationId = d .Id 
	INNER JOIN EXP_ExpertiseStageExecutors exc ON exc.ExpertiseStageId = exs.Id 
	INNER JOIN Employees emp ON emp.Id = exc.ExecutorId 
	INNER JOIN EXP_DIC_Stage stage ON stage.Id = exs.StageId 
	INNER JOIN EXP_DIC_StageStatus stages ON stages.Id = exs.StatusId
	left join EXP_DIC_StageResult stager on stager.Id=exs.ResultId
	left join EXP_DirectionToPays_DrugDeclaration dpd on dpd.DrugDeclarationId=d.Id
    left join EXP_DirectionToPays dp on dp.Id=dpd.DirectionToPayId and dp.Type=1
	left join Dictionaries dps on dps.Id=dp.StatusId
where 
	exs.IsHistory=0
GO


