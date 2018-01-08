USE ncels
GO

IF DB_NAME() <> N'ncels' SET NOEXEC ON
GO

--
-- ������� ������� [dbo].[OBK_AssessmentStageOP]
--
PRINT (N'������� ������� [dbo].[OBK_AssessmentStageOP]')
GO
CREATE TABLE dbo.OBK_AssessmentStageOP (
  Id uniqueidentifier NOT NULL,
  DeclarationId uniqueidentifier NULL,
  StageStatus int NULL,
  CONSTRAINT PK_OBK_AssessmentStageOP_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

--
-- ������� ������� ���� [FK_OBK_AssessmentStageOP_DeclarationId] ��� ������� ���� ������� [dbo].[OBK_AssessmentStageOP]
--
PRINT (N'������� ������� ���� [FK_OBK_AssessmentStageOP_DeclarationId] ��� ������� ���� ������� [dbo].[OBK_AssessmentStageOP]')
GO
ALTER TABLE dbo.OBK_AssessmentStageOP
  ADD CONSTRAINT FK_OBK_AssessmentStageOP_DeclarationId FOREIGN KEY (DeclarationId) REFERENCES dbo.OBK_AssessmentDeclaration (Id)
GO

--
-- ������� ������� ���� [FK_OBK_AssessmentStageOP_StageStatus] ��� ������� ���� ������� [dbo].[OBK_AssessmentStageOP]
--
PRINT (N'������� ������� ���� [FK_OBK_AssessmentStageOP_StageStatus] ��� ������� ���� ������� [dbo].[OBK_AssessmentStageOP]')
GO
ALTER TABLE dbo.OBK_AssessmentStageOP
  ADD CONSTRAINT FK_OBK_AssessmentStageOP_StageStatus FOREIGN KEY (StageStatus) REFERENCES dbo.OBK_Ref_StageStatus (Id)
GO