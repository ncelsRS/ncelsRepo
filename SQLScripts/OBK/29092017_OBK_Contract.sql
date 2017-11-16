IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'ExpertOrganization'
          AND Object_ID = Object_ID(N'dbo.OBK_Contract'))
BEGIN
    ALTER TABLE OBK_Contract
	ADD [ExpertOrganization] uniqueidentifier NULL,
	CONSTRAINT FK_OBK_Contract_ExpertOrganization_Units_Id
	FOREIGN KEY ([ExpertOrganization])
	REFERENCES Units(Id)
END
GO

DECLARE @defaultExpertOrganization uniqueidentifier
SET @defaultExpertOrganization = (SELECT Id FROM Units WHERE Code = '00')

UPDATE OBK_Contract
SET [ExpertOrganization] = @defaultExpertOrganization
WHERE [ExpertOrganization] IS NULL
GO

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'Signer'
          AND Object_ID = Object_ID(N'dbo.OBK_Contract'))
BEGIN
	ALTER TABLE OBK_Contract
	ADD [Signer] uniqueidentifier NULL,
	CONSTRAINT FK_OBK_Contract_Signer_Employees_Id
	FOREIGN KEY ([Signer])
	REFERENCES Employees(Id)
END
GO

DECLARE @signerId uniqueidentifier
SET @signerId = (
	SELECT Employees.Id from Employees
	INNER JOIN Units On Units.Id = Employees.PositionId
	WHERE Units.Code = 'ncels_ceo'
)

UPDATE OBK_Contract
SET [Signer] = @signerId
WHERE [Signer] IS NULL
GO
