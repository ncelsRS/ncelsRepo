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
 '98'
 ,N'onApprove'
 ,N'На утверждении'
 ,N'На утверждении'
 ,GETDATE()
 ,DEFAULT
);
GO