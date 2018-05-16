USE [ncelsProd]
GO

/****** Object:  Table [dbo].[UnitSigner]    Script Date: 03.10.2017 19:35:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UnitSigner](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UnitsId] [uniqueidentifier] NOT NULL,
	[SignerId] [uniqueidentifier] NOT NULL,
	[DocumentType] [uniqueidentifier] NULL,
	[DocNumber] [nvarchar](50) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NULL
 CONSTRAINT [PK_UnitSigner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


