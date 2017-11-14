DROP TABLE [dbo].[I1c_trl_DirectionElements]
GO

DROP TABLE [dbo].[I1c_trl_DirectionsToPay]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[I1c_trl_Application](
	[Id] [uniqueidentifier] NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[ApplicationNumber] [nvarchar](4000) NULL,
	[ApplicationDatetime] [datetime] NULL,
	[TranslatorFIO] [nvarchar](4000) NULL,
	[TranslatorId] [uniqueidentifier] NULL,
	[PageQuantity] [int] NULL,
	[PagePrice] [decimal](18, 2) NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[InvoiceCode_1C] [nvarchar](512) NULL,
	[InvoiceNumber_1C] [nvarchar](512) NULL,
	[InvoiceDatetime_1C] [datetime] NULL,
	[ExportDatetime] [datetime] NOT NULL,
	[ImportDatetime] [datetime] NULL,
	[refPrimaryApplication] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_i1c_DirectionsToPay] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[I1c_trl_Application] ADD  CONSTRAINT [DF__i1c_Directio__Id__7B5B524B]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[I1c_trl_Application] ADD  CONSTRAINT [DF_i1c_DirectionToPay_CreateDatetime]  DEFAULT (getdate()) FOR [ExportDatetime]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИД Направления на оплату на перевод' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_Application', @level2type=N'COLUMN',@level2name=N'ApplicationId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'№ направления на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_Application', @level2type=N'COLUMN',@level2name=N'ApplicationNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата направления на оплату по переводам' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_Application', @level2type=N'COLUMN',@level2name=N'ApplicationDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Переводчик' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_Application', @level2type=N'COLUMN',@level2name=N'TranslatorFIO'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИД переводчика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_Application', @level2type=N'COLUMN',@level2name=N'TranslatorId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'количество страниц' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_Application', @level2type=N'COLUMN',@level2name=N'PageQuantity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'цена за страницу' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_Application', @level2type=N'COLUMN',@level2name=N'PagePrice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'общая стоимость' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_Application', @level2type=N'COLUMN',@level2name=N'TotalPrice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код счета на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_Application', @level2type=N'COLUMN',@level2name=N'InvoiceCode_1C'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер счета на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_Application', @level2type=N'COLUMN',@level2name=N'InvoiceNumber_1C'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата счета на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_Application', @level2type=N'COLUMN',@level2name=N'InvoiceDatetime_1C'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата и время выгрузки в промежуточную базу' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_Application', @level2type=N'COLUMN',@level2name=N'ExportDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата и время загрузки в 1C' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_Application', @level2type=N'COLUMN',@level2name=N'ImportDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ссылка на I1c_primary_Applications по полю ApplicationId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_Application', @level2type=N'COLUMN',@level2name=N'refPrimaryApplication'
GO




CREATE TABLE [dbo].[I1c_trl_ApplicationPaymentState](
	[Id] [uniqueidentifier] NOT NULL,
	[ApplicationNumber] [nvarchar](500) NULL,
	[ApplicationDatetime] [datetime] NULL,
	[IsPaid] [bit] NULL,
	[PaymentDatetime] [datetime] NULL,
	[PaymentComment] [nvarchar](max) NULL,
 CONSTRAINT [PK_I1c_trl_ApplicationPaymentState] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[I1c_trl_ApplicationPaymentState] ADD  CONSTRAINT [DF_I1c_trl_ApplicationPaymentState_Id]  DEFAULT (newid()) FOR [Id]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер направления на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_ApplicationPaymentState', @level2type=N'COLUMN',@level2name=N'ApplicationNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата направления на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_ApplicationPaymentState', @level2type=N'COLUMN',@level2name=N'ApplicationDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Признак оплаты' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_ApplicationPaymentState', @level2type=N'COLUMN',@level2name=N'IsPaid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата оплаты' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_ApplicationPaymentState', @level2type=N'COLUMN',@level2name=N'PaymentDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Комментарий оплаты' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_ApplicationPaymentState', @level2type=N'COLUMN',@level2name=N'PaymentComment'
GO


CREATE TABLE [dbo].[I1c_trl_ApplicationRefuseState](
	[Id] [uniqueidentifier] NOT NULL,
	[ApplicationNumber] [nvarchar](500) NULL,
	[ApplicationDatetime] [datetime] NULL,
	[IsRefuse] [bit] NULL,
	[RefuseDatetime] [datetime] NULL,
	[refApplication] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_I1c_trl_ApplicationRefuseState] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[I1c_trl_ApplicationRefuseState] ADD  CONSTRAINT [DF_I1c_trl_ApplicationRefuseState_Id]  DEFAULT (newid()) FOR [Id]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'№ направления на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_ApplicationRefuseState', @level2type=N'COLUMN',@level2name=N'ApplicationNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата направления на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_ApplicationRefuseState', @level2type=N'COLUMN',@level2name=N'ApplicationDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'признак отказа' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_ApplicationRefuseState', @level2type=N'COLUMN',@level2name=N'IsRefuse'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'дата отказа' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_ApplicationRefuseState', @level2type=N'COLUMN',@level2name=N'RefuseDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ссылка на Направление на оплату' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_trl_ApplicationRefuseState', @level2type=N'COLUMN',@level2name=N'refApplication'
GO