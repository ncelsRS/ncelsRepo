IF NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[OBK_Ref_DegreeRisk]') AND type in (N'U'))

BEGIN
	CREATE TABLE [dbo].[OBK_Ref_DegreeRisk]
	(
		[iD] uniqueidentifier NOT NULL,
		[NameRu] nvarchar(255) NOT NULL,
		[NameKz] nvarchar(255) NOT NULL,
		CONSTRAINT PK_OBK_Ref_DegreeRisk
		PRIMARY KEY ([Id])
	)
END