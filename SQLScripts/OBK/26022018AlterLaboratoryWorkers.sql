USE [ncels]
GO

/****** Object:  View [dbo].[OBK_laboratoryWorkers]    Script Date: 26.02.2018 18:42:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[OBK_laboratoryWorkers]
AS
SELECT 
 [Units].Id,
 [Employees].[FullName] + ' (' + [Units].[Name] + ') ' AS [Executor]
FROM [ncels].[dbo].[Units] 
INNER JOIN [ncels].[dbo].[Employees] ON [Employees].[Id] = [Units].[EmployeeId]
WHERE [Units].[ParentId] in 
	(SELECT [Id] FROM [ncels].[dbo].Units u WHERE u.[ParentId] IN 
		(SELECT [Id] FROM [ncels].[dbo].Units u WHERE u.Code='researchcenter'))
GO


USE [ncels]
GO
/****** Object:  UserDefinedFunction [dbo].[FindProductName]    Script Date: 27.02.2018 9:26:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
ALTER FUNCTION [dbo].[FindProductName] 
(
	-- Add the parameters for the function here
	@TaskId UNIQUEIDENTIFIER
)
RETURNS NVARCHAR(1000)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(4000);

	SELECT TOP 1 @ResultVar = [OBK_RS_Products].[DrugFormFullName] FROM [ncels].[dbo].[OBK_TaskMaterial] 
    LEFT JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_Procunts_Series].[Id] = [OBK_TaskMaterial].[ProductSeriesId]
    LEFT JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_RS_Products].[Id] = [OBK_Procunts_Series].[OBK_RS_ProductsId]
	WHERE [OBK_TaskMaterial].[TaskId] = @TaskId

	-- Return the result of the function
	RETURN @ResultVar;

END


