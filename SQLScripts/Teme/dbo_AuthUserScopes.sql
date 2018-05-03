--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.5.327.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 03.05.2018 10:10:48
-- Версия сервера: 13.00.4001
--

SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

SET IDENTITY_INSERT dbo.AuthUserScopes ON
GO
MERGE INTO dbo.AuthUserScopes t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)
WHEN MATCHED THEN UPDATE  SET AuthUserId = 1, DateCreate = '2018-05-02 11:03:29.8310194', DateUpdate = '2018-05-02 11:03:29.8310198', Scope = N'ext'
WHEN NOT MATCHED THEN INSERT (Id, AuthUserId, DateCreate, DateUpdate, Scope) VALUES (1, 1, '2018-05-02 11:03:29.8310194', '2018-05-02 11:03:29.8310198', N'ext');
GO
SET IDENTITY_INSERT dbo.AuthUserScopes OFF
GO