insert into Dictionaries (Code, Name, NameKz, DisplayName, Type, IsGuide)
values ('0', N'Договор', N'Договор', N'Договор', 'sysAttachContractDict', 0)
go
update Dictionaries set Code='1'
where Type='sysAttachContractDict' and Name='Доверенность от производителя'
go
update Dictionaries set Code='2'
where Type='sysAttachContractDict' and Name='Доверенность от Держателя регистрационного удостоверения'
go
update Dictionaries set Code='3'
where Type='sysAttachContractDict' and Name='Доверенность на менеджера'
go
update Dictionaries set Code='4'
where Type='sysAttachContractDict' and Name='Справка с портала e.gov о государственной регистрации'
go
update Dictionaries set Code='5'
where Type='sysAttachContractDict' and Name='Устав'