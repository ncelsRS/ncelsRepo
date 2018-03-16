-- ================================================
-- Template generated from Template Explorer using:
-- Create Scalar Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION DefectiveResults 
(
	-- Add the parameters for the function here
	@ProductSeriesId int
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(MAX)

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar = COALESCE(@ResultVar + N', ', N'') +  COALESCE([OBK_Ref_LaboratoryMark].[NameRu] , '') 
  FROM [ncels].[dbo].[OBK_TaskMaterial]
  LEFT JOIN [ncels].[dbo].[OBK_ResearchCenterResult] ON [OBK_TaskMaterial].[Id] = [OBK_ResearchCenterResult].[TaskMaterialId]
  LEFT JOIN [ncels].[dbo].[OBK_Ref_LaboratoryMark] ON [OBK_Ref_LaboratoryMark].[Id] = [OBK_ResearchCenterResult].[LaboratoryMarkId]
  WHERE [OBK_TaskMaterial].[ProductSeriesId] = @ProductSeriesId

	-- Return the result of the function
	RETURN @ResultVar

END
GO


USE [ncels]
GO

/****** Object:  View [dbo].[OBK_DefectiveProductsReportView]    Script Date: 16.03.2018 16:10:00 ******/
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
			,(N'Не соотвествует: ' + [ncels].[dbo].DefectiveResults([OBK_Procunts_Series].[Id])) AS [ExpReasonName]

	FROM [ncels].[dbo].[OBK_StageExpDocument]
	INNER JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
	INNER JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
	LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
	INNER JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_StageExpDocument].[ProductSeriesId] = [OBK_Procunts_Series].[Id]
	INNER JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_Procunts_Series].[OBK_RS_ProductsId] = [OBK_RS_Products].[Id]
	LEFT JOIN [ncels].[dbo].[Units] ON [OBK_Contract].[ExpertOrganization] = [Units].[Id]
	WHERE [OBK_StageExpDocument].[DecisionRefuse] = 1


GO


