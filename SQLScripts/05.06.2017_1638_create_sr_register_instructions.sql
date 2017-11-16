CREATE TABLE [dbo].[sr_register_instructions](
	[id] [int] NOT NULL,
	[register_id] [int] NOT NULL,
	[instruction_type_id] [int] NOT NULL CONSTRAINT [DF_sr_register_instructions_sr_instruction_type_id]  DEFAULT ((3)),
	[comment] [varchar](250) NULL,
 CONSTRAINT [PK_r_instructions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[sr_register_instructions]  WITH NOCHECK ADD  CONSTRAINT [FK_sr_register_instructions_sr_instruction_types] FOREIGN KEY([instruction_type_id])
REFERENCES [dbo].[sr_instruction_types] ([id])
GO

ALTER TABLE [dbo].[sr_register_instructions] CHECK CONSTRAINT [FK_sr_register_instructions_sr_instruction_types]
GO

ALTER TABLE [dbo].[sr_register_instructions]  WITH NOCHECK ADD  CONSTRAINT [FK_sr_register_instructions_sr_register] FOREIGN KEY([register_id])
REFERENCES [dbo].[sr_register] ([id])
GO

ALTER TABLE [dbo].[sr_register_instructions] CHECK CONSTRAINT [FK_sr_register_instructions_sr_register]
GO


