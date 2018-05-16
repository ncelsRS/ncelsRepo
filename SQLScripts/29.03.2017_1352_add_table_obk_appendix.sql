CREATE TABLE [dbo].[obk_appendix](
	[id] [bigint] NOT NULL,
	[certification_id] [bigint] NOT NULL,
	[appendix_number] [varchar](50) NOT NULL,
	[comment] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_obk_appendix] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[obk_appendix] ADD  CONSTRAINT [DF_obk_appendix_comment]  DEFAULT ('') FOR [comment]
GO

ALTER TABLE [dbo].[obk_appendix]  WITH NOCHECK ADD  CONSTRAINT [FK_obk_appendix_certifications] FOREIGN KEY([certification_id])
REFERENCES [dbo].[obk_certifications] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[obk_appendix] NOCHECK CONSTRAINT [FK_obk_appendix_certifications]
GO


