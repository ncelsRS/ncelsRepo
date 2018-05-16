USE [ncelsProd]
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OBK_AssessmentDeclarationView'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OBK_AssessmentDeclarationView'
GO

/****** Object:  View [dbo].[OBK_AssessmentDeclarationView]    Script Date: 06.11.2017 15:17:30 ******/
DROP VIEW [dbo].[OBK_AssessmentDeclarationView]
GO

/****** Object:  View [dbo].[OBK_AssessmentDeclarationView]    Script Date: 06.11.2017 15:17:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[OBK_AssessmentDeclarationView]
AS
SELECT        ad.Id, ad.EmployeeId AS OwnerId, ad.TypeId, ad.StatusId, ad.Number, ad.SendDate, rs.NameRu AS StausName, rt.NameRu AS TypeName, ad.CreatedDate, (CASE WHEN DesignDate IS NOT NULL 
                         THEN DesignDate ELSE ad.CreatedDate END) AS SortDate
FROM            dbo.OBK_AssessmentDeclaration AS ad INNER JOIN
                         dbo.OBK_Ref_Status AS rs ON ad.StatusId = rs.Id INNER JOIN
                         dbo.OBK_Ref_Type AS rt ON ad.TypeId = rt.Id
WHERE        (ad.IsDeleted = 'false')
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[49] 4[26] 2[23] 3) )"
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
         Begin Table = "ad"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 253
               Right = 272
            End
            DisplayFlags = 280
            TopColumn = 26
         End
         Begin Table = "rs"
            Begin Extent = 
               Top = 10
               Left = 788
               Bottom = 197
               Right = 962
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "rt"
            Begin Extent = 
               Top = 18
               Left = 1065
               Bottom = 148
               Right = 1239
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
         Column = 4515
         Alias = 2775
         Table = 3660
         Output = 1020
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 2100
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OBK_AssessmentDeclarationView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OBK_AssessmentDeclarationView'
GO


