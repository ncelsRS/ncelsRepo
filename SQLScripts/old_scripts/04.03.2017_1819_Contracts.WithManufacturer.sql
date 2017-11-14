alter table Contracts
add WithManufacturer bit
go

update Contracts set WithManufacturer=1
go

alter table Contracts
alter column WithManufacturer bit not null
go