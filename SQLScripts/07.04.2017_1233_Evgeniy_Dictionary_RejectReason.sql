USE [ncels]
GO

delete from Dictionaries where Type='RejectReason';
insert into Dictionaries(Code, Type, Name, DisplayName) values('01', 'RejectReason',N'Снятие с производства',N'со снятием с производства');
insert into Dictionaries(Code, Type, Name, DisplayName) values('02', 'RejectReason',N'Решение компании не осуществлять ввоз на территорию РК',N'с решением компании не осуществлять ввоз на территорию РК');
insert into Dictionaries(Code, Type, Name, DisplayName) values('03', 'RejectReason',N'Решение компании не участвовать в государственных закупках',N'с решением компании не участвовать в государственных закупках');
insert into Dictionaries(Code, Type, Name, DisplayName) values('04', 'RejectReason',N'Решение компании не продлевать государственную регистрацию/перерегистрацию лекарственного средства, изделия медицинского назначения',N'с решением компании не продлевать государственную регистрацию/перерегистрацию лекарственного средства, изделия медицинского назначения');
insert into Dictionaries(Code, Type, Name, DisplayName) values('05', 'RejectReason',N'Другая причина',N'-');

GO
