ALTER TABLE [dbo].[Prices]
ADD [IsIncluded] [bit] NOT NULL default(0)
GO
ALTER VIEW [dbo].[PricesView]
AS
SELECT        p.Id, p.Type, p.Name, p.PriceProjectId, p.CountryId, p.ManufacturerPrice, p.ManufacturerPriceCurrencyDicId, p.ManufacturerPriceNote, p.LimitPrice, p.LimitPriceCurrencyDicId, p.LimitPriceNote, p.AvgOptPrice, 
                         p.AvgOptPriceCurrencyDicId, p.AvgOptPriceNote, p.AvgRozPrice, p.AvgRozPriceCurrencyDicId, p.AvgRozPriceNote, p.CipPrice, p.CipPriceCurrencyDicId, p.RefPriceTypeDicId, p.RefPrice, p.RefPriceCurrencyDicId, 
                         p.OwnerPrice, p.OwnerPriceCurrencyDicId, p.BritishPrice, d1.Name AS CountryName, d2.Name AS ManufacturerPriceCurrencyName, d3.Name AS LimitPriceCurrencyName, d4.Name AS AvgOptPriceCurrencyName,
                          d5.Name AS AvgRozPriceCurrencyName, d6.Name AS CipPriceCurrencyName, d7.Name AS RefPriceCurrencyName, d8.Name AS RefPriceTypeName, d9.Name AS OwnerPriceCurrencyName, p.UnitPrice, 
                         p.UnitPriceCurrencyDicId, d10.Name AS UnitPriceCurrencyName, p.CreatedDate, p.BritishCost, p.MtPartsId, sr_mt_p.name AS PartsName, p.IsIncluded
FROM            dbo.Prices AS p LEFT OUTER JOIN
                         dbo.Dictionaries AS d1 ON d1.Id = p.CountryId LEFT OUTER JOIN
                         dbo.Dictionaries AS d2 ON d2.Id = p.ManufacturerPriceCurrencyDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d3 ON d3.Id = p.LimitPriceCurrencyDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d4 ON d4.Id = p.AvgOptPriceCurrencyDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d5 ON d5.Id = p.AvgRozPriceCurrencyDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d6 ON d6.Id = p.CipPriceCurrencyDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d7 ON d7.Id = p.RefPriceCurrencyDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d8 ON d8.Id = p.RefPriceTypeDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d9 ON d9.Id = p.OwnerPriceCurrencyDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d10 ON d10.Id = p.UnitPriceCurrencyDicId LEFT OUTER JOIN
                         dbo.sr_register_mt_parts AS sr_mt_p ON sr_mt_p.id = p.MtPartsId

GO


