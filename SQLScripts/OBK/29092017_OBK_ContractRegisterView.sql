CREATE VIEW [OBK_ContractRegisterView]
AS
SELECT
		OBK_Contract.Id AS 'ContractId',
		OBK_Contract.Number AS 'ContractNumber',
		OBK_Contract.CreatedDate AS 'ContractCreatedDate',
		OBK_Contract.StartDate AS 'ContractStartDate',
		OBK_Contract.EndDate AS 'ContractEndDate',
		OBK_Declarant.NameRu AS 'DeclarantNameRu',
		OBK_ContractStageExecutors.ExecutorId AS 'ExecutorId',
		OBK_Ref_Status.Id AS 'ContractStatusId',
		OBK_Ref_Status.NameRu AS 'ContractStatusNameRu',
		OBK_Ref_StageStatus.Id AS 'StageStatusId',
		OBK_Ref_StageStatus.NameRu AS 'StageStatusNameRu',
		OBK_Ref_StageStatus.Code AS 'StageStatusCode',
		OBK_ContractStage.Id AS 'ContractStageId',
		OBK_ContractStage.StageId AS 'ContractStageStageId'
FROM OBK_ContractStage
INNER JOIN OBK_Contract ON OBK_Contract.Id = OBK_ContractStage.ContractId
INNER JOIN OBK_ContractStageExecutors ON OBK_ContractStageExecutors.ContractStageId = OBK_ContractStage.Id
INNER JOIN OBK_Declarant ON OBK_Declarant.Id = OBK_Contract.DeclarantId
INNER JOIN OBK_Ref_Status ON OBK_Ref_Status.Id = OBK_Contract.Status
INNER JOIN OBK_Ref_StageStatus ON OBK_Ref_StageStatus.Id = OBK_ContractStage.StageStatusId
INNER JOIN OBK_Ref_Stage ON OBK_Ref_Stage.Id = OBK_ContractStage.StageId
GO

