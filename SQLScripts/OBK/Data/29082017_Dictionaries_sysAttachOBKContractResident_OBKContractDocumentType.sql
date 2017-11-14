DECLARE @Id uniqueidentifier
DECLARE @Code nvarchar(4000)
DECLARE @Name nvarchar(4000)
DECLARE @Type nvarchar(4000)
DECLARE @ExpireDate nvarchar(4000)
DECLARE @Year nvarchar(4000)
DECLARE @Note nvarchar(4000)
DECLARE @DepartmentsId nvarchar(4000)
DECLARE @DepartmentsValue nvarchar(4000)
DECLARE @ParentId uniqueidentifier
DECLARE @DisplayName nvarchar(4000)
DECLARE @EmployeesValue nvarchar(4000)
DECLARE @EmployeesId nvarchar(4000)
DECLARE @NameKz nvarchar(4000)
DECLARE @IsGuide bit
DECLARE @OrganizationId uniqueidentifier


SET	@Id = '1d6c9cd9-1fc1-4599-83ae-407343788e01'
SET	@Code = '1'
SET	@Name = N'Справка с портала egov.kz о государственной регистрации юридического лица'
SET	@Type = N'sysAttachOBKContractResident'
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
INSERT INTO [Dictionaries]
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
           (@Id 
           ,@Code
           ,@Name
           ,@Type
           ,@ExpireDate
           ,@Year
           ,@Note
           ,@DepartmentsId
           ,@DepartmentsValue
           ,@ParentId
           ,@DisplayName
           ,@EmployeesValue
           ,@EmployeesId
           ,@NameKz
           ,@IsGuide
           ,@OrganizationId
		   )
END		   
		   

SET	@Id = '1d6c9cd9-1fc1-4599-83ae-407343788e02'
SET	@Code = NULL
SET	@Name = N'Справка с портала egov.kz об учетной регистрации Представительства Филиала юридического лица'
SET	@Type = N'sysAttachOBKContractResident'
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
INSERT INTO [Dictionaries]
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
           (@Id 
           ,@Code
           ,@Name
           ,@Type
           ,@ExpireDate
           ,@Year
           ,@Note
           ,@DepartmentsId
           ,@DepartmentsValue
           ,@ParentId
           ,@DisplayName
           ,@EmployeesValue
           ,@EmployeesId
           ,@NameKz
           ,@IsGuide
           ,@OrganizationId
		   )
END


SET	@Id = '1d6c9cd9-1fc1-4599-83ae-407343788e03'
SET	@Code = NULL
SET	@Name = N'Доверенность представителя'
SET	@Type = N'sysAttachOBKContractResident'
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
INSERT INTO [Dictionaries]
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
           (@Id 
           ,@Code
           ,@Name
           ,@Type
           ,@ExpireDate
           ,@Year
           ,@Note
           ,@DepartmentsId
           ,@DepartmentsValue
           ,@ParentId
           ,@DisplayName
           ,@EmployeesValue
           ,@EmployeesId
           ,@NameKz
           ,@IsGuide
           ,@OrganizationId
		   )
END


SET	@Id = '1d6c9cd9-1fc1-4599-83ae-407343788e04'
SET	@Code = '1'
SET	@Name = N'Сведения об организации с органов юстиции, либо Выписка из Единого государственного реестра юридических лиц'
SET	@Type = N'sysAttachOBKContractNonResident'
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
INSERT INTO [Dictionaries]
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
           (@Id 
           ,@Code
           ,@Name
           ,@Type
           ,@ExpireDate
           ,@Year
           ,@Note
           ,@DepartmentsId
           ,@DepartmentsValue
           ,@ParentId
           ,@DisplayName
           ,@EmployeesValue
           ,@EmployeesId
           ,@NameKz
           ,@IsGuide
           ,@OrganizationId
		   )
END	


SET	@Id = '1d6c9cd9-1fc1-4599-83ae-407343788e05'
SET	@Code = NULL
SET	@Name = N'Доверенность представителя'
SET	@Type = N'sysAttachOBKContractNonResident'
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
INSERT INTO [Dictionaries]
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
           (@Id 
           ,@Code
           ,@Name
           ,@Type
           ,@ExpireDate
           ,@Year
           ,@Note
           ,@DepartmentsId
           ,@DepartmentsValue
           ,@ParentId
           ,@DisplayName
           ,@EmployeesValue
           ,@EmployeesId
           ,@NameKz
           ,@IsGuide
           ,@OrganizationId
		   )
END

