CREATE TABLE OBK_ContractStage
(
	[Id] uniqueidentifier NOT NULL,
	[ContractId] uniqueidentifier NOT NULL,
	[StageId] INT NOT NULL,
	[StageStatusId] INT NOT NULL,
	[ParentStageId] uniqueidentifier NULL,
	[ResultId] INT NULL,
	CONSTRAINT PK_OBK_ContractStage
	PRIMARY KEY ([Id]),
	CONSTRAINT FK_OBK_ContractStage_ContractId_OBK_Contract_Id
	FOREIGN KEY ([ContractId])
	REFERENCES OBK_Contract([Id]),
	CONSTRAINT FK_OBK_ContractStage_StageId_OBK_Ref_Stage_Id
	FOREIGN KEY ([StageId])
	REFERENCES OBK_Ref_Stage(Id),
	CONSTRAINT FK_OBK_ContractStage_StageStatusId_OBK_Ref_StageStatus_Id
	FOREIGN KEY ([StageStatusId])
	REFERENCES OBK_Ref_StageStatus([Id]),
	CONSTRAINT FK_OBK_ContractStage_ParentStageId_OBK_ContractStage_Id
	FOREIGN KEY ([ParentStageId])
	REFERENCES OBK_ContractStage([Id])
)
