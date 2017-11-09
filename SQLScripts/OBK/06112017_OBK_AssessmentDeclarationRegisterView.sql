USE [ncelsProd]
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OBK_AssessmentDeclarationRegisterView'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OBK_AssessmentDeclarationRegisterView'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OBK_AssessmentDeclarationRegisterView'
GO

/****** Object:  View [dbo].[OBK_AssessmentDeclarationRegisterView]    Script Date: 06.11.2017 15:16:58 ******/
DROP VIEW [dbo].[OBK_AssessmentDeclarationRegisterView]
GO

/****** Object:  View [dbo].[OBK_AssessmentDeclarationRegisterView]    Script Date: 06.11.2017 15:16:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[OBK_AssessmentDeclarationRegisterView]
AS
SELECT        ad.StatusId, ad.Number, ad.FirstSendDate, d.NameRu AS DeclarantName, [as].StageStatusId, [as].StartDate, [as].EndDate, ase.ExecutorId, rt.NameRu AS RegType, [as].Id AS StageId, s.NameRu AS StatusName, 
                         ss.Code AS StageStatusCode, rs.Code AS StageCode, d.CountryId, dic.Name AS CountryNameRu, c.Number AS ContractNumber
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
                         dbo.Dictionaries AS dic ON d.CountryId = dic.Id
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[56] 4[22] 2[19] 3) )"
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
         Top = -288
         Left = -144
      End
      Begin Tables = 
         Begin Table = "as"
            Begin Extent = 
               Top = 294
               Left = 182
               Bottom = 424
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
            TopColumn = 0
         End
         Begin Table = "dc"
            Begin Extent = 
               Top = 394
               Left = 444
               Bottom = 524
               Right = 650
            End
            DisplayFlags = 280
            TopColumn = 16
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
 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OBK_AssessmentDeclarationRegisterView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'        End
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
         Column = 2625
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OBK_AssessmentDeclarationRegisterView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OBK_AssessmentDeclarationRegisterView'
GO


