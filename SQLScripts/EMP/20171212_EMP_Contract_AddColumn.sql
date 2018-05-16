alter table EMP_Contract
add ContractScopeId uniqueidentifier null;

go

ALTER TABLE [dbo].[EMP_Contract]  WITH CHECK ADD  CONSTRAINT [FK_EMP_Contract_ContractScopeId_EMP_Ref_ContractScope_Id] FOREIGN KEY([ContractScopeId])
REFERENCES [dbo].[EMP_Ref_ContractScope] ([Id])
GO