USE ncels
GO

DELETE FROM OBK_Ref_StageStatus WHERE Code IN('taskNew','taskAcceptCoz','taskSendRC','taskAcceptRC','taskSendLab')

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
  '99'
 ,N'taskNew'
 ,N'Сформирован'
 ,N'Сформирован'
 ,GETDATE()
 ,0
),
(
  '100'
 ,N'taskAcceptCoz'
 ,N'Образцы приняты ЦОЗ'
 ,N'Образцы приняты ЦОЗ'
 ,GETDATE()
 ,0
),
(
  '101'
 ,N'taskSendRC'
 ,N'Передано в ИЦл'
 ,N'Передано в ИЦл'
 ,GETDATE()
 ,0
),
(
  '102'
 ,N'taskAcceptRC'
 ,N'Принято ИЦл'
 ,N'Принято ИЦл'
 ,GETDATE()
 ,0
),
(
 '103'
 ,N'taskSendLab'
 ,N'Передано в лабораторию'
 ,N'Передано в лабораторию'
 ,GETDATE()
 ,0
);
GO

SELECT * FROM OBK_Ref_StageStatus