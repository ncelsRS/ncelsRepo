USE [ncels]
GO

update Dictionaries set Code='6' where Id='0FEA7A1B-B5A9-47F8-961F-4B68CF72A907';
update Dictionaries set Name=N'Черновик', DisplayName=N'Черновик' where Id='0FEA7A1B-B5A9-47F8-961F-4B68CF72A901';
delete from Dictionaries where id='0FEA7A1B-B5A9-47F8-961F-4B68CF72A908';
insert into Dictionaries(Id, Code, Name, Type, DisplayName) values('0FEA7A1B-B5A9-47F8-961F-4B68CF72A908', '7', N'На регистрации', 'PriceProjectStatus', N'На регистрации');

GO
