CREATE TABLE OBK_Products_SeriesComRecord
(
	[Id] uniqueidentifier NOT NULL,
	[CommentId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ValueField] [nvarchar](500) NULL,
	[Note] [nvarchar](2000) NULL,
	[DisplayField] [nvarchar](500) NULL,
	CONSTRAINT PK_OBK_Products_SeriesComRecord
	PRIMARY KEY ([Id]),
	CONSTRAINT FK_OBK_Products_SeriesComRecord_CommentId_OBK_Products_SeriesCom_Id
	FOREIGN KEY ([CommentId])
	REFERENCES OBK_Products_SeriesCom([Id]),
	CONSTRAINT FK_OBK_Products_SeriesComRecord_UserId_Employees_Id
	FOREIGN KEY ([UserId])
	REFERENCES [Employees]([Id])
)