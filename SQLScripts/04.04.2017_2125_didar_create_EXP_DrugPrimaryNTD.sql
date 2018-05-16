CREATE TABLE [dbo].[EXP_DIC_TypeFileND](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](2000) NULL,
	[NameKz] [nvarchar](2000) NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEdit] [datetime] NULL,
 CONSTRAINT [PK_EXP_DIC_TypeFileND] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EXP_DIC_TypeND]    Script Date: 04.04.2017 21:22:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EXP_DIC_TypeND](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](2000) NULL,
	[NameKz] [nvarchar](2000) NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEdit] [datetime] NULL,
 CONSTRAINT [PK_EXP_DIC_TypeND] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EXP_DrugPrimaryNTD]    Script Date: 04.04.2017 21:22:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EXP_DrugPrimaryNTD](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DrugDeclarationId] [uniqueidentifier] NOT NULL,
	[DateReg] [datetime] NULL,
	[DateConfirm] [datetime] NULL,
	[TypeNDId] [int] NULL,
	[TypeFileNDId] [int] NULL,
	[isNdConfirm] [bit] NOT NULL,
	[NumberNd] [nvarchar](50) NULL,
	[Note] [nvarchar](2000) NULL,
 CONSTRAINT [PK_EXP_DrugPrimaryNTD] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[EXP_DIC_TypeFileND] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (1, N'1', N'Основной файл НД', N'Основной файл НД', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_TypeFileND] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (2, N'2', N'Изменение №1', N'Изменение №1', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_TypeFileND] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (3, N'3', N'Изменение №2', N'Изменение №2', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_TypeFileND] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (4, N'4', N'Изменение №3', N'Изменение №3', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_TypeFileND] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (5, N'5', N'Изменение №4', N'Изменение №4', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_TypeFileND] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (6, N'6', N'Изменение №5', N'Изменение №5', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_TypeFileND] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (7, N'7', N'Изменение №6', N'Изменение №6', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_TypeFileND] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (8, N'8', N'Изменение №7', N'Изменение №7', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_TypeND] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (1, N'1', N'ВНД РК', N'ҚР УНҚ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_TypeND] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (2, N'2', N'ВФС РК', N'ҚР УФБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_TypeND] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (3, N'3', N'НД РК', N'ҚР НҚ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_TypeND] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (4, N'4', N'СП РК', N'ҚР СП', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_TypeND] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (5, N'5', N'СР РК', N'ҚР СР', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_TypeND] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (6, N'6', N'ФС РК', N'ҚР ФБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_TypeND] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (7, N'7', N'АНД', N'ТНҚ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_TypeND] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (8, N'8', N'ВАНД', N'УТНҚ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
ALTER TABLE [dbo].[EXP_DrugPrimaryNTD]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugPrimaryNTD_EXP_DIC_TypeFileND] FOREIGN KEY([TypeFileNDId])
REFERENCES [dbo].[EXP_DIC_TypeFileND] ([Id])
GO
ALTER TABLE [dbo].[EXP_DrugPrimaryNTD] CHECK CONSTRAINT [FK_EXP_DrugPrimaryNTD_EXP_DIC_TypeFileND]
GO
ALTER TABLE [dbo].[EXP_DrugPrimaryNTD]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugPrimaryNTD_EXP_DIC_TypeND] FOREIGN KEY([TypeNDId])
REFERENCES [dbo].[EXP_DIC_TypeND] ([Id])
GO
ALTER TABLE [dbo].[EXP_DrugPrimaryNTD] CHECK CONSTRAINT [FK_EXP_DrugPrimaryNTD_EXP_DIC_TypeND]
GO
ALTER TABLE [dbo].[EXP_DrugPrimaryNTD]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugPrimaryNTD_EXP_DrugDeclaration] FOREIGN KEY([DrugDeclarationId])
REFERENCES [dbo].[EXP_DrugDeclaration] ([Id])
GO
ALTER TABLE [dbo].[EXP_DrugPrimaryNTD] CHECK CONSTRAINT [FK_EXP_DrugPrimaryNTD_EXP_DrugDeclaration]
GO
