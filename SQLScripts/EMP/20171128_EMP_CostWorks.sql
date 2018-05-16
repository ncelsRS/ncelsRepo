USE [ncels]
GO

/****** Object:  Table [dbo].[EMP_CostWorks]    Script Date: 28.11.2017 10:55:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_CostWorks](
	[Id] [uniqueidentifier] NOT NULL,
	[PriceListId] [uniqueidentifier] NOT NULL,
	[Count] [int] NULL,
	[Price] [decimal](18, 2) NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[ContractId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EMP_CostWorks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EMP_CostWorks]  WITH CHECK ADD  CONSTRAINT [FK_EMP_CostWorks_EMP_Contract] FOREIGN KEY([ContractId])
REFERENCES [dbo].[EMP_Contract] ([Id])
GO

ALTER TABLE [dbo].[EMP_CostWorks] CHECK CONSTRAINT [FK_EMP_CostWorks_EMP_Contract]
GO

ALTER TABLE [dbo].[EMP_CostWorks]  WITH CHECK ADD  CONSTRAINT [FK_EMP_CostWorks_EMP_Ref_PriceList] FOREIGN KEY([PriceListId])
REFERENCES [dbo].[EMP_Ref_PriceList] ([Id])
GO

ALTER TABLE [dbo].[EMP_CostWorks] CHECK CONSTRAINT [FK_EMP_CostWorks_EMP_Ref_PriceList]
GO


