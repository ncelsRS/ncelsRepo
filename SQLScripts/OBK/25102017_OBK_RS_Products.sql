ALTER TABLE OBK_RS_Products
ADD
	RegisterId int null,
	RegNumber nvarchar(50) null,
	RegNumberKz nvarchar(50) null,
	RegDate datetime null,
	ExpirationDate datetime null,
	NdName nvarchar(50) null,
	NdNumber nvarchar(50) null
GO

UPDATE OBK_RS_Products
SET RegisterId = 9849
WHERE RegisterId IS NULL
GO

UPDATE OBK_RS_Products
SET RegDate = '2010-01-01'
WHERE RegDate IS NULL
GO

ALTER TABLE OBK_RS_Products
ALTER COLUMN RegisterId int not null
GO

ALTER TABLE OBK_RS_Products
ALTER COLUMN RegDate datetime not null
GO

