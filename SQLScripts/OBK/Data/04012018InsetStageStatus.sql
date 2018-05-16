USE [ncels]
GO

INSERT INTO [dbo].[OBK_Ref_StageStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted])
     VALUES
           ((select max(Id) + 1 from [ncels].[dbo].[OBK_Ref_StageStatus])
           ,'requiresIssuingZBKCopy'
           ,N'Требует выдачи копии ЗБК'
           ,N'Требует выдачи копии ЗБК'
           ,GETDATE()
           ,0)
GO


