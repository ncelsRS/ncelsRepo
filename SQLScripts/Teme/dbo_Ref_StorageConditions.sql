SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

SET IDENTITY_INSERT dbo.Ref_StorageConditions ON
GO
MERGE INTO dbo.Ref_StorageConditions t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:07:22.0866667', DateUpdate = '2018-05-03 10:07:22.0866667', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Хранение в сухом месте, в помещениях с относительной влажностью воздуха не более 65%', NameRu = N'Хранение в сухом месте, в помещениях с относительной влажностью воздуха не более 65%'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (1, N'', '2018-05-03 10:07:22.0866667', '2018-05-03 10:07:22.0866667', CONVERT(bit, 'False'), N'Хранение в сухом месте, в помещениях с относительной влажностью воздуха не более 65%', N'Хранение в сухом месте, в помещениях с относительной влажностью воздуха не более 65%');
MERGE INTO dbo.Ref_StorageConditions t1 USING (SELECT 1 id) t2 ON (t1.Id = 2)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:07:22.0866667', DateUpdate = '2018-05-03 10:07:22.0866667', IsDeleted = CONVERT(bit, 'False'), NameKz = N'В сухих проветриваемых помещениях', NameRu = N'В сухих проветриваемых помещениях'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (2, N'', '2018-05-03 10:07:22.0866667', '2018-05-03 10:07:22.0866667', CONVERT(bit, 'False'), N'В сухих проветриваемых помещениях', N'В сухих проветриваемых помещениях');
MERGE INTO dbo.Ref_StorageConditions t1 USING (SELECT 1 id) t2 ON (t1.Id = 3)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:07:22.0866667', DateUpdate = '2018-05-03 10:07:22.0866667', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Хранение в защищенном от света месте', NameRu = N'Хранение в защищенном от света месте'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (3, N'', '2018-05-03 10:07:22.0866667', '2018-05-03 10:07:22.0866667', CONVERT(bit, 'False'), N'Хранение в защищенном от света месте', N'Хранение в защищенном от света месте');
MERGE INTO dbo.Ref_StorageConditions t1 USING (SELECT 1 id) t2 ON (t1.Id = 4)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:07:22.0866667', DateUpdate = '2018-05-03 10:07:22.0866667', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Хранить в сухом, прохладном месте', NameRu = N'Хранить в сухом, прохладном месте'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (4, N'', '2018-05-03 10:07:22.0866667', '2018-05-03 10:07:22.0866667', CONVERT(bit, 'False'), N'Хранить в сухом, прохладном месте', N'Хранить в сухом, прохладном месте');
MERGE INTO dbo.Ref_StorageConditions t1 USING (SELECT 1 id) t2 ON (t1.Id = 5)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:07:22.0866667', DateUpdate = '2018-05-03 10:07:22.0866667', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Не замораживать', NameRu = N'Не замораживать'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (5, N'', '2018-05-03 10:07:22.0866667', '2018-05-03 10:07:22.0866667', CONVERT(bit, 'False'), N'Не замораживать', N'Не замораживать');
MERGE INTO dbo.Ref_StorageConditions t1 USING (SELECT 1 id) t2 ON (t1.Id = 6)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:07:22.0866667', DateUpdate = '2018-05-03 10:07:22.0866667', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Хранятся в вентилируемом, темном, сухом помещении', NameRu = N'Хранятся в вентилируемом, темном, сухом помещении'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (6, N'', '2018-05-03 10:07:22.0866667', '2018-05-03 10:07:22.0866667', CONVERT(bit, 'False'), N'Хранятся в вентилируемом, темном, сухом помещении', N'Хранятся в вентилируемом, темном, сухом помещении');
MERGE INTO dbo.Ref_StorageConditions t1 USING (SELECT 1 id) t2 ON (t1.Id = 7)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:07:22.0866667', DateUpdate = '2018-05-03 10:07:22.0866667', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Защищать от воздействия повышенной температуры (более +20оС)', NameRu = N'Защищать от воздействия повышенной температуры (более +20оС)'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (7, N'', '2018-05-03 10:07:22.0866667', '2018-05-03 10:07:22.0866667', CONVERT(bit, 'False'), N'Защищать от воздействия повышенной температуры (более +20оС)', N'Защищать от воздействия повышенной температуры (более +20оС)');
MERGE INTO dbo.Ref_StorageConditions t1 USING (SELECT 1 id) t2 ON (t1.Id = 8)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:07:22.0866667', DateUpdate = '2018-05-03 10:07:22.0866667', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Защищать от пониженной температуры (ниже 0оС)', NameRu = N'Защищать от пониженной температуры (ниже 0оС)'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (8, N'', '2018-05-03 10:07:22.0866667', '2018-05-03 10:07:22.0866667', CONVERT(bit, 'False'), N'Защищать от пониженной температуры (ниже 0оС)', N'Защищать от пониженной температуры (ниже 0оС)');
MERGE INTO dbo.Ref_StorageConditions t1 USING (SELECT 1 id) t2 ON (t1.Id = 9)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:07:22.0866667', DateUpdate = '2018-05-03 10:07:22.0866667', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Защищать от попадания текучего воздуха (сквозняков, механической вентиляции)', NameRu = N'Защищать от попадания текучего воздуха (сквозняков, механической вентиляции)'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (9, N'', '2018-05-03 10:07:22.0866667', '2018-05-03 10:07:22.0866667', CONVERT(bit, 'False'), N'Защищать от попадания текучего воздуха (сквозняков, механической вентиляции)', N'Защищать от попадания текучего воздуха (сквозняков, механической вентиляции)');
MERGE INTO dbo.Ref_StorageConditions t1 USING (SELECT 1 id) t2 ON (t1.Id = 10)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:07:22.0866667', DateUpdate = '2018-05-03 10:07:22.0866667', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Защищать от механических повреждений (в том числе от сдавливания, сгибания, скручивания, вытягивания)', NameRu = N'Защищать от механических повреждений (в том числе от сдавливания, сгибания, скручивания, вытягивания)'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (10, N'', '2018-05-03 10:07:22.0866667', '2018-05-03 10:07:22.0866667', CONVERT(bit, 'False'), N'Защищать от механических повреждений (в том числе от сдавливания, сгибания, скручивания, вытягивания)', N'Защищать от механических повреждений (в том числе от сдавливания, сгибания, скручивания, вытягивания)');
MERGE INTO dbo.Ref_StorageConditions t1 USING (SELECT 1 id) t2 ON (t1.Id = 11)
WHEN MATCHED THEN UPDATE  SET Code = N'', DateCreate = '2018-05-03 10:07:22.0866667', DateUpdate = '2018-05-03 10:07:22.0866667', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Защищать от воздействия влаги', NameRu = N'Защищать от воздействия влаги'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (11, N'', '2018-05-03 10:07:22.0866667', '2018-05-03 10:07:22.0866667', CONVERT(bit, 'False'), N'Защищать от воздействия влаги', N'Защищать от воздействия влаги');
GO
SET IDENTITY_INSERT dbo.Ref_StorageConditions OFF
GO