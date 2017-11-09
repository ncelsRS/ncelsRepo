IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'IsResident'
          AND Object_ID = Object_ID(N'dbo.OBK_Declarant'))
BEGIN
    ALTER TABLE [OBK_Declarant]
	ADD [IsResident] BIT NOT NULL DEFAULT(1)
END