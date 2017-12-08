USE [ncels]
GO

INSERT INTO [dbo].[EMP_Ref_DegreeRisk]
           ([Id]
           ,[NameRu]
           ,[NameKz]
           ,[Code])
     VALUES
           (newid()
           ,N'Класс 1'
           ,N'Класс 1'
           ,N'1'),
		   (newid()
           ,N'Класс 2а'
           ,N'Класс 2а'
           ,N'1'),
		   (newid()
           ,N'Класс 2б'
           ,N'Класс 2б'
           ,N'2'),
		   (newid()
           ,N'Класс 3'
           ,N'Класс 3'
           ,N'2'),
		   (newid()
           ,N'МИБП (инвитро)'
           ,N'МИБП (инвитро)'
           ,N'2')
GO


