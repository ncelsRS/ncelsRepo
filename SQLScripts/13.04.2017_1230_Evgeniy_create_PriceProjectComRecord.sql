USE [ncels]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PriceProjectComRecord](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CommentId] [bigint] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ValueField] [nvarchar](500) NULL,
	[Note] [nvarchar](2000) NULL,
	[DisplayField] [nvarchar](500) NULL,
 CONSTRAINT [PK_PriceProjectComRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PriceProjectComRecord]  WITH CHECK ADD  CONSTRAINT [FK_PriceProjectComRecord_PriceProjectCom] FOREIGN KEY([CommentId])
REFERENCES [dbo].[PriceProjectCom] ([Id])
GO

ALTER TABLE [dbo].[PriceProjectComRecord] CHECK CONSTRAINT [FK_PriceProjectComRecord_PriceProjectCom]
GO


