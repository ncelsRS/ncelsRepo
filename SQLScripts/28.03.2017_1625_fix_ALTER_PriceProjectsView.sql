USE [ncels]
GO

/****** Object:  View [dbo].[PriceProjectsView]    Script Date: 26.03.2017 12:50:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Скрипт для команды SelectTopNRows из среды SSMS  ******/
ALTER VIEW [dbo].[PriceProjectsView]
AS
SELECT        
	pp.Id, 
	pp.Type, 
	pp.Number, 
	pp.CreatedDate, 
	pp.Status, 
	pp.OwnerId, 
	pp.ManufacturerOrganizationId, 
	pp.HolderOrganizationId, 
	pp.ProxyOrganizationId, 
	pp.DoverennostNumber, 
	pp.DoverennostCreatedDate, 
	pp.DoverennostExpiryDate, 
	pp.Filial, 
	pp.NameKz, 
	pp.NameRu, 
	pp.RegNumber, 
	pp.RegDate, 
	pp.LsTypeDicId, 
	pp.NameOriginal, 
	pp.MnnRu, 
	pp.MnnEn, 
	pp.FormNameKz, 
	pp.FormNameRu, 
	pp.Dosage, 
	pp.CountPackage, 
	pp.Concentration, 
	pp.CodeAtx, 
	pp.IntroducingMethodDicId, 
	pp.IsConvention, 
	pp.ImnSecuryTypeDicId, 
	pp.RePriceDicId, 
	e.LastName AS OwnerLastName, 
	e.FirstName AS OwnerFirstName, 
	e.MiddleName AS OwnerMiddleName, 
	mo.NameRu AS ManufacturerOrganizationName, 
	ho.NameRu AS HolderOrganizationName, 
	po.NameRu AS ProxyOrganizationName, 
	d1.Name AS LsTypeName, 
	d2.Name AS IntroducingMethodName, 
	d3.Name AS ImnSecuryTypeName, 
	d4.Name AS RePriceName, 
	pp.PriceProjectId,
	pp.RegisterId,
	pp.RegisterDfId, 
	pp.Volume
FROM
	dbo.PriceProjects AS pp 
	LEFT OUTER JOIN dbo.Employees AS e ON e.Id = pp.OwnerId 
	LEFT OUTER JOIN dbo.Organizations AS mo ON mo.Id = pp.ManufacturerOrganizationId 
	LEFT OUTER JOIN dbo.Organizations AS ho ON ho.Id = pp.HolderOrganizationId 
	LEFT OUTER JOIN dbo.Organizations AS po ON po.Id = pp.ProxyOrganizationId 
	LEFT OUTER JOIN dbo.Dictionaries AS d1 ON d1.Id = pp.LsTypeDicId 
	LEFT OUTER JOIN dbo.Dictionaries AS d2 ON d2.Id = pp.IntroducingMethodDicId 
	LEFT OUTER JOIN dbo.Dictionaries AS d3 ON d3.Id = pp.ImnSecuryTypeDicId 
	LEFT OUTER JOIN dbo.Dictionaries AS d4 ON d4.Id = pp.RePriceDicId


GO
