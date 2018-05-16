DECLARE @Id uniqueidentifier
DECLARE @Code nvarchar(50)
DECLARE @NameRu nvarchar(2000)
DECLARE @NameKz nvarchar(2000)
DECLARE @DateCreate datetime
DECLARE @IsDeleted bit
DECLARE @DateEdit datetime

SET @Id = 'a0683cfb-6a0f-462c-ad16-284d4ee1cd65'
SET @Code = 'approved'
SET @NameRu = N'Согласован'
SET @NameKz = @NameKz
SET @DateCreate = GETDATE()
SET @IsDeleted = 0
SET @DateEdit = NULL

IF NOT EXISTS(SELECT * FROM OBK_Ref_ContractHistoryStatus WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_ContractHistoryStatus]
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

SET @Id = 'a0683cfb-6a0f-462c-ad16-284d4ee1cd66'
SET @Code = 'signed'
SET @NameRu = N'Подписан'
SET @NameKz = @NameKz
SET @DateCreate = GETDATE()
SET @IsDeleted = 0
SET @DateEdit = NULL

IF NOT EXISTS(SELECT * FROM OBK_Ref_ContractHistoryStatus WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_ContractHistoryStatus]
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

SET @Id = 'a0683cfb-6a0f-462c-ad16-284d4ee1cd67'
SET @Code = 'registered'
SET @NameRu = N'Зарегистрирован'
SET @NameKz = @NameKz
SET @DateCreate = GETDATE()
SET @IsDeleted = 0
SET @DateEdit = NULL

IF NOT EXISTS(SELECT * FROM OBK_Ref_ContractHistoryStatus WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_ContractHistoryStatus]
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

SET @Id = 'a0683cfb-6a0f-462c-ad16-284d4ee1cd68'
SET @Code = 'attached'
SET @NameRu = N'Прикреплено вложение'
SET @NameKz = @NameKz
SET @DateCreate = GETDATE()
SET @IsDeleted = 0
SET @DateEdit = NULL

IF NOT EXISTS(SELECT * FROM OBK_Ref_ContractHistoryStatus WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_ContractHistoryStatus]
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