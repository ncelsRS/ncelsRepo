alter table Organizations
add OriginalOrgId uniqueidentifier;


ALTER TABLE Organizations  WITH CHECK ADD  CONSTRAINT [FK_Organizations_OriginalOrgId_Organizations] FOREIGN KEY(OriginalOrgId)
REFERENCES [dbo].[Organizations] ([Id])
GO

ALTER TABLE Organizations CHECK CONSTRAINT [FK_Organizations_OriginalOrgId_Organizations]
GO


