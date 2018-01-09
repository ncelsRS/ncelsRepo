use ncels
ALTER TABLE [ncels].[dbo].[OBK_ZBKCopyStage] ADD SendToAccountant BIT NULL

ALTER TABLE [ncels].[dbo].[OBK_ZBKCopyStage] DROP COLUMN InOBK

ALTER TABLE [ncels].[dbo].[OBK_ZBKCopyStage] DROP COLUMN OBK_Completed


