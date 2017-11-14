DECLARE @Id uniqueidentifier
DECLARE @NameRu NVARCHAR(255)
DECLARE @NameKz NVARCHAR(255)
DECLARE @NameGenitiveRu NVARCHAR(255)
DECLARE @NameGenitiveKz NVARCHAR(255)

SET	@Id = '9e48c346-4829-44f4-9897-358a47295771'
SET @NameRu = N'Доверенность'
SET @NameKz = @NameRu
SET @NameGenitiveRu = N'Доверенности'
SET @NameGenitiveKz = @NameGenitiveRu

IF NOT EXISTS(SELECT * FROM [OBK_Ref_ContractDocumentType] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_ContractDocumentType]
           ([Id]
           ,[NameRu]
           ,[NameKz]
		   ,[NameGenitiveRu]
		   ,[NameGenitiveKz]
		   )
     VALUES
           (@Id,
           @NameRu,
           @NameKz,
		   @NameGenitiveRu,
		   @NameGenitiveKz
		   )
END

SET	@Id = '9e48c346-4829-44f4-9897-358a47295772'
SET @NameRu = N'Устав'
SET @NameKz = @NameRu
SET @NameGenitiveRu = N'Устава'
SET @NameGenitiveKz = @NameGenitiveRu

IF NOT EXISTS(SELECT * FROM [OBK_Ref_ContractDocumentType] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_ContractDocumentType]
           ([Id]
           ,[NameRu]
           ,[NameKz]
		   ,[NameGenitiveRu]
		   ,[NameGenitiveKz]
		   )
     VALUES
           (@Id,
           @NameRu,
           @NameKz,
		   @NameGenitiveRu,
		   @NameGenitiveKz
		   )
END

SET	@Id = '9e48c346-4829-44f4-9897-358a47295773'
SET @NameRu = N'Приказ'
SET @NameKz = @NameRu
SET @NameGenitiveRu = N'Приказа'
SET @NameGenitiveKz = @NameGenitiveRu

IF NOT EXISTS(SELECT * FROM [OBK_Ref_ContractDocumentType] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_ContractDocumentType]
           ([Id]
           ,[NameRu]
           ,[NameKz]
		   ,[NameGenitiveRu]
		   ,[NameGenitiveKz]
		   )
     VALUES
           (@Id,
           @NameRu,
           @NameKz,
		   @NameGenitiveRu,
		   @NameGenitiveKz
		   )
END