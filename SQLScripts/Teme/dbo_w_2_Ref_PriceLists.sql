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

SET IDENTITY_INSERT dbo.Ref_PriceLists ON
GO
MERGE INTO dbo.Ref_PriceLists t1 USING (SELECT 1 id) t2 ON (t1.Id = 5)
WHEN MATCHED THEN UPDATE  SET DateCreate = '2018-05-03 10:06:18.7033333', DateUpdate = '2018-05-03 10:06:18.7033333', IsDeleted = CONVERT(bit, 'False'), IsImport = CONVERT(bit, 'False'), Price = 308335.00, PriceTypeId = 5, ServiceTypeId = 1
WHEN NOT MATCHED THEN INSERT (Id, DateCreate, DateUpdate, IsDeleted, IsImport, Price, PriceTypeId, ServiceTypeId) VALUES (5, '2018-05-03 10:06:18.7033333', '2018-05-03 10:06:18.7033333', CONVERT(bit, 'False'), CONVERT(bit, 'False'), 308335.00, 5, 1);
MERGE INTO dbo.Ref_PriceLists t1 USING (SELECT 1 id) t2 ON (t1.Id = 6)
WHEN MATCHED THEN UPDATE  SET DateCreate = '2018-05-03 10:06:18.7033333', DateUpdate = '2018-05-03 10:06:18.7033333', IsDeleted = CONVERT(bit, 'False'), IsImport = CONVERT(bit, 'False'), Price = 355360.00, PriceTypeId = 5, ServiceTypeId = 2
WHEN NOT MATCHED THEN INSERT (Id, DateCreate, DateUpdate, IsDeleted, IsImport, Price, PriceTypeId, ServiceTypeId) VALUES (6, '2018-05-03 10:06:18.7033333', '2018-05-03 10:06:18.7033333', CONVERT(bit, 'False'), CONVERT(bit, 'False'), 355360.00, 5, 2);
MERGE INTO dbo.Ref_PriceLists t1 USING (SELECT 1 id) t2 ON (t1.Id = 7)
WHEN MATCHED THEN UPDATE  SET DateCreate = '2018-05-03 10:06:18.7033333', DateUpdate = '2018-05-03 10:06:18.7033333', IsDeleted = CONVERT(bit, 'False'), IsImport = CONVERT(bit, 'False'), Price = 396928.00, PriceTypeId = 5, ServiceTypeId = 3
WHEN NOT MATCHED THEN INSERT (Id, DateCreate, DateUpdate, IsDeleted, IsImport, Price, PriceTypeId, ServiceTypeId) VALUES (7, '2018-05-03 10:06:18.7033333', '2018-05-03 10:06:18.7033333', CONVERT(bit, 'False'), CONVERT(bit, 'False'), 396928.00, 5, 3);
MERGE INTO dbo.Ref_PriceLists t1 USING (SELECT 1 id) t2 ON (t1.Id = 8)
WHEN MATCHED THEN UPDATE  SET DateCreate = '2018-05-03 10:06:18.7033333', DateUpdate = '2018-05-03 10:06:18.7033333', IsDeleted = CONVERT(bit, 'False'), IsImport = CONVERT(bit, 'False'), Price = 457219.00, PriceTypeId = 5, ServiceTypeId = 4
WHEN NOT MATCHED THEN INSERT (Id, DateCreate, DateUpdate, IsDeleted, IsImport, Price, PriceTypeId, ServiceTypeId) VALUES (8, '2018-05-03 10:06:18.7033333', '2018-05-03 10:06:18.7033333', CONVERT(bit, 'False'), CONVERT(bit, 'False'), 457219.00, 5, 4);
MERGE INTO dbo.Ref_PriceLists t1 USING (SELECT 1 id) t2 ON (t1.Id = 9)
WHEN MATCHED THEN UPDATE  SET DateCreate = '2018-05-03 10:06:18.7033333', DateUpdate = '2018-05-03 10:06:18.7033333', IsDeleted = CONVERT(bit, 'False'), IsImport = CONVERT(bit, 'False'), Price = 346083.00, PriceTypeId = 5, ServiceTypeId = 5
WHEN NOT MATCHED THEN INSERT (Id, DateCreate, DateUpdate, IsDeleted, IsImport, Price, PriceTypeId, ServiceTypeId) VALUES (9, '2018-05-03 10:06:18.7033333', '2018-05-03 10:06:18.7033333', CONVERT(bit, 'False'), CONVERT(bit, 'False'), 346083.00, 5, 5);
GO
SET IDENTITY_INSERT dbo.Ref_PriceLists OFF
GO