
USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_StageSendEDO]    Script Date: 26.01.2018 14:58:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_StageSendEDO](
	[Id] [uniqueidentifier] NOT NULL,
	[SendDate] [datetime] NULL,
	[Number] [nvarchar](50) NULL,
 CONSTRAINT [PK_OBK_StageSendEDO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO




  ALTER TABLE [ncels].[dbo].[OBK_StageExpDocument]
  ADD OBK_StageSendEDOId UNIQUEIDENTIFIER NULL

  ALTER TABLE [ncels].[dbo].[OBK_StageExpDocument]
  ADD FOREIGN KEY (OBK_StageSendEDOId) REFERENCES [ncels].[dbo].[OBK_StageSendEDO](Id);



  


CREATE VIEW [dbo].[OBK_DefectiveProductsView]
AS
SELECT	 [OBK_StageExpDocument].[Id]
		,[OBK_Declarant].[NameRu] AS [Declarant] --Информация о Заявителе
		,[OBK_RS_Products].[DrugFormFullName]--Наименование продукции
		,[OBK_RS_Products].[ProducerNameRu] AS [ProducerName]  --Производитель
		,[OBK_RS_Products].[CountryNameRu] AS [ProducerCountry]  --Страна
		,[OBK_Procunts_Series].Series --Серия 
		,[OBK_Procunts_Series].[SeriesEndDate]	 --Срок годности
		,[OBK_Procunts_Series].[Quantity] --Количество
		,[sr_measures].[name]  AS [Measure]-- Единица измерения
		,[OBK_StageExpDocument].[RefuseDate] -- Дата отказа	
		,[OBK_StageSendEDO].[Number] -- Номерь письма
		,[OBK_StageSendEDO].[SendDate] -- Дата отправки письма
		,[OBK_StageExpDocument].[OBK_StageSendEDOId] -- Айди отправленного письма
		,(SELECT CASE 
			WHEN [OBK_StageExpDocument].[OBK_StageSendEDOId] IS NULL THEN N'не отправлено' 
			WHEN [OBK_StageExpDocument].[OBK_StageSendEDOId] IS NOT NULL THEN N'отправлено' END) AS [Status]
FROM [ncels].[dbo].[OBK_StageExpDocument]
INNER JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
INNER JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
INNER JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_StageExpDocument].[ProductSeriesId] = [OBK_Procunts_Series].[Id]
INNER JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_Procunts_Series].[OBK_RS_ProductsId] = [OBK_RS_Products].[Id]
LEFT JOIN [ncels].[dbo].[sr_measures] ON [OBK_Procunts_Series].[SeriesMeasureId] = [sr_measures].[Id]
LEFT JOIN [ncels].[dbo].[OBK_StageSendEDO] ON [ncels].[dbo].[OBK_StageExpDocument].[OBK_StageSendEDOId] = [OBK_StageSendEDO].[Id]

GO
