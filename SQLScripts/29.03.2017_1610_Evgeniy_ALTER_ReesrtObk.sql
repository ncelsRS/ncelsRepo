USE [ncels]
GO

/****** Object:  View [dbo].[ReesrtObk]    Script Date: 29.03.2017 15:46:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[ReesrtObk]
AS
SELECT
	prd.id, 
	prd.name, 
	prd.producer_name, 
	prd.country_name, 
	prd.tnved_code, 
	prd.kpved_code, 
	prd.register_nd, 
	cost.cost, 
	curr.currency_name,
	(SELECT 
		TOP (1) exch.rate
	 FROM            
		dbo.obk_exchangerate AS exch
	 WHERE        
		(exch.currency_id = cost.currency_id) 
		AND (exch.rate_date = CAST(cost.date_cost AS date))
	 ORDER BY 
		rate_date DESC
	 ) * cost.cost AS costExch,
	 prd.register_id,
	 prd.drug_form_id,
	 crt.reg_date
FROM            
	dbo.obk_products AS prd 
	LEFT JOIN dbo.obk_product_cost AS cost ON cost.id = prd.id 
	LEFT JOIN dbo.obk_currencies AS curr ON curr.id = cost.currency_id
	LEFT JOIN dbo.obk_certifications as crt on crt.product_id=prd.id


GO