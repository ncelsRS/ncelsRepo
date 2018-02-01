USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ArchiveView]    Script Date: 30.01.2018 19:34:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[OBK_ArchiveView]
AS

SELECT 
	   [OBK_AssessmentDeclaration].Id
	  ,[OBK_AssessmentDeclaration].[Number] AS [DeclarationNumber]
	  ,[OBK_AssessmentDeclaration].[SendDate]
	  ,[OBK_Ref_Type].[NameRu] AS [RequestType]
	  ,[OBK_Declarant].[NameRu] AS [Declarant]
	  ,[Dictionaries].[Name] AS [Country]
	  ,[OBK_Contract].[Number] AS [ContractNumber]
	  ,[OBK_Procunts_Series].[Quantity]
	  ,[OBK_StageExpDocument].[ExpConclusionNumber]
	  ,[OBK_BlankNumber].[Number] AS [BlankNumber]
	  ,[OBK_AssessmentDeclaration].[ExtraditeDate]
	  ,[OBK_RS_Products].[Id] AS [ProductId]
	  ,[OBK_RS_Products].[DrugFormFullName] AS [Product]
FROM [ncels].[dbo].[OBK_AssessmentDeclaration]
INNER JOIN [ncels].[dbo].[OBK_Ref_Type] ON [OBK_AssessmentDeclaration].[TypeId] = [OBK_Ref_Type].[Id]
INNER JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id] 
LEFT JOIN [ncels].[dbo].[Dictionaries] ON [OBK_Declarant].[CountryId] = [Dictionaries].[Id]
LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] ON [OBK_StageExpDocument].AssessmentDeclarationId = [OBK_AssessmentDeclaration].[Id]
LEFT JOIN [ncels].[dbo].[OBK_BlankNumber] ON [OBK_BlankNumber].[Object_Id] = [OBK_StageExpDocument].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_StageExpDocument].[ProductSeriesId] = [OBK_Procunts_Series].[Id]
LEFT JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_Procunts_Series].[OBK_RS_ProductsId] = [OBK_RS_Products].[Id]
WHERE [OBK_StageExpDocument].[ExpConclusionNumber] IS NOT NULL


GO



USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ZBKArchiveView]    Script Date: 30.01.2018 19:34:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[OBK_ZBKArchiveView]
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


