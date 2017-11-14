CREATE TABLE [dbo].[DIC_FileLinkStatus](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](2000) NULL,
	[NameKz] [nvarchar](2000) NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEdit] [datetime] NULL,
 CONSTRAINT [PK_DIC_FileLinkStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[DIC_FileLinkStatus] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (1, N'DRAFT', N'Черновик', N'Черновик', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[DIC_FileLinkStatus] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (2, N'ON_AGREEMENT', N'На согласовании', N'На согласовании', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[DIC_FileLinkStatus] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (3, N'AGREED', N'Согласован', N'Согласован', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[DIC_FileLinkStatus] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (4, N'REFUSED', N'Отказ', N'Отказ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[DIC_FileLinkStatus] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (5, N'SENDED', N'Отправлен', N'Отправлен', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[DIC_FileLinkStatus] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (6, N'ACCEPTED', N'Принят', N'Принят', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[DIC_FileLinkStatus] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (7, N'FOR_REVISION', N'На доработку', N'На доработку', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
GO

ALTER TABLE [dbo].[FileLinks]
ADD [StatusId] [int] NULL
GO

ALTER TABLE [dbo].[FileLinks]  WITH CHECK ADD  CONSTRAINT [FK_FileLinks_DIC_FileLinkStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[DIC_FileLinkStatus] ([Id])
GO

ALTER TABLE [dbo].[FileLinks] CHECK CONSTRAINT [FK_FileLinks_DIC_FileLinkStatus]
GO
insert into Dictionaries (Id, Type, Code, Name, NameKz, DisplayName, IsGuide)
values (NEWID(), 'ExpAgreedDocType', '6', N'Файл с перевода', N'Файл с перевода', N'Файл с перевода',0)
GO
insert into Dictionaries (Id, Type, Code, Name, NameKz, DisplayName, IsGuide)
values (newid(), 'ExpActivityType', '8', N'Согласование документа на перевод', N'Согласование документа на перевод', N'Согласование документа на перевод', 0)