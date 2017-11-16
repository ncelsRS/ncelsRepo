insert into Dictionaries (Id, Type, Code, Name, NameKz, DisplayName, IsGuide)
values (NEWID(), 'ExpAgreedDocType', '7', N'Макет', N'Макет', N'Макет',0)
GO
insert into Dictionaries (Id, Type, Code, Name, NameKz, DisplayName, IsGuide)
values (newid(), 'ExpActivityType', '9', N'Согласование макета', N'Согласование макета', N'Согласование макета', 0)
GO
update [EXP_DIC_Status]
set [NameRu]=N'Заключения для согласования',[NameKz]=N'Заключения для согласования'
where Id=8