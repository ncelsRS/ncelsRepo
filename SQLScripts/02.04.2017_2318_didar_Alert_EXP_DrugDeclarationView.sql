ALTER VIEW [dbo].[EXP_DrugDeclarationView]
AS
SELECT        d.Id, d.TypeId, t.NameRu AS TypeName, d.Number, d.CreatedDate, d.StatusId, s.NameRu AS StausName, t.NameRu, d.OwnerId, d.SendDate, d.ExecuterId, e.DisplayName AS ExecuterName,
                             (SELECT        COUNT(Id) AS Expr1
                               FROM            dbo.EXP_DrugDosage AS dd
                               WHERE        (DrugDeclarationId = d.Id) AND (Id IN
                                                             (SELECT        DrugDosageId
                                                               FROM            dbo.EXP_DrugSubstance AS ds
                                                               WHERE        (IsControl = 'true' or IsPoison='true')))) AS CountDosageIsControl
FROM            dbo.EXP_DrugDeclaration AS d INNER JOIN
                         dbo.EXP_DIC_Type AS t ON t.Id = d.TypeId INNER JOIN
                         dbo.EXP_DIC_Status AS s ON s.Id = d.StatusId LEFT OUTER JOIN
                         dbo.Employees AS e ON e.Id = d.ExecuterId
WHERE        (d.IsDeleted = 'false')


GO


