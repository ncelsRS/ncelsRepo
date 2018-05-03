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

SET IDENTITY_INSERT dbo.Ref_DegreeRiskClasses ON
GO
MERGE INTO dbo.Ref_DegreeRiskClasses t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 09:55:53.6533333', DateUpdate = '2018-05-03 09:55:53.6533333', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Класс 1 - с низкой степенью риска', NameRu = N'Класс 1 - с низкой степенью риска'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (1, N'', '2018-05-03 09:55:53.6533333', '2018-05-03 09:55:53.6533333', CONVERT(bit, 'False'), N'Класс 1 - с низкой степенью риска', N'Класс 1 - с низкой степенью риска');
MERGE INTO dbo.Ref_DegreeRiskClasses t1 USING (SELECT 1 id) t2 ON (t1.Id = 2)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 09:55:53.6533333', DateUpdate = '2018-05-03 09:55:53.6533333', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Класс 2а - со средней степенью риска', NameRu = N'Класс 2а - со средней степенью риска'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (2, N'', '2018-05-03 09:55:53.6533333', '2018-05-03 09:55:53.6533333', CONVERT(bit, 'False'), N'Класс 2а - со средней степенью риска', N'Класс 2а - со средней степенью риска');
MERGE INTO dbo.Ref_DegreeRiskClasses t1 USING (SELECT 1 id) t2 ON (t1.Id = 3)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 09:55:53.6533333', DateUpdate = '2018-05-03 09:55:53.6533333', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Класс 2б - с повышенной степенью риска', NameRu = N'Класс 2б - с повышенной степенью риска'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (3, N'', '2018-05-03 09:55:53.6533333', '2018-05-03 09:55:53.6533333', CONVERT(bit, 'False'), N'Класс 2б - с повышенной степенью риска', N'Класс 2б - с повышенной степенью риска');
MERGE INTO dbo.Ref_DegreeRiskClasses t1 USING (SELECT 1 id) t2 ON (t1.Id = 4)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 09:55:53.6533333', DateUpdate = '2018-05-03 09:55:53.6533333', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Класс 3 - с высокой степенью риска', NameRu = N'Класс 3 - с высокой степенью риска'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (4, N'', '2018-05-03 09:55:53.6533333', '2018-05-03 09:55:53.6533333', CONVERT(bit, 'False'), N'Класс 3 - с высокой степенью риска', N'Класс 3 - с высокой степенью риска');
GO
SET IDENTITY_INSERT dbo.Ref_DegreeRiskClasses OFF
GO