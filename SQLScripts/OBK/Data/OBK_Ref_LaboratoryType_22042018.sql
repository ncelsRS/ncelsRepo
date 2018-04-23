SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

UPDATE OBK_Ref_LaboratoryType SET IsDeleted = 'True'
GO

INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb00', N'Физико-химические испытания', N'Физико-химические испытания', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb01', N'По физическим показателям', N'По физическим показателям', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb02', N'Микробиологическая чистота', N'Микробиологическая чистота', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb03', N'Стерильность', N'Стерильность', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb04', N'Токсичность', N'Токсичность', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb05', N'Пирогенность', N'Пирогенность', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb06', N'Санитарно-химические показатели', N'Санитарно-химические показатели', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb07', N'Биологические испытания (Совместимость с биологическими тканями)', N'Биологические испытания (Совместимость с биологическими тканями)', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb08', N'Биологические испытания (Местно-раздражающее действие)', N'Биологические испытания (Местно-раздражающее действие)', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb09', N'Биологические испытания (Гемолитическая активность)', N'Биологические испытания (Гемолитическая активность)', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb10', N'Вирусная безопасность', N'Вирусная безопасность', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb11', N'Специфическая активность', N'Специфическая активность', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb12', N'Биологические испытания (Имплантационный тест)', N'Биологические испытания (Имплантационный тест)', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb13', N'Биологические испытания (Количественное определение)', N'Биологические испытания (Количественное определение)', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb14', N'Токсичность (Количественное определение)', N'Токсичность (Количественное определение)', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb15', N'Стерильность (метод мембранной фильтрации)', N'Стерильность (метод мембранной фильтрации)', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb16', N'Стерильность (метод прямого посева)', N'Стерильность (метод прямого посева)', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb17', N'Стерильность (стерильные ИМН)', N'Стерильность (стерильные ИМН)', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb18', N'Биологические испытания (Бактериальные эндотоксины)', N'Биологические испытания (Бактериальные эндотоксины)', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb19', N'Физико-химические испытания (Маркировка и упаковка)', N'Физико-химические испытания (Маркировка и упаковка)', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb20', N'Физико-химические испытания (Маркировка)', N'Физико-химические испытания (Маркировка)', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb21', N'Физико-химические испытания (Упаковка)', N'Физико-химические испытания (Упаковка)', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb22', N'По физическим показателям (Маркировка и упаковка ИМН)', N'По физическим показателям (Маркировка и упаковка ИМН)', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb23', N'По физическим показателям (Маркировка)', N'По физическим показателям (Маркировка)', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb24', N'По физическим показателям (Упаковка)', N'По физическим показателям (Упаковка)', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb25', N'Физико-химические испытания (описание, упаковка и маркировка)', N'Физико-химические испытания (описание, упаковка и маркировка)', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb26', N'Бактериальные эндотоксины', N'Бактериальные эндотоксины', CONVERT(bit, 'False'))
INSERT ncelsTest.dbo.OBK_Ref_LaboratoryType(Id, NameRu, NameKz, IsDeleted) VALUES ('eb4bde3e-1f1b-44a6-9135-efe58677eb27', N'Родственные примеси', N'Родственные примеси', CONVERT(bit, 'False'))
GO