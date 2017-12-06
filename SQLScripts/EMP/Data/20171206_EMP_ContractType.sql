USE [ncels]
GO

INSERT INTO [dbo].[EMP_Ref_ContractType]
           ([Id]
           ,[NameRu]
           ,[NameKz]
           ,[IsDeleted])
     VALUES
           (NEWID()
           ,N'Регистрация'
           ,N'Регистрация'
           ,'False'),
		   (NEWID()
           ,N'Перерегистрация'
           ,N'Перерегистрация'
           ,'False'),
		   (NEWID()
           ,N'Внесение изменений'
           ,N'Внесение изменений'
           ,'False')
GO


