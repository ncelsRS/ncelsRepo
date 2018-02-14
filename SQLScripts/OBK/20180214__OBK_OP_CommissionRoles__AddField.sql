--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.5.327.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 14.02.2018 15:57:17
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
-- Создать столбец [Code] для таблицы [dbo].[OBK_OP_CommissionRoles]
--
ALTER TABLE dbo.OBK_OP_CommissionRoles
  ADD Code nvarchar(50) NULL
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

USE ncels
SET DATEFORMAT ymd
UPDATE dbo.OBK_OP_CommissionRoles SET Id = 'd290b497-3073-4195-9e6a-78094a53120a', NameRu = N'Член комиссии', NameKk = N'Член комиссии', Code = N'Member' WHERE Id = 'd290b497-3073-4195-9e6a-78094a53120a'
GO
UPDATE dbo.OBK_OP_CommissionRoles SET Id = '3935ad57-dea8-4d41-bb94-c99bc56973df', NameRu = N'Председатель комиссии', NameKk = N'Председатель комиссии', Code = N'Chairman' WHERE Id = '3935ad57-dea8-4d41-bb94-c99bc56973df'
GO

--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.5.327.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 14.02.2018 15:59:43
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
-- Изменить столбец [Code] для таблицы [dbo].[OBK_OP_CommissionRoles]
--
ALTER TABLE dbo.OBK_OP_CommissionRoles
  ALTER
    COLUMN Code nvarchar(50) NOT NULL
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