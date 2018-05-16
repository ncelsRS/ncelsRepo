CREATE TABLE OBK_ContractFactory
(
	Id uniqueidentifier NOT NULL,
	ContractId uniqueidentifier NOT NULL,
	Name nvarchar(255) NOT NULL,
	Location nvarchar(255) NOT NULL,
	CONSTRAINT PK_OBK_ContractFactory
	PRIMARY KEY ([Id]),
	CONSTRAINT FK_OBK_ContractFactory_ContractId_OBK_Contract_Id
	FOREIGN KEY ([ContractId])
	REFERENCES [OBK_Contract]([Id])
)
GO