USE ncels
GO

DROP TABLE IF EXISTS dbo.OBK_AssessmentReportOP
GO


--
-- Создать таблицу [dbo].[OBK_AssessmentReportOP]
--
PRINT (N'Создать таблицу [dbo].[OBK_AssessmentReportOP]')
GO
CREATE TABLE dbo.OBK_AssessmentReportOP (
  Id int IDENTITY,
  Date date NULL,
  Result nvarchar(2000) NULL,
  ExecuteResult int NULL,
  DeclarationId uniqueidentifier NULL,
  StageStatusId int NULL,
  CONSTRAINT PK_OBK_AssessmentReportOP_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_OBK_AssessmentReportOP_DeclarationId] для объекта типа таблица [dbo].[OBK_AssessmentReportOP]
--
PRINT (N'Создать внешний ключ [FK_OBK_AssessmentReportOP_DeclarationId] для объекта типа таблица [dbo].[OBK_AssessmentReportOP]')
GO
ALTER TABLE dbo.OBK_AssessmentReportOP
  ADD CONSTRAINT FK_OBK_AssessmentReportOP_DeclarationId FOREIGN KEY (DeclarationId) REFERENCES dbo.OBK_AssessmentDeclaration (Id)
GO

--
-- Создать внешний ключ [FK_OBK_AssessmentReportOP_StageStatusId] для объекта типа таблица [dbo].[OBK_AssessmentReportOP]
--
PRINT (N'Создать внешний ключ [FK_OBK_AssessmentReportOP_StageStatusId] для объекта типа таблица [dbo].[OBK_AssessmentReportOP]')
GO
ALTER TABLE dbo.OBK_AssessmentReportOP
  ADD CONSTRAINT FK_OBK_AssessmentReportOP_StageStatusId FOREIGN KEY (StageStatusId) REFERENCES dbo.OBK_Ref_StageStatus (Id)
GO