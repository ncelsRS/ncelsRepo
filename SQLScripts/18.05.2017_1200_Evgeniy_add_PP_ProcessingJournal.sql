USE [ncels]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[PP_ProcessingJournal]
AS

select
	pp.Id,
	pp.Type,
	pp.Status,
	(CASE WHEN doc.Number IS NOT NULL THEN doc.Number ELSE 'б/н' END) AS Number,
	pp.Number as IncomeNumber,
	pp.CreatedDate,
	pp.ProxyOrganizationId,
	po.NameRu AS ProxyOrgName,
	pp.ManufacturerOrganizationId,
	mo.NameRu AS ManufacturerOrgName,
	pp.RegNumber,
	pp.MnnRu, 
	pp.MnnEn,
	pp.NameOriginal,
	cast(0 as bit) as HasEcp,
	cast(null as datetime) as EndExecDate
from
	PriceProjects as pp
	left join Documents AS doc ON doc.Id = pp.Id
	left join Organizations AS po ON po.Id = pp.ProxyOrganizationId
	left join Organizations AS mo ON mo.Id = pp.ManufacturerOrganizationId

GO