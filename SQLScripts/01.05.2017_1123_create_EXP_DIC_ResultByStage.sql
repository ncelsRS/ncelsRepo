﻿CREATE TABLE [dbo].[EXP_DIC_ResultByStage](
	[StageId] [int] NOT NULL,
	[StageResultId] [int] NOT NULL,
 CONSTRAINT [PK_EXP_DIC_ResultByStage] PRIMARY KEY CLUSTERED 
(
	[StageId] ASC,
	[StageResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_DIC_ResultByStage]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DIC_ResultByStage_StageId_EXP_DIC_Stage] FOREIGN KEY([StageId])
REFERENCES [dbo].[EXP_DIC_Stage] ([Id])
GO

ALTER TABLE [dbo].[EXP_DIC_ResultByStage] CHECK CONSTRAINT [FK_EXP_DIC_ResultByStage_StageId_EXP_DIC_Stage]
GO

ALTER TABLE [dbo].[EXP_DIC_ResultByStage]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DIC_ResultByStage_StageResultId_EXP_DIC_StageResult] FOREIGN KEY([StageResultId])
REFERENCES [dbo].[EXP_DIC_StageResult] ([Id])
GO

ALTER TABLE [dbo].[EXP_DIC_ResultByStage] CHECK CONSTRAINT [FK_EXP_DIC_ResultByStage_StageResultId_EXP_DIC_StageResult]
GO


