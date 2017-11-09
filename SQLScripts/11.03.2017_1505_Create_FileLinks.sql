create table FileLinks(
	Id uniqueidentifier not null,
	CreateDate datetime not null,
	FileName nvarchar(4000) not null,
	Version int,
	DocumentId uniqueidentifier,
	CategoryId uniqueidentifier,
	ParentId uniqueidentifier,
	CONSTRAINT [PK_FileLinks] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)
GO
ALTER TABLE FileLinks ADD  CONSTRAINT DF_FileLinks_Id  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE FileLinks  WITH CHECK ADD  CONSTRAINT [FK_FileLinksCategoryId_Dictionaries] FOREIGN KEY(CategoryId)
REFERENCES Dictionaries (Id)
GO
ALTER TABLE FileLinks CHECK CONSTRAINT [FK_FileLinksCategoryId_Dictionaries]
GO
ALTER TABLE FileLinks  WITH CHECK ADD  CONSTRAINT [FK_FileLinksParentId_FileLinks] FOREIGN KEY(ParentId)
REFERENCES FileLinks (Id)
GO
ALTER TABLE FileLinks CHECK CONSTRAINT [FK_FileLinksParentId_FileLinks]
GO