USE [ncels]
GO

ALTER TABLE EXP_ExpertiseStage add [IsSuspended] [bit] NOT NULL DEFAULT (0);
ALTER TABLE EXP_ExpertiseStage add [SuspendedStartDate] [datetime] NULL;

GO