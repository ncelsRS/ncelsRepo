USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ArchiveView]    Script Date: 12.02.2018 16:50:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








ALTER VIEW [dbo].[OBK_ArchiveView]
AS


SELECT 
	   [OBK_AssessmentDeclaration].Id
	  ,[OBK_AssessmentDeclaration].[Number] AS [DeclarationNumber]
	  ,[OBK_AssessmentDeclaration].[SendDate]
	  ,[OBK_Ref_Type].[NameRu] AS [RequestType]
	  ,[OBK_Declarant].[NameRu] AS [Declarant]
	  ,[Dictionaries].[Name] AS [Country]
	  ,[OBK_Contract].[Number] AS [ContractNumber]
	  ,(SELECT COUNT(*) FROM [OBK_RS_Products] WHERE [ContractId] = [OBK_AssessmentDeclaration].[ContractId]) AS [Quantity]
	  ,[OBK_StageExpDocument].[ExpConclusionNumber]
	  ,[OBK_BlankNumber].[Number] AS [BlankNumber]
	  ,[OBK_StageExpDocument].[ExpStartDate]  AS [ExtraditeDate]
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


