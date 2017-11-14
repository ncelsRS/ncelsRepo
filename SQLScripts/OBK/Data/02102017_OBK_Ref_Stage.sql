DECLARE @Id int
DECLARE @Code nvarchar(50)
DECLARE @NameRu  nvarchar(2000)
DECLARE @NameKz nvarchar(2000)
DECLARE @DateCreate datetime
DECLARE @IsDeleted bit
DECLARE @DateEdit datetime

SET @Id = 4
SET @Code = N'4'
SET @NameRu = N'ДЭФ'
SET @NameKz = N'ДЭФ'
SET @DateCreate = GETDATE()
SET @IsDeleted = 0
SET @DateEdit = NULL

IF (NOT EXISTS(SELECT * FROM [OBK_Ref_Stage] WHERE [Id] = @Id))
BEGIN
INSERT INTO [dbo].[OBK_Ref_Stage]
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

GO