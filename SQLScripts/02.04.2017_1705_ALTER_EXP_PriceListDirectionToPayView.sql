
ALTER VIEW [dbo].[EXP_PriceListDirectionToPayView]
AS
SELECT        Tp.Id, Tp.Number, Tp.NameRu, Tp.NameKz, Tp.NameEn, Tp.PriceRegisterForeign, Tp.PriceRegisterForeignNds, Tp.PriceReRegisterForeign, Tp.PriceReRegisterForeignNds, Tp.PriceRegisterKz, 
                         Tp.PriceRegisterKzNds, Tp.PriceReRegisterKz, Tp.PriceReRegisterKzNds, Tp.Category, Tdp.DirectionToPayId, Tdp.Count, Tdp.Price, Tdp.Total
FROM            dbo.EXP_PriceList AS Tp LEFT OUTER JOIN
                         dbo.EXP_DirectionToPays_PriceList AS Tdp ON Tdp.PriceListId = Tp.Id

GO


