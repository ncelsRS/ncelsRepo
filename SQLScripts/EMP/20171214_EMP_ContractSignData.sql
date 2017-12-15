USE [ncels]
GO

/****** Object:  Table [dbo].[EMP_ContractSignData]    Script Date: 14.12.2017 10:46:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_ContractSignData](
	[ContractId] [uniqueidentifier] NOT NULL,
	[ApplicationSign] [ntext] NOT NULL,
	[ApplicationSignDate] [datetime] NOT NULL,
	[CeoSign] [ntext] NULL,
	[CeoSignDate] [datetime] NULL,
 CONSTRAINT [PK_EMP_ContractSignData_1] PRIMARY KEY CLUSTERED 
(
	[ContractId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[EMP_ContractSignData]  WITH CHECK ADD  CONSTRAINT [FK_EMP_ContractSignData_ContractId_EMP_Contract_Id] FOREIGN KEY([ContractId])
REFERENCES [dbo].[EMP_Contract] ([Id])
GO

ALTER TABLE [dbo].[EMP_ContractSignData] CHECK CONSTRAINT [FK_EMP_ContractSignData_ContractId_EMP_Contract_Id]
GO


