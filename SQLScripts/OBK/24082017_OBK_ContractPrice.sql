IF NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[OBK_ContractPrice]') AND type in (N'U'))
BEGIN
	CREATE TABLE OBK_ContractPrice(
		[Id] UNIQUEIDENTIFIER NOT NULL,
		[PriceRefId] UNIQUEIDENTIFIER NOT NULL,
		[Count] INT NOT NULL,
		[PriceWithoutTax] FLOAT NOT NULL,
		[PriceWithTax] FLOAT NOT NULL,
		[ProductId] INT NULL,
		[ContractId] UNIQUEIDENTIFIER NOT NULL,
		CONSTRAINT PK_OBK_ContractPrice_Id
		PRIMARY KEY ([Id]),
		CONSTRAINT FK_OBK_ContractPrice_PriceRefId__OBK_Ref_PriceList_Id
		FOREIGN KEY ([PriceRefId])
		REFERENCES OBK_Ref_PriceList([Id]),
		CONSTRAINT FK_OBK_ContractPrice_ProductId__OBK_RS_Products_Id
		FOREIGN KEY ([ProductId])
		REFERENCES OBK_RS_Products([Id]),
		CONSTRAINT FK_OBK_ContractPrice_ContractId__OBK_Contract_Id
		FOREIGN KEY ([ContractId])
		REFERENCES OBK_Contract([Id])
	)
END