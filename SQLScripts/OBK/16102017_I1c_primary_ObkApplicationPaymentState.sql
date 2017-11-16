USE [ncelsProd]
GO

/****** Object:  Table [dbo].[I1c_primary_ObkApplicationPaymentState]    Script Date: 19.10.2017 16:33:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[I1c_primary_ObkApplicationPaymentState](
	[Id] [uniqueidentifier] NOT NULL,
	[IsPaid] [bit] NULL,
	[PaymentDatetime] [datetime] NULL,
	[refContractId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_I1c_primary_ObkApplicationPaymentState] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[I1c_primary_ObkApplicationPaymentState] ADD  CONSTRAINT [DF_I1c_primary_ObkApplicationPaymentState_Id]  DEFAULT (newid()) FOR [Id]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Признак оплаты' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplicationPaymentState', @level2type=N'COLUMN',@level2name=N'IsPaid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Выставленная сумма по счету' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplicationPaymentState', @level2type=N'COLUMN',@level2name=N'PaymentBill'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ссылка на ИД договора' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_primary_ObkApplicationPaymentState', @level2type=N'COLUMN',@level2name=N'refContractId'
GO


