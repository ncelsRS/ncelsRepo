SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Скрипт для команды SelectTopNRows из среды SSMS  ******/
ALTER VIEW [dbo].[TmcOutCountView]
AS
SELECT        CASE WHEN
                             (SELECT        COUNT(*)
                               FROM            TmcOffs
                               WHERE        TmcOffs.TmcOutId = toc.Id) = 0 THEN toc.CountFact WHEN
                             (SELECT        COUNT(*)
                               FROM            TmcOffs
                               WHERE        TmcOffs.TmcOutId = toc.Id) > 0 THEN toc.CountFact -
                             (SELECT        SUM([Count])
                               FROM            TmcOffs
                               WHERE        TmcOffs.TmcOutId = toc.Id) END AS CountActual, toc.Id, toc.TmcOutId, toc.TmcId, t.Name, t.MeasureTypeConvertDicId, d1.Name AS MeasureTypeConvertDicValue, toc.Count, toc.CountFact, 
                         toc.StateType, 'Статус' AS StateTypeValue, toc.Note, tout.OwnerEmployeeValue, tout.StorageDicValue, tout.Safe, tout.Rack, t.CreatedDate, tout.CreatedEmployeeId, t.StateType AS TmcStateType, t.TmcInId, 
                         t.Number, t.Code, t.Manufacturer, t.Serial, t.Count AS TmcCount, t.MeasureTypeDicId, t.CountFact AS TmcCountFact, t.CountConvert, t.MeasureTypeConvertDicId AS TmcMeasureTypeConvertDicId, 
                         t.ManufactureDate, t.ExpiryDate, t.PackageDicId, t.TmcTypeDicId, t.StorageDicId, t.Safe AS TmcSafe, t.Rack AS TmcRack, t.OwnerEmployeeId, d2.Name AS MeasureTypeDicValue, d3.Name AS PackageDicValue, 
                         d4.Name AS TmcTypeDicValue, e.DisplayName AS OwnerEmployeeName, tout.StateType AS ApplicationStateType
FROM            dbo.TmcOutCounts AS toc LEFT OUTER JOIN
                         dbo.Tmcs AS t ON t.Id = toc.TmcId LEFT OUTER JOIN
                         dbo.TmcOutView AS tout ON tout.Id = toc.TmcOutId LEFT OUTER JOIN
                         dbo.Employees AS e ON e.Id = t.OwnerEmployeeId LEFT OUTER JOIN
                         dbo.Dictionaries AS d1 ON d1.Id = t.MeasureTypeConvertDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d2 ON d2.Id = t.MeasureTypeDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d3 ON d3.Id = t.PackageDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d4 ON d4.Id = t.TmcTypeDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d5 ON d5.Id = t.StorageDicId

GO


