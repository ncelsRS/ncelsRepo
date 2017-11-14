USE [ncels]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE dbo.FileLinks
ADD Comment varchar(max) NULL
GO

EXEC sp_addextendedproperty 'MS_Description', N'Комментарий к файлу, оставляемый работником после проверки', N'schema', N'dbo', N'table', N'FileLinks', N'column', N'Comment'
GO

ALTER TABLE dbo.FileLinks
ADD Language varchar(20) NULL
GO

EXEC sp_addextendedproperty 'MS_Description', N'Язык файла kz = Казахский, NULL или ru - Русский', N'schema', N'dbo', N'table', N'FileLinks', N'column', N'Language'
GO

ALTER TABLE dbo.FileLinks
ADD PageNumbers int NULL
GO

EXEC sp_addextendedproperty 'MS_Description', N'Количество страниц в файле (если это текстовый документ)', N'schema', N'dbo', N'table', N'FileLinks', N'column', N'PageNumbers'
GO

ALTER TABLE dbo.FileLinks
ADD StageId int NULL
GO

EXEC sp_addextendedproperty 'MS_Description', N'Ссылка на этап, на котором была произведена загрузка данного файла', N'schema', N'dbo', N'table', N'FileLinks', N'column', N'StageId'
GO

ALTER TABLE dbo.FileLinks
ADD CONSTRAINT FileLinks_EXP_DIC_Sage_fk FOREIGN KEY (StageId) 
  REFERENCES dbo.EXP_DIC_Stage (Id) 
  ON UPDATE NO ACTION
  ON DELETE SET NULL
GO
