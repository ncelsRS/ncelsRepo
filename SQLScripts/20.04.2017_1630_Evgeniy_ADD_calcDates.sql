USE [ncels]
GO

ALTER TABLE [dbo].[Prices] ADD [CalcDateStart]  [date] NULL;  
ALTER TABLE [dbo].[Prices] ADD [CalcDateEnd]  [date] NULL;  

GO
