SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

SET IDENTITY_INSERT dbo.Ref_ServiceTypes ON
GO
MERGE INTO dbo.Ref_ServiceTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)
WHEN MATCHED THEN UPDATE  SET ApplicationTypeId = 1, Code = N'', DateCreate = '2018-05-03 10:05:56.2233333', DateUpdate = '2018-05-03 10:05:56.2233333', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Класс 1 - базовая', NameRu = N'Класс 1 - базовая', ParentId = NULL
WHEN NOT MATCHED THEN INSERT (Id, ApplicationTypeId, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu, ParentId) VALUES (1, 1, N'', '2018-05-03 10:05:56.2233333', '2018-05-03 10:05:56.2233333', CONVERT(bit, 'False'), N'Класс 1 - базовая', N'Класс 1 - базовая', NULL);
MERGE INTO dbo.Ref_ServiceTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 2)
WHEN MATCHED THEN UPDATE  SET ApplicationTypeId = 1, Code = N'', DateCreate = '2018-05-03 10:05:56.2233333', DateUpdate = '2018-05-03 10:05:56.2233333', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Класс 2А - базовая ставка', NameRu = N'Класс 2А - базовая ставка', ParentId = NULL
WHEN NOT MATCHED THEN INSERT (Id, ApplicationTypeId, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu, ParentId) VALUES (2, 1, N'', '2018-05-03 10:05:56.2233333', '2018-05-03 10:05:56.2233333', CONVERT(bit, 'False'), N'Класс 2А - базовая ставка', N'Класс 2А - базовая ставка', NULL);
MERGE INTO dbo.Ref_ServiceTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 3)
WHEN MATCHED THEN UPDATE  SET ApplicationTypeId = 1, Code = N'', DateCreate = '2018-05-03 10:05:56.2233333', DateUpdate = '2018-05-03 10:05:56.2233333', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Класс 2Б - базовая ставка', NameRu = N'Класс 2Б - базовая ставка', ParentId = NULL
WHEN NOT MATCHED THEN INSERT (Id, ApplicationTypeId, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu, ParentId) VALUES (3, 1, N'', '2018-05-03 10:05:56.2233333', '2018-05-03 10:05:56.2233333', CONVERT(bit, 'False'), N'Класс 2Б - базовая ставка', N'Класс 2Б - базовая ставка', NULL);
MERGE INTO dbo.Ref_ServiceTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 4)
WHEN MATCHED THEN UPDATE  SET ApplicationTypeId = 1, Code = N'', DateCreate = '2018-05-03 10:05:56.2233333', DateUpdate = '2018-05-03 10:05:56.2233333', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Класс 3 - базовая ставка', NameRu = N'Класс 3 - базовая ставка', ParentId = NULL
WHEN NOT MATCHED THEN INSERT (Id, ApplicationTypeId, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu, ParentId) VALUES (4, 1, N'', '2018-05-03 10:05:56.2233333', '2018-05-03 10:05:56.2233333', CONVERT(bit, 'False'), N'Класс 3 - базовая ставка', N'Класс 3 - базовая ставка', NULL);
MERGE INTO dbo.Ref_ServiceTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 5)
WHEN MATCHED THEN UPDATE  SET ApplicationTypeId = 2, Code = N'', DateCreate = '2018-05-03 10:05:56.2233333', DateUpdate = '2018-05-03 10:05:56.2233333', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Экспертиза при внесении изменений в регистрационное досье изделий медицинского назначения и медицинской техники', NameRu = N'Экспертиза при внесении изменений в регистрационное досье изделий медицинского назначения и медицинской техники', ParentId = NULL
WHEN NOT MATCHED THEN INSERT (Id, ApplicationTypeId, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu, ParentId) VALUES (5, 2, N'', '2018-05-03 10:05:56.2233333', '2018-05-03 10:05:56.2233333', CONVERT(bit, 'False'), N'Экспертиза при внесении изменений в регистрационное досье изделий медицинского назначения и медицинской техники', N'Экспертиза при внесении изменений в регистрационное досье изделий медицинского назначения и медицинской техники', NULL);
GO
SET IDENTITY_INSERT dbo.Ref_ServiceTypes OFF
GO