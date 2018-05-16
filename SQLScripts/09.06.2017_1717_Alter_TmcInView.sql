SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[TmcInView]
AS
SELECT
  ti.Id,
  ti.CreatedDate,
  ti.CreatedEmployeeId,
  CASE
    WHEN DATEADD(DAY, 15, ti.CreatedDate) < GETDATE() AND
      ti.StateType < 2 THEN -1
    ELSE ti.StateType
  END AS StateType,
  CASE
    WHEN (DATEADD(DAY, 15, ti.CreatedDate)
      < GETDATE() AND
      ti.StateType < 2) OR
      (ti.StateType = -1) THEN N'Отклонена'
    WHEN ti.StateType = 0 THEN N'Новая'
    WHEN ti.StateType = 1 THEN N'Отпавлена в 1С'
    WHEN ti.StateType = 3 THEN N'Закрыта'
    WHEN ti.StateType = 4 THEN N'Аннулирована'
    WHEN ti.StateType = 2 OR (Tla.PowerOfAttorneyNumber_1C IS NOT NULL) THEN N'Получена из 1С'
	WHEN ti.StateType = 10 THEN N'Согласование ИЦ'
    WHEN ti.StateType = 11 THEN N'Согласование Бухгалтерия'
    WHEN ti.StateType = 12 THEN N'Согласование Руководство'
  END AS StateTypeValue,
  ti.OwnerEmployeeId,
  e.DisplayName AS OwnerEmployeeValue,
  ti.Provider,
  ti.ProviderBin,
  ti.ContractNumber,
  ti.ContractDate,
  ti.LastDeliveryDate,
  ti.IsFullDelivery,
  CASE
    WHEN IsFullDelivery = 0 THEN N'Нет'
    WHEN IsFullDelivery = 1 THEN N'Да'
  END AS IsFullDeliveryValue,
  ISNULL(ti.PowerOfAttorney, Tla.PowerOfAttorneyNumber_1C) AS PowerOfAttorney,
  e1.DisplayName AS ExecutorEmployeeValue,
  e2.DisplayName AS AgreementEmployeeValue,
  ti.ExecutorEmployeeId,
  ti.AgreementEmployeeId,
  ti.IsScan,
  CASE
    WHEN IsScan = 0 THEN N'Нет'
    WHEN IsScan = 1 THEN N'Да'
  END AS IsScanValue,
  DATEDIFF(DAY, ti.CreatedDate, GETDATE()) AS Func1,
  ti.AccountantEmployeeId,
  e3.DisplayName AS AccountantEmployeeValue
FROM dbo.TmcIns AS ti
LEFT OUTER JOIN dbo.Employees AS e  ON e.Id = ti.OwnerEmployeeId
LEFT OUTER JOIN dbo.Employees AS e1  ON e1.Id = ti.ExecutorEmployeeId
LEFT OUTER JOIN dbo.Employees AS e2  ON e2.Id = ti.AgreementEmployeeId
LEFT OUTER JOIN dbo.Employees AS e3  ON e3.Id = ti.AccountantEmployeeId
LEFT OUTER JOIN dbo.I1c_lims_Applications AS Tla ON Tla.Number = Ti.Id

GO


