USE [ncels]
GO

INSERT INTO [dbo].[OBK_Ref_Status]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted]
           ,[DateEdit])
     VALUES
           (17
           ,17
           ,N'����� �������������� �����'
           ,N'����� �������������� �����'
           ,GETDATE()
           ,0
           ,NULL)
