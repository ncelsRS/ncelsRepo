CREATE TABLE [dbo].[DIC_Storages](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](512) NOT NULL,
	[NameKz] [nvarchar](512) NULL,
	[NameEn] [nvarchar](512) NULL,
	[Note] [nvarchar](1024) NULL,
	[CreatedDate] [datetime] NULL,
	[DeleteDate] [datetime] NULL,
	[ParentId] [uniqueidentifier] NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_DIC_Storages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[DIC_Storages] ADD  CONSTRAINT [DF_DIC_Storages_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[DIC_Storages] ADD  CONSTRAINT [DF_DIC_Storages_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[DIC_Storages]  WITH CHECK ADD  CONSTRAINT [FK_DIC_Storages_Parent_Storage] FOREIGN KEY([ParentId])
REFERENCES [dbo].[DIC_Storages] ([Id])
GO

ALTER TABLE [dbo].[DIC_Storages] CHECK CONSTRAINT [FK_DIC_Storages_Parent_Storage]
GO

ALTER TABLE [dbo].[DIC_Storages]  WITH CHECK ADD  CONSTRAINT [FK_DIC_Storages_Units] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Units] ([Id])
GO

ALTER TABLE [dbo].[DIC_Storages] CHECK CONSTRAINT [FK_DIC_Storages_Units]
GO


