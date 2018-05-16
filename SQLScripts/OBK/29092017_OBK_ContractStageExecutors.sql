CREATE TABLE OBK_ContractStageExecutors
(
	[ContractStageId] uniqueidentifier NOT NULL,
	[ExecutorId] uniqueidentifier NOT NULL,
	CONSTRAINT PK_OBK_ContractStageExecutors
	PRIMARY KEY ([ContractStageId], [ExecutorId]),
	CONSTRAINT FK_OBK_ContractStageExecutors_ContractStageId_OBK_ContractStage_Id
	FOREIGN KEY ([ContractStageId])
	REFERENCES OBK_ContractStage([Id]),
	CONSTRAINT FK_OBK_ContractStageExecutors_ExecutorId_Employees_Id
	FOREIGN KEY ([ExecutorId])
	REFERENCES Employees([Id])
)