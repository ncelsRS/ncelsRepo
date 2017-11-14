USE [ncels]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PriceRejectProjects](
	[Id] [uniqueidentifier] NOT NULL,
	[RegNumber] [nvarchar](500) NULL,
	[RejectReasonDicId] [uniqueidentifier] NULL,
	[DocumentId] [uniqueidentifier] NULL,
	[RegisterId] [int] NULL,
	[RegisterDfId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ProjectRejectPrice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[PriceRejectProjects] ADD  CONSTRAINT [DF_PriceRejectProjects_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[PriceRejectProjects] ADD  CONSTRAINT [DF_PriceRejectProjects_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO

GO
