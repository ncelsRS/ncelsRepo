--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.5.327.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 04.05.2018 12:46:49
-- Версия сервера: 13.00.4001
--

SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

SET IDENTITY_INSERT dbo.Ref_Countries ON
GO
MERGE INTO dbo.Ref_Countries t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)
WHEN MATCHED THEN UPDATE  SET Code = N'01', DateCreate = '2018-05-04 12:11:53.5900000', DateUpdate = '2018-05-04 12:11:53.5900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Казахстан', NameRu = N'Казахстан'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (1, N'01', '2018-05-04 12:11:53.5900000', '2018-05-04 12:11:53.5900000', CONVERT(bit, 'False'), N'Казахстан', N'Казахстан');
MERGE INTO dbo.Ref_Countries t1 USING (SELECT 1 id) t2 ON (t1.Id = 2)
WHEN MATCHED THEN UPDATE  SET Code = N'01', DateCreate = '2018-05-04 12:11:53.5900000', DateUpdate = '2018-05-04 12:11:53.5900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Россия', NameRu = N'Россия'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (2, N'01', '2018-05-04 12:11:53.5900000', '2018-05-04 12:11:53.5900000', CONVERT(bit, 'False'), N'Россия', N'Россия');
GO
SET IDENTITY_INSERT dbo.Ref_Countries OFF
GO