SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

SET IDENTITY_INSERT dbo.Ref_OrganizationForms ON
GO
MERGE INTO dbo.Ref_OrganizationForms t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)
WHEN MATCHED THEN UPDATE  SET Code = N'01', DateCreate = '2018-05-04 12:17:58.6233333', DateUpdate = '2018-05-04 12:17:58.6233333', IsConfirmed = CONVERT(bit, 'False'), IsDeleted = CONVERT(bit, 'False'), NameKz = N'ТОО', NameRu = N'ТОО'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsConfirmed, IsDeleted, NameKz, NameRu) VALUES (1, N'01', '2018-05-04 12:17:58.6233333', '2018-05-04 12:17:58.6233333', CONVERT(bit, 'False'), CONVERT(bit, 'False'), N'ТОО', N'ТОО');
MERGE INTO dbo.Ref_OrganizationForms t1 USING (SELECT 1 id) t2 ON (t1.Id = 2)
WHEN MATCHED THEN UPDATE  SET Code = N'02', DateCreate = '2018-05-04 12:17:58.6233333', DateUpdate = '2018-05-04 12:17:58.6233333', IsConfirmed = CONVERT(bit, 'False'), IsDeleted = CONVERT(bit, 'False'), NameKz = N'АО', NameRu = N'АО'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsConfirmed, IsDeleted, NameKz, NameRu) VALUES (2, N'02', '2018-05-04 12:17:58.6233333', '2018-05-04 12:17:58.6233333', CONVERT(bit, 'False'), CONVERT(bit, 'False'), N'АО', N'АО');
GO
SET IDENTITY_INSERT dbo.Ref_OrganizationForms OFF
GO