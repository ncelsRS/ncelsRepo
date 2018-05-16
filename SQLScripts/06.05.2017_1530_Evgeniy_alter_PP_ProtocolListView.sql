USE [ncels]
GO

/****** Object:  View [dbo].[PP_ProtocolListView]    Script Date: 06.05.2017 15:28:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER VIEW [dbo].[PP_ProtocolListView]
AS

select 
	p.Id,
	p.ProtocolDate,
	p.Number,
	case when Status = 2 then CAST(1 AS BIT) else CAST(0 AS BIT) end as IsFinal,
	p.Type,
	dt.Name as TypeName,
	p.OwnerId,
	e.FullName as OwnerName,
	p.RequesterName,
	p.Status,
	ds.Name as StatusName,
	p.IsDeleted
from 
	PP_Protocols as p
	left join Dictionaries as dt on dt.Type = 'PpProtocolType' and dt.Code=p.Type
	left join Dictionaries as ds on ds.Type = 'PpProtocolStatus' and ds.Code=p.Status
	left join Employees as e on p.OwnerId=e.Id
where p.IsDeleted=0


GO


