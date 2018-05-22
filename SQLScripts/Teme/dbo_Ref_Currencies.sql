SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

SET IDENTITY_INSERT dbo.Ref_Currencies ON
GO
MERGE INTO dbo.Ref_Currencies t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)
WHEN MATCHED THEN UPDATE  SET Code = N'01', DateCreate = '2018-05-04 12:28:21.5300000', DateUpdate = '2018-05-04 12:28:21.5300000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Тенге', NameRu = N'Тенге'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (1, N'01', '2018-05-04 12:28:21.5300000', '2018-05-04 12:28:21.5300000', CONVERT(bit, 'False'), N'Тенге', N'Тенге');
MERGE INTO dbo.Ref_Currencies t1 USING (SELECT 1 id) t2 ON (t1.Id = 2)
WHEN MATCHED THEN UPDATE  SET Code = N'02', DateCreate = '2018-05-04 12:28:21.5300000', DateUpdate = '2018-05-04 12:28:21.5300000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Рубль', NameRu = N'Рубль'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (2, N'02', '2018-05-04 12:28:21.5300000', '2018-05-04 12:28:21.5300000', CONVERT(bit, 'False'), N'Рубль', N'Рубль');
MERGE INTO dbo.Ref_Currencies t1 USING (SELECT 1 id) t2 ON (t1.Id = 3)
WHEN MATCHED THEN UPDATE  SET Code = N'03', DateCreate = '2018-05-04 12:28:21.5300000', DateUpdate = '2018-05-04 12:28:21.5300000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Доллар', NameRu = N'Доллар'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (3, N'03', '2018-05-04 12:28:21.5300000', '2018-05-04 12:28:21.5300000', CONVERT(bit, 'False'), N'Доллар', N'Доллар');
GO
SET IDENTITY_INSERT dbo.Ref_Currencies OFF
GO