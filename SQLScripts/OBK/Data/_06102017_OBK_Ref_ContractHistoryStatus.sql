DECLARE @Id uniqueidentifier
DECLARE @Code nvarchar(50)
DECLARE @NameRu nvarchar(2000)
DECLARE @NameKz nvarchar(2000)
DECLARE @DateCreate datetime
DECLARE @IsDeleted bit
DECLARE @DateEdit datetime

SET @Id = 'a0683cfb-6a0f-462c-ad16-284d4ee1cd58'
SET @Code = 'sentByApplicant'
SET @NameRu = N'Отправлен заявителем'
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

SET @Id = 'a0683cfb-6a0f-462c-ad16-284d4ee1cd59'
SET @Code = 'sentToWork'
SET @NameRu = N'Распределен'
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

SET @Id = 'a0683cfb-6a0f-462c-ad16-284d4ee1cd60'
SET @Code = 'meetsRequirements'
SET @NameRu = N'Соответствует требованиям'
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

SET @Id = 'a0683cfb-6a0f-462c-ad16-284d4ee1cd61'
SET @Code = 'doesNotMeetRequirements'
SET @NameRu = N'Не соответствует требованиям'
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

SET @Id = 'a0683cfb-6a0f-462c-ad16-284d4ee1cd62'
SET @Code = 'returned'
SET @NameRu = N'Возвращен на доработку'
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

SET @Id = 'a0683cfb-6a0f-462c-ad16-284d4ee1cd63'
SET @Code = 'sentToApproval'
SET @NameRu = N'Отправлен на согласование'
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

SET @Id = 'a0683cfb-6a0f-462c-ad16-284d4ee1cd64'
SET @Code = 'refused'
SET @NameRu = N'Отказано в согласовании'
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

