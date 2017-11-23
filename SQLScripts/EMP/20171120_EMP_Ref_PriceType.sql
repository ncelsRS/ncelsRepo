USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_EMP_ServiceType]    Script Date: 20.11.2017 15:40:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_Ref_PriceType](
	[Id] [uniqueidentifier] NOT NULL,
	[NameRu] [nvarchar](255) NOT NULL,
	[NameKz] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_EMP_Ref_PriceType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


