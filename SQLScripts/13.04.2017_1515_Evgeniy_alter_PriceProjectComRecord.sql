USE [ncels]
GO

ALTER TABLE [dbo].[PriceProjectComRecord]  WITH CHECK ADD  CONSTRAINT [FK_PriceProjectComRecord_Employees] FOREIGN KEY([UserId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[PriceProjectComRecord] CHECK CONSTRAINT [FK_PriceProjectComRecord_Employees]
GO
