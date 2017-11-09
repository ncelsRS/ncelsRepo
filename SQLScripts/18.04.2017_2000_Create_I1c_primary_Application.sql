
DROP TABLE [dbo].[I1c_primary_ApplicationPaymentState]
GO

DROP TABLE [dbo].[I1c_primary_ApplicationRefuseState]
GO

DROP TABLE [dbo].[I1c_primary_PriceListElements]
GO

DROP TABLE [dbo].[I1c_primary_ApplicationElements]
GO

DROP TABLE [dbo].[I1c_primary_Applications]
GO


IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'I1c_primary_RequestList'))
BEGIN
    DROP TABLE [dbo].[I1c_primary_RequestList]	
END
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[I1c_primary_Applications](
	[Id] [uniqueidentifier] NOT NULL,
	[ExportDatetime] [datetime] NOT NULL,
	[ImportDatetime] [datetime] NULL,
	[ApplicationId] [uniqueidentifier] NULL,
	[ApplicationNumber] [nvarchar](450) NULL,
	[ApplicationDatetime] [datetime] NULL,
	[Producer] [nvarchar](4000) NULL,
	[ProducerId] [uniqueidentifier] NULL,
	[Applicant] [nvarchar](4000) NULL,
	[ApplicantId] [uniqueidentifier] NULL,
	[ContractNumber] [nvarchar](450) NULL,
	[ContractStartDate] [date] NULL,
	[ContractEndDate] [date] NULL,
	[ContractId] [uniqueidentifier] NULL,
	[DoverennostNumber] [nvarchar](500) NULL,
	[DoverennostCreatedDate] [date] NULL,
	[DoverennostExpiryDate] [date] NULL,
	[Address] [nvarchar](4000) NULL,
	[Phone] [nvarchar](500) NULL,
	[Payer] [nvarchar](4000) NULL,
	[PayerId] [uniqueidentifier] NULL,
	[PayerAddress] [nvarchar](4000) NULL,
	[PayerBank] [nvarchar](4000) NULL,
	[PayerAccount] [nvarchar](500) NULL,
	[PayerBIK] [nvarchar](500) NULL,
	[PayerSWIFT] [nvarchar](500) NULL,
	[PayerBIN] [nvarchar](500) NULL,
	[PayerIIN] [nvarchar](500) NULL,
	[PayerCurrency] [nvarchar](500) NULL,
	[PayerCurrencyId] [uniqueidentifier] NULL,
	[Country] [nvarchar](500) NULL,
	[CountryId] [uniqueidentifier] NULL,
	[IsResident] [bit] NULL,
	[IsLegal] [bit] NULL,
	[InvoiceNumber1C] [nvarchar](500) NULL,
	[InvoiceDatetime1C] [datetime] NULL,
	[StatementNumber] [nvarchar](500) NULL,
	[DrugTradeName] [nvarchar](500) NULL,
	[DrugTradeNameKz] [nvarchar](500) NULL,
	[DrugPackage] [nvarchar](500) NULL,
	[DrugPackageKz] [nvarchar](500) NULL,
	[Type] [nvarchar](500) NULL,
	[TypeId] [uniqueidentifier] NULL,
	[TotalPrice] [decimal](18, 2) NULL,
 CONSTRAINT [PK_I1c_primary_Applications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[I1c_primary_Applications] ADD  CONSTRAINT [DF_I1c_primary_Applications_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[I1c_primary_Applications] ADD  CONSTRAINT [DF_I1c_primary_Applications_ExportDatetime]  DEFAULT (getdate()) FOR [ExportDatetime]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата и время выгрузки в промежуточную базу' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'ExportDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата и время загрузки в 1С' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'ImportDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ид направления на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'ApplicationId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'№ направления на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'ApplicationNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата направления на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'ApplicationDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Производитель' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'Producer'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИД производителя в БД Экспертизы' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'ProducerId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Наименование Заявителя' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'Applicant'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИД заявителя в БД Экспертизы' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'ApplicantId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'ContractNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата начала договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'ContractStartDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата завершения договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'ContractEndDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИД договора в БД Экспертизы' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'ContractId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'№ доверенности' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'DoverennostNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата начала доверенности' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'DoverennostCreatedDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата окончания доверенности' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'DoverennostExpiryDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Адрес' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'Address'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Телефон' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'Phone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Наименование Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'Payer'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИД Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'PayerId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Адрес Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'PayerAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Банк Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'PayerBank'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Расчетный счет Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'PayerAccount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'БИК Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'PayerBIK'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'SWIFT Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'PayerSWIFT'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'БИН Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'PayerBIN'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИИН Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'PayerIIN'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Валюта Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'PayerCurrency'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИД Валюта Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'PayerCurrencyId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Страна' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'Country'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИД страны' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'CountryId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'признак резидент РК или не резидент РК' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'IsResident'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'признак физическое лицо или юридическое лицо' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'IsLegal'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер счета на оплату будет передаваться из 1С' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'InvoiceNumber1C'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата счета на оплату, возвратившееся из 1С' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'InvoiceDatetime1C'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер заявления ЛС' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'StatementNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Торговое название' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'DrugTradeName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Торговое название каз' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'DrugTradeNameKz'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Упаковка' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'DrugPackage'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Упаковка каз' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'DrugPackageKz'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Тип' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'Type'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Тип ИД' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'TypeId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ОБщая сумма для оплаты' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_Applications', @level2type=N'COLUMN',@level2name=N'TotalPrice'
GO









CREATE TABLE [dbo].[I1c_primary_ApplicationElements](
	[Id] [uniqueidentifier] NOT NULL,
	[Dosage] [decimal](18, 2) NULL,
	[MeasureName] [nvarchar](500) NULL,
	[RegNumber] [nvarchar](50) NULL,
	[ConcentrationRu] [nvarchar](500) NULL,
	[ConcentrationKz] [nvarchar](500) NULL,
	[refApplication] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_I1c_primary_RequestList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[I1c_primary_ApplicationElements] ADD  CONSTRAINT [DF_I1c_primary_RequestList_Id]  DEFAULT (newid()) FOR [Id]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дозировка' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ApplicationElements', @level2type=N'COLUMN',@level2name=N'Dosage'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Единица измерения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ApplicationElements', @level2type=N'COLUMN',@level2name=N'MeasureName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'регистрационный номер заявки в БД НЦЭЛС' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ApplicationElements', @level2type=N'COLUMN',@level2name=N'RegNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Концентрация' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ApplicationElements', @level2type=N'COLUMN',@level2name=N'ConcentrationRu'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Концентрация каз' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ApplicationElements', @level2type=N'COLUMN',@level2name=N'ConcentrationKz'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ссылка на Направление на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ApplicationElements', @level2type=N'COLUMN',@level2name=N'refApplication'
GO




CREATE TABLE [dbo].[I1c_primary_PriceListElements](
	[Id] [uniqueidentifier] NOT NULL,
	[PriceListId] [uniqueidentifier] NULL,
	[PriceListName] [nvarchar](500) NULL,
	[PriceListNameKz] [nvarchar](500) NULL,
	[Quantity] [int] NULL,
	[Price] [decimal](18, 2) NULL,
	[refApplication] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_I1c_primary_PriceListElements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[I1c_primary_PriceListElements] ADD  CONSTRAINT [DF_I1c_primary_PriceListElements_Id]  DEFAULT (newid()) FOR [Id]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИД прейскуранта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_PriceListElements', @level2type=N'COLUMN',@level2name=N'PriceListId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Наименование прейскуранта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_PriceListElements', @level2type=N'COLUMN',@level2name=N'PriceListName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Наименование прейскуранта на каз' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_PriceListElements', @level2type=N'COLUMN',@level2name=N'PriceListNameKz'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'количество прейскурантов' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_PriceListElements', @level2type=N'COLUMN',@level2name=N'Quantity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'цена за единицу прескуранта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_PriceListElements', @level2type=N'COLUMN',@level2name=N'Price'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ссылка на Направление на оплату (Applications)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_PriceListElements', @level2type=N'COLUMN',@level2name=N'refApplication'
GO







CREATE TABLE [dbo].[I1c_primary_ApplicationRefuseState](
	[Id] [uniqueidentifier] NOT NULL,
	[ApplicationNumber] [nvarchar](500) NULL,
	[ApplicationDatetime] [datetime] NULL,
	[IsRefuse] [bit] NULL,
	[RefuseDatetime] [datetime] NULL,
	[refApplication] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_I1c_primary_ApplicationRefuseState] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[I1c_primary_ApplicationRefuseState] ADD  CONSTRAINT [DF_I1c_primary_ApplicationRefuseState_Id]  DEFAULT (newid()) FOR [Id]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'№ направления на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ApplicationRefuseState', @level2type=N'COLUMN',@level2name=N'ApplicationNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата направления на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ApplicationRefuseState', @level2type=N'COLUMN',@level2name=N'ApplicationDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'признак отказа' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ApplicationRefuseState', @level2type=N'COLUMN',@level2name=N'IsRefuse'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'дата отказа' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ApplicationRefuseState', @level2type=N'COLUMN',@level2name=N'RefuseDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ссылка на Направление на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ApplicationRefuseState', @level2type=N'COLUMN',@level2name=N'refApplication'
GO





CREATE TABLE [dbo].[I1c_primary_ApplicationPaymentState](
	[Id] [uniqueidentifier] NOT NULL,
	[ApplicationNumber] [nvarchar](500) NULL,
	[ApplicationDatetime] [datetime] NULL,
	[IsPaid] [bit] NULL,
	[PaymentDatetime] [datetime] NULL,
	[PaymentComment] [nvarchar](max) NULL,
 CONSTRAINT [PK_I1c_primary_ApplicationPaymentState] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[I1c_primary_ApplicationPaymentState] ADD  CONSTRAINT [DF_I1c_primary_ApplicationPaymentState_Id]  DEFAULT (newid()) FOR [Id]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер направления на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ApplicationPaymentState', @level2type=N'COLUMN',@level2name=N'ApplicationNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата направления на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ApplicationPaymentState', @level2type=N'COLUMN',@level2name=N'ApplicationDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Признак оплаты' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ApplicationPaymentState', @level2type=N'COLUMN',@level2name=N'IsPaid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата оплаты' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ApplicationPaymentState', @level2type=N'COLUMN',@level2name=N'PaymentDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Комментарий оплаты' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ApplicationPaymentState', @level2type=N'COLUMN',@level2name=N'PaymentComment'
GO