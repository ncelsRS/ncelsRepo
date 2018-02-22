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
CREATE FUNCTION ConvertNvarcharTwoObject 
(
	-- Add the parameters for the function here
	@Param1 NVARCHAR(MAX), @Param2 NVARCHAR(MAX)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	
		Declare @ResultVar NVARCHAR(MAX);
		SELECT @ResultVar = @Param1 + ' ' + @Param2;
		RETURN  @ResultVar;

END
GO



CREATE VIEW [ZBKDetailReportView]
AS
SELECT [OBK_BlankNumber].[Id]
	  ,[OBK_AssessmentDeclaration].[Number] AS [DeclarationNumber]
	  ,[OBK_AssessmentDeclaration].[SendDate]
	  ,[OBK_Ref_Type].[NameRu] AS [DeclarationType]
	  ,[OBK_StageExpDocument].[ExpConclusionNumber]
      ,[ncels].[dbo].BuildBlankNumber([OBK_BlankNumber].[Number]) AS [BlankNumber]	
	  ,[OBK_StageExpDocument].[ExpStartDate]
	  ,[OBK_StageExpDocument].[ExpEndDate]
	  ,[Units].[Name] AS [ExpertOrganization] 
	  ,(SELECT CASE WHEN [OBK_RS_Products].[RegTypeId] = 1 THEN N'ЛС'
	  WHEN [OBK_RS_Products].[RegTypeId] = 2 THEN N'ИМН' END) AS [ProductType]--Тип продукции	
	  ,[OBK_RS_Products].[DrugFormFullName]
	  ,[OBK_Procunts_Series].[Quantity] AS [SerieQuantity]
	  ,1 AS [ProtocolQuantity]
	  ,[OBK_RS_Products].[ProducerNameRu] +' '+ [OBK_RS_Products].[CountryNameRu] AS [ProducerName]
	  ,[OBK_StageExpDocument].[ExpReasonNameRu] AS [ExpReasonName]
	  ,([OBK_Declarant].[NameRu] + ' ' + [OBK_DeclarantContact].[AddressLegalRu] 
	  + ' ' +[OBK_DeclarantContact].[Phone] ) AS [Declarant]
	  ,[ncels].[dbo].ConvertNvarcharTwoObject([OBK_Procunts_Series].[SeriesParty], [sr_measures].[name]) AS [SerieDetail]
  FROM [ncels].[dbo].[OBK_BlankNumber]
  INNER JOIN [ncels].[dbo].[OBK_StageExpDocument] ON [OBK_BlankNumber].[Object_Id] = [OBK_StageExpDocument].[Id]
  INNER JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
  INNER JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
  INNER JOIN [ncels].[dbo].[OBK_BlankType] ON [OBK_BlankNumber].[BlankTypeId] = [OBK_BlankType].[Id]
  LEFT JOIN [ncels].[dbo].[Units] ON [OBK_Contract].[ExpertOrganization] = [Units].[Id]
  INNER JOIN [ncels].[dbo].[OBK_Ref_Type] ON [OBK_Ref_Type].[Id] = [OBK_AssessmentDeclaration].[TypeId]
  LEFT JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_Procunts_Series].[Id] = [OBK_StageExpDocument].[ProductSeriesId]
  LEFT JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_RS_Products].[Id] = [OBK_Procunts_Series].[OBK_RS_ProductsId]
  LEFT JOIN [ncels].[dbo].[OBK_DeclarantContact] ON [OBK_DeclarantContact].[Id] = [OBK_Contract].[DeclarantContactId]
  LEFT JOIN [ncels].[dbo].[Dictionaries] ON [OBK_Declarant].[CountryId] = [Dictionaries].[Id]
  LEFT JOIN [ncels].[dbo].[sr_measures] ON [sr_measures].[id] = [OBK_Procunts_Series].[SeriesMeasureId]
  WHERE [OBK_BlankType].[Code] IN ('ZBK') AND [OBK_StageExpDocument].[ExpConclusionNumber] IS NOT NULL



ALTER VIEW [dbo].[OBK_DefectiveProductsView]
AS
SELECT	 [OBK_StageExpDocument].[Id]
		,[OBK_Declarant].[NameRu] AS [Declarant] --Информация о Заявителе
		,[OBK_RS_Products].[DrugFormFullName]--Наименование продукции
		,[OBK_RS_Products].[ProducerNameRu] AS [ProducerName]  --Производитель
		,[OBK_RS_Products].[CountryNameRu] AS [ProducerCountry]  --Страна
		,[OBK_Procunts_Series].Series --Серия 
		,[OBK_Procunts_Series].[SeriesEndDate]	 --Срок годности
		,[OBK_Procunts_Series].[Quantity] --Количество
		,[sr_measures].[name]  AS [Measure]-- Единица измерения
		,[OBK_StageExpDocument].[RefuseDate] -- Дата отказа	
		,[OBK_StageSendEDO].[Number] -- Номерь письма
		,[OBK_StageSendEDO].[SendDate] -- Дата отправки письма
		,[OBK_StageExpDocument].[OBK_StageSendEDOId] -- Айди отправленного письма
		,(SELECT CASE 
			WHEN [OBK_StageExpDocument].[OBK_StageSendEDOId] IS NULL THEN N'не отправлено' 
			WHEN [OBK_StageExpDocument].[OBK_StageSendEDOId] IS NOT NULL THEN N'отправлено' END) AS [Status]
FROM [ncels].[dbo].[OBK_StageExpDocument]
INNER JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
INNER JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
INNER JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_StageExpDocument].[ProductSeriesId] = [OBK_Procunts_Series].[Id]
INNER JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_Procunts_Series].[OBK_RS_ProductsId] = [OBK_RS_Products].[Id]
LEFT JOIN [ncels].[dbo].[sr_measures] ON [OBK_Procunts_Series].[SeriesMeasureId] = [sr_measures].[Id]
LEFT JOIN [ncels].[dbo].[OBK_StageSendEDO] ON [ncels].[dbo].[OBK_StageExpDocument].[OBK_StageSendEDOId] = [OBK_StageSendEDO].[Id]
WHERE [OBK_StageExpDocument].[ExpResult] = 0
GO

CREATE VIEW [OBK_GMPReportView]
AS
SELECT [OBK_CertificateReference].[Id]
      ,[OBK_CertificateReference].[Number]
      ,[OBK_CertificateReference].[CertificateNumber]
	  ,(SELECT CASE WHEN [OBK_CertificateReference].[CertificateTypeId] = 1 THEN 'GMP'
	  WHEN [OBK_CertificateReference].[CertificateTypeId] = 2 THEN 'ISO' END) AS [CertificateType]
	  ,[OBK_CertificateReference].[CertificateProducer]
      ,[OBK_CertificateReference].[StartDate]
      ,[OBK_CertificateReference].[EndDate]
	  ,[OBK_CertificateReference].[CertificateOrganization]
	  ,GETDATE() AS [RegisterDate]
	  ,[Dictionaries].[Name] AS [Country]
      ,[OBK_CertificateValidityType].[Name] AS [CertificateValidityType]
	  ,[OBK_CertificateReference].[CreateDate] 
	  ,[Units].[Name] AS [ExpertOrganization] 
  FROM [ncels].[dbo].[OBK_CertificateReference]
  LEFT JOIN [ncels].[dbo].[Dictionaries] ON [OBK_CertificateReference].[CertificateCountryId] = [Dictionaries].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_CertificateValidityType] ON [OBK_CertificateReference].[CertificateValidityTypeId] = [OBK_CertificateValidityType].[Id]
  LEFT JOIN [ncels].[dbo].[Units] ON [OBK_CertificateReference].[ExpertOrganization] = [Units].[Id]
GO




CREATE FUNCTION SummaryReportTF
(	
	@DateFrom DATETIME, @DateTo DATETIME
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT 
   [Units].[Id]
  ,[Units].[Name]
  ,COUNT([OBK_AssessmentDeclaration].[Id]) AS [DeclarationCount]
  ,COUNT([OBK_StageExpDocument].[Id]) AS [ZBKCount]
  ,COUNT([DeclarationZBK].[Id]) AS [DeclarationZBKCount]
  ,COUNT([PartyZBK].[Id]) AS [PartyZBKCount]
  ,COUNT([SerialZBK].[Id]) AS [SerialZBKCount]
  ,COUNT([SerialZBK].[Id]) AS [DefectiveProductsCount]
  ,COUNT([OBK_TaskMaterial].[Id]) AS [ProtocolCount]
  ,COUNT([TaskMaterialInWork].[Id]) AS [ProtocolInWorkCount]
  ,COUNT([TaskMaterialCompleted].[Id]) AS [TaskMaterialCompletedCount]
  FROM [ncels].[dbo].[OBK_AssessmentDeclaration]
  INNER JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
  INNER JOIN [ncels].[dbo].[Units] ON [OBK_Contract].[ExpertOrganization] = [Units].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] AS [DeclarationZBK] ON [DeclarationZBK].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
  AND [OBK_AssessmentDeclaration].[TypeId] = 3
  LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] AS [PartyZBK] ON [PartyZBK].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
  AND [OBK_AssessmentDeclaration].[TypeId] = 2
  LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] AS [SerialZBK] ON [SerialZBK].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
  AND [OBK_AssessmentDeclaration].[TypeId] = 1
  LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] AS [DefectiveProducts] ON [DefectiveProducts].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
  AND [DefectiveProducts].[ExpResult] = 0
  LEFT JOIN [ncels].[dbo].[OBK_Tasks] ON [OBK_Tasks].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_TaskMaterial] ON [OBK_TaskMaterial].[TaskId] = [OBK_Tasks].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_TaskMaterial] AS [TaskMaterialInWork] ON [TaskMaterialInWork].[TaskId] = [OBK_Tasks].[Id]
  AND [TaskMaterialInWork].[StatusId] = (SELECT [Id] FROM [ncels].[dbo].[OBK_Ref_StageStatus] WHERE [Code] = 'inWork')
  LEFT JOIN [ncels].[dbo].[OBK_TaskMaterial] AS [TaskMaterialCompleted] ON [TaskMaterialCompleted].[TaskId] = [OBK_Tasks].[Id]
  AND [TaskMaterialCompleted].[StatusId] = (SELECT [Id] FROM [ncels].[dbo].[OBK_Ref_StageStatus] WHERE [Code] = 'completed')
  WHERE [OBK_StageExpDocument].[ExpConclusionNumber] IS NOT NULL AND [OBK_AssessmentDeclaration].[SendDate] BETWEEN @DateFrom AND @DateTo
  GROUP BY [Units].[Name], [Units].[Id] 
)
GO 

CREATE FUNCTION IdentifyTaskProtocolResult 
(
	-- Add the parameters for the function here
	@TaskId UNIQUEIDENTIFIER
)
RETURNS int
AS
BEGIN

	DECLARE @Result int;
	SELECT @Result = COUNT(*) FROM [ncels].[dbo].[OBK_TaskMaterial] 
	INNER JOIN [ncels].[dbo].[OBK_Ref_StageStatus] ON [OBK_TaskMaterial].[StatusId] = [OBK_Ref_StageStatus].[Id]
	WHERE [TaskId] = @TaskId AND [OBK_Ref_StageStatus].[Code] != 'completed' GROUP BY [StatusId]
	IF(@Result > 0)
		RETURN 2;
	
	SELECT @Result = COUNT(*) FROM [ncels].[dbo].[OBK_ResearchCenterResult]
	LEFT JOIN [ncels].[dbo].[OBK_TaskMaterial] ON [OBK_TaskMaterial].[Id] = [OBK_ResearchCenterResult].[TaskMaterialId]
	WHERE [OBK_TaskMaterial].[TaskId] = @TaskId AND [OBK_ResearchCenterResult].[ExpertiseResult] IN (NULL, 0)
	GROUP BY [OBK_ResearchCenterResult].[ExpertiseResult]
	IF(@Result > 0)
		RETURN 0;

	SELECT @Result = COUNT(*) FROM [ncels].[dbo].[OBK_ResearchCenterResult]
	LEFT JOIN [ncels].[dbo].[OBK_TaskMaterial] ON [OBK_TaskMaterial].[Id] = [OBK_ResearchCenterResult].[TaskMaterialId]
	WHERE [OBK_TaskMaterial].[TaskId] = @TaskId AND [OBK_ResearchCenterResult].[ExpertiseResult] IN (1)
	GROUP BY [OBK_ResearchCenterResult].[ExpertiseResult]
		RETURN 1;

END
GO

CREATE FUNCTION FindProductType 
(
	-- Add the parameters for the function here
	@TaskId UNIQUEIDENTIFIER
)
RETURNS NVARCHAR(5)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(10);
	DECLARE @ProductType INT;

	SELECT TOP 1 @ProductType = [OBK_RS_Products].[RegTypeId] FROM [ncels].[dbo].[OBK_TaskMaterial] 
    LEFT JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_Procunts_Series].[Id] = [OBK_TaskMaterial].[ProductSeriesId]
    LEFT JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_RS_Products].[Id] = [OBK_Procunts_Series].[OBK_RS_ProductsId]
	WHERE [OBK_TaskMaterial].[TaskId] = @TaskId

	IF (@ProductType = 1)
		SELECT @ResultVar = N'ЛС'
	
	IF (@ProductType = 2)
		SELECT @ResultVar = N'ИМН'
	
	RETURN @ResultVar
 
END
GO


CREATE VIEW [OBK_SpecialistsReport]
AS
SELECT [OBK_Tasks].[Id]
      ,[OBK_Tasks].[TaskNumber]
      ,[OBK_Tasks].[SendToIC] AS [RegisterDate]
	  ,[Units].[Name] AS [ExpertOrganization]
	  ,[OBK_Declarant].[NameRu] AS [Declarant]
	  ,[Employees].[FullName] + ' (' + [Position].[Name] + ') ' AS [Executor]
	  ,[ncels].[dbo].IdentifyTaskProtocolResult([OBK_Tasks].[Id]) AS [ProtocolResult]
	  ,[ncels].[dbo].FindProductType([OBK_Tasks].[Id]) AS [ProductType]
  FROM [ncels].[dbo].[OBK_Tasks]
  LEFT JOIN [ncels].[dbo].[Units] ON [OBK_Tasks].[UnitId] = [Units].[Id]
  INNER JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_Tasks].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
  INNER JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
  LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Declarant].[Id] = [OBK_Contract].[DeclarantId]
  LEFT JOIN [ncels].[dbo].[OBK_TaskExecutor] ON [OBK_TaskExecutor].[TaskId] = [OBK_Tasks].[Id]
  LEFT JOIN [ncels].[dbo].[Employees] ON [Employees].[Id] = [OBK_TaskExecutor].[ExecutorId]
  LEFT JOIN [ncels].[dbo].[Units] AS [Position] ON [Position].[Id] = [Employees].[PositionId]


CREATE VIEW OBK_laboratoryWorkers
AS
SELECT 
 [Units].Id,
 [Employees].[FullName] + ' (' + [Units].[Name] + ') ' AS [Executor]
FROM [ncels].[dbo].[Units] 
INNER JOIN [ncels].[dbo].[Employees] ON [Employees].[Id] = [Units].[EmployeeId]
WHERE [Units].[ParentId] in 
	(SELECT [Id] FROM [ncels].[dbo].Units u WHERE u.[ParentId] = 
		(SELECT [Id] FROM [ncels].[dbo].Units u WHERE u.Code='researchcenter'))


CREATE FUNCTION [dbo].[FindProductName] 
(
	-- Add the parameters for the function here
	@TaskId UNIQUEIDENTIFIER
)
RETURNS NVARCHAR(1000)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(10);

	SELECT TOP 1 @ResultVar = [OBK_RS_Products].[DrugFormFullName] FROM [ncels].[dbo].[OBK_TaskMaterial] 
    LEFT JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_Procunts_Series].[Id] = [OBK_TaskMaterial].[ProductSeriesId]
    LEFT JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_RS_Products].[Id] = [OBK_Procunts_Series].[OBK_RS_ProductsId]
	WHERE [OBK_TaskMaterial].[TaskId] = @TaskId

	-- Return the result of the function
	RETURN @ResultVar;

END

CREATE FUNCTION [dbo].[GetTaskLaboratoryTypes] 
(
	-- Add the parameters for the function here
	@TaskId UNIQUEIDENTIFIER, @UnitLaboratoryId UNIQUEIDENTIFIER
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar NVARCHAR(MAX);

	IF(@UnitLaboratoryId IS NOT NULL)
		BEGIN
			SELECT @ResultVar =  COALESCE(@ResultVar + N', ', N'') +  COALESCE([OBK_Ref_LaboratoryType].[NameRu], '') 
			FROM [ncels].[dbo].[OBK_TaskMaterial]
			LEFT JOIN [ncels].[dbo].[OBK_Tasks] ON [OBK_TaskMaterial].[TaskId] = [OBK_Tasks].[Id]
			LEFT JOIN [ncels].[dbo].[OBK_Ref_LaboratoryType] ON [OBK_Ref_LaboratoryType].[Id] = [OBK_TaskMaterial].[LaboratoryTypeId]
			WHERE [OBK_Tasks].[Id] = @TaskId AND [OBK_TaskMaterial].[UnitLaboratoryId] = @UnitLaboratoryId
		END
	 ELSE
		BEGIN
			SELECT @ResultVar =  COALESCE(@ResultVar + N', ', N'') +  COALESCE([OBK_Ref_LaboratoryType].[NameRu], '') 
			FROM [ncels].[dbo].[OBK_TaskMaterial]
			LEFT JOIN [ncels].[dbo].[OBK_Tasks] ON [OBK_TaskMaterial].[TaskId] = [OBK_Tasks].[Id]
			LEFT JOIN [ncels].[dbo].[OBK_Ref_LaboratoryType] ON [OBK_Ref_LaboratoryType].[Id] = [OBK_TaskMaterial].[LaboratoryTypeId]
			WHERE [OBK_Tasks].[Id] = @TaskId
		END

	RETURN @ResultVar

END
GO




CREATE FUNCTION [dbo].[OBK_LaboratoryFunction] 
(	
	-- Add the parameters for the function here
	@UnitLaboratoryId UNIQUEIDENTIFIER 
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT [OBK_Tasks].[TaskNumber]
		,[Units].[Name] AS [ExpertOrganization]
		,[ncels].[dbo].[FindProductName]([OBK_Tasks].[Id]) AS [ProductName]
		,(SELECT TOP 1 [OBK_TaskMaterial].[SubTaskNumber] FROM [ncels].[dbo].[OBK_TaskMaterial] 
			WHERE [OBK_TaskMaterial].[TaskId] = [OBK_Tasks].[Id] 
			AND [OBK_TaskMaterial].[SubTaskNumber] IS NOT NULL) AS [SubTaskNumber]
		,[OBK_ActReception].[ActDate]
		,[OBK_Tasks].[SendToIC]
		,[ncels].[dbo].GetTaskLaboratoryTypes([OBK_Tasks].[Id], @UnitLaboratoryId) AS [Tests]
		,[ncels].[dbo].IdentifyTaskProtocolResult([OBK_Tasks].[Id]) AS [ProtocolResult]
  FROM [ncels].[dbo].[OBK_Tasks]
  LEFT JOIN [ncels].[dbo].[Units] ON [OBK_Tasks].[UnitId] = [Units].[Id]
  INNER JOIN [ncels].[dbo].[OBK_ActReception] ON [OBK_ActReception].[Id] = [OBK_Tasks].[ActReceptionId]

)
GO




