IF NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[OBK_Ref_PriceList]') AND type in (N'U'))

BEGIN
	CREATE TABLE [dbo].[OBK_Ref_PriceList](
		[Id] uniqueidentifier NOT NULL,
		[TypeId] INT NOT NULL,
		[NameRu] NVARCHAR(255) NOT NULL,
		[NameKz] NVARCHAR(255) NOT NULL,
		[UnitId] uniqueidentifier NOT NULL,
		[Price] FLOAT NOT NULL,
		[ServiceTypeId] uniqueidentifier NOT NULL,
		[IsDeleted] BIT NOT NULL,
		[DegreeRiskId] uniqueidentifier NULL,
		CONSTRAINT PK_OBK_Ref_PriceList
		PRIMARY KEY ([Id]),
		CONSTRAINT FK_OBK_Ref_PriceList__TypeId__OBK_Ref_Type__Id
		FOREIGN KEY ([TypeId])
		REFERENCES [OBK_Ref_Type]([Id]),
		CONSTRAINT FK_OBK_Ref_PriceList__UnitId__Dictionaries__Id
		FOREIGN KEY ([UnitId])
		REFERENCES [Dictionaries]([Id]),
		CONSTRAINT FK_OBK_Ref_PriceList__ServiceTypeId__OBK_Ref_ServiceType__Id
		FOREIGN KEY ([ServiceTypeId])
		REFERENCES [OBK_Ref_ServiceType]([Id]),
		CONSTRAINT FK_OBK_Ref_PriceList__DegreeRiskId__OBK_Ref_DegreeRisk__Id
		FOREIGN KEY ([DegreeRiskId])
		REFERENCES [OBK_Ref_DegreeRisk]([Id])
	)
END