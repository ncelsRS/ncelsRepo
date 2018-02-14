--
-- Скрипт сгенерирован Devart dbForge Studio for SQL Server, Версия 5.5.327.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 08.02.2018 17:07:19
-- Версия сервера: 14.00.3008
--

SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 1)
WHEN MATCHED THEN UPDATE  SET Code = N'inQueue', NameRu = N'На распределение', NameKz = N'На распределение', DateCreate = '2017-01-01 00:00:00.000', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (1, N'inQueue', N'На распределение', N'На распределение', '2017-01-01 00:00:00.000', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 2)
WHEN MATCHED THEN UPDATE  SET Code = N'inWork', NameRu = N'В работе', NameKz = N'В работе', DateCreate = '2017-01-01 00:00:00.000', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (2, N'inWork', N'В работе', N'В работе', '2017-01-01 00:00:00.000', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 3)
WHEN MATCHED THEN UPDATE  SET Code = N'inReWork', NameRu = N'На даработке', NameKz = N'На даработке', DateCreate = '2017-01-01 00:00:00.000', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (3, N'inReWork', N'На даработке', N'На даработке', '2017-01-01 00:00:00.000', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 4)
WHEN MATCHED THEN UPDATE  SET Code = N'completed', NameRu = N'Выполнен', NameKz = N'Выполнен', DateCreate = '2017-01-01 00:00:00.000', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (4, N'completed', N'Выполнен', N'Выполнен', '2017-01-01 00:00:00.000', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 5)
WHEN MATCHED THEN UPDATE  SET Code = N'onCorrection', NameRu = N'На корректировке у заявителя', NameKz = N'На корректировке у заявителя', DateCreate = '2017-10-03 18:21:47.763', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (5, N'onCorrection', N'На корректировке у заявителя', N'На корректировке у заявителя', '2017-10-03 18:21:47.763', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 6)
WHEN MATCHED THEN UPDATE  SET Code = N'onAgreement', NameRu = N'На согласовании у руководителя', NameKz = N'На согласовании у руководителя', DateCreate = '2017-10-03 18:21:47.763', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (6, N'onAgreement', N'На согласовании у руководителя', N'На согласовании у руководителя', '2017-10-03 18:21:47.763', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 7)
WHEN MATCHED THEN UPDATE  SET Code = N'notAgreed', NameRu = N'Несогласованный', NameKz = N'Несогласованный', DateCreate = '2017-10-06 19:30:10.053', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (7, N'notAgreed', N'Несогласованный', N'Несогласованный', '2017-10-06 19:30:10.053', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 8)
WHEN MATCHED THEN UPDATE  SET Code = N'requiresRegistration', NameRu = N'Требует регистрации', NameKz = N'Требует регистрации', DateCreate = '2017-10-06 19:30:10.053', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (8, N'requiresRegistration', N'Требует регистрации', N'Требует регистрации', '2017-10-06 19:30:10.053', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 9)
WHEN MATCHED THEN UPDATE  SET Code = N'requiresSigning', NameRu = N'Требует подписания', NameKz = N'Требует подписания', DateCreate = '2017-10-06 19:30:10.053', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (9, N'requiresSigning', N'Требует подписания', N'Требует подписания', '2017-10-06 19:30:10.053', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 10)
WHEN MATCHED THEN UPDATE  SET Code = N'active', NameRu = N'Активный', NameKz = N'Активный', DateCreate = '2017-10-06 19:30:10.053', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (10, N'active', N'Активный', N'Активный', '2017-10-06 19:30:10.053', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 11)
WHEN MATCHED THEN UPDATE  SET Code = N'requiresConclusion', NameRu = N'Требует заключения', NameKz = N'Требует заключения', DateCreate = '2017-11-03 18:59:17.980', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (11, N'requiresConclusion', N'Требует заключения', N'Требует заключения', '2017-11-03 18:59:17.980', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 12)
WHEN MATCHED THEN UPDATE  SET Code = N'onExpDocument', NameRu = N'На экспертизе документов', NameKz = N'На экспертизе документов', DateCreate = '2017-11-03 18:59:17.980', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (12, N'onExpDocument', N'На экспертизе документов', N'На экспертизе документов', '2017-11-03 18:59:17.980', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 13)
WHEN MATCHED THEN UPDATE  SET Code = N'documentReviewCompleted', NameRu = N'Экспертиза документов завершена', NameKz = N'Экспертиза документов завершена', DateCreate = '2017-12-09 17:10:51.247', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (13, N'documentReviewCompleted', N'Экспертиза документов завершена', N'Экспертиза документов завершена', '2017-12-09 17:10:51.247', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 14)
WHEN MATCHED THEN UPDATE  SET Code = N'OPCompleted', NameRu = N'Оценка условий производства завершена', NameKz = N'Оценка условий производства завершена', DateCreate = '2017-12-27 17:10:51.247', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (14, N'OPCompleted', N'Оценка условий производства завершена', N'Оценка условий производства завершена', '2017-12-27 17:10:51.247', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 16)
WHEN MATCHED THEN UPDATE  SET Code = N'OPProgramSigned', NameRu = N'Программа сохранена', NameKz = N'Программа сохранена', DateCreate = '2018-01-25 16:45:06.620', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (16, N'OPProgramSigned', N'Программа сохранена', N'Программа сохранена', '2018-01-25 16:45:06.620', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 17)
WHEN MATCHED THEN UPDATE  SET Code = N'OPProgramInConfirm', NameRu = N'Программа на подтверждении', NameKz = N'Программа на подтверждении', DateCreate = '2018-01-25 16:47:10.397', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (17, N'OPProgramInConfirm', N'Программа на подтверждении', N'Программа на подтверждении', '2018-01-25 16:47:10.397', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 18)
WHEN MATCHED THEN UPDATE  SET Code = N'OPProgramConfirmed', NameRu = N'Программа подтверждена', NameKz = N'Программа подтверждена', DateCreate = '2018-01-25 16:47:47.757', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (18, N'OPProgramConfirmed', N'Программа подтверждена', N'Программа подтверждена', '2018-01-25 16:47:47.757', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 19)
WHEN MATCHED THEN UPDATE  SET Code = N'OPProgramInReWork', NameRu = N'Программа на доработке', NameKz = N'Программа на доработке', DateCreate = '2018-01-29 11:09:49.380', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (19, N'OPProgramInReWork', N'Программа на доработке', N'Программа на доработке', '2018-01-29 11:09:49.380', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 20)
WHEN MATCHED THEN UPDATE  SET Code = N'OPReportNew', NameRu = N'Отчет ОП создан', NameKz = N'Отчет ОП создан', DateCreate = '2018-02-01 12:46:25.957', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (20, N'OPReportNew', N'Отчет ОП создан', N'Отчет ОП создан', '2018-02-01 12:46:25.957', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 21)
WHEN MATCHED THEN UPDATE  SET Code = N'OPReportInConfirm', NameRu = N'Отчет ОП на подтверждении', NameKz = N'Отчет ОП на подтверждении', DateCreate = '2018-02-01 12:49:19.630', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (21, N'OPReportInConfirm', N'Отчет ОП на подтверждении', N'Отчет ОП на подтверждении', '2018-02-01 12:49:19.630', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 22)
WHEN MATCHED THEN UPDATE  SET Code = N'OPReportConfirmed', NameRu = N'Отчет ОП согласован', NameKz = N'Отчет ОП согласован', DateCreate = '2018-02-01 12:51:06.843', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (22, N'OPReportConfirmed', N'Отчет ОП согласован', N'Отчет ОП согласован', '2018-02-01 12:51:06.843', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 23)
WHEN MATCHED THEN UPDATE  SET Code = N'OPReportOnEC', NameRu = N'На экспертном совете', NameKz = N'На экспертном совете', DateCreate = '2018-02-01 15:06:15.293', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (23, N'OPReportOnEC', N'На экспертном совете', N'На экспертном совете', '2018-02-01 15:06:15.293', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 24)
WHEN MATCHED THEN UPDATE  SET Code = N'OPReportOnReWork', NameRu = N'Отчет ОП на доработке', NameKz = N'Отчет ОП на доработке', DateCreate = '2018-02-01 15:08:49.617', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (24, N'OPReportOnReWork', N'Отчет ОП на доработке', N'Отчет ОП на доработке', '2018-02-01 15:08:49.617', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 25)
WHEN MATCHED THEN UPDATE  SET Code = N'OPProgramNew', NameRu = N'Разработка программы', NameKz = N'Разработка программы', DateCreate = '2018-01-25 16:44:09.363', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (25, N'OPProgramNew', N'Разработка программы', N'Разработка программы', '2018-01-25 16:44:09.363', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 26)
WHEN MATCHED THEN UPDATE  SET Code = N'OPReportCompleted', NameRu = N'Отчет ОП завершен', NameKz = N'Отчет ОП завершен', DateCreate = '2018-02-05 10:22:11.597', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (26, N'OPReportCompleted', N'Отчет ОП завершен', N'Отчет ОП завершен', '2018-02-05 10:22:11.597', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 98)
WHEN MATCHED THEN UPDATE  SET Code = N'onApprove', NameRu = N'На утверждении', NameKz = N'На утверждении', DateCreate = '2018-02-01 09:03:00.653', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (98, N'onApprove', N'На утверждении', N'На утверждении', '2018-02-01 09:03:00.653', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 99)
WHEN MATCHED THEN UPDATE  SET Code = N'taskNew', NameRu = N'Сформирован', NameKz = N'Сформирован', DateCreate = '2018-01-09 12:28:23.090', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (99, N'taskNew', N'Сформирован', N'Сформирован', '2018-01-09 12:28:23.090', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 100)
WHEN MATCHED THEN UPDATE  SET Code = N'taskAcceptCoz', NameRu = N'Образцы приняты ЦОЗ', NameKz = N'Образцы приняты ЦОЗ', DateCreate = '2018-01-09 12:28:23.090', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (100, N'taskAcceptCoz', N'Образцы приняты ЦОЗ', N'Образцы приняты ЦОЗ', '2018-01-09 12:28:23.090', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 101)
WHEN MATCHED THEN UPDATE  SET Code = N'taskSendRC', NameRu = N'Передано в ИЦл', NameKz = N'Передано в ИЦл', DateCreate = '2018-01-09 12:28:23.090', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (101, N'taskSendRC', N'Передано в ИЦл', N'Передано в ИЦл', '2018-01-09 12:28:23.090', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 102)
WHEN MATCHED THEN UPDATE  SET Code = N'taskAcceptRC', NameRu = N'Принято ИЦл', NameKz = N'Принято ИЦл', DateCreate = '2018-01-09 12:28:23.090', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (102, N'taskAcceptRC', N'Принято ИЦл', N'Принято ИЦл', '2018-01-09 12:28:23.090', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 103)
WHEN MATCHED THEN UPDATE  SET Code = N'taskSendLab', NameRu = N'Передано в лабораторию', NameKz = N'Передано в лабораторию', DateCreate = '2018-01-09 12:28:23.090', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (103, N'taskSendLab', N'Передано в лабораторию', N'Передано в лабораторию', '2018-01-09 12:28:23.090', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 104)
WHEN MATCHED THEN UPDATE  SET Code = N'requiresIssuingZBKCopy', NameRu = N'Требует выдачи копии ЗБК', NameKz = N'Требует выдачи копии ЗБК', DateCreate = '2018-02-01 09:04:02.307', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (104, N'requiresIssuingZBKCopy', N'Требует выдачи копии ЗБК', N'Требует выдачи копии ЗБК', '2018-02-01 09:04:02.307', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 27)
WHEN MATCHED THEN UPDATE  SET Code = N'OPMotivatedRefusalNew', NameRu = N'Формирование мотивированного отказа', NameKz = N'Формирование мотивированного отказа', DateCreate = '2018-02-08 17:05:38.107', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (27, N'OPMotivatedRefusalNew', N'Формирование мотивированного отказа', N'Формирование мотивированного отказа', '2018-02-08 17:05:38.107', CONVERT(bit, 'False'));
GO