SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RequestOrders](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[OrderType] [int] NOT NULL,
	[OrderYear] [int] NOT NULL,
	[OrderNumber] [nvarchar](500) NULL,
	[IsDeleted] [bit] NOT NULL
 CONSTRAINT [PK_RequestOrders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[RequestOrders] ADD  CONSTRAINT [DF_RequestOrders_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[RequestOrders] ADD  CONSTRAINT [DF_RequestOrders_OrderType]  DEFAULT ((0)) FOR [OrderType]
GO

ALTER TABLE [dbo].[RequestOrders] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO


