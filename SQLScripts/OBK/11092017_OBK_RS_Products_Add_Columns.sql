IF	NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'DrugFormBoxCount'
          AND Object_ID = Object_ID(N'dbo.OBK_RS_Products'))
BEGIN
    ALTER TABLE OBK_RS_Products
	ADD DrugFormBoxCount NVARCHAR(255) NULL,
	DrugFormFullName NVARCHAR(4000) NULL,
	DrugFormFullNameKz NVARCHAR(4000) NULL
END

GO