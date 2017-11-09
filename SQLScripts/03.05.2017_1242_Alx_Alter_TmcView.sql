SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[TmcView]
AS
SELECT        CASE WHEN
                             (SELECT        COUNT(*)
                               FROM            TmcOutCounts
                               WHERE        TmcOutCounts.TmcId = t .Id AND StateType = 1) = 0 THEN t .CountConvert WHEN
                             (SELECT        COUNT(*)
                               FROM            TmcOutCounts
                               WHERE        TmcOutCounts.TmcId = t .Id AND StateType = 1) > 0 THEN t .CountConvert -
                             (SELECT        SUM(CountFact)
                               FROM            TmcOutCounts
                               WHERE        TmcOutCounts.TmcId = t .Id AND StateType = 1) END AS CountActual, t.Id, t.CreatedDate, t.CreatedEmployeeId, t.StateType, 
                         (CASE WHEN t .StateType = 0 THEN N'Новый' WHEN t .StateType = 2 THEN N'Списанный' ELSE N'Принятый' END) AS StateTypeValue, t.TmcInId, t.Number, t.Name, t.Code, t.Manufacturer, t.Serial, t.Count, t.MeasureTypeDicId, 
                         d1.Name AS MeasureTypeDicValue, t.CountFact, t.CountConvert, t.MeasureTypeConvertDicId, d2.Name AS MeasureTypeConvertDicValue, t.ManufactureDate, t.ExpiryDate, t.PackageDicId, d3.Name AS PackageDicValue, 
                         t.TmcTypeDicId, d4.Name AS TmcTypeDicValue, t.StorageDicId, d5.Name AS StorageDicValue, t.Safe, t.Rack, t.OwnerEmployeeId, t.ReceivingDate, e.DisplayName AS OwnerEmployeeValue, Tused.UsedCount
FROM            dbo.Tmcs AS t LEFT OUTER JOIN
                         dbo.Employees AS e ON e.Id = t.OwnerEmployeeId LEFT OUTER JOIN
                         dbo.Dictionaries AS d1 ON d1.Id = t.MeasureTypeDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d2 ON d2.Id = t.MeasureTypeConvertDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d3 ON d3.Id = t.PackageDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d4 ON d4.Id = t.TmcTypeDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d5 ON d5.Id = t.StorageDicId LEFT OUTER JOIN
                             (SELECT        Ttoc.TmcId, SUM(Tto.Count) AS UsedCount
                               FROM            dbo.TmcOffs AS Tto INNER JOIN
                                                         dbo.TmcOutCounts AS Ttoc ON Ttoc.Id = Tto.TmcOutId
                               GROUP BY Ttoc.TmcId) AS Tused ON Tused.TmcId = t.Id

GO


