USE [ncels]
GO

/****** Object:  Table [dbo].[EMP_DirectionToPayments]    Script Date: 25.12.2017 14:06:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_DirectionToPayments](
	[Id] [uniqueidentifier] NOT NULL,
	[Number] [nvarchar](255) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[DirectionDate] [datetime] NOT NULL,
	[ContractId] [uniqueidentifier] NOT NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[PayerId] [uniqueidentifier] NULL,
	[PayerValue] [nvarchar](4000) NULL,
	[CreateEmployeeId] [uniqueidentifier] NOT NULL,
	[CreateEmployeeValues] [nvarchar](1024) NOT NULL,
	[DeleteDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[StatusId] [uniqueidentifier] NOT NULL,
	[InvoiceNumber] [nvarchar](512) NULL,
	[InvoiceNumber1C] [nvarchar](512) NULL,
	[InvoiceDate1C] [datetime] NULL,
	[IsPaid] [bit] NOT NULL,
	[IsNotFullPaid] [bit] NOT NULL,
	[PaymentDate] [datetime] NULL,
	[PaymentValue] [decimal](18, 2) NULL,
	[PaymentBill] [decimal](18, 2) NULL,
	[SendNotification] [nvarchar](20) NULL,
	[ModifyDate] [datetime] NULL,
 CONSTRAINT [PK_EMP_DirectionToPayments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EMP_DirectionToPayments]  WITH CHECK ADD  CONSTRAINT [FK_EMP_DirectionToPayments_ContractId_EMP_Contract_Id] FOREIGN KEY([ContractId])
REFERENCES [dbo].[EMP_Contract] ([Id])
GO

ALTER TABLE [dbo].[EMP_DirectionToPayments] CHECK CONSTRAINT [FK_EMP_DirectionToPayments_ContractId_EMP_Contract_Id]
GO

ALTER TABLE [dbo].[EMP_DirectionToPayments]  WITH CHECK ADD  CONSTRAINT [FK_EMP_DirectionToPayments_CreateEmployeeId_Employee_Id] FOREIGN KEY([CreateEmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[EMP_DirectionToPayments] CHECK CONSTRAINT [FK_EMP_DirectionToPayments_CreateEmployeeId_Employee_Id]
GO

ALTER TABLE [dbo].[EMP_DirectionToPayments]  WITH CHECK ADD  CONSTRAINT [FK_EMP_DirectionToPayments_PayerId_OBK_Declarant_Id] FOREIGN KEY([PayerId])
REFERENCES [dbo].[OBK_Declarant] ([Id])
GO

ALTER TABLE [dbo].[EMP_DirectionToPayments] CHECK CONSTRAINT [FK_EMP_DirectionToPayments_PayerId_OBK_Declarant_Id]
GO

ALTER TABLE [dbo].[EMP_DirectionToPayments]  WITH CHECK ADD  CONSTRAINT [FK_EMP_DirectionToPayments_StatusId_EBK_RefPaymentStatus_Id] FOREIGN KEY([StatusId])
REFERENCES [dbo].[OBK_Ref_PaymentStatus] ([Id])
GO

ALTER TABLE [dbo].[EMP_DirectionToPayments] CHECK CONSTRAINT [FK_EMP_DirectionToPayments_StatusId_EBK_RefPaymentStatus_Id]
GO


