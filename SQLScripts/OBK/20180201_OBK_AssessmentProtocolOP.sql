USE ncels
GO

DROP TABLE IF EXISTS dbo.OBK_AssessmentProtocolOP
GO


--
-- Создать таблицу [dbo].[OBK_AssessmentProtocolOP]
--
PRINT (N'Создать таблицу [dbo].[OBK_AssessmentProtocolOP]')
GO
CREATE TABLE dbo.OBK_AssessmentProtocolOP (
  Id int IDENTITY,
  DeclarationId uniqueidentifier NULL,
  Number nvarchar(50) NOT NULL,
  Executor nvarchar(50) NULL,
  ExecuteResult int NULL,
  NameRu nvarchar(1000) NULL,
  NameKz nvarchar(1000) NULL,
  Date date NULL,
  CONSTRAINT PK_OBK_AssessmentProtocolOP_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_OBK_AssessmentProtocolOP_DeclarationId] для объекта типа таблица [dbo].[OBK_AssessmentProtocolOP]
--
PRINT (N'Создать внешний ключ [FK_OBK_AssessmentProtocolOP_DeclarationId] для объекта типа таблица [dbo].[OBK_AssessmentProtocolOP]')
GO
ALTER TABLE dbo.OBK_AssessmentProtocolOP
  ADD CONSTRAINT FK_OBK_AssessmentProtocolOP_DeclarationId FOREIGN KEY (DeclarationId) REFERENCES dbo.OBK_AssessmentDeclaration (Id)
GO