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
ALTER FUNCTION [dbo].ActInfo 
(
	-- Add the parameters for the function here
	 @DeclarationId UNIQUEIDENTIFIER
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	
	DECLARE @ActList NVARCHAR(MAX);

	SELECT @ActList = COALESCE(@ActList + N', ', N'') + COALESCE([Number], '') 
	+ ', ' + CONVERT(NVARCHAR(10), COALESCE([ActDate], ''), 104)
	FROM [ncels].[dbo].[OBK_ActReception] 
	WHERE [OBK_AssessmentDeclarationId] = @DeclarationId

	RETURN  @ActList

END
GO


CREATE VIEW [DeclarationReportView]
AS

SELECT DISTINCT
[OBK_AssessmentDeclaration].[Id],
[OBK_AssessmentDeclaration].[Number],
[OBK_AssessmentDeclaration].[SendDate],
[OBK_Ref_Type].[NameRu] AS [DeclarationType],
[OBK_Declarant].[NameRu] + '. ' + [OBK_DeclarantContact].[AddressLegalRu] AS [Declarant],
[OBK_AssessmentDeclaration].[Number] + ', ' + CONVERT(NVARCHAR(10),[OBK_AssessmentDeclaration].[SendDate], 104) AS [Desicion], 
[ncels].[dbo].ActInfo([OBK_AssessmentDeclaration].[Id]) as [ActInfo],
(SELECT COUNT(*) FROM [ncels].[dbo].[OBK_StageExpDocument] WHERE [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]) AS ZbkQuantity,
[Units].[Name] AS [ExpertOrganization]
  FROM [ncels].[dbo].[OBK_AssessmentDeclaration]
  INNER JOIN [ncels].[dbo].[OBK_StageExpDocument]  ON [OBK_AssessmentDeclaration].[Id] = [OBK_StageExpDocument].[AssessmentDeclarationId]
  INNER JOIN [ncels].[dbo].[OBK_Ref_Type] ON [OBK_AssessmentDeclaration].[TypeId] = [OBK_Ref_Type].[Id]
  INNER JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_Contract].[Id] = [OBK_AssessmentDeclaration].[ContractId]
  LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Declarant].[Id] = [OBK_Contract].[DeclarantId]
  LEFT JOIN [ncels].[dbo].[OBK_DeclarantContact] ON [OBK_DeclarantContact].[Id] = [OBK_Contract].[DeclarantContactId]
  LEFT JOIN [ncels].[dbo].[OBK_ActReception] ON [OBK_ActReception].[OBK_AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
  LEFT JOIN [ncels].[dbo].[Units] ON [OBK_Contract].[ExpertOrganization] = [Units].[Id]
  WHERE [OBK_StageExpDocument].[ExpConclusionNumber] IS NOT NULL 
