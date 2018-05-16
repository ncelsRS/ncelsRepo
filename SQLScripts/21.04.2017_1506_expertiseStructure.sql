CREATE TABLE [dbo].[EXP_DIC_StageResult](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[NameRu] [nvarchar](2000) NOT NULL,
	[NameKz] [nvarchar](2000) NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEdit] [datetime] NULL,
 CONSTRAINT [PK_EXP_DIC_StageResult] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_DIC_StageResult] ADD  CONSTRAINT [DF_EXP_DIC_StageResult_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

CREATE TABLE [dbo].[EXP_DIC_StageStatus](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[NameRu] [nvarchar](2000) NOT NULL,
	[NameKz] [nvarchar](2000) NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEnd] [datetime] NULL,
 CONSTRAINT [PK_EXP_DIC_StageStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_DIC_StageStatus] ADD  CONSTRAINT [DF_EXP_DIC_StageStatus_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

CREATE TABLE [dbo].[EXP_ExpertiseStage](
	[Id] [uniqueidentifier] NOT NULL,
	[DeclarationId] [uniqueidentifier] NOT NULL,
	[StageId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[FactEndDate] [datetime] NULL,
	[ResultId] [int] NULL,
	[ParentStageId] [uniqueidentifier] NULL,
	[IsHistory] [bit] NOT NULL,
 CONSTRAINT [PK_EXP_ExpertiseStage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_ExpertiseStage] ADD  CONSTRAINT [DF_EXP_ExpertiseStage_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[EXP_ExpertiseStage] ADD  CONSTRAINT [DF_EXP_ExpertiseStage_IsHistory]  DEFAULT ((0)) FOR [IsHistory]
GO

ALTER TABLE [dbo].[EXP_ExpertiseStage]  WITH CHECK ADD  CONSTRAINT [FK_EXP_ExpertiseStage_DeclarationId_EXP_DrugDeclaration] FOREIGN KEY([DeclarationId])
REFERENCES [dbo].[EXP_DrugDeclaration] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertiseStage] CHECK CONSTRAINT [FK_EXP_ExpertiseStage_DeclarationId_EXP_DrugDeclaration]
GO

ALTER TABLE [dbo].[EXP_ExpertiseStage]  WITH CHECK ADD  CONSTRAINT [FK_EXP_ExpertiseStage_ParentId_EXP_ExpertiseStage] FOREIGN KEY([ParentStageId])
REFERENCES [dbo].[EXP_ExpertiseStage] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertiseStage] CHECK CONSTRAINT [FK_EXP_ExpertiseStage_ParentId_EXP_ExpertiseStage]
GO

ALTER TABLE [dbo].[EXP_ExpertiseStage]  WITH CHECK ADD  CONSTRAINT [FK_EXP_ExpertiseStage_ResultId_EXP_DIC_StageResult] FOREIGN KEY([ResultId])
REFERENCES [dbo].[EXP_DIC_StageResult] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertiseStage] CHECK CONSTRAINT [FK_EXP_ExpertiseStage_ResultId_EXP_DIC_StageResult]
GO

ALTER TABLE [dbo].[EXP_ExpertiseStage]  WITH CHECK ADD  CONSTRAINT [FK_EXP_ExpertiseStage_StageId_EXP_DIC_Stage] FOREIGN KEY([StageId])
REFERENCES [dbo].[EXP_DIC_Stage] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertiseStage] CHECK CONSTRAINT [FK_EXP_ExpertiseStage_StageId_EXP_DIC_Stage]
GO

ALTER TABLE [dbo].[EXP_ExpertiseStage]  WITH CHECK ADD  CONSTRAINT [FK_EXP_ExpertiseStage_StatusId_EXP_DIC_StageStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[EXP_DIC_StageStatus] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertiseStage] CHECK CONSTRAINT [FK_EXP_ExpertiseStage_StatusId_EXP_DIC_StageStatus]
GO

CREATE TABLE [dbo].[EXP_ExpertiseStageDosage](
	[Id] [uniqueidentifier] NOT NULL,
	[StageId] [uniqueidentifier] NOT NULL,
	[DosageId] [bigint] NOT NULL,
	[ResultId] [int] NULL,
	[FinalDocStatusId] [uniqueidentifier] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
 CONSTRAINT [PK_EXP_ExpertiseStageDosage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_ExpertiseStageDosage] ADD  CONSTRAINT [DF_EXP_ExpertiseStageDosage_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[EXP_ExpertiseStageDosage]  WITH CHECK ADD  CONSTRAINT [FK_EXP_ExpertiseStageDosage_DosageId_EXP_DrugDosage] FOREIGN KEY([DosageId])
REFERENCES [dbo].[EXP_DrugDosage] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertiseStageDosage] CHECK CONSTRAINT [FK_EXP_ExpertiseStageDosage_DosageId_EXP_DrugDosage]
GO

ALTER TABLE [dbo].[EXP_ExpertiseStageDosage]  WITH CHECK ADD  CONSTRAINT [FK_EXP_ExpertiseStageDosage_FinalDocStatusId_Dictionaries] FOREIGN KEY([FinalDocStatusId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertiseStageDosage] CHECK CONSTRAINT [FK_EXP_ExpertiseStageDosage_FinalDocStatusId_Dictionaries]
GO

ALTER TABLE [dbo].[EXP_ExpertiseStageDosage]  WITH CHECK ADD  CONSTRAINT [FK_EXP_ExpertiseStageDosage_ResultId_EXP_DIC_StageResult] FOREIGN KEY([ResultId])
REFERENCES [dbo].[EXP_DIC_StageResult] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertiseStageDosage] CHECK CONSTRAINT [FK_EXP_ExpertiseStageDosage_ResultId_EXP_DIC_StageResult]
GO

ALTER TABLE [dbo].[EXP_ExpertiseStageDosage]  WITH CHECK ADD  CONSTRAINT [FK_EXP_ExpertiseStageDosage_StageId_EXP_ExpertiseStage] FOREIGN KEY([StageId])
REFERENCES [dbo].[EXP_ExpertiseStage] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertiseStageDosage] CHECK CONSTRAINT [FK_EXP_ExpertiseStageDosage_StageId_EXP_ExpertiseStage]
GO

CREATE TABLE [dbo].[EXP_ExpertiseStageExecutors](
	[ExpertiseStageId] [uniqueidentifier] NOT NULL,
	[ExecutorId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EXP_ExpertiseStageExecutors] PRIMARY KEY CLUSTERED 
(
	[ExpertiseStageId] ASC,
	[ExecutorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_ExpertiseStageExecutors]  WITH CHECK ADD  CONSTRAINT [FK_EXP_ExpertiseStageExecutors_ExecutorId_Employees] FOREIGN KEY([ExecutorId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertiseStageExecutors] CHECK CONSTRAINT [FK_EXP_ExpertiseStageExecutors_ExecutorId_Employees]
GO

ALTER TABLE [dbo].[EXP_ExpertiseStageExecutors]  WITH CHECK ADD  CONSTRAINT [FK_EXP_ExpertiseStageExecutors_StageId_EXP_ExpertiseStage] FOREIGN KEY([ExpertiseStageId])
REFERENCES [dbo].[EXP_ExpertiseStage] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertiseStageExecutors] CHECK CONSTRAINT [FK_EXP_ExpertiseStageExecutors_StageId_EXP_ExpertiseStage]
GO

CREATE TABLE [dbo].[EXP_ExpertisePrimaryFinalDoc](
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
	[DosageStageId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EXP_ExpertisePrimaryFinalDoc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_ExpertisePrimaryFinalDoc]  WITH CHECK ADD  CONSTRAINT [FK_EXP_ExpertisePrimaryFinalDoc_DosageStageId_EXP_ExpertiseStageDosage] FOREIGN KEY([DosageStageId])
REFERENCES [dbo].[EXP_ExpertiseStageDosage] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertisePrimaryFinalDoc] CHECK CONSTRAINT [FK_EXP_ExpertisePrimaryFinalDoc_DosageStageId_EXP_ExpertiseStageDosage]
GO

