USE [ncels]

SELECT 
	'КОЛИЧЕСТВО ПОДАННЫХ ЗАЯВЛЕНИЙ ВСЕГО' as n
	, cast(count(*) as nvarchar(20)) as v
FROM 
	PriceProjectJournal as p
	LEFT JOIN Documents as d ON p.id=d.id
WHERE
	Status > 0
	AND d.IsArchive IS NOT NULL

UNION ALL

SELECT 
	ISNULL(e.FullName, '-') as n, COUNT(*) as v
FROM 
	PriceProjectJournal as p
	LEFT JOIN Documents as d ON p.id=d.id
	LEFT JOIN Tasks as t ON t.DocumentId = d.Id
	LEFT JOIN Employees as e ON e.Id = t.ExecutorId
WHERE
	Status > 0
	AND d.IsArchive IS NOT NULL
GROUP BY e.FullName

UNION ALL

SELECT 
	'КОЛИЧЕСТВО ПОДАННЫХ ЗАЯВЛЕНИЙ(ЛС)' as n
	, cast(count(*) as nvarchar(20)) as v
FROM 
	PriceProjectJournal as p
	LEFT JOIN Documents as d ON p.id=d.id
WHERE
	Type <> 1
	AND Status > 0
	AND d.IsArchive IS NOT NULL

UNION ALL

SELECT 
	ISNULL(e.FullName, '-') as n, COUNT(*) as v
FROM 
	PriceProjectJournal as p
	LEFT JOIN Documents as d ON p.id=d.id
	LEFT JOIN Tasks as t ON t.DocumentId = d.Id
	LEFT JOIN Employees as e ON e.Id = t.ExecutorId
WHERE
	p.Type <> 1
	AND Status > 0
	AND d.IsArchive IS NOT NULL
GROUP BY e.FullName

UNION ALL
SELECT 
	'КОЛИЧЕСТВО ПОДАННЫХ ЗАЯВЛЕНИЙ(ИМН)' as n
	, cast(count(*) as nvarchar(20)) as v
FROM 
	PriceProjectJournal as p
	LEFT JOIN Documents as d ON p.id=d.id
WHERE
	Type = 1
	AND Status > 0
	AND d.IsArchive IS NOT NULL

UNION ALL

SELECT 
	ISNULL(e.FullName, '-') as n, COUNT(*) as v
FROM 
	PriceProjectJournal as p
	LEFT JOIN Documents as d ON p.id=d.id
	LEFT JOIN Tasks as t ON t.DocumentId = d.Id
	LEFT JOIN Employees as e ON e.Id = t.ExecutorId
WHERE
	p.Type = 1
	AND Status > 0
	AND d.IsArchive IS NOT NULL
GROUP BY e.FullName

UNION ALL

SELECT 
	'КОЛИЧЕСТВО ЗАЯВИТЕЛЕЙ ВСЕГО' as n
	, cast(count(*) as nvarchar(20)) as v
FROM 
	aspnet_Users as u
UNION ALL
SELECT 
	'КОЛИЧЕСТВО ВОЗВРАТА НА ДОРАБОТКУ ЗАЯВИТЕЛЮ' as n
	, cast(count(*) as nvarchar(20)) as v
FROM 
	PriceProjectJournal as p
	LEFT JOIN Documents as d ON p.id=d.id
WHERE
	Status = 4
	AND d.IsArchive IS NOT NULL

UNION ALL

SELECT 
	ISNULL(e.FullName, '-') as n, COUNT(*) as v
FROM 
	PriceProjectJournal as p
	LEFT JOIN Documents as d ON p.id=d.id
	LEFT JOIN Tasks as t ON t.DocumentId = d.Id
	LEFT JOIN Employees as e ON e.Id = t.ExecutorId
WHERE
	Status = 4
	AND d.IsArchive IS NOT NULL
GROUP BY e.FullName

UNION ALL

SELECT 
	'КОЛИЧЕСТВО ОТКАЗАННЫХ ЗАЯВЛЕНИЙ' as n
	, '-' as v

UNION ALL

SELECT 
	'КОЛИЧЕСТВО ЗАЯВЛЕНИЙ СО СТАТУСОМ «ПЕРЕГОВОРЫ»' as n
	, cast(count(*) as nvarchar(20)) as v
FROM 
	PriceProjectJournal as p
	LEFT JOIN Documents as d ON p.id=d.id
WHERE
	Status = 3
	AND d.IsArchive IS NOT NULL

UNION ALL

SELECT 
	ISNULL(e.FullName, '-') as n, COUNT(*) as v
FROM 
	PriceProjectJournal as p
	LEFT JOIN Documents as d ON p.id=d.id
	LEFT JOIN Tasks as t ON t.DocumentId = d.Id
	LEFT JOIN Employees as e ON e.Id = t.ExecutorId
WHERE
	Status = 3
	AND d.IsArchive IS NOT NULL
GROUP BY e.FullName

UNION ALL

SELECT 
	'КОЛИЧЕСТВО ЗАЯВЛЕНИЙ СО СТАТУСОМ «В РАБОТЕ»' as n
	, cast(count(*) as nvarchar(20)) as v
FROM 
	PriceProjectJournal as p
	LEFT JOIN Documents as d ON p.id=d.id
WHERE
	Status = 2
	AND d.IsArchive IS NOT NULL

UNION ALL

SELECT 
	ISNULL(e.FullName, '-') as n, COUNT(*) as v
FROM 
	PriceProjectJournal as p
	LEFT JOIN Documents as d ON p.id=d.id
	LEFT JOIN Tasks as t ON t.DocumentId = d.Id
	LEFT JOIN Employees as e ON e.Id = t.ExecutorId
WHERE
	Status = 2
	AND d.IsArchive IS NOT NULL
GROUP BY e.FullName

UNION ALL

SELECT 
	'КОЛИЧЕСТВО ЗАЯВЛЕНИЙ СО СТАТУСОМ «АННУЛИРОВАНО»' as n
	, cast(count(*) as nvarchar(20)) as v
FROM 
	PriceProjectJournal as p
	LEFT JOIN Documents as d ON p.id=d.id
WHERE
	d.IsArchive = 1

UNION ALL

SELECT 
	ISNULL(e.FullName, '-') as n, COUNT(*) as v
FROM 
	PriceProjectJournal as p
	LEFT JOIN Documents as d ON p.id=d.id
	LEFT JOIN Tasks as t ON t.DocumentId = d.Id
	LEFT JOIN Employees as e ON e.Id = t.ExecutorId
WHERE
	d.IsArchive = 1
GROUP BY e.FullName

UNION ALL

SELECT 
	'КОЛИЧЕСТВО ЗАЯВЛЕНИЙ НА ФОРМУЛЯРНОЙ КОМИССИИ' as n
	, cast(count(*) as nvarchar(20)) as v
FROM 
	PriceProjectJournal as p
	LEFT JOIN Documents as d ON p.id=d.id
WHERE
	Status = 5
	AND d.IsArchive IS NOT NULL

UNION ALL

SELECT 
	ISNULL(e.FullName, '-') as n, COUNT(*) as v
FROM 
	PriceProjectJournal as p
	LEFT JOIN Documents as d ON p.id=d.id
	LEFT JOIN Tasks as t ON t.DocumentId = d.Id
	LEFT JOIN Employees as e ON e.Id = t.ExecutorId
WHERE
	Status = 5
	AND d.IsArchive IS NOT NULL
GROUP BY e.FullName

UNION ALL

SELECT 
	'КОЛИЧЕСТВО ЗАВЕРШЕННЫХ ЗАЯВЛЕНИЙ В РАМКАХ ГОБМП' as n
	, cast(count(*) as nvarchar(20)) as v
FROM 
	RequestList AS req 
	LEFT JOIN PriceProjects AS prj ON prj.RegNumber = req.RegNumber
WHERE 
	req.type=1
	and prj.RegNumber IS NOT NULL
	and Status > 0