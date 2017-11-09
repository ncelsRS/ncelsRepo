CREATE TABLE OBK_ContractComRecord
(
	[Id] uniqueidentifier NOT NULL,
	[CommentId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ValueField] [nvarchar](500) NULL,
	[Note] [nvarchar](2000) NULL,
	[DisplayField] [nvarchar](500) NULL,
	CONSTRAINT PK_OBK_ContractComRecord
	PRIMARY KEY ([Id]),
	CONSTRAINT FK_OBK_ContractComRecord_CommentId_OBK_ContractCom_Id
	FOREIGN KEY ([CommentId])
	REFERENCES OBK_ContractCom([Id]),
	CONSTRAINT FK_OBK_ContractComRecord_UserId_Employees_Id
	FOREIGN KEY ([UserId])
	REFERENCES [Employees]([Id])
)