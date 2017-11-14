USE [ncels]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PriceProjectFieldHistory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PriceProjectId] [uniqueidentifier] NOT NULL,
	[ControlId] [nvarchar](500) NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ValueField] [nvarchar](500) NULL,
	[DisplayField] [nvarchar](500) NULL,
 CONSTRAINT [PK_PriceProjectFieldHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PriceProjectFieldHistory]  WITH CHECK ADD  CONSTRAINT [FK_PriceProjectFieldHistory_Employees] FOREIGN KEY([UserId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[PriceProjectFieldHistory] CHECK CONSTRAINT [FK_PriceProjectFieldHistory_Employees]
GO

ALTER TABLE [dbo].[PriceProjectFieldHistory]  WITH CHECK ADD  CONSTRAINT [FK_PriceProjectFieldHistory_PriceProjects] FOREIGN KEY([PriceProjectId])
REFERENCES [dbo].[PriceProjects] ([Id])
GO

ALTER TABLE [dbo].[PriceProjectFieldHistory] CHECK CONSTRAINT [FK_PriceProjectFieldHistory_PriceProjects]
GO