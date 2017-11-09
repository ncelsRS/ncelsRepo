SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER VIEW [dbo].[PriceProjectArchiveView]
AS
select 
	pp.Id,
	ppe.MnnCode,
	pp.MnnRu,
	ppe.DrugDescription,
	pp.NameRu as DrugName,
	pp.CountPackage,
	prod.NameRu as ProducerName,
	d_p_c.Name as ProducerCountry,
	pp.RegNumber,
	pp.RegDate,
	ppe.RegEndDate,
	proxy.NameRu as ProxyName,
	ppe.RequesterContacts,
	pp.LsTypeDicId,
	d_lsType.Name as LsTypeName,
	ppe.MarginalPriceTn622,
	p_b.UnitPrice as BasePrice,
	p_b.RequestDate as BasePriceDate,
	p_t.UnitPrice as TalkPrice,
	p_t.RequestDate as TalkPriceDate,
	p_b.BritishPrice,
	p_b.LimitCost,
	ppe.MinRefPrice2016,
	ppe.FinalPrice,
	ppe.FinalFixPrice,
	ppe.FinalMarginalPriceTn,
	pp.CreatedDate,
	pp.RequestOrderType,
	pp.RequestOrderYear
from 
	PriceProjects as pp
	inner join PriceProject_Ext as ppe on ppe.Id=pp.Id
	left join Prices as p_b on p_b.PriceProjectId=pp.id	
		and p_b.Type=0
	left join Prices as p_t on p_t.PriceProjectId=pp.id	
		and p_t.Type=8
	left join Organizations as prod on prod.Id = pp.ManufacturerOrganizationId
	left join Organizations as proxy on proxy.Id = pp.ProxyOrganizationId
	left join Dictionaries as d_p_c on d_p_c.Id = prod.CountryDicId
	left join Dictionaries as d_lsType on d_lsType.Id = pp.LsTypeDicId
where 
	pp.IsArchive=1



GO


