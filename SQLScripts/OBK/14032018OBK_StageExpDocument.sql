  ALTER TABLE [ncels].[dbo].[OBK_StageExpDocument]
  ADD [ExpProductShortNameRu] NVARCHAR(MAX) NULL

  ALTER TABLE [ncels].[dbo].[OBK_StageExpDocument]
  ADD [ExpProductShortNameKz] NVARCHAR(MAX) NULL
  
  ALTER TABLE [ncels].[dbo].[UnitsAddress]
  ADD [PhoneNumber] NVARCHAR(50) NULL

  ALTER TABLE [ncels].[dbo].[UnitsAddress]
  ADD [Fax] NVARCHAR(100) NULL


  USE [ncels]
GO

/****** Object:  View [dbo].[OBK_DefectiveProductsReportView]    Script Date: 15.03.2018 10:56:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






	ALTER VIEW [dbo].[OBK_DefectiveProductsReportView]
	AS
	SELECT	 [OBK_StageExpDocument].[Id]
			,[OBK_StageExpDocument].[DecisionRefuseDate] AS [ExpStartDate]
			,[OBK_AssessmentDeclaration].[Number] AS [DeclarationNumber]
			,[OBK_AssessmentDeclaration].[SendDate]	
			,[OBK_Declarant].[NameRu] AS [Declarant] --Информация о Заявителе
			,[Units].[Name] AS [ExpertOrganization] 
			,[OBK_RS_Products].[RegNumber] AS [ExpConclusionNumber]
			,[OBK_RS_Products].[DrugFormFullName] AS [ProductInfo]--Наименование продукции
			,[OBK_RS_Products].[ProducerNameRu] +' '+ [OBK_RS_Products].[CountryNameRu] AS [ProducerName]
			,[OBK_StageExpDocument].[ExpReasonNameRu] AS [ExpReasonName]

	FROM [ncels].[dbo].[OBK_StageExpDocument]
	INNER JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
	INNER JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
	LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
	INNER JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_StageExpDocument].[ProductSeriesId] = [OBK_Procunts_Series].[Id]
	INNER JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_Procunts_Series].[OBK_RS_ProductsId] = [OBK_RS_Products].[Id]
	LEFT JOIN [ncels].[dbo].[Units] ON [OBK_Contract].[ExpertOrganization] = [Units].[Id]
	WHERE [OBK_StageExpDocument].[DecisionRefuse] = 1
GO


  ALTER TABLE [ncels].[dbo].[OBK_StageExpDocument]
  ADD [DecisionRefuseDate] DATETIME NULL


  USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ZBKRegisterView]    Script Date: 15.03.2018 11:46:58 ******/
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

UNION

SELECT '' AS [BlankNumber]
	  ,[OBK_StageExpDocument].[DecisionRefuseDate] AS [ExtraditeDate]
	  ,[OBK_StageExpDocument].[ExpConclusionNumber]
	  ,N'Решение об отказе' AS  [ZBKType]
	  ,[OBK_Declarant].[NameRu] AS [Declarant]
	  ,[OBK_RS_Products].[DrugFormFullName] AS [ProductName]
	  ,[OBK_StageExpDocument].[Id]
FROM [ncels].[dbo].[OBK_StageExpDocument]
LEFT JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_StageExpDocument].[ProductSeriesId] = [OBK_Procunts_Series].[Id]
LEFT JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_Procunts_Series].[OBK_RS_ProductsId] = [OBK_RS_Products].[Id]
WHERE [OBK_StageExpDocument].[DecisionRefuse] = 1

GO

USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ZBKTransferRegisterView]    Script Date: 15.03.2018 12:20:13 ******/
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

UNION

SELECT
	   [OBK_StageExpDocument].[Id]
	  ,[OBK_Declarant].[NameRu] AS [Declarant] 
	  ,[OBK_StageExpDocument].[ExpConclusionNumber] AS [ConclusionNumber]
	  ,[OBK_AssessmentDeclaration].[ExtraditeDate] AS [SendDate]
	  ,[OBK_RS_Products].[DrugFormFullName] 
	  ,N'Решение об отказе' AS [RequestType]
	  ,[OBK_AssessmentDeclaration].[ReceivedDate] AS [ExtraditeDate]
	  ,[OBK_AssessmentDeclaration].[ReceiverFIO]
	 
FROM [ncels].[dbo].[OBK_StageExpDocument] 
LEFT JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_StageExpDocument].[ProductSeriesId] = [OBK_Procunts_Series].[Id]
LEFT JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_Procunts_Series].[OBK_RS_ProductsId] = [OBK_RS_Products].[Id]
WHERE [OBK_StageExpDocument].[DecisionRefuse] = 1

GO





