  -- Дополнения в БД для миграции данных
  ALTER TABLE OBK_CertificateReference ADD CertificateScope NVARCHAR(4000) NULL
  ALTER TABLE OBK_CertificateReference ADD CertificateAddressLocation NVARCHAR(500) NULL