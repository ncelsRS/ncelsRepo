
ALTER VIEW [dbo].[EXP_DirectionToPaysView]
AS
SELECT        Tdp.[Id], Tdp.[Number], Tdp.[DirectionDate] AS [DirectionDate], STUFF
                             ((SELECT DISTINCT '; ' + Tt.Number
                                 FROM            [dbo].[EXP_DrugDeclaration] Tt INNER JOIN
                                                          [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DrugDeclarationId = Tt.Id
                                 WHERE        Td.DirectionToPayId = Tdp.Id FOR XML PATH('')), 1, 2, '') AS ApplicationNumbers, STUFF
                             ((SELECT DISTINCT '; ' + CONVERT(NVARCHAR(12), Tt.CreatedDate, 104)
                                 FROM            [dbo].[EXP_DrugDeclaration] Tt INNER JOIN
                                                          [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DrugDeclarationId = Tt.Id
                                 WHERE        Td.DirectionToPayId = Tdp.Id FOR XML PATH('')), 1, 2, '') AS ApplicationDates, STUFF
                             ((SELECT DISTINCT '; ' + Tdt.NameRu
                                 FROM            [dbo].[EXP_DrugDeclaration] Tt INNER JOIN
                                                          [dbo].[EXP_DIC_Type] AS Tdt ON Tdt.Id = Tt.TypeId INNER JOIN
                                                          [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DrugDeclarationId = Tt.Id
                                 WHERE        Td.DirectionToPayId = Tdp.Id FOR XML PATH('')), 1, 2, '') AS RegistrationForm, STUFF
                             ((SELECT DISTINCT '; ' + Tt.NameKz
                                 FROM            [dbo].[EXP_DrugDeclaration] Tt INNER JOIN
                                                          [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DrugDeclarationId = Tt.Id
                                 WHERE        Td.DirectionToPayId = Tdp.Id FOR XML PATH('')), 1, 2, '') AS TradeNameKz, STUFF
                             ((SELECT DISTINCT '; ' + Tt.NameRu
                                 FROM            [dbo].[EXP_DrugDeclaration] Tt INNER JOIN
                                                          [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DrugDeclarationId = Tt.Id
                                 WHERE        Td.DirectionToPayId = Tdp.Id FOR XML PATH('')), 1, 2, '') AS TradeNameRu, STUFF
                             ((SELECT DISTINCT '; ' + Tt.NameEn
                                 FROM            [dbo].[EXP_DrugDeclaration] Tt INNER JOIN
                                                          [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DrugDeclarationId = Tt.Id
                                 WHERE        Td.DirectionToPayId = Tdp.Id FOR XML PATH('')), 1, 2, '') AS TradeNameEn, STUFF
                             ((SELECT DISTINCT '; ' + Tt.DrugFormKz
                                 FROM            [dbo].[EXP_DrugDeclaration] Tt INNER JOIN
                                                          [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DrugDeclarationId = Tt.Id
                                 WHERE        Td.DirectionToPayId = Tdp.Id FOR XML PATH('')), 1, 2, '') AS DrugFormKz, STUFF
                             ((SELECT DISTINCT '; ' + Tt.DrugFormRu
                                 FROM            [dbo].[EXP_DrugDeclaration] Tt INNER JOIN
                                                          [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DrugDeclarationId = Tt.Id
                                 WHERE        Td.DirectionToPayId = Tdp.Id FOR XML PATH('')), 1, 2, '') AS DrugFormRu, '' AS DrugFormEn, '' AS ManufactureKz, '' AS ManufactureRu, '' AS ManufactureEn, '' AS ExpertDisplayName, 
                         CAST(0 AS decimal(18, 2)) AS Price, [TotalPrice] AS [TotalPrice], '' AS [Currency], STUFF
                             ((SELECT DISTINCT '; ' + Tt.NameKz
                                 FROM            [dbo].[EXP_PriceList] Tt INNER JOIN
                                                          [dbo].[EXP_DirectionToPays_PriceList] AS Td ON Td.PriceListId = Tt.Id
                                 WHERE        Td.DirectionToPayId = Tdp.Id FOR XML PATH('')), 1, 2, '') AS PriceListNameKz, STUFF
                             ((SELECT DISTINCT '; ' + Tt.NameRu
                                 FROM            [dbo].[EXP_PriceList] Tt INNER JOIN
                                                          [dbo].[EXP_DirectionToPays_PriceList] AS Td ON Td.PriceListId = Tt.Id
                                 WHERE        Td.DirectionToPayId = Tdp.Id FOR XML PATH('')), 1, 2, '') AS PriceListNameRu, STUFF
                             ((SELECT DISTINCT '; ' + Tt.NameEn
                                 FROM            [dbo].[EXP_PriceList] Tt INNER JOIN
                                                          [dbo].[EXP_DirectionToPays_PriceList] AS Td ON Td.PriceListId = Tt.Id
                                 WHERE        Td.DirectionToPayId = Tdp.Id FOR XML PATH('')), 1, 2, '') AS PriceListNameEn, '' AS PriceListValue, [InvoiceNumber1C], [InvoiceDatetime1C], [PaymentDatetime1C], [PaymentValue1C], 
                         [PaymentComment1C], Tdp.[StatusId], Ts .[Code] AS StatusCode, Ts .[Name] AS StatusName, Ts .[NameKz] AS StatusNameKz, Tdp.CreateEmployeeId, Ta.TaskExecutorId AS ExecutorId, 
                         Ta.TaskExecutorValue AS ExecutorName, Ta.TaskComment AS Comment 
FROM            [dbo].[EXP_DirectionToPays] AS Tdp INNER JOIN
                         [dbo].[Dictionaries] AS Ts ON Ts .Id = Tdp.StatusId LEFT OUTER JOIN
                         [dbo].[EXP_ActivityTaskActualView] AS Ta ON Ta.[DocumentId] = Tdp.Id
WHERE        Tdp.DeleteDate IS NULL
GROUP BY Tdp.Id, Tdp.[Number], Tdp.[DirectionDate], Tdp.[TotalPrice], Tdp.[InvoiceNumber1C], Tdp.[InvoiceDatetime1C], Tdp.[PaymentDatetime1C], Tdp.[PaymentValue1C], Tdp.[PaymentComment1C], Tdp.[StatusId], 
                         Ts .[Name], Ts .[NameKz], Ts .[Code], Tdp.CreateEmployeeId, Ta.TaskExecutorId, Ta.TaskExecutorValue, Ta.TaskComment

GO


