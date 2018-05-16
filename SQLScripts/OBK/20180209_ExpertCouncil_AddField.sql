--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.5.327.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 09.02.2018 12:13:48
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
-- Создать столбец [Number] для таблицы [dbo].[OBK_ExpertCouncil]
--
ALTER TABLE dbo.OBK_ExpertCouncil
  ADD Number nvarchar(50) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [IsComplited] для таблицы [dbo].[OBK_ExpertCouncil]
--
ALTER TABLE dbo.OBK_ExpertCouncil
  ADD IsComplited bit NULL
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