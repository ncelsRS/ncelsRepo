 
CREATE TABLE [dbo].[OBK_ZBKCopyBlankNumber](
	[Id] [uniqueidentifier] NOT NULL,
	[OBK_ZBKCopyBlankId] [uniqueidentifier] NULL,
	[Number] [int] NULL,
	[Application] [bit] NULL,
 CONSTRAINT [PK_OBK_ZBKCopyBlankNumber] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_ZBKCopyBlankNumber]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ZBKCopyBlankNumber_OBK_ZBKCopyBlank1] FOREIGN KEY([OBK_ZBKCopyBlankId])
REFERENCES [dbo].[OBK_ZBKCopyBlank] ([Id])
GO

ALTER TABLE [dbo].[OBK_ZBKCopyBlankNumber] CHECK CONSTRAINT [FK_OBK_ZBKCopyBlankNumber_OBK_ZBKCopyBlank1]
GO



  ALTER TABLE [ncels].[dbo].[OBK_ZBKCopyBlank]
ALTER COLUMN [StartNumber] int

  ALTER TABLE [ncels].[dbo].[OBK_ZBKCopyBlank]
ALTER COLUMN [EndPrimeNumber] int

  ALTER TABLE [ncels].[dbo].[OBK_ZBKCopyBlank]
ALTER COLUMN [StartApplicationNumber] int

  ALTER TABLE [ncels].[dbo].[OBK_ZBKCopyBlank]
ALTER COLUMN [EndApplicationNumber] int


USE [ncels]
GO

/****** Object:  View [dbo].[EMP_ContractHistoryView]    Script Date: 09.01.2018 18:31:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[OBK_ZBKCopyStageView]
AS
SELECT [OBK_ZBKCopyStage].[Id] as [StageId]
	  ,[OBK_StageExpDocument].[ExpConclusionNumber]
	  ,[OBK_StageExpDocument].[ExpStartDate]
	  ,[OBK_StageExpDocument].[ExpEndDate]
	  ,[OBK_StageExpDocument].[ExpApplication]
	  ,[OBK_AssessmentDeclaration].[Number] as [DeclarationNumber]
	  ,[OBK_ZBKCopy].[SendDate] as [ZBKCopySendDate]
	  ,[OBK_DeclarantContact].[BossLastName] + [OBK_DeclarantContact].[BossFirstName] + [OBK_DeclarantContact].[BossMiddleName] as [Declarer]
	  ,[OBK_Ref_Type].[NameRu] as [DeclarationType]
	  ,[Dictionaries].[Name] as [OrganizationName]
	  ,[OBK_Ref_StageStatus].[Code] as [StageStatusCode]
		

FROM [ncels].[dbo].[OBK_ZBKCopyStage]
LEFT JOIN [ncels].[dbo].[OBK_ZBKCopy] on [OBK_ZBKCopyStage].[OBK_ZBKCopyId] = [OBK_ZBKCopy].[Id]
LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] on [OBK_StageExpDocument].[Id] = [OBK_ZBKCopy].[OBK_StageExpDocumentId]
LEFT JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] on [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Contract] on [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
LEFT JOIN [ncels].[dbo].[OBK_DeclarantContact] on [OBK_Contract].[DeclarantContactId] = [OBK_DeclarantContact].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Ref_Type] on [OBK_AssessmentDeclaration].[TypeId] = [OBK_Ref_Type].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Declarant] on [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
LEFT JOIN [ncels].[dbo].[Dictionaries] on [OBK_Declarant].[OrganizationFormId] = [Dictionaries].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Ref_StageStatus] on [OBK_ZBKCopyStage].[StageStatusId] = [OBK_Ref_StageStatus].[Id]

GO




DELETE FROM [ncels].[dbo].[OBK_ZBKCopyCorruptedBlank]   

DROP TABLE [ncels].[dbo].[OBK_ZBKCopyCorruptedBlank]   



USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_ZBKCopyCorruptedBlank]    Script Date: 10.01.2018 10:33:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_ZBKCopyCorruptedBlank](
	[Id] [uniqueidentifier] NOT NULL,
	[ZBKCopyBlankNumberId] [uniqueidentifier] NULL,
	[CorruptedBlankNumber] [int] NULL,
	[NewBlankNumber] [int] NULL,
	[EmployeeId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_PK_OBK_ZBKCopyCorruptedBlank] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_ZBKCopyCorruptedBlank]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ZBKCopyCorruptedBlank_OBK_ZBKCopyBlankNumber] FOREIGN KEY([ZBKCopyBlankNumberId])
REFERENCES [dbo].[OBK_ZBKCopyBlankNumber] ([Id])
GO

ALTER TABLE [dbo].[OBK_ZBKCopyCorruptedBlank] CHECK CONSTRAINT [FK_OBK_ZBKCopyCorruptedBlank_OBK_ZBKCopyBlankNumber]
GO



ALTER TABLE [dbo].[OBK_ZBKCopyCorruptedBlank]
ADD UNIQUE (ZBKCopyBlankNumberId);	

USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ZBKRegisterView]    Script Date: 10.01.2018 19:10:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[OBK_ZBKRegisterView]
AS
SELECT CONVERT(varchar(50), [OBK_StageExpDocument].[ExpBlankNumber]) AS [BlankNumber]
	  ,[OBK_ZBKCopy].[ExtraditeDate]
	  ,[OBK_StageExpDocument].[ExpConclusionNumber]
	  ,(SELECT CASE
	   WHEN [OBK_ZBKCopyBlankNumber].[Application] = 1 THEN N'Копия приложения ЗБК' 
	   WHEN [OBK_ZBKCopyBlankNumber].[Application] = 0 THEN N'Копия ЗБК' END) AS [ZBKType]
	  ,[OBK_Declarant].[NameRu] AS [Declarant]
	  ,[OBK_RS_Products].[DrugFormFullName] AS [ProductName]
	  ,[OBK_ZBKCopyBlankNumber].[Id]
FROM [ncels].[dbo].[OBK_ZBKCopyBlankNumber]
LEFT JOIN [ncels].[dbo].[OBK_ZBKCopyBlank] ON [OBK_ZBKCopyBlankNumber].[OBK_ZBKCopyBlankId] = [OBK_ZBKCopyBlank].[Id]
LEFT JOIN [ncels].[dbo].[OBK_ZBKCopy] ON [OBK_ZBKCopyBlank].[ZBKCopyId] = [OBK_ZBKCopy].[Id]
LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] ON [OBK_ZBKCopy].[OBK_StageExpDocumentId] = [OBK_StageExpDocument].[Id]
LEFT JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_StageExpDocument].[ProductSeriesId] = [OBK_Procunts_Series].[Id]
LEFT JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_Procunts_Series].[OBK_RS_ProductsId] = [OBK_RS_Products].[Id]

UNION ALL

SELECT CONVERT(varchar(50), [OBK_StageExpDocument].[ExpBlankNumber]) AS [BlankNumber]
      ,[OBK_StageExpDocument].[ExpStartDate] AS [ExtraditeDate]
	  ,[OBK_StageExpDocument].[ExpConclusionNumber]
	  ,(SELECT N'ЗБК') AS [ZBKType]
	  ,[OBK_Declarant].[NameRu] AS [Declarant]
	  ,[OBK_RS_Products].[DrugFormFullName] AS [ProductName]
	  ,[OBK_StageExpDocument].[Id]
FROM [ncels].[dbo].[OBK_StageExpDocument]
LEFT JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Declarant] ON [OBK_Contract].[DeclarantId] = [OBK_Declarant].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_StageExpDocument].[ProductSeriesId] = [OBK_Procunts_Series].[Id]
LEFT JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_Procunts_Series].[OBK_RS_ProductsId] = [OBK_RS_Products].[Id]
WHERE [OBK_StageExpDocument].[ExpConclusionNumber] IS NOT NULL


GO





ALTER TABLE [ncels].[dbo].[OBK_StageExpDocument] ADD RefuseDate DATETIME NULL
USE [ncels]
GO

/****** Object:  View [dbo].[OBK_ZBKRegisterView]    Script Date: 11.01.2018 14:36:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[OBK_DeclarationCorrespondingView]
AS
SELECT [OBK_ZBKCopyBlankNumber].[Id] --Id 
	  ,CONVERT(varchar(50), [OBK_StageExpDocument].[ExpBlankNumber]) AS [BlankNumber] --Номер бланка
	  ,[OBK_ZBKCopy].[ExtraditeDate] -- Дата выдачи Дубликата
	  ,[OBK_StageExpDocument].[ExpConclusionNumber] -- № сертификата
	  ,(SELECT CASE
	   WHEN [OBK_ZBKCopyBlankNumber].[Application] = 1 THEN N'Копия приложения ЗБК' 
	   WHEN [OBK_ZBKCopyBlankNumber].[Application] = 0 THEN N'Копия ЗБК' END) AS [ZBKType] --Тип документа
	  ,[OBK_RS_Products].[DrugFormFullName] AS [ProductName] --Наименование продукции
	  ,[OBK_Procunts_Series].[Series] AS [SeriesNumber] --№ серий
	  ,[OBK_Procunts_Series].[SeriesEndDate]	 --Срок годности
	  ,[OBK_Procunts_Series].[SeriesParty]			--Количество партии
	  ,[OBK_StageExpDocument].[ExpStartDate] --Дата регистрации
	  ,[OBK_StageExpDocument].[ExpEndDate] --Дата окончания 
	  ,[OBK_Ref_Type].[NameRu]  AS [RequestType]  --Тип заявки
	  ,NULL AS [ZBKReviewDate] --Дата отзыва ЗБК
	  ,[Units].[Name] AS [OrganName] --Орган по подтверждению
	  ,[Units].[Id] AS [OrganId] --Это вместо страны
	  ,[OBK_StageExpDocument].[ExpResult] --Отказано Да/Нет
	  ,[OBK_StageExpDocument].[ExpReasonNameRu]  AS [ExpReason] --Основание/Причина отказа
	  ,[OBK_AssessmentDeclaration].[TypeId] AS [RequestTypeId]  --Айди типа заявки
	  ,[OBK_RS_Products].[ProducerNameRu] AS [ProducerName]  --Производитель
	  ,[OBK_StageExpDocument].[RefuseDate] -- Дата отказа	
FROM [ncels].[dbo].[OBK_ZBKCopyBlankNumber]
LEFT JOIN [ncels].[dbo].[OBK_ZBKCopyBlank] ON [OBK_ZBKCopyBlankNumber].[OBK_ZBKCopyBlankId] = [OBK_ZBKCopyBlank].[Id]
LEFT JOIN [ncels].[dbo].[OBK_ZBKCopy] ON [OBK_ZBKCopyBlank].[ZBKCopyId] = [OBK_ZBKCopy].[Id]
LEFT JOIN [ncels].[dbo].[OBK_StageExpDocument] ON [OBK_ZBKCopy].[OBK_StageExpDocumentId] = [OBK_StageExpDocument].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Ref_Reason] ON [OBK_StageExpDocument].[RefReasonId] = [OBK_Ref_Reason].[Id]
LEFT JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Ref_Type] ON [OBK_AssessmentDeclaration].[TypeId] = [OBK_Ref_Type].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
LEFT JOIN [ncels].[dbo].[Units] ON [OBK_Contract].[ExpertOrganization] = [Units].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_StageExpDocument].[ProductSeriesId] = [OBK_Procunts_Series].[Id]
LEFT JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_Procunts_Series].[OBK_RS_ProductsId] = [OBK_RS_Products].[Id]

UNION ALL

SELECT [OBK_StageExpDocument].[Id] --Id 
	  ,CONVERT(varchar(50), [OBK_StageExpDocument].[ExpBlankNumber]) AS [BlankNumber]--Номер бланка
      ,NULL AS [ExtraditeDate] -- Дата выдачи Дубликата
	  ,[OBK_StageExpDocument].[ExpConclusionNumber] -- № сертификата
	  ,(SELECT N'ЗБК') AS [ZBKType] --Тип документа
	  ,[OBK_RS_Products].[DrugFormFullName] AS [ProductName]  --Наименование продукции
	  ,[OBK_Procunts_Series].[Series] AS [SeriesNumber] --№ серий
	  ,[OBK_Procunts_Series].[SeriesEndDate]	 --Срок годности
	  ,[OBK_Procunts_Series].[SeriesParty]			--Количество партии
	  ,[OBK_StageExpDocument].[ExpStartDate] --Дата регистрации
	  ,[OBK_StageExpDocument].[ExpEndDate] --Дата окончания 
	  ,[OBK_Ref_Type].[NameRu] AS [RequestType] --Тип заявки
	  ,NULL AS [ZBKReviewDate] --Дата отзыва ЗБК
	  ,[Units].[Name] AS [OrganName] --Орган по подтверждению
	  ,[Units].[Id] AS [OrganId] --Это вместо страны
	  ,[OBK_StageExpDocument].[ExpResult] --Отказано Да/Нет
	  ,[OBK_StageExpDocument].[ExpReasonNameRu]  AS [ExpReason] --Основание/Причина отказа
	  ,[OBK_AssessmentDeclaration].[TypeId] AS [RequestTypeId]  --Айди типа заявки
	  ,[OBK_RS_Products].[ProducerNameRu] AS [ProducerName]  --Производитель
	  ,[OBK_StageExpDocument].[RefuseDate] -- Дата отказа	
FROM [ncels].[dbo].[OBK_StageExpDocument]
LEFT JOIN [ncels].[dbo].[OBK_AssessmentDeclaration] ON [OBK_StageExpDocument].[AssessmentDeclarationId] = [OBK_AssessmentDeclaration].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Ref_Type] ON [OBK_AssessmentDeclaration].[TypeId] = [OBK_Ref_Type].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Contract] ON [OBK_AssessmentDeclaration].[ContractId] = [OBK_Contract].[Id]
LEFT JOIN [ncels].[dbo].[Units] ON [OBK_Contract].[ExpertOrganization] = [Units].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Procunts_Series] ON [OBK_StageExpDocument].[ProductSeriesId] = [OBK_Procunts_Series].[Id]
LEFT JOIN [ncels].[dbo].[OBK_RS_Products] ON [OBK_Procunts_Series].[OBK_RS_ProductsId] = [OBK_RS_Products].[Id]
LEFT JOIN [ncels].[dbo].[OBK_Ref_Reason] ON [OBK_StageExpDocument].[RefReasonId] = [OBK_Ref_Reason].[Id]

GO


