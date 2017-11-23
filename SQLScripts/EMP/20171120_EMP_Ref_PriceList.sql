USE [ncels]
GO

/****** Object:  Table [dbo].[EMP_Ref_PriceList]    Script Date: 20.11.2017 16:58:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_Ref_PriceList](
	[Id] [uniqueidentifier] NOT NULL,
	[ServiceTypeId] [uniqueidentifier] NOT NULL,
	[PriceTypeId] [uniqueidentifier] NOT NULL,
	[Import] bit NULL,
	[Price] [decimal](18, 2) NULL
 CONSTRAINT [PK_EMP_Ref_PriceList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EMP_Ref_PriceList]  WITH CHECK ADD  CONSTRAINT [FK_EMP_Ref_PriceList_ServiceTypeId_EMP_Ref_ServiceType_Id] FOREIGN KEY([ServiceTypeId])
REFERENCES [dbo].[EMP_Ref_ServiceType] ([Id])
GO

ALTER TABLE [dbo].[EMP_Ref_PriceList]  WITH CHECK ADD  CONSTRAINT [FK_EMP_Ref_PriceList_PriceTypeId_EMP_Ref_PriceType_Id] FOREIGN KEY([PriceTypeId])
REFERENCES [dbo].[EMP_Ref_PriceType] ([Id])
GO


