SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alexandr Zarichanskiy
-- Create date: 2017-05-14
-- Description:	Send data to Db 1C
-- =============================================
CREATE TRIGGER [dbo].[TR_SendToI1cActOnUpdate]
   ON  [dbo].[EXP_CertificateOfCompletion]
   AFTER UPDATE
AS 
BEGIN

	SET NOCOUNT ON;

	DECLARE @sended1CStatusId  uniqueidentifier;
	
	SELECT TOP 1 @sended1CStatusId = [Id]   
	FROM [dbo].[Dictionaries]
	WHERE [Type] = 'CertificateOfCompletionStatus' AND Code = 'sended' AND [ExpireDate] IS NULL;
	
	IF ((select TOP 1 StatusId from inserted) = @sended1CStatusId)
	BEGIN
		INSERT INTO [dbo].[I1c_act_Application]
           ([Id]
           ,[Number]
           ,[CreateDatetime]
           ,[TotalPrice]
           ,[ActNumber1C]
           ,[ActDate1C]
           ,[ExportDatetime]
           ,[ImportDatetime]
           ,[StageId]
           ,[StageCode]
           ,[StageValue]
           ,[refPrimaryApplication])
          SELECT 
				NEWID() AS [Id]
				,Number AS [Number]
				,SendDate AS [CreateDatetime]
				,TotalPrice AS [TotalPrice]
				,NULL AS [ActNumber1C]
				,NULL AS [ActDate1C]
				,GETDATE() AS [ExportDatetime]
				,NULL AS [ImportDatetime]
				,DicStageId AS [StageId]
				,Tds.Code AS [StageCode]
				,Tds.NameRu AS [StageValue]
				,Tdpdd.DirectionToPayId AS [refPrimaryApplication]
		  FROM inserted AS Tcoc
			INNER JOIN [dbo].[EXP_DIC_Stage] AS Tds ON Tcoc.DicStageId = Tds.Id
			INNER JOIN [dbo].[EXP_DirectionToPays_DrugDeclaration] AS Tdpdd ON Tdpdd.DrugDeclarationId = Tcoc.DrugDeclarationId
	END
END

GO

ALTER TABLE [dbo].[EXP_CertificateOfCompletion] ENABLE TRIGGER [TR_SendToI1cActOnUpdate]
GO


