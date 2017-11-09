ALTER TABLE EXP_DrugPrimaryRemark
ADD [IsReadOnly] [bit] NOT NULL default(0)
GO
ALTER TABLE EXP_DrugPrimaryRemark
ADD [CorespondenceId] [nvarchar](50)
GO
ALTER TABLE EXP_DrugCorespondence
ADD [IsDeleted] [bit] NOT NULL default(0)
GO
ALTER TABLE EXP_DrugCorespondence
ADD [IsReadOnly] [bit] NOT NULL default(0)
GO
insert into Dictionaries (Id, Type, Code, Name, NameKz, DisplayName, IsGuide)
values(NEWID(), 'SEND', 'SEND', N'Отправлено', N'Отправлено', N'Отправлено', 0)
GO
INSERT [dbo].[EXP_DIC_Status] ([Id], [Code], [NameRu], [NameKz], [DateCreate], [IsDeleted], [DateEdit]) VALUES (6, N'6', N'Возвращено с экспертизы', N'Возвращено с экспертизы', CAST(N'2017-01-01T00:00:00.000' AS DateTime), 0, NULL)
GO


ALTER VIEW [dbo].[EXP_DrugDeclarationView]
AS
SELECT        TOP (100) PERCENT d.Id, d.TypeId, t.NameRu AS TypeName, d.Number, d.CreatedDate, d.StatusId, s.NameRu AS StausName, t.NameRu, d.OwnerId, d.SendDate, d.ExecuterId, e.DisplayName AS ExecuterName,
                             (SELECT        COUNT(Id) AS Expr1
                               FROM            dbo.EXP_DrugDosage AS dd
                               WHERE        (DrugDeclarationId = d.Id) AND (Id IN
                                                             (SELECT        DrugDosageId
                                                               FROM            dbo.EXP_DrugSubstance AS ds
                                                               WHERE        (IsControl = 'true') OR
                                                                                         (IsPoison = 'true')))) AS CountDosageIsControl, (CASE WHEN DesignDate IS NOT NULL THEN DesignDate ELSE d .CreatedDate END) AS SortDate
FROM            dbo.EXP_DrugDeclaration AS d INNER JOIN
                         dbo.EXP_DIC_Type AS t ON t.Id = d.TypeId INNER JOIN
                         dbo.EXP_DIC_Status AS s ON s.Id = d.StatusId LEFT OUTER JOIN
                         dbo.Employees AS e ON e.Id = d.ExecuterId
WHERE        (d.IsDeleted = 'false')

GO
