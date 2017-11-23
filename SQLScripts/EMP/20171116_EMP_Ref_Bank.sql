USE [ncels]
GO

/****** Object:  Table [dbo].[EMP_Ref_Bank]    Script Date: 17.11.2017 20:43:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_Ref_Bank](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[NameRu] [nvarchar](max) NOT NULL,
	[NameKz] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsConfirmed] [bit] NOT NULL,
 CONSTRAINT [PK_EMP_Ref_Bank] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


