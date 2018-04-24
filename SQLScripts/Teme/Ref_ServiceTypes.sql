USE teme
GO

INSERT INTO dbo.Ref_ServiceTypes
(
  Code
 ,DateCreate
 ,DateUpdate
 ,IsDeleted
 ,NameKz
 ,NameRu
 ,ParentId
 ,ApplicationTypeId
)
VALUES
(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'Класс 1 - базовая'
 ,N'Класс 1 - базовая'
 ,NULL
 ,1
),(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'Класс 2А - базовая ставка'
 ,N'Класс 2А - базовая ставка'
 ,NULL
 ,1
),(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'Класс 2Б - базовая ставка'
 ,N'Класс 2Б - базовая ставка'
 ,NULL
 ,1
),(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'Класс 3 - базовая ставка'
 ,N'Класс 3 - базовая ставка'
 ,NULL
 ,1
),(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'Экспертиза при внесении изменений в регистрационное досье изделий медицинского назначения и медицинской техники'
 ,N'Экспертиза при внесении изменений в регистрационное досье изделий медицинского назначения и медицинской техники'
 ,NULL
 ,2
);
GO