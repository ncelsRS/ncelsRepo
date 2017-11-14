USE [ncels]
GO

/****** Object:  View [dbo].[RequestListView]    Script Date: 10.04.2017 16:35:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[RequestListView]
AS
SELECT        
	req.Id
	, req.ReestrId
	, req.MnnName
	, req.Characteristic
	, req.DrugForm
	, req.Concentration
	, req.Dosage
	, req.TradeName
	, req.RegNumber
	, req.RegDate
	, req.RegDateExpire
	, req.AtxCode
	, req.Manufacturer
	, req.Measure
	, req.Type
	, req.State
	, req.LimitPriceTn
	, req.LimitPriceMnn
	, req.Country
	, req.Number
	, req.Applicant
	, req.substance_count
	, req.unit_count
	, req.volume
	, req.dosage_comment
	, req.Mark
	, CASE WHEN prj.RegNumber IS NULL 
			THEN 'Нет' 
		WHEN prp.Id IS NOT NULL 
			THEN 'Отказ' 
			ELSE 'Да'
		END AS Status
	, (SELECT
			Name
		FROM
			dbo.Dictionaries
		WHERE
			(Id = prp.RejectReasonDicId)
	) AS Reason
	, prp.RegisterDfId
FROM            
	dbo.RequestList AS req 
	LEFT JOIN dbo.PriceProjects AS prj ON prj.RegNumber = req.RegNumber
	LEFT JOIN dbo.PriceRejectProjects as prp ON req.ReestrId=prp.RegisterId 
		AND req.RegisterDfId = prp.RegisterDfId


GO


