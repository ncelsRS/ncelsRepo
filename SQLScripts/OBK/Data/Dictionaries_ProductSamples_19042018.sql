--Маркировки
USE ncelsTest
GO

UPDATE Dictionaries SET Type='OldProductSample' WHERE Type='ProductSample'
GO

INSERT INTO dbo.Dictionaries
(
  Id
 ,Code
 ,Name
 ,Type
 ,ExpireDate
 ,Year
 ,Note
 ,DepartmentsId
 ,DepartmentsValue
 ,ParentId
 ,DisplayName
 ,EmployeesValue
 ,EmployeesId
 ,NameKz
 ,IsGuide
 ,OrganizationId
 ,AdditionalInfo
)
VALUES
(
  NEWID()
 ,N''
 ,N'в соответствии с ГОСТ 24861-91'
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'в соответствии с ГОСТ 24861-91'
 ,0
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'в соответствии с НД'
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'в соответствии с НД'
 ,0
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'в соответствии с ГФ РК'
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'в соответствии с ГФ РК'
 ,0
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'в соответствии с ГОСТ и НД фирмы'
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'в соответствии с ГОСТ и НД фирмы'
 ,0
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'в соответствии с ГОСТ 24861-91'
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'в соответствии с ГОСТ 24861-91'
 ,0
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'в соответствии с ГОСТ 17768-90Е и НД фирмы'
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'в соответствии с ГОСТ 17768-90Е и НД фирмы'
 ,0
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'в соответствии с ГОСТ 25047-87, ГОСТ 24861-91'
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'в соответствии с ГОСТ 25047-87, ГОСТ 24861-91'
 ,0
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'в соответствии с ТУ и ГОСТ'
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'в соответствии с ТУ и ГОСТ'
 ,0
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'в соответствии с ТУ 9393-046-17121966-2002'
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'в соответствии с ТУ 9393-046-17121966-2002'
 ,0
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'в соответствии с ТУ 9398-002-56257679-2004'
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'в соответствии с ТУ 9398-002-56257679-2004'
 ,0
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'в соответствии с ТУ 9432-003-52876351-2003'
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'в соответствии с ТУ 9432-003-52876351-2003'
 ,0
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'в соответствии с ТУ У6-00152253.014-96'
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'в соответствии с ТУ У6-00152253.014-96'
 ,0
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'в соответствии с ТУ фирмы'
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'в соответствии с ТУ фирмы'
 ,0
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'в соответствии с СП фирмы'
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'в соответствии с СП фирмы'
 ,0
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'соответствует '
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'соответствует '
 ,0
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'ГОСТ 1172-93'
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'ГОСТ 1172-93'
 ,0
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'ГОСТ ISO 4090-2011'
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'ГОСТ ISO 4090-2011'
 ,0
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'ГОСТ 16427-93'
 ,N'ProductSample'
 ,N''
 ,N''
 ,N''
 ,N''
 ,N''
 ,NULL
 ,N''
 ,N''
 ,N''
 ,N'ГОСТ 16427-93'
 ,0
 ,NULL
 ,N''
);
GO