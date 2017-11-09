alter table Contracts
add SignerId uniqueidentifier
go
ALTER TABLE [dbo].Contracts  WITH CHECK ADD  CONSTRAINT [FK__Contracts_Employees] FOREIGN KEY(SignerId)
REFERENCES [dbo].Employees ([Id])
GO

ALTER TABLE [dbo].Contracts CHECK CONSTRAINT [FK__Contracts_Employees]
GO