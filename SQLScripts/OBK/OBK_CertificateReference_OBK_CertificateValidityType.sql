	USE [ncels]
	GO
	/****** Object:  Table [dbo].[OBK_CertificateReference]    Script Date: 16.11.2017 12:42:22 ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE TABLE [dbo].[OBK_CertificateReference](
		[Id] [uniqueidentifier] NOT NULL,
		[Number] [nvarchar](max) NULL,
		[CertificateNumber] [nvarchar](max) NULL,
		[StartDate] [datetime] NULL,
		[EndDate] [datetime] NULL,
		[CertificateCountryId] [uniqueidentifier] NULL,
		[CertificateOrganization] [nvarchar](500) NULL,
		[CertificateTypeId] [int] NOT NULL,
		[LastInspection] [datetime] NULL,
		[CertificateValidityTypeId] [smallint] NULL,
		[DocumentId] [uniqueidentifier] NULL,
	 CONSTRAINT [PK_OBK_CertificateReference] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	GO
	/****** Object:  Table [dbo].[OBK_CertificateValidityType]    Script Date: 16.11.2017 12:42:22 ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE TABLE [dbo].[OBK_CertificateValidityType](
		[Id] [smallint] NOT NULL,
		[Name] [nvarchar](max) NULL,
		[Code] [nvarchar](max) NULL,
	 CONSTRAINT [PK_OBK_CertificateValidityType] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	GO
	INSERT [dbo].[OBK_CertificateValidityType] ([Id], [Name], [Code]) VALUES (1, N'Действующий', N'Active')
	INSERT [dbo].[OBK_CertificateValidityType] ([Id], [Name], [Code]) VALUES (2, N'Не действующий', N'Passive')
	INSERT [dbo].[OBK_CertificateValidityType] ([Id], [Name], [Code]) VALUES (3, N'Отозван', N'Recalled')
	ALTER TABLE [dbo].[OBK_CertificateReference]  WITH CHECK ADD  CONSTRAINT [FK_OBK_CertificateReference_OBK_CertificateValidityType] FOREIGN KEY([CertificateValidityTypeId])
	REFERENCES [dbo].[OBK_CertificateValidityType] ([Id])
	GO
	ALTER TABLE [dbo].[OBK_CertificateReference] CHECK CONSTRAINT [FK_OBK_CertificateReference_OBK_CertificateValidityType]
	GO
