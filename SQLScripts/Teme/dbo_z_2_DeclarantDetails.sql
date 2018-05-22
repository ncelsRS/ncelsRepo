
SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

SET IDENTITY_INSERT dbo.DeclarantDetails ON
GO
MERGE INTO dbo.DeclarantDetails t1 USING (SELECT 1 id) t2 ON (t1.Id = 2)
WHEN MATCHED THEN UPDATE  SET BankBin = N'Bekbol test', BankId = 1, BankIik = N'Bekbol test', BankName = N'Bekbol test', BankSwift = N'Bekbol test', BossFirstName = N'Bekbol test', BossLastName = N'Bekbol test', BossMiddleName = N'Bekbol test', BossPositionKz = N'Bekbol test', BossPositionRu = N'Bekbol test', CurrencyId = 1, DateCreate = '2018-05-04 12:38:38.0166667', DateUpdate = '2018-05-04 12:38:38.0166667', DeclarantDocEndDate = '2018-05-04 12:38:38.0166667', DeclarantDocNumber = N'Bekbol test', DeclarantDocStartDate = '2018-05-04 12:38:38.0166667', DeclarantDocType = 0, DeclarantDocWithoutNumber = CONVERT(bit, 'False'), DeclarantId = 3, DeclarantPerpetualDoc = CONVERT(bit, 'False'), Email = N'Bekbol test', FactAddress = N'Bekbol test', LegalAddress = N'Bekbol test', Phone = N'Bekbol test', Phone2 = N'Bekbol test'
WHEN NOT MATCHED THEN INSERT (Id, BankBin, BankId, BankIik, BankName, BankSwift, BossFirstName, BossLastName, BossMiddleName, BossPositionKz, BossPositionRu, CurrencyId, DateCreate, DateUpdate, DeclarantDocEndDate, DeclarantDocNumber, DeclarantDocStartDate, DeclarantDocType, DeclarantDocWithoutNumber, DeclarantId, DeclarantPerpetualDoc, Email, FactAddress, LegalAddress, Phone, Phone2) VALUES (2, N'Bekbol test', 1, N'Bekbol test', N'Bekbol test', N'Bekbol test', N'Bekbol test', N'Bekbol test', N'Bekbol test', N'Bekbol test', N'Bekbol test', 1, '2018-05-04 12:38:38.0166667', '2018-05-04 12:38:38.0166667', '2018-05-04 12:38:38.0166667', N'Bekbol test', '2018-05-04 12:38:38.0166667', 0, CONVERT(bit, 'False'), 3, CONVERT(bit, 'False'), N'Bekbol test', N'Bekbol test', N'Bekbol test', N'Bekbol test', N'Bekbol test');
GO
SET IDENTITY_INSERT dbo.DeclarantDetails OFF
GO