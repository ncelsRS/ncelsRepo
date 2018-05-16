USE [ncels]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PP_ProtocolComissionMembers](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ProtocolId] [uniqueidentifier] NOT NULL,
	[EmployeeId] [uniqueidentifier] NOT NULL

 CONSTRAINT [PK_PP_ProtocolComissionMembers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PP_ProtocolComissionMembers] ADD  CONSTRAINT [DF_PP_ProtocolComissionMembers_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[PP_ProtocolComissionMembers]  WITH CHECK ADD  CONSTRAINT [FK_PP_ProtocolComissionMembers_Protocol] FOREIGN KEY([ProtocolId])
REFERENCES [dbo].[PP_Protocols] ([Id])
GO

ALTER TABLE [dbo].[PP_ProtocolComissionMembers] CHECK CONSTRAINT [FK_PP_ProtocolComissionMembers_Protocol]
GO

ALTER TABLE [dbo].[PP_ProtocolComissionMembers]  WITH CHECK ADD  CONSTRAINT [FK_PP_ProtocolComissionMembers_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[PP_ProtocolComissionMembers] CHECK CONSTRAINT [FK_PP_ProtocolComissionMembers_Employee]
GO