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

SET	@Id = '680ad6f5-cef6-4613-8c3f-cf0ab6b66f73'
SET	@Code = N'1'
SET	@Name = N'Соглашение об изменении юридического адреса'
SET	@Type = N'OBKContractAddition'
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

SET	@Id = '680ad6f5-cef6-4613-8c3f-cf0ab6b66f74'
SET	@Code = N'2'
SET	@Name = N'Соглашение о смене руководителя'
SET	@Type = N'OBKContractAddition'
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

SET	@Id = '680ad6f5-cef6-4613-8c3f-cf0ab6b66f75'
SET	@Code = N'3'
SET	@Name = N'Соглашение об изменении банковских реквизитов'
SET	@Type = N'OBKContractAddition'
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