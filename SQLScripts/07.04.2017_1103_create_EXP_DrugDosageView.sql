create view EXP_DrugDosageView as
select DrugDeclarationId,
STUFF((SELECT DISTINCT '; ' + CONVERT(nvarchar(50), cd.Dosage) + ' ' + dm.short_name + ' | ' + cd.ConcentrationRu
          FROM EXP_DrugDosage cd
		  left join sr_measures dm on dm.id=cd.DosageMeasureTypeId
          WHERE cd.DrugDeclarationId=ds.DrugDeclarationId
          FOR XML PATH (''))
          , 1, 2, '')  AS DosageRu
,STUFF((SELECT DISTINCT '; ' + CONVERT(nvarchar(50), cd.Dosage) + ' ' + dm.short_name_kz + ' | ' + cd.ConcentrationKz
          FROM EXP_DrugDosage cd
		  left join sr_measures dm on dm.id=cd.DosageMeasureTypeId
          WHERE cd.DrugDeclarationId=ds.DrugDeclarationId
          FOR XML PATH (''))
          , 1, 2, '')  AS DosageKz
		  from EXP_DrugDosage ds
group by DrugDeclarationId