CREATE TABLE OBK_ContractPriceCom
(
	[Id] uniqueidentifier NOT NULL,
	[ContractPriceId] uniqueidentifier NOT NULL,
	[IsError] [bit] NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	CONSTRAINT PK_OBK_ContractPriceCom
	PRIMARY KEY ([Id]),
	CONSTRAINT FK_OBK_ContractPriceCom_OBK_ContractPrice_ContractPriceId
	FOREIGN KEY ([ContractPriceId])
	REFERENCES [OBK_ContractPrice]([Id])
)