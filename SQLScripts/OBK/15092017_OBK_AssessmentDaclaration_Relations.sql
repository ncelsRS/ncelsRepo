ALTER TABLE [dbo].[OBK_AssessmentDeclaration]  WITH CHECK ADD  CONSTRAINT [FK_OBK_AssessmentDeclaration_Dictionaries] FOREIGN KEY([CertificateCountryId])
REFERENCES [dbo].[Dictionaries] ([Id])
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclaration] CHECK CONSTRAINT [FK_OBK_AssessmentDeclaration_Dictionaries]
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclaration]  WITH CHECK ADD  CONSTRAINT [FK_OBK_AssessmentDeclaration_OBK_Ref_CertificateType] FOREIGN KEY([CertificateTypeId])
REFERENCES [dbo].[OBK_Ref_CertificateType] ([Id])
GO

ALTER TABLE [dbo].[OBK_AssessmentDeclaration] CHECK CONSTRAINT [FK_OBK_AssessmentDeclaration_OBK_Ref_CertificateType]
GO