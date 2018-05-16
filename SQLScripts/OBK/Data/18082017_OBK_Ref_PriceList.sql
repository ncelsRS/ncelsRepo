DECLARE @Id UNIQUEIDENTIFIER
DECLARE @TypeId INT
DECLARE @NameRu NVARCHAR(255)
DECLARE @NameKz NVARCHAR(255)
DECLARE @UnitId UNIQUEIDENTIFIER
DECLARE @Price FLOAT
DECLARE @ServiceTypeId UNIQUEIDENTIFIER
DECLARE @IsDeleted BIT
DECLARE @DegreeRiskId UNIQUEIDENTIFIER



SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4701'
SET @TypeId = 3
SET @NameRu = N'Оценка безопасности и качества лекарственных средств и изделий медицинского назначения путем декларирования'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET @Price = 10983
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4702'
SET @TypeId = 3
SET @NameRu = N'Оценка безопасности и качества лекарственных средств и изделий медицинского назначения путем декларирования'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2185'
SET @Price = 10983
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c82'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4703'
SET @TypeId = 3
SET @NameRu = N'Продлении срока действия заключения о безопасности и качестве без проведения испытаний'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2181'
SET @Price = 3969
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c80'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4704'
SET @TypeId = 3
SET @NameRu = N'Оформлении копии заключения о безопасности и качестве'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2182'
SET @Price = 219
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c80'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4705'
SET @TypeId = 3
SET @NameRu = N'Оформлении дубликата заключения о безопасности и качестве'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2183'
SET @Price = 1438
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c80'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4706'
SET @TypeId = 2
SET @NameRu = N'Продлении срока действия заключения о безопасности и качестве без проведения испытаний'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2181'
SET @Price = 3969
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c80'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4707'
SET @TypeId = 2
SET @NameRu = N'Оформлении копии заключения о безопасности и качестве'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2182'
SET @Price = 219
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c80'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4708'
SET @TypeId = 2
SET @NameRu = N'Оформлении дубликата заключения о безопасности и качестве'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2183'
SET @Price = 1438
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c80'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4709'
SET @TypeId = 1
SET @NameRu = N'Продлении срока действия заключения о безопасности и качестве без проведения испытаний'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2181'
SET @Price = '3969'
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c80'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4710'
SET @TypeId = 1
SET @NameRu = N'Оформлении копии заключения о безопасности и качестве'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2182'
SET @Price = 219
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c80'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4711'
SET @TypeId = 1
SET @NameRu = N'Оформлении дубликата заключения о безопасности и качестве'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2183'
SET @Price = 1438
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c80'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4712'
SET @TypeId = 2
SET @NameRu = N'Оценка безопасности и качества лекарственных средств партии (серии) - многокомпонентный лекарственный препарат'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET @Price = 71545
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4713'
SET @TypeId = 2
SET @NameRu = N'Оценка безопасности и качества лекарственных средств партии (серии) - однокомпонентный лекарственный препарат'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET @Price = 59555
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4714'
SET @TypeId = 2
SET @NameRu = N'Оценка безопасности и качества лекарственных средств партии (серии) - многокомпонентный биологический  лекарственный препарат'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET @Price = 97152
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4715'
SET @TypeId = 2
SET @NameRu = N'Оценка безопасности и качества лекарственных средств партии (серии) - однокомпонентный биологический  лекарственный препарат'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET @Price = 69899
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4716'
SET @TypeId = 2
SET @NameRu = N'Оценка безопасности и качества лекарственных средств партии (серии) - гомеопатический лекарственный препарат'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET @Price = 29868
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4717'
SET @TypeId = 2
SET @NameRu = N'Оценка безопасности и качества лекарственных средств партии (серии) - растительный  лекарственный препарат'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET @Price = 33191
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4718'
SET @TypeId = 2
SET @NameRu = N'Оценка безопасности и качества лекарственных средств партии (серии) - балк-продукт'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET @Price = 62276
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4719'
SET @TypeId = 2
SET @NameRu = N'Оценка безопасности и качества лекарственных средств партии (серии) - лекарственное растительное сырье'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET @Price = '32244'
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4720'
SET @TypeId = 2
SET @NameRu = N'Оценка безопасности и качества изделий медицинского назначения партии (серии) - Класс 1'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2185'
SET @Price = '55335'
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c82'
SET @IsDeleted = 0
SET @DegreeRiskId = 'c02f40cc-757c-42ba-a400-3f7937cf8600'

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4721'
SET @TypeId = 2
SET @NameRu = N'Оценка безопасности и качества изделий медицинского назначения партии (серии) - Класс 2А'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2185'
SET @Price = 68175
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c82'
SET @IsDeleted = 0
SET @DegreeRiskId = 'c02f40cc-757c-42ba-a400-3f7937cf8601'

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4722'
SET @TypeId = 2
SET @NameRu = N'Оценка безопасности и качества изделий медицинского назначения партии (серии) - Класс 2Б'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2185'
SET @Price = 84122
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c82'
SET @IsDeleted = 0
SET @DegreeRiskId = 'c02f40cc-757c-42ba-a400-3f7937cf8602'

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4723'
SET @TypeId = 2
SET @NameRu = N'Оценка безопасности и качества изделий медицинского назначения партии (серии) - Класс 3'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2185'
SET @Price = 115967
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c82'
SET @IsDeleted = 0
SET @DegreeRiskId = 'c02f40cc-757c-42ba-a400-3f7937cf8603'

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4724'
SET @TypeId = 1
SET @NameRu = N'Серийной оценке безопасности и качества лекарственных средств и изделий медицинского назначения   - 1 производственный цех'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2186'
SET @Price = 480546
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c80'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4725'
SET @TypeId = 1
SET @NameRu = N'Серийной оценке безопасности и качества лекарственных средств - многокомпонентный лекарственный препарат'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET @Price = 59550
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4726'
SET @TypeId = 1
SET @NameRu = N'Серийной оценке безопасности и качества лекарственных средств - однокомпонентный лекарственный препарат'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET @Price = 47560
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4727'
SET @TypeId = 1
SET @NameRu = N'Серийной оценке безопасности и качества лекарственных средств - многокомпонентный биологический  лекарственный препарат'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET @Price = 85156
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4728'
SET @TypeId = 1
SET @NameRu = N'Серийной оценке безопасности и качества лекарственных средств - однокомпонентный биологический  лекарственный препарат'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET @Price = '57902'
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4729'
SET @TypeId = 1
SET @NameRu = N'Серийной оценке безопасности и качества лекарственных средств - гомеопатический лекарственный препарат'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET @Price = 17873
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4730'
SET @TypeId = 1
SET @NameRu = N'Серийной оценке безопасности и качества лекарственных средств- растительный  лекарственный препарат'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET @Price = 21196
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4731'
SET @TypeId = 1
SET @NameRu = N'Серийной оценке безопасности и качества лекарственных средств - балк-продукт'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET @Price = 50281
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4732'
SET @TypeId = 1
SET @NameRu = N'Серийной оценке безопасности и качества лекарственных средств - лекарственное растительное сырье'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET @Price = 20249
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c81'
SET @IsDeleted = 0
SET @DegreeRiskId = NULL

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4733'
SET @TypeId = 1
SET @NameRu = N'Серийной оценке безопасности и качества изделий медицинского назначения - Класс 1'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2185'
SET @Price = 43340
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c82'
SET @IsDeleted = 0
SET @DegreeRiskId = 'c02f40cc-757c-42ba-a400-3f7937cf8600'

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4734'
SET @TypeId = 1
SET @NameRu = N'Серийной оценке безопасности и качества изделий медицинского назначения - Класс 2А'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2185'
SET @Price = 56180
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c82'
SET @IsDeleted = 0
SET @DegreeRiskId = 'c02f40cc-757c-42ba-a400-3f7937cf8601'

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4735'
SET @TypeId = 1
SET @NameRu = N'Серийной оценке безопасности и качества изделий медицинского назначения - Класс 2Б'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2185'
SET @Price = '72127'
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c82'
SET @IsDeleted = 0
SET @DegreeRiskId = 'c02f40cc-757c-42ba-a400-3f7937cf8602'

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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

SET @Id = 'dfd0f766-fb67-4c2c-8607-21fb41cb4736'
SET @TypeId = 1
SET @NameRu = N'Серийной оценке безопасности и качества изделий медицинского назначения - Класс 3'
SET @NameKz = @NameRu
SET @UnitId = 'd97b760d-d9bb-4152-838e-f27eda3d2185'
SET @Price = 103972
SET @ServiceTypeId = '9106d5e8-35dc-4178-8882-b30166de4c82'
SET @IsDeleted = 0
SET @DegreeRiskId = 'c02f40cc-757c-42ba-a400-3f7937cf8603'

IF NOT EXISTS(SELECT * FROM [OBK_Ref_PriceList] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[OBK_Ref_PriceList]
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
