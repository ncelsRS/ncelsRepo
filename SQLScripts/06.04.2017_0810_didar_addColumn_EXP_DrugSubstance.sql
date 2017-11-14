ALTER TABLE [dbo].[EXP_DrugSubstance]
ADD [IsNotFound] [bit] NOT NULL default(0)
GO
ALTER TABLE [dbo].[EXP_DrugSubstance]
ADD [NewName] [nvarchar](500) NULL
GO

CREATE TABLE [dbo].[EXP_DIC_NormDocFarm](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](2000) NULL,
	[NameKz] [nvarchar](2000) NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEdit] [datetime] NULL,
 CONSTRAINT [PK_EXP_DIC__NormDocFarm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[EXP_DIC_NormDocFarm] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (1, N'1', N'Государственная Фармакопея Республики Казахстан ГФ РК', N'ҚР МФ Қазақстан Республикасының Мемлекеттік фармакопеясы', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_NormDocFarm] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (2, N'2', N'Британская Фармакопея', N'Британ фармакопеясы BR BR', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_NormDocFarm] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (3, N'3', N'Фармакопея США', N'АҚШ фармакопеясы USP', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_NormDocFarm] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (4, N'4', N'Фармакопея Франции', N'Франция фармакопеясы Fr.Ph', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_NormDocFarm] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (5, N'5', N'Фармакопея Германии', N'Германия фармакопеясы DAB', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_NormDocFarm] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (6, N'6', N'Европейская Фармакопея', N'Еуропалық фармакопея Ph. Eur', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_NormDocFarm] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (7, N'7', N'Фармакопея Швейцарии', N'Швейцария фармакопеясы Swiss Ph.', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_NormDocFarm] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (8, N'8', N'Государственная Фармакопея', N'Мемлекеттiк фармакопея', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_NormDocFarm] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (9, N'9', N'НД фирмы', N'Фирманың НҚ ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_NormDocFarm] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (10, N'10', N'Фармакопея Индии', N'Үндістан фармакопеясы', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_NormDocFarm] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (11, N'11', N'Фармакопея Китая', N'Қытай Фармакопеясы', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
GO
ALTER TABLE [dbo].[EXP_DrugSubstance]
ADD [NormDocFarmId] [int] NULL
GO
ALTER TABLE [dbo].[EXP_DrugSubstance]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugSubstance_NormDocFarm] FOREIGN KEY([NormDocFarmId])
REFERENCES [dbo].[EXP_DIC_NormDocFarm] ([Id])
GO
ALTER TABLE [dbo].[EXP_DrugDeclaration]
ADD [GrlsNote] [nvarchar](2000) NULL
GO