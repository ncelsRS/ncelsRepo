CREATE VIEW OBK_ContractView
AS
SELECT
	OBK_Contract.Id AS 'Id',
	CASE WHEN OBK_Contract.ParentId IS NOT NULL AND OBK_Contract.Number <> N'б/н' THEN (SELECT TOP 1 X.Number FROM OBK_Contract X WHERE X.Id = OBK_Contract.ParentId) + '-' + OBK_Contract.Number
													ELSE OBK_Contract.Number END
													AS 'Number',
	OBK_Contract.CreatedDate AS 'CreatedDate',
	OBK_Ref_Status.NameRu AS 'StatusNameRu',
	OBK_Declarant.NameRu AS 'DeclarantNameRu',
	OBK_Ref_Type.NameRu AS 'Type',
	OBK_Ref_Type.NameKz AS 'TypeKz',
	OBK_Contract.StartDate AS 'StartDate',
	OBK_Contract.EndDate AS 'EndDate',
	OBK_Contract.ParentId AS 'ParentId',
	OBK_Contract.EmployeeId AS 'EmployeeId',
	CASE WHEN OBK_Contract.ParentId IS NULL THEN N'Договор'
			ELSE N'Доп. соглашение' END
	AS 'DocType'
FROM OBK_Contract
LEFT JOIN OBK_Ref_Status ON OBK_Ref_Status.Id = OBK_Contract.Status
LEFT JOIN OBK_Ref_Type ON OBK_Ref_Type.Id = OBK_Contract.Type
LEFT JOIN OBK_Declarant ON OBK_Declarant.Id = OBK_Contract.DeclarantId