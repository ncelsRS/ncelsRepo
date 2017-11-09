ALTER TABLE [dbo].[EXP_DrugDeclaration]
ADD [RegisterId] [int] NULL
GO
ALTER TABLE [dbo].[EXP_DrugDeclaration]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugDeclaration_register] FOREIGN KEY([RegisterId])
REFERENCES [dbo].[sr_register] ([Id])
GO

ALTER TABLE [dbo].[EXP_DrugDeclaration] CHECK CONSTRAINT [FK_EXP_DrugDeclaration_register]
GO

CREATE TABLE [dbo].[EXP_DIC_ChangeType](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[ChangeName] [nvarchar](2000) NULL,
	[ChangeType] [nvarchar](2000) NULL,
	[DateCreate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateEdit] [datetime] NULL,
 CONSTRAINT [PK_EXP_DIC_ChangeType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EXP_DrugChangeType]    Script Date: 28.03.2017 13:47:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EXP_DrugChangeType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DrugDeclarationId] [uniqueidentifier] NOT NULL,
	[ChangeTypeId] [int] NOT NULL,
	[Condition] [nvarchar](2000) NULL,
 CONSTRAINT [PK_EXP_DrugChangeType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (1, N'1', N'Изменение названия производителя лекарственного средства', N'IA', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (2, N'2', N'Изменение юридического адреса производителя', N'IA', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (3, N'3', N'Изменение места (мест) производства для части или всего производственного процесса лекарственного средства', N'IA', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (4, N'4', N'Изменение держателя регистрационного удостоверения', N'IA', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (5, N'5', N'Изменение названия лекарственного средства (как торгового, так и общепринятого названия)', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (6, N'6', N'Внесение дополнительного торгового названия лекарственного средства производимого отечественными производителями для экспорта', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (7, N'7', N'Изменение названия активной субстанции', N'IA', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (8, N'8', N'Замена наполнителя на другой сравнимый наполнитель (аналогичный) (за исключением компонентов вакцин и биотехнологических наполнителей)', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (9, N'9', N'Изменение системы красителей продукта (добавление, удаление или замена красителя)', N'А) Уменьшение или изъятие: IA; Б) Увеличение, дополнение или замена одного или больше компонентов: IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (10, N'10', N'Изменение системы вкусовых добавок (добавление, удаление или замена вкусовой добавки)', N'А) уменьшение или изъятие: IA; Б) увеличение, дополнение или замена одного или больше компонентов: IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (11, N'11', N'Изменение массы (веса) покрытия таблетки или изменение массы (веса) оболочки капсулы', N'Б) лекарственные формы, стойкие к действию желудочного сока, с модифицированным высвобождением или пролонгированного действия: IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (12, N'12', N'Изменение качественного состава первичной (внутренней) упаковки', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (13, N'13', N'Удаление одного из показаний к применению', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (14, N'14', N'Удаление способа применения (введения)', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (15, N'15', N'Добавление показаний к применению в утвержденной терапевтической области', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (16, N'16', N'Добавление нового/новых побочных действий в инструкцию по медицинскому применению', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (17, N'17', N'Добавление или удаление противопоказаний в инструкцию по медицинскому применению', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (18, N'18', N'Добавление нового/новых предостережения при медицинском применении лекарственного препарата в инструкцию по медицинскому применению', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (19, N'19', N'Изменение условий отпуска из аптек', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (20, N'20', N'Внесение изменений в инструкцию по медицинскому применению общих сведений о лекарственном препарате, производителе, представительстве ', N'IА', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (21, N'21', N'Добавление или замена измерительного устройства для оральных жидких дозировочных форм и других дозировочных форм', N'IА', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (22, N'22', N'Смена организациипроизводителя (производителей) активной субстанции, добавление новой организации-производителя (производителей) активной субстанции', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (23, N'23', N'Изменение в имени организации-производителя активной субстанции', N'IА', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (24, N'24', N'Смена поставщика промежуточной смеси, используемой в производстве активной субстанции', N'IА', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (25, N'25', N'Незначительные изменения в процессе производства активной субстанции', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (26, N'26', N'Изменение размера производственной партии активной субстанции', N'А) до 10 раз в сравнении с оригинальным размером серии, утвержденным при регистрации, уменьшение обьема производства - IБ; Б) Свыше 10 раз в сравнении с оригинальным размером серии, утвержденном при государственной регистрации IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (27, N'27', N'Изменение нормативного документа по контролю за качеством и безопасностью активной субстанции', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (28, N'28', N'Незначительные изменения в производстве лекарственного средства', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (29, N'29', N'Изменение в контроле за незавершенным производством', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (30, N'30', N'Изменение размера производственной партии (серии) готового продукта', N'А) до 10 раз в сравнении с первичным размером серии, утвержденным при регистрации: IБ; Б)уменьшение до 10 раз: IБ; В) другие случаи: IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (31, N'31', N'Изменение нормативного документа по контролю за качеством и безопасностью лекарственного средства', N'А) сужение допустимых границ: IБ; Б) дополнение нового показателя: IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (32, N'32', N'Изменение синтеза или утилизации наполнителей, не указанных в Фармакопее', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (33, N'33', N'Изменение в спецификации наполнителей лекарственного средства (исключая компоненты вакцин)', N'А) сужение допустимых границ: IБ; Б) дополнение нового показателя: IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (34, N'34', N'Изменение срока хранения по отношению к указанному при регистрации', N'А) Уменьшение срока хранения – IА; Б) Увеличение срока хранения – IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (35, N'35', N'Изменение срока хранения после первого открытия упаковки', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (36, N'36', N'Изменение срока хранения после воспроизведения лекарственного средства', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (37, N'37', N'Изменение условий хранения', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (38, N'38', N'Увеличение срока хранения или периода повторного тестирования активной субстанции', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (39, N'39', N'Изменение в процедуре тестирования активной субстанции', N'А) незначительные изменения в утвержденных методах испытаний: IА; Б) другие изменения в методах испытаний, включая замену или дополнения метода испытаний: IА ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (40, N'40', N'Изменение в процедуре тестирования начального и промежуточного материала, используемого в производстве активной субстанции', N'А) незначительные изменения в утвержденных методах испытаний: IА; Б) другие изменения в методах испытаний, включая замену или дополнения метода испытаний: IА', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (41, N'41', N'Изменение (метода анализа) в процедуре контроля качества лекарственного средства', N'А) незначительные изменения в утвержденных методах испытаний: IБ; Б) незначительное изменение утвержденного метода испытаний для биологического активного вещества или вспомогательных веществ биологического происхождения: IБ; В) другие изменения в методах испытаний, включая замену или дополнения метода испытаний: IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (42, N'42', N'Изменени которое соответствует дополнениям к Фармакопее. (Если торговая лицензия относится к текущему изданию Фармакопеи и изменение представлено в течение 6 месяцев после принятия обновленной монографии, уведомления не требуется.)', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (43, N'43', N'Изменение в процедурах тестирования нефармакопейных наполнителей', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (44, N'44', N'Изменение в процедуре тестирования внутренней упаковки', N'А) незначительные изменения в утвержденных методах испытаний: IА; Б) другие изменения в методах испытаний, включая замену или дополнения метода испытаний: IА', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (45, N'45', N'Изменение в процедуре тестирования устройства (оборудования) для применения лекарственных средств', N'IА', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (46, N'46', N'Изменение размера упаковки лекарственного средства', N'Изменение количества единиц в упаковке: А) изменения в рамках утвержденного размера упаковки: IБ; Б) изменение вне утвержденного размера упаковки: IБ; В) изменение массы/объема вспомогательных веществ для парентеральных многодозированных препаратов: IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (47, N'47', N'Изменение в форме упаковки лекарственного средства', N'IА', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (48, N'48', N'Изменение оттисков, грунтовки или других маркировок (за исключением штампов на таблетках и надписей на капсулах, включая добавление или изменение краски, используемой для маркировки)', N'IА', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (49, N'49', N'Изменение размеров таблеток, капсул, суппозиториев или пессариев без изменения количественного состава и средней массы', N'А) лекарственные формы, стойкие к действию желудочного сока, модифицированного или пролонгированного высвобождения, и делимые таблетки: IБ; Б) все другие таблетки, капсулы суппозитории и пессарии: IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
INSERT [dbo].[EXP_DIC_ChangeType] ([Id], [Code], [ChangeName], [ChangeType], [DateCreate], [IsDeleted], [DateEdit]) VALUES (50, N'50', N'Изменение в производственном процессе для компонентов, требующих процедуру тестирования на новые примеси', N'IБ', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
ALTER TABLE [dbo].[EXP_DrugChangeType]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugChangeType_EXP_DIC_ChangeType] FOREIGN KEY([ChangeTypeId])
REFERENCES [dbo].[EXP_DIC_ChangeType] ([Id])
GO
ALTER TABLE [dbo].[EXP_DrugChangeType] CHECK CONSTRAINT [FK_EXP_DrugChangeType_EXP_DIC_ChangeType]
GO
ALTER TABLE [dbo].[EXP_DrugChangeType]  WITH CHECK ADD  CONSTRAINT [FK_EXP_DrugChangeType_EXP_DrugDeclaration] FOREIGN KEY([DrugDeclarationId])
REFERENCES [dbo].[EXP_DrugDeclaration] ([Id])
GO
ALTER TABLE [dbo].[EXP_DrugChangeType] CHECK CONSTRAINT [FK_EXP_DrugChangeType_EXP_DrugDeclaration]
GO
