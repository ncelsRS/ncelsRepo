CREATE TABLE [dbo].[EXP_ExpertisePharmacologicalFinalDoc](
	[Id] [uniqueidentifier] NOT NULL,
	[Indicator1] [ntext] NULL,
	[Indicator2] [ntext] NULL,
	[Indicator3] [ntext] NULL,
	[Indicator4] [ntext] NULL,
	[Indicator5] [ntext] NULL,
	[Indicator6] [ntext] NULL,
	[Indicator7] [ntext] NULL,
	[Indicator8] [ntext] NULL,
	[Indicator9] [ntext] NULL,
	[Indicator10] [ntext] NULL,
	[Indicator11] [ntext] NULL,
	[Indicator12] [ntext] NULL,
	[Indicator13] [ntext] NULL,
	[Indicator14] [ntext] NULL,
	[Indicator15] [ntext] NULL,
	[Indicator16] [ntext] NULL,
	[Indicator17] [ntext] NULL,
	[Indicator18] [ntext] NULL,
	[Indicator19] [ntext] NULL,
	[Indicator20] [ntext] NULL,
	[Indicator21] [ntext] NULL,
	[Indicator22] [ntext] NULL,
	[Indicator23] [ntext] NULL,
	[DosageStageId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EXP_ExpertisePharmacologicalFinalDoc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_ExpertisePharmacologicalFinalDoc] ADD  CONSTRAINT [DF_EXP_ExpertisePharmacologicalFinalDoc_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[EXP_ExpertisePharmacologicalFinalDoc]  WITH CHECK ADD  CONSTRAINT [FK_EXP_ExpertisePharmacologicalFinalDoc_DosageStageId_EXP_ExpertiseStageDosage] FOREIGN KEY([DosageStageId])
REFERENCES [dbo].[EXP_ExpertiseStageDosage] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertisePharmacologicalFinalDoc] CHECK CONSTRAINT [FK_EXP_ExpertisePharmacologicalFinalDoc_DosageStageId_EXP_ExpertiseStageDosage]
GO


