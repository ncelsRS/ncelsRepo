IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'RegTypeId'
          AND Object_ID = Object_ID(N'dbo.OBK_RS_Products'))
BEGIN
    ALTER TABLE [OBK_RS_Products]
	ADD	[RegTypeId] INT NULL
END
GO

UPDATE [OBK_RS_Products]
SET [RegTypeId] = 1
WHERE [RegTypeId] IS NULL

ALTER TABLE [OBK_RS_Products]
ALTER COLUMN [RegTypeId] INT NOT NULL

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'DegreeRiskId'
          AND Object_ID = Object_ID(N'dbo.OBK_RS_Products'))
BEGIN
    ALTER TABLE [OBK_RS_Products]
	ADD	[DegreeRiskId] INT NULL
END

