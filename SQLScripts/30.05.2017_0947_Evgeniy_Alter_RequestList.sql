DELETE FROM [dbo].[RequestList]

ALTER TABLE [dbo].[RequestList] ADD [RequestOrderId] [uniqueidentifier] NULL
GO

ALTER TABLE [dbo].[RequestList]  WITH CHECK ADD  CONSTRAINT [FK_RequestList_RequestOrder] FOREIGN KEY([RequestOrderId])
REFERENCES [dbo].[RequestOrders] ([Id])
GO

ALTER TABLE [dbo].[RequestList] CHECK CONSTRAINT [FK_RequestList_RequestOrder]
GO
