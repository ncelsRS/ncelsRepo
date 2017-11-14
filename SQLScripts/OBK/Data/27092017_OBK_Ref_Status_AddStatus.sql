DECLARE @Id int
DECLARE @Code nvarchar(50)
DECLARE @NameRu nvarchar(2000)
DECLARE @NameKz nvarchar(2000)
DECLARE @DateCreate datetime
DECLARE @IsDeleted bit
DECLARE @DateEdit datetime

SET @Id = 5
SET @Code = N'5'
SET @NameRu = N'В обработке'
SET @NameKz = N'В обработке'
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