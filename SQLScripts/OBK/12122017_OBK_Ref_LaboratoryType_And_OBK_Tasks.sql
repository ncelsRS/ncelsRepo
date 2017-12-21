USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_Ref_LaboratoryType]    Script Date: 11.12.2017 17:19:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_Ref_LaboratoryType](
	[Id] [uniqueidentifier] NOT NULL,
	[NameRu] [nvarchar](255) NOT NULL,
	[NameKz] [nvarchar](255) NOT NULL,
	[IsDeleted] [bit] NOT NULL
 CONSTRAINT [PK_OBK_Ref_LaboratoryType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [ncels]
GO

INSERT INTO [dbo].[OBK_Ref_LaboratoryType]
           ([Id]
           ,[NameRu]
           ,[NameKz]
           ,[IsDeleted])
     VALUES
           (newid()
           ,N'Физико-химические испытания'
           ,N'Физико-химические испытания'
           ,'False'),
		   (newid()
           ,N'По физическим показателям'
           ,N'По физическим показателям'
           ,'False'),
		   (newid()
           ,N'Микробиологическая чистота'
           ,N'Микробиологическая чистота'
           ,'False'),
		   (newid()
           ,N'Токсикология'
           ,N'Токсикология'
           ,'False')
GO




USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_Tasks]    Script Date: 12.12.2017 9:55:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_Tasks](
	[Id] [uniqueidentifier] NOT NULL,
	[TaskNumber] [nvarchar](50) NULL,
	[RegisterDate] [nvarchar](50) NULL,
	[ExecutorId] [uniqueidentifier] NULL,
	[UnitId] [uniqueidentifier] NULL,
	[ActReceptionId] [uniqueidentifier] NULL,
	[AssessmentDeclarationId] [uniqueidentifier] NULL,
	[TaskEndDate] [datetime] NULL,
	[IsSigned] [bit] NOT NULL,
	[LaboratoryTypeId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_OBK_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_Tasks] ADD  DEFAULT ((0)) FOR [IsSigned]
GO

ALTER TABLE [dbo].[OBK_Tasks]  WITH CHECK ADD  CONSTRAINT [FK_OBK_Tasks_Employees] FOREIGN KEY([ExecutorId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[OBK_Tasks] CHECK CONSTRAINT [FK_OBK_Tasks_Employees]
GO

ALTER TABLE [dbo].[OBK_Tasks]  WITH CHECK ADD  CONSTRAINT [FK_OBK_Tasks_OBK_AssessmentDeclaration] FOREIGN KEY([AssessmentDeclarationId])
REFERENCES [dbo].[OBK_AssessmentDeclaration] ([Id])
GO

ALTER TABLE [dbo].[OBK_Tasks] CHECK CONSTRAINT [FK_OBK_Tasks_OBK_AssessmentDeclaration]
GO

ALTER TABLE [dbo].[OBK_Tasks]  WITH CHECK ADD  CONSTRAINT [FK_OBK_Tasks_OBK_Ref_LaboratoryType] FOREIGN KEY([LaboratoryTypeId])
REFERENCES [dbo].[OBK_Ref_LaboratoryType] ([Id])
GO

ALTER TABLE [dbo].[OBK_Tasks] CHECK CONSTRAINT [FK_OBK_Tasks_OBK_Ref_LaboratoryType]
GO

ALTER TABLE [dbo].[OBK_Tasks]  WITH CHECK ADD  CONSTRAINT [FK_OBK_Tasks_Units] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Units] ([Id])
GO

ALTER TABLE [dbo].[OBK_Tasks] CHECK CONSTRAINT [FK_OBK_Tasks_Units]
GO


alter table OBK_Procunts_Series add TaskId uniqueidentifier null
CONSTRAINT FK_OBK_Procunts_Series_TaskId_OBK_Tasks_Id
		 FOREIGN KEY ([TaskId])
		 REFERENCES [OBK_Tasks]([Id])
go

