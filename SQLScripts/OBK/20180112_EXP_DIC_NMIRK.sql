USE ncels
GO

IF DB_NAME() <> N'ncels' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[EXP_DIC_NMIRK]
--
PRINT (N'Создать таблицу [dbo].[EXP_DIC_NMIRK]')
GO
CREATE TABLE dbo.EXP_DIC_NMIRK (
  Id int IDENTITY,
  Code int NOT NULL,
  NameRu nvarchar(50) NULL,
  NameKk nvarchar(50) NULL,
  DescriptionRu nvarchar(1000) NULL,
  Descriptionkk nvarchar(1000) NULL,
  CONSTRAINT PK_EXP_DIC_NMIRK_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO