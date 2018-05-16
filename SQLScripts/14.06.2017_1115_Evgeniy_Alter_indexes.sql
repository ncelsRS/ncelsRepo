IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'PricesTypeIndex') 
    DROP INDEX PricesTypeIndex ON Prices; 
GO
CREATE NONCLUSTERED INDEX PricesTypeIndex
ON [dbo].[Prices] ([Type])
INCLUDE ([PriceProjectId],[BritishPrice],[UnitPrice],[LimitCost],[RequestDate])
GO

IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'PPRequestOrderTypeIndex') 
    DROP INDEX PPRequestOrderTypeIndex ON PriceProjects; 
GO
CREATE NONCLUSTERED INDEX PPRequestOrderTypeIndex
ON [dbo].[PriceProjects] ([RequestOrderType])
GO


IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'PPRequestOrderYearIndex') 
    DROP INDEX PPRequestOrderYearIndex ON PriceProjects; 
GO
CREATE NONCLUSTERED INDEX PPRequestOrderYearIndex
ON [dbo].[PriceProjects] ([RequestOrderYear])
GO

