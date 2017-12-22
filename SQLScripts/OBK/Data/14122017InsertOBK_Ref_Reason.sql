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
           ,N'Не соответствует документации'
           ,N'Не соответствует документации'
           ,0
           ,GETDATE()
           ,0),
		   ('Party'
           ,N'Не соответствует маркировке'
           ,N'Не соответствует маркировке'
           ,0
           ,GETDATE()
           ,0),
		   ('Party'
           ,N'Не соответствует маркировке и документации'
           ,N'Не соответствует маркировке и документации'
           ,0
           ,GETDATE()
           ,0),
		   ('Party'
           ,N'Не соответствует доступа на склад'
           ,N'Не соответствует доступа на склад'
           ,0
           ,GETDATE()
           ,0)
  
GO


  UPDATE [ncels].[dbo].[OBK_Ref_Reason]
	SET [Code] = 'Declaration'
	 where [Code] IS NULL


