USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ZBKRegisterView]    Script Date: 05.02.2018 9:46:20 ******/
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
AND [OBK_BlankNumber].[Corrupted] != 1

UNION

SELECT CONVERT(varchar(50), [OBK_BlankNumber].[Number]) AS [BlankNumber]
	  ,[OBK_ZBKCopy].[ExtraditeDate] AS [ExtraditeDate]
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
AND [OBK_BlankNumber].[Corrupted] != 1
GO


USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ZBKTransferRegisterView]    Script Date: 05.02.2018 9:46:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






ALTER VIEW [dbo].[OBK_ZBKTransferRegisterView]
AS
SELECT
	   [OBK_BlankNumber].[Id]
	  ,[OBK_Declarant].[NameRu] AS [Declarant] 
	  ,[OBK_StageExpDocument].[ExpConclusionNumber] AS [ConclusionNumber]
	  ,[OBK_AssessmentDeclaration].[ExtraditeDate] AS [SendDate]
	  ,[OBK_RS_Products].[DrugFormFullName] 
	  ,[OBK_BlankType].[NameRu] AS [RequestType]
	  ,[OBK_AssessmentDeclaration].[ReceivedDate] AS [ExtraditeDate]
	  ,[OBK_AssessmentDeclaration].[ReceiverFIO]
	 
FROM [ncels].[dbo].[OBK_BlankNumber]
LEFT JOIN [ncels].[dbo].[OBK_BlankType] ON [OBK_BlankNumber].[BlankTypeId] = [OBK_BlankType].[Id]
LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] ON [OBK_BlankNumber].[Object_Id] = [OBK_StageExpDocument].[Id]
LEFT JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_StageExpDocument].[ProductSeriesId] = [OBK_Procunts_Series].[Id]
LEFT JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_Procunts_Series].[OBK_RS_ProductsId] = [OBK_RS_Products].[Id]
WHERE [OBK_BlankType].[Code] IN ('ZBK','Application') AND [OBK_StageExpDocument].[ExpConclusionNumber] IS NOT NULL
AND [OBK_AssessmentDeclaration].[ExtraditeDate] IS NOT NULL AND [OBK_BlankNumber].[Corrupted] != 1

UNION

SELECT
	   [OBK_BlankNumber].[Id] 
	  ,[OBK_Declarant].[NameRu] AS [Declarant] 
	  ,[OBK_StageExpDocument].[ExpConclusionNumber] AS [ConclusionNumber]
      ,[OBK_ZBKCopy].[zbkCopyReadyDate] AS [SendDate]
	  ,[OBK_RS_Products].[DrugFormFullName] 
	  ,[OBK_BlankType].[NameRu] AS [RequestType]
	  ,[OBK_ZBKCopy].[ExtraditeDate]
	  ,[OBK_ZBKCopy].[ReceiverFIO]
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
AND [OBK_BlankNumber].[Corrupted] != 1
GO


USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ZBKArchiveView]    Script Date: 05.02.2018 9:47:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER VIEW [dbo].[OBK_ZBKArchiveView]
AS

SELECT  [OBK_StageExpDocument].[ExpConclusionNumber]
	   ,[OBK_ZBKCopy].[ReceiverFIO]
	   ,[OBK_ZBKCopy].[CopyQuantity]
	   ,[OBK_ZBKCopy].[ExtraditeDate]
	   ,[Employees].[DisplayName]
	   ,[OBK_StageExpDocument].[AssessmentDeclarationId]
	   ,[OBK_ZBKCopyStage].[Id]
FROM [ncels].[dbo].[OBK_ZBKCopy]
INNER JOIN [ncels].[dbo].[OBK_StageExpDocument] ON [OBK_ZBKCopy].[OBK_StageExpDocumentId] = [OBK_StageExpDocument].[Id]
INNER JOIN [ncels].[dbo].[OBK_ZBKCopyStage] ON [OBK_ZBKCopy].[Id] = [OBK_ZBKCopyStage].[OBK_ZBKCopyId] AND [OBK_ZBKCopyStage].[StageId] = 2
INNER JOIN [ncels].[dbo].[OBK_ZBKCopyStageExecutors] ON  [OBK_ZBKCopyStage].[Id] = [OBK_ZBKCopyStageExecutors].ZBKCopyStageId
INNER JOIN [ncels].[dbo].[Employees] ON [Employees].[Id] = [OBK_ZBKCopyStageExecutors].[ExecutorId]

GO


