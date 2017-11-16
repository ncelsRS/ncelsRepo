DECLARE @Id int
DECLARE @Code nvarchar(50)
DECLARE @NameRu nvarchar(2000)
DECLARE @NameKz nvarchar(2000)
DECLARE @DateCreate datetime
DECLARE @IsDeleted bit

SET @Id = 7
SET @Code = N'notAgreed'
SET @NameRu = N'Несогласованный'
SET @NameKz = N'Несогласованный'
SET @DateCreate = GETDATE()
SET @IsDeleted = 0

IF NOT EXISTS(SELECT * FROM [OBK_Ref_StageStatus] WHERE Id = @Id)
BEGIN		   
INSERT INTO [OBK_Ref_StageStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted])
     VALUES
           (@Id
           ,@Code
           ,@NameRu
           ,@NameKz
           ,@DateCreate
           ,@IsDeleted)
END

SET @Id = 8
SET @Code = N'requiresRegistration'
SET @NameRu = N'Требует регистрации'
SET @NameKz = N'Требует регистрации'
SET @DateCreate = GETDATE()
SET @IsDeleted = 0

IF NOT EXISTS(SELECT * FROM [OBK_Ref_StageStatus] WHERE Id = @Id)
BEGIN		   
INSERT INTO [OBK_Ref_StageStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted])
     VALUES
           (@Id
           ,@Code
           ,@NameRu
           ,@NameKz
           ,@DateCreate
           ,@IsDeleted)
END

SET @Id = 9
SET @Code = N'requiresSigning'
SET @NameRu = N'Требует подписания'
SET @NameKz = N'Требует подписания'
SET @DateCreate = GETDATE()
SET @IsDeleted = 0

IF NOT EXISTS(SELECT * FROM [OBK_Ref_StageStatus] WHERE Id = @Id)
BEGIN		   
INSERT INTO [OBK_Ref_StageStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted])
     VALUES
           (@Id
           ,@Code
           ,@NameRu
           ,@NameKz
           ,@DateCreate
           ,@IsDeleted)
END

SET @Id = 10
SET @Code = N'active'
SET @NameRu = N'Активный'
SET @NameKz = N'Активный'
SET @DateCreate = GETDATE()
SET @IsDeleted = 0

IF NOT EXISTS(SELECT * FROM [OBK_Ref_StageStatus] WHERE Id = @Id)
BEGIN		   
INSERT INTO [OBK_Ref_StageStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted])
     VALUES
           (@Id
           ,@Code
           ,@NameRu
           ,@NameKz
           ,@DateCreate
           ,@IsDeleted)
END

