CREATE TABLE [dbo].[sr_register_instructions_files](
	[id] [int] NOT NULL,
	[file_kaz] [image] NULL,
	[file_rus] [image] NULL,
	[file_ext_kz] [nvarchar](10) NULL,
	[file_ext] [nvarchar](10) NULL,
 CONSTRAINT [PK_sr_register_instructions_files] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[sr_register_instructions_files]  WITH NOCHECK ADD  CONSTRAINT [FK_sr_register_instructions_files_sr_register_instructions] FOREIGN KEY([id])
REFERENCES [dbo].[sr_register_instructions] ([id])
GO

ALTER TABLE [dbo].[sr_register_instructions_files] CHECK CONSTRAINT [FK_sr_register_instructions_files_sr_register_instructions]
GO


