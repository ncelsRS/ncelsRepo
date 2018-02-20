USE ncels
GO

DROP VIEW IF EXISTS dbo.OBK_AssessmentDeclarationRegisterView
GO


SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Создать представление [dbo].[OBK_AssessmentDeclarationRegisterView]
--
GO
PRINT (N'Создать представление [dbo].[OBK_AssessmentDeclarationRegisterView]')
GO
CREATE VIEW dbo.OBK_AssessmentDeclarationRegisterView 
AS SELECT      ad.Id AS DeclarationId, ad.StatusId, ad.Number, ad.FirstSendDate, d.NameRu AS DeclarantName, [as].StageStatusId, [as].StartDate, [as].EndDate, ase.ExecutorId, rt.NameRu AS RegType, [as].Id AS StageId, s.NameRu AS StatusName, 
                         ss.Code AS StageStatusCode, ss.NameRu AS StageStatusNameRu, ss.NameKz AS StageStatusNameKz, rs.Code AS StageCode, d.CountryId, dic.Name AS CountryNameRu, c.Number AS ContractNumber,
                             (SELECT        COUNT(*) AS Expr1
                               FROM            dbo.OBK_RS_Products AS rs
                               WHERE        (ContractId = c.Id)) AS ProductsCount, dbo.OBK_Procunts_Series.Series
FROM            dbo.OBK_AssessmentStage AS [as] INNER JOIN
                         dbo.OBK_AssessmentDeclaration AS ad ON [as].DeclarationId = ad.Id INNER JOIN
                         dbo.OBK_AssessmentStageExecutors AS ase ON [as].Id = ase.AssessmentStageId INNER JOIN
                         dbo.OBK_Declarant AS d INNER JOIN
                         dbo.OBK_Contract AS c ON d.Id = c.DeclarantId INNER JOIN
                         dbo.OBK_DeclarantContact AS dc ON c.DeclarantContactId = dc.Id ON ad.ContractId = c.Id INNER JOIN
                         dbo.OBK_Ref_Type AS rt ON ad.TypeId = rt.Id INNER JOIN
                         dbo.OBK_Ref_Status AS s ON ad.StatusId = s.Id INNER JOIN
                         dbo.OBK_Ref_StageStatus AS ss ON [as].StageStatusId = ss.Id INNER JOIN
                         dbo.OBK_Ref_Stage AS rs ON [as].StageId = rs.Id INNER JOIN
                         dbo.Dictionaries AS dic ON d.CountryId = dic.Id INNER JOIN
                         dbo.OBK_Procunts_Series ON rt.Id = dbo.OBK_Procunts_Series.Id
GO

--
-- Добавить расширенное свойство [MS_DiagramPane1] для [dbo].[OBK_AssessmentDeclarationRegisterView] (представление)
--
PRINT (N'Добавить расширенное свойство [MS_DiagramPane1] для [dbo].[OBK_AssessmentDeclarationRegisterView] (представление)')
GO
EXEC sys.sp_addextendedproperty N'MS_DiagramPane1', N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[48] 4[24] 2[28] 3) )"
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
         Top = -384
         Left = -144
      End
      Begin Tables = 
         Begin Table = "as"
            Begin Extent = 
               Top = 486
               Left = 182
               Bottom = 616
               Right = 377
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ad"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 183
               Right = 272
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "ase"
            Begin Extent = 
               Top = 159
               Left = 680
               Bottom = 255
               Right = 870
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 2
               Left = 710
               Bottom = 132
               Right = 905
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 2
               Left = 419
               Bottom = 152
               Right = 610
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "dc"
            Begin Extent = 
               Top = 394
               Left = 444
               Bottom = 524
               Right = 650
            End
            DisplayFlags = 280
            TopColumn = 17
         End
         Begin Table = "rt"
            Begin Extent = 
               Top = 277
               Left = 712
               Bottom = 407
               Right = 886
            End
            DisplayFlags = 280
            TopColumn = 0
 ', 'SCHEMA', N'dbo', 'VIEW', N'OBK_AssessmentDeclarationRegisterView'
GO

--
-- Добавить расширенное свойство [MS_DiagramPane2] для [dbo].[OBK_AssessmentDeclarationRegisterView] (представление)
--
PRINT (N'Добавить расширенное свойство [MS_DiagramPane2] для [dbo].[OBK_AssessmentDeclarationRegisterView] (представление)')
GO
EXEC sys.sp_addextendedproperty N'MS_DiagramPane2', N'        End
         Begin Table = "s"
            Begin Extent = 
               Top = 263
               Left = 70
               Bottom = 393
               Right = 244
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ss"
            Begin Extent = 
               Top = 517
               Left = 276
               Bottom = 647
               Right = 450
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "rs"
            Begin Extent = 
               Top = 422
               Left = 709
               Bottom = 552
               Right = 883
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dic"
            Begin Extent = 
               Top = 180
               Left = 323
               Bottom = 310
               Right = 509
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "OBK_Procunts_Series"
            Begin Extent = 
               Top = 528
               Left = 488
               Bottom = 658
               Right = 680
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
         Column = 3135
         Alias = 2880
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
', 'SCHEMA', N'dbo', 'VIEW', N'OBK_AssessmentDeclarationRegisterView'
GO

--
-- Добавить расширенное свойство [MS_DiagramPaneCount] для [dbo].[OBK_AssessmentDeclarationRegisterView] (представление)
--
PRINT (N'Добавить расширенное свойство [MS_DiagramPaneCount] для [dbo].[OBK_AssessmentDeclarationRegisterView] (представление)')
GO
EXEC sys.sp_addextendedproperty N'MS_DiagramPaneCount', 2, 'SCHEMA', N'dbo', 'VIEW', N'OBK_AssessmentDeclarationRegisterView'
GO