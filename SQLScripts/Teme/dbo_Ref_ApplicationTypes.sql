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

SET IDENTITY_INSERT dbo.Ref_ApplicationTypes ON
GO
MERGE INTO dbo.Ref_ApplicationTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)
WHEN MATCHED THEN UPDATE  SET Code = N'eaesrg,eaesgp', ContractForm = N'1,3', DateCreate = '2018-05-03 09:54:28.0933333', DateUpdate = '2018-05-03 09:54:28.0933333', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Экспертиза при государственной регистрации, перерегистрации и внесении изменений в регистрационное досье лекарственных средств, медицинских изделий (изделий медицинского назначения и медицинской техники)', NameRu = N'Экспертиза при государственной регистрации, перерегистрации и внесении изменений в регистрационное досье лекарственных средств, медицинских изделий (изделий медицинского назначения и медицинской техники)'
WHEN NOT MATCHED THEN INSERT (Id, Code, ContractForm, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (1, N'eaesrg,eaesgp', N'1,3', '2018-05-03 09:54:28.0933333', '2018-05-03 09:54:28.0933333', CONVERT(bit, 'False'), N'Экспертиза при государственной регистрации, перерегистрации и внесении изменений в регистрационное досье лекарственных средств, медицинских изделий (изделий медицинского назначения и медицинской техники)', N'Экспертиза при государственной регистрации, перерегистрации и внесении изменений в регистрационное досье лекарственных средств, медицинских изделий (изделий медицинского назначения и медицинской техники)');
MERGE INTO dbo.Ref_ApplicationTypes t1 USING (SELECT 1 id) t2 ON (t1.Id = 2)
WHEN MATCHED THEN UPDATE  SET Code = N'eaesrg,eaesgp', ContractForm = N'2', DateCreate = '2018-05-03 09:54:28.0933333', DateUpdate = '2018-05-03 09:54:28.0933333', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Экспертиза при внесении изменений в регистрационное досье медицинских изделий (изделий медицинского назначения и медицинской техники)', NameRu = N'Экспертиза при внесении изменений в регистрационное досье медицинских изделий (изделий медицинского назначения и медицинской техники)'
WHEN NOT MATCHED THEN INSERT (Id, Code, ContractForm, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (2, N'eaesrg,eaesgp', N'2', '2018-05-03 09:54:28.0933333', '2018-05-03 09:54:28.0933333', CONVERT(bit, 'False'), N'Экспертиза при внесении изменений в регистрационное досье медицинских изделий (изделий медицинского назначения и медицинской техники)', N'Экспертиза при внесении изменений в регистрационное досье медицинских изделий (изделий медицинского назначения и медицинской техники)');
GO
SET IDENTITY_INSERT dbo.Ref_ApplicationTypes OFF
GO