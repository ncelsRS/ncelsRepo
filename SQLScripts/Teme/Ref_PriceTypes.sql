USE teme
GO

INSERT INTO dbo.Ref_PriceTypes
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
 ,N'1 лекарственный препарат'
 ,N'1 лекарственный препарат'
),(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'1 лекарственная доза'
 ,N'1 лекарственная доза'
),(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'1 фасовка'
 ,N'1 фасовка'
),(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'1 вид'
 ,N'1 вид'
),(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'1 изделие'
 ,N'1 изделие'
),(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'1 набор'
 ,N'1 набор'
),(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'1 заключение'
 ,N'1 заключение'
),(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'1 копия'
 ,N'1 копия'
),(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'1 дубликат'
 ,N'1 дубликат'
),(
  N''
 ,GETDATE()
 ,GETDATE()
 ,0
 ,N'1 экспертиза'
 ,N'1 экспертиза'
);
GO