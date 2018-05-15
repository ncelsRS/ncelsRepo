--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.5.327.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 04.05.2018 12:46:49
-- Версия сервера: 13.00.4001
--

SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

SET IDENTITY_INSERT dbo.Contracts ON
GO
MERGE INTO dbo.Contracts t1 USING (SELECT 1 id) t2 ON (t1.Id = 3)
WHEN MATCHED THEN UPDATE  SET ChoosePayer = 0, ContractForm = 0, ContractScope = N'Bekbol test', ContractType = 0, DateCreate = '2018-05-04 12:43:56.4566667', DateUpdate = '2018-05-04 12:43:56.4566667', DeclarantDetailId = 2, DeclarantId = 3, DeclarantIsManufacture = CONVERT(bit, 'False'), HolderType = 0, ManufacturDetailId = 2, ManufacturId = 3, MedicalDeviceNameKz = N'Bekbol test', MedicalDeviceNameRu = N'Bekbol test', Number = N'Bekbol test', PayerDetailId = 2, PayerId = 3, WorkflowId = N'Bekbol test', isDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, ChoosePayer, ContractForm, ContractScope, ContractType, DateCreate, DateUpdate, DeclarantDetailId, DeclarantId, DeclarantIsManufacture, HolderType, ManufacturDetailId, ManufacturId, MedicalDeviceNameKz, MedicalDeviceNameRu, Number, PayerDetailId, PayerId, WorkflowId, isDeleted) VALUES (3, 0, 0, N'Bekbol test', 0, '2018-05-04 12:43:56.4566667', '2018-05-04 12:43:56.4566667', 2, 3, CONVERT(bit, 'False'), 0, 2, 3, N'Bekbol test', N'Bekbol test', N'Bekbol test', 2, 3, N'Bekbol test', CONVERT(bit, 'False'));
GO
SET IDENTITY_INSERT dbo.Contracts OFF
GO