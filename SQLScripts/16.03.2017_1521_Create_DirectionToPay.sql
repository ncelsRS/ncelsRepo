SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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






CREATE TABLE [dbo].[EXP_DirectionToPays_DrugDeclaration](
	[DirectionToPayId] [uniqueidentifier] NOT NULL,
	[DrugDeclarationId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EXP_DirectionToPays_DrugDeclaration_1] PRIMARY KEY CLUSTERED 
(
	[DirectionToPayId] ASC,
	[DrugDeclarationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_DirectionToPays_DrugDeclaration]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DirectionToPays_DrugDeclaration_EXP_DirectionToPays] FOREIGN KEY([DirectionToPayId])
REFERENCES [dbo].[EXP_DirectionToPays] ([Id])
GO

ALTER TABLE [dbo].[EXP_DirectionToPays_DrugDeclaration] CHECK CONSTRAINT [FK_EXP_DirectionToPays_DrugDeclaration_EXP_DirectionToPays]
GO

ALTER TABLE [dbo].[EXP_DirectionToPays_DrugDeclaration]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DirectionToPays_DrugDeclaration_EXP_DrugDeclaration] FOREIGN KEY([DrugDeclarationId])
REFERENCES [dbo].[EXP_DrugDeclaration] ([Id])
GO

ALTER TABLE [dbo].[EXP_DirectionToPays_DrugDeclaration] CHECK CONSTRAINT [FK_EXP_DirectionToPays_DrugDeclaration_EXP_DrugDeclaration]
GO






CREATE TABLE [dbo].[EXP_PriceList](
	[Id] [uniqueidentifier] NOT NULL,
	[Number] [nvarchar](50) NULL,
	[NameRu] [nvarchar](450) NULL,
	[NameKz] [nvarchar](450) NULL,
	[NameEn] [nvarchar](450) NULL,
	[PriceRegisterForeign] [decimal](18, 2) NULL,
	[PriceRegisterForeignNds] [decimal](18, 2) NULL,
	[PriceReRegisterForeign] [decimal](18, 2) NULL,
	[PriceReRegisterForeignNds] [decimal](18, 2) NULL,
	[PriceRegisterKz] [decimal](18, 2) NULL,
	[PriceRegisterKzNds] [decimal](18, 2) NULL,
	[PriceReRegisterKz] [decimal](18, 2) NULL,
	[PriceReRegisterKzNds] [decimal](18, 2) NULL,
	[Category] [nvarchar](100) NULL,
 CONSTRAINT [PK_EXP_PriceList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_PriceList] ADD  CONSTRAINT [DF_EXP_PriceList_Id]  DEFAULT (newid()) FOR [Id]
GO






CREATE TABLE [dbo].[EXP_DirectionToPays_PriceList](
	[DirectionToPayId] [uniqueidentifier] NOT NULL,
	[PriceListId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EXP_DirectionToPays_PriceList] PRIMARY KEY CLUSTERED 
(
	[DirectionToPayId] ASC,
	[PriceListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EXP_DirectionToPays_PriceList]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DirectionToPays_PriceList_EXP_DirectionToPays] FOREIGN KEY([DirectionToPayId])
REFERENCES [dbo].[EXP_DirectionToPays] ([Id])
GO

ALTER TABLE [dbo].[EXP_DirectionToPays_PriceList] CHECK CONSTRAINT [FK_EXP_DirectionToPays_PriceList_EXP_DirectionToPays]
GO

ALTER TABLE [dbo].[EXP_DirectionToPays_PriceList]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DirectionToPays_PriceList_EXP_PriceList] FOREIGN KEY([PriceListId])
REFERENCES [dbo].[EXP_PriceList] ([Id])
GO

ALTER TABLE [dbo].[EXP_DirectionToPays_PriceList] CHECK CONSTRAINT [FK_EXP_DirectionToPays_PriceList_EXP_PriceList]
GO



CREATE VIEW [dbo].[EXP_DirectionToPaysView]
AS
SELECT Tdp.[Id],
       Tdp.[Number],
       Tdp.[DirectionDate] AS [DirectionDate],
       STUFF (
                (SELECT '; ' + Tt.Number
                 FROM [dbo].[EXP_DrugDeclaration] Tt
                 INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DirectionToPayId = Tt.Id
                 WHERE Td.DrugDeclarationId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS ApplicationNumbers,
       STUFF (
                (SELECT '; ' + CONVERT(NVARCHAR(12), Tt.CreatedDate, 104)
                 FROM [dbo].[EXP_DrugDeclaration] Tt
                 INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DirectionToPayId = Tt.Id
                 WHERE Td.DrugDeclarationId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS ApplicationDates,
       STUFF (
                (SELECT '; ' + Tdt.NameRu
                 FROM [dbo].[EXP_DrugDeclaration] Tt
                 INNER JOIN [dbo].[EXP_DIC_Type] AS Tdt ON Tdt.Id = Tt.TypeId
                 INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DirectionToPayId = Tt.Id
                 WHERE Td.DrugDeclarationId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS RegistrationForm,
       STUFF (
                (SELECT '; ' + Tt.NameKz
                 FROM [dbo].[EXP_DrugDeclaration] Tt
                 INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DirectionToPayId = Tt.Id
                 WHERE Td.DrugDeclarationId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS TradeNameKz,
       STUFF (
                (SELECT '; ' + Tt.NameRu
                 FROM [dbo].[EXP_DrugDeclaration] Tt
                 INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DirectionToPayId = Tt.Id
                 WHERE Td.DrugDeclarationId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS TradeNameRu,
       STUFF (
                (SELECT '; ' + Tt.NameEn
                 FROM [dbo].[EXP_DrugDeclaration] Tt
                 INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DirectionToPayId = Tt.Id
                 WHERE Td.DrugDeclarationId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS TradeNameEn,
       STUFF (
                (SELECT '; ' + Tt.DrugFormKz
                 FROM [dbo].[EXP_DrugDeclaration] Tt
                 INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DirectionToPayId = Tt.Id
                 WHERE Td.DrugDeclarationId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS DrugFormKz,
       STUFF (
                (SELECT '; ' + Tt.DrugFormRu
                 FROM [dbo].[EXP_DrugDeclaration] Tt
                 INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Td ON Td.DirectionToPayId = Tt.Id
                 WHERE Td.DrugDeclarationId = Tdp.Id
                   FOR XML PATH('')), 1, 2, '') AS DrugFormRu,
       '' AS DrugFormEn,
       '' AS ManufactureKz,
       '' AS ManufactureRu,
       '' AS ManufactureEn,
       '' AS ExpertDisplayName,
       CAST(0 AS decimal(18, 2)) AS Price,
       [TotalPrice] AS [TotalPrice],
       '' AS [Currency],
       '' AS PriceListNameKz,
       '' AS PriceListNameRu,
       '' AS PriceListNameEn,
       '' AS PriceListValue,
       [InvoiceNumber1C],
       [InvoiceDatetime1C],
	   [PaymentDatetime1C],
	   [PaymentValue1C]
FROM [dbo].[EXP_DirectionToPays] AS Tdp
GROUP BY Tdp.Id,
         Tdp.[Number],
         Tdp.[DirectionDate],
         Tdp.[TotalPrice],
         Tdp.[InvoiceNumber1C],
         Tdp.[InvoiceDatetime1C],
		 Tdp.[PaymentDatetime1C],
		 Tdp.[PaymentValue1C]

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
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 25
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EXP_DirectionToPaysView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EXP_DirectionToPaysView'
GO


