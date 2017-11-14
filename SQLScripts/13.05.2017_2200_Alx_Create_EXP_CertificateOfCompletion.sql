SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EXP_CertificateOfCompletion](
	[Id] [uniqueidentifier] NOT NULL,
	[Number] [nvarchar](500) NULL,
	[DicStageId] [int] NULL,
	[DrugDeclarationId] [uniqueidentifier] NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[StatusId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NULL,
	[DeleteDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[SendDate] [datetime] NULL,
	[CreateEmployeeId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_EXP_CertificateOfCompletion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_CertificateOfCompletion] ADD  CONSTRAINT [DF_EXP_CertificateOfCompletion_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[EXP_CertificateOfCompletion]  WITH CHECK ADD  CONSTRAINT [FK_EXP_CertificateOfCompletion_CreatorEmployee] FOREIGN KEY([CreateEmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[EXP_CertificateOfCompletion] CHECK CONSTRAINT [FK_EXP_CertificateOfCompletion_CreatorEmployee]
GO

ALTER TABLE [dbo].[EXP_CertificateOfCompletion]  WITH CHECK ADD  CONSTRAINT [FK_EXP_CertificateOfCompletion_EXP_DIC_Stage] FOREIGN KEY([DicStageId])
REFERENCES [dbo].[EXP_DIC_Stage] ([Id])
GO

ALTER TABLE [dbo].[EXP_CertificateOfCompletion] CHECK CONSTRAINT [FK_EXP_CertificateOfCompletion_EXP_DIC_Stage]
GO

ALTER TABLE [dbo].[EXP_CertificateOfCompletion]  WITH CHECK ADD  CONSTRAINT [FK_EXP_CertificateOfCompletion_EXP_DrugDeclaration] FOREIGN KEY([DrugDeclarationId])
REFERENCES [dbo].[EXP_DrugDeclaration] ([Id])
GO

ALTER TABLE [dbo].[EXP_CertificateOfCompletion] CHECK CONSTRAINT [FK_EXP_CertificateOfCompletion_EXP_DrugDeclaration]
GO

ALTER TABLE [dbo].[EXP_CertificateOfCompletion]  WITH CHECK ADD  CONSTRAINT [FK_EXP_CertificateOfCompletion_Statuses] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_CertificateOfCompletion] CHECK CONSTRAINT [FK_EXP_CertificateOfCompletion_Statuses]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Этапы экспертизы' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_CertificateOfCompletion', @level2type=N'COLUMN',@level2name=N'DicStageId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ссылка на заявление' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_CertificateOfCompletion', @level2type=N'COLUMN',@level2name=N'DrugDeclarationId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Сумма' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_CertificateOfCompletion', @level2type=N'COLUMN',@level2name=N'TotalPrice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Статус' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_CertificateOfCompletion', @level2type=N'COLUMN',@level2name=N'StatusId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата отправки на согласование' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_CertificateOfCompletion', @level2type=N'COLUMN',@level2name=N'SendDate'
GO


