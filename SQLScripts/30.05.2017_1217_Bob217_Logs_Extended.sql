ALTER TABLE [dbo].[ActionLogs]
ADD [IpAddress] nvarchar(1000) NULL
GO
ALTER VIEW dbo.ActionLogsView
	AS
SELECT L.Id
	,L.[Date]
    ,L.PlaceId
    ,P.[Name] AS PlaceName
    ,L.[Text]
    ,L.AdditionalText
    ,L.[Type]
    ,L.IpAddress
    ,L.EmployeeId
    ,E.LastName AS EmployeeLastName
    ,E.FirstName AS EmployeeFirstName
    ,E.MiddleName AS EmployeeMiddleName    
    ,E.[Login] AS EmployeeLogin
FROM dbo.ActionLogs AS L
	LEFT JOIN dbo.Employees AS E ON E.Id = L.EmployeeId
    LEFT JOIN dbo.ActionLogPlaces AS P ON P.Id = L.PlaceId
GO