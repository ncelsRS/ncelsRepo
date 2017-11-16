USE [ncelsProd]
GO

/****** Object:  Table [dbo].[OBK_AssessmentStageSignData]    Script Date: 01.11.2017 9:24:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_AssessmentStageSignData](
	[Id] [uniqueidentifier] NOT NULL,
	[AssessmentStageId] [uniqueidentifier] NOT NULL,
	[SignerId] [uniqueidentifier] NOT NULL,
	[SignXmlData] [ntext] NOT NULL,
	[SignDateTime] [datetime] NOT NULL
 CONSTRAINT [PK_OBK_AssessmentStageSignData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[AssessmentStageId] ASC,
	[SignerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_AssessmentStageSignData]  WITH CHECK ADD  CONSTRAINT [FK_OBK_AssessmentStageSignData_Employees] FOREIGN KEY([SignerId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[OBK_AssessmentStageSignData] CHECK CONSTRAINT [FK_OBK_AssessmentStageSignData_Employees]
GO

ALTER TABLE [dbo].[OBK_AssessmentStageSignData]  WITH CHECK ADD  CONSTRAINT [FK_OBK_AssessmentStageSignData_OBK_AssessmentStage] FOREIGN KEY([AssessmentStageId])
REFERENCES [dbo].[OBK_AssessmentStage] ([Id])
GO

ALTER TABLE [dbo].[OBK_AssessmentStageSignData] CHECK CONSTRAINT [FK_OBK_AssessmentStageSignData_OBK_AssessmentStage]
GO


