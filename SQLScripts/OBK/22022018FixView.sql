USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ZBKReportView]    Script Date: 22.02.2018 16:32:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[OBK_ZBKReportView]
AS
SELECT [OBK_BlankNumber].[Id]
      ,[ncels].[dbo].BuildBlankNumber([OBK_BlankNumber].[Number]) AS [BlankNumber]	
	  ,[OBK_StageExpDocument].[ExpStartDate]
	  ,[OBK_StageExpDocument].[ExpConclusionNumber]
	  ,[OBK_AssessmentDeclaration].[Number] AS [DeclarationNumber]
	  ,[Units].[Name] AS [ExpertOrganization] 
	  ,[OBK_Declarant].[NameRu] AS [Declarant]
  FROM [ncels].[dbo].[OBK_BlankNumber]
  LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] ON [OBK_BlankNumber].[Object_Id] = [OBK_StageExpDocument].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_BlankType] ON [OBK_BlankNumber].[BlankTypeId] = [OBK_BlankType].[Id]
  LEFT JOIN [ncels].[dbo].[Units] ON [OBK_Contract].[ExpertOrganization] = [Units].[Id]
  WHERE [OBK_BlankType].[Code] IN ('ZBK') AND [OBK_StageExpDocument].[ExpConclusionNumber] IS NOT NULL
GO


