USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_CertificateReferenceFieldHistory]    Script Date: 19.11.2017 16:12:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_CertificateReferenceFieldHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Controlld] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NULL,
	[ValueField] [nvarchar](max) NULL,
	[DisplayField] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[OBK_CertificateReferenceId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_OBK_CertificateReferenceFieldHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_CertificateReferenceFieldHistory]  WITH CHECK ADD  CONSTRAINT [FK_OBK_CertificateReferenceFieldHistory_OBK_CertificateReference] FOREIGN KEY([OBK_CertificateReferenceId])
REFERENCES [dbo].[OBK_CertificateReference] ([Id])
GO

ALTER TABLE [dbo].[OBK_CertificateReferenceFieldHistory] CHECK CONSTRAINT [FK_OBK_CertificateReferenceFieldHistory_OBK_CertificateReference]
GO


