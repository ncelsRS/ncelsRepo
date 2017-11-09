create table ContractComments(
	Id uniqueidentifier not null,	
	CreateDate datetime not null,
	Comment nvarchar(4000) not null,
	AuthorId uniqueidentifier not null,
	ContractId uniqueidentifier not null
	CONSTRAINT [PK_ContractComments] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)
ALTER TABLE ContractComments ADD  CONSTRAINT DF_ContractComments_Id  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE ContractComments  WITH CHECK ADD  CONSTRAINT [FK_ContractComments_AuthorId_Employees_Id] FOREIGN KEY(AuthorId)
REFERENCES Employees (Id)
GO
ALTER TABLE ContractComments CHECK CONSTRAINT [FK_ContractComments_AuthorId_Employees_Id]
GO
ALTER TABLE ContractComments  WITH CHECK ADD  CONSTRAINT [FK_ContractComments_ContractId_Contracts_Id] FOREIGN KEY(ContractId)
REFERENCES Contracts (Id)
GO
ALTER TABLE ContractComments CHECK CONSTRAINT [FK_ContractComments_ContractId_Contracts_Id]
GO