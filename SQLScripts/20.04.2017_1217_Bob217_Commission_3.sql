CREATE VIEW dbo.CommissionDrugDeclarationsCountView
	AS
SELECT CD.CommissionId
	,CD.ConclusionTypeId AS ConclusionTypeId
    ,D.TypeId AS TypeId
    ,COUNT(1) AS Count
FROM dbo.CommissionDrugDeclarations AS CD
    INNER JOIN dbo.EXP_DrugDeclaration AS D ON D.Id = CD.DrugDeclarationId
WHERE CD.ConclusionTypeId IS NOT NULL            
GROUP BY CD.CommissionId, CD.ConclusionTypeId, D.TypeId