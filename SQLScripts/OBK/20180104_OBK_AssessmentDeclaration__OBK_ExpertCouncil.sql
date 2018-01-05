--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.5.311.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 04.01.2018 11:47:29
-- Версия сервера: 14.00.3008
-- Пожалуйста, сохраните резервную копию вашей базы перед запуском этого скрипта
--

USE ncels
GO

IF DB_NAME() <> N'ncels' SET NOEXEC ON
GO

--
-- Начать транзакцию
--
BEGIN TRANSACTION
GO

--
-- Создать таблицу [dbo].[OBK_AssessmentDeclaration__OBK_ExpertCouncil]
--
CREATE TABLE dbo.OBK_AssessmentDeclaration__OBK_ExpertCouncil (
  Id int IDENTITY,
  DeclarationId uniqueidentifier NULL,
  ExpertCouncilId int NULL,
  CONSTRAINT PK_OBK_AssessmentDeclaration__OBK_ExpertCouncil_Id PRIMARY KEY CLUSTERED (Id)
)
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать внешний ключ [FK_OBK_AssessmentDeclaration__OBK_ExpertCouncil_DeclarationId] для объекта типа таблица [dbo].[OBK_AssessmentDeclaration__OBK_ExpertCouncil]
--
ALTER TABLE dbo.OBK_AssessmentDeclaration__OBK_ExpertCouncil
  ADD CONSTRAINT FK_OBK_AssessmentDeclaration__OBK_ExpertCouncil_DeclarationId FOREIGN KEY (DeclarationId) REFERENCES dbo.OBK_AssessmentDeclaration (Id)
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать внешний ключ [FK_OBK_AssessmentDeclaration__OBK_ExpertCouncil_ExpertCouncilId] для объекта типа таблица [dbo].[OBK_AssessmentDeclaration__OBK_ExpertCouncil]
--
ALTER TABLE dbo.OBK_AssessmentDeclaration__OBK_ExpertCouncil
  ADD CONSTRAINT FK_OBK_AssessmentDeclaration__OBK_ExpertCouncil_ExpertCouncilId FOREIGN KEY (ExpertCouncilId) REFERENCES dbo.OBK_ExpertCouncil (Id)
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Фиксировать транзакцию
--
IF @@TRANCOUNT>0 COMMIT TRANSACTION
GO

--
-- Установить NOEXEC в состояние off
--
SET NOEXEC OFF
GO