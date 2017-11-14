IF OBJECT_ID('dbo.OBK_MtPart') IS NULL
BEGIN
	CREATE TABLE OBK_MtPart
	(
		Id uniqueidentifier not null,
		ProductId INT NOT NULL,
		PartNumber INT NULL,
		Model varchar(500) NULL,
		Specification varchar(500),
		SpecificationKz nvarchar(500),
		ProducerName varchar(2000),
		CountryName varchar(500),
		ProducerNameKz nvarchar(2000),
		CountryNameKz nvarchar(500)
		CONSTRAINT PK_OBK_MtPart_Id
		PRIMARY KEY ([Id]),
		CONSTRAINT FK_OBK_MtPart_ProductId_OBK_RS_Products_Id
		FOREIGN KEY ([ProductId])
		REFERENCES OBK_RS_Products(Id)
	)
END
GO