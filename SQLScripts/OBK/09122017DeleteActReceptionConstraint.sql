ALTER TABLE [ncels].[dbo].[OBK_ActReception]
DROP CONSTRAINT UQ__OBK_ActR__3214EC062D20D4E7; 

ALTER TABLE [ncels].[dbo].[OBK_ActReception] ADD OBK_AssessmentDeclarationId uniqueidentifier

ALTER TABLE [dbo].[OBK_ActReception]  WITH CHECK ADD  CONSTRAINT [FK_OBK_ActReception_OBK_AssessmentDeclarationId] FOREIGN KEY([OBK_AssessmentDeclarationId])
REFERENCES [dbo].[OBK_AssessmentDeclaration] ([Id])
GO

ALTER TABLE [dbo].[OBK_ActReception] CHECK CONSTRAINT [FK_OBK_ActReception_OBK_AssessmentDeclarationId]
GO