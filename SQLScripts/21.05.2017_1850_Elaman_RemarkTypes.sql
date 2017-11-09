USE [ncels]
CREATE TABLE [dbo].[RemarkTypes] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	PRIMARY KEY (Id)
);

INSERT INTO [dbo].[RemarkTypes] (Name) VALUES ('Замечания')
INSERT INTO [dbo].[RemarkTypes] (Name) VALUES ('Приглашение на переговоры')

ALTER TABLE [dbo].[Documents]
ADD RemarkId integer CONSTRAINT fk FOREIGN KEY (RemarkId) REFERENCES [dbo].[RemarkTypes](Id); 