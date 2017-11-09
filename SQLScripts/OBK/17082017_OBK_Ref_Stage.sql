USE [ncelsProd]
GO

/****** Object:  Table [dbo].[OBK_Ref_Stage]    Script Date: 16.08.2017 17:09:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_Ref_Stage](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](2000) NULL,
	[NameKz] [nvarchar](2000) NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEdit] [datetime] NULL,
 CONSTRAINT [PK_OBK_Ref_Stage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [ncelsProd]
GO

INSERT INTO [dbo].[OBK_Ref_Stage]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted]
           ,[DateEdit])
     VALUES
           ('1'
           ,'1'
           ,'ЦОЗ'
           ,'ЦОЗ'
           ,'2017-01-01 00:00:00.000'
           ,'False'
           ,NULL)
GO


