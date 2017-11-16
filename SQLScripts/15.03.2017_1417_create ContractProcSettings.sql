create table ContractProcSettings(
	Id uniqueidentifier not null,
	ProcCenterHeadId uniqueidentifier not null
	CONSTRAINT [PK_ContractProcSettings] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)
ALTER TABLE ContractProcSettings ADD  CONSTRAINT DF_ContractProcSettings_Id  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE ContractProcSettings  WITH CHECK ADD  CONSTRAINT [FK_ContractProcSettings_ProcCenterHeadId_Employees_Id] FOREIGN KEY(ProcCenterHeadId)
REFERENCES Employees (Id)
GO
ALTER TABLE ContractProcSettings CHECK CONSTRAINT [FK_ContractProcSettings_ProcCenterHeadId_Employees_Id]
GO