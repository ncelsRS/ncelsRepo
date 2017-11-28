USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_Dictionaries]    Script Date: 27.11.2017 20:28:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_Dictionaries](
	[Id] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](4000) NULL,
	[Name] [nvarchar](4000) NULL,
	[NameKz] [nvarchar](4000) NULL,
	[CreateDate] [datetime] NULL,
	[ExpireDate] [datetime] NULL,
 CONSTRAINT [PK_OBK_Dictionary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_ActReception]    Script Date: 21.11.2017 16:54:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_ActReception](
	[Id] [uniqueidentifier] NOT NULL,
	[Number] [nvarchar](50) NULL,
	[ActDate] [datetime] NULL,
	[Address] [nvarchar](max) NULL,
	[Worker] [nvarchar](max) NULL,
	[Producer] [nvarchar](max) NULL,
	[Provider] [nvarchar](max) NULL,
	[sr_measuresId] [bigint] NULL,
	[ProductSamplesId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_OBK_ActReception] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_ActReception]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ActReception_Dictionaries] FOREIGN KEY([ProductSamplesId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[OBK_ActReception] CHECK CONSTRAINT [FK_OBK_ActReception_Dictionaries]
GO

ALTER TABLE [dbo].[OBK_ActReception]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ActReception_OBK_AssessmentDeclaration] FOREIGN KEY([Id])
REFERENCES [dbo].[OBK_AssessmentDeclaration] ([Id])
GO

ALTER TABLE [dbo].[OBK_ActReception] CHECK CONSTRAINT [FK_OBK_ActReception_OBK_AssessmentDeclaration]
GO

ALTER TABLE [dbo].[OBK_ActReception]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ActReception_sr_measures] FOREIGN KEY([sr_measuresId])
REFERENCES [dbo].[sr_measures] ([id])
GO

ALTER TABLE [dbo].[OBK_ActReception] CHECK CONSTRAINT [FK_OBK_ActReception_sr_measures]
GO

USE [ncels]
GO


ALTER TABLE [dbo].[OBK_ActReception] ADD InspectionInstalledId UNIQUEIDENTIFIER NULL
ALTER TABLE [dbo].[OBK_ActReception] ADD PackageConditionId UNIQUEIDENTIFIER NULL
ALTER TABLE [dbo].[OBK_ActReception] ADD MarkingId UNIQUEIDENTIFIER NULL
ALTER TABLE [dbo].[OBK_ActReception] ADD StorageConditionsId UNIQUEIDENTIFIER NULL

ALTER TABLE [dbo].[OBK_ActReception]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ActReception_InspectionInstalledId__OBK_DictionariesId] FOREIGN KEY([InspectionInstalledId])
REFERENCES [dbo].[OBK_Dictionaries] ([id])

ALTER TABLE [dbo].[OBK_ActReception]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ActReception_PackageConditionId__OBK_DictionariesId] FOREIGN KEY([PackageConditionId])
REFERENCES [dbo].[OBK_Dictionaries] ([id])

ALTER TABLE [dbo].[OBK_ActReception]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ActReception_MarkingId__OBK_DictionariesId] FOREIGN KEY([MarkingId])
REFERENCES [dbo].[OBK_Dictionaries] ([id])

ALTER TABLE [dbo].[OBK_ActReception]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ActReception_StorageConditionsId__OBK_DictionariesId] FOREIGN KEY([StorageConditionsId])
REFERENCES [dbo].[OBK_Dictionaries] ([id])

ALTER TABLE [dbo].[OBK_ActReception] ADD Accept bit NULL




ALTER TABLE [ncels].[dbo].[OBK_AssessmentDeclaration] ADD DomesticProducer bit NULL
ALTER TABLE [ncels].[dbo].[OBK_AssessmentDeclaration] ADD KfSelection bit NULL
ALTER TABLE [ncels].[dbo].[OBK_AssessmentDeclaration] ADD GDPItself bit NULL


ALTER TABLE [ncels].[dbo].[OBK_Procunts_Series] ADD Quantity INT NULL
ALTER TABLE [ncels].[dbo].[OBK_Procunts_Series] ADD Available bit NULL
ALTER TABLE [ncels].[dbo].[OBK_Procunts_Series] ADD Comment nvarchar(4000) NULL