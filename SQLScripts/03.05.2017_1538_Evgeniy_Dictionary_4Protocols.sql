USE [ncels]
GO

delete from Dictionaries where Type='PpProtocolType';
insert into Dictionaries(Code, Type, Name, DisplayName) values('1', 'PpProtocolType',N'Протокол 1',N'Протокол 1');

delete from Dictionaries where Type='PpProtocolStatus';
insert into Dictionaries(Code, Type, Name, DisplayName) values('0', 'PpProtocolStatus',N'Черновик',N'Черновик');
insert into Dictionaries(Code, Type, Name, DisplayName) values('1', 'PpProtocolStatus',N'В работе',N'В работе');
insert into Dictionaries(Code, Type, Name, DisplayName) values('2', 'PpProtocolStatus',N'Завершено',N'Завершено');

GO