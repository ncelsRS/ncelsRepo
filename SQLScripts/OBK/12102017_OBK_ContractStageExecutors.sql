ALTER TABLE OBK_ContractStageExecutors
ADD ExecutorType INT NULL
GO

UPDATE OBK_ContractStageExecutors
SET ExecutorType = 2
GO

ALTER TABLE OBK_ContractStageExecutors
ALTER COLUMN ExecutorType INT NOT NULL
GO
