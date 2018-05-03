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

SET IDENTITY_INSERT dbo.Ref_ClassifierMedicalAreas ON
GO
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)
WHEN MATCHED THEN UPDATE  SET Code = N'010', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Акушерство и гинекология', NameRu = N'Акушерство и гинекология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (1, N'010', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Акушерство и гинекология', N'Акушерство и гинекология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 2)
WHEN MATCHED THEN UPDATE  SET Code = N'020', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Аллергология и иммунология', NameRu = N'Аллергология и иммунология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (2, N'020', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Аллергология и иммунология', N'Аллергология и иммунология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 3)
WHEN MATCHED THEN UPDATE  SET Code = N'030', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Анестезиология', NameRu = N'Анестезиология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (3, N'030', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Анестезиология', N'Анестезиология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 4)
WHEN MATCHED THEN UPDATE  SET Code = N'040', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Бактериология', NameRu = N'Бактериология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (4, N'040', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Бактериология', N'Бактериология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 5)
WHEN MATCHED THEN UPDATE  SET Code = N'050', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Гастроэнтерология', NameRu = N'Гастроэнтерология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (5, N'050', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Гастроэнтерология', N'Гастроэнтерология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 6)
WHEN MATCHED THEN UPDATE  SET Code = N'060', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Гематология', NameRu = N'Гематология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (6, N'060', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Гематология', N'Гематология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 7)
WHEN MATCHED THEN UPDATE  SET Code = N'070', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Генетика', NameRu = N'Генетика'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (7, N'070', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Генетика', N'Генетика');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 8)
WHEN MATCHED THEN UPDATE  SET Code = N'080', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Дерматовенерология', NameRu = N'Дерматовенерология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (8, N'080', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Дерматовенерология', N'Дерматовенерология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 9)
WHEN MATCHED THEN UPDATE  SET Code = N'090', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Диабетология', NameRu = N'Диабетология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (9, N'090', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Диабетология', N'Диабетология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 10)
WHEN MATCHED THEN UPDATE  SET Code = N'100', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Инфекционные болезни', NameRu = N'Инфекционные болезни'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (10, N'100', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Инфекционные болезни', N'Инфекционные болезни');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 11)
WHEN MATCHED THEN UPDATE  SET Code = N'110', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Кардиология', NameRu = N'Кардиология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (11, N'110', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Кардиология', N'Кардиология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 12)
WHEN MATCHED THEN UPDATE  SET Code = N'120', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Колопроктология', NameRu = N'Колопроктология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (12, N'120', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Колопроктология', N'Колопроктология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 13)
WHEN MATCHED THEN UPDATE  SET Code = N'130', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Клиническая лабораторная диагностика', NameRu = N'Клиническая лабораторная диагностика'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (13, N'130', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Клиническая лабораторная диагностика', N'Клиническая лабораторная диагностика');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 14)
WHEN MATCHED THEN UPDATE  SET Code = N'140', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Клиническая микология', NameRu = N'Клиническая микология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (14, N'140', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Клиническая микология', N'Клиническая микология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 15)
WHEN MATCHED THEN UPDATE  SET Code = N'150', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Косметология', NameRu = N'Косметология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (15, N'150', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Косметология', N'Косметология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 16)
WHEN MATCHED THEN UPDATE  SET Code = N'160', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Лечебная физкультура и спортивная медицина', NameRu = N'Лечебная физкультура и спортивная медицина'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (16, N'160', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Лечебная физкультура и спортивная медицина', N'Лечебная физкультура и спортивная медицина');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 17)
WHEN MATCHED THEN UPDATE  SET Code = N'170', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Медицинский массаж', NameRu = N'Медицинский массаж'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (17, N'170', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Медицинский массаж', N'Медицинский массаж');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 18)
WHEN MATCHED THEN UPDATE  SET Code = N'180', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Медицинская реабилитация', NameRu = N'Медицинская реабилитация'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (18, N'180', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Медицинская реабилитация', N'Медицинская реабилитация');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 19)
WHEN MATCHED THEN UPDATE  SET Code = N'190', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Наркология', NameRu = N'Наркология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (19, N'190', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Наркология', N'Наркология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 20)
WHEN MATCHED THEN UPDATE  SET Code = N'200', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Неврология', NameRu = N'Неврология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (20, N'200', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Неврология', N'Неврология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 21)
WHEN MATCHED THEN UPDATE  SET Code = N'210', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Нейрохирургия', NameRu = N'Нейрохирургия'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (21, N'210', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Нейрохирургия', N'Нейрохирургия');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 22)
WHEN MATCHED THEN UPDATE  SET Code = N'220', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Неонатология', NameRu = N'Неонатология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (22, N'220', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Неонатология', N'Неонатология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 23)
WHEN MATCHED THEN UPDATE  SET Code = N'230', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Нефрология', NameRu = N'Нефрология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (23, N'230', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Нефрология', N'Нефрология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 24)
WHEN MATCHED THEN UPDATE  SET Code = N'240', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Онкология', NameRu = N'Онкология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (24, N'240', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Онкология', N'Онкология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 25)
WHEN MATCHED THEN UPDATE  SET Code = N'250', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Оториноларингология', NameRu = N'Оториноларингология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (25, N'250', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Оториноларингология', N'Оториноларингология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 26)
WHEN MATCHED THEN UPDATE  SET Code = N'260', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Офтальмология (в том числе оптика)', NameRu = N'Офтальмология (в том числе оптика)'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (26, N'260', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Офтальмология (в том числе оптика)', N'Офтальмология (в том числе оптика)');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 27)
WHEN MATCHED THEN UPDATE  SET Code = N'270', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Патологическая анатомия', NameRu = N'Патологическая анатомия'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (27, N'270', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Патологическая анатомия', N'Патологическая анатомия');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 28)
WHEN MATCHED THEN UPDATE  SET Code = N'280', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Педиатрия', NameRu = N'Педиатрия'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (28, N'280', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Педиатрия', N'Педиатрия');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 29)
WHEN MATCHED THEN UPDATE  SET Code = N'290', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Пластическая хирургия', NameRu = N'Пластическая хирургия'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (29, N'290', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Пластическая хирургия', N'Пластическая хирургия');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 30)
WHEN MATCHED THEN UPDATE  SET Code = N'300', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Психиатрия', NameRu = N'Психиатрия'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (30, N'300', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Психиатрия', N'Психиатрия');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 31)
WHEN MATCHED THEN UPDATE  SET Code = N'310', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Пульмонология', NameRu = N'Пульмонология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (31, N'310', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Пульмонология', N'Пульмонология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 32)
WHEN MATCHED THEN UPDATE  SET Code = N'320', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Радиология', NameRu = N'Радиология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (32, N'320', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Радиология', N'Радиология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 33)
WHEN MATCHED THEN UPDATE  SET Code = N'330', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Радиотерапия', NameRu = N'Радиотерапия'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (33, N'330', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Радиотерапия', N'Радиотерапия');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 34)
WHEN MATCHED THEN UPDATE  SET Code = N'340', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Реаниматология', NameRu = N'Реаниматология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (34, N'340', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Реаниматология', N'Реаниматология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 35)
WHEN MATCHED THEN UPDATE  SET Code = N'350', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Рентгенология', NameRu = N'Рентгенология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (35, N'350', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Рентгенология', N'Рентгенология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 36)
WHEN MATCHED THEN UPDATE  SET Code = N'360', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Ревматология', NameRu = N'Ревматология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (36, N'360', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Ревматология', N'Ревматология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 37)
WHEN MATCHED THEN UPDATE  SET Code = N'370', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Рентгенэндоваскулярная диагностика и лечение', NameRu = N'Рентгенэндоваскулярная диагностика и лечение'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (37, N'370', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Рентгенэндоваскулярная диагностика и лечение', N'Рентгенэндоваскулярная диагностика и лечение');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 38)
WHEN MATCHED THEN UPDATE  SET Code = N'380', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Рефлексотерапия', NameRu = N'Рефлексотерапия'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (38, N'380', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Рефлексотерапия', N'Рефлексотерапия');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 39)
WHEN MATCHED THEN UPDATE  SET Code = N'390', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Сердечно-сосудистая хирургия', NameRu = N'Сердечно-сосудистая хирургия'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (39, N'390', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Сердечно-сосудистая хирургия', N'Сердечно-сосудистая хирургия');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 40)
WHEN MATCHED THEN UPDATE  SET Code = N'400', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Стоматология', NameRu = N'Стоматология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (40, N'400', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Стоматология', N'Стоматология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 41)
WHEN MATCHED THEN UPDATE  SET Code = N'410', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Терапия', NameRu = N'Терапия'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (41, N'410', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Терапия', N'Терапия');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 42)
WHEN MATCHED THEN UPDATE  SET Code = N'420', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Травматология и ортопедия', NameRu = N'Травматология и ортопедия'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (42, N'420', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Травматология и ортопедия', N'Травматология и ортопедия');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 43)
WHEN MATCHED THEN UPDATE  SET Code = N'430', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Трансфузиология', NameRu = N'Трансфузиология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (43, N'430', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Трансфузиология', N'Трансфузиология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 44)
WHEN MATCHED THEN UPDATE  SET Code = N'440', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Ультразвуковая диагностика', NameRu = N'Ультразвуковая диагностика'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (44, N'440', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Ультразвуковая диагностика', N'Ультразвуковая диагностика');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 45)
WHEN MATCHED THEN UPDATE  SET Code = N'450', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Урология', NameRu = N'Урология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (45, N'450', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Урология', N'Урология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 46)
WHEN MATCHED THEN UPDATE  SET Code = N'460', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Физиотерапия', NameRu = N'Физиотерапия'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (46, N'460', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Физиотерапия', N'Физиотерапия');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 47)
WHEN MATCHED THEN UPDATE  SET Code = N'470', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Фтизиатрия', NameRu = N'Фтизиатрия'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (47, N'470', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Фтизиатрия', N'Фтизиатрия');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 48)
WHEN MATCHED THEN UPDATE  SET Code = N'480', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Функциональная диагностика', NameRu = N'Функциональная диагностика'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (48, N'480', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Функциональная диагностика', N'Функциональная диагностика');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 49)
WHEN MATCHED THEN UPDATE  SET Code = N'490', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Хирургия', NameRu = N'Хирургия'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (49, N'490', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Хирургия', N'Хирургия');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 50)
WHEN MATCHED THEN UPDATE  SET Code = N'500', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Челюстно-лицевая хирургия', NameRu = N'Челюстно-лицевая хирургия'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (50, N'500', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Челюстно-лицевая хирургия', N'Челюстно-лицевая хирургия');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 51)
WHEN MATCHED THEN UPDATE  SET Code = N'510', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Эндокринология', NameRu = N'Эндокринология'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (51, N'510', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Эндокринология', N'Эндокринология');
MERGE INTO dbo.Ref_ClassifierMedicalAreas t1 USING (SELECT 1 id) t2 ON (t1.Id = 52)
WHEN MATCHED THEN UPDATE  SET Code = N'520', DateCreate = '2018-05-03 09:55:02.1900000', DateUpdate = '2018-05-03 09:55:02.1900000', IsDeleted = CONVERT(bit, 'False'), NameKz = N'Эндоскопия', NameRu = N'Эндоскопия'
WHEN NOT MATCHED THEN INSERT (Id, Code, DateCreate, DateUpdate, IsDeleted, NameKz, NameRu) VALUES (52, N'520', '2018-05-03 09:55:02.1900000', '2018-05-03 09:55:02.1900000', CONVERT(bit, 'False'), N'Эндоскопия', N'Эндоскопия');
GO
SET IDENTITY_INSERT dbo.Ref_ClassifierMedicalAreas OFF
GO