--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.5.327.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 07.02.2018 11:50:43
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
-- Создать столбец [Result] для таблицы [dbo].[OBK_AssessmentDeclaration__OBK_ExpertCouncil]
--
ALTER TABLE dbo.OBK_AssessmentDeclaration__OBK_ExpertCouncil
  ADD Result int NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [Comment] для таблицы [dbo].[OBK_AssessmentDeclaration__OBK_ExpertCouncil]
--
ALTER TABLE dbo.OBK_AssessmentDeclaration__OBK_ExpertCouncil
  ADD Comment nvarchar(500) NULL
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