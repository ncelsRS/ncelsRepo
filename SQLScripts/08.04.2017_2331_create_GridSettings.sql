CREATE TABLE [dbo].[GridSettings](
	[Id] [uniqueidentifier] NOT NULL,
	[EmployeeId] [uniqueidentifier] NOT NULL,
	[Model] [ntext] NOT NULL,
	[GridName] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_GridSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[GridSettings] ADD  CONSTRAINT [DF_GridSettings_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[GridSettings]  WITH CHECK ADD  CONSTRAINT [FK_GridSettings_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[GridSettings] CHECK CONSTRAINT [FK_GridSettings_Employees]
GO


