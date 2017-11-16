DELETE FROM [dbo].[EXP_DirectionToPays_DrugDeclaration]
GO

DELETE FROM [dbo].[EXP_DirectionToPays_PriceList]
GO

DELETE FROM [dbo].[EXP_DirectionToPays];
GO

ALTER TABLE [dbo].[EXP_DirectionToPays_PriceList] DROP CONSTRAINT [FK_EXP_DirectionToPays_PriceList_EXP_DirectionToPays]
GO

ALTER TABLE [dbo].[EXP_DirectionToPays_DrugDeclaration] DROP CONSTRAINT [FK_EXP_DirectionToPays_DrugDeclaration_EXP_DirectionToPays]
GO


DROP TABLE [dbo].[EXP_DirectionToPays]
GO

CREATE TABLE [dbo].[EXP_DirectionToPays](
	[Id] [uniqueidentifier] NOT NULL,
	[Number] [nvarchar](512) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[DeleteDate] [datetime] NULL,
	[DirectionDate] [datetime] NOT NULL,
	[PayerId] [uniqueidentifier] NULL,
	[PayerValue] [nvarchar](4000) NULL,
	[CreateEmployeeId] [uniqueidentifier] NOT NULL,
	[CreateEmployeeValue] [nvarchar](1024) NOT NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[InvoiceCode1C] [nvarchar](512) NULL,
	[InvoiceNumber1C] [nvarchar](512) NULL,
	[InvoiceDatetime1C] [datetime] NULL,
	[PaymentDatetime1C] [datetime] NULL,
	[PaymentValue1C] [decimal](18, 2) NULL,
	[PaymentComment1C] [nvarchar](4000) NULL,
	[StatusId] [uniqueidentifier] NOT NULL,
	[StatusValue] [nvarchar](4000) NULL,
	[Type] [int] NOT NULL,
	[TypeValue] [nvarchar](4000) NULL,
 CONSTRAINT [PK_EXP_DirectionToPays] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_DirectionToPays] ADD  CONSTRAINT [DF_EXP_DirectionToPays_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[EXP_DirectionToPays] ADD  CONSTRAINT [DF_EXP_DirectionToPays_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[EXP_DirectionToPays] ADD  CONSTRAINT [DF_EXP_DirectionToPays_ApplicationDate]  DEFAULT (getdate()) FOR [DirectionDate]
GO

ALTER TABLE [dbo].[EXP_DirectionToPays]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DirectionToPays_Dictionaries] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_DirectionToPays] CHECK CONSTRAINT [FK_EXP_DirectionToPays_Dictionaries]
GO

ALTER TABLE [dbo].[EXP_DirectionToPays]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DirectionToPays_Employees] FOREIGN KEY([CreateEmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[EXP_DirectionToPays] CHECK CONSTRAINT [FK_EXP_DirectionToPays_Employees]
GO

ALTER TABLE [dbo].[EXP_DirectionToPays]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DirectionToPays_Organizations] FOREIGN KEY([PayerId])
REFERENCES [dbo].[Organizations] ([Id])
GO

ALTER TABLE [dbo].[EXP_DirectionToPays] CHECK CONSTRAINT [FK_EXP_DirectionToPays_Organizations]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер заявки' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_DirectionToPays', @level2type=N'COLUMN',@level2name=N'Number'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата создания' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_DirectionToPays', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO




ALTER TABLE [dbo].[EXP_DirectionToPays_DrugDeclaration]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DirectionToPays_DrugDeclaration_EXP_DirectionToPays] FOREIGN KEY([DirectionToPayId])
REFERENCES [dbo].[EXP_DirectionToPays] ([Id])
GO

ALTER TABLE [dbo].[EXP_DirectionToPays_DrugDeclaration] CHECK CONSTRAINT [FK_EXP_DirectionToPays_DrugDeclaration_EXP_DirectionToPays]
GO



ALTER TABLE [dbo].[EXP_DirectionToPays_PriceList]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DirectionToPays_PriceList_EXP_DirectionToPays] FOREIGN KEY([DirectionToPayId])
REFERENCES [dbo].[EXP_DirectionToPays] ([Id])
GO

ALTER TABLE [dbo].[EXP_DirectionToPays_PriceList] CHECK CONSTRAINT [FK_EXP_DirectionToPays_PriceList_EXP_DirectionToPays]
GO
