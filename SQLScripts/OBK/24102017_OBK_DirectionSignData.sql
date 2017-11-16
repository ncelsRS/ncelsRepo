USE [ncelsProd]
GO

/****** Object:  Table [dbo].[OBK_DirectionSignData]    Script Date: 24.10.2017 15:39:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_DirectionSignData](
	[DirectionToPaymentId] [uniqueidentifier] NOT NULL,
	[ExecutorId] [uniqueidentifier] NULL,
	[ExecutorSign] [ntext] NULL,
	[ExecutorSignDate] [datetime] NULL,
	[ChiefAccountantId] [uniqueidentifier] NULL,
	[ChiefAccountantSign] [ntext] NULL,
	[ChiefAccountantSignDate] [datetime] NULL,
 CONSTRAINT [PK_OBK_DirectionSignData] PRIMARY KEY CLUSTERED 
(
	[DirectionToPaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_DirectionSignData]  WITH CHECK ADD  CONSTRAINT [FK_OBK_DirectionSignData_DirectionToPaymentId_OBK_DirectionToPayments_Id] FOREIGN KEY([DirectionToPaymentId])
REFERENCES [dbo].[OBK_DirectionToPayments] ([Id])
GO

ALTER TABLE [dbo].[OBK_DirectionSignData] CHECK CONSTRAINT [FK_OBK_DirectionSignData_DirectionToPaymentId_OBK_DirectionToPayments_Id]
GO


