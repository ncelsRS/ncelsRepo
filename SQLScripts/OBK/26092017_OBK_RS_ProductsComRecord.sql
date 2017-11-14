CREATE TABLE OBK_RS_ProductsComRecord
(
	[Id] uniqueidentifier NOT NULL,
	[CommentId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ValueField] [nvarchar](500) NULL,
	[Note] [nvarchar](2000) NULL,
	[DisplayField] [nvarchar](500) NULL,
	CONSTRAINT PK_OBK_RS_ProductsComRecord
	PRIMARY KEY ([Id]),
	CONSTRAINT FK_OBK_RS_ProductsComRecord_CommentId_OBK_RS_ProductsCom_Id
	FOREIGN KEY ([CommentId])
	REFERENCES OBK_RS_ProductsCom([Id]),
	CONSTRAINT OBK_RS_ProductsComRecord_UserId_Employees_Id
	FOREIGN KEY ([UserId])
	REFERENCES [Employees]([Id])
)