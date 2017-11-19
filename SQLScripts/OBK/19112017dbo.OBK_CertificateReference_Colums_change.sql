USE [ncels]
GO

ALTER TABLE dbo.OBK_CertificateReference DROP COLUMN DocumentId

ALTER TABLE dbo.OBK_CertificateReference ADD AttachPath nvarchar(300) NULL 



