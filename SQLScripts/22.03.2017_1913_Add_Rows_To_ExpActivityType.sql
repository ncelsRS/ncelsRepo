insert into Dictionaries
(Type, Code, Name, DisplayName, NameKz, IsGuide, ParentId) values
(N'ExpActivityType', '1', N'������������ �������� ������������� ���', N'������������ �������� ������������� ���', N'������������ �������� ������������� ���', 0,
(select Id from Dictionaries where Type='ExpAgreedDocType' and Code='1'))
go
insert into Dictionaries
(Type, Code, Name, DisplayName, NameKz, IsGuide, ParentId) values
(N'ExpActivityType', '2', N'���������� �������� ���������� � ����������', N'���������� �������� ���������� � ����������', N'���������� �������� ���������� � ����������', 0,
(select Id from Dictionaries where Type='ExpAgreedDocType' and Code='1'))
go
insert into Dictionaries
(Type, Code, Name, DisplayName, NameKz, IsGuide, ParentId) values
(N'ExpActivityType', '3', N'���������� �������� ��� ���������', N'���������� �������� ��� ���������', N'���������� �������� ��� ���������', 0,
(select Id from Dictionaries where Type='ExpAgreedDocType' and Code='1'))
go
