USE [ncels]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PP_ProtocolProductPrices](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ProtocolId] [uniqueidentifier] NOT NULL,
	[ProductNameRu] [nvarchar](500) NULL,
	[ProductNameKz] [nvarchar](500) NULL,
	[PriceFirst] [decimal](18, 2) NOT NULL,
	[PriceNew] [decimal](18, 2) NOT NULL,
	[PriceProjectId] [uniqueidentifier] NULL

 CONSTRAINT [PK_PP_ProtocolProductPrices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PP_ProtocolProductPrices] ADD  CONSTRAINT [DF_PP_ProtocolProductPrices_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[PP_ProtocolProductPrices]  WITH CHECK ADD  CONSTRAINT [FK_PP_ProtocolProductPrices_Protocol] FOREIGN KEY([ProtocolId])
REFERENCES [dbo].[PP_Protocols] ([Id])
GO

ALTER TABLE [dbo].[PP_ProtocolProductPrices] CHECK CONSTRAINT [FK_PP_ProtocolProductPrices_Protocol]
GO

