--
-- ������ ������������ Devart dbForge Studio for SQL Server, ������ 5.5.311.0
-- �������� �������� ��������: http://www.devart.com/ru/dbforge/sql/studio
-- ���� �������: 03.01.2018 14:51:36
-- ������ �������: 14.00.3008
--

SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 15)
WHEN MATCHED THEN UPDATE  SET Code = N'reportCompleted', NameRu = N'����������� �����', NameKz = N'����������� �����', DateCreate = '2018-01-03 14:36:51.063', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (15, N'reportCompleted', N'����������� �����', N'����������� �����', '2018-01-03 14:36:51.063', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 16)
WHEN MATCHED THEN UPDATE  SET Code = N'inWorkOfCommission', NameRu = N'� ������ ��������', NameKz = N'� ������ ��������', DateCreate = '2018-01-03 14:38:24.783', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (16, N'inWorkOfCommission', N'� ������ ��������', N'� ������ ��������', '2018-01-03 14:38:24.783', CONVERT(bit, 'False'));
MERGE INTO ncels.dbo.OBK_Ref_StageStatus t1 USING (SELECT 1 id) t2 ON (t1.Id = 17)
WHEN MATCHED THEN UPDATE  SET Code = N'onExpertCouncil', NameRu = N'�� ���������� ������', NameKz = N'�� ���������� ������', DateCreate = '2018-01-03 14:39:43.153', IsDeleted = CONVERT(bit, 'False')
WHEN NOT MATCHED THEN INSERT (Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES (17, N'onExpertCouncil', N'�� ���������� ������', N'�� ���������� ������', '2018-01-03 14:39:43.153', CONVERT(bit, 'False'));
GO