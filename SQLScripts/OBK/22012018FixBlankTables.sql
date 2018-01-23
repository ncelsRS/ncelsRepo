delete  from [ncels].[dbo].[OBK_ZBKCopyCorruptedBlank]
drop table [ncels].[dbo].[OBK_ZBKCopyCorruptedBlank]

delete  from [ncels].[dbo].[OBK_ZBKCopyBlankNumber]
drop table [ncels].[dbo].[OBK_ZBKCopyBlankNumber]

USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_BlankType]    Script Date: 22.01.2018 19:38:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_BlankType](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[NameRu] [nvarchar](50) NULL,
	[NameKz] [nvarchar](50) NULL,
	[Code] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_OBK_BlankType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO




USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_BlankNumber]    Script Date: 22.01.2018 19:38:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_BlankNumber](
	[Id] [uniqueidentifier] NOT NULL,
	[Object_Id] [uniqueidentifier] NULL,
	[Number] [int] NULL,
	[BlankTypeId] [smallint] NULL,
	[EmployeeId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NULL,
	[Decommissioned] [bit] NULL,
	[Corrupted] [bit] NULL,
	[DecommissionedDate] [datetime] NULL,
	[ParentId] [uniqueidentifier] NULL,
	[CorruptDate] [datetime] NULL,
	[CorruptEmployee] [uniqueidentifier] NULL,
 CONSTRAINT [PK_OBK_ZBKCopyBlankNumber] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_BlankNumber] ADD  CONSTRAINT [DF_OBK_BlankNumber_Corrupted]  DEFAULT ((0)) FOR [Corrupted]
GO

ALTER TABLE [dbo].[OBK_BlankNumber]  WITH CHECK ADD  CONSTRAINT [FK_OBK_BlankNumber_OBK_BlankNumber] FOREIGN KEY([ParentId])
REFERENCES [dbo].[OBK_BlankNumber] ([Id])
GO

ALTER TABLE [dbo].[OBK_BlankNumber] CHECK CONSTRAINT [FK_OBK_BlankNumber_OBK_BlankNumber]
GO

ALTER TABLE [dbo].[OBK_BlankNumber]  WITH CHECK ADD  CONSTRAINT [FK_OBK_BlankNumber_OBK_BlankType] FOREIGN KEY([BlankTypeId])
REFERENCES [dbo].[OBK_BlankType] ([Id])
GO

ALTER TABLE [dbo].[OBK_BlankNumber] CHECK CONSTRAINT [FK_OBK_BlankNumber_OBK_BlankType]
GO


 ALTER TABLE [dbo].[OBK_ZBKCopy] 
 ADD [StartNumber] int NULL

 ALTER TABLE [dbo].[OBK_ZBKCopy] 
 ADD [EndPrimeNumber] int NULL

 ALTER TABLE [dbo].[OBK_ZBKCopy] 
 ADD [StartApplicationNumber] int NULL
 
 ALTER TABLE [dbo].[OBK_ZBKCopy] 
 ADD [EndApplicationNumber] int NULL
