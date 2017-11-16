
CREATE TABLE [dbo].[EXP_DrugProtectionDoc](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DrugDeclarationId] [uniqueidentifier] NOT NULL,
	[NameDocument] [nvarchar](500) NULL,
	[NumberDocument] [nvarchar](500) NULL,
	[IssueDate] [datetime] NULL,
	[ExpireDate] [datetime] NULL,
 CONSTRAINT [PK_EXP_DrugProtectionDoc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_DrugProtectionDoc]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugProtectionDoc_EXP_DrugDeclaration] FOREIGN KEY([DrugDeclarationId])
REFERENCES [dbo].[EXP_DrugDeclaration] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugProtectionDoc] CHECK CONSTRAINT [FK_EXP_DrugProtectionDoc_EXP_DrugDeclaration]
GO


CREATE TABLE [dbo].[EXP_DIC_DrugTypeKind](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](2000) NULL,
	[NameKz] [nvarchar](2000) NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsNeedName] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEdit] [datetime] NULL,
 CONSTRAINT [PK_EXP_DIC_DrugTypeKind] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[EXP_DIC_DrugTypeKind] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsNeedName], [IsDeleted], [DateEdit]) VALUES (1, N'11', N'Оригинальный', N'Оригинальный', CAST(N'2016-01-01T00:00:00.000' AS DateTime), 1, 0, NULL)
INSERT [dbo].[EXP_DIC_DrugTypeKind] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsNeedName], [IsDeleted], [DateEdit]) VALUES (2, N'12', N'Генерик', N'Генерик', CAST(N'2016-01-01T00:00:00.000' AS DateTime), 1, 0, NULL)
INSERT [dbo].[EXP_DIC_DrugTypeKind] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsNeedName], [IsDeleted], [DateEdit]) VALUES (3, N'13', N'Биосимиляр', N'Биосимиляр', CAST(N'2016-01-01T00:00:00.000' AS DateTime), 1, 0, NULL)
INSERT [dbo].[EXP_DIC_DrugTypeKind] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsNeedName], [IsDeleted], [DateEdit]) VALUES (4, N'14', N'Автогенерик', N'Автогенерик', CAST(N'2016-01-01T00:00:00.000' AS DateTime), 1, 0, NULL)
INSERT [dbo].[EXP_DIC_DrugTypeKind] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsNeedName], [IsDeleted], [DateEdit]) VALUES (5, N'15', N'Исследования водных растворов генерических препаратов ин-витро', N'Исследования водных растворов генерических препаратов ин-витро', CAST(N'2016-01-01T00:00:00.000' AS DateTime), 0, 0, NULL)
INSERT [dbo].[EXP_DIC_DrugTypeKind] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsNeedName], [IsDeleted], [DateEdit]) VALUES (6, N'16', N'Внедрение трансфера производственных и технологических процессов', N'Внедрение трансфера производственных и технологических процессов', CAST(N'2016-01-01T00:00:00.000' AS DateTime), 0, 0, NULL)

GO
ALTER TABLE [EXP_DrugType]
ADD DrugTypeKind int NULL
GO

ALTER TABLE [dbo].[EXP_DrugType]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugType_EXP_DIC_DrugTypeKind] FOREIGN KEY([DrugTypeKind])
REFERENCES [dbo].[EXP_DIC_DrugTypeKind] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugType] CHECK CONSTRAINT [FK_EXP_DrugType_EXP_DIC_DrugTypeKind]
GO

CREATE TABLE [dbo].[EXP_DrugOtherCountry](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DrugDeclarationId] [uniqueidentifier] NOT NULL,
	[RegNumber] [nvarchar](500) NULL,
	[IssueDate] [date] NULL,
	[ExpireDate] [date] NULL,
	[CountryId] [bigint] NULL,
 CONSTRAINT [PK_EXP_DrugOtherCountry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_DrugOtherCountry]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugOtherCountry_EXP_DrugDeclaration] FOREIGN KEY([DrugDeclarationId])
REFERENCES [dbo].[EXP_DrugDeclaration] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugOtherCountry] CHECK CONSTRAINT [FK_EXP_DrugOtherCountry_EXP_DrugDeclaration]
GO

ALTER TABLE [dbo].[EXP_DrugOtherCountry]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugOtherCountry_sr_countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[sr_countries] ([id])
GO

ALTER TABLE [dbo].[EXP_DrugOtherCountry] CHECK CONSTRAINT [FK_EXP_DrugOtherCountry_sr_countries]
GO

CREATE TABLE [dbo].[EXP_DrugOrganizations](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DrugDeclarationId] [uniqueidentifier] NOT NULL,
	[Type] [int] NOT NULL,
	[NameKz] [nvarchar](500) NULL,
	[NameRu] [nvarchar](500) NULL,
	[NameEn] [nvarchar](500) NULL,
	[CountryDicId] [uniqueidentifier] NULL,
	[AddressLegal] [nvarchar](500) NULL,
	[AddressFact] [nvarchar](500) NULL,
	[Phone] [nvarchar](500) NULL,
	[Fax] [nvarchar](500) NULL,
	[Email] [nvarchar](500) NULL,
	[BossFio] [nvarchar](500) NULL,
	[BossPosition] [nvarchar](500) NULL,
	[ContactFio] [nvarchar](500) NULL,
	[ContactPosition] [nvarchar](500) NULL,
	[ContactPhone] [nvarchar](500) NULL,
	[ContactFax] [nvarchar](500) NULL,
	[ContactEmail] [nvarchar](500) NULL,
	[OrgManufactureTypeDicId] [uniqueidentifier] NULL,
	[DocNumber] [nvarchar](500) NULL,
	[DocDate] [date] NULL,
	[DocExpiryDate] [date] NULL,
	[ObjectId] [uniqueidentifier] NULL,
	[OpfTypeDicId] [uniqueidentifier] NULL,
	[BankName] [nvarchar](500) NULL,
	[BankIik] [nvarchar](500) NULL,
	[BankCurencyDicId] [uniqueidentifier] NULL,
	[BankSwift] [nvarchar](500) NULL,
	[Bin] [nvarchar](500) NULL,
	[IsResident] [bit] NOT NULL,
	[PayerTypeDicId] [uniqueidentifier] NULL,
	[BossLastName] [nvarchar](100) NULL,
	[BossFirstName] [nvarchar](100) NULL,
	[BossMiddleName] [nvarchar](100) NULL,
	[PaymentBill] [nvarchar](500) NULL,
	[Iin] [nvarchar](500) NULL,
	[BankBik] [nvarchar](500) NULL,
 CONSTRAINT [PK_EXP_DrugOrganizations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[EXP_DrugOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_EXP_DrugDeclaration] FOREIGN KEY([DrugDeclarationId])
REFERENCES [dbo].[EXP_DrugDeclaration] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugOrganizations] CHECK CONSTRAINT [FK_Organizations_EXP_DrugDeclaration]
GO

ALTER TABLE [dbo].[EXP_DrugOrganizations] ADD  CONSTRAINT [DF_DrugOrganizations_Type]  DEFAULT ((0)) FOR [Type]
GO

ALTER TABLE [dbo].[EXP_DrugOrganizations] ADD  CONSTRAINT [DF_DrugOrganizations_IsResident]  DEFAULT ((0)) FOR [IsResident]
GO

CREATE TABLE [dbo].[EXP_DrugPrice](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DrugDeclarationId] [uniqueidentifier] NOT NULL,
	[PrimaryValue] [nvarchar](500) NULL,
	[SecondaryValue] [nvarchar](500) NULL,
	[IntermediateValue] [nvarchar](500) NULL,
	[CountUnit] [float] NULL,
	[Barcode] [nvarchar](50) NULL,
	[ManufacturePrice] [float] NULL,
	[RefPrice] [float] NULL,
	[RegPrice] [float] NULL,
 CONSTRAINT [PK_EXP_DrugPrice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_DrugPrice]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugPrice_EXP_DrugDeclaration] FOREIGN KEY([DrugDeclarationId])
REFERENCES [dbo].[EXP_DrugDeclaration] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugPrice] CHECK CONSTRAINT [FK_EXP_DrugPrice_EXP_DrugDeclaration]
GO


