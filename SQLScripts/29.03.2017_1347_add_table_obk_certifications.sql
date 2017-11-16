CREATE TABLE [dbo].[obk_certifications](
	[id] [bigint] NOT NULL,
	[product_id] [bigint] NOT NULL,
	[reg_date] [smalldatetime] NULL,
	[end_date] [smalldatetime] NULL,
	[reg_number] [varchar](50) NOT NULL,
	[reg_number_kz] [nvarchar](50) NOT NULL,
	[blank_number] [varchar](50) NOT NULL,
	[product_name] [varchar](max) NOT NULL,
	[product_name_kz] [nvarchar](max) NULL,
	[add_info] [varchar](max) NOT NULL,
	[add_info_kz] [nvarchar](max) NULL,
	[date_prolongation] [smalldatetime] NULL,
	[annulment_sign] [bit] NOT NULL,
	[annulment_date] [smalldatetime] NULL,
	[annulment_reason] [varchar](max) NULL,
	[dublicate_sign] [bit] NOT NULL,
	[cert_dublicate_id] [bigint] NULL,
	[refuse_sign] [bit] NOT NULL,
	[refuse_date] [smalldatetime] NULL,
	[refuse_reason] [varchar](max) NULL,
	[argument] [varchar](max) NULL,
	[argument_kz] [nvarchar](max) NULL,
	[user_id] [bigint] NULL,
	[unlimited_sign] [bit] NULL,
 CONSTRAINT [PK_obk_certifications] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[obk_certifications] ADD  CONSTRAINT [DF_obk_certifications_annulment_sign]  DEFAULT ((0)) FOR [annulment_sign]
GO

ALTER TABLE [dbo].[obk_certifications] ADD  CONSTRAINT [DF_obk_certifications_dublicate_sign]  DEFAULT ((0)) FOR [dublicate_sign]
GO

ALTER TABLE [dbo].[obk_certifications] ADD  CONSTRAINT [DF_obk_certifications_refuse_sign]  DEFAULT ((0)) FOR [refuse_sign]
GO

ALTER TABLE [dbo].[obk_certifications]  WITH NOCHECK ADD  CONSTRAINT [FK__obk_certifications_products] FOREIGN KEY([product_id])
REFERENCES [dbo].obk_products ([id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[obk_certifications] NOCHECK CONSTRAINT [FK__obk_certifications_products]
GO


