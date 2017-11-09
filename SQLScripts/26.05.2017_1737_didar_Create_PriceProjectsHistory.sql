ALTER TABLE [dbo].[PriceProjects]
ADD [IsSended] [bit] NOT NULL default(0)
GO
ALTER TABLE [dbo].[PriceProjects]
ADD [IsSigned] [bit] NOT NULL default(0)
GO

CREATE TABLE [dbo].[PriceProjectsHistory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[StatusId] [int] NOT NULL,
	[Note] [nvarchar](max) NULL,
	[PriceProjectId] [uniqueidentifier] NOT NULL,
	[XmlSign] [nvarchar](max) NULL,
 CONSTRAINT [PK_EXP_PriceProjectsHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[PriceProjectsHistory]  WITH CHECK ADD  CONSTRAINT [FK_EXP_PriceProjectsHistory_Employees] FOREIGN KEY([UserId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[PriceProjectsHistory] CHECK CONSTRAINT [FK_EXP_PriceProjectsHistory_Employees]
GO

ALTER TABLE [dbo].[PriceProjectsHistory]  WITH CHECK ADD  CONSTRAINT [FK_EXP_PriceProjectsHistory_EXP_PriceProjects] FOREIGN KEY([PriceProjectId])
REFERENCES [dbo].[PriceProjects] ([Id])
GO

ALTER TABLE [dbo].[PriceProjectsHistory] CHECK CONSTRAINT [FK_EXP_PriceProjectsHistory_EXP_PriceProjects]
GO

ALTER VIEW [dbo].[PriceProjectJournal]
AS
SELECT        pp.Id, pp.Type, (CASE WHEN doc.Number IS NOT NULL THEN doc.Number ELSE 'б/н' END) AS Number, (CASE WHEN doc.OutgoingNumber IS NOT NULL THEN doc.OutgoingNumber ELSE 'б/н' END) 
                         AS OutgoingNumber, pp.CreatedDate, pp.Status, pp.OwnerId, pp.ManufacturerOrganizationId, pp.HolderOrganizationId, pp.ProxyOrganizationId, pp.DoverennostNumber, pp.DoverennostCreatedDate, 
                         pp.DoverennostExpiryDate, pp.Filial, pp.NameKz, pp.NameRu, pp.RegNumber, pp.RegDate, pp.LsTypeDicId, pp.NameOriginal, pp.MnnRu, pp.MnnEn, pp.FormNameKz, pp.FormNameRu, pp.Dosage, 
                         pp.CountPackage, pp.Concentration, pp.CodeAtx, pp.IntroducingMethodDicId, pp.IsConvention, pp.ImnSecuryTypeDicId, pp.RePriceDicId, pp.ResultTypeDicId, pp.IsPayed, pp.PayDate, pp.StartDate, pp.ContrDate, 
                         pp.ConclusionDate, pp.IsStageExpired, pp.ExpiredDayCount, pp.ExpertAz, pp.OutgoingDoc, pp.DayCount, pp.IsNewManufacrurer, mo.NameRu AS ManufacturerOrgName, po.NameRu AS ApplicantOrgName, 
                         d1.Name AS CountryName, pp.ListTypeDicId, pp.MnnOrderNumber, d2.Name AS RePriceName, 
                         (CASE WHEN rl.Type = 1 THEN 'ЕД' WHEN rl.Type = 2 THEN 'АЛО' WHEN rl.Type = 3 THEN 'КНФ' WHEN rl.Type = 4 THEN 'Другие' END) AS ListTypeName, d_t.Name AS TypeValue, d_s.Name AS StatusValue, 
                         pp.PriceProjectId, (CASE WHEN doc.CountDay IS NOT NULL THEN doc.CountDay ELSE DATEDIFF(day, GETDATE(), doc.CompareConterDate + 30) END) AS LeftDays, doc.ReturnDate, 
                         (CASE WHEN doc.CountDay IS NOT NULL THEN DATEDIFF(day, doc.ReturnDate, GETDATE()) ELSE NULL END) AS PassedDays, pp.IsSigned
FROM            dbo.PriceProjects AS pp LEFT OUTER JOIN
                         dbo.Organizations AS mo ON mo.Id = pp.ManufacturerOrganizationId LEFT OUTER JOIN
                         dbo.Organizations AS po ON po.Id = pp.ProxyOrganizationId LEFT OUTER JOIN
                         dbo.Dictionaries AS d1 ON d1.Id = mo.CountryDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d2 ON d2.Id = pp.RePriceDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d3 ON d3.Id = pp.ListTypeDicId LEFT OUTER JOIN
                         dbo.Dictionaries AS d_t ON d_t.Type = 'PriceProjectType' AND d_t.Code = CAST(pp.Type AS nvarchar(MAX)) LEFT OUTER JOIN
                         dbo.Dictionaries AS d_s ON d_s.Type = 'PriceProjectStatus' AND d_s.Code = CAST(pp.Status AS nvarchar(MAX)) LEFT OUTER JOIN
                         dbo.Documents AS doc ON doc.Id = pp.Id LEFT OUTER JOIN
                         dbo.RequestList AS rl ON rl.RegNumber = pp.RegNumber
WHERE        (pp.IsArchive = 0)

GO
