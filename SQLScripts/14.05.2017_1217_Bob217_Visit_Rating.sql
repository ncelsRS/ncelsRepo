ALTER TABLE [dbo].[Visits]
ADD [RatingValue] int NULL


EXEC sp_addextendedproperty 'MS_Description', N'Оценка сервиса', 'schema', 'dbo', 'table', 'Visits', 'column', 'RatingValue'
GO

ALTER TABLE [dbo].[Visits]
ADD [RatingComment] nvarchar(4000) NULL
GO


EXEC sp_addextendedproperty 'MS_Description', N'Оценка сервиса(Комментарий)', 'schema', 'dbo', 'table', 'Visits', 'column', 'RatingComment'
GO

ALTER VIEW dbo.VisitsView
	AS
SELECT V.Id AS VisitId
	,V.[Date] AS VisitDate
    ,V.Duration AS VisitDuration
    ,V.TimeBegin AS VisitTimeBegin
    ,V.Comment AS VisitComment
    ,V.VisitStatusId AS VisitStatusId
    ,V.RatingComment AS VisitRatingComment
    ,V.RatingValue AS VisitRatingValue
    ,VS.[Name] AS VisitStatusName
    ,V.VisitTypeId AS VisitTypeId
    ,VT.[Name] AS VisitTypeName
    ,VT.[Group] AS VisitTypeGroup     
    ,V.VisitorId aS VisitorId 
    ,VV.LastName AS VisitorLastName
    ,VV.FirstName AS VisitorFirstName
    ,VV.MiddleName AS VisitorMiddleName  
    ,V.EmployeeId AS EmployeeId
    ,VE.LastName AS EmployeeLastName
    ,VE.FirstName AS EmployeeFirstName
    ,VE.MiddleName AS EmployeeMiddleName    
FROM dbo.Visits AS V
	INNER JOIN dbo.Employees AS VV ON VV.Id = V.VisitorId
    INNER JOIN dbo.Employees AS VE ON VE.Id = V.EmployeeId
    INNER JOIN dbo.VisitTypes AS VT ON VT.Id = V.VisitTypeId
    INNER JOIN dbo.VisitStatuses AS VS ON VS.Id = V.VisitStatusId
GO