USE [ncels]
GO

INSERT INTO [dbo].[OBK_Ref_Reason]
           ([Code]
           ,[NameRu]
           ,[NameKz]
           ,[ExpertiseResult]
           ,[CreatedDate]
           ,[IsDeleted])
     VALUES
           ('Party'
           ,N'�� ������������� ������������'
           ,N'�� ������������� ������������'
           ,0
           ,GETDATE()
           ,0),
		   ('Party'
           ,N'�� ������������� ����������'
           ,N'�� ������������� ����������'
           ,0
           ,GETDATE()
           ,0),
		   ('Party'
           ,N'�� ������������� ���������� � ������������'
           ,N'�� ������������� ���������� � ������������'
           ,0
           ,GETDATE()
           ,0),
		   ('Party'
           ,N'�� ������������� ������� �� �����'
           ,N'�� ������������� ������� �� �����'
           ,0
           ,GETDATE()
           ,0)
  
GO


  UPDATE [ncels].[dbo].[OBK_Ref_Reason]
	SET [Code] = 'Declaration'
	 where [Code] IS NULL


