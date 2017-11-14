USE [ncels]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[PP_ProtocolListView]
AS

select 
	p.Id,
	CAST(1 AS BIT) as HasEdit,
	p.ProtocolDate,
	p.Number,
	case when Status = 2 then CAST(1 AS BIT) else CAST(0 AS BIT) end as IsFinal,
	p.Type,
	dt.Name as TypeName,
	p.OwnerId as UserId,
	e.FullName as OwnerName,
	p.RequesterName,
	p.Status,
	ds.Name as StatusName,
	p.IsDeleted,
	NULL as ChiefId
from 
	PP_Protocols as p
	left join Dictionaries as dt on dt.Type = 'PpProtocolType' and dt.Code=p.Type
	left join Dictionaries as ds on ds.Type = 'PpProtocolStatus' and ds.Code=p.Status
	left join Employees as e on p.OwnerId=e.Id
where 
	p.IsDeleted=0

union

select 
	p.Id,
	CAST(0 AS BIT) as HasEdit,
	p.ProtocolDate,
	p.Number,
	case when Status = 2 then CAST(1 AS BIT) else CAST(0 AS BIT) end as IsFinal,
	p.Type,
	dt.Name as TypeName,
	cm.EmployeeId as UserId,
	e.FullName as OwnerName,
	p.RequesterName,
	p.Status,
	ds.Name as StatusName,
	p.IsDeleted,
	p.ChiefId
from 
	PP_Protocols as p
	left join Dictionaries as dt on dt.Type = 'PpProtocolType' and dt.Code=p.Type
	left join Dictionaries as ds on ds.Type = 'PpProtocolStatus' and ds.Code=p.Status
	left join Employees as e on p.OwnerId=e.Id
	left join PP_ProtocolComissionMembers as cm on cm.ProtocolId = p.Id
		and cm.EmployeeId <> p.OwnerId
where 
	p.IsDeleted=0
	and (p.Status=1 or p.Status=2)


GO


