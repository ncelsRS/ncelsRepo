USE ncels
  UPDATE [ncels].[dbo].[Dictionaries]
  SET [Name] = N'�� ��'
  where [Type] = 'ProductSample' and [Name] = N'��������'

  UPDATE [ncels].[dbo].[OBK_Applicant]
  SET [NameRu] = N'������������� ��',
	  [NameKz] = N'������������� ��'
  where [NameRu] = N'�������������'

  
INSERT INTO [dbo].[OBK_Applicant]
           ([Id]
           ,[NameRu]
           ,[NameKz]
           ,[CreateDate]
           ,[ExpireDate])
     VALUES
           ( NEWID()
           ,N'������������� ��'
           ,N'������������� ��'
           ,GETDATE()
           ,NULL)
GO
