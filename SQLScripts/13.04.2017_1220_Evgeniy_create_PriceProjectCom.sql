USE [ncels]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PriceProjectCom](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PriceProjectId] [uniqueidentifier] NOT NULL,
	[IsError] [bit] NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[ControlId] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_PriceProjectCom] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PriceProjectCom]  WITH CHECK ADD  CONSTRAINT [FK_PriceProjectCom_PriceProjects] FOREIGN KEY([PriceProjectId])
REFERENCES [dbo].[PriceProjects] ([Id])
GO

ALTER TABLE [dbo].[PriceProjectCom] CHECK CONSTRAINT [FK_PriceProjectCom_PriceProjects]
GO


