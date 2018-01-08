USE ncels
GO

INSERT INTO dbo.OBK_Ref_StageStatus
(
  Id
 ,Code
 ,NameRu
 ,NameKz
 ,DateCreate
 ,IsDeleted
)
VALUES
(
  '15'
 ,N'taskNew'
 ,N'Сформирован'
 ,N'Сформирован'
 ,GETDATE()
 ,0
),
(
  '16'
 ,N'taskAcceptCoz'
 ,N'Образцы приняты ЦОЗ'
 ,N'Образцы приняты ЦОЗ'
 ,GETDATE()
 ,0
),
(
  '17'
 ,N'taskSendRC'
 ,N'Передано в ИЦл'
 ,N'Передано в ИЦл'
 ,GETDATE()
 ,0
),
(
  '18'
 ,N'taskAcceptRC'
 ,N'Принято ИЦл'
 ,N'Принято ИЦл'
 ,GETDATE()
 ,0
),
(
 '19'
 ,N'taskSendLab'
 ,N'Передано в лабораторию'
 ,N'Передано в лабораторию'
 ,GETDATE()
 ,0
);
GO