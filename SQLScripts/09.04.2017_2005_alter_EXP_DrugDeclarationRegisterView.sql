ALTER view [dbo].[EXP_DrugDeclarationRegisterView] as
select d.Id,d.Number, d.TypeId, t.NameRu as TypeNameRu, t.NameKz as TypeNameKz, d.NameRu, d.NameKz, d.NameEn, d.FirstSendDate,
p.NameRu as ProducerRu, p.NameKz as ProducerKz, p.NameEn as ProducerEn, pk.NameRu as PackerRu, pk.NameKz as PackerKz, pk.NameEn as PackerEn,
rc.NameRu ReleaseControlRu, rc.nameKz ReleaseControlKz, rc.NameEn ReleaseControlEn,
c.Id as CountryId, c.Name as CountryRu, c.NameKz as CountryKz, d.StatusId, s.NameRu as StatusRu, s.NameKz as StatusKz, dt.DrugTypeRu, dt.DrugTypeKz,
d.MnnId, mnn.name_rus as MnnRu, mnn.name_kz as MnnKz, mnn.name_eng as MnnEn, df.name as DrugFormRu, df.name_kz as DrugFormKz,
ds.DosageRu, ds.DosageKz, convert(uniqueidentifier, null) as StageId, CONVERT(nvarchar(500), null) as StageRu, CONVERT(nvarchar(500), null) as StageKz,
convert(uniqueidentifier, null) as ExpertId, convert(nvarchar(500), null) as ExpertInitials, convert(bit, null) Suspended,
iif(dp.Id is null,0,1) as Paid, dp.PaymentDatetime1C as PaymentDate, convert(bit, null) OnBoard, convert(bit, null) ProductionEvaluation,
convert(datetime, null) StageStartDate, convert(datetime, null) StageControlDate, convert(datetime, null) ConclusionDate, convert(bit, null) StageCompleted,
convert(datetime, null) StageEndDate, convert(bit, null) StageOverdue, convert(int, null) OverdueDays, convert(uniqueidentifier, null) LetterId,
convert(nvarchar(250), null) LetterNumber, convert(int, null) StageDays, convert(bit, null) IsNewProducer,
dsub.ActiveSubstanceRu, dsub.ActiveSubstanceKz, dsub.SecondarySubstanceRu, dsub.SecondarySubstanceKz,
(SELECT        COUNT(Id) AS Expr1
                               FROM            dbo.EXP_DrugDosage AS dd
                               WHERE        (DrugDeclarationId = d.Id) AND (Id IN
                                                             (SELECT        DrugDosageId
                                                               FROM            dbo.EXP_DrugSubstance AS ds
                                                               WHERE        (IsControl = 'true' or IsPoison='true')))) AS CountDosageIsControl,
app.NameRu as ApplicantRu, app.NameKz as ApplicantKz, app.NameEn as ApplicantEn, appc.Name as ApplicantCountryRu, appc.NameKz as ApplicantCountryKz,
h.NameRu as HolderRu, h.NameKz as HolderKz, h.NameEn as HolderEn, hc.Name as HolderCountryRu, hc.NameKz as HolderCountryKz
from EXP_DrugDeclaration d
inner join EXP_DIC_Type t on t.Id=d.TypeId
inner join EXP_DIC_Status s on s.Id=d.StatusId
left join (select p.*, pt.Code ProducerTypeCode from EXP_DrugOrganizations p
inner join Dictionaries pt on pt.Id=p.OrgManufactureTypeDicId) p on p.DrugDeclarationId=d.Id and p.ProducerTypeCode='1'
left join Dictionaries c on c.Id=p.CountryDicId
left join (select p.*, pt.Code ProducerTypeCode from EXP_DrugOrganizations p
inner join Dictionaries pt on pt.Id=p.OrgManufactureTypeDicId) app on app.DrugDeclarationId=d.Id and app.ProducerTypeCode='5'
left join Dictionaries appc on appc.Id=app.CountryDicId
left join (select p.*, pt.Code ProducerTypeCode from EXP_DrugOrganizations p
inner join Dictionaries pt on pt.Id=p.OrgManufactureTypeDicId) h on h.DrugDeclarationId=d.Id and h.ProducerTypeCode='2'
left join Dictionaries hc on hc.Id=h.CountryDicId
left join (select pk.*, pt.Code ProducerTypeCode from EXP_DrugOrganizations pk
inner join Dictionaries pt on pt.Id=pk.OrgManufactureTypeDicId) pk on pk.DrugDeclarationId=d.Id and pk.ProducerTypeCode='4'
left join (select rc.*, pt.Code ProducerTypeCode from EXP_DrugOrganizations rc
inner join Dictionaries pt on pt.Id=rc.OrgManufactureTypeDicId) rc on rc.DrugDeclarationId=d.Id and rc.ProducerTypeCode='7'
left join EXP_DrugTypeView dt on dt.DrugDeclarationId=d.Id
left join sr_international_names mnn on mnn.id=d.MnnId
left join sr_dosage_forms df on df.id=d.DrugFormId
left join EXP_DrugDosageView ds on ds.DrugDeclarationId=d.Id
left join (select dp.id, dpd.DrugDeclarationId, dp.PaymentDatetime1C from EXP_DirectionToPays dp
inner join EXP_DirectionToPays_DrugDeclaration dpd on dpd.DirectionToPayId=dp.Id
inner join Dictionaries dps on dps.Id=dp.StatusId and dps.Code='6') dp on dp.DrugDeclarationId=d.Id
left join EXP_DrugSubstanceView dsub on dsub.DrugDeclarationId=d.Id

GO


