CREATE TABLE [dbo].[EXP_Activities](
	[Id] [uniqueidentifier] NOT NULL,
	[TypeId] [uniqueidentifier] NOT NULL,
	[TypeValue] [nvarchar](4000) NULL,
	[Text] [nvarchar](4000) NOT NULL,
	[StatusId] [uniqueidentifier] NOT NULL,
	[StatusValue] [nvarchar](4000) NULL,
	[AuthorId] [uniqueidentifier] NOT NULL,
	[AuthorValue] [nvarchar](4000) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[ExecutedDate] [datetime] NULL,
	[ClosedDate] [datetime] NULL,
	[DocumentId] [uniqueidentifier] NOT NULL,
	[DocumentValue] [nvarchar](4000) NULL,
	[DocumentTypeId] [uniqueidentifier] NOT NULL,
	[DocumentTypeValue] [nvarchar](4000) NULL,
 CONSTRAINT [PK_EXP_Activities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_Activities] ADD  CONSTRAINT [DF_EXP_Activities_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[EXP_Activities] ADD  CONSTRAINT [DF_EXP_Activities_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[EXP_Activities]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Activities_AuthorEmployee] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[EXP_Activities] CHECK CONSTRAINT [FK_EXP_Activities_AuthorEmployee]
GO

ALTER TABLE [dbo].[EXP_Activities]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Activities_ExpActivityStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_Activities] CHECK CONSTRAINT [FK_EXP_Activities_ExpActivityStatus]
GO

ALTER TABLE [dbo].[EXP_Activities]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Activities_ExpActivityType] FOREIGN KEY([TypeId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_Activities] CHECK CONSTRAINT [FK_EXP_Activities_ExpActivityType]
GO

ALTER TABLE [dbo].[EXP_Activities]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Activities_ExpAgreedDocType] FOREIGN KEY([DocumentTypeId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_Activities] CHECK CONSTRAINT [FK_EXP_Activities_ExpAgreedDocType]
GO



CREATE TABLE [dbo].[EXP_Tasks](
	[Id] [uniqueidentifier] NOT NULL,
	[TypeId] [uniqueidentifier] NOT NULL,
	[TypeValue] [nvarchar](4000) NULL,
	[StatusId] [uniqueidentifier] NOT NULL,
	[StatusValue] [nvarchar](4000) NULL,
	[OrderNumber] [int] NOT NULL,
	[Number] [nvarchar](256) NULL,
	[Text] [nvarchar](4000) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[ExecutedDate] [datetime] NULL,
	[ClosedDate] [datetime] NULL,
	[AuthorId] [uniqueidentifier] NOT NULL,
	[AuthorValue] [nvarchar](4000) NULL,
	[ExecutorId] [uniqueidentifier] NOT NULL,
	[ExecutorValue] [nvarchar](4000) NULL,
	[ActivityId] [uniqueidentifier] NOT NULL,
	[ParentTaskId] [uniqueidentifier] NULL,
	[Comment] [nvarchar](4000) NULL,
 CONSTRAINT [PK_EXP_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_Tasks] ADD  CONSTRAINT [DF_EXP_Tasks_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[EXP_Tasks] ADD  CONSTRAINT [DF_EXP_Tasks_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[EXP_Tasks]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Tasks_AuthorEmployee] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[EXP_Tasks] CHECK CONSTRAINT [FK_EXP_Tasks_AuthorEmployee]
GO

ALTER TABLE [dbo].[EXP_Tasks]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Tasks_ExecutorEmployee] FOREIGN KEY([ExecutorId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[EXP_Tasks] CHECK CONSTRAINT [FK_EXP_Tasks_ExecutorEmployee]
GO

ALTER TABLE [dbo].[EXP_Tasks]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Tasks_EXP_Activities] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[EXP_Activities] ([Id])
GO

ALTER TABLE [dbo].[EXP_Tasks] CHECK CONSTRAINT [FK_EXP_Tasks_EXP_Activities]
GO

ALTER TABLE [dbo].[EXP_Tasks]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Tasks_EXP_Tasks] FOREIGN KEY([ParentTaskId])
REFERENCES [dbo].[EXP_Tasks] ([Id])
GO

ALTER TABLE [dbo].[EXP_Tasks] CHECK CONSTRAINT [FK_EXP_Tasks_EXP_Tasks]
GO

ALTER TABLE [dbo].[EXP_Tasks]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Tasks_ExpTaskType] FOREIGN KEY([TypeId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_Tasks] CHECK CONSTRAINT [FK_EXP_Tasks_ExpTaskType]
GO

ALTER TABLE [dbo].[EXP_Tasks]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Tasks_StatusesDictionary] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_Tasks] CHECK CONSTRAINT [FK_EXP_Tasks_StatusesDictionary]
GO


