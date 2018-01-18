USE ncels
GO

IF DB_NAME() <> N'ncels' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[EMP_EAESStatementRegCountry]
--
PRINT (N'Создать таблицу [dbo].[EMP_EAESStatementRegCountry]')
GO
CREATE TABLE dbo.EMP_EAESStatementRegCountry (
  Id int IDENTITY,
  Country nvarchar(50) NOT NULL,
  RegNumber nvarchar(50) NULL,
  DateOfIssue date NULL,
  ExpDate date NULL,
  IsIndefinitely bit NULL,
  StatementId uniqueidentifier NOT NULL,
  CONSTRAINT PK_EMP_EAESStatementRegCountry_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO