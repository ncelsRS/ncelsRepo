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

SET	@Id = '1d6c9cd9-1fc1-4599-83ae-407343788e06'
SET	@Code = N'0'
SET	@Name = N'Договор'
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

SET	@Id = '1d6c9cd9-1fc1-4599-83ae-407343788e07'
SET	@Code = N'0'
SET	@Name = N'Договор'
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