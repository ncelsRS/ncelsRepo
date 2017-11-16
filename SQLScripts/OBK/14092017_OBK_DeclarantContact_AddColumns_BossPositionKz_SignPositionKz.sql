IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'SignPositionKz'
          AND Object_ID = Object_ID(N'dbo.OBK_DeclarantContact'))
BEGIN
    ALTER TABLE OBK_DeclarantContact
	ADD [SignPositionKz] nvarchar(255) NULL
END

GO

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'BossPositionKz'
          AND Object_ID = Object_ID(N'dbo.OBK_DeclarantContact'))
BEGIN
    ALTER TABLE OBK_DeclarantContact
	ADD [BossPositionKz] nvarchar(255) NULL
END

GO