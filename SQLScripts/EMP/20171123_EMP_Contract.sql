USE [ncels]
GO

ALTER TABLE [dbo].[EMP_Contract] DROP CONSTRAINT [FK_EMP_Contract_OBK_PayerContact]
GO

ALTER TABLE [dbo].[EMP_Contract] DROP CONSTRAINT [FK_EMP_Contract_OBK_ManufacturContact]
GO

ALTER TABLE [dbo].[EMP_Contract] DROP CONSTRAINT [FK_EMP_Contract_OBK_DeclarantPayer]
GO

ALTER TABLE [dbo].[EMP_Contract] DROP CONSTRAINT [FK_EMP_Contract_OBK_DeclarantManufactur]
GO

ALTER TABLE [dbo].[EMP_Contract] DROP CONSTRAINT [FK_EMP_Contract_OBK_DeclarantContact]
GO

ALTER TABLE [dbo].[EMP_Contract] DROP CONSTRAINT [FK_EMP_Contract_OBK_Declarant]
GO

ALTER TABLE [dbo].[EMP_Contract] DROP CONSTRAINT [FK_EMP_Contract_Employees]
GO

/****** Object:  Table [dbo].[EMP_Contract]    Script Date: 23.11.2017 11:21:33 ******/
DROP TABLE [dbo].[EMP_Contract]
GO

/****** Object:  Table [dbo].[EMP_Contract]    Script Date: 23.11.2017 11:21:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_Contract](
	[Id] [uniqueidentifier] NOT NULL,
	[Number] [nvarchar](30) NOT NULL,
	[HolderType] [uniqueidentifier] NULL,
	[Status] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[MedicalDeviceName] [nvarchar](255) NULL,
	[EmployeeId] [uniqueidentifier] NULL,
	[DeclarantId] [uniqueidentifier] NULL,
	[ManufacturId] [uniqueidentifier] NULL,
	[PayerId] [uniqueidentifier] NULL,
	[DeclarantContactId] [uniqueidentifier] NULL,
	[ManufacturContactId] [uniqueidentifier] NULL,
	[PayerContactId] [uniqueidentifier] NULL,
	[ParentId] [uniqueidentifier] NULL,
	[ExpertOrganization] [uniqueidentifier] NULL,
	[Signer] [uniqueidentifier] NULL,
	[SendDate] [datetime] NULL,
 CONSTRAINT [PK_EMP_Contract] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EMP_Contract]  WITH CHECK ADD  CONSTRAINT [FK_EMP_Contract_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[EMP_Contract] CHECK CONSTRAINT [FK_EMP_Contract_Employees]
GO

ALTER TABLE [dbo].[EMP_Contract]  WITH CHECK ADD  CONSTRAINT [FK_EMP_Contract_OBK_Declarant] FOREIGN KEY([DeclarantId])
REFERENCES [dbo].[OBK_Declarant] ([Id])
GO

ALTER TABLE [dbo].[EMP_Contract] CHECK CONSTRAINT [FK_EMP_Contract_OBK_Declarant]
GO

ALTER TABLE [dbo].[EMP_Contract]  WITH CHECK ADD  CONSTRAINT [FK_EMP_Contract_OBK_DeclarantContact] FOREIGN KEY([DeclarantContactId])
REFERENCES [dbo].[OBK_DeclarantContact] ([Id])
GO

ALTER TABLE [dbo].[EMP_Contract] CHECK CONSTRAINT [FK_EMP_Contract_OBK_DeclarantContact]
GO

ALTER TABLE [dbo].[EMP_Contract]  WITH CHECK ADD  CONSTRAINT [FK_EMP_Contract_OBK_DeclarantManufactur] FOREIGN KEY([ManufacturId])
REFERENCES [dbo].[OBK_Declarant] ([Id])
GO

ALTER TABLE [dbo].[EMP_Contract] CHECK CONSTRAINT [FK_EMP_Contract_OBK_DeclarantManufactur]
GO

ALTER TABLE [dbo].[EMP_Contract]  WITH CHECK ADD  CONSTRAINT [FK_EMP_Contract_OBK_DeclarantPayer] FOREIGN KEY([PayerId])
REFERENCES [dbo].[OBK_Declarant] ([Id])
GO

ALTER TABLE [dbo].[EMP_Contract] CHECK CONSTRAINT [FK_EMP_Contract_OBK_DeclarantPayer]
GO

ALTER TABLE [dbo].[EMP_Contract]  WITH CHECK ADD  CONSTRAINT [FK_EMP_Contract_OBK_ManufacturContact] FOREIGN KEY([ManufacturContactId])
REFERENCES [dbo].[OBK_DeclarantContact] ([Id])
GO

ALTER TABLE [dbo].[EMP_Contract] CHECK CONSTRAINT [FK_EMP_Contract_OBK_ManufacturContact]
GO

ALTER TABLE [dbo].[EMP_Contract]  WITH CHECK ADD  CONSTRAINT [FK_EMP_Contract_OBK_PayerContact] FOREIGN KEY([PayerContactId])
REFERENCES [dbo].[OBK_DeclarantContact] ([Id])
GO

ALTER TABLE [dbo].[EMP_Contract] CHECK CONSTRAINT [FK_EMP_Contract_OBK_PayerContact]
GO


