USE ncels
GO

IF DB_NAME() <> N'ncels' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[EMP_EAESStatement]
--
PRINT (N'Создать таблицу [dbo].[EMP_EAESStatement]')
GO
CREATE TABLE dbo.EMP_EAESStatement (
  Id uniqueidentifier NOT NULL,
  RegistrationKindValue nvarchar(50) NULL,
  RegistrationCertificateNumber nvarchar(50) NULL,
  NormativeDocumentNumber nvarchar(50) NULL,
  RegistrationDate datetime NULL,
  ExpirationDate datetime NULL,
  LetterNumber nvarchar(50) NULL,
  LetterDate datetime NULL,
  IsMt bit NULL,
  MedicalDeviceNameKz nvarchar(255) NULL,
  MedicalDeviceNameRu nvarchar(255) NULL,
  NomenclatureCode nvarchar(50) NULL,
  NomenclatureNameKz nvarchar(255) NULL,
  NomenclatureNameRu nvarchar(255) NULL,
  NomenclatureDescriptionKz nvarchar(2000) NULL,
  NomenclatureDescriptionRu nvarchar(2000) NULL,
  ApplicationAreaKz nvarchar(2000) NULL,
  ApplicationAreaRu nvarchar(2000) NULL,
  PurposeKz nvarchar(255) NULL,
  PurposeRu nvarchar(255) NULL,
  IsClosedSystem bit NULL,
  RegistrationDossierPageNumber nvarchar(50) NULL,
  ShortTechnicalCharacteristicKz nvarchar(255) NULL,
  ShortTechnicalCharacteristicRu nvarchar(255) NULL,
  ClassOfPotentialRisk nvarchar(50) NULL,
  IsBalk bit NULL,
  IsMeasurementDevice bit NULL,
  IsForInvitroDiagnostics bit NULL,
  IsSterile bit NULL,
  IsMedicalProductPresence bit NULL,
  WithouAe bit NULL,
  TransportConditions nvarchar(255) NULL,
  StorageConditions nvarchar(255) NULL,
  Production nvarchar(255) NULL,
  IsComplectation bit NULL,
  ManufacturerType nvarchar(255) NULL,
  ManufacturerNameRu nvarchar(255) NULL,
  AllowedDocumentNumber nvarchar(255) NULL,
  BossLastName nvarchar(50) NULL,
  BossPosition nvarchar(255) NULL,
  OrganizationForm nvarchar(255) NULL,
  ManufacturerNameKz nvarchar(255) NULL,
  DateOfIssue datetime NULL,
  BossFirstName nvarchar(50) NULL,
  Phone nvarchar(50) NULL,
  Country nvarchar(255) NULL,
  ManufacturerNameEn nvarchar(255) NULL,
  ManufacturerExpirationDate datetime NULL,
  BossMiddleName nvarchar(50) NULL,
  Email nvarchar(255) NULL,
  ContactPersonInitials nvarchar(255) NULL,
  ContactPersonLegalAddress nvarchar(255) NULL,
  ContactPersonPosition nvarchar(255) NULL,
  ContactPersonFactAddress nvarchar(255) NULL,
  Agreement nvarchar(3999) NULL,
  IsAgreed bit NULL,
  ContractId uniqueidentifier NULL,
  RegistrationTypeValue nvarchar(50) NULL,
  NmirkId int NULL,
  RefCountry nvarchar(50) NULL,
  ConCountry nvarchar(50) NULL,
  GarantExpDate int NULL,
  GarantUnit nvarchar(50) NULL,
  GarantNoExp bit NULL,
  CONSTRAINT PK_EMP_EAESStatement PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_EMP_EAESStatement_NmirkId] для объекта типа таблица [dbo].[EMP_EAESStatement]
--
PRINT (N'Создать внешний ключ [FK_EMP_EAESStatement_NmirkId] для объекта типа таблица [dbo].[EMP_EAESStatement]')
GO
ALTER TABLE dbo.EMP_EAESStatement
  ADD CONSTRAINT FK_EMP_EAESStatement_NmirkId FOREIGN KEY (NmirkId) REFERENCES dbo.EXP_DIC_NMIRK (Id)
GO