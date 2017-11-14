USE [ncels]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UniqueIdentificators](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](500) NULL,
	[Value] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL
 CONSTRAINT [PK_UniqueIdentificators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE [dbo].[UniqueIdentificators] ADD  CONSTRAINT [DF_UniqueIdentificators_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO