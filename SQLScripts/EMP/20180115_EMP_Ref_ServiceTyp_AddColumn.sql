﻿ALTER TABLE EMP_Ref_ServiceType ADD Code NVARCHAR(20) NULL
GO 
UPDATE EMP_Ref_ServiceType SET Code = N'national'
GO
INSERT INTO EMP_Ref_ServiceType
(
  Id
 ,NameRu
 ,NameKz
 ,ParentId
 ,ChangeType
 ,IsDeleted
 ,DegreeRiskId
 ,Code
)
VALUES
(
  '106034C9-78E6-4DE4-ABBB-EB27C325D733'
 ,N'Экспертиза при регистрации (перерегистрации) медицинских изделий (изделий медицинского назначения и медицинской техники)'
 ,N'Экспертиза при регистрации (перерегистрации) медицинских изделий (изделий медицинского назначения и медицинской техники)'
 ,NULL
 ,0
 ,0
 ,NULL
 ,'eaes'
),(
  'EE0A8D6B-FA09-4FD1-ACF9-84069582C762'
 ,N'Класс 1 - базовая'
 ,N'Класс 1 - базовая'
 ,'106034C9-78E6-4DE4-ABBB-EB27C325D733'
 ,0
 ,0
 ,NULL
 ,'eaes'
),(
  'BFD7982D-2517-41E6-97CA-19AC8602D02D'
 ,N'Класс 2А - базовая ставка'
 ,N'Класс 2А - базовая ставка'
 ,'106034C9-78E6-4DE4-ABBB-EB27C325D733'
 ,0
 ,0
 ,NULL
 ,'eaes'
),(
  'C1ED22A7-8AD7-49D4-839C-8E85D1DBA373'
 ,N'Класс 2Б - базовая ставка'
 ,N'Класс 2Б - базовая ставка'
 ,'106034C9-78E6-4DE4-ABBB-EB27C325D733'
 ,0
 ,0
 ,NULL
 ,'eaes'
),(
  '2A7ED9EC-9976-484A-9D56-35E2F76583DD'
 ,N'Класс 3 - базовая ставка'
 ,N'Класс 3 - базовая ставка'
 ,'106034C9-78E6-4DE4-ABBB-EB27C325D733'
 ,0
 ,0
 ,NULL
 ,'eaes'
),(
  '20635B2A-2F53-4BE3-9306-936E52FBC0E3'
 ,N'Экспертиза при внесении изменений в регистрационное досье медицинских изделий (изделий медицинского назначения и медицинской техники)'
 ,N'Экспертиза при внесении изменений в регистрационное досье медицинских изделий (изделий медицинского назначения и медицинской техники)'
 ,NULL
 ,0
 ,0
 ,NULL
 ,'eaes'
),(
  '305BDFB0-8F8D-41BA-9763-E904487603CA'
 ,N'Экспертиза при внесении изменений в регистрационное досье изделий медицинского назначения и медицинской техники'
 ,N'Экспертиза при внесении изменений в регистрационное досье изделий медицинского назначения и медицинской техники'
 ,'20635B2A-2F53-4BE3-9306-936E52FBC0E3'
 ,0
 ,0
 ,NULL
 ,'eaes'
)
GO

INSERT INTO dbo.EMP_Ref_PriceList
(
  Id
 ,ServiceTypeId
 ,PriceTypeId
 ,Import
 ,Price
)
VALUES
(
  NEWID()
 ,'EE0A8D6B-FA09-4FD1-ACF9-84069582C762'
 ,'b066aa5c-d016-43bb-8ef3-0be62f5c7904'
 ,0
 ,308335 
),(
  NEWID()
 ,'BFD7982D-2517-41E6-97CA-19AC8602D02D'
 ,'b066aa5c-d016-43bb-8ef3-0be62f5c7904'
 ,0
 ,355360
),(
  NEWID()
 ,'C1ED22A7-8AD7-49D4-839C-8E85D1DBA373'
 ,'b066aa5c-d016-43bb-8ef3-0be62f5c7904'
 ,0
 ,396928
),(
  NEWID()
 ,'2A7ED9EC-9976-484A-9D56-35E2F76583DD'
 ,'b066aa5c-d016-43bb-8ef3-0be62f5c7904'
 ,0
 ,457219
),(
  NEWID()
 ,'305BDFB0-8F8D-41BA-9763-E904487603CA'
 ,'b066aa5c-d016-43bb-8ef3-0be62f5c7904'
 ,0
 ,346083
);
GO
