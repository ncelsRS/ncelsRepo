USE ncels
  UPDATE [ncels].[dbo].[Dictionaries]
  SET [Name] = N'ГФ РК'
  where [Type] = 'ProductSample' and [Name] = N'правилам'

  UPDATE [ncels].[dbo].[OBK_Applicant]
  SET [NameRu] = N'представитель КФ',
	  [NameKz] = N'представитель КФ'
  where [NameRu] = N'Представитель'

  
INSERT INTO [dbo].[OBK_Applicant]
           ([Id]
           ,[NameRu]
           ,[NameKz]
           ,[CreateDate]
           ,[ExpireDate])
     VALUES
           ( NEWID()
           ,N'представитель ТФ'
           ,N'представитель ТФ'
           ,GETDATE()
           ,NULL)
GO
