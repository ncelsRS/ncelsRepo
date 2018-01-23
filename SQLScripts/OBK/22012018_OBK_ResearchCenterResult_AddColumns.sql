ALTER TABLE OBK_ResearchCenterResult ADD ExecutorId UNIQUEIDENTIFIER NULL
  CONSTRAINT FK_OBK_ResearchCenterResult_Employees FOREIGN KEY (ExecutorId) REFERENCES dbo.Employees (Id)
GO
ALTER TABLE OBK_ResearchCenterResult ADD ExpertiseResult bit NULL