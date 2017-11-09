IF NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[OBK_Ref_ServiceType]') AND type in (N'U'))

BEGIN
CREATE TABLE [dbo].[OBK_Ref_ServiceType](
	[Id] uniqueidentifier NOT NULL,
	[NameRu] NVARCHAR(255) NOT NULL,
	[NameKz] NVARCHAR(255) NOT NULL,
	CONSTRAINT PK_OBK_Ref_ServiceType
	PRIMARY KEY ([Id])
) 
END