DECLARE @Id uniqueidentifier
DECLARE @NameRu NVARCHAR(255)
DECLARE @NameKz NVARCHAR(255)

SET @Id = 'c02f40cc-757c-42ba-a400-3f7937cf8600'
SET @NameRu = N'Класс 1'
SET @NameKz = @NameRu

IF NOT EXISTS(SELECT * FROM [OBK_Ref_DegreeRisk] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_DegreeRisk]
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

SET @Id = 'c02f40cc-757c-42ba-a400-3f7937cf8601'
SET @NameRu = N'Класс 2А'
SET @NameKz = @NameRu

IF NOT EXISTS(SELECT * FROM [OBK_Ref_DegreeRisk] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_DegreeRisk]
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

SET @Id = 'c02f40cc-757c-42ba-a400-3f7937cf8602'
SET @NameRu = N'Класс 2Б'
SET @NameKz = @NameRu

IF NOT EXISTS(SELECT * FROM [OBK_Ref_DegreeRisk] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_DegreeRisk]
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

SET @Id = 'c02f40cc-757c-42ba-a400-3f7937cf8603'
SET @NameRu = N'Класс 3'
SET @NameKz = @NameRu

IF NOT EXISTS(SELECT * FROM [OBK_Ref_DegreeRisk] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_DegreeRisk]
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