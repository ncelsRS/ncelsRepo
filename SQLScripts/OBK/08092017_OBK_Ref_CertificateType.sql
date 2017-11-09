USE [ncelsProd]
GO

/****** Object:  Table [dbo].[OBK_Ref_CertificateType]    Script Date: 08.09.2017 11:36:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_Ref_CertificateType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](2000) NOT NULL,
	[NameKz] [nvarchar](2000) NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_OBK_Ref_CertificateType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [dbo].[OBK_Ref_CertificateType]
           ([Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted])
     VALUES
           (null
           ,'GMP'
           ,'GMP'
           ,'2017-09-08 00:00:00.000'
           ,'False')
GO
INSERT INTO [dbo].[OBK_Ref_CertificateType]
           ([Code]
           ,[NameRu]
           ,[NameKz]
           ,[DateCreate]
           ,[IsDeleted])
     VALUES
           (null
           ,'ISO'
           ,'ISO'
           ,'2017-09-08 00:00:00.000'
           ,'False')
GO

