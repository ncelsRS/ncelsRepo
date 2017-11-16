
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[I1c_lims_ContractProducts](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [nvarchar](50) NULL,
	[Name] [nvarchar](500) NULL,
	[Unit] [nvarchar](500) NULL,
	[QuantityVolume] [decimal](18, 6) NULL,
	[ContractNumber] [nvarchar](500) NULL,
	[ExportDatetime] [datetime] NULL,
	[ImportDatetime] [datetime] NULL,
 CONSTRAINT [PK_I1c_lims_ContractProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[I1c_lims_Contracts]    Script Date: 23.05.2017 12:36:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[I1c_lims_Contracts](
	[Id] [uniqueidentifier] NOT NULL,
	[BIN] [nvarchar](50) NULL,
	[Name] [nchar](10) NULL,
	[ContractNumber] [nvarchar](500) NULL,
	[ContractDate] [datetime] NULL,
	[ContractDeliveryLastDate] [datetime] NULL,
	[ExportDatetime] [datetime] NULL,
	[ImportDatetime] [datetime] NULL,
 CONSTRAINT [PK_I1c_lims_Contracts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[I1c_lims_ContractProducts] ADD  CONSTRAINT [DF_I1c_lims_ContractProducts_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[I1c_lims_ContractProducts] ADD  CONSTRAINT [DF_Table_1_ExportDatetim]  DEFAULT (getdate()) FOR [ExportDatetime]
GO
ALTER TABLE [dbo].[I1c_lims_Contracts] ADD  CONSTRAINT [DF_I1c_lims_Contracts_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[I1c_lims_Contracts] ADD  CONSTRAINT [DF_I1c_lims_Contracts_ExportDatetime]  DEFAULT (getdate()) FOR [ExportDatetime]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИД товара' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_lims_ContractProducts', @level2type=N'COLUMN',@level2name=N'ProductId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Наименование товара' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_lims_ContractProducts', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Единица измерения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_lims_ContractProducts', @level2type=N'COLUMN',@level2name=N'Unit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Количество/объем поставляемого товара (остаток)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_lims_ContractProducts', @level2type=N'COLUMN',@level2name=N'QuantityVolume'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_lims_ContractProducts', @level2type=N'COLUMN',@level2name=N'ContractNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Метка времени экспорта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_lims_ContractProducts', @level2type=N'COLUMN',@level2name=N'ExportDatetime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Метка времени импорта' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_lims_ContractProducts', @level2type=N'COLUMN',@level2name=N'ImportDatetime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'БИН поставщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_lims_Contracts', @level2type=N'COLUMN',@level2name=N'BIN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Наименование поставщика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_lims_Contracts', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_lims_Contracts', @level2type=N'COLUMN',@level2name=N'ContractNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_lims_Contracts', @level2type=N'COLUMN',@level2name=N'ContractDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Последний срок поставки по договору' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_lims_Contracts', @level2type=N'COLUMN',@level2name=N'ContractDeliveryLastDate'
GO
