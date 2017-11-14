BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
IF NOT EXISTS( select *
      from sys.all_columns c
      join sys.tables t on t.object_id = c.object_id
      join sys.schemas s on s.schema_id = t.schema_id
      join sys.default_constraints d on c.default_object_id = d.object_id
    where t.name = 'Tmcs'
      and c.name = 'Id'
      and s.name = 'dbo')
BEGIN
	ALTER TABLE dbo.Tmcs ADD CONSTRAINT
		DF_Tmcs_Id DEFAULT newid() FOR Id
END
GO
IF NOT EXISTS( select *
      from sys.all_columns c
      join sys.tables t on t.object_id = c.object_id
      join sys.schemas s on s.schema_id = t.schema_id
      join sys.default_constraints d on c.default_object_id = d.object_id
    where t.name = 'Tmcs'
      and c.name = 'CreatedDate'
      and s.name = 'dbo')
BEGIN
	ALTER TABLE dbo.Tmcs ADD CONSTRAINT
		DF_Tmcs_CreatedDate DEFAULT getdate() FOR CreatedDate
END
GO
ALTER TABLE dbo.Tmcs SET (LOCK_ESCALATION = TABLE)
GO
COMMIT