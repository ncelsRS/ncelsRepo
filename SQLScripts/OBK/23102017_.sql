ALTER TABLE [OBK_ContractExtHistory]
ADD [EmployeeId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_OBK_ContractExtHistory_EmployeeId_Employees_Id
	FOREIGN KEY ([EmployeeId])
	REFERENCES [Employees] ([Id])