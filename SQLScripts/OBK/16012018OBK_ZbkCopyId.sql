ALTER TABLE [ncels].[dbo].[OBK_CertificateOfCompletion]
ADD ZBKCopyId UNIQUEIDENTIFIER NULL

ALTER TABLE [dbo].[I1c_primary_ObkApplications]
ADD ZBKCopyId UNIQUEIDENTIFIER NULL

ALTER TABLE [dbo].[I1c_primary_ObkPriceListElements]
ADD ZBKCopyId UNIQUEIDENTIFIER NULL

ALTER TABLE [dbo].[I1c_primary_ObkApplicationPaymentState]
ADD ZBKCopyId UNIQUEIDENTIFIER NULL

ALTER TABLE [dbo].[I1c_primary_ObkApplicationNotFullPaymentState]
ADD ZBKCopyId UNIQUEIDENTIFIER NULL

ALTER TABLE [ncels].[dbo].[I1c_act_ObkApplication]
ADD ZBKCopyId UNIQUEIDENTIFIER NULL

 ALTER TABLE [ncels].[dbo].[OBK_CertificateOfCompletion] ALTER COLUMN [ContractId] UNIQUEIDENTIFIER NULL
 ALTER TABLE [ncels].[dbo].[OBK_CertificateOfCompletion] ALTER COLUMN [AssessmentDeclarationId] UNIQUEIDENTIFIER NULL
