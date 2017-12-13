USE [ncels]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_ContractFactoryCom](
	[Id] [uniqueidentifier] NOT NULL,
	[ContractFactoryId] [uniqueidentifier] NOT NULL,
	[IsError] [bit] NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_OBK_ContractFactoryCom] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_ContractFactoryCom]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ContractFactoryCom_OBK_ContractFactory_Id] FOREIGN KEY([ContractFactoryId])
REFERENCES [dbo].[OBK_ContractFactory] ([Id])
GO

ALTER TABLE [dbo].[OBK_ContractFactoryCom] CHECK CONSTRAINT [FK_OBK_ContractFactoryCom_OBK_ContractFactory_Id]
GO


USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_ContractFactoryComRecord]    Script Date: 13.12.2017 15:00:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_ContractFactoryComRecord](
	[Id] [uniqueidentifier] NOT NULL,
	[CommentId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ValueField] [nvarchar](500) NULL,
	[Note] [nvarchar](2000) NULL,
	[DisplayField] [nvarchar](500) NULL,
 CONSTRAINT [PK_OBK_ContractFactoryComRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_ContractFactoryComRecord]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ContractFactoryComRecord_CommentId_OBK_ContractFactoryCom_Id] FOREIGN KEY([CommentId])
REFERENCES [dbo].[OBK_ContractFactoryCom] ([Id])
GO

ALTER TABLE [dbo].[OBK_ContractFactoryComRecord] CHECK CONSTRAINT [FK_OBK_ContractFactoryComRecord_CommentId_OBK_ContractFactoryCom_Id]
GO

ALTER TABLE [dbo].[OBK_ContractFactoryComRecord]  WITH CHECK ADD  CONSTRAINT [OBK_ContractFactoryComRecord_UserId_Employees_Id] FOREIGN KEY([UserId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[OBK_ContractFactoryComRecord] CHECK CONSTRAINT [OBK_ContractFactoryComRecord_UserId_Employees_Id]
GO