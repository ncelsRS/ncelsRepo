--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.5.327.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 15.01.2018 15:48:08
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
-- Изменить столбец [NomenclatureDescriptionKz] для таблицы [dbo].[EMP_Statement]
--
ALTER TABLE dbo.EMP_Statement
  ALTER
    COLUMN NomenclatureDescriptionKz nvarchar(2000)
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Изменить столбец [NomenclatureDescriptionRu] для таблицы [dbo].[EMP_Statement]
--
ALTER TABLE dbo.EMP_Statement
  ALTER
    COLUMN NomenclatureDescriptionRu nvarchar(2000)
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Изменить столбец [ApplicationAreaKz] для таблицы [dbo].[EMP_Statement]
--
ALTER TABLE dbo.EMP_Statement
  ALTER
    COLUMN ApplicationAreaKz nvarchar(2000)
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Изменить столбец [ApplicationAreaRu] для таблицы [dbo].[EMP_Statement]
--
ALTER TABLE dbo.EMP_Statement
  ALTER
    COLUMN ApplicationAreaRu nvarchar(2000)
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Изменить столбец [Agreement] для таблицы [dbo].[EMP_Statement]
--
ALTER TABLE dbo.EMP_Statement
  ALTER
    COLUMN Agreement nvarchar(3999)
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [NmirkId] для таблицы [dbo].[EMP_Statement]
--
ALTER TABLE dbo.EMP_Statement
  ADD NmirkId int NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать внешний ключ [FK_EMP_Statement_NmirkId] для объекта типа таблица [dbo].[EMP_Statement]
--
ALTER TABLE dbo.EMP_Statement
  ADD CONSTRAINT FK_EMP_Statement_NmirkId FOREIGN KEY (NmirkId) REFERENCES dbo.EXP_DIC_NMIRK (Id)
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