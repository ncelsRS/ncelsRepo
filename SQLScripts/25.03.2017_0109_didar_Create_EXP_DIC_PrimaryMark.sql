CREATE TABLE [dbo].[EXP_DIC_PrimaryMark](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](2000) NULL,
	[NameKz] [nvarchar](2000) NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEdit] [datetime] NULL,
 CONSTRAINT [PK_EXP_DIC_PrimaryMark] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[EXP_DIC_PrimaryMark] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (1, N'mark', N'Сертификат GMP', N'Сертификат GMP', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_PrimaryMark] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (2, N'mark', N'Патент РК', N'Патент РК', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_PrimaryMark] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (3, N'mark', N'Новый производитель', N'Новый производитель', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_PrimaryMark] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (4, N'mark', N'Признак ОТД', N'Признак ОТД', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_PrimaryMark] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (5, N'mark', N'Товарный знак', N'Товарный знак', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_PrimaryMark] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (6, N'mark', N'Без аналитики', N'Без аналитики', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_PrimaryMark] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (7, N'mark', N'Бессрочность', N'Бессрочность', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_PrimaryMark] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (8, N'kind', N'Нет', N'Нет', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_PrimaryMark] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (9, N'kind', N'Ускорение сроков', N'Ускорение сроков', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_PrimaryMark] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (10, N'kind', N'Исключение этапов', N'Исключение этапов', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
GO
CREATE TABLE [dbo].[EXP_DrugPrimaryKind](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DrugDeclarationId] [uniqueidentifier] NOT NULL,
	[PrimaryKindId] [int] NOT NULL,
 CONSTRAINT [PK_EXP_DrugPrimaryKind] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_DrugPrimaryKind]  WITH CHECK ADD  CONSTRAINT [FK_EXP_PrimaryKind_EXP_DrugDeclaration] FOREIGN KEY([DrugDeclarationId])
REFERENCES [dbo].[EXP_DrugDeclaration] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugPrimaryKind] CHECK CONSTRAINT [FK_EXP_PrimaryKind_EXP_DrugDeclaration]
GO

ALTER TABLE [dbo].[EXP_DrugPrimaryKind]  WITH CHECK ADD  CONSTRAINT [FK_EXP_PrimaryKind_EXP_DIC_PrimaryMark] FOREIGN KEY([PrimaryKindId])
REFERENCES [dbo].[EXP_DIC_PrimaryMark] ([id])
GO

ALTER TABLE [dbo].[EXP_DrugPrimaryKind] CHECK CONSTRAINT [FK_EXP_PrimaryKind_EXP_DIC_PrimaryMark]
GO
ALTER TABLE [dbo].[EXP_DrugDeclaration]
ADD [ExpeditedType] [nvarchar](2000) NULL
GO

CREATE TABLE [dbo].[EXP_DIC_RemarkType](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](2000) NULL,
	[NameKz] [nvarchar](2000) NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEdit] [datetime] NULL,
 CONSTRAINT [PK_EXP_DIC_RemarkType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[EXP_DIC_RemarkType] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (1, N'reject', N'Не полная информация', N'Не полная информация', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_RemarkType] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (2, N'reject', N'Запрос пояснения', N'Запрос пояснения', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_RemarkType] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (3, N'reject', N'Не достоверная информация', N'Не достоверная информация', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
GO

CREATE TABLE [dbo].[EXP_DrugAppDosageResult](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NameResult] [nvarchar](2000) NULL,
	[IsMark] [bit] NOT NULL,
	[RemarkTypeId] [int] NULL,
	[Note] [nvarchar](2000) NULL,
	[RemarkDate] [datetime] NULL,
	[FixedDate] [datetime] NULL,
	[DrugDosageId] [bigint] NOT NULL,
 CONSTRAINT [PK_EXP_DrugAppDosageResult] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_DrugAppDosageResult]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugAppDosageResult_DrugDosage] FOREIGN KEY([DrugDosageId])
REFERENCES [dbo].[EXP_DrugDosage] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugAppDosageResult] CHECK CONSTRAINT [FK_EXP_DrugAppDosageResult_DrugDosage]
GO

ALTER TABLE [dbo].[EXP_DrugAppDosageResult]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugAppDosageResult_RemarkType] FOREIGN KEY([RemarkTypeId])
REFERENCES [dbo].[EXP_DIC_RemarkType] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugAppDosageResult] CHECK CONSTRAINT [FK_EXP_DrugAppDosageResult_RemarkType]
GO



CREATE TABLE [dbo].[EXP_DrugAppDosageRemark](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NameResult] [nvarchar](2000) NULL,
	[IsMark] [bit] NOT NULL,
	[RemarkTypeId] [int] NULL,
	[Note] [nvarchar](2000) NULL,
	[RemarkDate] [datetime] NULL,
	[FixedDate] [datetime] NULL,
	[DrugDosageId] [bigint] NOT NULL,
 CONSTRAINT [PK_EXP_DrugAppDosageRemark] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE [dbo].[EXP_DrugAppDosageRemark]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugAppDosageRemark_DrugDosage] FOREIGN KEY([DrugDosageId])
REFERENCES [dbo].[EXP_DrugDosage] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugAppDosageRemark] CHECK CONSTRAINT [FK_EXP_DrugAppDosageRemark_DrugDosage]
GO

ALTER TABLE [dbo].[EXP_DrugAppDosageRemark]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugAppDosageRemark_RemarkType] FOREIGN KEY([RemarkTypeId])
REFERENCES [dbo].[EXP_DIC_RemarkType] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugAppDosageRemark] CHECK CONSTRAINT [FK_EXP_DrugAppDosageRemark_RemarkType]
GO


