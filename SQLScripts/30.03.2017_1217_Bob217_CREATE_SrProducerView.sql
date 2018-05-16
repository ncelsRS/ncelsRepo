CREATE VIEW dbo.SrProducerView
	AS
SELECT SrPr.id AS Id
	,SrPr.[name] AS Name
    ,SrPr.name_eng AS NameEng
    ,SrPr.name_kz AS NameKz
    ,SrFormType.id AS FormTypeId
    ,SrFormType.full_name AS FormTypeFullName
    ,SrFormType.full_name_kz AS FormTypeFullNameKz
    ,SrFormType.[name] AS FormTypeName
    ,SrFormType.name_kz AS FormTypeNameKz
FROM dbo.sr_producers AS SrPr
	LEFT JOIN dbo.sr_form_types AS SrFormType ON SrFormType.id = SrPr.form_type_id