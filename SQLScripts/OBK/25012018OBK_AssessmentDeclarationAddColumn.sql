 USE [ncels]

  ALTER TABLE [ncels].[dbo].[OBK_AssessmentDeclaration]
  ADD ZBKTaken BIT NULL

  ALTER TABLE [ncels].[dbo].[OBK_AssessmentDeclaration]
  ADD ReceiverFIO NVARCHAR(500) NULL

  ALTER TABLE [ncels].[dbo].[OBK_AssessmentDeclaration]
  ADD ReceivedDate DATETIME NULL

  ALTER TABLE [ncels].[dbo].[OBK_ZBKCopy]
  ADD zbkCopyReadyDate DATETIME NULL

  ALTER TABLE [ncels].[dbo].[OBK_AssessmentDeclaration]
  ADD ExtraditeDate DATETIME NULL