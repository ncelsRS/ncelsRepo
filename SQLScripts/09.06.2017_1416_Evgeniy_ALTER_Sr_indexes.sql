IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'SrProdTypeIndex') 
    DROP INDEX SrProdTypeIndex ON sr_register_producers; 
GO
CREATE NONCLUSTERED INDEX SrProdTypeIndex
ON [dbo].[sr_register_producers] ([producer_type_id])
INCLUDE ([register_id],[producer_id],[country_id]);
GO

IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'SrProdTypeCountryIndex') 
    DROP INDEX SrProdTypeCountryIndex ON sr_register_producers; 
GO
CREATE NONCLUSTERED INDEX SrProdTypeCountryIndex
ON [dbo].[sr_register_producers] ([register_id],[producer_type_id])
INCLUDE ([producer_id],[country_id]);
GO


IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'SrRegSubstanceIndex') 
    DROP INDEX SrRegSubstanceIndex ON sr_register_substances; 
GO
CREATE NONCLUSTERED INDEX SrRegSubstanceIndex
ON [dbo].[sr_register_substances] ([register_id]);
GO


IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'SrRegisterUmIndex') 
    DROP INDEX SrRegisterUmIndex ON sr_register_use_methods; 
GO
CREATE NONCLUSTERED INDEX SrRegisterUmIndex
ON [dbo].[sr_register_use_methods] ([register_id])
INCLUDE ([use_method_id])
GO



IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'SrDrugFormIndex') 
    DROP INDEX SrDrugFormIndex ON sr_drug_forms; 
GO
CREATE NONCLUSTERED INDEX SrDrugFormIndex
ON [dbo].[sr_drug_forms] ([register_id])
INCLUDE ([id],[pr_box_id],[sec_box_id],[box_count],[pr_box_count],[sec_box_count])
GO
