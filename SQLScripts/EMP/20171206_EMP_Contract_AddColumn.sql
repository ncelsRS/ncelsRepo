alter table EMP_Contract add DeclarantIsManufactur bit null
alter table EMP_Contract add ChoosePayer nvarchar(20) null

alter table EMP_Contract add ContractType uniqueidentifier null
CONSTRAINT FK_EMP_Contract_ContractType_EMP_Ref_ContractType_Id
		 FOREIGN KEY ([ContractType])
		 REFERENCES [EMP_Ref_ContractType]([Id])

alter table OBK_DeclarantContact add BankAccount nvarchar(50) null

alter table OBK_DeclarantContact add BankId int null
CONSTRAINT FK_OBK_DeclarantContact_BankId_EMP_Ref_Bank_Id
		 FOREIGN KEY ([BankId])
		 REFERENCES [EMP_Ref_Bank]([Id])



