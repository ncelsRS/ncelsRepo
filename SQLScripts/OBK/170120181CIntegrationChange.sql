USE [ncels]

ALTER TABLE [dbo].[I1c_primary_ObkApplications]
ADD ZBKCopyId UNIQUEIDENTIFIER NULL

ALTER TABLE [dbo].[I1c_primary_ObkPriceListElements]
ADD ZBKCopyId UNIQUEIDENTIFIER NULL

ALTER TABLE [dbo].[I1c_primary_ObkApplicationPaymentState]
ADD ZBKCopyId UNIQUEIDENTIFIER NULL

ALTER TABLE [dbo].[I1c_primary_ObkApplicationNotFullPaymentState]
ADD ZBKCopyId UNIQUEIDENTIFIER NULL
