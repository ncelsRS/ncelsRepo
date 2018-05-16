create view EXP_ExpertiseStageDosageCommissionView as
select c.Id, c.FullNumber, c.Date, c.IsComplete, ct.Name as CommisionType,
 cnt.Name as ConclusionName, cd.ConclusionComment, cd.StageId, cd.DrugDosageId from Commissions c
inner join CommissionTypes ct on ct.Id=c.TypeId
inner join CommissionDrugDosage cd on cd.CommissionId=c.Id
left join CommissionConclusionTypes cnt on cnt.Id=cd.ConclusionTypeId