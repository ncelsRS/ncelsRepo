-- contractId
alter table OBK_AssessmentDeclaration add ContractId int null
update OBK_AssessmentDeclaration set ContractId = Contract_Id
alter table OBK_AssessmentDeclaration alter column ContractId uniqueidentifier not null
alter table OBK_AssessmentDeclaration drop constraint FK_OBK_AssessmentDeclaration_OBK_Contract
alter table OBK_AssessmentDeclaration drop column Contract_Id

alter table OBK_AssessmentDeclaration with check add 
constraint FK_OBK_AssessmentDeclaration_OBK_Contract foreign key (ContractId)
references OBK_Contract (Id)

-- typeId
alter table OBK_AssessmentDeclaration add TypeId int null
update OBK_AssessmentDeclaration set TypeId = Type_Id
alter table OBK_AssessmentDeclaration alter column TypeId int not null

alter table OBK_AssessmentDeclaration drop constraint FK_OBK_AssessmentDeclaration_OBK_Ref_Type
drop index IX_FK_OBK_Ref_Type_AssessmentDeclaration on OBK_AssessmentDeclaration  
alter table OBK_AssessmentDeclaration drop column Type_Id

alter table OBK_AssessmentDeclaration with check add 
constraint FK_OBK_AssessmentDeclaration_new_OBK_Ref_Type foreign key (TypeId)
references OBK_Ref_Type (Id)