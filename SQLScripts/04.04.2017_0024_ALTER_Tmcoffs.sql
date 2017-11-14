
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
ALTER TABLE dbo.TmcOffs
	DROP CONSTRAINT DF_TmcOffs_StateType
GO
ALTER TABLE dbo.TmcOffs
	DROP CONSTRAINT DF_TmcOffs_Count
GO
CREATE TABLE dbo.Tmp_TmcOffs
	(
	Id uniqueidentifier NOT NULL,
	CreatedDate datetime NOT NULL,
	CreatedEmployeeId uniqueidentifier NOT NULL,
	StateType int NOT NULL,
	TmcOutId uniqueidentifier NULL,
	Note nvarchar(450) NULL,
	Count decimal(18, 6) NOT NULL,
	ExpertiseStatementId uniqueidentifier NULL,
	ExpertiseStatementNumber nvarchar(512) NULL,
	ExpertiseStatementTypeStr nvarchar(512) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_TmcOffs SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_TmcOffs ADD CONSTRAINT
	DF_TmcOffs_StateType DEFAULT ((0)) FOR StateType
GO
ALTER TABLE dbo.Tmp_TmcOffs ADD CONSTRAINT
	DF_TmcOffs_Count DEFAULT ((0)) FOR Count
GO
IF EXISTS(SELECT * FROM dbo.TmcOffs)
	 EXEC('INSERT INTO dbo.Tmp_TmcOffs (Id, CreatedDate, CreatedEmployeeId, StateType, TmcOutId, Note, Count, ExpertiseStatementId, ExpertiseStatementNumber, ExpertiseStatementTypeStr)
		SELECT Id, CONVERT(datetime, CreatedDate), CreatedEmployeeId, StateType, TmcOutId, Note, Count, ExpertiseStatementId, ExpertiseStatementNumber, ExpertiseStatementTypeStr FROM dbo.TmcOffs WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.TmcOffs
GO
EXECUTE sp_rename N'dbo.Tmp_TmcOffs', N'TmcOffs', 'OBJECT' 
GO
ALTER TABLE dbo.TmcOffs ADD CONSTRAINT
	PK_TmcOffs PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
-- =============================================
-- Author:		Alexandr Zarichanskiy
-- Create date: 2017-03-09
-- Description:	log on update
-- =============================================
CREATE TRIGGER [dbo].[history_update_tmc_on_tmcoff]
   ON  dbo.TmcOffs
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	declare @ModifiedUser nvarchar(4000);
	select Top 1 @ModifiedUser = DisplayName From Employees as Te
	INNER JOIN  inserted as Ti ON Ti.CreatedEmployeeId =  Te.Id
	
	INSERT INTO [dbo].[History] ([GroupId], [OperationId], [TableName], [ColumnName], [ObjectId], [Record], [NewValue], [OldValue],[CreatedTime], [UserName], [Ip])
	SELECT NEWID(), 'UPDATE', 'TMC', 'Column_TMC_UsedDate', Toc.TmcId, i.[Note], i.CreatedDate, NULL, GetDate(), @ModifiedUser, NULL 
	FROM inserted as i
	INNER JOIN [dbo].[TmcOutCounts] AS Toc ON Toc.Id = i.TmcOutId
END
GO
COMMIT