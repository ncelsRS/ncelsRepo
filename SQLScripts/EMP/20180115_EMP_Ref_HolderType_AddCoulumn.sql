ALTER TABLE EMP_Ref_HolderType ADD Code NVARCHAR(20) NULL

UPDATE EMP_Ref_HolderType SET Code = N'national'

INSERT INTO dbo.EMP_Ref_HolderType
(
  Id
 ,NameRu
 ,NameKz
 ,IsDeleted
 ,Code
)
VALUES
(
  NEWID()
 ,N'Производитель'
 ,N'Производитель'
 ,0
 ,N'eaes'
),(
  NEWID()
 ,N'Уполномоченный представитель производителя'
 ,N'Уполномоченный представитель производителя'
 ,0
 ,N'eaes'
);