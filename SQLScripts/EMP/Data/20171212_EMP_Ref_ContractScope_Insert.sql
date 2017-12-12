insert into EMP_Ref_ContractScope(Id, Code, NameRu, NameKz, CreateDate, IsDeleted)
values(NEWID(), N'national', N'Экспертиза ИМН и МТ по процедуре РК', N'Экспертиза ИМН и МТ по процедуре РК', GETDATE(), 0);

insert into EMP_Ref_ContractScope(Id, Code, NameRu, NameKz, CreateDate, IsDeleted)
values(NEWID(), N'eaesgp', N'Экспертиза ИМН и МТ в рамках ЕАЭС (ГП)', N'Экспертиза ИМН и МТ в рамках ЕАЭС (ГП)', GETDATE(), 0);

insert into EMP_Ref_ContractScope(Id, Code, NameRu, NameKz, CreateDate, IsDeleted)
values(NEWID(), N'eaesrg', N'Экспертиза ИМН и МТ в рамках ЕАЭС (РГ)', N'Экспертиза ИМН и МТ в рамках ЕАЭС (РГ)', GETDATE(), 0);

go

update EMP_Contract
set ContractScopeId = (select top 1 Id from EMP_Ref_ContractScope where Code = 'national');