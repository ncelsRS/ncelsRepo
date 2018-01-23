USE [ncels]
GO

/****** Object:  View [dbo].[OBK_BlankAccountingView]    Script Date: 22.01.2018 19:29:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER VIEW  [dbo].[OBK_BlankAccountingView]

AS

SELECT [OBK_BlankNumber].[Id]
      ,[OBK_BlankNumber].[Number]	
	  ,(CASE WHEN [OBK_BlankNumber].[Corrupted] = 1 THEN [OBK_BlankNumber].[CorruptDate] 
		WHEN [OBK_BlankNumber].[Corrupted] = 0 THEN [OBK_ZBKCopy].[ExtraditeDate] END) AS [RegisterDate]
	  ,[OBK_StageExpDocument].[ExpConclusionNumber]
	  ,[OBK_Declarant].[NameRu] AS [Declarant]
	  ,(CASE WHEN [OBK_BlankNumber].[Corrupted] = 1 THEN [OBK_CorruptEmployee].[FullName] 
		WHEN [OBK_BlankNumber].[Corrupted] = 0 THEN [Employees].[FullName] END) AS [Executor]
	  ,[OBK_BlankType].[NameRu] AS [DocumentType]
	  ,(SELECT CASE WHEN [OBK_BlankNumber].[Corrupted] = 1 THEN N'Испорчен' 
	    WHEN [OBK_BlankNumber].[Corrupted] = 0 THEN N'Использован' END) AS [Sign] 
	  ,[Units].[Name] AS [OrganName] 
	  ,[OBK_BlankNumber].[Decommissioned]
	  ,[OBK_BlankNumber].[DecommissionedDate]
	  ,[OBK_BlankNumber].[Corrupted] 
  FROM [ncels].[dbo].[OBK_BlankNumber]
  LEFT JOIN [ncels].[dbo].[OBK_ZBKCopy] ON [OBK_BlankNumber].[Object_Id] = [OBK_ZBKCopy].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] ON [OBK_ZBKCopy].[OBK_StageExpDocumentId] = [OBK_StageExpDocument].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
  LEFT JOIN [ncels].[dbo].[Employees] ON [OBK_BlankNumber].[EmployeeId] = [Employees].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_BlankType] ON [OBK_BlankNumber].[BlankTypeId] = [OBK_BlankType].[Id]
  LEFT JOIN [ncels].[dbo].[Units] ON [OBK_Contract].[ExpertOrganization] = [Units].[Id]
  LEFT JOIN [ncels].[dbo].[Employees] AS [OBK_CorruptEmployee] ON [OBK_BlankNumber].[CorruptEmployee] = [OBK_CorruptEmployee].[Id]
  WHERE [OBK_BlankType].[Code] IN ('ZBKcopy','ZBKApplicationCopy') AND [OBK_ZBKCopy].[ExtraditeDate] IS NOT NULL

UNION ALL

SELECT [OBK_BlankNumber].[Id]
      ,[OBK_BlankNumber].[Number]	
	  ,(CASE WHEN [OBK_BlankNumber].[Corrupted] = 1 THEN [OBK_BlankNumber].[CorruptDate] 
		WHEN [OBK_BlankNumber].[Corrupted] = 0 THEN [OBK_StageExpDocument].[ExpStartDate] END) AS [RegisterDate]
	  ,[OBK_StageExpDocument].[ExpConclusionNumber]
	  ,[OBK_Declarant].[NameRu] AS [Declarant]
	  ,(CASE WHEN [OBK_BlankNumber].[Corrupted] = 1 THEN [OBK_CorruptEmployee].[FullName] 
		WHEN [OBK_BlankNumber].[Corrupted] = 0 THEN [Employees].[FullName] END) AS [Executor]
	  ,[OBK_BlankType].[NameRu] AS [DocumentType]
	  ,(SELECT CASE WHEN [OBK_BlankNumber].[Corrupted] = 1 THEN N'Испорчен' 
	    WHEN [OBK_BlankNumber].[Corrupted] = 0 THEN N'Использован' END) AS [Sign] 
	  ,[Units].[Name] AS [OrganName] 
	  ,[OBK_BlankNumber].[Decommissioned]
	  ,[OBK_BlankNumber].[DecommissionedDate]
	  ,[OBK_BlankNumber].[Corrupted] 
  FROM [ncels].[dbo].[OBK_BlankNumber]
  LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] ON [OBK_BlankNumber].[Object_Id] = [OBK_StageExpDocument].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
  LEFT JOIN [ncels].[dbo].[Employees] ON [OBK_BlankNumber].[EmployeeId] = [Employees].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_BlankType] ON [OBK_BlankNumber].[BlankTypeId] = [OBK_BlankType].[Id]
  LEFT JOIN [ncels].[dbo].[Units] ON [OBK_Contract].[ExpertOrganization] = [Units].[Id]
  LEFT JOIN [ncels].[dbo].[Employees] AS [OBK_CorruptEmployee] ON [OBK_BlankNumber].[CorruptEmployee] = [OBK_CorruptEmployee].[Id]
  WHERE [OBK_BlankType].[Code] IN ('ZBK','Application') AND [OBK_StageExpDocument].[ExpConclusionNumber] IS NOT NULL

GO


USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ZBKRegisterView]    Script Date: 22.01.2018 19:30:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER VIEW [dbo].[OBK_ZBKRegisterView]
AS
SELECT CONVERT(varchar(50), [OBK_BlankNumber].[Number]) AS [BlankNumber]
	  ,[OBK_StageExpDocument].[ExpStartDate] AS [ExtraditeDate]
	  ,[OBK_StageExpDocument].[ExpConclusionNumber]
	  ,[OBK_BlankType].NameRu AS  [ZBKType]
	  ,[OBK_Declarant].[NameRu] AS [Declarant]
	  ,[OBK_RS_Products].[DrugFormFullName] AS [ProductName]
	  ,[OBK_BlankNumber].[Id]
FROM [ncels].[dbo].[OBK_BlankNumber]
LEFT JOIN [ncels].[dbo].[OBK_BlankType] ON [OBK_BlankNumber].[BlankTypeId] = [OBK_BlankType].[Id]
LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] ON [OBK_BlankNumber].[Object_Id] = [OBK_StageExpDocument].[Id]
LEFT JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_StageExpDocument].[ProductSeriesId] = [OBK_Procunts_Series].[Id]
LEFT JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_Procunts_Series].[OBK_RS_ProductsId] = [OBK_RS_Products].[Id]
WHERE [OBK_BlankType].[Code] IN ('ZBK','Application') AND [OBK_StageExpDocument].[ExpConclusionNumber] IS NOT NULL

UNION

SELECT CONVERT(varchar(50), [OBK_BlankNumber].[Number]) AS [BlankNumber]
	  ,(SELECT CASE 
	   WHEN [OBK_BlankType].Code IN ('ZBKcopy', 'ZBKApplicationCopy') THEN [OBK_ZBKCopy].[ExtraditeDate] 
	   WHEN [OBK_BlankType].Code IN ('ZBK', 'Application') THEN [OBK_StageExpDocument].[ExpStartDate] END) AS [ExtraditeDate]
	  ,[OBK_StageExpDocument].[ExpConclusionNumber]
	  ,[OBK_BlankType].NameRu AS  [ZBKType]
	  ,[OBK_Declarant].[NameRu] AS [Declarant]
	  ,[OBK_RS_Products].[DrugFormFullName] AS [ProductName]
	  ,[OBK_BlankNumber].[Id]
FROM [ncels].[dbo].[OBK_BlankNumber]
LEFT JOIN [ncels].[dbo].[OBK_ZBKCopy] ON [OBK_BlankNumber].[Object_Id] = [OBK_ZBKCopy].[Id]
LEFT JOIN [ncels].[dbo].[OBK_BlankType] ON [OBK_BlankNumber].[BlankTypeId] = [OBK_BlankType].[Id]
LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] ON [OBK_ZBKCopy].[OBK_StageExpDocumentId] = [OBK_StageExpDocument].[Id]
LEFT JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_StageExpDocument].[ProductSeriesId] = [OBK_Procunts_Series].[Id]
LEFT JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_Procunts_Series].[OBK_RS_ProductsId] = [OBK_RS_Products].[Id]
WHERE [OBK_BlankType].[Code] IN ('ZBKcopy','ZBKApplicationCopy') AND [OBK_ZBKCopy].[ExtraditeDate] IS NOT NULL

GO

USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ZBKTransferRegisterView]    Script Date: 22.01.2018 19:31:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[OBK_ZBKTransferRegisterView]
AS
SELECT [OBK_ZBKCopy].[Id]
	  ,[OBK_Declarant].[NameRu] AS [Declarant]
	  ,[OBK_StageExpDocument].[ExpConclusionNumber] AS [ConclusionNumber]
	  ,[OBK_ZBKCopy].[SendDate] 
	  ,[OBK_StageExpDocument].[ExpProductNameRu] AS [DrugFormFullName]
	  ,N'Копия' AS [RequestType]
	  ,[OBK_ZBKCopy].[ExtraditeDate]
	  ,[OBK_ZBKCopy].[ReceiverFIO]
FROM [ncels].[dbo].[OBK_ZBKCopy]
LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] ON [OBK_ZBKCopy].[OBK_StageExpDocumentId] = [OBK_StageExpDocument].[Id]
LEFT JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
WHERE [ExtraditeDate] IS NOT NULL
GO


USE [ncels]
GO

/****** Object:  View [dbo].[OBK_CorruptedBlankNumberView]    Script Date: 22.01.2018 19:32:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[OBK_CorruptedBlankNumberView]
AS
SELECT [Id]
      ,[Number]
      ,[CorruptDate]
	  ,[CorruptEmployee]
  FROM [ncels].[dbo].[OBK_BlankNumber]
  WHERE [CorruptDate] IS NOT NULL AND [Corrupted] = 1

GO


