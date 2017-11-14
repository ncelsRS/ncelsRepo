ALTER TABLE [dbo].[EXP_Materials] ADD [ConcentrationUnitId] uniqueidentifier NULL;
GO

ALTER TABLE [dbo].[EXP_Materials] ALTER COLUMN [ProducerId] uniqueidentifier NULL;
GO

ALTER TABLE [dbo].[EXP_Materials] ALTER COLUMN [CountryId] uniqueidentifier NULL;
GO

ALTER TABLE [dbo].[EXP_Materials]  WITH CHECK ADD  CONSTRAINT [FK_EXP_Materials_ConcentrationUnit] FOREIGN KEY([ConcentrationUnitId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[EXP_Materials] CHECK CONSTRAINT [FK_EXP_Materials_ConcentrationUnit]
GO