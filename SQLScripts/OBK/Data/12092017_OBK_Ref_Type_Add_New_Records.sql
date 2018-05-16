SET IDENTITY_INSERT OBK_Ref_Type ON

DECLARE @Id INT = NULL
DECLARE @Code nvarchar(max) = null
DECLARE @NameRu nvarchar(max) = null
DECLARE @NameKz nvarchar(max) = null
DECLARE @CreatedDate datetime = null
DECLARE @IsDeleted bit = null
DECLARE @ViewOption int = null

SET @Id = 4
SET @Code = N'4'
SET @NameRu = N'Продление срока действия заключения о безопасности и качестве без проведения испытаний'
SET @NameKz = @NameRu
SET @CreatedDate = GETDATE()
SET @IsDeleted = 0
SET @ViewOption = 2

IF NOT EXISTS(SELECT * FROM OBK_Ref_Type WHERE [NameRu] = @NameRu)
BEGIN
INSERT INTO [OBK_Ref_Type]
           ([Id]
		   ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[CreatedDate]
           ,[IsDeleted]
		   ,[ViewOption])
     VALUES
           (@Id
		   ,@Code
           ,@NameRu
           ,@NameKz
           ,@CreatedDate
           ,@IsDeleted
		   ,@ViewOption
		   )
END

SET @Id = 5
SET @Code = N'5'
SET @NameRu = N'Оформление копии заключения о безопасности и качестве'
SET @NameKz = @NameRu
SET @CreatedDate = GETDATE()
SET @IsDeleted = 0
SET @ViewOption = 2

IF NOT EXISTS(SELECT * FROM OBK_Ref_Type WHERE [NameRu] = @NameRu)
BEGIN
INSERT INTO [OBK_Ref_Type]
           ([Id]
		   ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[CreatedDate]
           ,[IsDeleted]
		   ,[ViewOption])
     VALUES
           (@Id
		   ,@Code
           ,@NameRu
           ,@NameKz
           ,@CreatedDate
           ,@IsDeleted
		   ,@ViewOption
		   )
END

SET @Id = 6
SET @Code = N'6'
SET @NameRu = N'Оформление дубликата заключения о безопасности и качестве'
SET @NameKz = @NameRu
SET @CreatedDate = GETDATE()
SET @IsDeleted = 0
SET @ViewOption = 2

IF NOT EXISTS(SELECT * FROM OBK_Ref_Type WHERE [NameRu] = @NameRu)
BEGIN
INSERT INTO [OBK_Ref_Type]
           ([Id]
		   ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[CreatedDate]
           ,[IsDeleted]
		   ,[ViewOption])
     VALUES
           (@Id
		   ,@Code
           ,@NameRu
           ,@NameKz
           ,@CreatedDate
           ,@IsDeleted
		   ,@ViewOption
		   )
END

SET IDENTITY_INSERT OBK_Ref_Type OFF