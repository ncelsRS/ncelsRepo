DECLARE @Id uniqueidentifier
DECLARE @Code nvarchar(50)
DECLARE @NameRu nvarchar(2000)
DECLARE @NameKz nvarchar(2000)
DECLARE @DescriptionRu nvarchar(2000)
DECLARE @DescriptionKz nvarchar(2000)
DECLARE @DateCreate datetime
DECLARE @IsDeleted bit
DECLARE @DateEdit datetime

SET @Id = 'fd78db09-b394-4bce-874d-8e90a89ef811'
SET @Code = N'draft'
SET @NameRu = N'Черновик'
SET @NameKz = NULL
SET @DescriptionRu = N'Черновик'
SET @DescriptionKz = NULL
SET @DateCreate = GETDATE()
SET @IsDeleted = 0
SET @DateEdit = NULL

IF NOT EXISTS(SELECT * FROM OBK_Ref_ContractExtHistoryStatus WHERE [Id] = @Id)
BEGIN
INSERT INTO [OBK_Ref_ContractExtHistoryStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DescriptionRu]
           ,[DescriptionKz]
           ,[DateCreate]
           ,[IsDeleted]
           ,[DateEdit])
     VALUES
           (@Id
           ,@Code
           ,@NameRu
           ,@NameKz
           ,@DescriptionRu
           ,@DescriptionKz
           ,@DateCreate
           ,@IsDeleted
           ,@DateEdit)
END

SET @Id = 'fd78db09-b394-4bce-874d-8e90a89ef812'
SET @Code = N'inprocessing'
SET @NameRu = N'В обработке'
SET @NameKz = NULL
SET @DescriptionRu = N'Отправлен в ЦОЗ'
SET @DescriptionKz = NULL
SET @DateCreate = GETDATE()
SET @IsDeleted = 0
SET @DateEdit = NULL

IF NOT EXISTS(SELECT * FROM OBK_Ref_ContractExtHistoryStatus WHERE [Id] = @Id)
BEGIN
INSERT INTO [OBK_Ref_ContractExtHistoryStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DescriptionRu]
           ,[DescriptionKz]
           ,[DateCreate]
           ,[IsDeleted]
           ,[DateEdit])
     VALUES
           (@Id
           ,@Code
           ,@NameRu
           ,@NameKz
           ,@DescriptionRu
           ,@DescriptionKz
           ,@DateCreate
           ,@IsDeleted
           ,@DateEdit)
END

SET @Id = 'fd78db09-b394-4bce-874d-8e90a89ef813'
SET @Code = N'work'
SET @NameRu = N'В работе'
SET @NameKz = NULL
SET @DescriptionRu = N'Договор получен'
SET @DescriptionKz = NULL
SET @DateCreate = GETDATE()
SET @IsDeleted = 0
SET @DateEdit = NULL

IF NOT EXISTS(SELECT * FROM OBK_Ref_ContractExtHistoryStatus WHERE [Id] = @Id)
BEGIN
INSERT INTO [OBK_Ref_ContractExtHistoryStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DescriptionRu]
           ,[DescriptionKz]
           ,[DateCreate]
           ,[IsDeleted]
           ,[DateEdit])
     VALUES
           (@Id
           ,@Code
           ,@NameRu
           ,@NameKz
           ,@DescriptionRu
           ,@DescriptionKz
           ,@DateCreate
           ,@IsDeleted
           ,@DateEdit)
END

SET @Id = 'fd78db09-b394-4bce-874d-8e90a89ef814'
SET @Code = N'oncorrection'
SET @NameRu = N'На корректировке у заявителя'
SET @NameKz = NULL
SET @DescriptionRu = N'Возвращен на доработку'
SET @DescriptionKz = NULL
SET @DateCreate = GETDATE()
SET @IsDeleted = 0
SET @DateEdit = NULL

IF NOT EXISTS(SELECT * FROM OBK_Ref_ContractExtHistoryStatus WHERE [Id] = @Id)
BEGIN
INSERT INTO [OBK_Ref_ContractExtHistoryStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DescriptionRu]
           ,[DescriptionKz]
           ,[DateCreate]
           ,[IsDeleted]
           ,[DateEdit])
     VALUES
           (@Id
           ,@Code
           ,@NameRu
           ,@NameKz
           ,@DescriptionRu
           ,@DescriptionKz
           ,@DateCreate
           ,@IsDeleted
           ,@DateEdit)
END

SET @Id = 'fd78db09-b394-4bce-874d-8e90a89ef815'
SET @Code = N'active'
SET @NameRu = N'Активный'
SET @NameKz = NULL
SET @DescriptionRu = N'Договор зарегистрирован'
SET @DescriptionKz = NULL
SET @DateCreate = GETDATE()
SET @IsDeleted = 0
SET @DateEdit = NULL

IF NOT EXISTS(SELECT * FROM OBK_Ref_ContractExtHistoryStatus WHERE [Id] = @Id)
BEGIN
INSERT INTO [OBK_Ref_ContractExtHistoryStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DescriptionRu]
           ,[DescriptionKz]
           ,[DateCreate]
           ,[IsDeleted]
           ,[DateEdit])
     VALUES
           (@Id
           ,@Code
           ,@NameRu
           ,@NameKz
           ,@DescriptionRu
           ,@DescriptionKz
           ,@DateCreate
           ,@IsDeleted
           ,@DateEdit)
END