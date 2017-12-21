USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_Ref_LaboratoryType]    Script Date: 19.12.2017 10:12:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_Ref_LaboratoryType](
	[Id] [uniqueidentifier] NOT NULL,
	[NameRu] [nvarchar](255) NOT NULL,
	[NameKz] [nvarchar](255) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_OBK_Ref_LaboratoryType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


