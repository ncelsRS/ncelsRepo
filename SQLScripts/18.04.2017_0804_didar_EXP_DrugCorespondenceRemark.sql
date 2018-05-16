

CREATE TABLE [dbo].[EXP_DrugCorespondenceRemark](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DrugCorespondenceId] [uniqueidentifier] NOT NULL,
	[RemarkTypeId] [int] NULL,
	[NameRemark] [nvarchar](2000) NULL,
	[ExecuterId] [uniqueidentifier] NULL,
	[RemarkDate] [datetime] NULL,
	[AnswerRemark] [nvarchar](2000) NULL,
	[IsFixed] [bit] NOT NULL,
	[IsAccepted] [bit] NOT NULL,
	[FixedDate] [datetime] NULL,
	[Note] [nvarchar](2000) NULL,
 CONSTRAINT [PK_EXP_DrugCorespondenceRemark] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_DrugCorespondenceRemark]  WITH CHECK ADD  CONSTRAINT [FK_DrugEXP_CorespondenceRemark_RemarkType] FOREIGN KEY([RemarkTypeId])
REFERENCES [dbo].[EXP_DIC_RemarkType] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugCorespondenceRemark] CHECK CONSTRAINT [FK_DrugEXP_CorespondenceRemark_RemarkType]
GO

ALTER TABLE [dbo].[EXP_DrugCorespondenceRemark]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugCorespondenceRemark_Employees] FOREIGN KEY([ExecuterId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugCorespondenceRemark] CHECK CONSTRAINT [FK_EXP_DrugCorespondenceRemark_Employees]
GO

ALTER TABLE [dbo].[EXP_DrugCorespondenceRemark]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugCorespondenceRemark_Corespondence] FOREIGN KEY([DrugCorespondenceId])
REFERENCES [dbo].[EXP_DrugCorespondence] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugCorespondenceRemark] CHECK CONSTRAINT [FK_EXP_DrugCorespondenceRemark_Corespondence]
GO


