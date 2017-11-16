
CREATE TABLE [dbo].[EXP_ExpertiseStageRemark](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StageId] [uniqueidentifier] NOT NULL,
	[RemarkTypeId] [int] NULL,
	[OtdId] [int] NULL,
	[NameRemark] [nvarchar](2000) NULL,
	[ExecuterId] [uniqueidentifier] NULL,
	[RemarkDate] [datetime] NULL,
	[AnswerRemark] [nvarchar](2000) NULL,
	[IsFixed] [bit] NOT NULL,
	[IsAccepted] [bit] NOT NULL,
	[FixedDate] [datetime] NULL,
	[Note] [nvarchar](2000) NULL,
	[IsReadOnly] [bit] NOT NULL,
	[CorespondenceId] [nvarchar](50) NULL,
 CONSTRAINT [PK_EXP_ExpertiseStageRemark] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_ExpertiseStageRemark] ADD  DEFAULT ((0)) FOR [IsReadOnly]
GO

ALTER TABLE [dbo].[EXP_ExpertiseStageRemark]  WITH CHECK ADD  CONSTRAINT [FK_ExpertiseStageRemark_RemarkType] FOREIGN KEY([RemarkTypeId])
REFERENCES [dbo].[EXP_DIC_RemarkType] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertiseStageRemark] CHECK CONSTRAINT [FK_ExpertiseStageRemark_RemarkType]
GO

ALTER TABLE [dbo].[EXP_ExpertiseStageRemark]  WITH CHECK ADD  CONSTRAINT [FK_EXP_ExpertiseStageRemark_Employees] FOREIGN KEY([ExecuterId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertiseStageRemark] CHECK CONSTRAINT [FK_EXP_ExpertiseStageRemark_Employees]
GO

ALTER TABLE [dbo].[EXP_ExpertiseStageRemark]  WITH CHECK ADD  CONSTRAINT [FK_EXP_ExpertiseStageRemark_ExpertiseStage] FOREIGN KEY([StageId])
REFERENCES [dbo].[EXP_ExpertiseStage] ([Id])
GO

ALTER TABLE [dbo].[EXP_ExpertiseStageRemark] CHECK CONSTRAINT [FK_EXP_ExpertiseStageRemark_ExpertiseStage]
GO


