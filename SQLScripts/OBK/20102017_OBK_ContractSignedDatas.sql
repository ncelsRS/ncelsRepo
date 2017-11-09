CREATE TABLE [OBK_ContractSignedDatas]
(
	[ContractId] [uniqueidentifier] NOT NULL,
	[ApplicantSign] [ntext] NULL,
	[ApplicantSignDate] [datetime] NULL,
	[CeoSign] [ntext] NULL,
	[CeoSignDate] [datetime] NULL,
	CONSTRAINT PK_OBK_ContractSignedDatas
	PRIMARY KEY ([ContractId]),
	CONSTRAINT FK_OBK_ContractSignedDatas_ContractId_OBK_Contract_Id
	FOREIGN KEY ([ContractId])
	REFERENCES [OBK_Contract]([Id])
)