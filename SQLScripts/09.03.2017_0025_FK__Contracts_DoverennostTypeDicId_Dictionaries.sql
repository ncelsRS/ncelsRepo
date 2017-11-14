ALTER TABLE [dbo].Contracts  WITH CHECK ADD  CONSTRAINT [FK__Contracts_DoverennostTypeDicId_Dictionaries] FOREIGN KEY(DoverennostTypeDicId)
REFERENCES [dbo].Dictionaries ([Id])
GO
ALTER TABLE [dbo].Contracts CHECK CONSTRAINT [FK__Contracts_DoverennostTypeDicId_Dictionaries]
GO