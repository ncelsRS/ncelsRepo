CREATE TABLE [dbo].[EXP_ExpertisePharmaceuticalFinalDoc](
	[Id] [uniqueidentifier] NOT NULL,
	[Indicator1_1] [ntext] NULL,
	[Indicator2_1] [ntext] NULL,
	[Indicator2_2] [ntext] NULL,
	[Indicator2_3] [ntext] NULL,
	[Indicator2_4] [ntext] NULL,
	[Indicator3_1] [ntext] NULL,
	[Indicator3_2] [ntext] NULL,
	[Indicator3_3] [ntext] NULL,
	[Indicator4] [ntext] NULL,
	[Indicator5] [ntext] NULL,
	[Indicator6] [ntext] NULL,
	[Indicator7] [ntext] NULL,
	[Indicator12] [ntext] NULL,
	[Indicator13_1] [ntext] NULL,
	[Indicator13_2] [ntext] NULL,
	[Indicator13_3] [ntext] NULL,
	[Indicator14] [ntext] NULL,
	[Indicator15] [ntext] NULL,
	[Indicator16] [ntext] NULL,
	[Indicator17] [ntext] NULL,
	[Indicator18] [ntext] NULL,
	[Indicator19] [ntext] NULL,
	[Indicator20] [ntext] NULL,
	[DosageStageId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EXP_ExpertisePharmaceuticalFinalDoc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_ExpertisePharmaceuticalFinalDoc] ADD  CONSTRAINT [DF_EXP_ExpertisePharmaceuticalFinalDoc_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[EXP_ExpertisePharmaceuticalFinalDoc]  WITH CHECK ADD  CONSTRAINT [FK_EXP_ExpertisePharmaceuticalFinalDoc_DosageStageId_EXP_ExpertiseStageDosage] FOREIGN KEY([DosageStageId])
REFERENCES [dbo].[EXP_ExpertiseStageDosage] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertisePharmaceuticalFinalDoc] CHECK CONSTRAINT [FK_EXP_ExpertisePharmaceuticalFinalDoc_DosageStageId_EXP_ExpertiseStageDosage]
GO


