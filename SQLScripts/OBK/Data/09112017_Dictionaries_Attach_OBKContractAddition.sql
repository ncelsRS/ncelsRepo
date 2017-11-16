DECLARE @Id uniqueidentifier
DECLARE @Code nvarchar(4000)
DECLARE @Name nvarchar(4000)
DECLARE @Type nvarchar(4000)
DECLARE @NameKz nvarchar(4000)

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0203'
SET	@Code = N'0'
SET	@Name = N'Дополнительное соглашение'
SET	@Type = N'sysAttachOBKContractAdditionBankInfoChange'
SET	@NameKz = NULL

UPDATE [Dictionaries]
SET	[Code] = @Code,
	[Name] = @Name,
	[Type] = @Type,
	[NameKz] = @NameKz
WHERE [Id] = @Id

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0204'
SET	@Code = N'1'
SET	@Name = N'Скан действующего договора'
SET	@Type = N'sysAttachOBKContractAdditionBankInfoChange'
SET	@NameKz = NULL

UPDATE [Dictionaries]
SET	[Code] = @Code,
	[Name] = @Name,
	[Type] = @Type,
	[NameKz] = @NameKz
WHERE [Id] = @Id

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0205'
SET	@Code = N'0'
SET	@Name = N'Дополнительное соглашение'
SET	@Type = N'sysAttachOBKContractAdditionAddressChangeResident'
SET	@NameKz = NULL

UPDATE [Dictionaries]
SET	[Code] = @Code,
	[Name] = @Name,
	[Type] = @Type,
	[NameKz] = @NameKz
WHERE [Id] = @Id

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0206'
SET	@Code = N'1'
SET	@Name = N'Справка с портала egov.kz о гос. регистрации'
SET	@Type = N'sysAttachOBKContractAdditionAddressChangeResident'
SET	@NameKz = NULL

UPDATE [Dictionaries]
SET	[Code] = @Code,
	[Name] = @Name,
	[Type] = @Type,
	[NameKz] = @NameKz
WHERE [Id] = @Id

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0207'
SET	@Code = N'1'
SET	@Name = N'Скан действующего договора'
SET	@Type = N'sysAttachOBKContractAdditionAddressChangeResident'
SET	@NameKz = NULL

UPDATE [Dictionaries]
SET	[Code] = @Code,
	[Name] = @Name,
	[Type] = @Type,
	[NameKz] = @NameKz
WHERE [Id] = @Id

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0208'
SET	@Code = N'0'
SET	@Name = N'Дополнительное соглашение'
SET	@Type = N'sysAttachOBKContractAdditionAddressChangeNonResident'
SET	@NameKz = NULL

UPDATE [Dictionaries]
SET	[Code] = @Code,
	[Name] = @Name,
	[Type] = @Type,
	[NameKz] = @NameKz
WHERE [Id] = @Id

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0209'
SET	@Code = N'1'
SET	@Name = N'Сведения об организации с органов юстиции, либо Выписка из Единого государственного реестра юридических лиц'
SET	@Type = N'sysAttachOBKContractAdditionAddressChangeNonResident'
SET	@NameKz = NULL

UPDATE [Dictionaries]
SET	[Code] = @Code,
	[Name] = @Name,
	[Type] = @Type,
	[NameKz] = @NameKz
WHERE [Id] = @Id

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0210'
SET	@Code = N'1'
SET	@Name = N'Скан действующего договора'
SET	@Type = N'sysAttachOBKContractAdditionAddressChangeNonResident'
SET	@NameKz = NULL

UPDATE [Dictionaries]
SET	[Code] = @Code,
	[Name] = @Name,
	[Type] = @Type,
	[NameKz] = @NameKz
WHERE [Id] = @Id

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0211'
SET	@Code = N'0'
SET	@Name = N'Дополнительное соглашение'
SET	@Type = N'sysAttachOBKContractAdditionManagerChange'
SET	@NameKz = NULL

UPDATE [Dictionaries]
SET	[Code] = @Code,
	[Name] = @Name,
	[Type] = @Type,
	[NameKz] = @NameKz
WHERE [Id] = @Id

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0212'
SET	@Code = N'1'
SET	@Name = N'Приказ о назначении руководителя'
SET	@Type = N'sysAttachOBKContractAdditionManagerChange'
SET	@NameKz = NULL

UPDATE [Dictionaries]
SET	[Code] = @Code,
	[Name] = @Name,
	[Type] = @Type,
	[NameKz] = @NameKz
WHERE [Id] = @Id

SET	@Id = '63330faf-633b-4a77-b90c-c7f4641b0213'
SET	@Code = N'1'
SET	@Name = N'Скан действуюшего договора'
SET	@Type = N'sysAttachOBKContractAdditionManagerChange'
SET	@NameKz = NULL

UPDATE [Dictionaries]
SET	[Code] = @Code,
	[Name] = @Name,
	[Type] = @Type,
	[NameKz] = @NameKz
WHERE [Id] = @Id
