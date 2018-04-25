USE teme
GO

INSERT INTO dbo.Ref_ValueAddedTaxes
(
  DateCreate
 ,DateUpdate
 ,IsDeleted
 ,Value
 ,Year
)
VALUES
(
  GETDATE()
 ,GETDATE()
 ,0
 ,12
 ,2018
);
GO