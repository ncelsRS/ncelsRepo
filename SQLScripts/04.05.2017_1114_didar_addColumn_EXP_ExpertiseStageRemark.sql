alter table EXP_ExpertiseStageRemark
add AtthachId uniqueidentifier null
GO
INSERT INTO [dbo].[Dictionaries]
           ([Code],[Name],[NameKz],[Type],[IsGuide],[Note])
     VALUES ('REMARK', N'Замечания', N'Замечания', 'DrugDeclaration',0,'application/msword')
GO