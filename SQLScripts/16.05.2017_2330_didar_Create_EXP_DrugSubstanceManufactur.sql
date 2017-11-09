
CREATE TABLE [dbo].[EXP_DrugSubstanceManufacture](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DrugSubstanceId] [bigint] NOT NULL,
	[ProducerName] [nvarchar](500) NULL,
	[ProducerAddress] [nvarchar](500) NULL,
	[CountryId] [bigint] NULL,

 CONSTRAINT [PK_EXP_DrugSubstanceManufacture] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE [dbo].[EXP_DrugSubstanceManufacture]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugSubstanceManufacture_DrugSubstance] FOREIGN KEY([DrugSubstanceId])
REFERENCES [dbo].[EXP_DrugSubstance] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugSubstanceManufacture] CHECK CONSTRAINT [FK_EXP_DrugSubstanceManufacture_DrugSubstance]
GO


ALTER TABLE [dbo].[EXP_DrugSubstanceManufacture]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugSubstanceManufacture_sr_countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[sr_countries] ([id])
GO

ALTER TABLE [dbo].[EXP_DrugSubstanceManufacture] CHECK CONSTRAINT [FK_EXP_DrugSubstanceManufacture_sr_countries]
GO

ALTER TABLE [dbo].[EXP_DrugWrapping]
ADD [WrappingSizeStr] [nvarchar](500) NULL
GO
ALTER TABLE [dbo].[EXP_DrugWrapping]
ADD [WrappingVolumeStr] [nvarchar](500) NULL
GO