USE [ncels]
GO

INSERT INTO [dbo].[OBK_Ref_LaboratoryMark]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[IsDeleted])
     VALUES
           (newid()
           ,NULL
           ,N'��������'
           ,N'��������'
           ,'False'),
		   (newid()
           ,NULL
           ,N'�������������'
           ,N'�������������'
           ,'False'),
		   (newid()
           ,NULL
           ,N'����� ������������ ����'
           ,N'����� ������������ ����'
           ,'False'),
		   (newid()
           ,NULL
           ,N'����� ����������'
           ,N'����� ����������'
           ,'False'),
		   (newid()
           ,NULL
           ,N'����� ���'
           ,N'����� ���'
           ,'False')
GO


