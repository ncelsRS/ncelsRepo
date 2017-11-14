USE [ncelsProd]
GO

/****** Object:  Table [dbo].[OBK_DirectionToPayments]    Script Date: 11.10.2017 15:24:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_DirectionToPayments](
	[Id] [uniqueidentifier] NOT NULL,
	[Number] [nvarchar](512) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[DirectionDate] [datetime] NOT NULL,
	[ContractId] [uniqueidentifier] NOT NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[PayerId] [uniqueidentifier] NULL,
	[PayerValue] [nvarchar](4000) NULL,
	[CreateEmployeeId] [uniqueidentifier] NOT NULL,
	[CreateEmployeeValue] [nvarchar](1024) NOT NULL,
	[DeleteDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[StatusId] [uniqueidentifier] NOT NULL,
	[InvoiceNumber] [nvarchar](512) NULL,
 CONSTRAINT [PK_OBK_DirectionToPayments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_DirectionToPayments] ADD  DEFAULT ('1EA197E7-0746-45A4-9D91-AC73487E4DB2') FOR [StatusId]
GO

ALTER TABLE [dbo].[OBK_DirectionToPayments]  WITH CHECK ADD  CONSTRAINT [FK_OBK_DirectionToPayments_Employees] FOREIGN KEY([CreateEmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[OBK_DirectionToPayments] CHECK CONSTRAINT [FK_OBK_DirectionToPayments_Employees]
GO

ALTER TABLE [dbo].[OBK_DirectionToPayments]  WITH CHECK ADD  CONSTRAINT [FK_OBK_DirectionToPayments_OBK_Contract] FOREIGN KEY([ContractId])
REFERENCES [dbo].[OBK_Contract] ([Id])
GO

ALTER TABLE [dbo].[OBK_DirectionToPayments] CHECK CONSTRAINT [FK_OBK_DirectionToPayments_OBK_Contract]
GO

ALTER TABLE [dbo].[OBK_DirectionToPayments]  WITH CHECK ADD  CONSTRAINT [FK_OBK_DirectionToPayments_OBK_Declarant] FOREIGN KEY([PayerId])
REFERENCES [dbo].[OBK_Declarant] ([Id])
GO

ALTER TABLE [dbo].[OBK_DirectionToPayments] CHECK CONSTRAINT [FK_OBK_DirectionToPayments_OBK_Declarant]
GO


