SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[EXP_MaterialDirectionsView]
AS
SELECT        Tmd.Id, Tmd.CreateDate, Tmd.DeleteDate, Tmd.Number, Tmd.DrugDeclarationId, Tdd.StatusId, Ts.Code AS StatusCode, Ts.Name AS StatusStr, Ts.NameKz AS StatusKzStr, Tmd.SendEmployeeId, Tmd.SendDate, 
                         Tmd.ExecutorEmployeeId, Tmd.ReceiveDate, Tmd.RejectDate, Tmd.Comment, Tdd.Number AS DdNumber, Tdd.CreatedDate AS RegisteredDate, Tdt.NameRu AS RegistrationTypeRu, Tdt.NameKz AS RegistrationTypeKz, 
                         Tdd.NameRu AS TradeNameRu, Tdd.NameKz AS TradeNameKz, Tdd.NameEn AS TradeNameEn, Tdd.Dosage, Tdd.DosageMeasureTypeId, Tdmt.name AS DosageMeasureTypeName, Tdd.ConcentrationRu, Tdd.ConcentrationKz, 
                         Tdf.name AS DrugFormName, Tdf.name_kz AS DrugFormNameKz, Tddt.NameRu AS DrugTypeNameRu, Tddt.NameKz AS DrugTypeNameKz, Tdo.NameRu AS ProducerNameRu, Tdo.NameKz AS ProducerNameKz, 
                         Tdo.NameEn AS ProducerNameEn, Tc.Name AS CountryName, Tc.NameKz AS CountryNameKz
FROM            dbo.EXP_MaterialDirections AS Tmd INNER JOIN
                         dbo.EXP_DrugDeclaration AS Tdd ON Tdd.Id = Tmd.DrugDeclarationId LEFT OUTER JOIN
                         dbo.EXP_DIC_Type AS Tdt ON Tdt.Id = Tdd.TypeId LEFT OUTER JOIN
                         dbo.EXP_DrugType AS Tdrugt ON Tdrugt.DrugDeclarationId = Tdd.Id LEFT OUTER JOIN
                         dbo.EXP_DIC_DrugType AS Tddt ON Tddt.Id = Tdrugt.DrugTypeId INNER JOIN
                         dbo.Dictionaries AS Ts ON Ts.Id = Tmd.StatusId LEFT OUTER JOIN
                         dbo.sr_measures AS Tdmt ON Tdmt.id = Tdd.DosageMeasureTypeId LEFT OUTER JOIN
                         dbo.sr_dosage_forms AS Tdf ON Tdf.id = Tdd.DrugFormId LEFT OUTER JOIN
                         dbo.EXP_DrugOrganizations AS Tdo ON Tdo.DrugDeclarationId = Tdd.Id INNER JOIN
                         dbo.Dictionaries AS TorgType ON TorgType.Id = Tdo.OrgManufactureTypeDicId AND TorgType.Code = '1' LEFT OUTER JOIN
                         dbo.Dictionaries AS Tc ON Tc.Id = Tdo.CountryDicId

GO


