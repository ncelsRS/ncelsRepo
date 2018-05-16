USE [ncels]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PriceProject_Ext](
	[Id] [uniqueidentifier] NOT NULL,
	[MnnCode] [nvarchar](100) NULL,
	[DrugDescription] [nvarchar](1000) NULL,
	[RegEndDate] [datetime] NULL,
	[RequesterContacts] [nvarchar](500) NULL,
	[MarginalPriceMnn2016] [decimal](18, 2) NULL,
	[MarginalPriceTn622] [decimal](18, 2) NULL,
	[PriceDropPercent] [decimal](18, 2) NULL,
	[IsConformity639] [bit] NULL,
	[Conformity639Note] [nvarchar](500) NULL,
	[FixPrice_a_11_16] [decimal](18, 2) NULL,
	[PriceDifference2016] [decimal](18, 2) NULL,
	[IntRef_MarginalPrice206] [decimal](18, 2) NULL,
	[IntRef_AvgInPricePackage2015] [decimal](18, 2) NULL,
	[IntRef_AvgInPricePackage2015CurrencyId] [uniqueidentifier] NULL,
	[IntRef_AvgInPriceObkUnit2016] [decimal](18, 2) NULL,
	[IntRef_AvgInPricePackage] [decimal](18, 2) NULL,
	[IntRef_AvgOptPriceUnit_10_16] [decimal](18, 2) NULL,
	[IntRef_RetailAktobe] [decimal](18, 2) NULL,
	[IntRef_AvgRetPriceUnit_10_16] [decimal](18, 2) NULL,
	[IntRef_PurchasePriceUnit2015] [decimal](18, 2) NULL,
	[ExtRef_British] [decimal](18, 2) NULL,
	[ExtRef_Belarus] [decimal](18, 2) NULL,
	[ExtRef_Czech] [decimal](18, 2) NULL,
	[ExtRef_Hungary] [decimal](18, 2) NULL,
	[ExtRef_Latvia] [decimal](18, 2) NULL,
	[ExtRef_Rf] [decimal](18, 2) NULL,
	[ExtRef_Austria] [decimal](18, 2) NULL,
	[ExtRef_Ukraine] [decimal](18, 2) NULL,
	[ExtRef_Turkey] [decimal](18, 2) NULL,
	[MinRefPrice2016] [decimal](18, 2) NULL,
	[MinRefPriceCoef] [decimal](18, 2) NULL,

	[p16_Country] [nvarchar](100) NULL,
	[p16_RegPrice] [nvarchar](100) NULL,
	[p16_RegYear] [int] NULL,
	[p16_MarginalPrice] [nvarchar](100) NULL,
	[p16_MarginalYear] [int] NULL,
	[p16_AvgOptPricee] [nvarchar](100) NULL,
	[p16_AvgRetailPrice] [nvarchar](100) NULL,
	[RegNcelsPrice_11_16] [decimal](18, 2) NULL,
	[RegMzsrPrice_11_16] [decimal](18, 2) NULL,
	[FinalPrice] [decimal](18, 2) NULL,
	[FinalPricePercent] [decimal](18, 2) NULL,
	[FinalFixPrice] [decimal](18, 2) NULL,
	[FinalMarginalPriceTn] [decimal](18, 2) NULL,
	[ProjectPrice2017] [decimal](18, 2) NULL
 CONSTRAINT [PK_PriceProject_Ext] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


