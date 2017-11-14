USE [ncelsProd]
GO

INSERT INTO [dbo].[OBK_Ref_StageStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted])
     VALUES
           ('11'
           ,'requiresConclusion'
           ,'Требует заключения'
           ,'Требует заключения'
           ,'2017-11-03T18:59:17.980'
           ,'False')
GO

INSERT INTO [dbo].[OBK_Ref_StageStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted])
     VALUES
           ('12'
           ,'onExpDocument'
           ,'На экспертизе документов'
           ,'На экспертизе документов'
           ,'2017-11-03T18:59:17.980'
           ,'False')
GO


