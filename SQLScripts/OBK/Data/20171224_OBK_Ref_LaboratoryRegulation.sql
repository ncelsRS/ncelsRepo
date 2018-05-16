USE [ncels]
GO

INSERT INTO [dbo].[OBK_Ref_LaboratoryRegulation]
           ([Id]
           ,[LaboratoryMarkId]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[IsDeleted])
     select
           newId()
           ,OBK_Ref_LaboratoryMark.Id as LaboratoryMarkId
           ,NULL
           ,N'Сб. ФС 2010'
           ,N'Сб. ФС 2010'
           ,'False'
	from OBK_Ref_LaboratoryMark where NameRu=N'Идентификация'
GO

INSERT INTO [dbo].[OBK_Ref_LaboratoryRegulation]
           ([Id]
           ,[LaboratoryMarkId]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[IsDeleted])
     select
           newId()
           ,OBK_Ref_LaboratoryMark.Id as LaboratoryMarkId
           ,NULL
           ,N'Сб. СП 2010'
           ,N'Сб. СП 2010'
           ,'False'
	from OBK_Ref_LaboratoryMark where NameRu=N'Масса распыляющего газа'
GO

INSERT INTO [dbo].[OBK_Ref_LaboratoryRegulation]
           ([Id]
           ,[LaboratoryMarkId]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[IsDeleted])
     select
           newId()
           ,OBK_Ref_LaboratoryMark.Id as LaboratoryMarkId
           ,NULL
           ,N'Сб. АНД 2010-2015'
           ,N'Сб. АНД 2010-2015'
           ,'False'
	from OBK_Ref_LaboratoryMark where NameRu=N'Число доз'
GO

INSERT INTO [dbo].[OBK_Ref_LaboratoryRegulation]
           ([Id]
           ,[LaboratoryMarkId]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[IsDeleted])
     select
           newId()
           ,OBK_Ref_LaboratoryMark.Id as LaboratoryMarkId
           ,NULL
           ,N'Сб. НД 2010'
           ,N'Сб. НД 2010'
           ,'False'
	from OBK_Ref_LaboratoryMark where NameRu=N'Объем пропилента'
GO

INSERT INTO [dbo].[OBK_Ref_LaboratoryRegulation]
           ([Id]
           ,[LaboratoryMarkId]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[IsDeleted])
     select
           newId()
           ,OBK_Ref_LaboratoryMark.Id as LaboratoryMarkId
           ,NULL
           ,N'ГФ РК'
           ,N'ГФ РК'
           ,'False'
	from OBK_Ref_LaboratoryMark where NameRu=N'Описание'
GO


