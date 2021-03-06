create view [dbo].[EXP_DrugTypeView] as
select dt.DrugDeclarationId,
STUFF((SELECT DISTINCT '; ' + ddt.NameRu
          FROM EXP_DrugType cdt
		  left join EXP_DIC_DrugType ddt on ddt.Id=cdt.DrugTypeId
          WHERE cdt.DrugDeclarationId=dt.DrugDeclarationId
          FOR XML PATH (''))
          , 1, 2, '')  AS DrugTypeRu,
STUFF((SELECT DISTINCT '; ' + ddt.NameKz
          FROM EXP_DrugType cdt
		  left join EXP_DIC_DrugType ddt on ddt.Id=cdt.DrugTypeId
          WHERE cdt.DrugDeclarationId=dt.DrugDeclarationId
          FOR XML PATH (''))
          , 1, 2, '')  AS DrugTypeKz
		  from EXP_DrugType dt
group by dt.DrugDeclarationId
GO