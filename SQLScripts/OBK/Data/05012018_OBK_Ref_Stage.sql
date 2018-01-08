USE ncels
GO

INSERT INTO dbo.OBK_Ref_Stage
(
  Id
 ,Code
 ,NameRu
 ,NameKz
 ,DateCreate
 ,IsDeleted
 ,DateEdit
)
VALUES
(
  6
 ,N'6'
 ,N'Испытательный центр'
 ,N'Испытательный центр'
 ,GETDATE()
 ,0
 ,GETDATE()
),
(
  7
 ,N'7'
 ,N'ИЦл/ИЛ'
 ,N'ИЦл/ИЛ'
 ,GETDATE()
 ,0
 ,GETDATE()
);
GO