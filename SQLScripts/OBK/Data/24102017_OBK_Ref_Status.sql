DECLARE @Id int
DECLARE @Code nvarchar(50)
DECLARE @NameRu nvarchar(2000)
DECLARE @NameKz nvarchar(2000)
DECLARE @DateCreate datetime
DECLARE @IsDeleted bit
DECLARE @DateEdit datetime
	
SET @Id = 13
SET @Code = N'13'
SET @NameRu = N'На формировании счета на оплату'
SET @NameKz = @NameRu
SET @DateCreate = CURRENT_TIMESTAMP 
SET @IsDeleted = 0
SET @DateEdit = NULL
	
IF NOT EXISTS(SELECT * FROM OBK_Ref_Status WHERE [Id] = @Id)
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