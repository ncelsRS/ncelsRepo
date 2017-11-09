ALTER VIEW dbo.CommissionDrugDeclarationsCountView
	AS
SELECT CD.CommissionId
	,CD.ConclusionTypeId AS ConclusionTypeId
    ,D.TypeId AS TypeId
    ,COUNT(1) AS Count
FROM dbo.CommissionDrugDosage AS CD
	INNER JOIN dbo.EXP_DrugDosage AS DD ON DD.Id = CD.DrugDosageId
	INNER JOIN dbo.EXP_DrugDeclaration AS D ON D.Id = DD.DrugDeclarationId
WHERE CD.ConclusionTypeId IS NOT NULL            
GROUP BY CD.CommissionId, CD.ConclusionTypeId, D.TypeId
GO

EXEC sp_rename '[dbo].[CommissionDrugDeclarationsCountView]', 'CommissionDrugDosageCountView', 'OBJECT'
GO

ALTER VIEW dbo.CommissionDrugDosageCountView
	AS
SELECT CD.CommissionId
	,CD.ConclusionTypeId AS ConclusionTypeId
    ,D.TypeId AS TypeId
    ,COUNT(1) AS Count
FROM dbo.CommissionDrugDosage AS CD
	INNER JOIN dbo.EXP_DrugDosage AS DD ON DD.Id = CD.DrugDosageId
	INNER JOIN dbo.EXP_DrugDeclaration AS D ON D.Id = DD.DrugDeclarationId
WHERE CD.ConclusionTypeId IS NOT NULL            
GROUP BY CD.CommissionId, CD.ConclusionTypeId, D.TypeId
GO