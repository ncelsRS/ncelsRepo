IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'SignDocEndDate'
          AND Object_ID = Object_ID(N'dbo.OBK_DeclarantContact'))
BEGIN
	ALTER TABLE OBK_DeclarantContact
	ADD	[SignDocEndDate] DATETIME NULL,
		[SignDocUnlimited] BIT NOT NULL DEFAULT(0),
		[BossDocEndDate] DATETIME NULL,
		[BossDocUnlimited] BIT NOT NULL DEFAULT(0)
END
GO