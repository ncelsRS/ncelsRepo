USE [ncels]
GO

/****** Object:  Table [dbo].[EMP_ContractExtHistory]    Script Date: 15.12.2017 14:41:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_ContractExtHistory](
	[Id] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[StatusId] [uniqueidentifier] NOT NULL,
	[ContractId] [uniqueidentifier] NOT NULL,
	[EmployeeId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_EMP_ContractExtHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EMP_ContractExtHistory]  WITH CHECK ADD  CONSTRAINT [FK_EMP_ContractExtHistory_ContractId_EMP_Contract_Id] FOREIGN KEY([ContractId])
REFERENCES [dbo].[EMP_Contract] ([Id])
GO

ALTER TABLE [dbo].[EMP_ContractExtHistory] CHECK CONSTRAINT [FK_EMP_ContractExtHistory_ContractId_EMP_Contract_Id]
GO

ALTER TABLE [dbo].[EMP_ContractExtHistory]  WITH CHECK ADD  CONSTRAINT [FK_EMP_ContractExtHistory_EmployeeId_Employees_Id] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[EMP_ContractExtHistory] CHECK CONSTRAINT [FK_EMP_ContractExtHistory_EmployeeId_Employees_Id]
GO

ALTER TABLE [dbo].[EMP_ContractExtHistory]  WITH CHECK ADD  CONSTRAINT [FK_EMP_ContractExtHistory_StatusId_EMP_ContractHistoryStatus_Id] FOREIGN KEY([StatusId])
REFERENCES [dbo].[EMP_Ref_ContractHistoryStatus] ([Id])
GO

ALTER TABLE [dbo].[EMP_ContractExtHistory] CHECK CONSTRAINT [FK_EMP_ContractExtHistory_StatusId_EMP_ContractHistoryStatus_Id]
GO


