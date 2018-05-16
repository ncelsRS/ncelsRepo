ALTER TABLE [dbo].[Commissions]
DROP CONSTRAINT [UQ_Commissions_Number]
GO

ALTER TABLE [dbo].[Commissions]
ADD CONSTRAINT [UQ_Commissions_Number] 
UNIQUE NONCLUSTERED ([Number], [TypeId])
WITH (
  PAD_INDEX = OFF,
  IGNORE_DUP_KEY = OFF,
  STATISTICS_NORECOMPUTE = OFF,
  ALLOW_ROW_LOCKS = ON,
  ALLOW_PAGE_LOCKS = ON)
ON [PRIMARY]
GO

INSERT INTO dbo.CommissionUnitTypes
VALUES(4,'Заместитель Председателя', 1)
GO