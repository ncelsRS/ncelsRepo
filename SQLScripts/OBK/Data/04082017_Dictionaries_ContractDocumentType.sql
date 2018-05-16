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

SET	@Id = '9e48c346-4829-44f4-9897-358a47295771'
SET	@Code = NULL
SET	@Name = N'Доверенности'
SET	@Type = N'OBKContractDocumentType'
SET	@ExpireDate = NULL
SET	@Year = NULL
SET	@Note = NULL
SET	@DepartmentsId = NULL
SET	@DepartmentsValue = NULL
SET	@ParentId = NULL
SET	@DisplayName = NULL
SET	@EmployeesValue = NULL
SET	@EmployeesId = NULL
SET	@NameKz = NULL
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


SET	@Id = '9e48c346-4829-44f4-9897-358a47295772'
SET	@Code = NULL
SET	@Name = N'Устава'
SET	@Type = N'OBKContractDocumentType'
SET	@ExpireDate = NULL
SET	@Year = NULL
SET	@Note = NULL
SET	@DepartmentsId = NULL
SET	@DepartmentsValue = NULL
SET	@ParentId = NULL
SET	@DisplayName = NULL
SET	@EmployeesValue = NULL
SET	@EmployeesId = NULL
SET	@NameKz = NULL
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


SET	@Id = '9e48c346-4829-44f4-9897-358a47295773'
SET	@Code = NULL
SET	@Name = N'Приказа'
SET	@Type = N'OBKContractDocumentType'
SET	@ExpireDate = NULL
SET	@Year = NULL
SET	@Note = NULL
SET	@DepartmentsId = NULL
SET	@DepartmentsValue = NULL
SET	@ParentId = NULL
SET	@DisplayName = NULL
SET	@EmployeesValue = NULL
SET	@EmployeesId = NULL
SET	@NameKz = NULL
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
