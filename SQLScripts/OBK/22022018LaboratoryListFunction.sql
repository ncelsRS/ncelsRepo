USE [ncels]
GO

/****** Object:  UserDefinedFunction [dbo].[LaboratoryListFunction]    Script Date: 22.02.2018 15:19:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[LaboratoryListFunction] 
(	
	-- Add the parameters for the function here
	@FilialId UNIQUEIDENTIFIER 
	
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT 
		 [Units].[Id]
		,[Units].[Name]
	FROM [ncels].[dbo].[Units] 
	WHERE [Units].[ParentId] in 
		(SELECT [Id] FROM [ncels].[dbo].Units u WHERE u.Code='researchcenter' AND u.[ParentId] = @FilialId)
)
GO


CREATE VIEW [dbo].[OBK_DefectiveProductsReportView]
AS
SELECT	 [OBK_StageExpDocument].[Id]
		,[OBK_StageExpDocument].[ExpStartDate]
		,[OBK_AssessmentDeclaration].[Number] AS [DeclarationNumber]
		,[OBK_AssessmentDeclaration].[SendDate]	
		,[OBK_Declarant].[NameRu] AS [Declarant] --Информация о Заявителе
	    ,[Units].[Name] AS [ExpertOrganization] 
		,[OBK_StageExpDocument].[ExpConclusionNumber]
		,([OBK_RS_Products].[DrugFormFullName] + [OBK_Procunts_Series].[Series]
		+ [OBK_Procunts_Series].[SeriesParty] ) AS [ProductInfo]--Наименование продукции
		,[OBK_RS_Products].[ProducerNameRu] +' '+ [OBK_RS_Products].[CountryNameRu] AS [ProducerName]
		,[OBK_StageExpDocument].[ExpReasonNameRu] AS [ExpReasonName]

FROM [ncels].[dbo].[OBK_StageExpDocument]
INNER JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
INNER JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
INNER JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_StageExpDocument].[ProductSeriesId] = [OBK_Procunts_Series].[Id]
INNER JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_Procunts_Series].[OBK_RS_ProductsId] = [OBK_RS_Products].[Id]
LEFT JOIN [ncels].[dbo].[Units] ON [OBK_Contract].[ExpertOrganization] = [Units].[Id]
WHERE [OBK_StageExpDocument].[ExpConclusionNumber] IS NOT NULL
GO