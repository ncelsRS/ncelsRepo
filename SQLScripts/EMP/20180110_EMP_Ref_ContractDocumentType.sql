USE ncels
GO

IF DB_NAME() <> N'ncels' SET NOEXEC ON
GO

PRINT (N'Создать таблицу [dbo].[EMP_Ref_ContractDocumentType]')
GO
CREATE TABLE dbo.EMP_Ref_ContractDocumentType (
  Id uniqueidentifier NOT NULL,
  Code nvarchar(50) NULL,
  NameRu nvarchar(255) NOT NULL,
  NameKz nvarchar(255) NOT NULL,
  NameGenitiveRu nvarchar(255) NOT NULL,
  NameGenitiveKz nvarchar(255) NOT NULL,
  CONSTRAINT PK_EMP_Ref_ContractDocumentType PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

INSERT INTO dbo.EMP_Ref_ContractDocumentType
(
  Id
 ,Code
 ,NameRu
 ,NameKz
 ,NameGenitiveRu
 ,NameGenitiveKz
)
VALUES
(
  NEWID()
 ,N'powerOfAttorney'
 ,N'Доверенность'
 ,N'Сенімхат'
 ,N'Доверенности'
 ,N'Доверенности'
),
(
  NEWID()
 ,N'charter'
 ,N'Устав'
 ,N'Жарғы'
 ,N'Устава'
 ,N'Устава'
);
GO