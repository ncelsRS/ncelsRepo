alter table ContractComments
add Sended bit;
go
update ContractComments set Sended=0
go

alter table ContractComments
alter column Sended bit not null
