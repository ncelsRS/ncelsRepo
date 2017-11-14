SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[EXP_MaterialDirectionsView]
AS
SELECT        Tmd.Id, Tmd.CreateDate, Tmd.DeleteDate, Tmd.Number, Tmd.DrugDeclarationId, Tdd.StatusId, Ts.Code AS StatusCode, Ts.Name AS StatusStr, Ts.NameKz AS StatusKzStr, Tmd.SendEmployeeId, Tmd.SendDate, 
                         Tmd.ExecutorEmployeeId, Tmd.ReceiveDate, Tmd.RejectDate, Tmd.Comment, Tdd.Number AS DdNumber, Tdd.CreatedDate AS RegisteredDate, Tdt.NameRu AS RegistrationTypeRu, 
                         Tdt.NameKz AS RegistrationTypeKz, Tdd.NameRu AS TradeNameRu, Tdd.NameKz AS TradeNameKz, Tdd.NameEn AS TradeNameEn, Tdd.Dosage, Tdd.DosageMeasureTypeId, 
                         Tdmt.name AS DosageMeasureTypeName, Tdd.ConcentrationRu, Tdd.ConcentrationKz, Tdf.name AS DrugFormName, Tdf.name_kz AS DrugFormNameKz, Tddt.NameRu AS DrugTypeNameRu, 
                         Tddt.NameKz AS DrugTypeNameKz, Tdo.NameRu AS ProducerNameRu, Tdo.NameKz AS ProducerNameKz, Tdo.NameEn AS ProducerNameEn, Tc.Name AS CountryName, Tc.NameKz AS CountryNameKz
FROM            dbo.EXP_MaterialDirections AS Tmd INNER JOIN
                         dbo.EXP_DrugDeclaration AS Tdd ON Tdd.Id = Tmd.DrugDeclarationId LEFT OUTER JOIN
                         dbo.EXP_DIC_Type AS Tdt ON Tdt.Id = Tdd.TypeId LEFT OUTER JOIN
                         dbo.EXP_DrugType AS Tdrugt ON Tdrugt.DrugDeclarationId = Tdd.Id LEFT OUTER JOIN
                         dbo.EXP_DIC_DrugType AS Tddt ON Tddt.Id = Tdrugt.DrugTypeId INNER JOIN
                         dbo.Dictionaries AS Ts ON Ts.Id = Tmd.StatusId LEFT OUTER JOIN
                         dbo.sr_measures AS Tdmt ON Tdmt.id = Tdd.DosageMeasureTypeId LEFT OUTER JOIN
                         dbo.sr_dosage_forms AS Tdf ON Tdf.id = Tdd.DrugFormId LEFT OUTER JOIN
                         dbo.EXP_DrugOrganizations AS Tdo ON Tdo.DrugDeclarationId = Tdd.Id LEFT OUTER JOIN
                         dbo.Dictionaries AS Tc ON Tc.Id = Tdo.CountryDicId

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
         Begin Table = "Tmd"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 234
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tdd"
            Begin Extent = 
               Top = 6
               Left = 272
               Bottom = 136
               Right = 518
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tdt"
            Begin Extent = 
               Top = 6
               Left = 556
               Bottom = 136
               Right = 726
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tdrugt"
            Begin Extent = 
               Top = 6
               Left = 764
               Bottom = 136
               Right = 949
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tddt"
            Begin Extent = 
               Top = 6
               Left = 987
               Bottom = 136
               Right = 1157
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Ts"
            Begin Extent = 
               Top = 6
               Left = 1195
               Bottom = 136
               Right = 1381
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tdmt"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 208
            End
            DisplayFlags = 280
       ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EXP_MaterialDirectionsView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'     TopColumn = 0
         End
         Begin Table = "Tdf"
            Begin Extent = 
               Top = 138
               Left = 246
               Bottom = 268
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tdo"
            Begin Extent = 
               Top = 138
               Left = 454
               Bottom = 268
               Right = 684
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tc"
            Begin Extent = 
               Top = 138
               Left = 722
               Bottom = 268
               Right = 908
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
      Begin ColumnWidths = 9
         Width = 284
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EXP_MaterialDirectionsView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EXP_MaterialDirectionsView'
GO


