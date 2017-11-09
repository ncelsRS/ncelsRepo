DECLARE @Id int
DECLARE @Code nvarchar(50)
DECLARE @NameRu nvarchar(2000)
DECLARE @NameKz nvarchar(2000)
DECLARE @DateCreate datetime
DECLARE @IsDeleted bit

SET @Id = 5
SET @Code = N'onCorrection'
SET @NameRu = N'На корректировке у заявителя'
SET @NameKz = N'На корректировке у заявителя'
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

SET @Id = 6
SET @Code = N'onAgreement'
SET @NameRu = N'На согласовании у руководителя'
SET @NameKz = N'На согласовании у руководителя'
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
