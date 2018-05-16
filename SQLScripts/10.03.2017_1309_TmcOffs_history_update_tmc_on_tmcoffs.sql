USE [ncels]
GO

/****** Object:  Trigger [dbo].[history_update_tmc]    Script Date: 10.03.2017 10:54:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Alexandr Zarichanskiy
-- Create date: 2017-03-09
-- Description:	log on update
-- =============================================
CREATE TRIGGER [dbo].[history_update_tmc_on_tmcoff]
   ON  [dbo].[TmcOffs]
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

ALTER TABLE [dbo].[TmcOffs] ENABLE TRIGGER [history_update_tmc_on_tmcoff]
GO


