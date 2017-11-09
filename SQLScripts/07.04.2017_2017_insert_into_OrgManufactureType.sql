update Dictionaries set Code='1'
where Type='OrgManufactureType' and Name='Производитель'
go
update Dictionaries set Code='2'
where Type='OrgManufactureType' and Name='Держатель лицензии'
go
update Dictionaries set Code='3'
where Type='OrgManufactureType' and Name='Держатель регистрационного удостоверения'
go
update Dictionaries set Code='4'
where Type='OrgManufactureType' and Name='Предприятие-упаковщик'
go
update Dictionaries set Code='5'
where Type='OrgManufactureType' and Name='Заявитель или представительство'
go
update Dictionaries set Code='6'
where Type='OrgManufactureType' and Name='Уполномоченное лицо по осуществлению фармаконадзора в РК'
go
insert into Dictionaries (Type, Code, Name, NameKz, DisplayName, IsGuide)
values ('OrgManufactureType', '7', N'Выпускающий контроль', N'Выпускающий контроль', N'Выпускающий контроль', 0)
go