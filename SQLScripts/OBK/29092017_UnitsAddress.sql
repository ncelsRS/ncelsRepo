USE [ncelsProd]
GO

/****** Object:  Table [dbo].[UnitsAddress]    Script Date: 29.09.2017 18:32:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UnitsAddress](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UnitsId] [uniqueidentifier] NOT NULL,
	[RegionId] [uniqueidentifier] NOT NULL,
	[AddressNameRu] [nvarchar](4000) NULL,
	[AddressNameKz] [nvarchar](4000) NULL,
	[IsDeleted] [bit] NULL
 CONSTRAINT [PK_UnitsAddress] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


