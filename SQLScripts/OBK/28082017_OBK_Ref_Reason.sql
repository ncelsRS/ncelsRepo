USE [ncelsProd]
GO

/****** Object:  Table [dbo].[OBK_Ref_Reason]    Script Date: 28.08.2017 15:10:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_Ref_Reason](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[NameRu] [nvarchar](max) NOT NULL,
	[NameKz] [nvarchar](max) NOT NULL,
	[ExpertiseResult] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_OBK_Ref_Reason] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


