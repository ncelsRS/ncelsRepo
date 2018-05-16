CREATE TABLE dbo.OBK_ResearchCenterResultCom (
  Id uniqueidentifier NOT NULL,
  ResearchCenterResultId uniqueidentifier NOT NULL,
  UserId uniqueidentifier NOT NULL,
  Note nvarchar(max) NULL,
  Createdate datetime NULL,
  CONSTRAINT PK_OBK_ResearchCenterResultCom PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE dbo.OBK_ResearchCenterResultCom
  ADD CONSTRAINT FK_OBK_ResearchCenterResultCom_Employees FOREIGN KEY (UserId) REFERENCES dbo.Employees (Id)
GO

ALTER TABLE dbo.OBK_ResearchCenterResultCom
  ADD CONSTRAINT FK_OBK_ResearchCenterResultCom_OBK_ResearchCenterResult FOREIGN KEY (ResearchCenterResultId) REFERENCES dbo.OBK_ResearchCenterResult (Id)
GO