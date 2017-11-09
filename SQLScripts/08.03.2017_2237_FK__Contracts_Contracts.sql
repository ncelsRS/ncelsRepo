ALTER TABLE [dbo].Contracts  WITH CHECK ADD  CONSTRAINT [FK__Contracts_Contracts] FOREIGN KEY(ContractId)
REFERENCES [dbo].Contracts ([Id])
GO
ALTER TABLE [dbo].Contracts CHECK CONSTRAINT [FK__Contracts_Contracts]
GO