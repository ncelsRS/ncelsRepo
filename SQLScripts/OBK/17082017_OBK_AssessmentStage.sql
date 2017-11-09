USE [ncelsProd]
GO

/****** Object:  Table [dbo].[OBK_AssessmentStage]    Script Date: 16.08.2017 17:08:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_AssessmentStage](
	[Id] [uniqueidentifier] NOT NULL,
	[DeclarationId] [uniqueidentifier] NOT NULL,
	[StageId] [int] NOT NULL,
	[StageStatusId] [int] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[FactEndDate] [datetime] NULL,
	[ResultId] [int] NULL,
	[ParentStageId] [uniqueidentifier] NULL,
	[IsHistory] [bit] NOT NULL,
	[OtdIds] [nvarchar](4000) NULL,
	[OtdRemarks] [nvarchar](max) NULL,
	[IsSuspended] [bit] NOT NULL,
	[SuspendedStartDate] [datetime] NULL,
 CONSTRAINT [PK_OBK_AssessmentStage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_AssessmentStage]  WITH CHECK ADD  CONSTRAINT [FK_OBK_AssessmentStage_OBK_AssessmentDeclaration] FOREIGN KEY([DeclarationId])
REFERENCES [dbo].[OBK_AssessmentDeclaration] ([Id])
GO

ALTER TABLE [dbo].[OBK_AssessmentStage] CHECK CONSTRAINT [FK_OBK_AssessmentStage_OBK_AssessmentDeclaration]
GO

ALTER TABLE [dbo].[OBK_AssessmentStage]  WITH CHECK ADD  CONSTRAINT [FK_OBK_AssessmentStage_OBK_Ref_Stage] FOREIGN KEY([StageId])
REFERENCES [dbo].[OBK_Ref_Stage] ([Id])
GO

ALTER TABLE [dbo].[OBK_AssessmentStage] CHECK CONSTRAINT [FK_OBK_AssessmentStage_OBK_Ref_Stage]
GO

ALTER TABLE [dbo].[OBK_AssessmentStage]  WITH CHECK ADD  CONSTRAINT [FK_OBK_AssessmentStage_OBK_Ref_StageStatus] FOREIGN KEY([StageStatusId])
REFERENCES [dbo].[OBK_Ref_StageStatus] ([Id])
GO

ALTER TABLE [dbo].[OBK_AssessmentStage] CHECK CONSTRAINT [FK_OBK_AssessmentStage_OBK_Ref_StageStatus]
GO


