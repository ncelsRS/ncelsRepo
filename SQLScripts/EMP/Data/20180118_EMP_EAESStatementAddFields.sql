--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.5.327.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 18.01.2018 12:27:33
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
-- Создать столбец [PlaceType] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlaceType nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [PlaceNameRu] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlaceNameRu nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [PlaceAllowedDocumentNumber] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlaceAllowedDocumentNumber nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [PlaceBossLastName] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlaceBossLastName nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [PlaceBossPosition] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlaceBossPosition nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [PlaceOrganizationForm] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlaceOrganizationForm nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [PlaceNameKz] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlaceNameKz nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [PlaceDateOfIssue] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlaceDateOfIssue date NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [PlaceBossFirstName] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlaceBossFirstName nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [PlacePhone] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlacePhone nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [PlaceCountry] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlaceCountry nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [PlaceNameEn] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlaceNameEn nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [PlaceExpirationDate] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlaceExpirationDate date NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [PlaceBossMiddleName] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlaceBossMiddleName nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [PlaceEmail] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlaceEmail nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [PlaceContactPersonInitials] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlaceContactPersonInitials nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [PlaceContactPersonPosition] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlaceContactPersonPosition nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [PlaceContactPersonFactAddress] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD PlaceContactPersonFactAddress nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerType] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerType nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerNameRu] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerNameRu nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerPAllowedDocumentNumber] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerPAllowedDocumentNumber nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerBossLastName] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerBossLastName nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerBossPosition] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerBossPosition nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerOrganizationForm] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerOrganizationForm nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerNameKz] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerNameKz nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerDateOfIssue] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerDateOfIssue date NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerBossFirstName] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerBossFirstName nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerPhone] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerPhone nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerCountry] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerCountry nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerNameEn] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerNameEn nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerExpirationDate] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerExpirationDate date NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerBossMiddleName] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerBossMiddleName nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerEmail] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerEmail nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerContactPersonInitials] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerContactPersonInitials nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerContactPersonPosition] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerContactPersonPosition nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerContactPersonFactAddress] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerContactPersonFactAddress nvarchar(255) NULL
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- Создать столбец [ShowerContactPersonActualAddress] для таблицы [dbo].[EMP_EAESStatement]
--
ALTER TABLE dbo.EMP_EAESStatement
  ADD ShowerContactPersonActualAddress nvarchar(255) NULL
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