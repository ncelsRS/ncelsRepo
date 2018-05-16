DELETE FROM [dbo].[EXP_DirectionToPays_DrugDeclaration]
GO

DELETE FROM [dbo].[EXP_DirectionToPays_PriceList]
GO




ALTER TABLE [dbo].[EXP_DirectionToPays_DrugDeclaration] DROP CONSTRAINT [FK_EXP_DirectionToPays_DrugDeclaration_EXP_DirectionToPays]
GO

ALTER TABLE [dbo].[EXP_DirectionToPays_PriceList] DROP CONSTRAINT [FK_EXP_DirectionToPays_PriceList_EXP_DirectionToPays]
GO

DROP TABLE [dbo].[EXP_DirectionToPays]
GO

CREATE TABLE [dbo].[EXP_DirectionToPays](
	[Id] [uniqueidentifier] NOT NULL,
	[Number] [nvarchar](512) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[DeleteDate] [datetime] NULL,
	[DirectionDate] [datetime] NOT NULL,
	[Type] [int] NULL,
	[TypeValue] [nvarchar](512) NULL,
	[PayerId] [uniqueidentifier] NULL,
	[PayerValue] [nvarchar](4000) NULL,
	[CreateEmployeeId] [uniqueidentifier] NOT NULL,
	[CreateEmployeeValue] [nvarchar](1024) NOT NULL,
	[TotalPrice] [decimal](18, 2) NULL,
	[InvoiceCode1C] [nvarchar](512) NULL,
	[InvoiceNumber1C] [nvarchar](512) NULL,
	[InvoiceDatetime1C] [datetime] NULL,
	[PaymentDatetime1C] [datetime] NULL,
	[PaymentValue1C] [decimal](18, 2) NULL,
	[PaymentComment1C] [nvarchar](4000) NULL,
 CONSTRAINT [PK_EXP_DirectionToPays] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_DirectionToPays] ADD  CONSTRAINT [DF_EXP_DirectionToPays_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[EXP_DirectionToPays] ADD  CONSTRAINT [DF_EXP_DirectionToPays_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[EXP_DirectionToPays] ADD  CONSTRAINT [DF_EXP_DirectionToPays_ApplicationDate]  DEFAULT (getdate()) FOR [DirectionDate]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Номер заявки' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_DirectionToPays', @level2type=N'COLUMN',@level2name=N'Number'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Дата создания' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EXP_DirectionToPays', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO



ALTER TABLE [dbo].[EXP_DirectionToPays_PriceList]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DirectionToPays_PriceList_EXP_DirectionToPays] FOREIGN KEY([DirectionToPayId])
REFERENCES [dbo].[EXP_DirectionToPays] ([Id])
GO

ALTER TABLE [dbo].[EXP_DirectionToPays_PriceList] CHECK CONSTRAINT [FK_EXP_DirectionToPays_PriceList_EXP_DirectionToPays]
GO



ALTER TABLE [dbo].[EXP_DirectionToPays_DrugDeclaration]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DirectionToPays_DrugDeclaration_EXP_DirectionToPays] FOREIGN KEY([DirectionToPayId])
REFERENCES [dbo].[EXP_DirectionToPays] ([Id])
GO

ALTER TABLE [dbo].[EXP_DirectionToPays_DrugDeclaration] CHECK CONSTRAINT [FK_EXP_DirectionToPays_DrugDeclaration_EXP_DirectionToPays]
GO



DROP VIEW [dbo].[EXP_DirectionToPaysView]
GO

CREATE VIEW [dbo].[EXP_DirectionToPaysView]
AS
SELECT Tdp.[Id],
       Tdp.[Number],
       Tdp.[DirectionDate] AS [DirectionDate],
       STUFF (
                (SELECT DISTINCT '; ' + Tt.Number
                 FROM [dbo].[EXP_DrugDeclaration] Tt
                 INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DrugDeclarationId = Tt.Id
                 WHERE Td.DirectionToPayId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS ApplicationNumbers,
       STUFF (
                (SELECT DISTINCT '; ' + CONVERT(NVARCHAR(12), Tt.CreatedDate, 104)
                 FROM [dbo].[EXP_DrugDeclaration] Tt
                 INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DrugDeclarationId = Tt.Id
                 WHERE Td.DirectionToPayId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS ApplicationDates,
       STUFF (
                (SELECT DISTINCT '; ' + Tdt.NameRu
                 FROM [dbo].[EXP_DrugDeclaration] Tt
                 INNER JOIN [dbo].[EXP_DIC_Type] AS Tdt ON Tdt.Id = Tt.TypeId
                 INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DrugDeclarationId = Tt.Id
                 WHERE Td.DirectionToPayId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS RegistrationForm,
       STUFF (
                (SELECT DISTINCT '; ' + Tt.NameKz
                 FROM [dbo].[EXP_DrugDeclaration] Tt
                 INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DrugDeclarationId = Tt.Id
                 WHERE Td.DirectionToPayId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS TradeNameKz,
       STUFF (
                (SELECT DISTINCT '; ' + Tt.NameRu
                 FROM [dbo].[EXP_DrugDeclaration] Tt
                 INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DrugDeclarationId = Tt.Id
                 WHERE Td.DirectionToPayId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS TradeNameRu,
       STUFF (
                (SELECT DISTINCT '; ' + Tt.NameEn
                 FROM [dbo].[EXP_DrugDeclaration] Tt
                 INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DrugDeclarationId = Tt.Id
                 WHERE Td.DirectionToPayId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS TradeNameEn,
       STUFF (
                (SELECT DISTINCT '; ' + Tt.DrugFormKz
                 FROM [dbo].[EXP_DrugDeclaration] Tt
                 INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DrugDeclarationId = Tt.Id
                 WHERE Td.DirectionToPayId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS DrugFormKz,
       STUFF (
                (SELECT DISTINCT '; ' + Tt.DrugFormRu
                 FROM [dbo].[EXP_DrugDeclaration] Tt
                 INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DrugDeclarationId = Tt.Id
                 WHERE Td.DirectionToPayId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS DrugFormRu,
       '' AS DrugFormEn,
       '' AS ManufactureKz,
       '' AS ManufactureRu,
       '' AS ManufactureEn,
       '' AS ExpertDisplayName,
       CAST(0 AS decimal(18, 2)) AS Price,
       [TotalPrice] AS [TotalPrice],
       '' AS [Currency],
        STUFF ((SELECT DISTINCT '; ' + Tt.NameKz
                 FROM [dbo].[EXP_PriceList] Tt
                 INNER JOIN [dbo].[EXP_DirectionToPays_PriceList] AS Td ON Td.PriceListId = Tt.Id
                 WHERE Td.DirectionToPayId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS PriceListNameKz,
       STUFF ((SELECT DISTINCT '; ' + Tt.NameRu
                 FROM [dbo].[EXP_PriceList] Tt
                 INNER JOIN [dbo].[EXP_DirectionToPays_PriceList] AS Td ON Td.PriceListId = Tt.Id
                 WHERE Td.DirectionToPayId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS PriceListNameRu,
       STUFF ((SELECT DISTINCT '; ' + Tt.NameEn
                 FROM [dbo].[EXP_PriceList] Tt
                 INNER JOIN [dbo].[EXP_DirectionToPays_PriceList] AS Td ON Td.PriceListId = Tt.Id
                 WHERE Td.DirectionToPayId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS PriceListNameEn,
       '' AS PriceListValue,
       [InvoiceNumber1C],
       [InvoiceDatetime1C],
       [PaymentDatetime1C],
       [PaymentValue1C],
       [PaymentComment1C]
FROM [dbo].[EXP_DirectionToPays] AS Tdp
GROUP BY Tdp.Id,
         Tdp.[Number],
         Tdp.[DirectionDate],
         Tdp.[TotalPrice],
         Tdp.[InvoiceNumber1C],
         Tdp.[InvoiceDatetime1C],
         Tdp.[PaymentDatetime1C],
         Tdp.[PaymentValue1C],
         Tdp.[PaymentComment1C]

GO



CREATE VIEW [dbo].[EXP_DrugDeclarationDirectionToPayView]
AS
SELECT        Td.Id, Td.Number, Td.CreatedDate, Td.TypeId, Tt.NameRu AS TypeNameRu, Tt.NameKz AS TypeNameKz, Td.NameRu, Td.NameKz, Td.NameEn, Tsdf.full_name AS DrugFormRu, Tsdf.full_name_kz AS DrugFormKz,
                          Td.StatusId, s.NameRu AS StausName, Td.OwnerId, Tdd.DirectionToPayId
FROM            dbo.EXP_DrugDeclaration AS Td INNER JOIN
                         dbo.EXP_DIC_Type AS Tt ON Tt.Id = Td.TypeId INNER JOIN
                         dbo.EXP_DIC_Status AS s ON s.Id = Td.StatusId INNER JOIN
                         dbo.sr_drug_forms AS Tsdf ON Tsdf.id = Td.DrugFormId LEFT OUTER JOIN
                         dbo.EXP_DirectionToPays_DrugDeclaration AS Tdd ON Tdd.DrugDeclarationId = Td.Id

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
         Begin Table = "Td"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 284
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tt"
            Begin Extent = 
               Top = 6
               Left = 322
               Bottom = 136
               Right = 492
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "s"
            Begin Extent = 
               Top = 6
               Left = 530
               Bottom = 136
               Right = 700
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tsdf"
            Begin Extent = 
               Top = 6
               Left = 738
               Bottom = 136
               Right = 908
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tdd"
            Begin Extent = 
               Top = 6
               Left = 946
               Bottom = 102
               Right = 1131
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
         Column ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EXP_DrugDeclarationDirectionToPayView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'= 1440
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EXP_DrugDeclarationDirectionToPayView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EXP_DrugDeclarationDirectionToPayView'
GO



CREATE VIEW [dbo].[EXP_PriceListDirectionToPayView]
AS
SELECT        Tp.Id, Tp.Number, Tp.NameRu, Tp.NameKz, Tp.NameEn, Tp.PriceRegisterForeign, Tp.PriceRegisterForeignNds, Tp.PriceReRegisterForeign, Tp.PriceReRegisterForeignNds, Tp.PriceRegisterKz, 
                         Tp.PriceRegisterKzNds, Tp.PriceReRegisterKz, Tp.PriceReRegisterKzNds, Tp.Category, Tdp.DirectionToPayId
FROM            dbo.EXP_PriceList AS Tp LEFT OUTER JOIN
                         dbo.EXP_DirectionToPays_PriceList AS Tdp ON Tdp.PriceListId = Tp.Id

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
         Begin Table = "Tp"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 269
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tdp"
            Begin Extent = 
               Top = 6
               Left = 307
               Bottom = 102
               Right = 487
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EXP_PriceListDirectionToPayView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EXP_PriceListDirectionToPayView'
GO




