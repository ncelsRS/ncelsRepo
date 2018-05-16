SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[I1c_act_Application](
	[Id] [uniqueidentifier] NOT NULL,
	[Number] [nvarchar](500) NULL,
	[CreateDatetime] [datetime] NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[ActNumber1C] [nvarchar](500) NULL,
	[ActDate1C] [datetime] NULL,
	[ExportDatetime] [datetime] NOT NULL,
	[ImportDatetime] [datetime] NULL,
	[StageId] [int] NULL,
	[StageCode] [nvarchar](500) NULL,
	[StageValue] [nvarchar](500) NULL,
	[refPrimaryApplication] [uniqueidentifier] NULL,
 CONSTRAINT [PK_I1c_CertificationOfCompletion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[I1c_act_Application] ADD  CONSTRAINT [DF_I1c_CertificationOfCompletion_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[I1c_act_Application] ADD  CONSTRAINT [DF_I1c_CertificationOfCompletion_ExportDatetim]  DEFAULT (getdate()) FOR [ExportDatetime]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер акта в Экспертизе' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_Application', @level2type=N'COLUMN',@level2name=N'Number'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата акта в системе Экспертиза' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_Application', @level2type=N'COLUMN',@level2name=N'CreateDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Сумма оплаты по акту' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_Application', @level2type=N'COLUMN',@level2name=N'TotalPrice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер акта из 1С' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_Application', @level2type=N'COLUMN',@level2name=N'ActNumber1C'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата акта из 1С' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_Application', @level2type=N'COLUMN',@level2name=N'ActDate1C'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата завгрузки в промежуточную БД' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_Application', @level2type=N'COLUMN',@level2name=N'ExportDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата считывания из промежуточной БД' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_Application', @level2type=N'COLUMN',@level2name=N'ImportDatetime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ИД этапа с которого отправлен Акт' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_Application', @level2type=N'COLUMN',@level2name=N'StageId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Код этапа с которого отправлен Акт' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_Application', @level2type=N'COLUMN',@level2name=N'StageCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Название этапа с которого отправлен Акт' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_Application', @level2type=N'COLUMN',@level2name=N'StageValue'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ссылка на I1c_primary_Applications по полю ApplicationId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'I1c_act_Application', @level2type=N'COLUMN',@level2name=N'refPrimaryApplication'
GO


