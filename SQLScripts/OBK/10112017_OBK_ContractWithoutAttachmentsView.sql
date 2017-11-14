create view OBK_ContractWithoutAttachmentsView
AS
	select
			CASE WHEN OBK_Contract.ParentId IS NOT NULL AND OBK_Contract.Number <> N'б/н' THEN (SELECT TOP 1 X.Number FROM OBK_Contract X WHERE X.Id = OBK_Contract.ParentId) + '-' + OBK_Contract.Number
													ELSE OBK_Contract.Number END
													AS 'Number',
			OBK_ContractStageExecutors.ExecutorId AS 'ExecutorId'
	from OBK_Contract
	inner join OBK_ContractStage on OBK_Contract.Id = OBK_ContractStage.ContractId
	inner join OBK_ContractStageExecutors on OBK_ContractStageExecutors.ContractStageId = OBK_ContractStage.Id
	where 
	OBK_Contract.Number != N'б/н' AND OBK_Contract.Number is not null
	and OBK_ContractStage.StageStatusId = 8
	and OBK_Contract.Id NOT IN (SELECT ContractId FROM OBK_ContractSignedDatas)
	and OBK_ContractStageExecutors.ExecutorType = 2