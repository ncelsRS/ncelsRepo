CREATE VIEW [OBK_ZBKReportView]
AS
SELECT [OBK_BlankNumber].[Id]
      ,[OBK_BlankNumber].[Number] AS [BlankNumber]	
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



CREATE VIEW [OBK_ZBKCopyReportView]
AS

SELECT [OBK_ZBKCopy].[Id]
	  ,[OBK_ZBKCopyStageSignData].[SignDateTime] AS [ExtraditeDate]
	  ,([ncels].[dbo].BuildBlankInterval([OBK_ZBKCopy].[StartNumber], [OBK_ZBKCopy].[EndPrimeNumber])) AS [BlankInterval] 
	  ,[ncels].[dbo].CountZBKCopyBlanks([OBK_ZBKCopy].[Id]) AS [BlankQuantity]
	  ,[Units].[Name] AS [ExpertOrganization]
	  ,[OBK_Declarant].[NameRu] AS [Declarant]
	  ,[OBK_StageExpDocument].[ExpConclusionNumber]
  FROM [ncels].[dbo].[OBK_ZBKCopy]
  INNER JOIN [ncels].[dbo].[OBK_StageExpDocument] ON [OBK_ZBKCopy].[OBK_StageExpDocumentId] = [OBK_StageExpDocument].[Id]
  INNER JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
  INNER JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
  LEFT JOIN [ncels].[dbo].[Units] ON [OBK_Contract].[ExpertOrganization] = [Units].[Id]
  INNER JOIN [ncels].[dbo].[OBK_ZBKCopyStage] ON [OBK_ZBKCopyStage].[OBK_ZBKCopyId] = [OBK_ZBKCopy].[Id] AND [OBK_ZBKCopyStage].[StageId] = 2
  INNER JOIN [ncels].[dbo].[OBK_ZBKCopyStageSignData] ON [OBK_ZBKCopyStageSignData].[StageId] = [OBK_ZBKCopyStage].[Id]
 