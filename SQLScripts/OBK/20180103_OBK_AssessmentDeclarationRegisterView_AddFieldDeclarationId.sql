--
-- ������ ������������ Devart dbForge Studio for SQL Server, ������ 5.5.311.0
-- �������� �������� ��������: http://www.devart.com/ru/dbforge/sql/studio
-- ���� �������: 03.01.2018 17:11:07
-- ������ �������: 14.00.3008
-- ����������, ��������� ��������� ����� ����� ���� ����� �������� ����� �������
--

USE ncels
GO

IF DB_NAME() <> N'ncels' SET NOEXEC ON
GO

--
-- ������ ����������
--
BEGIN TRANSACTION
GO

--
-- �������� ������������� [dbo].[OBK_AssessmentDeclarationRegisterView]
--
GO
ALTER VIEW dbo.OBK_AssessmentDeclarationRegisterView 
AS SELECT      ad.Id AS DeclarationId, ad.StatusId, ad.Number, ad.FirstSendDate, d.NameRu AS DeclarantName, [as].StageStatusId, [as].StartDate, [as].EndDate, ase.ExecutorId, rt.NameRu AS RegType, [as].Id AS StageId, s.NameRu AS StatusName, 
                         ss.Code AS StageStatusCode, rs.Code AS StageCode, d.CountryId, dic.Name AS CountryNameRu, c.Number AS ContractNumber,
                             (SELECT        COUNT(*) AS Expr1
                               FROM            dbo.OBK_RS_Products AS rs
                               WHERE        (ContractId = c.Id)) AS ProductsCount, dbo.OBK_Procunts_Series.Series
FROM            dbo.OBK_AssessmentStage AS [as] INNER JOIN
                         dbo.OBK_AssessmentDeclaration AS ad ON [as].DeclarationId = ad.Id INNER JOIN
                         dbo.OBK_AssessmentStageExecutors AS ase ON [as].Id = ase.AssessmentStageId INNER JOIN
                         dbo.OBK_Declarant AS d INNER JOIN
                         dbo.OBK_Contract AS c ON d.Id = c.DeclarantId INNER JOIN
                         dbo.OBK_DeclarantContact AS dc ON c.DeclarantContactId = dc.Id ON ad.ContractId = c.Id INNER JOIN
                         dbo.OBK_Ref_Type AS rt ON ad.TypeId = rt.Id INNER JOIN
                         dbo.OBK_Ref_Status AS s ON ad.StatusId = s.Id INNER JOIN
                         dbo.OBK_Ref_StageStatus AS ss ON [as].StageStatusId = ss.Id INNER JOIN
                         dbo.OBK_Ref_Stage AS rs ON [as].StageId = rs.Id INNER JOIN
                         dbo.Dictionaries AS dic ON d.CountryId = dic.Id INNER JOIN
                         dbo.OBK_Procunts_Series ON rt.Id = dbo.OBK_Procunts_Series.Id
GO
IF @@ERROR<>0 OR @@TRANCOUNT=0 BEGIN IF @@TRANCOUNT>0 ROLLBACK SET NOEXEC ON END
GO

--
-- ����������� ����������
--
IF @@TRANCOUNT>0 COMMIT TRANSACTION
GO

--
-- ���������� NOEXEC � ��������� off
--
SET NOEXEC OFF
GO