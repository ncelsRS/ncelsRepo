USE [ncels]
GO
INSERT INTO [dbo].[EMP_Ref_HolderType]
           ([Id]
           ,[NameRu]
           ,[NameKz]
           ,[IsDeleted])
     VALUES
           (NEWID()
           ,N'Заявителем'
           ,N'Заявителем'
           ,'False'),
		   (NEWID()
           ,N'Производителем'
           ,N'Производителем'
           ,'False')
GO


