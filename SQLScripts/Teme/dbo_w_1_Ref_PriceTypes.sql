--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.5.327.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 03.05.2018 10:10:51
-- Версия сервера: 13.00.4001
--

SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

SET IDENTITY_INSERT dbo.Ref_PriceTypes ON
GO
MERGE INTO dbo.Ref_PriceTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:02:15.4800000', DateUpdate = '2018-05-03 10:02:15.4800000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'1 лекарственный препарат', NameRu = N'1 лекарственный препарат'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (1, N'', '2018-05-03 10:02:15.4800000', '2018-05-03 10:02:15.4800000', CONVERT(bit, 'False'), N'1 лекарственный препарат', N'1 лекарственный препарат');
MERGE INTO dbo.Ref_PriceTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 2)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:02:15.4800000', DateUpdate = '2018-05-03 10:02:15.4800000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'1 лекарственная доза', NameRu = N'1 лекарственная доза'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (2, N'', '2018-05-03 10:02:15.4800000', '2018-05-03 10:02:15.4800000', CONVERT(bit, 'False'), N'1 лекарственная доза', N'1 лекарственная доза');
MERGE INTO dbo.Ref_PriceTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 3)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:02:15.4800000', DateUpdate = '2018-05-03 10:02:15.4800000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'1 фасовка', NameRu = N'1 фасовка'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (3, N'', '2018-05-03 10:02:15.4800000', '2018-05-03 10:02:15.4800000', CONVERT(bit, 'False'), N'1 фасовка', N'1 фасовка');
MERGE INTO dbo.Ref_PriceTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 4)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:02:15.4800000', DateUpdate = '2018-05-03 10:02:15.4800000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'1 вид', NameRu = N'1 вид'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (4, N'', '2018-05-03 10:02:15.4800000', '2018-05-03 10:02:15.4800000', CONVERT(bit, 'False'), N'1 вид', N'1 вид');
MERGE INTO dbo.Ref_PriceTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 5)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:02:15.4800000', DateUpdate = '2018-05-03 10:02:15.4800000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'1 изделие', NameRu = N'1 изделие'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (5, N'', '2018-05-03 10:02:15.4800000', '2018-05-03 10:02:15.4800000', CONVERT(bit, 'False'), N'1 изделие', N'1 изделие');
MERGE INTO dbo.Ref_PriceTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 6)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:02:15.4800000', DateUpdate = '2018-05-03 10:02:15.4800000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'1 набор', NameRu = N'1 набор'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (6, N'', '2018-05-03 10:02:15.4800000', '2018-05-03 10:02:15.4800000', CONVERT(bit, 'False'), N'1 набор', N'1 набор');
MERGE INTO dbo.Ref_PriceTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 7)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:02:15.4800000', DateUpdate = '2018-05-03 10:02:15.4800000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'1 заключение', NameRu = N'1 заключение'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (7, N'', '2018-05-03 10:02:15.4800000', '2018-05-03 10:02:15.4800000', CONVERT(bit, 'False'), N'1 заключение', N'1 заключение');
MERGE INTO dbo.Ref_PriceTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 8)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:02:15.4800000', DateUpdate = '2018-05-03 10:02:15.4800000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'1 копия', NameRu = N'1 копия'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (8, N'', '2018-05-03 10:02:15.4800000', '2018-05-03 10:02:15.4800000', CONVERT(bit, 'False'), N'1 копия', N'1 копия');
MERGE INTO dbo.Ref_PriceTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 9)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:02:15.4800000', DateUpdate = '2018-05-03 10:02:15.4800000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'1 дубликат', NameRu = N'1 дубликат'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (9, N'', '2018-05-03 10:02:15.4800000', '2018-05-03 10:02:15.4800000', CONVERT(bit, 'False'), N'1 дубликат', N'1 дубликат');
MERGE INTO dbo.Ref_PriceTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 10)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:02:15.4800000', DateUpdate = '2018-05-03 10:02:15.4800000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'1 экспертиза', NameRu = N'1 экспертиза'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (10, N'', '2018-05-03 10:02:15.4800000', '2018-05-03 10:02:15.4800000', CONVERT(bit, 'False'), N'1 экспертиза', N'1 экспертиза');
GO
SET IDENTITY_INSERT dbo.Ref_PriceTypes OFF
GO