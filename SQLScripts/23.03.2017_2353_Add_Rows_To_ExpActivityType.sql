insert into Dictionaries
(Type, Code, Name, DisplayName, NameKz, IsGuide, ParentId) values
(N'ExpActivityType', '4', N'������������ ����������� �� ������', N'������������ ����������� �� ������', N'������������ ����������� �� ������', 0,
(select Id from Dictionaries where Type='ExpAgreedDocType' and Code='2'))
go