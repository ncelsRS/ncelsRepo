alter table Contracts
add PayerTranslationOrganizationId uniqueidentifier;

ALTER TABLE [dbo].Contracts  WITH CHECK ADD  CONSTRAINT [FK__Contracts_PayerTranslation_Organizations] FOREIGN KEY(PayerTranslationOrganizationId)
REFERENCES [dbo].Organizations ([Id])
GO

ALTER TABLE [dbo].Contracts CHECK CONSTRAINT [FK__Contracts_PayerTranslation_Organizations]
GO

alter table Contracts
add OwnerId uniqueidentifier
go

update Contracts set Ownerid=cast(cast(0 as binary) as uniqueidentifier)
go

alter table Contracts
alter column OwnerId uniqueidentifier not null