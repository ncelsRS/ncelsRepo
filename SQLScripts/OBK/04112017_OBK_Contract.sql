IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'ContractAdditionType'
          AND Object_ID = Object_ID(N'dbo.OBK_Contract'))
BEGIN
	ALTER TABLE OBK_Contract
	ADD	[ContractAdditionType] uniqueidentifier NULL,
		 CONSTRAINT FK_OBK_Contract_ContractAdditionType_Dictionaries_Id
		 FOREIGN KEY ([ContractAdditionType])
		 REFERENCES [Dictionaries]([Id])
END
GO