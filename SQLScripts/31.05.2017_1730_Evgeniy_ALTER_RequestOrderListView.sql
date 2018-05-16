SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[RequestOrderListView]
AS
SELECT        
	ro.Id as OrderId
	, ro.OrderType
	, ro.OrderYear
	, ro.OrderNumber
	, req.Id
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
			THEN N'Нет' 
		WHEN prp.Id IS NOT NULL 
			THEN N'Отказ' 
			ELSE N'Да'
		END AS Status
	, (SELECT
			Name
		FROM
			dbo.Dictionaries
		WHERE
			(Id = prp.RejectReasonDicId)
	) AS Reason
	, req.RegisterDfId
FROM    
	dbo.RequestOrders as ro        
	INNER JOIN dbo.RequestList AS req on ro.Id = req.RequestOrderId
	LEFT JOIN dbo.PriceProjects AS prj ON prj.RegisterId = req.ReestrId
		AND prj.RegisterDfId = req.RegisterDfId
	LEFT JOIN dbo.PriceRejectProjects as prp ON req.ReestrId=prp.RegisterId 
		AND req.RegisterDfId = prp.RegisterDfId


GO


