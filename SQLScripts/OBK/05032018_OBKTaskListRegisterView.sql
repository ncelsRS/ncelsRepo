USE ncels
GO
DROP VIEW IF EXISTS dbo.OBKTaskListRegisterView
GO
SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
CREATE VIEW dbo.OBKTaskListRegisterView 
AS SELECT
  t.Id
 ,t.TaskNumber
 ,t.RegisterDate
 ,t.TaskEndDate
 ,e.ShortName
 ,t.UnitId
 ,te.StageId
 ,te.ExecutorId
 ,orss.NameRu AS StatusNameRu
 ,orss.NameKz AS StatusNameKz
 ,Cast(t.RegisterDate as datetime) AS RegDate
FROM dbo.OBK_Tasks t
LEFT OUTER JOIN dbo.OBK_TaskExecutor te
  ON t.Id = te.TaskId
INNER JOIN dbo.Employees e
  ON te.ExecutorId = e.Id
LEFT JOIN dbo.OBK_Ref_StageStatus orss 
  ON t.TaskStatusId = orss.Id