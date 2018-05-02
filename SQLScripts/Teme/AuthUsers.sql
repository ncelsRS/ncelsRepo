--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.5.327.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 02.05.2018 12:32:08
-- Версия сервера: 14.00.3008
--

SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

SET IDENTITY_INSERT teme.dbo.AuthUsers ON
GO
MERGE INTO teme.dbo.AuthUsers t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)
WHEN MATCHED THEN UPDATE  SET Bin = N'1234567890123', CompanyName = N'companyName', DateCreate = '2018-05-02 11:03:29.8300902', DateUpdate = '2018-05-02 11:03:29.8300913', Email = N'admin@post.net', FirstName = N'Администратор', HasIin = CONVERT(bit, 'True'), Iin = N'1234567890123', LastName = N'lastName', MiddleName = N'middleName', Pwdhash = N'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', UserType = NULL, UserName = N'admin'
WHEN NOT MATCHED THEN INSERT (Id, Bin, CompanyName, DateCreate, DateUpdate, Email, FirstName, HasIin, Iin, LastName, MiddleName, Pwdhash, UserType, UserName) VALUES (1, N'1234567890123', N'companyName', '2018-05-02 11:03:29.8300902', '2018-05-02 11:03:29.8300913', N'admin@post.net', N'Администратор', CONVERT(bit, 'True'), N'1234567890123', N'lastName', N'middleName', N'ujJTh2rta8ItSm/1PYQGxq2GQZXtFEq1yHYhtsIztUi66uaVbfNG7IwX9eoQ817jy8UUeX7X3dMUVGTioLq0Ew==', NULL, N'admin');
GO
SET IDENTITY_INSERT teme.dbo.AuthUsers OFF
GO

SET IDENTITY_INSERT teme.dbo.AuthUserScopes ON
GO
MERGE INTO teme.dbo.AuthUserScopes t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)
WHEN MATCHED THEN UPDATE  SET AuthUserId = 1, DateCreate = '2018-05-02 11:03:29.8310194', DateUpdate = '2018-05-02 11:03:29.8310198', Scope = N'ext'
WHEN NOT MATCHED THEN INSERT (Id, AuthUserId, DateCreate, DateUpdate, Scope) VALUES (1, 1, '2018-05-02 11:03:29.8310194', '2018-05-02 11:03:29.8310198', N'ext');
GO
SET IDENTITY_INSERT teme.dbo.AuthUserScopes OFF
GO