USE [ncels]
GO

/****** Object:  Table [dbo].[EMP_ContractHistory]    Script Date: 15.12.2017 15:58:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_ContractHistory](
	[Id] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NOT NULL,
	[EmployeeId] [uniqueidentifier] NOT NULL,
	[UnitName] [nvarchar](4000) NULL,
	[StatusId] [uniqueidentifier] NOT NULL,
	[RefuseReason] [nvarchar](4000) NULL,
	[ContractId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EMP_ContractHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EMP_ContractHistory]  WITH CHECK ADD  CONSTRAINT [FK_EMP_ContractHistory_StatusId_OBK_RefContractHistoryStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[OBK_Ref_ContractHistoryStatus] ([Id])
GO

ALTER TABLE [dbo].[EMP_ContractHistory] CHECK CONSTRAINT [FK_EMP_ContractHistory_StatusId_OBK_RefContractHistoryStatus]
GO


