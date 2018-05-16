USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ZBKTransferRegisterView]    Script Date: 02.02.2018 14:25:29 ******/
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
AND [OBK_AssessmentDeclaration].[ExtraditeDate] IS NOT NULL

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

GO


