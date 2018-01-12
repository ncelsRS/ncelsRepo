USE ncels
GO

IF DB_NAME() <> N'ncels' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[OBK_AssessmentDeclarationProgramExecutors]
--
PRINT (N'Создать таблицу [dbo].[OBK_AssessmentDeclarationProgramExecutors]')
GO
CREATE TABLE dbo.OBK_AssessmentDeclarationProgramExecutors (
  Id uniqueidentifier NOT NULL,
  DeclarationId uniqueidentifier NOT NULL,
  ProgramId uniqueidentifier NOT NULL,
  EmployeeId uniqueidentifier NOT NULL,
  CONSTRAINT PK_OBK_AssessmentDeclarationProgramExecutors PRIMARY KEY CLUSTERED (Id, DeclarationId, ProgramId, EmployeeId)
)
ON [PRIMARY]
GO