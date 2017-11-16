CREATE VIEW OBK_ContractHistoryView
AS
SELECT [OBK_ContractHistory].[Id]
      ,[OBK_ContractHistory].[Created]
      ,[OBK_ContractHistory].[EmployeeId]
      ,[OBK_ContractHistory].[UnitName]
      ,[OBK_ContractHistory].[StatusId]
      ,[OBK_ContractHistory].[RefuseReason]
      ,[OBK_ContractHistory].[ContractId]
	  ,[Employees].[FullName] AS 'EmployeeFullName'
	  , CASE WHEN [Employees].[ShortName] IS NOT NULL THEN [Employees].[ShortName] 
			 ELSE [Employees].[DisplayName] END AS 'EmployeeShortName'
	  ,[OBK_Ref_ContractHistoryStatus].[Code] AS 'StatusCode'
	  ,[OBK_Ref_ContractHistoryStatus].[NameRu] AS 'StatusNameRu'
	  ,[OBK_Ref_ContractHistoryStatus].[NameKz] AS 'StatusNameKz'
FROM [OBK_ContractHistory]
LEFT JOIN [Employees] ON [Employees].[Id] = [OBK_ContractHistory].[EmployeeId]
LEFT JOIN [OBK_Ref_ContractHistoryStatus] ON [OBK_Ref_ContractHistoryStatus].[Id] = [OBK_ContractHistory].[StatusId]

GO