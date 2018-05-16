CREATE TABLE [dbo].[EXP_AgreementProcSettings](
	[Id] [uniqueidentifier] NOT NULL,
	[AgreedDocTypeId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EXP_AgreementProcSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_AgreementProcSettings] ADD  CONSTRAINT [DF_EXP_AgreementProcSettings_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[EXP_AgreementProcSettings]  WITH CHECK ADD  CONSTRAINT [FK_EXP_AgreementProcSettings_AgreedDocTypeId_Dictionaries] FOREIGN KEY([AgreedDocTypeId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_AgreementProcSettings] CHECK CONSTRAINT [FK_EXP_AgreementProcSettings_AgreedDocTypeId_Dictionaries]
GO


