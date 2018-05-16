DECLARE @Id uniqueidentifier
DECLARE @Code nvarchar(50)
DECLARE @NameRu nvarchar(2000)
DECLARE @NameKz nvarchar(2000)
DECLARE @DescriptionRu nvarchar(2000)
DECLARE @DescriptionKz nvarchar(2000)
DECLARE @DateCreate datetime
DECLARE @IsDeleted bit
DECLARE @DateEdit datetime

SET @Id = 'fd78db09-b394-4bce-874d-8e90a89ef816'
SET @Code = N'workContractAddition'
SET @NameRu = N'В работе'
SET @NameKz = NULL
SET @DescriptionRu = N'Доп. соглашение получено'
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

SET @Id = 'fd78db09-b394-4bce-874d-8e90a89ef817'
SET @Code = N'activeContractAddition'
SET @NameRu = N'Активный'
SET @NameKz = NULL
SET @DescriptionRu = N'Доп. соглашение зарегистрировано'
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