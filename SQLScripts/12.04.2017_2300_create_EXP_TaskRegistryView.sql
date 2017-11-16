create view EXP_TaskRegistryView as
select t.Id, t.ActivityId, t.CreatedDate, a.DocumentId, a.DocNumber, a.DocDate
, t.StatusId, st.Name StatusRu, st.NameKz StatusKz, st.Code StatusCode
, t.TypeId, tt.Code TaskTypeCode
, a.DocumentTypeId, dt.Name DocumentTypeRu, dt.NameKz DocumentTypeKz, dt.Code DocumentTypeCode
, t.AuthorId, coalesce(ath.ShortName, ath.FullName) AuthorName,
t.ExecutorId
, STUFF((SELECT DISTINCT '; ' + coalesce(ex.ShortName, ex.FullName)
          FROM EXP_Tasks ct
		  inner join Employees ex on ex.Id=ct.ExecutorId
		  inner join Dictionaries ctt on ctt.Id=ct.TypeId
          WHERE ct.ActivityId=a.Id and ctt.Code='1'
          FOR XML PATH (''))
          , 1, 2, '')  AS ApproverNames
, STUFF((SELECT DISTINCT '; ' + coalesce(ex.ShortName, ex.FullName)
          FROM EXP_Tasks ct
		  inner join Employees ex on ex.Id=ct.ExecutorId
		  inner join Dictionaries ctt on ctt.Id=ct.TypeId
          WHERE ct.ActivityId=a.Id and ctt.Code='2'
          FOR XML PATH (''))
          , 1, 2, '')  AS SignerNames
from EXP_Tasks t
inner join EXP_Activities a on a.Id=t.ActivityId
inner join Dictionaries st on st.Id=t.StatusId
inner join Dictionaries tt on tt.Id=t.TypeId
inner join Dictionaries dt on dt.Id=a.DocumentTypeId
inner join Employees ath on ath.Id=t.AuthorId