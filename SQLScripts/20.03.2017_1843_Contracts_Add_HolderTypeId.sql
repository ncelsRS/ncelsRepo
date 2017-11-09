INSERT INTO [dbo].[Dictionaries]
           ([Code], [Name], [Type], [DisplayName], [NameKz], [IsGuide])
     VALUES ('holder', N'Держателем регистрационного удостоверения', 'ContractHolderType', N'Держателем регистрационного удостоверения', N'Держателем регистрационного удостоверения', 0)
GO
INSERT INTO [dbo].[Dictionaries]
           ([Code], [Name], [Type], [DisplayName], [NameKz], [IsGuide])
     VALUES ('producer', N'Производителем', 'ContractHolderType', N'Производителем', N'Производителем', 0)
GO
INSERT INTO [dbo].[Dictionaries]
           ([Code], [Name], [Type], [DisplayName], [NameKz], [IsGuide])
     VALUES ('applicant', N'Заявителем', 'ContractHolderType', N'Заявителем', N'Заявителем', 0)
GO
alter table Contracts
add HolderTypeId uniqueidentifier
go
ALTER TABLE [dbo].Contracts  WITH CHECK ADD  CONSTRAINT [FK_ContractsHolderTypeId_DictionariesId] FOREIGN KEY(HolderTypeId)
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].Contracts CHECK CONSTRAINT [FK_ContractsHolderTypeId_DictionariesId]
GO

update Contracts set HolderTypeId=(select id from Dictionaries where Type='ContractHolderType' and Code='producer')
where WithManufacturer=1
go

update Contracts set HolderTypeId=(select id from Dictionaries where Type='ContractHolderType' and Code='holder')
where WithManufacturer=0
go
alter table Contracts
drop column WithManufacturer
go

