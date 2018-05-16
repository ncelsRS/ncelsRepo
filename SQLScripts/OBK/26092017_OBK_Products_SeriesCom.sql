CREATE TABLE OBK_Products_SeriesCom
(
	[Id] uniqueidentifier NOT NULL,
	[ProductSerieId] int NOT NULL,
	[IsError] [bit] NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	CONSTRAINT PK_OBK_Products_SeriesCom
	PRIMARY KEY ([Id]),
	CONSTRAINT FK_OBK_Products_SeriesCom_OBK_Procunts_Series_Id
	FOREIGN KEY ([ProductSerieId])
	REFERENCES [OBK_Procunts_Series]([Id])
)