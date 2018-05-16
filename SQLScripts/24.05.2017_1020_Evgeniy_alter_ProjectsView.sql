USE [ncels]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[ProjectsView]
AS
SELECT
	pp.[Id]
	, pp.[Type]
	, d_t.Name AS TypeValue
	, doc.OutgoingNumber as Number
	, pp.[CreatedDate]
	, [Status]
	, d_s.Name AS StausValue
	, [NameRu]
	, cast(0 AS bit) AS 'IsRegisterProject'
	, pp.OwnerId
FROM            
	[ncels].[dbo].[PriceProjects] AS pp 
	LEFT JOIN Documents AS doc ON doc.Id = pp.Id 
	LEFT JOIN Dictionaries AS d_t ON d_t.Type = 'PriceProjectType' 
		AND d_t.Code = CAST(pp.[Type] AS nvarchar(max)) 
	LEFT JOIN Dictionaries AS d_s ON d_s.Type = 'PriceProjectStatus' 
		AND d_s.Code = CAST(pp.Status AS nvarchar(max))
WHERE pp.IsArchive=0

UNION ALL

SELECT        
	rp.[Id]
	, rp.[Type] AS TypeValue
	, d_t.Name
	, doc.OutgoingNumber as Number
	, rp.[CreatedDate]
	, [Status]
	, d_s.Name AS StausValue
	, [NameRu]
	, cast(1 AS bit) AS 'IsRegisterProject'
	, rp.OwnerId
FROM
	[ncels].[dbo].[RegisterProjects] AS rp 
	LEFT JOIN Documents AS doc ON doc.Id = rp.Id 
	LEFT JOIN Dictionaries AS d_t ON d_t.Type = 'RegisterProjectType' 
		AND d_t.Code = CAST(rp.[Type] AS nvarchar(max)) 
	LEFT JOIN Dictionaries AS d_s ON d_s.Type = 'RegisterProjectStatus' 
		AND d_s.Code = CAST(rp.Status AS nvarchar(max))


GO


