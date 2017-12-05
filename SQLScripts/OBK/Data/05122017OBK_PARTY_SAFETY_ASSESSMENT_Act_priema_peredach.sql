USE [ncels]
GO

INSERT INTO [dbo].[Dictionaries]
           ([Id]
           ,[Code]
           ,[Name]
           ,[Type]
           ,[DisplayName]
           ,[NameKz]
           ,[IsGuide])
     VALUES
           ( NEWID ()  
           ,2
           ,N'Акт приема-передачи'
           ,'OBK_PARTY_SAFETY_ASSESSMENT'
           ,N'Акт приема-передачи'
           ,N'Акт приема-передачи'
           ,0)


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
           (13
		   ,'documentReviewCompleted'
           ,N'Экспертиза документов завершена'
           ,N'Экспертиза документов завершена'
           ,GETDATE() 
           ,0)
GO

USE [ncels]
GO

INSERT INTO [dbo].[OBK_Applicant]
           ([Id]
           ,[NameRu]
           ,[NameKz]
           ,[CreateDate]
           ,[ExpireDate])
     VALUES
           (NEWID()
           ,N'Представитель'
           ,N'Представитель'
           ,GETDATE()
           ,NULL
		   )
GO






