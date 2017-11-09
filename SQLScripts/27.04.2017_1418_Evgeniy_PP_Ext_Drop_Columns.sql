USE [ncels]
GO

ALTER TABLE PriceProject_Ext DROP COLUMN IntRef_MarginalPrice206;
ALTER TABLE PriceProject_Ext DROP COLUMN IntRef_AvgInPriceObkUnit2016;
ALTER TABLE PriceProject_Ext DROP COLUMN IntRef_AvgOptPriceUnit_10_16;
ALTER TABLE PriceProject_Ext DROP COLUMN IntRef_AvgRetPriceUnit_10_16;
ALTER TABLE PriceProject_Ext DROP COLUMN IntRef_PurchasePriceUnit2015;
ALTER TABLE PriceProject_Ext DROP COLUMN ExtRef_British;
ALTER TABLE PriceProject_Ext DROP COLUMN ExtRef_Belarus;
ALTER TABLE PriceProject_Ext DROP COLUMN ExtRef_Czech;
ALTER TABLE PriceProject_Ext DROP COLUMN ExtRef_Hungary;
ALTER TABLE PriceProject_Ext DROP COLUMN ExtRef_Latvia;
ALTER TABLE PriceProject_Ext DROP COLUMN ExtRef_Rf;
ALTER TABLE PriceProject_Ext DROP COLUMN ExtRef_Austria;
ALTER TABLE PriceProject_Ext DROP COLUMN ExtRef_Ukraine;
ALTER TABLE PriceProject_Ext DROP COLUMN ExtRef_Turkey;
ALTER TABLE PriceProject_Ext DROP COLUMN MinRefPrice2016;

GO


