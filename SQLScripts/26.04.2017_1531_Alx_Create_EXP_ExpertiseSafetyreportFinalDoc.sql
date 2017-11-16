﻿SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EXP_ExpertiseSafetyreportFinalDoc](
	[Id] [uniqueidentifier] NOT NULL,
	[Conclusion] [ntext] NULL,
	[DosageStageId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EXP_ExpertiseSafetyreportFinalDoc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_ExpertiseSafetyreportFinalDoc] ADD  CONSTRAINT [DF_EXP_ExpertiseSafetyreportFinalDoc_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[EXP_ExpertiseSafetyreportFinalDoc]  WITH CHECK ADD  CONSTRAINT [FK_EXP_ExpertiseSafetyreportFinalDoc_EXP_ExpertiseStageDosage] FOREIGN KEY([DosageStageId])
REFERENCES [dbo].[EXP_ExpertiseStageDosage] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertiseSafetyreportFinalDoc] CHECK CONSTRAINT [FK_EXP_ExpertiseSafetyreportFinalDoc_EXP_ExpertiseStageDosage]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Заключение' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_ExpertiseSafetyreportFinalDoc', @level2type=N'COLUMN',@level2name=N'Conclusion'
GO


