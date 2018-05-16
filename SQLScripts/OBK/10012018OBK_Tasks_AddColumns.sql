ALTER TABLE OBK_Tasks ADD TaskStatusId INT NULL

UPDATE Units SET Code='supplyDivision' WHERE Id='9e06bc5d-14fd-4c3b-b819-a241cd197019'

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