USE [ncels]
GO

/****** Object:  View [dbo].[EMP_ContractHistoryView]    Script Date: 20.12.2017 9:18:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[EMP_ContractHistoryView]
AS
SELECT [EMP_ContractHistory].[Id]
      ,[EMP_ContractHistory].[Created]
      ,[EMP_ContractHistory].[EmployeeId]
      ,[EMP_ContractHistory].[UnitName]
      ,[EMP_ContractHistory].[StatusId]
      ,[EMP_ContractHistory].[RefuseReason]
      ,[EMP_ContractHistory].[ContractId]
	  ,[Employees].[FullName] AS 'EmployeeFullName'
	  , CASE WHEN [Employees].[ShortName] IS NOT NULL THEN [Employees].[ShortName] 
			 ELSE [Employees].[DisplayName] END AS 'EmployeeShortName'
	  ,[OBK_Ref_ContractHistoryStatus].[Code] AS 'StatusCode'
	  ,[OBK_Ref_ContractHistoryStatus].[NameRu] AS 'StatusNameRu'
	  ,[OBK_Ref_ContractHistoryStatus].[NameKz] AS 'StatusNameKz'
FROM [EMP_ContractHistory]
LEFT JOIN [Employees] ON [Employees].[Id] = [EMP_ContractHistory].[EmployeeId]
LEFT JOIN [OBK_Ref_ContractHistoryStatus] ON [OBK_Ref_ContractHistoryStatus].[Id] = [EMP_ContractHistory].[StatusId]
GO


