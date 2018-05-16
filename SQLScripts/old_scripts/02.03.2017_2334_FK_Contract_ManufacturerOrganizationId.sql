ALTER TABLE [dbo].Contracts  WITH CHECK ADD  CONSTRAINT [FK_Contracts_Organizations] FOREIGN KEY(ManufacturerOrganizationId)
REFERENCES [dbo].Organizations ([Id])
GO

ALTER TABLE [dbo].Contracts CHECK CONSTRAINT [FK_Contracts_Organizations]
GO