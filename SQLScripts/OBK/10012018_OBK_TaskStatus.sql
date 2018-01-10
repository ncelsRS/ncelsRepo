USE ncels
GO

IF DB_NAME() <> N'ncels' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[OBK_TaskStatus]
--
PRINT (N'Создать таблицу [dbo].[OBK_TaskStatus]')
GO
CREATE TABLE dbo.OBK_TaskStatus (
  Id uniqueidentifier NOT NULL,
  TaskId uniqueidentifier NOT NULL,
  StatusId int NOT NULL,
  UnitLaboratoryId uniqueidentifier NULL,
  CONSTRAINT PK_OBK_TaskStatus PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_OBK_TaskStatus_OBK_Ref_StageStatus] для объекта типа таблица [dbo].[OBK_TaskStatus]
--
PRINT (N'Создать внешний ключ [FK_OBK_TaskStatus_OBK_Ref_StageStatus] для объекта типа таблица [dbo].[OBK_TaskStatus]')
GO
ALTER TABLE dbo.OBK_TaskStatus
  ADD CONSTRAINT FK_OBK_TaskStatus_OBK_Ref_StageStatus FOREIGN KEY (StatusId) REFERENCES dbo.OBK_Ref_StageStatus (Id)
GO

--
-- Создать внешний ключ [FK_OBK_TaskStatus_OBK_Tasks] для объекта типа таблица [dbo].[OBK_TaskStatus]
--
PRINT (N'Создать внешний ключ [FK_OBK_TaskStatus_OBK_Tasks] для объекта типа таблица [dbo].[OBK_TaskStatus]')
GO
ALTER TABLE dbo.OBK_TaskStatus
  ADD CONSTRAINT FK_OBK_TaskStatus_OBK_Tasks FOREIGN KEY (TaskId) REFERENCES dbo.OBK_Tasks (Id)
GO

--
-- Создать внешний ключ [FK_OBK_TaskStatus_Units] для объекта типа таблица [dbo].[OBK_TaskStatus]
--
PRINT (N'Создать внешний ключ [FK_OBK_TaskStatus_Units] для объекта типа таблица [dbo].[OBK_TaskStatus]')
GO
ALTER TABLE dbo.OBK_TaskStatus
  ADD CONSTRAINT FK_OBK_TaskStatus_Units FOREIGN KEY (UnitLaboratoryId) REFERENCES dbo.Units (Id)
GO