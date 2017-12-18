alter table EMP_Ref_ServiceType add ChangeType bit null
update EMP_Ref_ServiceType set ChangeType='false'
alter table EMP_Ref_ServiceType alter column ChangeType bit not null