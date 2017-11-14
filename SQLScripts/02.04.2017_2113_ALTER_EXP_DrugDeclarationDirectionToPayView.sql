ALTER VIEW [dbo].[EXP_DrugDeclarationDirectionToPayView]
AS
SELECT DISTINCT 
                         Td.Id, Td.Number, Td.CreatedDate, Td.TypeId, Tt.NameRu AS TypeNameRu, Tt.NameKz AS TypeNameKz, Td.NameRu, Td.NameKz, Td.NameEn, Tsdf.full_name AS DrugFormRu, Tsdf.full_name_kz AS DrugFormKz,
                          Td.StatusId, s.NameRu AS StausName, Td.OwnerId, Td.ContractId, Td.ExecuterId, Tdd.DirectionToPayId
FROM            dbo.EXP_DrugDeclaration AS Td INNER JOIN
                         dbo.EXP_DIC_Type AS Tt ON Tt.Id = Td.TypeId INNER JOIN
                         dbo.EXP_DIC_Status AS s ON s.Id = Td.StatusId LEFT OUTER JOIN
                         dbo.sr_drug_forms AS Tsdf ON Tsdf.id = Td.DrugFormId LEFT OUTER JOIN
                         dbo.EXP_DirectionToPays_DrugDeclaration AS Tdd ON Tdd.DrugDeclarationId = Td.Id

GO


