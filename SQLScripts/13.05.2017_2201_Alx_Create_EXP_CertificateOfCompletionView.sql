SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[EXP_CertificateOfCompletionView]
AS
SELECT        Tcc.Id, Tcc.Number, Tcc.DicStageId, Tcc.DrugDeclarationId, Tcc.TotalPrice, Tcc.StatusId, Tcc.CreateDate, Tcc.DeleteDate, Tcc.ModifyDate, Tcc.SendDate, Tcc.CreateEmployeeId, Tdd.NameRu AS TradeNameRu, 
                         Tdd.NameKz AS TradeNameKz, Tdd.NameEn AS TradeNameEn, Tmanuf.NameRu AS ManufacturerNameRu, Tmanuf.NameKz AS ManufacturerNameKz, Tmanuf.NameEn AS ManufacturerNameEn, 
                         TCountry.Name AS CountryName, TCountry.NameKz AS CountryNameKz, Tstatus.Name AS StatusStr, Tstatus.NameKz AS StatusStrKz, Tstatus.Code AS StatusCode, TCEmp.FullName AS CreateEmployeeStr, 
                         TStage.NameRu AS Stage, TStage.NameKz AS StageKz, Tat.TaskExecutorValue, Tat.TaskExecutorId, Tat.TaskExecutedDate, Tat.TaskComment
FROM            dbo.EXP_CertificateOfCompletion AS Tcc INNER JOIN
                         dbo.EXP_DrugDeclaration AS Tdd ON Tdd.Id = Tcc.DrugDeclarationId INNER JOIN
                         dbo.EXP_DrugOrganizations AS Tmanuf ON Tmanuf.DrugDeclarationId = Tdd.Id INNER JOIN
                         dbo.Dictionaries AS Tmt ON Tmt.Id = Tmanuf.OrgManufactureTypeDicId AND Tmt.Code = '1' INNER JOIN
                         dbo.Dictionaries AS TCountry ON TCountry.Id = Tmanuf.CountryDicId INNER JOIN
                         dbo.Dictionaries AS Tstatus ON Tstatus.Id = Tcc.StatusId INNER JOIN
                         dbo.Employees AS TCEmp ON TCEmp.Id = Tcc.CreateEmployeeId INNER JOIN
                         dbo.EXP_DIC_Stage AS TStage ON TStage.Id = Tcc.DicStageId LEFT OUTER JOIN
                         dbo.EXP_ActivityTaskActualView AS Tat ON Tat.DocumentId = Tcc.Id

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
         Begin Table = "Tcc"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 223
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tdd"
            Begin Extent = 
               Top = 6
               Left = 261
               Bottom = 136
               Right = 507
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tmanuf"
            Begin Extent = 
               Top = 6
               Left = 545
               Bottom = 136
               Right = 775
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tmt"
            Begin Extent = 
               Top = 6
               Left = 813
               Bottom = 136
               Right = 999
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TCountry"
            Begin Extent = 
               Top = 138
               Left = 310
               Bottom = 268
               Right = 496
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tstatus"
            Begin Extent = 
               Top = 6
               Left = 1037
               Bottom = 136
               Right = 1223
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TCEmp"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 272
            End
            DisplayFlags = 2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EXP_CertificateOfCompletionView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'80
            TopColumn = 0
         End
         Begin Table = "TStage"
            Begin Extent = 
               Top = 6
               Left = 1261
               Bottom = 136
               Right = 1431
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tat"
            Begin Extent = 
               Top = 138
               Left = 534
               Bottom = 268
               Right = 734
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
      Begin ColumnWidths = 22
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
         Width = 1500
         Width = 1500
         Width = 1500
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
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EXP_CertificateOfCompletionView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EXP_CertificateOfCompletionView'
GO


