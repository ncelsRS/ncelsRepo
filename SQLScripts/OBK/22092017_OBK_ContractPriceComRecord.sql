CREATE TABLE OBK_ContractPriceComRecord
(
	[Id] uniqueidentifier NOT NULL,
	[CommentId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ValueField] [nvarchar](500) NULL,
	[Note] [nvarchar](2000) NULL,
	[DisplayField] [nvarchar](500) NULL,
	CONSTRAINT PK_OBK_ContractPriceComRecord
	PRIMARY KEY ([Id]),
	CONSTRAINT FK_OBK_ContractPriceComRecord_CommentId_OBK_ContractPriceCom_Id
	FOREIGN KEY ([CommentId])
	REFERENCES OBK_ContractPriceCom([Id]),
	CONSTRAINT FK_OBK_ContractPriceComRecord_UserId_Employees_Id
	FOREIGN KEY ([UserId])
	REFERENCES [Employees]([Id])
)