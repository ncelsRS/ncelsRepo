USE ncels
GO

IF DB_NAME() <> N'ncels' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[EMP_StatementSamples]
--
PRINT (N'Создать таблицу [dbo].[EMP_StatementSamples]')
GO
CREATE TABLE dbo.EMP_StatementSamples (
  Id int IDENTITY,
  StatementId uniqueidentifier NOT NULL,
  SampleType nvarchar(50) NOT NULL,
  Name nvarchar(50) NOT NULL,
  Count int NOT NULL,
  Unit nvarchar(50) NOT NULL,
  SeriesPart nvarchar(50) NOT NULL,
  Storage nvarchar(50) NULL,
  Conditions nvarchar(50) NULL,
  CreateDate date NULL,
  ExpirationDate date NULL,
  Addition bit NULL,
  CONSTRAINT PK_EMP_StatementSamples_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_EMP_StatementSamples_StatementId] для объекта типа таблица [dbo].[EMP_StatementSamples]
--
PRINT (N'Создать внешний ключ [FK_EMP_StatementSamples_StatementId] для объекта типа таблица [dbo].[EMP_StatementSamples]')
GO
ALTER TABLE dbo.EMP_StatementSamples
  ADD CONSTRAINT FK_EMP_StatementSamples_StatementId FOREIGN KEY (StatementId) REFERENCES dbo.EMP_Statement (Id)
GO