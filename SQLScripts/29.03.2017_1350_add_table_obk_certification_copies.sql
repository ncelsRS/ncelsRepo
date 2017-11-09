CREATE TABLE [dbo].[obk_certification_copies](
	[id] [bigint] NOT NULL,
	[copies_application_id] [bigint] NOT NULL,
	[certification_id] [bigint] NOT NULL,
	[blank_begin_number] [varchar](50) NOT NULL,
	[blank_end_number] [varchar](50) NOT NULL,
	[certificate_reg_number] [varchar](50) NULL,
	[certificate_blank_number] [varchar](50) NULL,
	[comment] [varchar](250) NULL,
 CONSTRAINT [PK__obk_certification_copies] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[obk_certification_copies]  WITH NOCHECK ADD  CONSTRAINT [FK_obk_certification_copies_certifications] FOREIGN KEY([certification_id])
REFERENCES [dbo].[obk_certifications] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[obk_certification_copies] NOCHECK CONSTRAINT [FK_obk_certification_copies_certifications]
GO

