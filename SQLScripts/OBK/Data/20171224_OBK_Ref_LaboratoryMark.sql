USE [ncels]
GO

INSERT INTO [dbo].[OBK_Ref_LaboratoryMark]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[IsDeleted])
     VALUES
           (newid()
           ,NULL
           ,N'Описание'
           ,N'Описание'
           ,'False'),
		   (newid()
           ,NULL
           ,N'Идентификация'
           ,N'Идентификация'
           ,'False'),
		   (newid()
           ,NULL
           ,N'Масса распыляющего газа'
           ,N'Масса распыляющего газа'
           ,'False'),
		   (newid()
           ,NULL
           ,N'Объем пропилента'
           ,N'Объем пропилента'
           ,'False'),
		   (newid()
           ,NULL
           ,N'Число доз'
           ,N'Число доз'
           ,'False')
GO


