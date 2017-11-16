IF NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[OBK_Ref_ContractDocumentType]') AND type in (N'U'))

BEGIN
CREATE TABLE OBK_Ref_ContractDocumentType
(
	[Id] [uniqueidentifier] NOT NULL,
	[NameRu] [nvarchar](255) NOT NULL,
	[NameKz] [nvarchar](255) NOT NULL,
	[NameGenitiveRu] [nvarchar](255) NOT NULL,
	[NameGenitiveKz] [nvarchar](255) NOT NULL,
	CONSTRAINT PK_OBK_Ref_ContractDocumentType
	PRIMARY KEY ([Id])
)
END