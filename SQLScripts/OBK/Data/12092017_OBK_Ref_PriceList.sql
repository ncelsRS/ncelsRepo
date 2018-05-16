DELETE FROM OBK_Ref_PriceList
WHERE NameRu = N'Продлении срока действия заключения о безопасности и качестве без проведения испытаний'
OR
NameRu = N'Оформлении дубликата заключения о безопасности и качестве'
OR
NameRu = N'Оформлении копии заключения о безопасности и качестве'
GO

DECLARE @Id UNIQUEIDENTIFIER
DECLARE @TypeId INT
DECLARE @NameRu NVARCHAR(255)
DECLARE @NameKz NVARCHAR(255)
DECLARE @UnitId UNIQUEIDENTIFIER
DECLARE @Price FLOAT
DECLARE @ServiceTypeId UNIQUEIDENTIFIER
DECLARE @IsDeleted BIT
DECLARE @DegreeRiskId UNIQUEIDENTIFIER

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4737'
SET @TypeId = 4
SET @NameRu = N'Продлении срока действия заключения о безопасности и качестве без проведения испытаний'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2181'
SET @Price = 3969
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c80'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [OBK_Ref_PriceList]
           ([Id]
           ,[TypeId]
           ,[NameRu]
		   ,[NameKz]
		   ,[UnitId]
		   ,[Price]
		   ,[ServiceTypeId]
		   ,[IsDeleted]
		   ,[DegreeRiskId]
		   )
     VALUES
           (@Id,
           @TypeId,
           @NameRu,
		   @NameKz,
		   @UnitId,
		   @Price,
		   @ServiceTypeId,
		   @IsDeleted,
		   @DegreeRiskId
		   )
END

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4738'
SET @TypeId = 5
SET @NameRu = N'Оформлении копии заключения о безопасности и качестве'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2182'
SET @Price = 219
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c80'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [OBK_Ref_PriceList]
           ([Id]
           ,[TypeId]
           ,[NameRu]
		   ,[NameKz]
		   ,[UnitId]
		   ,[Price]
		   ,[ServiceTypeId]
		   ,[IsDeleted]
		   ,[DegreeRiskId]
		   )
     VALUES
           (@Id,
           @TypeId,
           @NameRu,
		   @NameKz,
		   @UnitId,
		   @Price,
		   @ServiceTypeId,
		   @IsDeleted,
		   @DegreeRiskId
		   )
END

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4739'
SET @TypeId = 6
SET @NameRu = N'Оформлении дубликата заключения о безопасности и качестве'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2183'
SET @Price = 1438
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c80'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [OBK_Ref_PriceList]
           ([Id]
           ,[TypeId]
           ,[NameRu]
		   ,[NameKz]
		   ,[UnitId]
		   ,[Price]
		   ,[ServiceTypeId]
		   ,[IsDeleted]
		   ,[DegreeRiskId]
		   )
     VALUES
           (@Id,
           @TypeId,
           @NameRu,
		   @NameKz,
		   @UnitId,
		   @Price,
		   @ServiceTypeId,
		   @IsDeleted,
		   @DegreeRiskId
		   )
END

