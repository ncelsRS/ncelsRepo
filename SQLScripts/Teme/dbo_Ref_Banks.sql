SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

SET IDENTITY_INSERT dbo.Ref_Banks ON
GO
MERGE INTO dbo.Ref_Banks t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)
WHEN MATCHED THEN UPDATE  SET Code = N'01', DateCreate = '2018-05-04 12:33:25.1900000', DateUpdate = '2018-05-04 12:33:25.1900000', IsConfirmed = CONVERT(bit, 'False'), IsDeleted = CONVERT(bit, 'False'), NameKz = N'Народный Банк', NameRu = N'Народный Банк'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsConfirmed, IsDeleted, NameKz, NameRu) VALUES (1, N'01', '2018-05-04 12:33:25.1900000', '2018-05-04 12:33:25.1900000', CONVERT(bit, 'False'), CONVERT(bit, 'False'), N'Народный Банк', N'Народный Банк');
MERGE INTO dbo.Ref_Banks t1 USING (SELECT 1 id) t2 ON (t1.Id = 2)
WHEN MATCHED THEN UPDATE  SET Code = N'02', DateCreate = '2018-05-04 12:33:25.1900000', DateUpdate = '2018-05-04 12:33:25.1900000', IsConfirmed = CONVERT(bit, 'False'), IsDeleted = CONVERT(bit, 'False'), NameKz = N'Казком', NameRu = N'Казком'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsConfirmed, IsDeleted, NameKz, NameRu) VALUES (2, N'02', '2018-05-04 12:33:25.1900000', '2018-05-04 12:33:25.1900000', CONVERT(bit, 'False'), CONVERT(bit, 'False'), N'Казком', N'Казком');
MERGE INTO dbo.Ref_Banks t1 USING (SELECT 1 id) t2 ON (t1.Id = 3)
WHEN MATCHED THEN UPDATE  SET Code = N'03', DateCreate = '2018-05-04 12:33:25.1900000', DateUpdate = '2018-05-04 12:33:25.1900000', IsConfirmed = CONVERT(bit, 'False'), IsDeleted = CONVERT(bit, 'False'), NameKz = N'Forte bank', NameRu = N'Forte bank'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsConfirmed, IsDeleted, NameKz, NameRu) VALUES (3, N'03', '2018-05-04 12:33:25.1900000', '2018-05-04 12:33:25.1900000', CONVERT(bit, 'False'), CONVERT(bit, 'False'), N'Forte bank', N'Forte bank');
GO
SET IDENTITY_INSERT dbo.Ref_Banks OFF
GO