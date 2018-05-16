select
	Login,
	StageName,
	ExpIsNotComplete,
	ExpIsComplete,
	ContractIsNotComplete,
	ContractIsComplete,
	VisitIsNotComplete,
	VisitIsComplete
from
	(select
		e.Login,
		dic_stage.Code,
		dic_stage.NameRu as StageName,
		sum(case when stage.StatusId = 4 then 0 else 1 end) as ExpIsNotComplete,
		sum(case when stage.StatusId = 4 then 1 else 0 end) as ExpIsComplete,
		null as ContractIsNotComplete,
		null as ContractIsComplete,
		null as VisitIsNotComplete,
		null as VisitIsComplete
	from 
		EXP_DrugDeclaration as dd
		inner join EXP_ExpertiseStage as stage on stage.DeclarationId=dd.Id
		left join EXP_ExpertiseStageExecutors as ese on ese.ExpertiseStageId = stage.Id
		left join Employees as e on ese.ExecutorId = e.Id
		left join EXP_DIC_Stage as dic_stage on stage.StageId = dic_stage.Id
		left join Units as u on e.PositionId=u.Id
	where
		dd.IsDeleted = 0
		and stage.IsHistory = 0
		and e.Login is not null
	group by
		e.Login,
		dic_stage.Code,
		dic_stage.NameRu

	union

	select 
		Login,
		'90' as Code,
		'ЦОЗ(Договоры)' StageName,
		null as ExpIsNotComplete,
		null as ExpIsComplete,
		sum(ContractIsNotComplete) as ContractIsNotComplete,
		sum(ContractIsComplete) as ContractIsComplete,
		null as VisitIsNotComplete,
		null as VisitIsComplete
	from
		(select
			case when d.Code = 3 then signer_e.Login else e.Login end as Login,
			case when d.Code <> 5 and d.Code <> 6 then 1 else 0 end as ContractIsNotComplete,
			case when d.Code = 5 then 1 else 0 end as ContractIsComplete
		from 
			Contracts as c
			LEFT JOIN Documents AS doc ON doc.Id = c.Id
			LEFT JOIN Employees as e ON doc.ExecutorsId=e.Id
			LEFT JOIN Employees as signer_e ON c.SignerId=signer_e.Id
			LEFT JOIN Dictionaries as d ON c.StatusId=d.Id
		where
			doc.ExecutorsId is not null 
			and (d.Code <> 3 or (d.Code = 3 and signer_e.Login is not null))
			and c.StatusId is not null
			and c.ContractId is null
		) as t1
	group by
		Login

	union 

	select 
		e.Login,
		'9' as Code,
		'ОНЛАЙН' StageName,
		null as ExpIsNotComplete,
		null as ExpIsComplete,
		null as ContractIsNotComplete,
		null as ContractIsComplete,
		count(*) as VisitIsNotComplete,
		sum(case when v.VisitStatusId = 4 then 1 else 0 end) as VisitIsComplete
	from 
		Visits as v
		left join Employees as e on v.EmployeeId=e.Id
	group by
		e.Login) as t

order by Login, Code