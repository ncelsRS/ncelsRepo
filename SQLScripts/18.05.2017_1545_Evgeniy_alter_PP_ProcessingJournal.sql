USE [ncels]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PP_ProcessingJournal]
AS

select
	pp.Id,
	pp.Type,
	pp.Status,
	case when pp.Status = 0 or pp.Status = 1 then 1
		else case when pp.Status >1and pp.Status < 5 then 2
			else case when pp.Status = 6 then 3
				else case when pp.Status = 5 then 4 
				end
			end
		end 
	end as SelectStatus,
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
	pp.NameRu as TradeName,
	cast(0 as bit) as HasSign,
	cast(null as datetime) as EndExecDate
from
	PriceProjects as pp
	left join Documents AS doc ON doc.Id = pp.Id
	left join Organizations AS po ON po.Id = pp.ProxyOrganizationId
	left join Organizations AS mo ON mo.Id = pp.ManufacturerOrganizationId
where
	pp.IsArchive=0


GO


