USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ZBKRegisterView]    Script Date: 01.02.2018 10:01:46 ******/
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

GO


USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ZBKCopyStageView]    Script Date: 01.02.2018 10:02:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER VIEW [dbo].[OBK_ZBKCopyStageView]
AS
SELECT [OBK_ZBKCopyStage].[Id] as [OBK_ZBKCopyStageId]
	  ,[OBK_StageExpDocument].[ExpConclusionNumber]
	  ,[OBK_StageExpDocument].[ExpStartDate]
	  ,[OBK_StageExpDocument].[ExpEndDate]
	  ,[OBK_StageExpDocument].[ExpApplication]
	  ,[OBK_AssessmentDeclaration].[Number] as [DeclarationNumber]
	  ,[OBK_ZBKCopy].[SendDate] as [ZBKCopySendDate]
	  ,IsNull([OBK_DeclarantContact].[BossLastName], '') + ' ' 
	  + IsNull([OBK_DeclarantContact].[BossFirstName], '' ) + ' ' 
	  + IsNull([OBK_DeclarantContact].[BossMiddleName], '') 
	  as [Declarer],[OBK_Ref_Type].[NameRu] as [DeclarationType]
	  ,[Dictionaries].[Name] as [OrganizationName]
	  ,[OBK_Ref_StageStatus].[Code] as [StageStatusCode]
	  ,[OBK_ZBKCopyStage].[StageId] 
		

FROM [ncels].[dbo].[OBK_ZBKCopyStage]
LEFT JOIN [ncels].[dbo].[OBK_ZBKCopy] on [OBK_ZBKCopyStage].[OBK_ZBKCopyId] = [OBK_ZBKCopy].[Id]
LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] on [OBK_StageExpDocument].[Id] = [OBK_ZBKCopy].[OBK_StageExpDocumentId]
LEFT JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] on [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Contract] on [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
LEFT JOIN [ncels].[dbo].[OBK_DeclarantContact] on [OBK_Contract].[DeclarantContactId] = [OBK_DeclarantContact].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Ref_Type] on [OBK_AssessmentDeclaration].[TypeId] = [OBK_Ref_Type].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Declarant] on [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
LEFT JOIN [ncels].[dbo].[Dictionaries] on [OBK_Declarant].[OrganizationFormId] = [Dictionaries].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Ref_StageStatus] on [OBK_ZBKCopyStage].[StageStatusId] = [OBK_Ref_StageStatus].[Id]

GO



