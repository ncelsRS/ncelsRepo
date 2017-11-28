USE [ncels]
GO

/****** Object:  Table [dbo].[EMP_Ref_ServiceType]    Script Date: 20.11.2017 17:50:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMP_Ref_ServiceType](
	[Id] [uniqueidentifier] NOT NULL,
	[NameRu] [nvarchar](255) NOT NULL,
	[NameKz] [nvarchar](255) NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_EMP_Ref_ServiceType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EMP_Ref_ServiceType]  WITH CHECK ADD  CONSTRAINT [FK_EMP_Ref_ServiceType_EMP_Ref_ServiceType] FOREIGN KEY([ParentId])
REFERENCES [dbo].[EMP_Ref_ServiceType] ([Id])
GO

ALTER TABLE [dbo].[EMP_Ref_ServiceType] CHECK CONSTRAINT [FK_EMP_Ref_ServiceType_EMP_Ref_ServiceType]
GO


