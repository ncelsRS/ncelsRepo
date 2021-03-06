CREATE TABLE [dbo].[EXP_PriceListDrugTypeMapping](
	[Id] [uniqueidentifier] NOT NULL,
	[DrugTypeCode] [nvarchar](50) NULL,
	[PriceListCode] [nvarchar](50) NULL,
	[PriceListMulticomponentCode] [nvarchar](50) NULL
) ON [PRIMARY]

GO
INSERT [dbo].[EXP_PriceListDrugTypeMapping] ([Id], [DrugTypeCode], [PriceListCode], [PriceListMulticomponentCode]) VALUES (N'f469f952-e152-4d6e-baf8-ec303349887d', N'1', N'3', N'1')
GO
INSERT [dbo].[EXP_PriceListDrugTypeMapping] ([Id], [DrugTypeCode], [PriceListCode], [PriceListMulticomponentCode]) VALUES (N'b64a2fdd-c908-486e-bd67-2f950592ad19', N'2', N'7', N'5')
GO
INSERT [dbo].[EXP_PriceListDrugTypeMapping] ([Id], [DrugTypeCode], [PriceListCode], [PriceListMulticomponentCode]) VALUES (N'115c19b4-4eb4-4669-81fd-cf6059a10e0b', N'3', N'12', NULL)
GO
INSERT [dbo].[EXP_PriceListDrugTypeMapping] ([Id], [DrugTypeCode], [PriceListCode], [PriceListMulticomponentCode]) VALUES (N'b3deed66-17cd-47a9-8cad-f9b312cb36db', N'4', N'10', NULL)
GO
INSERT [dbo].[EXP_PriceListDrugTypeMapping] ([Id], [DrugTypeCode], [PriceListCode], [PriceListMulticomponentCode]) VALUES (N'5a2d65ba-a237-4828-9e99-50c80dd56a17', N'5', N'11', NULL)
GO
INSERT [dbo].[EXP_PriceListDrugTypeMapping] ([Id], [DrugTypeCode], [PriceListCode], [PriceListMulticomponentCode]) VALUES (N'b79f9a66-a653-46ea-ae94-1c4b918316f3', N'6', N'4', N'2')
GO
INSERT [dbo].[EXP_PriceListDrugTypeMapping] ([Id], [DrugTypeCode], [PriceListCode], [PriceListMulticomponentCode]) VALUES (N'167ab29d-2d08-441f-8191-210ea51c74d2', N'7', N'8', N'6')
GO
INSERT [dbo].[EXP_PriceListDrugTypeMapping] ([Id], [DrugTypeCode], [PriceListCode], [PriceListMulticomponentCode]) VALUES (N'50e0be12-2950-483a-b185-b6793fa956ca', N'8', NULL, NULL)
GO
INSERT [dbo].[EXP_PriceListDrugTypeMapping] ([Id], [DrugTypeCode], [PriceListCode], [PriceListMulticomponentCode]) VALUES (N'd7c2d29c-3df9-472b-874d-429b68c86d26', N'9', NULL, NULL)
GO
INSERT [dbo].[EXP_PriceListDrugTypeMapping] ([Id], [DrugTypeCode], [PriceListCode], [PriceListMulticomponentCode]) VALUES (N'd9a6c275-c1f8-44f4-a8e9-c197014e0094', N'10', NULL, NULL)
GO
ALTER TABLE [dbo].[EXP_PriceListDrugTypeMapping] ADD  CONSTRAINT [DF_EXP_DrugTypePriceListMapping_Id]  DEFAULT (newid()) FOR [Id]
GO
