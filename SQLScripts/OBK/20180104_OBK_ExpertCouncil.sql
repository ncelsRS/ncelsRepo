USE ncels
GO

IF DB_NAME() <> N'ncels' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[OBK_ExpertCouncil]
--
PRINT (N'Создать таблицу [dbo].[OBK_ExpertCouncil]')
GO
CREATE TABLE dbo.OBK_ExpertCouncil (
  Id int IDENTITY,
  Name nvarchar(50) NULL,
  Date datetime NOT NULL,
  ActualDate datetime NULL,
  CONSTRAINT PK_OBK_ExpertCouncil_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO