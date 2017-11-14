USE [ncelsProd]
GO

/****** Object:  Table [dbo].[OBK_CertificateOfCompletion]    Script Date: 01.11.2017 18:24:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_CertificateOfCompletion](
	[Id] [uniqueidentifier] NOT NULL,
	[Number] [nvarchar](500) NULL,
	[ContractId] [uniqueidentifier] NOT NULL,
	[AssessmentDeclarationId] [uniqueidentifier] NOT NULL,
	[InvoiceNumber1C] [nvarchar](512) NULL,
	[InvoiceDatetime1C] [datetime] NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[CreateDate] [datetime] NULL,
	[SendDate] [datetime] NULL,
	[ActNumber1C] [nvarchar](500) NULL,
	[ActDate1C] [datetime] NULL,
	[ActReturnedBack] [bit] NULL,
	[SendNotification] [bit] NULL,
 CONSTRAINT [PK_OBK_CertificateOfCompletion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_CertificateOfCompletion]  WITH CHECK ADD  CONSTRAINT [FK_OBK_CertificateOfCompletion_OBK_AssessmentDeclaration] FOREIGN KEY([AssessmentDeclarationId])
REFERENCES [dbo].[OBK_AssessmentDeclaration] ([Id])
GO

ALTER TABLE [dbo].[OBK_CertificateOfCompletion] CHECK CONSTRAINT [FK_OBK_CertificateOfCompletion_OBK_AssessmentDeclaration]
GO

ALTER TABLE [dbo].[OBK_CertificateOfCompletion]  WITH CHECK ADD  CONSTRAINT [FK_OBK_CertificateOfCompletion_OBK_Contract] FOREIGN KEY([ContractId])
REFERENCES [dbo].[OBK_Contract] ([Id])
GO

ALTER TABLE [dbo].[OBK_CertificateOfCompletion] CHECK CONSTRAINT [FK_OBK_CertificateOfCompletion_OBK_Contract]
GO


