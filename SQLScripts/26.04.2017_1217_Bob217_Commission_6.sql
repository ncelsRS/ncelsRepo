ALTER TABLE [dbo].[CommissionUnits]
DROP CONSTRAINT [CommissionUnits_UnitTypeId_fk]
GO

ALTER TABLE [dbo].[CommissionUnits]
ADD CONSTRAINT [CommissionUnits_UnitTypeId_fk] FOREIGN KEY ([UnitTypeId]) 
  REFERENCES [dbo].[CommissionUnitTypes] ([Id]) 
  ON UPDATE CASCADE
  ON DELETE NO ACTION
GO


UPDATE dbo.CommissionUnitTypes SET Id = 7 WHERE Id = 3
UPDATE dbo.CommissionUnitTypes SET Id = 3 WHERE Id = 2
UPDATE dbo.CommissionUnitTypes SET Id = 2 WHERE Id = 4
UPDATE dbo.CommissionUnitTypes SET Id = 4 WHERE Id = 7

UPDATE dbo.CommissionUnitTypes SET Name = 'Заместитель Председателя' WHERE Id = 2