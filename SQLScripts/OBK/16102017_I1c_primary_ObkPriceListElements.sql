USE [ncelsProd]
GO

/****** Object:  Table [dbo].[I1c_primary_ObkPriceListElements]    Script Date: 16.10.2017 19:12:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[I1c_primary_ObkPriceListElements](
	[Id] [uniqueidentifier] NOT NULL,
	[PriceListId] [uniqueidentifier] NULL,
	[PriceListName] [nvarchar](512) NULL,
	[Price] [decimal](18, 2) NULL,
	[Quantity] [int] NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[DrugTradeName] [nvarchar](512) NULL,
	[DrugTradeNameKz] [nvarchar](512) NULL,
	[DrugFormName] [nvarchar](512) NULL,
	[DrugFormNameKz] [nvarchar](512) NULL,
	[refContractId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_I1c_primary_ObkPriceListElements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[I1c_primary_ObkPriceListElements] ADD  CONSTRAINT [DF_I1c_primary_ObkPriceListElements_Id]  DEFAULT (newid()) FOR [Id]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИД прейскуранта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkPriceListElements', @level2type=N'COLUMN',@level2name=N'PriceListId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Наименование прейскуранта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkPriceListElements', @level2type=N'COLUMN',@level2name=N'PriceListName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'цена за единицу' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkPriceListElements', @level2type=N'COLUMN',@level2name=N'Price'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'количество' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkPriceListElements', @level2type=N'COLUMN',@level2name=N'Quantity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ОБщая сумма для оплаты' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkPriceListElements', @level2type=N'COLUMN',@level2name=N'TotalPrice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Торговое название' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkPriceListElements', @level2type=N'COLUMN',@level2name=N'DrugTradeName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Торговое название каз' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkPriceListElements', @level2type=N'COLUMN',@level2name=N'DrugTradeNameKz'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Лекарственная форма' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkPriceListElements', @level2type=N'COLUMN',@level2name=N'DrugFormName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Лекарственная форма Каз' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkPriceListElements', @level2type=N'COLUMN',@level2name=N'DrugFormNameKz'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ссылка на ИД договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkPriceListElements', @level2type=N'COLUMN',@level2name=N'refContractId'
GO


