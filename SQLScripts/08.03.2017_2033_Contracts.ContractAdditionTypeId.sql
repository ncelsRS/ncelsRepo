alter table Contracts
add ContractAdditionTypeId uniqueidentifier
go
ALTER TABLE [dbo].Contracts  WITH CHECK ADD  CONSTRAINT [FK__Contracts_ContractAdditionTypeId_Dictionaries] FOREIGN KEY(ContractAdditionTypeId)
REFERENCES [dbo].Dictionaries ([Id])
GO

ALTER TABLE [dbo].Contracts CHECK CONSTRAINT [FK__Contracts_ContractAdditionTypeId_Dictionaries]
GO

INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide])
     VALUES ('1', N'Соглашение об изменение банковских реквизитов', N'Соглашение об изменение банковских реквизитов', 'ContractAddition',0)
GO

INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide])
     VALUES ('2', N'Соглашение об изменение юридического адреса', N'Соглашение об изменение юридического адреса', 'ContractAddition',0)
GO

INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide])
     VALUES ('3', N'Соглашение о продлении срока действия договора', N'Соглашение о продлении срока действия договора', 'ContractAddition',0)
GO

INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide])
     VALUES ('4', N'Соглашение о смене руководителя', N'Соглашение о смене руководителя', 'ContractAddition',0)
GO