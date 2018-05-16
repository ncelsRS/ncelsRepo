USE [ncels]
GO

delete from EXP_DIC_ManufactureType;

insert into EXP_DIC_ManufactureType(Id,Code,NameRu,DateCreate,IsDeleted,DateEdit)
values(1,'01',N'Полностью на данном производстве',CURRENT_TIMESTAMP,0,CURRENT_TIMESTAMP);

insert into EXP_DIC_ManufactureType(Id,Code,NameRu,DateCreate,IsDeleted,DateEdit)
values(2,'02',N'Частично на данном производстве',CURRENT_TIMESTAMP,0,CURRENT_TIMESTAMP);

insert into EXP_DIC_ManufactureType(Id,Code,NameRu,DateCreate,IsDeleted,DateEdit)
values(3,'03',N'Полностью на другом производстве',CURRENT_TIMESTAMP,0,CURRENT_TIMESTAMP);

GO
