USE [ncels]
GO

alter table PriceProjects add IsArchive [bit] NOT NULL default 0;

GO