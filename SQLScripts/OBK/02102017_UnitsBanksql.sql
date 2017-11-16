USE [ncelsProd]
GO

/****** Object:  Table [dbo].[UnitsBank]    Script Date: 02.10.2017 15:16:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UnitsBank](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UnitsId] [uniqueidentifier] NOT NULL,
	[CurrencyId] [uniqueidentifier] NULL,
	[BankNameRu] [nvarchar](500) NULL,
	[BankNameKz] [nvarchar](500) NULL,
	[KBE] [nvarchar](50) NULL,
	[Code] [nvarchar](50) NULL,
	[SWIFT] [nvarchar](50) NULL,
	[IIK] [nvarchar](50) NULL,
	[CorrespondentBank] [nvarchar](500) NULL,
	[CorrespondentAccount] [nvarchar](500) NULL,
	[SWIFT1] [nvarchar](50) NULL,
	[CorrespondentAccount1] [nvarchar](500) NULL,
	[SWIFT2] [nvarchar](50) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NULL
 CONSTRAINT [PK_UnitsBank] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


