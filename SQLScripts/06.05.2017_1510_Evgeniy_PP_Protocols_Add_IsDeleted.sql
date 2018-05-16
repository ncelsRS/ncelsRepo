USE [ncels]
GO

ALTER TABLE PP_Protocols add [IsDeleted] [bit] NOT NULL DEFAULT (0);

GO