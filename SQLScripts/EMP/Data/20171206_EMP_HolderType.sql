USE [ncels]
GO
INSERT INTO [dbo].[EMP_Ref_HolderType]
           ([Id]
           ,[NameRu]
           ,[NameKz]
           ,[IsDeleted])
     VALUES
           (NEWID()
           ,N'����������'
           ,N'����������'
           ,'False'),
		   (NEWID()
           ,N'��������������'
           ,N'��������������'
           ,'False')
GO


