ALTER TABLE [dbo].[EMP_ContractHistory]  WITH CHECK ADD  CONSTRAINT [FK_EMP_ContractHistory_ContractId_EMP_Contract_Id] FOREIGN KEY([ContractId])
REFERENCES [dbo].[EMP_Contract] ([Id])
GO

ALTER TABLE [dbo].[EMP_ContractHistory] CHECK CONSTRAINT [FK_EMP_ContractHistory_ContractId_EMP_Contract_Id]
GO