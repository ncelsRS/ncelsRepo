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
           ,N'��. �� 2010'
           ,N'��. �� 2010'
           ,'False'
	from OBK_Ref_LaboratoryMark where NameRu=N'�������������'
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
           ,N'��. �� 2010'
           ,N'��. �� 2010'
           ,'False'
	from OBK_Ref_LaboratoryMark where NameRu=N'����� ������������ ����'
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
           ,N'��. ��� 2010-2015'
           ,N'��. ��� 2010-2015'
           ,'False'
	from OBK_Ref_LaboratoryMark where NameRu=N'����� ���'
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
           ,N'��. �� 2010'
           ,N'��. �� 2010'
           ,'False'
	from OBK_Ref_LaboratoryMark where NameRu=N'����� ����������'
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
           ,N'�� ��'
           ,N'�� ��'
           ,'False'
	from OBK_Ref_LaboratoryMark where NameRu=N'��������'
GO


