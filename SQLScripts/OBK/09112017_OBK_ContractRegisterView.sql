USE [ncelsProd]
GO

/****** Object:  View [dbo].[OBK_ContractRegisterView]    Script Date: 07.11.2017 10:23:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[OBK_ContractRegisterView]
AS
SELECT
		OBK_Contract.Id AS 'ContractId',
		OBK_Contract.ParentId AS 'ContractParentId',
		OBK_Ref_Type.NameRu AS 'ContractTypeRu',
		OBK_Ref_Type.NameKz AS 'ContractTypeKz',
		OBK_Contract.Number AS 'ContractNumber',
		CASE WHEN OBK_Contract.ParentId IS NOT NULL AND OBK_Contract.Number <> N'б/н' THEN (SELECT TOP 1 X.Number FROM OBK_Contract X WHERE X.Id = OBK_Contract.ParentId) + '-' + OBK_Contract.Number
													ELSE OBK_Contract.Number END
													AS 'ContractDisplayNumber',
		OBK_Contract.CreatedDate AS 'ContractCreatedDate',
		OBK_Contract.SendDate AS 'ContractSendDate',
		OBK_Contract.StartDate AS 'ContractStartDate',
		OBK_Contract.EndDate AS 'ContractEndDate',
		OBK_Contract.Signer AS 'ContractSigner',
		OBK_Declarant.NameRu AS 'DeclarantNameRu',
		OBK_ContractStageExecutors.ExecutorId AS 'ExecutorId',
		OBK_ContractStageExecutors.ExecutorType AS 'ExecutorType',
		OBK_Ref_Status.Id AS 'ContractStatusId',
		OBK_Ref_Status.NameRu AS 'ContractStatusNameRu',
		OBK_Ref_StageStatus.Id AS 'StageStatusId',
		OBK_Ref_StageStatus.NameRu AS 'StageStatusNameRu',
		OBK_Ref_StageStatus.Code AS 'StageStatusCode',
		OBK_ContractStage.Id AS 'ContractStageId',
		OBK_ContractStage.StageId AS 'ContractStageStageId',
		(SELECT ResultId FROM OBK_ContractStage WHERE ContractId = OBK_Contract.Id AND StageId = 1)
		AS 'StageCOZResult',
		(SELECT ResultId FROM OBK_ContractStage WHERE ContractId = OBK_Contract.Id AND StageId = 3)
		AS 'StageUOBKResult',
		(SELECT TOP 1 Employees.ShortName FROM Employees
		INNER JOIN OBK_ContractStageExecutors ON OBK_ContractStageExecutors.ExecutorId = Employees.Id
		INNER JOIN OBK_ContractStage ON OBK_ContractStage.Id = OBK_ContractStageExecutors.ContractStageId
		WHERE OBK_ContractStageExecutors.ExecutorType = 2
		AND OBK_ContractStage.Id = OBK_ContractStage.Id
		AND OBK_ContractStage.StageId = 3
		AND OBK_ContractStage.ContractId = OBK_Contract.Id
		)
		AS 'StageUOBKExecutor',
		(SELECT ResultId FROM OBK_ContractStage WHERE ContractId = OBK_Contract.Id AND StageId = 4)
		AS 'StageDEFResult',
		(SELECT TOP 1 Employees.ShortName FROM Employees
		INNER JOIN OBK_ContractStageExecutors ON OBK_ContractStageExecutors.ExecutorId = Employees.Id
		INNER JOIN OBK_ContractStage ON OBK_ContractStage.Id = OBK_ContractStageExecutors.ContractStageId
		WHERE OBK_ContractStageExecutors.ExecutorType = 2
		AND OBK_ContractStage.Id = OBK_ContractStage.Id
		AND OBK_ContractStage.StageId = 4
		AND OBK_ContractStage.ContractId = OBK_Contract.Id
		)
		AS 'StageDEFExecutor'
FROM OBK_ContractStage
INNER JOIN OBK_Contract ON OBK_Contract.Id = OBK_ContractStage.ContractId
INNER JOIN OBK_ContractStageExecutors ON OBK_ContractStageExecutors.ContractStageId = OBK_ContractStage.Id
INNER JOIN OBK_Declarant ON OBK_Declarant.Id = OBK_Contract.DeclarantId
INNER JOIN OBK_Ref_Status ON OBK_Ref_Status.Id = OBK_Contract.Status
INNER JOIN OBK_Ref_StageStatus ON OBK_Ref_StageStatus.Id = OBK_ContractStage.StageStatusId
INNER JOIN OBK_Ref_Stage ON OBK_Ref_Stage.Id = OBK_ContractStage.StageId
INNER JOIN OBK_Ref_Type ON OBK_Ref_Type.Id = OBK_Contract.Type
GO


