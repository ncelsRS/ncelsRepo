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

SET IDENTITY_INSERT dbo.Ref_EquipmentTypes ON
GO
MERGE INTO dbo.Ref_EquipmentTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)
WHEN MATCHED THEN UPDATE  SET Code = NULL, DateCreate = '2018-05-02 00:00:00.0000000', DateUpdate = '2018-05-02 00:00:00.0000000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Основной блок', NameRu = N'Основной блок'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (1, NULL, '2018-05-02 00:00:00.0000000', '2018-05-02 00:00:00.0000000', CONVERT(bit, 'False'), N'Основной блок', N'Основной блок');
MERGE INTO dbo.Ref_EquipmentTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 2)
WHEN MATCHED THEN UPDATE  SET Code = NULL, DateCreate = '2018-05-02 00:00:00.0000000', DateUpdate = '2018-05-02 00:00:00.0000000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Принадлежности', NameRu = N'Принадлежности'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (2, NULL, '2018-05-02 00:00:00.0000000', '2018-05-02 00:00:00.0000000', CONVERT(bit, 'False'), N'Принадлежности', N'Принадлежности');
MERGE INTO dbo.Ref_EquipmentTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 3)
WHEN MATCHED THEN UPDATE  SET Code = NULL, DateCreate = '2018-05-02 00:00:00.0000000', DateUpdate = '2018-05-02 00:00:00.0000000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Дополнительные комплектующие', NameRu = N'Дополнительные комплектующие'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (3, NULL, '2018-05-02 00:00:00.0000000', '2018-05-02 00:00:00.0000000', CONVERT(bit, 'False'), N'Дополнительные комплектующие', N'Дополнительные комплектующие');
MERGE INTO dbo.Ref_EquipmentTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 4)
WHEN MATCHED THEN UPDATE  SET Code = NULL, DateCreate = '2018-05-02 00:00:00.0000000', DateUpdate = '2018-05-02 00:00:00.0000000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Программное обеспечение', NameRu = N'Программное обеспечение'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (4, NULL, '2018-05-02 00:00:00.0000000', '2018-05-02 00:00:00.0000000', CONVERT(bit, 'False'), N'Программное обеспечение', N'Программное обеспечение');
MERGE INTO dbo.Ref_EquipmentTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 5)
WHEN MATCHED THEN UPDATE  SET Code = NULL, DateCreate = '2018-05-02 00:00:00.0000000', DateUpdate = '2018-05-02 00:00:00.0000000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Расходные материалы', NameRu = N'Расходные материалы'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (5, NULL, '2018-05-02 00:00:00.0000000', '2018-05-02 00:00:00.0000000', CONVERT(bit, 'False'), N'Расходные материалы', N'Расходные материалы');
GO
SET IDENTITY_INSERT dbo.Ref_EquipmentTypes OFF
GO