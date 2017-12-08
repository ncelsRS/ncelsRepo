alter table EMP_Contract
add ContractStatusId uniqueidentifier null;
go

ALTER TABLE [dbo].[EMP_Contract]  WITH CHECK ADD  CONSTRAINT [FK_EMP_Contract_ContractStatus_EMP_Ref_Status_Id] FOREIGN KEY([ContractStatusId])
REFERENCES [dbo].[EMP_Ref_Status] ([Id])
GO