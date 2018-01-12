USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ZBKCopyStageView]    Script Date: 12.01.2018 16:55:06 ******/
DROP VIEW [dbo].[OBK_ZBKCopyStageView]
GO




USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ZBKCopyStageView]    Script Date: 12.01.2018 16:53:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[OBK_ZBKCopyStageView]
AS
SELECT [OBK_ZBKCopyStage].[Id] as [OBK_ZBKCopyStageId]
	  ,[OBK_StageExpDocument].[ExpConclusionNumber]
	  ,[OBK_StageExpDocument].[ExpStartDate]
	  ,[OBK_StageExpDocument].[ExpEndDate]
	  ,[OBK_StageExpDocument].[ExpApplication]
	  ,[OBK_AssessmentDeclaration].[Number] as [DeclarationNumber]
	  ,[OBK_ZBKCopy].[SendDate] as [ZBKCopySendDate]
	  ,[OBK_DeclarantContact].[BossLastName] + [OBK_DeclarantContact].[BossFirstName] + [OBK_DeclarantContact].[BossMiddleName] as [Declarer]
	  ,[OBK_Ref_Type].[NameRu] as [DeclarationType]
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


