ALTER TABLE [dbo].[EXP_ExpertiseSafetyreportFinalDoc]
ADD [IsAccepted] [bit] NULL
GO
ALTER TABLE [dbo].[EXP_ExpertiseSafetyreportFinalDoc]
ADD [Remark] [nvarchar](500) NULL
GO
ALTER TABLE [dbo].[EXP_ExpertiseSafetyreportFinalDoc]
ADD [IsAcceptedKz] [bit] NULL
GO
ALTER TABLE [dbo].[EXP_ExpertiseSafetyreportFinalDoc]
ADD [RemarkKz] [nvarchar](500) NULL
GO
INSERT [dbo].[EXP_DIC_Status] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (11, N'11', N'Заключения приняты', N'Заключения приняты', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_Status] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (12, N'12', N'Заключения на доработку', N'Заключения на доработку', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
