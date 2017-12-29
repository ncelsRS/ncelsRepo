USE [ncels]
GO

/****** Object:  Table [dbo].[EMP_Statement]    Script Date: 27.12.2017 15:19:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_Statement](
	[Id] [uniqueidentifier] NOT NULL,
	[RegistrationKindValue] [nvarchar](50) NULL,
	[RegistrationCertificateNumber] [nvarchar](50) NULL,
	[NormativeDocumentNumber] [nvarchar](50) NULL,
	[RegistrationDate] [datetime] NULL,
	[ExpirationDate] [datetime] NULL,
	[LetterNumber] [nvarchar](50) NULL,
	[LetterDate] [datetime] NULL,
	[IsMt] [bit] NULL,
	[MedicalDeviceNameKz] [nvarchar](255) NULL,
	[MedicalDeviceNameRu] [nvarchar](255) NULL,
	[NomenclatureCode] [nvarchar](50) NULL,
	[NomenclatureNameKz] [nvarchar](255) NULL,
	[NomenclatureNameRu] [nvarchar](255) NULL,
	[NomenclatureDescriptionKz] [nvarchar](255) NULL,
	[NomenclatureDescriptionRu] [nvarchar](255) NULL,
	[ApplicationAreaKz] [nvarchar](255) NULL,
	[ApplicationAreaRu] [nvarchar](255) NULL,
	[PurposeKz] [nvarchar](255) NULL,
	[PurposeRu] [nvarchar](255) NULL,
	[IsClosedSystem] [bit] NULL,
	[RegistrationDossierPageNumber] [nvarchar](50) NULL,
	[ShortTechnicalCharacteristicKz] [nvarchar](255) NULL,
	[ShortTechnicalCharacteristicRu] [nvarchar](255) NULL,
	[ClassOfPotentialRisk] [nvarchar](50) NULL,
	[IsBalk] [bit] NULL,
	[IsMeasurementDevice] [bit] NULL,
	[IsForInvitroDiagnostics] [bit] NULL,
	[IsSterile] [bit] NULL,
	[IsMedicalProductPresence] [bit] NULL,
	[WithouAe] [bit] NULL,
	[TransportConditions] [nvarchar](255) NULL,
	[StorageConditions] [nvarchar](255) NULL,
	[Production] [nvarchar](255) NULL,
	[IsComplectation] [bit] NULL,
	[ManufacturerType] [nvarchar](255) NULL,
	[ManufacturerNameRu] [nvarchar](255) NULL,
	[AllowedDocumentNumber] [nvarchar](255) NULL,
	[BossLastName] [nvarchar](50) NULL,
	[BossPosition] [nvarchar](255) NULL,
	[OrganizationForm] [nvarchar](255) NULL,
	[ManufacturerNameKz] [nvarchar](255) NULL,
	[DateOfIssue] [datetime] NULL,
	[BossFirstName] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Country] [nvarchar](255) NULL,
	[ManufacturerNameEn] [nvarchar](255) NULL,
	[ManufacturerExpirationDate] [datetime] NULL,
	[BossMiddleName] [nvarchar](50) NULL,
	[Email] [nvarchar](255) NULL,
	[ContactPersonInitials] [nvarchar](255) NULL,
	[ContactPersonLegalAddress] [nvarchar](255) NULL,
	[ContactPersonPosition] [nvarchar](255) NULL,
	[ContactPersonFactAddress] [nvarchar](255) NULL,
	[Agreement] [nvarchar](4000) NULL,
	[IsAgreed] [bit] NULL,
	[ContractId] [uniqueidentifier] NULL,
	[RegistrationTypeValue] [nvarchar](50) NULL,
 CONSTRAINT [PK_EMP_Statement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [ncels]
GO

/****** Object:  Table [dbo].[EMP_StatementChange]    Script Date: 27.12.2017 15:20:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_StatementChange](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Type] [nvarchar](50) NULL,
	[BeforeChange] [nvarchar](50) NULL,
	[AfterChange] [nvarchar](50) NULL,
	[StatementId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_EMP_StatementChange] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [ncels]
GO

/****** Object:  Table [dbo].[EMP_StatementCountryRegistration]    Script Date: 27.12.2017 15:20:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_StatementCountryRegistration](
	[Id] [uniqueidentifier] NOT NULL,
	[Country] [nvarchar](255) NULL,
	[RegistrationNumber] [nvarchar](50) NULL,
	[StartDate] [datetime] NULL,
	[ExpirationDate] [datetime] NULL,
	[IsIndefinitely] [bit] NULL,
	[StatementId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_EMP_StatementCountryRegistration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [ncels]
GO

/****** Object:  Table [dbo].[EMP_StatementMedicalDeviceComplectation]    Script Date: 27.12.2017 15:20:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_StatementMedicalDeviceComplectation](
	[Id] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](50) NULL,
	[Name] [nvarchar](255) NULL,
	[Identifier] [nvarchar](50) NULL,
	[Model] [nvarchar](255) NULL,
	[Manufacturer] [nvarchar](255) NULL,
	[Country] [nvarchar](255) NULL,
	[StatementId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_EMP_StatementMedicalDeviceComplectation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [ncels]
GO

/****** Object:  Table [dbo].[EMP_StatementMedicalDevicePackage]    Script Date: 27.12.2017 15:20:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_StatementMedicalDevicePackage](
	[Id] [uniqueidentifier] NOT NULL,
	[Kind] [nvarchar](50) NULL,
	[Name] [nvarchar](255) NULL,
	[VolumeValue] [nvarchar](50) NULL,
	[VolumeUnit] [nvarchar](50) NULL,
	[Count] [int] NULL,
	[Description] [nvarchar](255) NULL,
	[StatementId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_EMP_StatementMedicalDevicePackage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [ncels]
GO

/****** Object:  Table [dbo].[EMP_StatementStorageLife]    Script Date: 27.12.2017 15:21:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_StatementStorageLife](
	[Id] [uniqueidentifier] NOT NULL,
	[Kind] [nvarchar](50) NULL,
	[ExpirationDate] [datetime] NULL,
	[Measure] [nvarchar](50) NULL,
	[IsIndefinitely] [bit] NULL,
	[StatementId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_EMP_StatementStorageLife] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


