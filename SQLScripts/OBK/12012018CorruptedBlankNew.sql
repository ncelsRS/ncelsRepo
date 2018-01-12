USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_ZBKCopyCorruptedBlank]    Script Date: 12.01.2018 16:39:07 ******/
DROP TABLE [dbo].[OBK_ZBKCopyCorruptedBlank]
GO



USE [ncels]
GO

/****** Object:  Table [dbo].[OBK_ZBKCopyCorruptedBlank]    Script Date: 12.01.2018 16:37:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OBK_ZBKCopyCorruptedBlank](
	[Id] [uniqueidentifier] NOT NULL,
	[ZBKCopyBlankNumberId] [uniqueidentifier] NULL,
	[ZBKCopyId] [uniqueidentifier] NULL,
	[CorruptedBlankNumber] [int] NULL,
	[NewBlankNumber] [int] NULL,
	[EmployeeId] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_PK_OBK_ZBKCopyCorruptedBlank] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OBK_ZBKCopyCorruptedBlank]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ZBKCopyCorruptedBlank_OBK_ZBKCopy] FOREIGN KEY([ZBKCopyId])
REFERENCES [dbo].[OBK_ZBKCopy] ([Id])
GO

ALTER TABLE [dbo].[OBK_ZBKCopyCorruptedBlank] CHECK CONSTRAINT [FK_OBK_ZBKCopyCorruptedBlank_OBK_ZBKCopy]
GO


