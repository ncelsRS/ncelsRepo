USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_UniqueNumber]    Script Date: 05.12.2017 9:29:16 ******/
DROP TABLE [dbo].[OBK_UniqueNumber]
GO

/****** Object:  Table [dbo].[OBK_UniqueNumber]    Script Date: 05.12.2017 9:29:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_UniqueNumber](
	[Id] [uniqueidentifier] NOT NULL,
	[DeclarantId] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](20) NOT NULL,
	[Number] [int] NOT NULL,
	[ProductSeriesId] [int] NOT NULL,
 CONSTRAINT [PK_OBK_UniqueNumber] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


