USE [ncelsProd]
GO

/****** Object:  Table [dbo].[OBK_Ref_StageStatus]    Script Date: 16.08.2017 17:10:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_Ref_StageStatus](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[NameRu] [nvarchar](2000) NOT NULL,
	[NameKz] [nvarchar](2000) NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL
 CONSTRAINT [PK_OBK_Ref_StageStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_Ref_StageStatus] ADD  CONSTRAINT [DF_OBK_Ref_StageStatus_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

USE [ncelsProd]
GO

INSERT INTO [dbo].[OBK_Ref_StageStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted])
     VALUES
           ('1'
           ,'inQueue'
           ,'На распределение'
           ,'На распределение'
           ,'2017-01-01 00:00:00.000'
           ,'False')
GO
INSERT INTO [dbo].[OBK_Ref_StageStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted])
     VALUES
           ('2'
           ,'inWork'
           ,'В работе'
           ,'В работе'
           ,'2017-01-01 00:00:00.000'
           ,'False')
GO
INSERT INTO [dbo].[OBK_Ref_StageStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted])
     VALUES
           ('3'
           ,'inReWork'
           ,'На даработке'
           ,'На даработке'
           ,'2017-01-01 00:00:00.000'
           ,'False')
GO
INSERT INTO [dbo].[OBK_Ref_StageStatus]
           ([Id]
           ,[Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted])
     VALUES
           ('4'
           ,'completed'
           ,'Выполнен'
           ,'Выполнен'
           ,'2017-01-01 00:00:00.000'
           ,'False')
GO
