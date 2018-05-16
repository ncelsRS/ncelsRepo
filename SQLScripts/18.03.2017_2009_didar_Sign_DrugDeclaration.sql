ALTER TABLE [dbo].[EXP_DrugDeclaration]
ADD [DesignDate]  [datetime] NULL
GO
ALTER TABLE [dbo].[EXP_DrugDeclaration]
ADD [DesignNote] [nvarchar](2000) NULL
GO
ALTER TABLE [dbo].[EXP_DrugDeclaration]
ADD IsSigned  bit NOT NULL DEFAULT('false')
GO
ALTER TABLE [dbo].[EXP_DrugDeclaration]
ADD [FirstSendDate]  [datetime] NULL
GO
ALTER TABLE [dbo].[EXP_DrugDeclaration]
ADD [SendDate]  [datetime] NULL
GO
ALTER TABLE [dbo].[EXP_DrugDeclaration]
ADD IsDeleted  bit NOT NULL DEFAULT('false')
GO
ALTER TABLE [dbo].[EXP_DrugDeclaration]
ADD [ExecuterId] [uniqueidentifier] 
GO

ALTER VIEW [dbo].[EXP_DrugDeclarationView]
AS
SELECT        d.Id, d.TypeId as TypeId, t.NameRu AS TypeName, d.Number, d.CreatedDate,d.StatusId as StatusId, s.NameRu AS StausName, t.NameRu, d.OwnerId, d.SendDate
FROM            dbo.EXP_DrugDeclaration AS d INNER JOIN
                         dbo.EXP_DIC_Type AS t ON t.Id = d.TypeId INNER JOIN
                         dbo.EXP_DIC_Status AS s ON s.Id = d.StatusId

						 where d.IsDeleted='false'

GO


