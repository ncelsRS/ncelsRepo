create view EXP_DrugSubstanceView as
select dd.DrugDeclarationId,
STUFF((SELECT DISTINCT '; ' + sb.name + ' ' + ds.SubstanceCount + ' ' + sm.name
          FROM EXP_DrugDosage cd
		  inner join EXP_DrugSubstance ds on ds.DrugDosageId=cd.Id
		  inner join sr_substances sb on sb.id=ds.SubstanceId
		  left join sr_measures sm on sm.id=ds.MeasureId
          WHERE cd.DrugDeclarationId=dd.DrugDeclarationId and ds.SubstanceTypeId=1
          FOR XML PATH (''))
          , 1, 2, N'Активный состав: ')  AS ActiveSubstanceRu,
STUFF((SELECT DISTINCT '; ' + sb.name_kz + ' ' + ds.SubstanceCount + ' ' + sm.name_kz
          FROM EXP_DrugDosage cd
		  inner join EXP_DrugSubstance ds on ds.DrugDosageId=cd.Id
		  inner join sr_substances sb on sb.id=ds.SubstanceId
		  left join sr_measures sm on sm.id=ds.MeasureId
          WHERE cd.DrugDeclarationId=dd.DrugDeclarationId and ds.SubstanceTypeId=1
          FOR XML PATH (''))
          , 1, 2, N'Активный состав: ')  AS ActiveSubstanceKz,
STUFF((SELECT DISTINCT '; ' + sb.name + ' ' + ds.SubstanceCount + ' ' + sm.name
          FROM EXP_DrugDosage cd
		  inner join EXP_DrugSubstance ds on ds.DrugDosageId=cd.Id
		  inner join sr_substances sb on sb.id=ds.SubstanceId
		  left join sr_measures sm on sm.id=ds.MeasureId
          WHERE cd.DrugDeclarationId=dd.DrugDeclarationId and ds.SubstanceTypeId=2
          FOR XML PATH (''))
          , 1, 2, N'Вспомогательный состав: ')  AS SecondarySubstanceRu,
STUFF((SELECT DISTINCT '; ' + sb.name_kz + ' ' + ds.SubstanceCount + ' ' + sm.name_kz
          FROM EXP_DrugDosage cd
		  inner join EXP_DrugSubstance ds on ds.DrugDosageId=cd.Id
		  inner join sr_substances sb on sb.id=ds.SubstanceId
		  left join sr_measures sm on sm.id=ds.MeasureId
          WHERE cd.DrugDeclarationId=dd.DrugDeclarationId and ds.SubstanceTypeId=2
          FOR XML PATH (''))
          , 1, 2, N'Вспомогательный состав: ')  AS SecondarySubstanceKz
		  from EXP_DrugDosage dd
group by dd.DrugDeclarationId
GO


