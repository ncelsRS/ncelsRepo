IF NOT (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'OBK_Ref_ContractExtHistoryStatus'))
BEGIN
CREATE TABLE OBK_Ref_ContractExtHistoryStatus
(
	[Id] uniqueidentifier NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](2000) NULL,
	[NameKz] [nvarchar](2000) NULL,
	[DescriptionRu] [nvarchar](2000) NULL,
	[DescriptionKz] [nvarchar](2000) NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEdit] [datetime] NULL,
	CONSTRAINT PK_OBK_Ref_ContractExtHistoryStatus
	PRIMARY KEY ([Id]),
)
END

IF NOT (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'OBK_ContractExtHistory'))
BEGIN
CREATE TABLE OBK_ContractExtHistory
(
	[Id] uniqueidentifier NOT NULL,
	[Created] datetime NOT NULL,
	[StatusId] uniqueidentifier NOT NULL,
	[ContractId] uniqueidentifier NOT NULL,
	CONSTRAINT PK_OBK_ContractExtHistory_Id
	PRIMARY KEY ([Id]),
	CONSTRAINT FK_OBK_ContractExtHistory_StatusId_OBK_Ref_ContractExtHistoryStatus_Id
	FOREIGN KEY ([StatusId])
	REFERENCES OBK_Ref_ContractExtHistoryStatus([Id]),
	CONSTRAINT FK_OBK_ContractExtHistory_ContractId_OBK_Contract_Id
	FOREIGN KEY ([ContractId])
	REFERENCES OBK_Contract([Id])
)
END

