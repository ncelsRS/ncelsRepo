SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ContractsView]
AS
SELECT        c.Id, c.ManufacturerOrganizationId, c.ApplicantOrganizationId, c.DoverennostTypeDicId, c.DoverennostNumber, c.DoverennostCreatedDate, c.DoverennostExpiryDate, c.HolderOrganizationId, 
                         c.PayerOrganizationId, c.PayerTranslationOrganizationId, c.Number, c.Status, c.CreatedDate, c.ContractId, c.Type, c.ContractDate, c.StartDate, c.EndDate, c.IsExpired, c.IsSite, c.IsHasDoverennostNumber, mo.NameRu AS ManufactureOrgName, 
                         ao.NameRu AS ApplicantOrgName, ho.NameRu AS HolderOrgName, po.NameRu AS PayerOrgName, pt.NameRu AS PayerTranslationOrgName, d.Name AS DoverenostName, d1.Name AS StatusName, c.OwnerId,
						 c.ContractAdditionTypeId, ad.Name as ContractAdditionTypeName
FROM            dbo.Contracts AS c LEFT OUTER JOIN
                         dbo.Organizations AS mo ON mo.Id = c.ManufacturerOrganizationId LEFT OUTER JOIN
                         dbo.Organizations AS ao ON ao.Id = c.ApplicantOrganizationId LEFT OUTER JOIN
                         dbo.Organizations AS ho ON ho.Id = c.HolderOrganizationId LEFT OUTER JOIN
                         dbo.Organizations AS po ON po.Id = c.PayerOrganizationId LEFT OUTER JOIN
						 dbo.Organizations AS pt ON pt.Id = c.PayerTranslationOrganizationId LEFT OUTER JOIN
                         dbo.Dictionaries AS d ON d.Id = c.DoverennostTypeDicId LEFT OUTER JOIN
						 dbo.Dictionaries AS ad ON ad.Id = c.ContractAdditionTypeId LEFT OUTER JOIN
                         dbo.Dictionaries AS d1 ON d1.Type = 'ContractStatus' AND d1.Code = c.Status


GO


