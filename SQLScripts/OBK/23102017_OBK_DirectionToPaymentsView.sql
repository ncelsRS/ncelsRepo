USE [ncelsProd]
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OBK_DirectionToPaymentsView'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OBK_DirectionToPaymentsView'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OBK_DirectionToPaymentsView'
GO

/****** Object:  View [dbo].[OBK_DirectionToPaymentsView]    Script Date: 23.10.2017 11:30:09 ******/
DROP VIEW [dbo].[OBK_DirectionToPaymentsView]
GO

/****** Object:  View [dbo].[OBK_DirectionToPaymentsView]    Script Date: 23.10.2017 11:30:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[OBK_DirectionToPaymentsView]
AS
SELECT        pay.CreateDate, pay.ContractId, pay.CreateEmployeeId, pay.CreateEmployeeValue, pay.PayerId, pay.PayerValue, pay.StatusId, pay.IsDeleted, emp.DisplayName, pay.TotalPrice, ref_status.Code AS StatusCode, 
                         pay.InvoiceNumber, contract.Number AS ContractNumber, contract.StartDate AS ContractStartDate, pay.Id, pay.InvoiceNumber1C
FROM            dbo.OBK_DirectionToPayments AS pay INNER JOIN
                         dbo.Employees AS emp ON pay.CreateEmployeeId = emp.Id INNER JOIN
                         dbo.OBK_Ref_PaymentStatus AS ref_status ON pay.StatusId = ref_status.Id INNER JOIN
                         dbo.OBK_Contract AS contract ON pay.ContractId = contract.Id
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[54] 4[16] 2[31] 3) )"
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
         Begin Table = "pay"
            Begin Extent = 
               Top = 11
               Left = 16
               Bottom = 300
               Right = 220
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "emp"
            Begin Extent = 
               Top = 177
               Left = 637
               Bottom = 466
               Right = 871
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "ref_status"
            Begin Extent = 
               Top = 244
               Left = 368
               Bottom = 374
               Right = 542
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "contract"
            Begin Extent = 
               Top = 0
               Left = 367
               Bottom = 173
               Right = 558
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
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
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1740
         Alias = 2190
         Table = 1905
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
       ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OBK_DirectionToPaymentsView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'  Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OBK_DirectionToPaymentsView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OBK_DirectionToPaymentsView'
GO


