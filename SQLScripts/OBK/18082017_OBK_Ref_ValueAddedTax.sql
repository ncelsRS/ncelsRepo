IF NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[OBK_Ref_ValueAddedTax]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[OBK_Ref_ValueAddedTax](
		[Id] uniqueidentifier NOT NULL,
		[Value] FLOAT NOT NULL,
		[Year] INT NOT NULL CHECK (LEN([Year]) = 4),
		CONSTRAINT PK_OBK_Ref_ValueAddedTax
		PRIMARY KEY ([Id]),
		CONSTRAINT UQ_OBK_Ref_ValueAddedTax_Year
		UNIQUE ([Year])
	)
END