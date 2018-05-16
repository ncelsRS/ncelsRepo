DECLARE @DictionaryType NVARCHAR(MAX) = N'OBKUnitOfMeasurement'

DECLARE	@Id [uniqueidentifier]
DECLARE	@Code [nvarchar](4000)
DECLARE	@Name [nvarchar](4000)
DECLARE	@Type [nvarchar](4000)
DECLARE	@ExpireDate [nvarchar](4000)
DECLARE	@Year [nvarchar](4000)
DECLARE	@Note [nvarchar](4000)
DECLARE	@DepartmentsId [nvarchar](4000)
DECLARE	@DepartmentsValue [nvarchar](4000)
DECLARE	@ParentId [uniqueidentifier]
DECLARE	@DisplayName [nvarchar](4000)
DECLARE	@EmployeesValue [nvarchar](4000)
DECLARE	@EmployeesId [nvarchar](4000)
DECLARE	@NameKz [nvarchar](4000)
DECLARE	@IsGuide [bit]
DECLARE	@OrganizationId [uniqueidentifier]

SET	@Id = 'd97b760d-d9bb-4152-838e-f27eda3d2180'
SET	@Code = NULL
SET	@Name = N'1 лекарственный препарат / 1 изделие'
SET	@Type = @DictionaryType
SET	@ExpireDate = NULL
SET	@Year = NULL
SET	@Note = NULL
SET	@DepartmentsId = NULL
SET	@DepartmentsValue = NULL
SET	@ParentId = NULL
SET	@DisplayName = NULL
SET	@EmployeesValue = NULL
SET	@EmployeesId = NULL
SET	@NameKz = @Name
SET	@IsGuide = 0
SET	@OrganizationId = NULL

IF NOT EXISTS(SELECT * FROM [Dictionaries] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[Dictionaries]
           ([Id]
           ,[Code]
           ,[Name]
           ,[Type]
           ,[ExpireDate]
           ,[Year]
           ,[Note]
           ,[DepartmentsId]
           ,[DepartmentsValue]
           ,[ParentId]
           ,[DisplayName]
           ,[EmployeesValue]
           ,[EmployeesId]
           ,[NameKz]
           ,[IsGuide]
           ,[OrganizationId])
     VALUES
           (@Id,
           @Code,
           @Name,
           @Type,
           @ExpireDate,
           @Year,
           @Note,
           @DepartmentsId,
           @DepartmentsValue,
           @ParentId,
           @DisplayName,
           @EmployeesValue,
           @EmployeesId,
           @NameKz,
           @IsGuide,
           @OrganizationId)
END

SET	@Id = 'd97b760d-d9bb-4152-838e-f27eda3d2181'
SET	@Code = NULL
SET	@Name = N'1 заключение'
SET	@Type = @DictionaryType
SET	@ExpireDate = NULL
SET	@Year = NULL
SET	@Note = NULL
SET	@DepartmentsId = NULL
SET	@DepartmentsValue = NULL
SET	@ParentId = NULL
SET	@DisplayName = NULL
SET	@EmployeesValue = NULL
SET	@EmployeesId = NULL
SET	@NameKz = @Name
SET	@IsGuide = 0
SET	@OrganizationId = NULL

IF NOT EXISTS(SELECT * FROM [Dictionaries] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[Dictionaries]
           ([Id]
           ,[Code]
           ,[Name]
           ,[Type]
           ,[ExpireDate]
           ,[Year]
           ,[Note]
           ,[DepartmentsId]
           ,[DepartmentsValue]
           ,[ParentId]
           ,[DisplayName]
           ,[EmployeesValue]
           ,[EmployeesId]
           ,[NameKz]
           ,[IsGuide]
           ,[OrganizationId])
     VALUES
           (@Id,
           @Code,
           @Name,
           @Type,
           @ExpireDate,
           @Year,
           @Note,
           @DepartmentsId,
           @DepartmentsValue,
           @ParentId,
           @DisplayName,
           @EmployeesValue,
           @EmployeesId,
           @NameKz,
           @IsGuide,
           @OrganizationId)
END

SET	@Id = 'd97b760d-d9bb-4152-838e-f27eda3d2182'
SET	@Code = NULL
SET	@Name = N'1 копия'
SET	@Type = @DictionaryType
SET	@ExpireDate = NULL
SET	@Year = NULL
SET	@Note = NULL
SET	@DepartmentsId = NULL
SET	@DepartmentsValue = NULL
SET	@ParentId = NULL
SET	@DisplayName = NULL
SET	@EmployeesValue = NULL
SET	@EmployeesId = NULL
SET	@NameKz = @Name
SET	@IsGuide = 0
SET	@OrganizationId = NULL

IF NOT EXISTS(SELECT * FROM [Dictionaries] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[Dictionaries]
           ([Id]
           ,[Code]
           ,[Name]
           ,[Type]
           ,[ExpireDate]
           ,[Year]
           ,[Note]
           ,[DepartmentsId]
           ,[DepartmentsValue]
           ,[ParentId]
           ,[DisplayName]
           ,[EmployeesValue]
           ,[EmployeesId]
           ,[NameKz]
           ,[IsGuide]
           ,[OrganizationId])
     VALUES
           (@Id,
           @Code,
           @Name,
           @Type,
           @ExpireDate,
           @Year,
           @Note,
           @DepartmentsId,
           @DepartmentsValue,
           @ParentId,
           @DisplayName,
           @EmployeesValue,
           @EmployeesId,
           @NameKz,
           @IsGuide,
           @OrganizationId)
END

SET	@Id = 'd97b760d-d9bb-4152-838e-f27eda3d2183'
SET	@Code = NULL
SET	@Name = N'1 дубликат'
SET	@Type = @DictionaryType
SET	@ExpireDate = NULL
SET	@Year = NULL
SET	@Note = NULL
SET	@DepartmentsId = NULL
SET	@DepartmentsValue = NULL
SET	@ParentId = NULL
SET	@DisplayName = NULL
SET	@EmployeesValue = NULL
SET	@EmployeesId = NULL
SET	@NameKz = @Name
SET	@IsGuide = 0
SET	@OrganizationId = NULL

IF NOT EXISTS(SELECT * FROM [Dictionaries] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[Dictionaries]
           ([Id]
           ,[Code]
           ,[Name]
           ,[Type]
           ,[ExpireDate]
           ,[Year]
           ,[Note]
           ,[DepartmentsId]
           ,[DepartmentsValue]
           ,[ParentId]
           ,[DisplayName]
           ,[EmployeesValue]
           ,[EmployeesId]
           ,[NameKz]
           ,[IsGuide]
           ,[OrganizationId])
     VALUES
           (@Id,
           @Code,
           @Name,
           @Type,
           @ExpireDate,
           @Year,
           @Note,
           @DepartmentsId,
           @DepartmentsValue,
           @ParentId,
           @DisplayName,
           @EmployeesValue,
           @EmployeesId,
           @NameKz,
           @IsGuide,
           @OrganizationId)
END

SET	@Id = 'd97b760d-d9bb-4152-838e-f27eda3d2184'
SET	@Code = NULL
SET	@Name = N'1 лекарственный препарат'
SET	@Type = @DictionaryType
SET	@ExpireDate = NULL
SET	@Year = NULL
SET	@Note = NULL
SET	@DepartmentsId = NULL
SET	@DepartmentsValue = NULL
SET	@ParentId = NULL
SET	@DisplayName = NULL
SET	@EmployeesValue = NULL
SET	@EmployeesId = NULL
SET	@NameKz = @Name
SET	@IsGuide = 0
SET	@OrganizationId = NULL

IF NOT EXISTS(SELECT * FROM [Dictionaries] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[Dictionaries]
           ([Id]
           ,[Code]
           ,[Name]
           ,[Type]
           ,[ExpireDate]
           ,[Year]
           ,[Note]
           ,[DepartmentsId]
           ,[DepartmentsValue]
           ,[ParentId]
           ,[DisplayName]
           ,[EmployeesValue]
           ,[EmployeesId]
           ,[NameKz]
           ,[IsGuide]
           ,[OrganizationId])
     VALUES
           (@Id,
           @Code,
           @Name,
           @Type,
           @ExpireDate,
           @Year,
           @Note,
           @DepartmentsId,
           @DepartmentsValue,
           @ParentId,
           @DisplayName,
           @EmployeesValue,
           @EmployeesId,
           @NameKz,
           @IsGuide,
           @OrganizationId)
END

SET	@Id = 'd97b760d-d9bb-4152-838e-f27eda3d2185'
SET	@Code = NULL
SET	@Name = N'1 изделие'
SET	@Type = @DictionaryType
SET	@ExpireDate = NULL
SET	@Year = NULL
SET	@Note = NULL
SET	@DepartmentsId = NULL
SET	@DepartmentsValue = NULL
SET	@ParentId = NULL
SET	@DisplayName = NULL
SET	@EmployeesValue = NULL
SET	@EmployeesId = NULL
SET	@NameKz = @Name
SET	@IsGuide = 0
SET	@OrganizationId = NULL

IF NOT EXISTS(SELECT * FROM [Dictionaries] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[Dictionaries]
           ([Id]
           ,[Code]
           ,[Name]
           ,[Type]
           ,[ExpireDate]
           ,[Year]
           ,[Note]
           ,[DepartmentsId]
           ,[DepartmentsValue]
           ,[ParentId]
           ,[DisplayName]
           ,[EmployeesValue]
           ,[EmployeesId]
           ,[NameKz]
           ,[IsGuide]
           ,[OrganizationId])
     VALUES
           (@Id,
           @Code,
           @Name,
           @Type,
           @ExpireDate,
           @Year,
           @Note,
           @DepartmentsId,
           @DepartmentsValue,
           @ParentId,
           @DisplayName,
           @EmployeesValue,
           @EmployeesId,
           @NameKz,
           @IsGuide,
           @OrganizationId)
END

SET	@Id = 'd97b760d-d9bb-4152-838e-f27eda3d2186'
SET	@Code = NULL
SET	@Name = N'1 экспертиза'
SET	@Type = @DictionaryType
SET	@ExpireDate = NULL
SET	@Year = NULL
SET	@Note = NULL
SET	@DepartmentsId = NULL
SET	@DepartmentsValue = NULL
SET	@ParentId = NULL
SET	@DisplayName = NULL
SET	@EmployeesValue = NULL
SET	@EmployeesId = NULL
SET	@NameKz = @Name
SET	@IsGuide = 0
SET	@OrganizationId = NULL

IF NOT EXISTS(SELECT * FROM [Dictionaries] WHERE [Id] = @Id)
BEGIN
INSERT INTO [dbo].[Dictionaries]
           ([Id]
           ,[Code]
           ,[Name]
           ,[Type]
           ,[ExpireDate]
           ,[Year]
           ,[Note]
           ,[DepartmentsId]
           ,[DepartmentsValue]
           ,[ParentId]
           ,[DisplayName]
           ,[EmployeesValue]
           ,[EmployeesId]
           ,[NameKz]
           ,[IsGuide]
           ,[OrganizationId])
     VALUES
           (@Id,
           @Code,
           @Name,
           @Type,
           @ExpireDate,
           @Year,
           @Note,
           @DepartmentsId,
           @DepartmentsValue,
           @ParentId,
           @DisplayName,
           @EmployeesValue,
           @EmployeesId,
           @NameKz,
           @IsGuide,
           @OrganizationId)
END
