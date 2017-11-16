
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE VIEW [dbo].[EXP_ActivityTaskActualView]
AS
SELECT        Ta.Id, Ta.TypeId, Ta.TypeValue, Ta.Text, Ta.StatusId, Ta.StatusValue, Ta.AuthorId, Ta.AuthorValue, Ta.CreatedDate, Ta.ModifiedDate, Ta.ExecutedDate, Ta.ClosedDate, Ta.DocumentId, Ta.DocumentValue, 
                         Ta.DocumentTypeId, Ta.DocumentTypeValue, Tt.Id AS TaskId, Tt.TypeId AS TaskTypeId, Tt.TypeValue AS TaskTypeValue, Tt.StatusId AS TaskStatusId, Tt.StatusValue AS TaskStatusValue, 
                         Tt.OrderNumber AS TaskOrderNumber, Tt.Number AS TaskNumber, Tt.Text AS TaskText, Tt.CreatedDate AS TaskCreatedDate, Tt.ModifiedDate AS TaskModifiedDate, Tt.ExecutedDate AS TaskExecutedDate, 
                         Tt.ClosedDate AS TaskClosedDate, Tt.AuthorId AS TaskAuthorId, Tt.AuthorValue AS TaskAuthorValue, Tt.ExecutorId AS TaskExecutorId, Te.DisplayName AS TaskExecutorValue, Tt.ActivityId AS TaskActivityId, 
                         Tt.ParentTaskId AS TaskParentTaskId, Tt.Comment AS TaskComment
FROM            dbo.EXP_Activities AS Ta INNER JOIN
                             (SELECT        MAX(CreatedDate) AS MaxCreateDate, DocumentId
                               FROM            dbo.EXP_Activities
                               GROUP BY DocumentId) AS TTa ON TTa.DocumentId = Ta.DocumentId AND TTa.MaxCreateDate = Ta.CreatedDate LEFT OUTER JOIN
                         dbo.EXP_Tasks AS Tt ON Tt.ActivityId = Ta.Id AND Tt.StatusValue IN ('3', '2') INNER JOIN
                         dbo.Employees AS Te ON Te.Id = Tt.ExecutorId

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
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
         Configuration = "(H (1[50] 2[25] 3) )"
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
         Configuration = "(H (2[66] 3) )"
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
      ActivePaneConfig = 5
   End
   Begin DiagramPane = 
      PaneHidden = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Ta"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 238
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TTa"
            Begin Extent = 
               Top = 6
               Left = 276
               Bottom = 102
               Right = 446
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tt"
            Begin Extent = 
               Top = 6
               Left = 484
               Bottom = 136
               Right = 654
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Te"
            Begin Extent = 
               Top = 6
               Left = 692
               Bottom = 136
               Right = 926
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
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
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
         O' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EXP_ActivityTaskActualView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'r = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EXP_ActivityTaskActualView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EXP_ActivityTaskActualView'
GO


