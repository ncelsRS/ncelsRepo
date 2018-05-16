USE [ncelsProd]
GO

/****** Object:  View [dbo].[OBK_AssessmentDeclarationView]    Script Date: 02.08.2017 15:44:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[OBK_AssessmentDeclarationView]
AS
SELECT        dbo.OBK_AssessmentDeclaration.Id, dbo.OBK_AssessmentDeclaration.OwnerId, dbo.OBK_AssessmentDeclaration.Type_Id, dbo.OBK_AssessmentDeclaration.StatusId, 
                         dbo.OBK_AssessmentDeclaration.Number, dbo.OBK_AssessmentDeclaration.SendDate, dbo.OBK_Ref_Status.NameRu AS StausName, dbo.OBK_Ref_Type.NameRu AS TypeName, 
                         dbo.OBK_AssessmentDeclaration.IsDeleted, dbo.OBK_AssessmentDeclaration.CreatedDate, (CASE WHEN DesignDate IS NOT NULL THEN DesignDate ELSE OBK_AssessmentDeclaration.CreatedDate END) 
                         AS SortDate
FROM            dbo.OBK_AssessmentDeclaration INNER JOIN
                         dbo.OBK_Ref_Status ON dbo.OBK_AssessmentDeclaration.StatusId = dbo.OBK_Ref_Status.Id INNER JOIN
                         dbo.OBK_Ref_Type ON dbo.OBK_AssessmentDeclaration.Type_Id = dbo.OBK_Ref_Type.Id
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[47] 4[27] 2[20] 3) )"
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
         Begin Table = "OBK_AssessmentDeclaration"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 253
               Right = 272
            End
            DisplayFlags = 280
            TopColumn = 18
         End
         Begin Table = "OBK_Ref_Status"
            Begin Extent = 
               Top = 10
               Left = 788
               Bottom = 197
               Right = 962
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "OBK_Ref_Type"
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
         Output = 720
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


