USE teme
GO

INSERT INTO dbo.Ref_DegreeRiskClasses
(
  Code
 ,DateCreate
 ,DateUpdate
 ,IsDeleted
 ,NameKz
 ,NameRu
)
VALUES
(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'Класс 1 - с низкой степенью риска'
 ,N'Класс 1 - с низкой степенью риска'
),(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'Класс 2а - со средней степенью риска'
 ,N'Класс 2а - со средней степенью риска'
),(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'Класс 2б - с повышенной степенью риска'
 ,N'Класс 2б - с повышенной степенью риска'
),(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'Класс 3 - с высокой степенью риска'
 ,N'Класс 3 - с высокой степенью риска'
);
GO