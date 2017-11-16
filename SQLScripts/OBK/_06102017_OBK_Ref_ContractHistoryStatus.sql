IF NOT (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'OBK_Ref_ContractHistoryStatus'))
BEGIN
CREATE TABLE OBK_Ref_ContractHistoryStatus
(
	[Id] uniqueidentifier NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](2000) NULL,
	[NameKz] [nvarchar](2000) NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEdit] [datetime] NULL,
	CONSTRAINT PK_OBK_Ref_ContractHistoryStatus
	PRIMARY KEY ([Id]),
)
END

IF NOT (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'OBK_ContractHistory'))
BEGIN
CREATE TABLE OBK_ContractHistory
(
	[Id] uniqueidentifier NOT NULL,
	[Created] datetime NOT NULL,
	[EmployeeId] uniqueidentifier NOT NULL,
	[UnitName] nvarchar(4000) NULL,
	[StatusId] uniqueidentifier NOT NULL,
	[RefuseReason] nvarchar(4000) NULL,
	[ContractId] uniqueidentifier NOT NULL,
	CONSTRAINT PK_OBK_ContractHistory_Id
	PRIMARY KEY ([Id]),
	CONSTRAINT FK_OBK_ContractHistory_EmployeeId_Employees_Id
	FOREIGN KEY ([EmployeeId])
	REFERENCES Employees([Id]),
	CONSTRAINT FK_OBK_ContractHistory_StatusId_OBK_Ref_ContractHistoryStatus_Id
	FOREIGN KEY ([StatusId])
	REFERENCES OBK_Ref_ContractHistoryStatus([Id]),
	CONSTRAINT FK_OBK_ContractHistory_ContractId_OBK_Contract_Id
	FOREIGN KEY ([ContractId])
	REFERENCES OBK_Contract([Id])
)
END

