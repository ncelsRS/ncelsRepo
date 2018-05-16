CREATE VIEW [dbo].[ContractAdditionJournal]
AS
SELECT        ca.Id, c.ManufacturerOrganizationId, c.ApplicantOrganizationId, c.DoverennostTypeDicId, c.DoverennostNumber, c.DoverennostCreatedDate,
 c.DoverennostExpiryDate, c.HolderOrganizationId, c.PayerOrganizationId, (CASE WHEN ca.Number IS NOT NULL THEN ca.Number ELSE 'á/í' END) as Number,
 c.Number as ContractNumber, ca.CreatedDate, ca.ContractId, ca.ContractDate,
 c.StartDate, c.EndDate, c.IsExpired, c.IsSite, mo.NameRu AS ManufacturerName, d1.Name AS ManufacturerCountry, d2.Name AS ApplicantCountry,
 d3.Name AS ApplicantCurrency, e.Login, d4.Name AS DocumentTypeName, d5.Name AS StatusName, d5.Code as StatusCode, ho.NameRu AS HolderName, ao.NameRu AS ApplicantName,
 po.NameRu AS PayerName, doc.CorrespondentsId, doc.CorrespondentsValue, doc.ExecutorsId as ExecutorId,
                             (SELECT        COUNT(*) AS Expr1
                               FROM            dbo.EXP_DrugDeclaration AS r
                               WHERE        (ContractId = ca.ContractId)) AS CountApplications,
							   cat.Name as ContractAdditionType
FROM            dbo.Contracts AS ca INNER JOIN
						 dbo.Contracts AS c ON c.Id = ca.ContractId LEFT OUTER JOIN
                         dbo.Employees AS e ON e.Id = ca.OwnerId LEFT OUTER JOIN
                         dbo.Organizations AS mo ON mo.Id = c.ManufacturerOrganizationId LEFT OUTER JOIN
                         dbo.Organizations AS ho ON ho.Id = c.HolderOrganizationId LEFT OUTER JOIN
                         dbo.Organizations AS po ON po.Id = c.PayerOrganizationId LEFT OUTER JOIN
                         dbo.Organizations AS ao ON ao.Id = c.ApplicantOrganizationId LEFT OUTER JOIN
                         dbo.Dictionaries AS d1 ON d1.Id = mo.CountryDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d2 ON d2.Id = ao.CountryDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d3 ON d3.Id = ao.BankCurencyDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d4 ON d4.Id = c.DoverennostTypeDicId LEFT OUTER JOIN
						 dbo.Dictionaries AS cat ON cat.Id = ca.ContractAdditionTypeId LEFT OUTER JOIN
                         dbo.Documents AS doc ON doc.Id = ca.Id LEFT OUTER JOIN
                         dbo.Dictionaries AS d5 ON d5.Id=ca.StatusId



GO


