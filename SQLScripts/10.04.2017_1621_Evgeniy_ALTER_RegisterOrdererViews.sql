USE [ncels]
GO

/****** Object:  View [dbo].[RegisterOrdererView]    Script Date: 10.04.2017 16:19:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[RegisterOrdererView]
AS
SELECT
	r.reestr AS ReestrId
	, r.MNN_NAME AS MnnName
	, (
		CASE WHEN r.PHARM_FORM_NAME IS NOT NULL 
			THEN r.PHARM_FORM_NAME 
			ELSE '' 
		END) 
		+ ' ' + (
		CASE WHEN r.CONC_NAME IS NOT NULL 
			THEN r.CONC_NAME 
			ELSE '' 
		END)
		+ ' ' + (
		CASE WHEN r.dose_union IS NOT NULL 
			THEN r.dose_union 
			ELSE '' 
		END
	) AS Characteristic
	, r.PHARM_FORM_NAME AS DrugForm
	, r.CONC_NAME AS Concentration
	, r.dose_union AS Dosage
	, r.PHARM_NAMES_NAME AS TradeName
	, r.registration_number AS RegNumber
	, r.reg_date AS RegDate
	, r.val_date AS RegDateExpire
	, r.ats_code AS AtxCode
	, r.FIRM_NAME AS Manufacturer
	, r.DOSES_UNIT_NAME AS Measure
	, r.COUNTRY_NAME AS Country
	, srs.substance_count
	, srb.unit_count
	, srb.volume
	, srd.dosage_comment
FROM
	dbo.sr_register_ordered AS r 
	LEFT OUTER JOIN dbo.sr_register_substances AS srs ON srs.register_id = r.reestr 
	LEFT OUTER JOIN dbo.sr_register_boxes AS srb ON srb.register_id = r.reestr 
	LEFT OUTER JOIN dbo.sr_register_drugs AS srd ON srd.id = r.reestr

GO


