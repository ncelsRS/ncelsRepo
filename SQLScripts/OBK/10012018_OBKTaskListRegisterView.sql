USE ncels
GO

IF DB_NAME() <> N'ncels' SET NOEXEC ON
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Создать представление [dbo].[OBKTaskListRegisterView]
--
GO
PRINT (N'Создать представление [dbo].[OBKTaskListRegisterView]')
GO
CREATE OR ALTER VIEW dbo.OBKTaskListRegisterView 
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
FROM dbo.OBK_Tasks t
LEFT OUTER JOIN dbo.OBK_TaskExecutor te
  ON t.Id = te.TaskId
INNER JOIN dbo.Employees e
  ON te.ExecutorId = e.Id
LEFT JOIN dbo.OBK_Ref_StageStatus orss 
  ON t.TaskStatusId = orss.Id
GO

--
-- Добавить расширенное свойство [MS_DiagramPane1] для [dbo].[OBKTaskListRegisterView] (представление)
--
PRINT (N'Добавить расширенное свойство [MS_DiagramPane1] для [dbo].[OBKTaskListRegisterView] (представление)')
GO
EXEC sys.sp_addextendedproperty N'MS_DiagramPane1', N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "t"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 259
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "e"
            Begin Extent = 
               Top = 116
               Left = 292
               Bottom = 246
               Right = 526
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "te"
            Begin Extent = 
               Top = 6
               Left = 569
               Bottom = 136
               Right = 743
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', 'SCHEMA', N'dbo', 'VIEW', N'OBKTaskListRegisterView'
GO

--
-- Добавить расширенное свойство [MS_DiagramPaneCount] для [dbo].[OBKTaskListRegisterView] (представление)
--
PRINT (N'Добавить расширенное свойство [MS_DiagramPaneCount] для [dbo].[OBKTaskListRegisterView] (представление)')
GO
EXEC sys.sp_addextendedproperty N'MS_DiagramPaneCount', 1, 'SCHEMA', N'dbo', 'VIEW', N'OBKTaskListRegisterView'
GO