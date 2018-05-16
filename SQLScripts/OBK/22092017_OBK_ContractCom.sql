CREATE TABLE OBK_ContractCom
(
	[Id] uniqueidentifier NOT NULL,
	[ContractId] uniqueidentifier NOT NULL,
	[IsError] [bit] NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[ControlId] [nvarchar](500) NOT NULL,
	CONSTRAINT PK_OBK_ContractCom
	PRIMARY KEY ([Id]),
	CONSTRAINT FK_OBK_ContractCom_ContractId_OBK_Contract_Id
	FOREIGN KEY ([ContractId])
	REFERENCES [OBK_Contract]([Id])
)