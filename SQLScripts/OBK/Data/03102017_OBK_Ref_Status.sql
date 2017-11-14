DECLARE @Id int
DECLARE @Code nvarchar(50)
DECLARE @NameRu nvarchar(2000)
DECLARE @NameKz nvarchar(2000)
DECLARE @DateCreate datetime
DECLARE @IsDeleted bit
DECLARE @DateEdit datetime

SET @Id = 6
SET @Code = N'6'
SET @NameRu = N'В работе'
SET @NameKz = N'В работе'
SET @DateCreate = GETDATE()
SET @IsDeleted = 0
SET @DateEdit = @DateCreate

IF NOT EXISTS(SELECT * FROM [OBK_Ref_Status] WHERE Id = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_Status]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted]
           ,[DateEdit])
     VALUES
           (@Id
           ,@Code
           ,@NameRu
           ,@NameKz
           ,@DateCreate
           ,@IsDeleted
           ,@DateEdit)
END

SET @Id = 7
SET @Code = N'7'
SET @NameRu = N'На корректировке у заявителя'
SET @NameKz = N'На корректировке у заявителя'
SET @DateCreate = GETDATE()
SET @IsDeleted = 0
SET @DateEdit = @DateCreate

IF NOT EXISTS(SELECT * FROM [OBK_Ref_Status] WHERE Id = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_Status]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted]
           ,[DateEdit])
     VALUES
           (@Id
           ,@Code
           ,@NameRu
           ,@NameKz
           ,@DateCreate
           ,@IsDeleted
           ,@DateEdit)
END

SET @Id = 8
SET @Code = N'8'
SET @NameRu = N'Активный'
SET @NameKz = N'Активный'
SET @DateCreate = GETDATE()
SET @IsDeleted = 0
SET @DateEdit = @DateCreate

IF NOT EXISTS(SELECT * FROM [OBK_Ref_Status] WHERE Id = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_Status]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted]
           ,[DateEdit])
     VALUES
           (@Id
           ,@Code
           ,@NameRu
           ,@NameKz
           ,@DateCreate
           ,@IsDeleted
           ,@DateEdit)
END