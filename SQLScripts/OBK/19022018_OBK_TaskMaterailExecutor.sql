
CREATE TABLE dbo.OBK_TaskMaterailExecutor (
  Id UNIQUEIDENTIFIER NOT NULL,
  TaskMaterialId uniqueidentifier NOT NULL,
  TaskExecutorId uniqueidentifier NOT NULL
  CONSTRAINT PK_OBK_TaskMaterailExecutor PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

ALTER TABLE dbo.OBK_TaskMaterailExecutor
  ADD CONSTRAINT FK_OBK_TaskMaterailExecutor_OBK_TaskExecutor FOREIGN KEY (TaskExecutorId) REFERENCES dbo.OBK_TaskExecutor (Id)
GO

ALTER TABLE dbo.OBK_TaskMaterailExecutor
  ADD CONSTRAINT FK_OBK_TaskMaterailExecutor_OBK_TaskMaterail FOREIGN KEY (TaskMaterialId) REFERENCES dbo.OBK_TaskMaterial (Id)
GO