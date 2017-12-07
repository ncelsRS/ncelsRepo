EXEC sp_RENAME 'dbo.OBK_ContractFactory.Location', 'LegalLocation', 'COLUMN'
Go
ALTER TABLE dbo.OBK_ContractFactory ADD ActualLocation NVARCHAR(255) NOT NULL DEFAULT ('')
Go
Update dbo.OBK_ContractFactory SET ActualLocation = LegalLocation Where ActualLocation = ''
Go
ALTER TABLE dbo.OBK_ContractFactory ADD Count int NOT NULL DEFAULT (1)
Go