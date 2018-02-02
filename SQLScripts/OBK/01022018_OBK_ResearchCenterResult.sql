ALTER TABLE OBK_ResearchCenterResult ADD TaskExecutorId UNIQUEIDENTIFIER NULL
  CONSTRAINT FK_OBK_ResearchCenterResult_OBK_TaskExecutor FOREIGN KEY (TaskExecutorId) REFERENCES dbo.OBK_TaskExecutor (Id)
