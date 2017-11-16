CREATE TABLE [dbo].[EXP_DIC_CorespondenceKind](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](2000) NULL,
	[NameKz] [nvarchar](2000) NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEdit] [datetime] NULL,
 CONSTRAINT [PK_EXP_DIC_CorespondenceKind] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EXP_DIC_CorespondenceType]    Script Date: 17.04.2017 23:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EXP_DIC_CorespondenceType](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](2000) NULL,
	[NameKz] [nvarchar](2000) NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEdit] [datetime] NULL,
 CONSTRAINT [PK_EXP_DIC_CorespondenceType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EXP_DIC_PrimaryFinalyDocStatus]    Script Date: 17.04.2017 23:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EXP_DIC_PrimaryFinalyDocResult](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](2000) NULL,
	[NameKz] [nvarchar](2000) NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEdit] [datetime] NULL,
 CONSTRAINT [PK_EXP_DIC_PrimaryFinalyDocResult] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EXP_DIC_Stage]    Script Date: 17.04.2017 23:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EXP_DIC_Stage](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](2000) NULL,
	[NameKz] [nvarchar](2000) NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEdit] [datetime] NULL,
 CONSTRAINT [PK_EXP_DIC_Stage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EXP_DrugCorespondence]    Script Date: 17.04.2017 23:33:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EXP_DrugCorespondence](
	[Id] [uniqueidentifier] NOT NULL,
	[DrugDeclarationId] [uniqueidentifier] NOT NULL,
	[AuthorId] [uniqueidentifier] NULL,
	[SignatoryId] [uniqueidentifier] NULL,
	[MatchingId] [uniqueidentifier] NULL,
	[DateCreate] [datetime] NOT NULL,
	[DateSend] [datetime] NULL,
	[DateRead] [datetime] NULL,
	[NumberLetter] [nvarchar](50) NULL,
	[Subject] [nvarchar](200) NULL,
	[Note] [nvarchar](2000) NULL,
	[StageId] [int] NOT NULL,
	[TypeId] [int] NOT NULL,
	[KindId] [int] NOT NULL,
	[StatusId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EXP_DrugCorespondence] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[EXP_DIC_CorespondenceKind] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (1, N'1', N'Электронный', N'Электронный', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_CorespondenceKind] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (2, N'2', N'Бумажный', N'Бумажный', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_CorespondenceType] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (1, N'1', N'Исходящее', N'Исходящее', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_CorespondenceType] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (2, N'2', N'Входящее', N'Входящее', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_PrimaryFinalyDocResult] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (1, N'1', N'Соответствует', N'Соответствует', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_PrimaryFinalyDocResult] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (2, N'2', N'Не соответствует', N'Не соответствует', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_PrimaryFinalyDocResult] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (3, N'3', N'Снять с регистрации', N'Снять с регистрации', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_Stage] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (1, N'1', N'Первичная экспертиза', N'Первичная экспертиза', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_Stage] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (2, N'2', N'Фармацевтическая экспертиза', N'Фармацевтическая экспертиза', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_Stage] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (3, N'3', N'Фармакологическая экспертиза', N'Фармакологическая экспертиза', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_Stage] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (4, N'4', N'Аналитическая экспертиза', N'Аналитическая экспертиза', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_Stage] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (5, N'5', N'Перевод', N'Перевод', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_Stage] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (6, N'6', N'Экспертный совет', N'Экспертный совет', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
ALTER TABLE [dbo].[EXP_DrugCorespondence]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugCorespondence_Employees] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[EXP_DrugCorespondence] CHECK CONSTRAINT [FK_EXP_DrugCorespondence_Employees]
GO
ALTER TABLE [dbo].[EXP_DrugCorespondence]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugCorespondence_EXP_DrugDeclaration] FOREIGN KEY([DrugDeclarationId])
REFERENCES [dbo].[EXP_DrugDeclaration] ([Id])
GO
ALTER TABLE [dbo].[EXP_DrugCorespondence] CHECK CONSTRAINT [FK_EXP_DrugCorespondence_EXP_DrugDeclaration]
GO
ALTER TABLE [dbo].[EXP_DrugCorespondence]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugCorespondence_Kind] FOREIGN KEY([KindId])
REFERENCES [dbo].[EXP_DIC_CorespondenceKind] ([Id])
GO
ALTER TABLE [dbo].[EXP_DrugCorespondence] CHECK CONSTRAINT [FK_EXP_DrugCorespondence_Kind]
GO
ALTER TABLE [dbo].[EXP_DrugCorespondence]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugCorespondence_Matching] FOREIGN KEY([MatchingId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[EXP_DrugCorespondence] CHECK CONSTRAINT [FK_EXP_DrugCorespondence_Matching]
GO
ALTER TABLE [dbo].[EXP_DrugCorespondence]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugCorespondence_Signatory] FOREIGN KEY([SignatoryId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[EXP_DrugCorespondence] CHECK CONSTRAINT [FK_EXP_DrugCorespondence_Signatory]
GO
ALTER TABLE [dbo].[EXP_DrugCorespondence]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugCorespondence_Stage] FOREIGN KEY([StageId])
REFERENCES [dbo].[EXP_DIC_Stage] ([Id])
GO
ALTER TABLE [dbo].[EXP_DrugCorespondence] CHECK CONSTRAINT [FK_EXP_DrugCorespondence_Stage]
GO
ALTER TABLE [dbo].[EXP_DrugCorespondence]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugCorespondence_StatusId_Dictionaries] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO
ALTER TABLE [dbo].[EXP_DrugCorespondence] CHECK CONSTRAINT [FK_EXP_DrugCorespondence_StatusId_Dictionaries]
GO
ALTER TABLE [dbo].[EXP_DrugCorespondence]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugCorespondence_Type] FOREIGN KEY([TypeId])
REFERENCES [dbo].[EXP_DIC_CorespondenceType] ([Id])
GO
ALTER TABLE [dbo].[EXP_DrugCorespondence] CHECK CONSTRAINT [FK_EXP_DrugCorespondence_Type]
GO
ALTER TABLE [dbo].[EXP_DrugPrimaryFinalDocument]
ADD [ResultId] [int] NULL
GO

ALTER TABLE [dbo].[EXP_DrugPrimaryFinalDocument]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugPrimaryFinalDocument_ResultId] FOREIGN KEY([ResultId])
REFERENCES [dbo].[EXP_DIC_PrimaryFinalyDocResult] ([Id])
GO
ALTER TABLE [dbo].[EXP_DrugPrimaryFinalDocument] CHECK CONSTRAINT [FK_EXP_DrugPrimaryFinalDocument_ResultId]
GO
insert into Dictionaries (Id, Type, Code, Name, NameKz, DisplayName, IsGuide)
values(NEWID(), 'DRAFT', 'DRAFT', N'Черновик', N'Черновик', N'Черновик', 0)