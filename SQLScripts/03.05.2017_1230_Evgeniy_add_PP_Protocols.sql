USE [ncels]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PP_Protocols](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Type] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Number] [nvarchar](500) NULL,
	[ProtocolDate] [datetime] NOT NULL,
	[OwnerId] [uniqueidentifier] NOT NULL,
	[ChiefId] [uniqueidentifier] NULL,
	[RequesterId] [uniqueidentifier] NULL,
	[RequesterName] [nvarchar](500) NULL,
	[AdditionalPersonName] [nvarchar](500) NULL,
	[IsImn] [bit] NOT NULL

 CONSTRAINT [PK_PP_Protocols] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PP_Protocols] ADD  DEFAULT ((0)) FOR [IsImn]
GO

ALTER TABLE [dbo].[PP_Protocols] ADD  CONSTRAINT [DF_PP_Protocols_Type]  DEFAULT ((0)) FOR [Type]
GO

ALTER TABLE [dbo].[PP_Protocols] ADD  CONSTRAINT [DF_PP_Protocols_Status]  DEFAULT ((0)) FOR [Status]
GO

ALTER TABLE [dbo].[PP_Protocols] ADD  CONSTRAINT [DF_PP_Protocols_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
