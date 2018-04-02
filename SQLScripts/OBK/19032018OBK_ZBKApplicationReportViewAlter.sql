USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ZBKApplicationReportView]    Script Date: 19.03.2018 11:33:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER VIEW [dbo].[OBK_ZBKApplicationReportView]
AS
SELECT [OBK_BlankNumber].[Id]
      ,[OBK_StageExpDocument].[ExpStartDate]
	  ,[ncels].[dbo].BuildBlankNumber([OBK_BlankNumber].[Number]) AS [BlankNumber]
	  ,[OBK_Ref_Type].[NameRu] AS [DeclarationType]	
	  ,[Units].[Name] AS [ExpertOrganization] 
	  ,[OBK_Declarant].[NameRu] AS [Declarant]
	  ,[OBK_StageExpDocument].[ExpConclusionNumber]
	  ,[OBK_AssessmentDeclaration].[Number] AS [DeclarationNumber]
	  ,[OBK_RS_Products].[DrugFormFullName] 
	  + N' Серия №'+[OBK_Procunts_Series].[Series]
	  + N' годен до ' + [OBK_Procunts_Series].[SeriesEndDate]
	  + N' кол-во партий ' + [OBK_Procunts_Series].[SeriesParty]
	  + N' ед измерения ' + ([sr_measures].[short_name]   collate SQL_Latin1_General_CP1_CI_AS )
	  AS [ProductName]
  FROM [ncels].[dbo].[OBK_BlankNumber]
  LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] ON [OBK_BlankNumber].[Object_Id] = [OBK_StageExpDocument].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_Ref_Type] ON [OBK_Ref_Type].[Id] = [OBK_AssessmentDeclaration].[TypeId]
  LEFT JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_BlankType] ON [OBK_BlankNumber].[BlankTypeId] = [OBK_BlankType].[Id]
  LEFT JOIN [ncels].[dbo].[Units] ON [OBK_Contract].[ExpertOrganization] = [Units].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_StageExpDocument].[ProductSeriesId] = [OBK_Procunts_Series].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_Procunts_Series].[OBK_RS_ProductsId] = [OBK_RS_Products].[Id]
  LEFT JOIN [ncels].[dbo].[sr_measures] ON [OBK_Procunts_Series].[SeriesMeasureId] = [sr_measures].[id]
  WHERE [OBK_BlankType].[Code] IN ('Application') AND [OBK_StageExpDocument].[ExpConclusionNumber] IS NOT NULL
GO


