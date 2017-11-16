ALTER VIEW [dbo].[OrganizationsView]
AS
SELECT        o.Id, o.Type, o.NameKz, o.NameRu, o.NameEn, o.CountryDicId, o.AddressLegal, o.AddressFact, o.Phone, o.Fax, o.Email, o.BossFio, o.BossPosition, o.ContactFio, o.ContactPosition, o.ContactPhone, o.ContactFax,
                          o.ContactEmail, o.OrgManufactureTypeDicId, o.DocNumber, o.DocDate, o.DocExpiryDate, o.ObjectId, o.OpfTypeDicId, o.BankName, o.BankIik, o.BankCurencyDicId, o.BankSwift, o.Bin, o.IsResident, 
                         o.PayerTypeDicId, dc.Name AS CountryName, dm.Name AS OrgManufactureTypeName, dopf.Name AS OpfTypeName, dbn.Name AS BankCurencyDicName, dpt.Name AS PayerTypeName, o.BossLastName, 
                         o.BossFirstName, o.BossMiddleName, o.PaymentBill, o.Iin, o.BankBik, o.OriginalOrgId
FROM            dbo.Organizations AS o LEFT OUTER JOIN
                         dbo.Dictionaries AS dc ON dc.Id = o.CountryDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS dm ON dm.Id = o.OrgManufactureTypeDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS dopf ON dopf.Id = o.OpfTypeDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS dbn ON dbn.Id = o.BankCurencyDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS dpt ON dpt.Id = o.PayerTypeDicId


GO


