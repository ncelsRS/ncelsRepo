USE ncels
GO

DROP TABLE IF EXISTS dbo.OBK_AssessmentReportOPExecutors
GO


--
-- Создать таблицу [dbo].[OBK_AssessmentReportOPExecutors]
--
PRINT (N'Создать таблицу [dbo].[OBK_AssessmentReportOPExecutors]')
GO
CREATE TABLE dbo.OBK_AssessmentReportOPExecutors (
  Id int IDENTITY,
  ReportId int NULL,
  EmployeeId uniqueidentifier NULL,
  ExecuteResult int NULL,
  Comment nvarchar(2000) NULL,
  Date date NULL,
  Type int NULL,
  CONSTRAINT PK_OBK_AssessmentReportOPExecutors_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_OBK_AssessmentReportOPExecutors_EmployeeId] для объекта типа таблица [dbo].[OBK_AssessmentReportOPExecutors]
--
PRINT (N'Создать внешний ключ [FK_OBK_AssessmentReportOPExecutors_EmployeeId] для объекта типа таблица [dbo].[OBK_AssessmentReportOPExecutors]')
GO
ALTER TABLE dbo.OBK_AssessmentReportOPExecutors
  ADD CONSTRAINT FK_OBK_AssessmentReportOPExecutors_EmployeeId FOREIGN KEY (EmployeeId) REFERENCES dbo.Employees (Id)
GO

--
-- Создать внешний ключ [FK_OBK_AssessmentReportOPExecutors_ReportId] для объекта типа таблица [dbo].[OBK_AssessmentReportOPExecutors]
--
PRINT (N'Создать внешний ключ [FK_OBK_AssessmentReportOPExecutors_ReportId] для объекта типа таблица [dbo].[OBK_AssessmentReportOPExecutors]')
GO
ALTER TABLE dbo.OBK_AssessmentReportOPExecutors
  ADD CONSTRAINT FK_OBK_AssessmentReportOPExecutors_ReportId FOREIGN KEY (ReportId) REFERENCES dbo.OBK_AssessmentReportOP (Id)
GO