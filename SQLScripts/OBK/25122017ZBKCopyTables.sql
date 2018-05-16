ALTER TABLE  [dbo].[OBK_ZBKCopy] ADD ReceiverFIO nvarchar(500) null
ALTER TABLE  [dbo].[OBK_ZBKCopy] ADD ExtraditeDate datetime null
ALTER TABLE  [dbo].[OBK_ZBKCopy] ADD OriginalsGiven bit null
ALTER TABLE  [dbo].[OBK_ZBKCopy] ADD zbkCopiesReady bit null



ALTER TABLE  [dbo].[OBK_ZBKCopyStage] ADD InOBK bit null
ALTER TABLE  [dbo].[OBK_ZBKCopyStage] ADD OBK_Completed bit null


USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_ZBKCopyBlank]    Script Date: 25.12.2017 2:27:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_ZBKCopyBlank](
	[Id] [uniqueidentifier] NOT NULL,
	[ZBKCopyId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NULL,
	[StartNumber] [nvarchar](50) NULL,
	[EndPrimeNumber] [nvarchar](50) NULL,
	[StartApplicationNumber] [nvarchar](50) NULL,
	[EndApplicationNumber] [nvarchar](50) NULL,
	[EmployeeId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_OBK_ZBKCopyBlank] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_ZBKCopyBlank]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ZBKCopyBlank_OBK_ZBKCopy] FOREIGN KEY([ZBKCopyId])
REFERENCES [dbo].[OBK_ZBKCopy] ([Id])
GO

ALTER TABLE [dbo].[OBK_ZBKCopyBlank] CHECK CONSTRAINT [FK_OBK_ZBKCopyBlank_OBK_ZBKCopy]
GO





USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_ZBKCopyCorruptedBlank]    Script Date: 25.12.2017 2:27:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_ZBKCopyCorruptedBlank](
	[Id] [uniqueidentifier] NOT NULL,
	[ZBKCopyId] [uniqueidentifier] NULL,
	[CorruptedBlankNumber] [nvarchar](50) NULL,
	[NewBlankNumber] [nvarchar](50) NULL,
	[EmployeeId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_OBK_ZBKCopyCorruptedBlank] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_ZBKCopyCorruptedBlank]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ZBKCopyCorruptedBlank_OBK_ZBKCopy] FOREIGN KEY([ZBKCopyId])
REFERENCES [dbo].[OBK_ZBKCopy] ([Id])
GO

ALTER TABLE [dbo].[OBK_ZBKCopyCorruptedBlank] CHECK CONSTRAINT [FK_OBK_ZBKCopyCorruptedBlank_OBK_ZBKCopy]
GO





USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_ZBKCopyStageSignData]    Script Date: 25.12.2017 2:28:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_ZBKCopyStageSignData](
	[Id] [uniqueidentifier] NOT NULL,
	[SignerId] [uniqueidentifier] NOT NULL,
	[SignXmlData] [ntext] NOT NULL,
	[SignDateTime] [datetime] NOT NULL,
	[StageId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_OBK_ZBKCopyStageSignData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_ZBKCopyStageSignData]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ZBKCopyStageSignData_Employees] FOREIGN KEY([SignerId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[OBK_ZBKCopyStageSignData] CHECK CONSTRAINT [FK_OBK_ZBKCopyStageSignData_Employees]
GO

ALTER TABLE [dbo].[OBK_ZBKCopyStageSignData]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ZBKCopyStageSignData_OBK_ZBKCopyStage] FOREIGN KEY([StageId])
REFERENCES [dbo].[OBK_ZBKCopyStage] ([Id])
GO

ALTER TABLE [dbo].[OBK_ZBKCopyStageSignData] CHECK CONSTRAINT [FK_OBK_ZBKCopyStageSignData_OBK_ZBKCopyStage]
GO


