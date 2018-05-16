DROP TABLE [dbo].[EXP_ExpertiseSafetyreportFinalDoc]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EXP_ExpertiseSafetyreportFinalDoc](
	[Id] [uniqueidentifier] NOT NULL,
	[PrimaryConclusion] [ntext] NULL,
	[PharmaceuticalConclusion] [ntext] NULL,
	[PharmacologicalConclusion] [ntext] NULL,
	[AnalyticalConclusion] [ntext] NULL,
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

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Заключение первичной' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_ExpertiseSafetyreportFinalDoc', @level2type=N'COLUMN',@level2name=N'PrimaryConclusion'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Заключение фармацевтика' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_ExpertiseSafetyreportFinalDoc', @level2type=N'COLUMN',@level2name=N'PharmaceuticalConclusion'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Заключение фармакология' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_ExpertiseSafetyreportFinalDoc', @level2type=N'COLUMN',@level2name=N'PharmacologicalConclusion'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Заключение испытательной лаборатории' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_ExpertiseSafetyreportFinalDoc', @level2type=N'COLUMN',@level2name=N'AnalyticalConclusion'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Заключение ЗОБ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_ExpertiseSafetyreportFinalDoc', @level2type=N'COLUMN',@level2name=N'Conclusion'
GO


