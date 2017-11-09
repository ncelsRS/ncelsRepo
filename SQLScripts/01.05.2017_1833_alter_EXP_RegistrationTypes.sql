alter table EXP_RegistrationTypes
add Code nvarchar(50);
go

update EXP_RegistrationTypes set Code='1' where Name='Регистрация ЛС'
go
update EXP_RegistrationTypes set Code='2' where Name='Перегистрация ЛС'
go
update EXP_RegistrationTypes set Code='3' where Name='Внесение изменений ЛС'
go

alter table EXP_RegistrationTypes
alter column Code nvarchar(50) not null;