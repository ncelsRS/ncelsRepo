USE ncels
SET DATEFORMAT ymd
INSERT INTO dbo.OBK_Ref_Stage(Id, Code, NameRu, NameKz, DateCreate, IsDeleted, DateEdit) VALUES(15, N'15', N'����������', N'����������', '2017-12-15 10:34:09.000', CONVERT(bit, 'False'), NULL)
GO

USE ncels
SET DATEFORMAT ymd
INSERT INTO dbo.OBK_Ref_StageStatus(Id, Code, NameRu, NameKz, DateCreate, IsDeleted) VALUES(14, N'OPCompleted', N'������ ������� ������������ ���������', N'������ ������� ������������ ���������', '2017-12-27 17:10:51.247', CONVERT(bit, 'False'))
GO
