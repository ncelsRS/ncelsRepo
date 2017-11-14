CREATE TABLE [dbo].[obk_appendix_series](
	[id] [bigint] NOT NULL,
	[appendix_id] [bigint] NOT NULL,
	[product_id] [bigint] NOT NULL,
 CONSTRAINT [PK_obk_appendix_series] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[obk_appendix_series]  WITH NOCHECK ADD  CONSTRAINT [FK_obk_appendix_series_appendix] FOREIGN KEY([appendix_id])
REFERENCES [dbo].[obk_appendix] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[obk_appendix_series] NOCHECK CONSTRAINT [FK_obk_appendix_series_appendix]
GO

ALTER TABLE [dbo].[obk_appendix_series]  WITH NOCHECK ADD  CONSTRAINT [FK_obk_appendix_series_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[obk_products] ([id])
GO

ALTER TABLE [dbo].[obk_appendix_series] NOCHECK CONSTRAINT [FK_obk_appendix_series_products]
GO


