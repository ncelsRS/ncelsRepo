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

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0203'
SET	@Code = NULL
SET	@Name = N'Дополнительное соглашение'
SET	@Type = N'sysAttachOBKContractAdditionBankInfoChange'
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

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0204'
SET	@Code = NULL
SET	@Name = N'Скан действующего договора'
SET	@Type = N'sysAttachOBKContractAdditionBankInfoChange'
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

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0205'
SET	@Code = NULL
SET	@Name = N'Дополнительное соглашение'
SET	@Type = N'sysAttachOBKContractAdditionAddressChangeResident'
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

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0206'
SET	@Code = NULL
SET	@Name = N'Справка с портала egov.kz о гос. регистрации'
SET	@Type = N'sysAttachOBKContractAdditionAddressChangeResident'
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

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0207'
SET	@Code = NULL
SET	@Name = N'Скан действующего договора'
SET	@Type = N'sysAttachOBKContractAdditionAddressChangeResident'
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

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0208'
SET	@Code = NULL
SET	@Name = N'Дополнительное соглашение'
SET	@Type = N'sysAttachOBKContractAdditionAddressChangeNonResident'
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

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0209'
SET	@Code = NULL
SET	@Name = N'Сведения об организации с органов юстиции, либо Выписка из Единого государственного реестра юридических лиц'
SET	@Type = N'sysAttachOBKContractAdditionAddressChangeNonResident'
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

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0210'
SET	@Code = NULL
SET	@Name = N'Скан действующего договора'
SET	@Type = N'sysAttachOBKContractAdditionAddressChangeNonResident'
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

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0211'
SET	@Code = NULL
SET	@Name = N'Дополнительное соглашение'
SET	@Type = N'sysAttachOBKContractAdditionManagerChange'
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

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0212'
SET	@Code = NULL
SET	@Name = N'Приказ о назначении руководителя'
SET	@Type = N'sysAttachOBKContractAdditionManagerChange'
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

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0213'
SET	@Code = NULL
SET	@Name = N'Скан действуюшего договора'
SET	@Type = N'sysAttachOBKContractAdditionManagerChange'
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

