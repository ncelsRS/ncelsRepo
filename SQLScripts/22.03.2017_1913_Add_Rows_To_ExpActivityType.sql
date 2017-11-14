insert into Dictionaries
(Type, Code, Name, DisplayName, NameKz, IsGuide, ParentId) values
(N'ExpActivityType', '1', N'Согласование договора руководителем ЦОЗ', N'Согласование договора руководителем ЦОЗ', N'Согласование договора руководителем ЦОЗ', 0,
(select Id from Dictionaries where Type='ExpAgreedDocType' and Code='1'))
go
insert into Dictionaries
(Type, Code, Name, DisplayName, NameKz, IsGuide, ParentId) values
(N'ExpActivityType', '2', N'Подписание договора заявителем и директором', N'Подписание договора заявителем и директором', N'Подписание договора заявителем и директором', 0,
(select Id from Dictionaries where Type='ExpAgreedDocType' and Code='1'))
go
insert into Dictionaries
(Type, Code, Name, DisplayName, NameKz, IsGuide, ParentId) values
(N'ExpActivityType', '3', N'Подписание договора без заявителя', N'Подписание договора без заявителя', N'Подписание договора без заявителя', 0,
(select Id from Dictionaries where Type='ExpAgreedDocType' and Code='1'))
go
