ALTER TABLE [dbo].[FileLinks]
ADD [OwnerId] [uniqueidentifier] NULL
GO
ALTER TABLE [dbo].[FileLinks]
ADD [IsDeleted] [bit] NOT NULL default(0)
GO
ALTER TABLE [dbo].[FileLinks]  WITH CHECK ADD  CONSTRAINT [FK_EXP_FileLinks_Employees] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[FileLinks] CHECK CONSTRAINT [FK_EXP_FileLinks_Employees]
GO

INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide],[Note])
     VALUES ('1', N'Регистрационное Досье (Список)', N'Регистрационное Досье (Список)', 'DrugDeclaration',0,'application/pdf')
GO
INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide],[Note])
     VALUES ('2', N'Регистрационное Досье формат ОТД', N'Регистрационное Досье формат ОТД', 'DrugDeclaration',0,'application/pdf')
GO
INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide],[Note])
     VALUES ('3', N'Макет', N'Макет', 'DrugDeclaration',0,'image/jpeg')
GO
INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide],[Note])
     VALUES ('4', N'Инструкция', N'Инструкция', 'DrugDeclaration',0,'application/msword')
GO
INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide],[Note])
     VALUES ('5', N'Аналитический нормативный документ', N'Аналитический нормативный документ', 'DrugDeclaration',0,'application/msword')
GO


CREATE TABLE [dbo].[FileLinksCategoryCom](
	[Id] [uniqueidentifier] NOT NULL,
	[DocumentId] [uniqueidentifier] NULL,
	[CategoryId] [uniqueidentifier] NULL,
	[IsError] [bit] NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_FileLinksCategoryCom] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FileLinksCategoryComRecord]    Script Date: 01.04.2017 8:19:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileLinksCategoryComRecord](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CommentId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NOT NULL,
	[Note] [nvarchar](2000) NULL,
 CONSTRAINT [PK_FileLinksCategoryComRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[FileLinksCategoryComRecord]  WITH CHECK ADD  CONSTRAINT [FK_FileLinksCategoryComRecord_FileLinksCategoryCom] FOREIGN KEY([CommentId])
REFERENCES [dbo].[FileLinksCategoryCom] ([Id])
GO
ALTER TABLE [dbo].[FileLinksCategoryComRecord] CHECK CONSTRAINT [FK_FileLinksCategoryComRecord_FileLinksCategoryCom]
GO

ALTER TABLE [dbo].[FileLinksCategoryComRecord]  WITH CHECK ADD  CONSTRAINT [FK_FileLinksCategoryComRecord_Employees] FOREIGN KEY([UserId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[FileLinksCategoryComRecord] CHECK CONSTRAINT [FK_FileLinksCategoryComRecord_Employees]
GO

