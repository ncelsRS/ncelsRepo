ALTER TABLE [dbo].[EXP_DrugPrimaryFinalDocument]
    DROP CONSTRAINT [PK_EXP_DrugPrimaryFinalDocument]
	Go
	ALTER TABLE [dbo].[EXP_DrugPrimaryFinalDocument]
    DROP CONSTRAINT [FK_EXP_DrugPrimaryFinalDocument_DrugDosage]
	Go
drop table [dbo].[EXP_DrugPrimaryFinalDocument]
go

CREATE TABLE [dbo].[EXP_DrugPrimaryFinalDocument](
	[Id] [uniqueidentifier] NOT NULL,
	[IsRKProduct] [bit] NULL,
	[IsDossierSection] [bit] NULL,
	[IsSetDocument] [bit] NULL,
	[IsColorModel] [bit] NULL,
	[IsForbiddenDyes] [bit] NULL,
	[IsFromBlood] [bit] NULL,
	[IsNarcoticDrug] [bit] NULL,
	[IsPhoneticSimilar] [bit] NULL,
	[IsAbilityMislead] [bit] NULL,
	[IsAdvertising] [bit] NULL,
	[IsMNNSimilar] [bit] NULL,
	[ExpertiseNormDoc] [nvarchar](2000) NULL,
	[SampleDrug] [nvarchar](2000) NULL,
	[ComplianceSeries] [nvarchar](2000) NULL,
	[ResidualShelfLife] [nvarchar](2000) NULL,
	[SampleSubstance] [nvarchar](2000) NULL,
	[SampleStandart] [nvarchar](2000) NULL,
	[TestLabRecommend] [nvarchar](2000) NULL,
	[MedicalInstruction] [nvarchar](2000) NULL,
	[ExpertOpinion] [nvarchar](2000) NULL,
	[DrugDosageId] [bigint] NOT NULL,
 CONSTRAINT [PK_EXP_DrugPrimaryFinalDocument] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_DrugPrimaryFinalDocument]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugPrimaryFinalDocument_DrugDosage] FOREIGN KEY([DrugDosageId])
REFERENCES [dbo].[EXP_DrugDosage] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugPrimaryFinalDocument] CHECK CONSTRAINT [FK_EXP_DrugPrimaryFinalDocument_DrugDosage]
GO




