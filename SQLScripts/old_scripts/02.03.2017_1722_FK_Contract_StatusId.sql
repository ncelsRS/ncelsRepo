alter table Contracts
add StatusId uniqueidentifier;

ALTER TABLE [dbo].Contracts  WITH CHECK ADD  CONSTRAINT [FK_Contracts_Dictionaries] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].Contracts CHECK CONSTRAINT [FK_Contracts_Dictionaries]
GO


