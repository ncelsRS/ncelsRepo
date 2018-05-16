DECLARE @Id uniqueidentifier
DECLARE @NameRu NVARCHAR(255)
DECLARE @NameKz NVARCHAR(255)

SET @Id = '9106d5e8-35dc-4178-8882-b30166de4c80'
SET @NameRu = N'Документ/Экспертиза'
SET @NameKz = @NameRu

IF NOT EXISTS(SELECT * FROM [OBK_Ref_ServiceType] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_ServiceType]
           ([Id]
           ,[NameRu]
           ,[NameKz]
		   )
     VALUES
           (@Id,
           @NameRu,
           @NameKz
		   )
END

SET @Id = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @NameRu = N'ЛС'
SET @NameKz = @NameRu

IF NOT EXISTS(SELECT * FROM [OBK_Ref_ServiceType] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_ServiceType]
           ([Id]
           ,[NameRu]
           ,[NameKz]
		   )
     VALUES
           (@Id,
           @NameRu,
           @NameKz
		   )
END

SET @Id = '9106d5e8-35dc-4178-8882-b30166de4c82'
SET @NameRu = N'ИМН'
SET @NameKz = @NameRu

IF NOT EXISTS(SELECT * FROM [OBK_Ref_ServiceType] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_ServiceType]
           ([Id]
           ,[NameRu]
           ,[NameKz]
		   )
     VALUES
           (@Id,
           @NameRu,
           @NameKz
		   )
END
