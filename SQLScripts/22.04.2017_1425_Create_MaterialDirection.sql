SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EXP_MaterialDirections](
	[Id] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NULL,
	[DeleteDate] [datetime] NULL,
	[Number] [nvarchar](128) NULL,
	[DrugDeclarationId] [uniqueidentifier] NOT NULL,
	[StatusId] [uniqueidentifier] NOT NULL,
	[SendEmployeeId] [uniqueidentifier] NULL,
	[SendDate] [datetime] NULL,
	[ExecutorEmployeeId] [uniqueidentifier] NULL,
	[ReceiveDate] [datetime] NULL,
	[RejectDate] [datetime] NULL,
	[Comment] [nvarchar](max) NULL,
 CONSTRAINT [PK_EXP_MaterialDirection] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_MaterialDirections] ADD  CONSTRAINT [DF_EXP_MaterialDirection_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[EXP_MaterialDirections] ADD  CONSTRAINT [DF_Table_1_CreatedDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[EXP_MaterialDirections]  WITH CHECK ADD  CONSTRAINT [FK_EXP_MaterialDirections_ExecutorEmp] FOREIGN KEY([ExecutorEmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[EXP_MaterialDirections] CHECK CONSTRAINT [FK_EXP_MaterialDirections_ExecutorEmp]
GO

ALTER TABLE [dbo].[EXP_MaterialDirections]  WITH CHECK ADD  CONSTRAINT [FK_EXP_MaterialDirections_EXP_DrugDeclaration] FOREIGN KEY([DrugDeclarationId])
REFERENCES [dbo].[EXP_DrugDeclaration] ([Id])
GO

ALTER TABLE [dbo].[EXP_MaterialDirections] CHECK CONSTRAINT [FK_EXP_MaterialDirections_EXP_DrugDeclaration]
GO

ALTER TABLE [dbo].[EXP_MaterialDirections]  WITH CHECK ADD  CONSTRAINT [FK_EXP_MaterialDirections_SendEmp] FOREIGN KEY([SendEmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[EXP_MaterialDirections] CHECK CONSTRAINT [FK_EXP_MaterialDirections_SendEmp]
GO

ALTER TABLE [dbo].[EXP_MaterialDirections]  WITH CHECK ADD  CONSTRAINT [FK_EXP_MaterialDirections_Statuses] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_MaterialDirections] CHECK CONSTRAINT [FK_EXP_MaterialDirections_Statuses]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер направления' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_MaterialDirections', @level2type=N'COLUMN',@level2name=N'Number'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Заявление на экспертизу' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_MaterialDirections', @level2type=N'COLUMN',@level2name=N'DrugDeclarationId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата отклонения' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_MaterialDirections', @level2type=N'COLUMN',@level2name=N'RejectDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Комментарий' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_MaterialDirections', @level2type=N'COLUMN',@level2name=N'Comment'
GO


