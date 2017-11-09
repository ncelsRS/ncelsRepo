USE [ncelsProd]
GO

/****** Object:  Table [dbo].[I1c_act_ObkApplication]    Script Date: 01.11.2017 17:43:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[I1c_act_ObkApplication](
	[Id] [uniqueidentifier] NOT NULL,
	[Number] [nvarchar](500) NULL,
	[CreateDatetime] [datetime] NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[InvoiceNumber1C] [nvarchar](512) NULL,
	[InvoiceDatetime1C] [datetime] NULL,
	[ExportDatetime] [datetime] NOT NULL,
	[StageId] [int] NULL,
	[StageCode] [nvarchar](500) NULL,
	[StageValue] [nvarchar](500) NULL,
	[ActNumber1C] [nvarchar](500) NULL,
	[ActDate1C] [datetime] NULL,
	[ActReturnedBack] [bit] NULL
 CONSTRAINT [PK_I1c_ObkCertificationOfCompletion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[I1c_act_ObkApplication] ADD  CONSTRAINT [DF_I1c_ObkCertificationOfCompletion_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[I1c_act_ObkApplication] ADD  CONSTRAINT [DF_I1c_ObkCertificationOfCompletion_ExportDatetim]  DEFAULT (getdate()) FOR [ExportDatetime]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер акта в ОБК' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_ObkApplication', @level2type=N'COLUMN',@level2name=N'Number'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата акта в ОБК' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_ObkApplication', @level2type=N'COLUMN',@level2name=N'CreateDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Сумма оплаты по акту' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_ObkApplication', @level2type=N'COLUMN',@level2name=N'TotalPrice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер счета на оплату (который ранее передан из 1С)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_ObkApplication', @level2type=N'COLUMN',@level2name=N'InvoiceNumber1C'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата счета на оплату (который ранее передан из 1С)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_ObkApplication', @level2type=N'COLUMN',@level2name=N'InvoiceDatetime1C'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата завгрузки в промежуточную БД' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_ObkApplication', @level2type=N'COLUMN',@level2name=N'ExportDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИД этапа с которого отправлен Акт' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_ObkApplication', @level2type=N'COLUMN',@level2name=N'StageId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код этапа с которого отправлен Акт' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_ObkApplication', @level2type=N'COLUMN',@level2name=N'StageCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Название этапа с которого отправлен Акт' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_ObkApplication', @level2type=N'COLUMN',@level2name=N'StageValue'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер акта из 1С' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_ObkApplication', @level2type=N'COLUMN',@level2name=N'ActNumber1C'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата акта из 1С' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_ObkApplication', @level2type=N'COLUMN',@level2name=N'ActDate1C'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Признак того что принесли акт' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_ObkApplication', @level2type=N'COLUMN',@level2name=N'ActReturnedBack'
GO