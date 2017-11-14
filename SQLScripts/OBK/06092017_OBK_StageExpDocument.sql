USE [ncelsProd]
GO

/****** Object:  Table [dbo].[OBK_StageExpDocument]    Script Date: 06.09.2017 16:27:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_StageExpDocument](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [int] NULL,
	[ProductSeriesId] [int] NULL,
	[ExpResult] [bit] NOT NULL,
	[ExpStartDate] [datetime] NULL,
	[ExpEndDate] [datetime] NULL,
	[ExpReasonNameRu] [nvarchar](MAX) NULL,
	[ExpReasonNameKz] [nvarchar](MAX) NULL,
	[ExpProductNameRu] [nvarchar](MAX) NULL,
	[ExpProductNameKz] [nvarchar](MAX) NULL,
	[ExpNomenclatureRu] [nvarchar](MAX) NULL,
	[ExpNomenclatureKz] [nvarchar](MAX) NULL,
	[ExpAddInfoRu] [nvarchar](MAX) NULL,
	[ExpAddInfoKz] [nvarchar](MAX) NULL,
	[ExpConclusionNumber] [nvarchar](50) NULL,
	[ExpBlankNumber] [nvarchar](50) NULL,
	[ExpApplication] [bit] NOT NULL,
	[ExpApplicationNumber] [nvarchar](50) NULL,
	[ExecutorId] [uniqueidentifier] NULL,
	[ExpExecutorSign] [nvarchar](MAX) NULL
 CONSTRAINT [PK_OBK_StageExpDocument] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO