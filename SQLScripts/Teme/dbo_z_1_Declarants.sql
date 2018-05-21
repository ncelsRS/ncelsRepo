SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

SET IDENTITY_INSERT dbo.Declarants ON
GO
MERGE INTO dbo.Declarants t1 USING (SELECT 1 id) t2 ON (t1.Id = 3)
WHEN MATCHED THEN UPDATE  SET CountryId = 1, DateCreate = '2018-05-04 12:18:46.1466667', DateUpdate = '2018-05-04 12:18:46.1466667', IdNumber = N'123456789012', IsConfirmed = CONVERT(bit, 'False'), IsDeleted = CONVERT(bit, 'False'), IsJuridical = CONVERT(bit, 'False'), IsResident = CONVERT(bit, 'False'), NameEn = N'Bekbol test', NameKz = N'Bekbol test', NameRu = N'Bekbol test', OrganizationFormId = 1
WHEN NOT MATCHED THEN INSERT (Id, CountryId, DateCreate, DateUpdate, IdNumber, IsConfirmed, IsDeleted, IsJuridical, IsResident, NameEn, NameKz, NameRu, OrganizationFormId) VALUES (3, 1, '2018-05-04 12:18:46.1466667', '2018-05-04 12:18:46.1466667', N'123456789012', CONVERT(bit, 'False'), CONVERT(bit, 'False'), CONVERT(bit, 'False'), CONVERT(bit, 'False'), N'Bekbol test', N'Bekbol test', N'Bekbol test', 1);
GO
SET IDENTITY_INSERT dbo.Declarants OFF
GO