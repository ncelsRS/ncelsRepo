insert into Units
(Id, CreatedDate, ModifiedDate, Code, Name, NameKz, ShortName, PositionState, Type, Rank, PositionType, PositionStaff, DisplayName, ParentId)
values(NEWID(), GETDATE(), GETDATE(), N'ValidationGroup', 
N'Группа валидации регистрационного досье медицинских изделий', N'Группа валидации регистрационного досье медицинских изделий',
N'ГВРД МИ', 0, 1, 0, 0, 0, N'Группа валидации регистрационного досье медицинских изделий',
(select top 1 Id from Units where code = '00'))