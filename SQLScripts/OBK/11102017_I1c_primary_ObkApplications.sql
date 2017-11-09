USE [ncelsProd]
GO

/****** Object:  Table [dbo].[I1c_primary_ObkApplications]    Script Date: 11.10.2017 16:33:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[I1c_primary_ObkApplications](
	[Id] [uniqueidentifier] NOT NULL,
	[Organization] [nvarchar](450) NULL,
	[OrganizationCode] [nvarchar](450) NULL,
	[ExportDatetime] [datetime] NOT NULL,
	[ContractNumber] [nvarchar](450) NULL,
	[ContractStartDate] [date] NULL,
	[ContractEndDate] [date] NULL,
	[ContractId] [uniqueidentifier] NULL,
	[DoverennostNumber] [nvarchar](500) NULL,
	[DoverennostCreatedDate] [date] NULL,
	[DoverennostExpiryDate] [date] NULL,
	[Payer] [nvarchar](4000) NULL,
	[PayerId] [uniqueidentifier] NULL,
	[PayerAddress] [nvarchar](4000) NULL,
	[PayerPhone] [nvarchar](500) NULL,
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
	[Type] [nvarchar](500) NULL,
	[TypeId] [int] NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[InvoiceNumber1C] [nvarchar](512) NULL,
	[InvoiceDatetime1C] [datetime] null
 CONSTRAINT [PK_I1c_primary_ObkApplications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[I1c_primary_ObkApplications] ADD  CONSTRAINT [DF_I1c_primary_ObkApplications_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[I1c_primary_ObkApplications] ADD  CONSTRAINT [DF_I1c_primary_ObkApplications_ExportDatetime]  DEFAULT (getdate()) FOR [ExportDatetime]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата и время выгрузки в промежуточную базу' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'ExportDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Организация (НЦЭЛС или ТФ)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'Organization'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код Организации' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'OrganizationCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'ContractNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата начала договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'ContractStartDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата завершения договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'ContractEndDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИД договора в БД Экспертизы' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'ContractId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'№ доверенности' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'DoverennostNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата начала доверенности' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'DoverennostCreatedDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата окончания доверенности' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'DoverennostExpiryDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Наименование Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'Payer'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИД Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'PayerId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Адрес Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'PayerAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Телефон Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'PayerPhone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Банк Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'PayerBank'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Расчетный счет Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'PayerAccount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'БИК Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'PayerBIK'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'SWIFT Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'PayerSWIFT'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'БИН Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'PayerBIN'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИИН Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'PayerIIN'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Валюта Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'PayerCurrency'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИД Валюта Плательщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'PayerCurrencyId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Страна' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'Country'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИД страны' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'CountryId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'признак резидент РК или не резидент РК' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'IsResident'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'признак физическое лицо или юридическое лицо' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'IsLegal'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Тип' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'Type'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ОБщая сумма для оплаты' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplications', @level2type=N'COLUMN',@level2name=N'TotalPrice'
GO


