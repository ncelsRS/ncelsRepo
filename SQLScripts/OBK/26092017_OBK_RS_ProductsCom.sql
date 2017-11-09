CREATE TABLE OBK_RS_ProductsCom
(
	[Id] uniqueidentifier NOT NULL,
	[ProductId] int NOT NULL,
	[IsError] [bit] NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	CONSTRAINT PK_OBK_RS_ProductsCom
	PRIMARY KEY ([Id]),
	CONSTRAINT FK_OBK_RS_ProductsCom_OBK_RS_Products_Id
	FOREIGN KEY ([ProductId])
	REFERENCES [OBK_RS_Products]([Id])
)