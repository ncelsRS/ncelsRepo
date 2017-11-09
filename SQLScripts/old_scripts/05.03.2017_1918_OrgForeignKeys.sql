ALTER TABLE [dbo].Contracts  WITH CHECK ADD  CONSTRAINT [FK__Contracts_ApplicantOrganization_Organizations] FOREIGN KEY(ApplicantOrganizationId)
REFERENCES [dbo].Organizations ([Id])
GO

ALTER TABLE [dbo].Contracts CHECK CONSTRAINT [FK__Contracts_ApplicantOrganization_Organizations]
GO

ALTER TABLE [dbo].Contracts  WITH CHECK ADD  CONSTRAINT [FK__Contracts_HolderOrganization_Organizations] FOREIGN KEY(HolderOrganizationId)
REFERENCES [dbo].Organizations ([Id])
GO

ALTER TABLE [dbo].Contracts CHECK CONSTRAINT [FK__Contracts_HolderOrganization_Organizations]
GO

ALTER TABLE [dbo].Contracts  WITH CHECK ADD  CONSTRAINT [FK__Contracts_PayerOrganization_Organizations] FOREIGN KEY(PayerOrganizationId)
REFERENCES [dbo].Organizations ([Id])
GO

ALTER TABLE [dbo].Contracts CHECK CONSTRAINT [FK__Contracts_PayerOrganization_Organizations]
GO