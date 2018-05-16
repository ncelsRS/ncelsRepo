-- а вообще так делать нельзя ;-)

update EMP_Ref_ContractType
set Code = '1' where NameRu = N'Регистрация';

update EMP_Ref_ContractType
set Code = '2' where NameRu = N'Перерегистрация';

update EMP_Ref_ContractType
set Code = '3' where NameRu = N'Внесение изменений';