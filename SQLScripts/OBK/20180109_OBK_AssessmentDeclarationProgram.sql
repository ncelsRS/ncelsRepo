USE ncels
GO

IF DB_NAME() <> N'ncels' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[OBK_AssessmentDeclarationProgram]
--
PRINT (N'Создать таблицу [dbo].[OBK_AssessmentDeclarationProgram]')
GO
CREATE TABLE dbo.OBK_AssessmentDeclarationProgram (
  Id uniqueidentifier NOT NULL,
  DeclarationId uniqueidentifier NOT NULL,
  DateFrom datetime NOT NULL,
  DateTo datetime NOT NULL,
  CONSTRAINT PK_OBK_AssessmentDeclarationProgram_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO