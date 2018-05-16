USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_ZBKCopy]    Script Date: 21.12.2017 10:00:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_ZBKCopy](
	[Id] [uniqueidentifier] NOT NULL,
	[OBK_StageExpDocumentId] [uniqueidentifier] NULL,
	[CopyQuantity] [int] NULL,
	[AttachPath] [nvarchar](400) NULL,
	[ExpApplication] [bit] NULL,
	[SendDate] [datetime] NULL,
	[StatusId] [int] NULL,
	[Notes] [ntext] NULL,
	[EmployeeId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_OBK_ZBKCopy] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_ZBKCopy]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ZBKCopy_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[OBK_ZBKCopy] CHECK CONSTRAINT [FK_OBK_ZBKCopy_Employees]
GO

ALTER TABLE [dbo].[OBK_ZBKCopy]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ZBKCopy_OBK_StageExpDocument] FOREIGN KEY([OBK_StageExpDocumentId])
REFERENCES [dbo].[OBK_StageExpDocument] ([Id])
GO

ALTER TABLE [dbo].[OBK_ZBKCopy] CHECK CONSTRAINT [FK_OBK_ZBKCopy_OBK_StageExpDocument]
GO


USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_ZBKCopySignData]    Script Date: 21.12.2017 10:01:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_ZBKCopySignData](
	[Id] [uniqueidentifier] NOT NULL,
	[OBK_ZBKCopyId] [uniqueidentifier] NOT NULL,
	[SignerId] [uniqueidentifier] NOT NULL,
	[SignXmlData] [ntext] NOT NULL,
	[SignDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_OBK_ZBKCopySignData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_ZBKCopySignData]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ZBKCopySignData_Employees] FOREIGN KEY([SignerId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[OBK_ZBKCopySignData] CHECK CONSTRAINT [FK_OBK_ZBKCopySignData_Employees]
GO

ALTER TABLE [dbo].[OBK_ZBKCopySignData]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ZBKCopySignData_OBK_ZBKCopy] FOREIGN KEY([OBK_ZBKCopyId])
REFERENCES [dbo].[OBK_ZBKCopy] ([Id])
GO

ALTER TABLE [dbo].[OBK_ZBKCopySignData] CHECK CONSTRAINT [FK_OBK_ZBKCopySignData_OBK_ZBKCopy]
GO


USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_ZBKCopyStage]    Script Date: 21.12.2017 10:01:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_ZBKCopyStage](
	[Id] [uniqueidentifier] NOT NULL,
	[OBK_ZBKCopyId] [uniqueidentifier] NULL,
	[StageId] [int] NOT NULL,
	[StageStatusId] [int] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ResultId] [int] NULL,
 CONSTRAINT [PK_OBK_ZBKCopyStage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_ZBKCopyStage]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ZBKCopyStage_OBK_Ref_Stage] FOREIGN KEY([StageId])
REFERENCES [dbo].[OBK_Ref_Stage] ([Id])
GO

ALTER TABLE [dbo].[OBK_ZBKCopyStage] CHECK CONSTRAINT [FK_OBK_ZBKCopyStage_OBK_Ref_Stage]
GO

ALTER TABLE [dbo].[OBK_ZBKCopyStage]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ZBKCopyStage_OBK_Ref_StageStatus] FOREIGN KEY([StageStatusId])
REFERENCES [dbo].[OBK_Ref_StageStatus] ([Id])
GO

ALTER TABLE [dbo].[OBK_ZBKCopyStage] CHECK CONSTRAINT [FK_OBK_ZBKCopyStage_OBK_Ref_StageStatus]
GO


USE [ncels]
GO

/** Object:  Table [dbo].[OBK_ZBKCopyStageExecutors]    Script Date: 25.12.2017 9:38:32 **/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_ZBKCopyStageExecutors](
  [ZBKCopyStageId] [uniqueidentifier] NOT NULL,
  [ExecutorId] [uniqueidentifier] NOT NULL,
  [ExecutorType] [int] NOT NULL,
 CONSTRAINT [PK_OBK_ZBKCopyStageExecutors] PRIMARY KEY CLUSTERED 
(
  [ZBKCopyStageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_ZBKCopyStageExecutors]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ZBKCopyStageExecutors_Employees] FOREIGN KEY([ExecutorId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[OBK_ZBKCopyStageExecutors] CHECK CONSTRAINT [FK_OBK_ZBKCopyStageExecutors_Employees]
GO

ALTER TABLE [dbo].[OBK_ZBKCopyStageExecutors]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ZBKCopyStageExecutors_OBK_ZBKCopyStage] FOREIGN KEY([ZBKCopyStageId])
REFERENCES [dbo].[OBK_ZBKCopyStage] ([Id])
GO

ALTER TABLE [dbo].[OBK_ZBKCopyStageExecutors] CHECK CONSTRAINT [FK_OBK_ZBKCopyStageExecutors_OBK_ZBKCopyStage]
GO



ALTER TABLE  [dbo].[OBK_DirectionToPayments] ADD ZBKCopy_id uniqueidentifier null

