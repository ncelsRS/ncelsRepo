IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'SignerIsBoss'
          AND Object_ID = Object_ID(N'dbo.OBK_DeclarantContact'))
BEGIN
    ALTER TABLE OBK_DeclarantContact
	ADD [SignerIsBoss] BIT NOT NULL DEFAULT(0)
END

GO