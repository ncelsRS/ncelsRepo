USE [ncels]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE [dbo].[Prices] ADD [RequestDate] [date] NULL;
ALTER TABLE [dbo].[Prices] ADD [Description] [nvarchar](500) NULL;

GO


