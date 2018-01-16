USE ncels
GO

INSERT INTO dbo.Dictionaries
(
  Id
 ,Code
 ,Name
 ,Type
 ,ExpireDate
 ,Year
 ,Note
 ,DepartmentsId
 ,DepartmentsValue
 ,ParentId
 ,DisplayName
 ,EmployeesValue
 ,EmployeesId
 ,NameKz
 ,IsGuide
 ,OrganizationId
 ,AdditionalInfo
)
VALUES
(
  NEWID()
 ,N''
 ,N'Доверенность на менеджера'
 ,N'sysAttachEMPContractEAESGP'
 ,N'' 
 ,N'' 
 ,N'' 
 ,N''
 ,N'' 
 ,NULL 
 ,N'Доверенность на менеджера' 
 ,N'' 
 ,N'' 
 ,N'Доверенность на менеджера' 
 ,DEFAULT 
 ,NULL 
 ,N''
),(
  NEWID()
 ,N''
 ,N'Доверенность от производителя'
 ,N'sysAttachEMPContractEAESGP'
 ,N'' 
 ,N'' 
 ,N'' 
 ,N'' 
 ,N''
 ,NULL 
 ,N'Доверенность от производителя' 
 ,N''
 ,N'' 
 ,N'Доверенность от производителя'
 ,DEFAULT
 ,NULL
 ,N''
)


,(
  NEWID()
 ,N''
 ,N'Доверенность на менеджера'
 ,N'sysAttachEMPContractEAESRG'
 ,N'' 
 ,N'' 
 ,N'' 
 ,N'' 
 ,N''
 ,NULL 
 ,N'Доверенность на менеджера' 
 ,N''
 ,N'' 
 ,N'Доверенность на менеджера'
 ,DEFAULT
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'Доверенность от производителя'
 ,N'sysAttachEMPContractEAESRG'
 ,N'' 
 ,N'' 
 ,N'' 
 ,N'' 
 ,N''
 ,NULL 
 ,N'Доверенность от производителя' 
 ,N''
 ,N'' 
 ,N'Доверенность от производителя'
 ,DEFAULT
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'Декларация о соответствии с классом риска'
 ,N'sysAttachEMPContractEAESRG'
 ,N'' 
 ,N'' 
 ,N'' 
 ,N'' 
 ,N''
 ,NULL 
 ,N'Декларация о соответствии с классом риска' 
 ,N''
 ,N'' 
 ,N'Декларация о соответствии с классом риска'
 ,DEFAULT
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'Эксплуатационный документ (инструкция по применению) от производителя с аутентичным переводом на русский язык'
 ,N'sysAttachEMPContractEAESRG'
 ,N'' 
 ,N'' 
 ,N'' 
 ,N'' 
 ,N''
 ,NULL 
 ,N'Эксплуатационный документ (инструкция по применению) от производителя с аутентичным переводом на русский язык' 
 ,N''
 ,N'' 
 ,N'Эксплуатационный документ (инструкция по применению) от производителя с аутентичным переводом на русский язык'
 ,DEFAULT
 ,NULL
 ,N''
),(
  NEWID()
 ,N''
 ,N'Проект заявления на экспертизу'
 ,N'sysAttachEMPContractEAESRG'
 ,N'' 
 ,N'' 
 ,N'' 
 ,N'' 
 ,N''
 ,NULL 
 ,N'Проект заявления на экспертизу' 
 ,N''
 ,N'' 
 ,N'Проект заявления на экспертизу'
 ,DEFAULT
 ,NULL
 ,N''
);
GO