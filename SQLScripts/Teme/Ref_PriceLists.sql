USE teme
GO

INSERT INTO dbo.Ref_PriceLists
(
  DateCreate
 ,DateUpdate
 ,IsDeleted
 ,IsImport
 ,Price
 ,PriceTypeId
 ,ServiceTypeId
)
VALUES
(
  GETDATE()
 ,GETDATE()
 ,0
 ,0 -- IsImport - bit NOT NULL
 ,308335 -- Price - decimal(18, 2) NOT NULL
 ,5 -- PriceTypeId - int NOT NULL
 ,1 -- ServiceTypeId - int NOT NULL
),(
  GETDATE()
 ,GETDATE()
 ,0
 ,0 -- IsImport - bit NOT NULL
 ,355360 -- Price - decimal(18, 2) NOT NULL
 ,5 -- PriceTypeId - int NOT NULL
 ,2 -- ServiceTypeId - int NOT NULL
),(
  GETDATE()
 ,GETDATE()
 ,0
 ,0 -- IsImport - bit NOT NULL
 ,396928 -- Price - decimal(18, 2) NOT NULL
 ,5 -- PriceTypeId - int NOT NULL
 ,3 -- ServiceTypeId - int NOT NULL
),(
  GETDATE()
 ,GETDATE()
 ,0
 ,0 -- IsImport - bit NOT NULL
 ,457219 -- Price - decimal(18, 2) NOT NULL
 ,5 -- PriceTypeId - int NOT NULL
 ,4 -- ServiceTypeId - int NOT NULL
),(
  GETDATE()
 ,GETDATE()
 ,0
 ,0 -- IsImport - bit NOT NULL
 ,346083 -- Price - decimal(18, 2) NOT NULL
 ,5 -- PriceTypeId - int NOT NULL
 ,5 -- ServiceTypeId - int NOT NULL
);
GO