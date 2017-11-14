-- =============================================
-- Author:		Alexandr Zarichanskiy
-- Create date: 2017-03-27
-- Description:	Update task report data on status agreed 
-- =============================================
CREATE TRIGGER [dbo].[Tr_update_tmcreportdata] 
   ON  [dbo].[TmcReportTasks]
   AFTER UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF(UPDATE(Operation))
	BEGIN
		INSERT INTO [dbo].[TmcReportData]
				( Id
				,CreatedDate
				,CreatedEmployeeId
				,CreatedEmployeeName
				,TmcId
				,TmcName
				,TmcCode
				,GeneralCount
				,GeneralCountFact
				,GeneralConvertFact
				,IssuedCount
				,UseCount
				,UseFactCount
				,ReceivedDate
				,MeasureName
				,MeasureConvertName
				,UseReasonText
				,PositionId
				,PositionName
				,SubDepartmentId
				,SubDepartmentName
				,DepartmentId
				,DepartmentName
				,CenterId
				,CenterName
				,ExpertiseStatementId
				,ExpertiseStatementNumber
				,ExpertiseStatementTypeStr
				,refTmcReport)
		 SELECT 
				   NEWID() AS Id
				   ,GETDATE() AS CreatedDate
				   ,Tdata.CreatedEmployeeId
				   ,Tdata.CreateEmployeeName
				   ,Tdata.TmcId
				   ,Tdata.TmcName
				   ,Tdata.TmcCode
				   ,NULL
				   ,Tdata.GeneralCountFact
				   ,Tdata.GeneralConvertFact
				   ,Tdata.IssuedCount           
				   ,Tdata.UsedCount
				   ,Tdata.UseFactCount
				   ,Tdata.ReceivingDate
				   ,Tdata.MeasureName
				   ,Tdata.MeasureConvertName
				   ,Tdata.UseReason
				   ,Tdata.PositionId
				   ,Tdata.PositionName
				   ,Tdata.SubDepartmentId
				   ,Tdata.SubDepartmentName
				   ,Tdata.DepartmentId
				   ,Tdata.DepartmentName
				   ,Tdata.CenterId
				   ,Tdata.CenterName
				   ,Tdata.ExpertiseStatementId
				   ,Tdata.ExpertiseStatementNumber
				   ,Tdata.ExpertiseStatementTypeStr
				   ,Ti.Id
			FROM [dbo].[TmcReportDataSourceView] AS Tdata
			INNER JOIN [dbo].[TmcReports] AS Tr ON Tr.DepartmentId = Tdata.DepartmentId OR Tr.DepartmentId = Tdata.SubDepartmentId
			AND Tdata.CreatedDate >= Tr.PeriodStartDate AND Tdata.CreatedDate <= Tr.PeriodEndDate
			INNER JOIN inserted AS Ti ON Ti.refTmcReport = Tr.Id AND Ti.Operation = 'AGREED' AND Ti.[State] = 1
	END
END

GO

ALTER TABLE [dbo].[TmcReportTasks] ENABLE TRIGGER [Tr_update_tmcreportdata]
GO


