USE ncels
GO

IF DB_NAME() <> N'ncels' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[OBK_AssessmentStageExecutors]
--
PRINT (N'Создать таблицу [dbo].[OBK_AssessmentStageExecutors]')
GO
CREATE TABLE dbo.OBK_AssessmentStageExecutors (
  AssessmentStageId uniqueidentifier NOT NULL,
  ExecutorId uniqueidentifier NOT NULL,
  ExecutorType int NOT NULL,
  ExecuteResult int NULL,
  ExecuteComment nvarchar(2000) NULL,
  Date date NULL,
  CONSTRAINT PK_OBK_AssessmentStageExecutors PRIMARY KEY CLUSTERED (AssessmentStageId, ExecutorId)
)
ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_OBK_AssessmentStageExecutors_Employees] для объекта типа таблица [dbo].[OBK_AssessmentStageExecutors]
--
PRINT (N'Создать внешний ключ [FK_OBK_AssessmentStageExecutors_Employees] для объекта типа таблица [dbo].[OBK_AssessmentStageExecutors]')
GO
ALTER TABLE dbo.OBK_AssessmentStageExecutors WITH NOCHECK
  ADD CONSTRAINT FK_OBK_AssessmentStageExecutors_Employees FOREIGN KEY (ExecutorId) REFERENCES dbo.Employees (Id)
GO

--
-- Создать внешний ключ [FK_OBK_AssessmentStageExecutors_OBK_AssessmentStage] для объекта типа таблица [dbo].[OBK_AssessmentStageExecutors]
--
PRINT (N'Создать внешний ключ [FK_OBK_AssessmentStageExecutors_OBK_AssessmentStage] для объекта типа таблица [dbo].[OBK_AssessmentStageExecutors]')
GO
ALTER TABLE dbo.OBK_AssessmentStageExecutors
  ADD CONSTRAINT FK_OBK_AssessmentStageExecutors_OBK_AssessmentStage FOREIGN KEY (AssessmentStageId) REFERENCES dbo.OBK_AssessmentStage (Id)
GO